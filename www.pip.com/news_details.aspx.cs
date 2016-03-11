using System;
using System.Web.UI;
using System.Data;
using System.Xml;
using System.Text.RegularExpressions;
using Ektron.Cms;
using Ektron.Cms.Framework.Content;
using SirSpeedy.CMS;
using Ektron.Cms.Content;
using Ektron.Cms.Common;
using Ektron.Cms.Instrumentation;
using System.Linq;
using System.IO;


public partial class news_details : System.Web.UI.Page
{

    protected DataTable NewsContent = new DataTable();
    /// <summary>
    /// page Init event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Init(object sender, EventArgs e)
    {
        cbNewsHeaderImage.DefaultContentID = ConfigHelper.GetValueLong("NewsDetailPageBannerImgCId");
        cbNewsHeaderImage.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbNewsHeaderImage.Fill();

        long aboutFfanchiseCId = ConfigHelper.GetValueLong("AboutFranchiseServices");
        cbAboutFranchise.DefaultContentID = aboutFfanchiseCId;
        cbAboutFranchise.CacheInterval = ConfigHelper.GetValueLong("smallCacheInterval");
        cbAboutFranchise.Fill();

        cbNewsSideContent.DefaultContentID = ConfigHelper.GetValueLong("NewsSideContentID");
        cbNewsSideContent.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbNewsSideContent.Fill();

        cbMediaInquires.DefaultContentID = ConfigHelper.GetValueLong("NewsMediaInquiresContentID");
        cbMediaInquires.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbMediaInquires.Fill();
    } 

    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {
            NewsContent = GetNewsContent();
            UxNews.DataSource = NewsContent;
            UxNews.DataBind();

            if (FransDataManager.IsFranchiseSelected())
            {
                Master.BodyClass += " local-news-link news ";
                desktopNavLocal.Visible = true;
                desktopNavNational.Visible = false;
            }
            else
            {
                Master.BodyClass += " news-link news ";
                desktopNavLocal.Visible = false;
                desktopNavNational.Visible = true;
            }
        }
    }

    private DataTable GetNewsContent()
    {
        DataTable DTSource = new DataTable();
        DataTable sortedDT = new DataTable();
        XmlDocument contentXML = new XmlDocument();
        string cid = Request.QueryString["id"];
        if (cid.Contains(",")) cid= cid.Substring(cid.IndexOf(",", System.StringComparison.Ordinal)+1, cid.Length-cid.IndexOf(",", System.StringComparison.Ordinal)-1);

        long contentId = Convert.ToInt64(cid);
        int counter = 0;

        DTSource.Columns.Add("title");
        DTSource.Columns.Add("date");
        DTSource.Columns.Add("rawDate");
        DTSource.Columns.Add("city");
        DTSource.Columns.Add("state");
        DTSource.Columns.Add("teaser");
        DTSource.Columns.Add("content1");
        DTSource.Columns.Add("content2");
        DTSource.Columns.Add("pullquote");
        DTSource.Columns.Add("imgSRC");
        DTSource.Columns.Add("imgTag");
        DTSource.Columns.Add("url");
        DTSource.Columns.Add("counter");

        DataTable dt = DTSource.Clone();

        var contents = SiteDataManager.GetNews();
        if (contents != null && contents.Count > 0)
        {
            foreach (Ektron.Cms.ContentData contentData in contents)
            {
                try
                {
                    contentXML.LoadXml(contentData.Html);
                    XmlNodeList xnList = contentXML.SelectNodes("/root");
                    string title = xnList[0]["title"].InnerXml;
                    string date = xnList[0]["date"].InnerXml;
                    string city = xnList[0]["city"].InnerXml;
                    string state = xnList[0]["state"].InnerXml;
                    string teaser = xnList[0]["teaser"].InnerXml;
                    string content = xnList[0]["content"].InnerXml;

                    //separating content to insert pull quote
                    string content1 = string.Empty;
                    string content2 = string.Empty;
                    try
                    {
                        content1 = "<p>" + GetFirstParagraph(content) + "</p>";
                        string temp = content.Substring(content1.Length);
                        string contenttemp = GetFirstParagraph(temp);
                        if (!String.IsNullOrWhiteSpace(contenttemp))
                        {
                            content1 += "<p>" + contenttemp + "</p>";
                        }

                        if (!string.IsNullOrEmpty(content))
                            content2 = content.Substring(content1.Length);
                    }
                    catch (Exception ex)
                    {
                        Log.WriteError(ex);
                    }
                    //get pull quote
                    string pullquote = xnList[0]["pullquote"].InnerXml;

                    if (!String.IsNullOrWhiteSpace(pullquote))
                    {
                        pullquote = "<blockquote>" + pullquote + "</blockquote>";
                    }

                    DateTime tempdate = Convert.ToDateTime(date);
                    string newFormattedDate = tempdate.ToString("MMMM dd, yyyy");

                    string xml = contentXML.SelectSingleNode("/root/image").InnerXml;
                    string imgSRC = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;

                    string imgTag = String.Empty;
                    if (!String.IsNullOrEmpty(imgSRC))
                    {
                        imgTag = "<img class=\"news_img\" src=\"" + imgSRC + "\">";
                    }

                    xml = contentXML.SelectSingleNode("/root/url").InnerXml;
                    string url = Regex.Match(xml, "<a.+?href=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value; //not sure about this
                    counter++;
                    if (contentData.Id.Equals(contentId))
                    {
                        dt.Rows.Add(title, newFormattedDate, date, city, state, teaser, content1, content2, pullquote, imgSRC, imgTag, url, counter);
                    }
                    else
                    DTSource.Rows.Add(title, newFormattedDate, date, city, state, teaser, content1, content2, pullquote, imgSRC, imgTag, url, counter);
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }

                DataView DVPSMMSort = DTSource.DefaultView;
                DVPSMMSort.Sort = "rawDate desc";
                sortedDT = DVPSMMSort.ToTable();
            }
            dt.Merge(sortedDT);
        }
        return dt;
    }

    private string GetFirstParagraph(string htmltext)
    {
        Match m = Regex.Match(htmltext, @"<p>\s*(.+?)\s*</p>");
        if (m.Success)
        {
            return m.Groups[1].Value;
        }
        else
        {
            return htmltext;
        }
    }
}