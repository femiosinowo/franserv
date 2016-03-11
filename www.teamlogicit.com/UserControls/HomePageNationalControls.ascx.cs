using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using TeamLogic.CMS;
using Ektron.Cms.Instrumentation;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Text;

public partial class UserControls_HomePageNationalControls : System.Web.UI.UserControl
{
    public string WhyWorkBackgroundImage = "/images/why_work_bg.jpg";
    public string JoinTeamBackgroundImage = "/images/join_team_bg.jpg";
    public string ITInflectionBackgroundImage = "/images/briefs_whitepapers_bg.jpg";

    /// <summary>
    /// Page init event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Init(object sender, EventArgs e)
    {
        cbFindLocation.DefaultContentID = ConfigHelper.GetValueLong("HomePageFindLocationCId");
        cbFindLocation.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbFindLocation.Fill();

        cbITInflectionImg.DefaultContentID = ConfigHelper.GetValueLong("HomePageITInflectionsImgCId");
        cbITInflectionImg.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbITInflectionImg.Fill();

        cbJoinTeam.DefaultContentID = ConfigHelper.GetValueLong("HomePageJoinOurTeamCId");
        cbJoinTeam.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbJoinTeam.Fill();

        if (cbJoinTeam.EkItem != null && !string.IsNullOrEmpty(cbJoinTeam.EkItem.Image) && cbJoinTeam.EkItem.Image.ToLower().IndexOf("workarea") <= -1)
            JoinTeamBackgroundImage = cbJoinTeam.EkItem.Image;

        if (cbITInflectionImg.EkItem != null && !string.IsNullOrEmpty(cbITInflectionImg.EkItem.Image) && cbITInflectionImg.EkItem.Image.ToLower().IndexOf("workarea") <= -1)
            ITInflectionBackgroundImage = cbITInflectionImg.EkItem.Image;  
    }

    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.GetITSourceContent();
            this.GetWorkWithUsContent();
            this.GetRSSFeed();
            this.GetBriefsWhitePapers();
        }

        why_work.Attributes.Add("data-image", WhyWorkBackgroundImage);
        why_work.Attributes.Add("data-image-mobile", WhyWorkBackgroundImage);
        briefs_whitepapers.Attributes.Add("data-image", ITInflectionBackgroundImage);
        briefs_whitepapers.Attributes.Add("data-image-mobile", ITInflectionBackgroundImage);
        join_team.Attributes.Add("data-image", JoinTeamBackgroundImage);
        join_team.Attributes.Add("data-image-mobile", JoinTeamBackgroundImage);
        
    }

    protected void btnFindLocation_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string address = txtAddress.Text.Trim();
            string distance = ddlDistance.SelectedValue;
            Response.Redirect("/locations/?location=" + address + "&distance=" + distance + "");
        }
    }

    private void GetITSourceContent()
    {
        long itSourcesCId = ConfigHelper.GetValueLong("HomePageNationalOurServicesCId");
        var cData = ContentHelper.GetContentById(itSourcesCId);
        if (cData != null && cData.Html != string.Empty)
        {
            try
            {
                XDocument xDoc = XDocument.Parse(cData.Html);               

                ltrSectionTitle.Text = xDoc.XPathSelectElement("/root/sectionTitle").Value;
                var xelements = xDoc.XPathSelectElements("/root/Content");
                if (xelements != null && xelements.Any())
                {
                    var itSolutionItems = from obj in xelements
                                          select new
                                          {
                                              Title = obj.XPathSelectElement("title") != null ? obj.XPathSelectElement("title").Value : string.Empty,
                                              SubTitle = obj.XPathSelectElement("description") != null ? obj.XPathSelectElement("description").ToString() : string.Empty,
                                              ImagePath = obj.XPathSelectElement("image/img") != null ? obj.XPathSelectElement("image/img").Attribute("src").Value : string.Empty,
                                              Link = obj.XPathSelectElement("link/a") != null ? obj.XPathSelectElement("link/a").Attribute("href").Value : "#"
                                          };

                    lvOurServices.DataSource = itSolutionItems;
                    lvOurServices.DataBind();
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
        }
    }

    private void GetWorkWithUsContent()
    {
        long itSourcesCId = ConfigHelper.GetValueLong("HomePageWhyWorkWithUsCId");
        var cData = ContentHelper.GetContentById(itSourcesCId);
        if (cData != null && cData.Html != string.Empty)
        {
            try
            {
                if (!string.IsNullOrEmpty(cData.Image) && cData.Image.ToLower().IndexOf("workarea") <= -1)
                    WhyWorkBackgroundImage = "/" + cData.Image;

                StringBuilder sb = new StringBuilder();
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(cData.Html);

                sb.Append("<div class=\"why_work_content caption\">");
                if (xDoc.SelectSingleNode("/root/title") != null)
                    sb.Append(xDoc.SelectSingleNode("/root/title").InnerXml);
                sb.Append("<div class=\"clear_text\"></div>");
                if (xDoc.SelectSingleNode("/root/description") != null)
                    sb.Append("<p>" + xDoc.SelectSingleNode("/root/description").InnerXml + "</p>");

                string link = xDoc.SelectSingleNode("/root/link/a") != null ? xDoc.SelectSingleNode("/root/link/a").Attributes["href"].Value : "#";
                sb.Append(" <a href=\"" + link + "\">Learn More</a>");
                sb.Append("</div>");
                if (xDoc.SelectSingleNode("/root/videoLink") != null)
                    sb.Append("<div class=\"why_work_video\"><iframe src=\"" + xDoc.SelectSingleNode("/root/videoLink").InnerXml + "\" frameborder=\"0\" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe></div>");

                ltrWorkWithUs.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
        }
    }
    
    private void GetRSSFeed()
    {
        var blogsData = BlogsDataManager.GetRssFeed(4);
        lvITInflectionRssFeed.DataSource = blogsData;
        lvITInflectionRssFeed.DataBind();
    }

    private void GetBriefsWhitePapers()
    {
        string domainName = Request.ServerVariables["SERVER_NAME"];
        var briefsWhitePaperData = SiteDataManager.GetBriefsAnsWhitePapersForHomePage(2);
        if(briefsWhitePaperData != null && briefsWhitePaperData.Any())
        {
            try
            {
                var contentData = from obj in briefsWhitePaperData
                                  select new
                                  {
                                      Id = obj.Id,
                                      Quicklink = obj.Quicklink,
                                      Html = XElement.Parse(obj.Html)
                                  };

                var contentHtml = from obj in contentData
                                  select new
                                  {
                                      Link = obj.Quicklink,
                                      ImagePath = obj.Html.XPathSelectElement("/image/img") != null ? obj.Html.XPathSelectElement("/image/img").Attribute("src").Value : string.Empty,
                                      ImageTitle = obj.Html.XPathSelectElement("/image/img") != null ? obj.Html.XPathSelectElement("/image/img").Attribute("alt").Value : string.Empty,
                                      Title = obj.Html.XPathSelectElement("/title") != null ? obj.Html.XPathSelectElement("/title").Value : string.Empty,
                                      Teaser = obj.Html.XPathSelectElement("/teaserMM") != null ? obj.Html.XPathSelectElement("/teaserMM").Value : string.Empty
                                  };
                lvBriefsWhitePapers.DataSource = contentHtml;
                lvBriefsWhitePapers.DataBind();
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
        }        
    }
    
    public string FormatTitle(string title)
    {
        string formattedTitle = title;
        if (!string.IsNullOrEmpty(title))
        {
            if(title.Length > 78)
            {
                formattedTitle = title.Substring(0, 78) + "...";
            }
        }
        return formattedTitle;
    }
    
}

