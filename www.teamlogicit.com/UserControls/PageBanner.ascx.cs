using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TeamLogic.CMS;
using Ektron.Cms;
using System.Text.RegularExpressions;
using Ektron.Cms.Instrumentation;
using System.Text;

public partial class UserControls_PageBanner : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (FransDataManager.IsFranchiseSelected())
            {
                long localMainNavId = ConfigHelper.GetValueLong("MainNavLocalId");
                this.GetSubNavItems(localMainNavId);
            }
            else
            {
                long nationalMainNavId = ConfigHelper.GetValueLong("MainNavNationalId");
                this.GetSubNavItems(nationalMainNavId);
            }
        }        

        if (!Page.IsPostBack)
        {
            this.LoadPageBanner();
        }
    }

    private void LoadPageBanner()
    {
        try
        {
            long pageBannerId = 0;
            long cid = 0;

            //as per client no banner for contact us.
            string url = Request.RawUrl;
            if (url.ToLower().Contains("contact-us"))
                return;

            if (url.ToLower().Contains("environmental-message") ||
                url.ToLower().Contains("privacy-policy") ||
                url.ToLower().Contains("terms-and-conditions") ||
                url.ToLower().Contains("thank-you") || url.ToLower().Contains("requestconsultation.aspx"))
            {
                pnlPageBanner.Visible = false;
                pnlNoBanner.Visible = true;
                return;
            }           


            if (!string.IsNullOrEmpty(Request.QueryString["sid"]))
            {
                //check if banner is assigned to content
                cid = Convert.ToInt64(Request.QueryString["sid"]);
                if (cid > 0)
                {
                    pageBannerId = GetBannerIdFromContentId(cid);
                }

                //if no banner is assigned to content then check if banner is assigned to a page
                if ((pageBannerId == 0) && (!string.IsNullOrEmpty(Request.QueryString["id"])))
                {
                    cid = Convert.ToInt64(Request.QueryString["id"]);
                    if (cid > 0)
                    {
                        pageBannerId = GetBannerIdFromContentId(cid);
                    }
                }
            }
            else if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                cid = Convert.ToInt64(Request.QueryString["id"]);
                if (cid > 0)
                {
                    pageBannerId = GetBannerIdFromContentId(cid);
                }
            }
            

            //get default banner id if selection is not found
            if (pageBannerId == 0)
                pageBannerId = ConfigHelper.GetValueLong("DefaultPageBannerID");

            if (pageBannerId > 0)
            {
                pnlPageBanner.Visible = true;
                ContentData bannerContentData = ContentHelper.GetContentById(pageBannerId);
                if (bannerContentData != null && bannerContentData.Html != string.Empty)
                {
                    XmlDocument contentXML = new XmlDocument();
                    contentXML.LoadXml(bannerContentData.Html);
                    XmlNodeList xnList = contentXML.SelectNodes("/root");

                    if (contentXML.SelectSingleNode("/root/link/a") != null)
                    {
                        string link = contentXML.SelectSingleNode("/root/link/a").Attributes["href"].Value;
                        string target = "_self";
                        if (contentXML.SelectSingleNode("/root/link/a").Attributes["target"] != null)
                            target = contentXML.SelectSingleNode("/root/link/a").Attributes["target"].Value;
                        title.Text = "<a target=\"" + target + "\" href=\"" + link + "\">" + xnList[0]["title"].InnerXml + "</a>";
                    }
                    else
                    {
                        title.Text = xnList[0]["title"].InnerXml;
                    }

                    if (xnList[0]["subTitle"] != null)
                    {
                        if (xnList[0]["subTitle"].InnerXml != string.Empty)
                            desc.Text = "<p>" + xnList[0]["subTitle"].InnerXml + "</p>";
                    }                    
                    string showShare = xnList[0]["showPrintShareBtn"].InnerXml;
                    string imagexml = contentXML.SelectSingleNode("/root/image").InnerXml;
                    string imgSRC = Regex.Match(imagexml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                    if (!string.IsNullOrEmpty(imgSRC))
                    {
                        bannerImage.Text = "<img src=\"" + imgSRC + "\">";
                        bannerImage.Visible = true;
                    }

                    string bgImageXML = contentXML.SelectSingleNode("/root/backGroundImage").InnerXml;
                    string bgimgSRC = Regex.Match(bgImageXML, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                    bgImage.Text = "<div data-height=\"892\" data-width=\"1800\" data-image=\"" + bgimgSRC + "\" data-image-mobile=\"" + bgimgSRC + "\" class=\"img-holder\" style=\"visibility: hidden; height: 580px;\"></div>";

                    if (showShare.Equals("true"))
                        pnlShare.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            Log.WriteError(ex);
        }
    }
      
    private void GetSubNavItems(long id)
    {
        mainNav.DefaultMenuID = id;
        mainNav.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        mainNav.Fill();

        try
        {
            if (mainNav.XmlDoc != null)
            {
                string strmenuXml = mainNav.XmlDoc.InnerXml;
                string rawURL = Request.RawUrl.ToLower();
                string selectedNavTitle = "about-desktop";
                StringBuilder sb = new StringBuilder();
                StringBuilder sbHtml = new StringBuilder();
                XDocument xDoc = XDocument.Parse(strmenuXml);

                var xnList = xDoc.XPathSelectElements("/MenuDataResult/Item/Item");
                foreach (var c in xnList)
                {
                    if (c.XPathSelectElement("Menu/Description") != null)
                    {
                        string menuDescription = c.XPathSelectElement("Menu/Description").Value;
                        if (menuDescription.ToLower().IndexOf("subnav") > -1)
                        {
                            //che ck if IT-Solution detail page is selected
                            if (c.XPathSelectElement("Menu/MenuSelected").Value == "true" &&
                                menuDescription.ToLower().IndexOf("itsolutions") > -1 &&
                                rawURL.ToLower().EndsWith("it-solutions/") == false)
                            {
                                ///sub-navs are being populated from it solution detail template
                                var contents = SiteDataManager.GetCompleteSolutionsContent();
                                if (contents != null && contents.Any())
                                {
                                    long contentId = 0;
                                    int counter = 1;
                                    if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                                        long.TryParse(Request.QueryString["id"], out contentId);

                                    sb.Append("<ul id=\"about-desktop-nav\" class=\"it_solutions_subnav\">");
                                    foreach (Ektron.Cms.ContentData contentData in contents)
                                    {
                                        string className = contentData.Id == contentId ? "active" : "";
                                        if (contentData.Id == contentId)
                                            selectedNavTitle = "it-solution-" + counter;

                                        if (contents.Count <= 4)
                                        {
                                            sb.Append("<li class=\"" + className + " it-solution-1-nav-link\">");
                                            sb.Append("<a class=\"" + className + "\" href=\"" + contentData.Quicklink + "\">" + contentData.Title + "</a>");
                                        }
                                        if (contents.Count > 4)
                                        {
                                            if (counter == 5)
                                            {
                                                sb.Append("<li id=\"more_drop\" class=\"it-solution-7-nav-link\"><a href=\"#\">More</a>");
                                                sb.Append("<ul class=\"more_dropdown dropit-submenu\">");
                                            }

                                            sb.Append("<li class=\"" + className + " it-solution-" + counter + "-nav-link\">");
                                            sb.Append("<a class=\"" + className + "\" href=\"" + contentData.Quicklink + "\">" + contentData.Title + "</a>");
                                            sb.Append("</li>");

                                            if (counter == contents.Count)
                                            {
                                                sb.Append("</ul></li>");
                                            }
                                        }
                                        else
                                        {
                                            sb.Append("</li>");
                                        }
                                        counter++;
                                    }
                                    sb.Append("</ul>");
                                }
                            }//about us content is selected
                            else if (c.XPathSelectElement("Menu/MenuSelected").Value == "true" && menuDescription.ToLower().IndexOf("aboutus") > -1)
                            {
                                var xmlNodes = c.XPathSelectElements("Menu/Item");
                                if (xmlNodes != null && xmlNodes.Any())
                                {
                                    sb.Append("<ul id=\"about-desktop-nav\">");
                                    foreach (var subNodes in xmlNodes)
                                    {
                                        if (subNodes.XPathSelectElement("Menu/Description").Value.IndexOf("aboutus") > -1)
                                        {
                                            selectedNavTitle = subNodes.XPathSelectElement("Menu/Title").Value.ToLower().Replace(" ", "");
                                            sb.Append("<li class=\"active " + subNodes.XPathSelectElement("Menu/Description").Value + "\"><a class=\"active\" href=\"" + subNodes.XPathSelectElement("Menu/Link").Value + "\">" + subNodes.XPathSelectElement("Menu/Title").Value + "</a></li>");
                                        }
                                        else
                                        {                                            
                                            sb.Append("<li class=\"" + subNodes.XPathSelectElement("Menu/Description").Value + "\"><a class=\"\" href=\"" + subNodes.XPathSelectElement("Menu/Link").Value + "\">" + subNodes.XPathSelectElement("Menu/Title").Value + "</a></li>");
                                        }
                                    }
                                    sb.Append("</ul>");
                                }
                            }
                            else if (c.XPathSelectElement("Menu/MenuSelected").Value == "true")
                            {
                                var xmlNodes = c.XPathSelectElements("Menu/Item");
                                bool isChildMenuSelected = Menuselected(xmlNodes);
                                if (xmlNodes != null && xmlNodes.Any())
                                {
                                    sb.Append("<ul id=\"about-desktop-nav\">");
                                    int count = 1;
                                    foreach (var subNodes in xmlNodes)
                                    {                                        
                                        if (subNodes.XPathSelectElement("Menu/MenuSelected").Value == "true")
                                        {
                                            selectedNavTitle = subNodes.XPathSelectElement("Menu/Title").Value.ToLower().Replace(" ", "");
                                            sb.Append("<li class=\"active " + subNodes.XPathSelectElement("Menu/Description").Value + "\"><a class=\"active\" href=\"" + subNodes.XPathSelectElement("Menu/Link").Value + "\">" + subNodes.XPathSelectElement("Menu/Title").Value + "</a></li>");
                                        }
                                        else
                                        {
                                            if (count == 1 && isChildMenuSelected == false)
                                            {
                                                //temp hack to make briefs & white ppaer selected bez resources is also using the same link
                                                selectedNavTitle = subNodes.XPathSelectElement("Menu/Title").Value.ToLower().Replace(" ", "");
                                                sb.Append("<li class=\"active " + subNodes.XPathSelectElement("Menu/Description").Value + "\"><a class=\"\" href=\"" + subNodes.XPathSelectElement("Menu/Link").Value + "\">" + subNodes.XPathSelectElement("Menu/Title").Value + "</a></li>");
                                            }
                                            else
                                            {
                                                sb.Append("<li class=\"" + subNodes.XPathSelectElement("Menu/Description").Value + "\"><a class=\"\" href=\"" + subNodes.XPathSelectElement("Menu/Link").Value + "\">" + subNodes.XPathSelectElement("Menu/Title").Value + "</a></li>");
                                            }
                                        }
                                        count++;
                                    }
                                    sb.Append("</ul>");
                                }
                            }
                            else
                            {
                                var xmlNodes = c.XPathSelectElements("Menu/Item");                               
                                bool isMenuSelected = Menuselected(xmlNodes);
                                if (isMenuSelected)
                                {
                                    sb.Append("<ul id=\"about-desktop-nav\">");
                                    foreach (var subNodes in xmlNodes)
                                    {                                        
                                        if (subNodes.XPathSelectElement("Menu/MenuSelected").Value == "true")
                                        {
                                            selectedNavTitle = subNodes.XPathSelectElement("Menu/Title").Value.ToLower().Replace(" ", "");
                                            sb.Append("<li class=\"active " + subNodes.XPathSelectElement("Menu/Description").Value + "\"><a class=\"active\" href=\"" + subNodes.XPathSelectElement("Menu/Link").Value + "\">" + subNodes.XPathSelectElement("Menu/Title").Value + "</a></li>");
                                        }
                                        else
                                        {                                           
                                            sb.Append("<li class=\"" + subNodes.XPathSelectElement("Menu/Description").Value + "\"><a class=\"\" href=\"" + subNodes.XPathSelectElement("Menu/Link").Value + "\">" + subNodes.XPathSelectElement("Menu/Title").Value + "</a></li>");
                                        }
                                    }
                                    sb.Append("</ul>");
                                }
                            }
                        }
                    }
                }
                if(sb.Length > 0)
                {
                    sbHtml.Append("<div id=\"" + selectedNavTitle + "-nav\" class=\"bottom_header bottom_subnav clearfix\"> ");
                    sbHtml.Append("<div class=\"container_24\">");
                    sbHtml.Append("<div id=\"mobile-nav-header\" class=\"lvl-2-title-wrap\">");
                    sbHtml.Append("<a id=\"page-title\" class=\"lvl-2-title\" href=\"#\"></a>");
                    sbHtml.Append("<a class=\"arrow-plus-minus\" href=\"#\"> </a>");
                    sbHtml.Append("</div>");
                    sbHtml.Append(sb.ToString());
                    sbHtml.Append("</div>");
                    sbHtml.Append("</div>");                    
                }
                ltrMenuItems.Text = sbHtml.ToString();                
            }
        }
        catch(Exception ex)
        {
            Log.WriteError(ex);
        }
    }

    private bool Menuselected(IEnumerable<XElement> xNodes)
    {
        bool status = false;
        var items = from item in xNodes.Descendants("Menu")
                    where item.Element("MenuSelected").Value == "true"
                    select new
                    {
                        menuStatus = item.Element("MenuSelected").Value
                    };

        foreach (var item in items)
        {
            if (item.menuStatus == "true")
                status = true;
        }       
        
        return status;
    }

    private long GetBannerIdFromContentId(long contentId)
    {
        long bannerId = 0;
        if (contentId > 0)
        {
            long pageBannerMetaDataId = ConfigHelper.GetValueLong("PageBannerMetaDataId");
            long pageBannerSmartFormId = ConfigHelper.GetValueLong("PageBannerSmartFormId");
            ContentData contentData = ContentHelper.GetContentById(contentId, true);
            if (contentData != null && contentData.Id > 0 && contentData.MetaData != null)
            {
                Ektron.Cms.ContentMetaData[] contentMetaDataArray = contentData.MetaData;
                foreach (Ektron.Cms.ContentMetaData contentMetaData in contentMetaDataArray)
                {
                    if (contentMetaData.Id == pageBannerMetaDataId)
                        if (!contentMetaData.Text.Equals(""))
                            bannerId = Convert.ToInt64(contentMetaData.Text);
                }
            }
            
            //if banner id is still 0 then check if the content belong to one of the following folders
            //news, case-studies, whitepaper, IT solution, project expert
            if(bannerId == 0)
            {
                if (contentData != null && contentData.Id > 0)
                {
                    if (contentData.FolderId == ConfigHelper.GetValueLong("NewsFolderID"))
                    {
                        bannerId = ConfigHelper.GetValueLong("NewsDetailBannerCId");
                    }
                    else if (contentData.FolderId == ConfigHelper.GetValueLong("CaseStudiesFolderID"))
                    {
                        bannerId = ConfigHelper.GetValueLong("CaseStudyDetailBannerCId");
                    }
                    else if (contentData.FolderId == ConfigHelper.GetValueLong("AllBriefsAndWhitepapersFolderID"))
                    {
                        bannerId = ConfigHelper.GetValueLong("WhitePapersDetailBannerCId");
                    }
                    else if (contentData.FolderId == ConfigHelper.GetValueLong("ITSolutionContentFolderID"))
                    {
                        bannerId = ConfigHelper.GetValueLong("ITSolutionsDetailBannerCId");
                    }
                    else if (contentData.FolderId == ConfigHelper.GetValueLong("ConsultingAndProjectsContentFId"))
                    {
                        bannerId = ConfigHelper.GetValueLong("ConsultingAndProjectsDetailBannerCId");
                    }                    
                }
            }

        }
        return bannerId;
    }   
}