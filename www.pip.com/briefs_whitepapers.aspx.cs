using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SirSpeedy.CMS;
using System.Xml;
using Ektron.Cms.Content;
using Ektron.Cms.Common;
using System.Text.RegularExpressions;
using Ektron.Cms.Instrumentation;

public partial class briefs_whitepapers : System.Web.UI.Page
{
    /// <summary>
    /// page init event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Init(object sender, EventArgs e)
    {

        cbBriefWhitepapersHeaderImage.DefaultContentID = ConfigHelper.GetValueLong("CaseStudiesSideContentID");
        cbBriefWhitepapersHeaderImage.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbBriefWhitepapersHeaderImage.Fill();


        cbBriefWhitepapersSideContent.DefaultContentID = ConfigHelper.GetValueLong("BriefWhitepapersSideContentID");
        cbBriefWhitepapersSideContent.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbBriefWhitepapersSideContent.Fill();

        //cbMediaInquires.DefaultContentID = ConfigHelper.GetValueLong("NewsMediaInquiresContentID");
        //cbMediaInquires.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        //cbMediaInquires.Fill();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            UxBriefAndWhitepapers.DataSource = GetBriefsWHitepaperContent();
            UxBriefAndWhitepapers.DataBind();
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
}