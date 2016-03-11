using System;
using System.Linq;
using System.Web.UI;
using System.Data;
using TeamLogic.CMS;
using System.Xml;
using System.Text;
using System.Xml.XPath;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using Ektron.Cms.Instrumentation;

public partial class briefs_whitepapers : PageBase
{
    public string HowWeCanHelpBackgroundImage = "/images/how_we_can_help_bkg.jpg";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            lvbBriefAndWhitepapers.DataSource = GetBriefsWHitepaperContent();
            lvbBriefAndWhitepapers.DataBind();

            GetSupplementOutsourcingContent();
        }
    }

    private DataTable GetBriefsWHitepaperContent()
    {
        DataTable DTSource = new DataTable();
        DataTable sortedDT = new DataTable();
        XmlDocument contentXML = new XmlDocument();
        int counter = 0;

        DTSource.Columns.Add("title");
        DTSource.Columns.Add("abstract");
        DTSource.Columns.Add("content");
        DTSource.Columns.Add("url");
        DTSource.Columns.Add("hrefText");
        DTSource.Columns.Add("imgSRC");
        DTSource.Columns.Add("dateCreated");
        DTSource.Columns.Add("counter");


        var contents = SiteDataManager.GetBriefsAndWhitePapers();
        if (contents != null && contents.Count > 0)
        {
            foreach (Ektron.Cms.ContentData contentData in contents)
            {
                try
                {
                    contentXML.LoadXml(contentData.Html);

                    string contentID = contentData.Id.ToString();
                    string hrefText = contentData.Quicklink;

                    string dateCreated = contentData.DateModified.ToString();

                    XmlNodeList xnList = contentXML.SelectNodes("/root");
                    string title = xnList[0]["title"].InnerXml;
                    string abstractText = xnList[0]["abstract"].InnerXml;
                    string content = xnList[0]["content"].InnerXml;
                    string xml = contentXML.SelectSingleNode("/root/image").InnerXml;
                    string imgSRC = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                    xml = contentXML.SelectSingleNode("/root/url").InnerXml;
                    string url = Regex.Match(xml, "<a.+?href=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value; //not sure about this
                    counter++;

                    DTSource.Rows.Add(title, abstractText, content, url, hrefText, imgSRC, dateCreated, counter);
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }
            DataView DVPSMMSort = DTSource.DefaultView;
            DVPSMMSort.Sort = "dateCreated desc";
            sortedDT = DVPSMMSort.ToTable();
        }
        return sortedDT;
    }

    private void GetSupplementOutsourcingContent()
    {
        long supplementCId = ConfigHelper.GetValueLong("BriefsWhitePapersHowCanWeHelpYouCId");
        if (supplementCId > 0)
        {
            var cData = ContentHelper.GetContentById(supplementCId);
            if (cData != null && cData.Html != string.Empty)
            {
                if (!string.IsNullOrEmpty(cData.Image) && cData.Image.ToLower().IndexOf("workarea") <= -1)
                {
                    HowWeCanHelpBackgroundImage = "/" + cData.Image;
                    how_we_can_help_Img.Attributes.Add("data-image", HowWeCanHelpBackgroundImage);
                    how_we_can_help_Img.Attributes.Add("data-image-mobile", HowWeCanHelpBackgroundImage);
                }

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