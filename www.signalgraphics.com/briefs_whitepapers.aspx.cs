using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Figleaf;
using SignalGraphics.CMS;
using System.Xml;
using Ektron.Cms.Content;
using Ektron.Cms.Common;
using System.Text.RegularExpressions;
using Ektron.Cms.Instrumentation;

public partial class briefs_whitepapers : System.Web.UI.Page
{
    private int clickCount
    {
        get
        {
            if (ViewState["clickCount"] == null)
                ViewState["clickCount"] = 0;
            return (int)ViewState["clickCount"];
        }
        set { ViewState["clickCount"] = value; }
    }

    public void addCount()
    {
        this.clickCount = this.clickCount + 1;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.addCount();

            UxBriefAndWhitepapers.DataSource = GetBriefsWHitepaperContent();
            UxBriefAndWhitepapers.DataBind();
        }
    }

    protected void LoadMoreLinkButton_Click(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            Repeater UxNewsArticleRpt = this.UxBriefAndWhitepapers;

            this.addCount();

            UxNewsArticleRpt.DataSource = GetBriefsWHitepaperContent(0, 10 * this.clickCount);
            UxNewsArticleRpt.DataBind();
        }
    }

    private DataTable GetBriefsWHitepaperContent(int startIndex = 0, int rowCount = 10)
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
            //enable load more News content
            if (contents.Count > rowCount)
                loadMoreContent.Visible = true;
            else
                loadMoreContent.Visible = false;

            foreach (var contentData in contents)
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
                    string imgSRC =
                        Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                    xml = contentXML.SelectSingleNode("/root/url").InnerXml;
                    string url =
                        Regex.Match(xml, "<a.+?href=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                    //not sure about this
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
            sortedDT = DataviewHelper.SelectTopDataRow(DVPSMMSort.ToTable(), rowCount);
        }

        DataTable finalResult = new DataTable();
        int indexRow = 1;
        finalResult = sortedDT.Clone();
        foreach (DataRow row in sortedDT.Rows)
        {
            if (indexRow <= sortedDT.Rows.Count && indexRow <= rowCount)
            {
                finalResult.ImportRow(sortedDT.Rows[indexRow - 1]);

                indexRow++;
            }
        }
        return finalResult;
    }
}