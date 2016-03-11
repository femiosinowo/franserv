using System;
using Ektron.Cms;
using Ektron.Cms.Framework.Content;
using System.Xml;
using System.Text.RegularExpressions;
using System.Data;
using SirSpeedy.CMS;
using Ektron.Cms.Content;
using Ektron.Cms.Common;
using Ektron.Cms.Instrumentation;
using System.Web.UI.WebControls;

public partial class news : PageBase
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
            cbNewsTagline.DefaultContentID = ConfigHelper.GetValueLong("NewsTaglineContentId");
            cbNewsTagline.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
            cbNewsTagline.Fill();

            this.addCount();

            UxNewsArticle.DataSource = GetNewsNational();
            UxNewsArticle.DataBind();

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

    protected void LoadMoreLinkButton_Click(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            cbNewsTagline.DefaultContentID = ConfigHelper.GetValueLong("NewsTaglineContentId");
            cbNewsTagline.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
            cbNewsTagline.Fill();

            Repeater UxNewsArticleRpt = this.UxNewsArticle;

            this.addCount();

            UxNewsArticleRpt.DataSource = GetNewsNational(0, 10 * this.clickCount);
            UxNewsArticleRpt.DataBind();
        }
    }

    private DataTable GetNewsNational(int startIndex = 0, int rowCount = 10)
    {
        DataTable DTSource = new DataTable();
        DataTable sortedDT = new DataTable();
        XmlDocument contentXML = new XmlDocument();
        int counter = 0;

        DTSource.Columns.Add("title");
        DTSource.Columns.Add("rawDate");
        DTSource.Columns.Add("date");
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
                    XmlNodeList xnList = contentXML.SelectNodes("/root");
                    string title = xnList[0]["title"].InnerXml;
                    string date = xnList[0]["date"].InnerXml;
                    string city = xnList[0]["city"].InnerXml;
                    string state = xnList[0]["state"].InnerXml;
                    string teaser = xnList[0]["teaser"].InnerXml;
                    string content = xnList[0]["content"].InnerXml;

                    DateTime tempdate = Convert.ToDateTime(date);
                    string newFormattedDate = tempdate.ToString("MMMM dd, yyyy");
                    string hreftext = contentData.Quicklink;
                    string xml = contentXML.SelectSingleNode("/root/image").InnerXml;
                    string imgSRC = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;

                    string imgTag = String.Empty;

                    if (!String.IsNullOrEmpty(imgSRC))
                    {
                        imgTag = "<img class=\"news_img\" src=\""+ imgSRC +"\">";
                    }


                    xml = contentXML.SelectSingleNode("/root/url").InnerXml;
                    string url = Regex.Match(xml, "<a.+?href=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value; //not sure about this
                    counter++;

                    DTSource.Rows.Add(title, date, newFormattedDate, city, state, teaser, content, imgSRC, imgTag, url, hreftext, counter);

                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }
            DataView DVPSMMSort = DTSource.DefaultView;
            DVPSMMSort.Sort = "rawDate desc";
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