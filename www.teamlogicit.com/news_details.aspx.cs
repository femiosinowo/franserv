using System;
using System.Web.UI;
using System.Data;
using System.Xml;
using System.Text.RegularExpressions;
using Ektron.Cms;
using Ektron.Cms.Framework.Content;
using TeamLogic.CMS;
using Ektron.Cms.Content;
using Ektron.Cms.Common;
using Ektron.Cms.Instrumentation;
using System.Linq;
using System.IO;


public partial class news_details : PageBase
{
    protected DataTable NewsContent = new DataTable();
    public string HereToHelpBackgroundImage = "/images/here_to_help_bkg.jpg";
    long contentIdQueryString = 0;

    protected void Page_Init(object sender, EventArgs e)
    {
        if (FransDataManager.IsFranchiseSelected())
            cbHereToHelp.DefaultContentID = ConfigHelper.GetValueLong("LocalNewsPageHereToHelpCId");
        else
            cbHereToHelp.DefaultContentID = ConfigHelper.GetValueLong("NewsPageHereToHelpCId");
        cbHereToHelp.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbHereToHelp.Fill();

        if (cbHereToHelp.EkItem != null && !string.IsNullOrEmpty(cbHereToHelp.EkItem.Image) && cbHereToHelp.EkItem.Image.ToLower().IndexOf("workarea") <= -1)
            HereToHelpBackgroundImage = cbHereToHelp.EkItem.Image;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["id"]))
        {
            ////a workaround for some of the news articles which are not aliased.
            //string ids = Request.QueryString["id"];
            //string id = "";
            //var idsArray = ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            //if (idsArray.Length > 1)
            //    id = idsArray[2];
            //else
            //    id = idsArray[1];

            contentIdQueryString = Convert.ToInt64(Request.QueryString["id"]);
        }
        here_to_help_img.Attributes.Add("data-image", HereToHelpBackgroundImage);
        here_to_help_img.Attributes.Add("data-image-mobile", HereToHelpBackgroundImage);

        if (!IsPostBack)
        {
            NewsContent = GetNewsNational();
            UxNewsDetails.DataSource = NewsContent;
            UxNewsDetails.DataBind();           
        }        
    }

    private DataTable GetNewsNational()
    {
        DataTable DTSource = new DataTable();
        DataTable sortedDT = new DataTable();
        XmlDocument contentXML = new XmlDocument();
        int counter = 0;

        DTSource.Columns.Add("id");
        DTSource.Columns.Add("title");
        DTSource.Columns.Add("quicklink");
        DTSource.Columns.Add("rawDate");
        DTSource.Columns.Add("date");
        DTSource.Columns.Add("ndate");
        DTSource.Columns.Add("nmonth");
        DTSource.Columns.Add("nyear");
        DTSource.Columns.Add("city");
        DTSource.Columns.Add("state");
        DTSource.Columns.Add("teaser");
        DTSource.Columns.Add("content");
        DTSource.Columns.Add("imgSRC");
        DTSource.Columns.Add("imgTag");
        DTSource.Columns.Add("url");
        DTSource.Columns.Add("hrefText");
        DTSource.Columns.Add("counter");

        var contents = SiteDataManager.GetNews();
        if (contents != null && contents.Count > 0)
        {
            string domainName = Request.ServerVariables["SERVER_NAME"];
            foreach (Ektron.Cms.ContentData contentData in contents)
            {
                try
                {
                    contentXML.LoadXml(contentData.Html);
                    XmlNodeList xnList = contentXML.SelectNodes("/root");
                    string contentId = contentData.Id.ToString();
                    //string contentQuickLink = contentData.Quicklink.ToString();            
                    string title = xnList[0]["title"].InnerXml;
                    string date = xnList[0]["date"].InnerXml;
                    string city = xnList[0]["city"].InnerXml;
                    string state = xnList[0]["state"].InnerXml;
                    string teaser = xnList[0]["teaser"].InnerXml;
                    string content = xnList[0]["content"].InnerXml;

                    DateTime tempdate = Convert.ToDateTime(date);
                    string newFormattedDate = tempdate.ToString("MMMM dd, yyyy");
                    string nmonth = tempdate.ToString("MMM");
                    string ndate = tempdate.ToString("dd");
                    string nyear = tempdate.ToString("yyyy");
                    string hreftext = contentData.Quicklink;
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

                    DTSource.Rows.Add(contentId, title, hreftext, date, newFormattedDate, ndate, nmonth, nyear, city, state, teaser, content, imgSRC, imgTag, url, hreftext, counter);

                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }
           
        }
        DataRow[] dr = DTSource.Select("id ='" + contentIdQueryString + "'");
        DataRow newRow = DTSource.NewRow();        
        newRow.ItemArray = dr[0].ItemArray;        
        DTSource.Rows.Remove(dr[0]);
        DTSource.Rows.InsertAt(newRow, 0);


        return DTSource;
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