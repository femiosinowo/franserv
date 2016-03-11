using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using Ektron.Cms.Framework.Content;
using Ektron.Cms;
using System.Text.RegularExpressions;
using SirSpeedy.CMS;
using Ektron.Cms.Instrumentation;

public partial class in_the_media : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {            
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

            this.GetInTheMediaContent();
        }
    }
    
    private void GetInTheMediaContent()
    {
        DataTable DTSource = new DataTable();
        DataTable sortedDT = new DataTable();        

        XmlDocument contentXML = new XmlDocument();
        int counter = 0;
        int tempcount = 1;

        DTSource.Columns.Add("mediaDate", typeof(DateTime));
        DTSource.Columns.Add("contentId");
        DTSource.Columns.Add("title");
        DTSource.Columns.Add("source");
        DTSource.Columns.Add("date");
        DTSource.Columns.Add("rawDate");
        DTSource.Columns.Add("teaser");
        DTSource.Columns.Add("imgSRC");
        DTSource.Columns.Add("url");
        DTSource.Columns.Add("logoCss");
        DTSource.Columns.Add("logoImage");
        DTSource.Columns.Add("counter");

        var contents = SiteDataManager.GetInTheMedia();
        if (contents != null && contents.Count > 0)
        {
            foreach (Ektron.Cms.ContentData contentData in contents)
            {
                try
                {
                    contentXML.LoadXml(contentData.Html);

                    XmlNodeList xnList = contentXML.SelectNodes("/root");
                    string contentId = contentData.Id.ToString();
                    string title = "";
                    if (xnList[0]["title"] != null)
                    title = xnList[0]["title"].InnerXml;

                    string source = "";
                    if (xnList[0]["sourceName"] != null)
                        source = xnList[0]["sourceName"].InnerXml;

                    string date = "";
                    if (xnList[0]["date"] != null)
                        date = xnList[0]["date"].InnerXml;
                    string teaser = "";
                    if (xnList[0]["teaser"] != null)
                        teaser = xnList[0]["teaser"].InnerXml;

                    DateTime mediaDate = Convert.ToDateTime(date);
                    string newFormattedDate = mediaDate.ToString("MMMM dd, yyyy");

                    string imgSRC = "";
                    if (contentXML.SelectSingleNode("/root/image") != null)
                    {
                        string xml = contentXML.SelectSingleNode("/root/image").InnerXml;
                        imgSRC = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                    }

                    string url = "";
                    if (contentXML.SelectSingleNode("/root/url") != null)
                    {
                        string xml = contentXML.SelectSingleNode("/root/url").InnerXml;
                        url = Regex.Match(xml, "<a.+?href=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value; //not sure about this
                    }
                    counter++;

                    string logoCss = "";
                    string logoImage = "";

                    if (!String.IsNullOrEmpty(imgSRC))
                    {
                        logoCss = "source_logo";
                        logoImage = "<div class=\"logo_wrapper\"><img src=\"" + imgSRC + "\" alt=\"\" /></div>";
                    }

                    DTSource.Rows.Add(mediaDate, contentId, title, source, newFormattedDate, date, teaser, imgSRC, url, logoCss, logoImage, counter);
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }
            DataView dv = DTSource.DefaultView;
            dv.Sort = "mediaDate DESC";
            sortedDT = dv.ToTable();

            ListView1.DataSource = sortedDT;
            ListView1.DataBind();

        }         
    }
}