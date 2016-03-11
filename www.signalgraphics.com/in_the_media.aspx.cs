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
using SignalGraphics.CMS;
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

        DataTable DTcol1 = DTSource.Clone();
        DataTable DTcol2 = DTSource.Clone();
        DataTable DTcol3 = DTSource.Clone();
        DataTable DTcol4 = DTSource.Clone();

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

            foreach (DataRow row in sortedDT.Rows)
            {
                string contentId = row["contentId"].ToString();
                string title = row["title"].ToString();
                string source = row["source"].ToString();
                string date = row["rawDate"].ToString();
                DateTime mediaDate = Convert.ToDateTime(date);
                string newFormattedDate = mediaDate.ToString("MMMM dd, yyyy");
                string teaser = row["teaser"].ToString();
                string imgSRC = row["imgSRC"].ToString();
                string logoCss = row["logoCss"].ToString();
                string logoImage = row["logoImage"].ToString();
                string url = row["url"].ToString();
                string scounter = row["counter"].ToString();

                if (tempcount == 1)
                {
                    DTcol1.Rows.Add(mediaDate, contentId, title, source, newFormattedDate, date, teaser, imgSRC, url, logoCss, logoImage, scounter);
                }
                else if (tempcount == 2)
                {
                    DTcol2.Rows.Add(mediaDate, contentId, title, source, newFormattedDate, date, teaser, imgSRC, url, logoCss, logoImage, scounter);
                }
                else if (tempcount == 3)
                {
                    DTcol3.Rows.Add(mediaDate, contentId, title, source, newFormattedDate, date, teaser, imgSRC, url, logoCss, logoImage, scounter);
                }
                else if (tempcount == 4)
                {
                    DTcol4.Rows.Add(mediaDate, contentId, title, source, newFormattedDate, date, teaser, imgSRC, url, logoCss, logoImage, scounter);
                }


                if (tempcount == 4)
                {
                    tempcount = 0;
                }
                tempcount++;
            }

            uxMediaWrapperColumnOne.DataSource = DTcol1;
            uxMediaWrapperColumnOne.DataBind();

            uxMediaWrapperColumnTwo.DataSource = DTcol2;
            uxMediaWrapperColumnTwo.DataBind();

            uxMediaWrapperColumnThree.DataSource = DTcol3;
            uxMediaWrapperColumnThree.DataBind();

            uxMediaWrapperColumnFour.DataSource = DTcol4;
            uxMediaWrapperColumnFour.DataBind();

        }         
    }
}