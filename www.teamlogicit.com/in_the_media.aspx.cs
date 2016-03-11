using System;
using System.Web.UI;
using System.Data;
using System.Xml;
using System.Text.RegularExpressions;
using TeamLogic.CMS;
using Ektron.Cms.Instrumentation;

public partial class in_the_media : System.Web.UI.Page
{
    private int defaultCount = ConfigHelper.GetValueInt("InTheMediaPageSize");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (SiteDataManager.GetInTheMedia().Count < defaultCount)
            loadMoreNews.Visible = false;

        if (!Page.IsPostBack)
        {
            GetInTheMediaContent();
        }
    }

    protected void LoadMoreLinkButton_Click(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            GetInTheMediaContent();
        }
    }

    private void GetInTheMediaContent(int column = 0)
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
        DTSource.Columns.Add("ndate");
        DTSource.Columns.Add("nmonth");
        DTSource.Columns.Add("nyear");
        DTSource.Columns.Add("url");
        DTSource.Columns.Add("counter");

        DataTable tempDataTable = DTSource.Clone();

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
                    string title = xnList[0]["title"].InnerXml;
                    string source = string.Empty;
                    //source name
                    if (xnList[0]["_"] != null)
                        source = xnList[0]["_"].InnerXml;
                    string date = xnList[0]["date"].InnerXml;
                    DateTime mediaDate = Convert.ToDateTime(date);
                    string newFormattedDate = mediaDate.ToString("MMMM dd, yyyy");
                    string nmonth = mediaDate.ToString("MMM");
                    string ndate = mediaDate.ToString("dd");
                    string nyear = mediaDate.ToString("yyyy");

                    string xml = contentXML.SelectSingleNode("/root/url").InnerXml;
                    string url = Regex.Match(xml, "<a.+?href=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value; //not sure about this
                    tempDataTable.Rows.Add(mediaDate, contentId, title, source, newFormattedDate, date, ndate, nmonth, nyear, url, counter);
                    counter++;

                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }

            DataView dv = tempDataTable.DefaultView;
            dv.Sort = "mediaDate DESC";
            sortedDT = dv.ToTable();

            uxMediaWrapperListView1.DataSource = sortedDT;
            uxMediaWrapperListView1.DataBind();

        }

    }
}