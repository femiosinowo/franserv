using System;
using System.Linq;
using System.Web.UI;
using System.Data;
using TeamLogic.CMS;
using System.Xml;
using System.Text.RegularExpressions;
using Ektron.Cms.Instrumentation;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Text;

public partial class case_studies : PageBase
{
    private int defaultCount = 10;
    public string HowWeCanHelpBackgroundImage = "/images/how_we_can_help_bkg.jpg";

    protected void Page_Init(object sender, EventArgs e)
    {
        cbIntro.DefaultContentID = ConfigHelper.GetValueLong("CaseStudiesIntroTextCId");
        cbIntro.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbIntro.Fill();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (GetCaseStudiesContent().Rows.Count < defaultCount)
            loadMoreNews.Visible = false;         

        if (!Page.IsPostBack)
        {
            DataTable test = GetCaseStudiesContent();
            lvCaseStudiesPhotos.DataSource = GetCaseStudiesContent();
            lvCaseStudiesPhotos.DataBind();

            GetSupplementOutsourcingContent();
            how_we_can_help_img.Attributes.Add("data-image", HowWeCanHelpBackgroundImage);
            how_we_can_help_img.Attributes.Add("data-image-mobile", HowWeCanHelpBackgroundImage); 
        }
    }

    protected void LoadMoreLinkButton_Click(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DataTable test = GetCaseStudiesContent();
            lvCaseStudiesPhotos.DataSource = GetCaseStudiesContent();
            lvCaseStudiesPhotos.DataBind();
        }
    }

    private DataTable GetCaseStudiesContent()
    {
        DataTable DTSource = new DataTable();
        DataTable sortedDT = new DataTable();
        XmlDocument contentXML = new XmlDocument();
        int counter = 0;

        DTSource.Columns.Add("Id");
        DTSource.Columns.Add("title");
        DTSource.Columns.Add("desc");
        DTSource.Columns.Add("url");
        DTSource.Columns.Add("imgSRC");
        DTSource.Columns.Add("iconimgSRC");
        DTSource.Columns.Add("counter");
        DTSource.Columns.Add("hreftext");
        DTSource.Columns.Add("cssClass");
        DTSource.Columns.Add("dateCreated");

        var contents = SiteDataManager.GetCaseStudies();
        if (contents != null && contents.Count > 0)
        {
            foreach (Ektron.Cms.ContentData contentData in contents)
            {
                try
                {
                    contentXML.LoadXml(contentData.Html);

                    string contentId = contentData.Id.ToString();
                    string hreftext = contentData.Quicklink; 
                    string dateCreated = contentData.DateModified.ToString();

                    XmlNodeList xnList = contentXML.SelectNodes("/root");
                    string title = xnList[0]["title"].InnerXml;
                    string desc = xnList[0]["desc"].InnerXml;
                    
                    string xml = contentXML.SelectSingleNode("/root/image").InnerXml;
                    string imgSRC = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;

                    string iconxml = contentXML.SelectSingleNode("/root/iconImage").InnerXml;
                    string iconimgSRC = Regex.Match(iconxml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;

                    

                    string url = hreftext; // contentData.Quicklink;
                    counter++;

                    string cssClass = "";
                    //if (counter == 1 || ((((counter - 1) % 4)) == 0))
                    //{
                    //    cssClass = "alpha";
                    //}
                    //else if (counter % 4 == 0)
                    //{
                    //    cssClass = "omega";
                    //}

                    DTSource.Rows.Add(contentId, title, desc, url, imgSRC,iconimgSRC, counter, hreftext, cssClass, dateCreated);
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }
            DataView DVPSMMSort = DTSource.DefaultView;
            DVPSMMSort.Sort = "counter asc"; //dateCreated
            sortedDT = DVPSMMSort.ToTable();
        }
        return sortedDT;
    }

    private void GetSupplementOutsourcingContent()
    {
        long supplementCId = ConfigHelper.GetValueLong("CaseStudiesIHowCanWeHelpYouCId");
        if (supplementCId > 0)
        {
            var cData = ContentHelper.GetContentById(supplementCId);
            if (cData != null && cData.Html != string.Empty)
            {
                HowWeCanHelpBackgroundImage = "/" + cData.Image;
                try
                {
                    XDocument xDoc = XDocument.Parse(cData.Html);
                    var xelements = xDoc.XPathSelectElements("/root/Content");
                    StringBuilder sb = new StringBuilder();
                    if (xelements != null && xelements.Any())
                    {
                        var firstElement = xelements.ElementAt(0);
                        var secondElement = xelements.ElementAt(1);
                        if (xDoc.XPathSelectElement("/root/sectionTitle") != null)
                            sb.Append("<div class=\"how_we_can_help_content\"><h2>" + xDoc.XPathSelectElement("/root/sectionTitle").Value + "</h2></div>");
                        sb.Append(" <div class=\"clear\"></div>");

                        sb.Append("<div class=\"bottom_header clearfix\">");
                        sb.Append(" <div class=\"container_24\">");

                        if (firstElement != null)
                        {
                            string link = firstElement.XPathSelectElement("link/a") != null ? firstElement.XPathSelectElement("link/a").Attribute("href").Value : "#";
                            string imagePath = firstElement.XPathSelectElement("image/img") != null ? firstElement.XPathSelectElement("image/img").Attribute("src").Value : "";
                            sb.Append("<div class=\"grid_12 left_choice\"><a href=\"" + link + "\"><img src=\"" + imagePath + "\" /><span>" + firstElement.XPathSelectElement("title").Value + "</span></a></div>");
                        }
                        if (secondElement != null)
                        {
                            string link = secondElement.XPathSelectElement("link/a") != null ? secondElement.XPathSelectElement("link/a").Attribute("href").Value : "#";
                            string imagePath = secondElement.XPathSelectElement("image/img") != null ? secondElement.XPathSelectElement("image/img").Attribute("src").Value : "";
                            sb.Append("<div class=\"grid_12 right_choice\"><a href=\"" + link + "\"><img src=\"" + imagePath + "\"><span>" + secondElement.XPathSelectElement("title").Value + "</span></a></div>");
                        }
                        sb.Append("</div>");
                        sb.Append("</div>");
                        ltrSupplementOutSourcing.Text = sb.ToString();
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }
        }
    }
}