using System;
using System.Web.UI;
using System.Data;
using System.Xml;
using System.Text.RegularExpressions;
using Ektron.Cms;
using Ektron.Cms.Framework.Content;
using SignalGraphics.CMS;
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

        long aboutFfanchiseCId = ConfigHelper.GetValueLong("AboutFranchiseServices");
        cbAboutFranchise.DefaultContentID = aboutFfanchiseCId;
        cbAboutFranchise.CacheInterval = ConfigHelper.GetValueLong("smallCacheInterval");
        cbAboutFranchise.Fill();
    } 

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            NewsContent = GetNewsContent();
            UxNewsDetails.DataSource = NewsContent;
            UxNewsDetails.DataBind();

            UxNextNews.DataSource = GetNextNewsContent(1, true);
            UxNextNews.DataBind();

            UxPrevNews.DataSource = GetNextNewsContent(1, false);
            UxPrevNews.DataBind();

            if (FransDataManager.IsFranchiseSelected())
            {
                desktopNavLocal.Visible = true;
                desktopNavNational.Visible = false;
            }
            else
            {
                desktopNavLocal.Visible = false;
                desktopNavNational.Visible = true;
            }
        }
    }

    private DataTable GetNextNewsContent(int count = 1, bool next = true)
    {
        DataTable DTSource = new DataTable();
        DataTable sortedDT = new DataTable();
        XmlDocument contentXML = new XmlDocument();
        long contentId;
        long.TryParse(Request.QueryString["id"], out contentId);
        int counter = 0;

        DTSource.Columns.Add("title");
        DTSource.Columns.Add("rawDate");
        DTSource.Columns.Add("date");
        DTSource.Columns.Add("HrefText");
        DTSource.Columns.Add("counter");

        var contents = SiteDataManager.GetNews();
        if (contents != null && contents.Count > 0)
        {
            foreach (Ektron.Cms.ContentData contentData in contents)
            {
                if (contentData.Id != contentId)
                {
                    try
                    {
                        contentXML.LoadXml(contentData.Html);
                        XmlNodeList xnList = contentXML.SelectNodes("/root");
                        string title = xnList[0]["title"].InnerXml;
                        if (title.Length > 50)
                        {
                            title = title.Substring(0, 50);
                        }
                        string titleReformated = HtmlRemoval.StripTagsRegexCompiled(title);
                        if (titleReformated.Length >= 50)
                        {
                            titleReformated += "...";
                        }
                        string date = xnList[0]["date"].InnerXml;

                        DateTime tempdate = Convert.ToDateTime(date);
                        string newFormattedDate = tempdate.ToString("MMMM dd, yyyy");
                        string hreftext = contentData.Quicklink;                        

                        counter++;
                        
                        string dateOfCurrNews = NewsContent.Rows[0]["rawDate"].ToString();
                        DateTime tempDateCurrNews = Convert.ToDateTime(dateOfCurrNews);

                        if (next && (tempDateCurrNews <= tempdate))
                        {
                            DTSource.Rows.Add(titleReformated, date, newFormattedDate, hreftext, counter);
                        }
                        else if ((!next) && (tempDateCurrNews >= tempdate))
                        {
                            DTSource.Rows.Add(titleReformated, date, newFormattedDate, hreftext, counter);
                        }

                    }
                    catch (Exception ex)
                    {
                        Log.WriteError(ex);
                    }
                }
            }
            DataView DVPSMMSort = DTSource.DefaultView;
            DVPSMMSort.Sort = "rawDate desc";
            sortedDT = DataviewHelper.SelectTopDataRow(DVPSMMSort.ToTable(), count);
        }
        return sortedDT;
    }

    private DataTable GetNewsContent()
    {
        DataTable DTSource = new DataTable();
        DataTable sortedDT = new DataTable();
        XmlDocument contentXML = new XmlDocument();
        long contentId;
        long.TryParse(Request.QueryString["id"], out contentId);
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

        long newsSFId = ConfigHelper.GetValueLong("NewsSmartFormID");
        var contentCriteria = new ContentCriteria(ContentProperty.DateModified, EkEnumeration.OrderByDirection.Descending);
        contentCriteria.AddFilter(ContentProperty.Id, CriteriaFilterOperator.EqualTo, contentId);
        contentCriteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, newsSFId);
        contentCriteria.OrderByField = ContentProperty.DateModified;
        var dataList = ContentHelper.GetListByCriteria(contentCriteria);
        if (dataList != null && dataList.Any())
        {
            var contentData = dataList.FirstOrDefault();
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
                string content1 = content;
                string content2 = string.Empty;
                //try
                //{
                //    content1 = "<p>" + GetFirstParagraph(content) + "</p>";
                //    string temp = content.Substring(content1.Length);
                //    string contenttemp = GetFirstParagraph(temp.Trim());
                //    if (!String.IsNullOrWhiteSpace(contenttemp))
                //    {
                //        content1 += "<p>" + contenttemp.Trim() + "</p>";
                //    }
                //    if (!string.IsNullOrEmpty(content))
                //        content2 = content.Substring(content1.Length);
                //        //content2 = content.Substring(content1.Length);
                //}
                //catch(Exception ex)
                //{
                //    Log.WriteError(ex);
                //}
                //get pull quote
                string pullquote = xnList[0]["pullquote"].InnerXml;

                if (!String.IsNullOrWhiteSpace(pullquote))
                {
                    pullquote = "<blockquote><p>" + pullquote + "</p></blockquote>";
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
        return sortedDT;
    }

    protected void EmailBtn_Click(object sender, EventArgs e)
    {

        //get the page to be emailed
        StringWriter str_wrt = new StringWriter();
        HtmlTextWriter html_wrt = new HtmlTextWriter(str_wrt);
        Page.RenderControl(html_wrt);
        String HTML = str_wrt.ToString();
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