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
using Ektron.Cms.Framework.Settings.UrlAliasing;

public partial class case_studies : PageBase
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
            UXCaseStudiesPhotos.DataSource = GetCaseStudiesContent();
            UXCaseStudiesPhotos.DataBind();            
        }
    }

    protected void LoadMoreLinkButton_Click(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            Repeater UxNewsArticleRpt = this.UXCaseStudiesPhotos;

            this.addCount();

            UxNewsArticleRpt.DataSource = GetCaseStudiesContent(0, 10 * this.clickCount);
            UxNewsArticleRpt.DataBind();
        }
    }

    private DataTable GetCaseStudiesContent(int startIndex = 0, int rowCount = 10)
    {
        DataTable DTSource = new DataTable();
        DataTable sortedDT = new DataTable();
        XmlDocument contentXML = new XmlDocument();
        int counter = 0;

        DTSource.Columns.Add("title");
        DTSource.Columns.Add("desc");
        DTSource.Columns.Add("url");
        DTSource.Columns.Add("imgSRC");
        DTSource.Columns.Add("counter");
        DTSource.Columns.Add("hreftext");
        DTSource.Columns.Add("cssClass");
        DTSource.Columns.Add("dateCreated");

        var contents = SiteDataManager.GetCaseStudies();
        if (contents != null && contents.Count > 0)
        {
            //enable load more News content
            if (contents.Count > rowCount)
                loadMoreContent.Visible = true;
            else
                loadMoreContent.Visible = false;

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
                    string isBig = xnList[0]["isBig"].InnerXml;
                    string xml = contentXML.SelectSingleNode("/root/image").InnerXml;
                    string imgSRC =
                        Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;

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

                    DTSource.Rows.Add(title, desc, url, imgSRC, counter, hreftext, cssClass, dateCreated);
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }
            DataView DVPSMMSort = DTSource.DefaultView;
            DVPSMMSort.Sort = "counter asc"; //dateCreated
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