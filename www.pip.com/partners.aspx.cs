using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SirSpeedy.CMS;
using System.Configuration;
using Ektron.Cms.Framework.Content;
using Ektron.Cms.Content;
using Ektron.Cms;
using Ektron.Cms.Common;
using System.Xml.Linq;
using System.Xml.XPath;
using Ektron.Cms.Instrumentation;
using System.Data;
using System.Xml;
using System.Text.RegularExpressions;

public partial class partners : PageBase
{
    // <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.BodyClass += " partners ";

        cbPartnersSideContent.DefaultContentID = ConfigHelper.GetValueLong("PartnersSideContentId");
        cbPartnersSideContent.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbPartnersSideContent.Fill();        

        if (!Page.IsPostBack)
        {
            UxPartnersRows.DataSource = GetRows();
            UxPartnersRows.DataBind();
        }
    }

    protected void UxPartnersRows_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            Repeater repPartnersRow = null;
            repPartnersRow = (Repeater)e.Item.FindControl("UxPartnersRow");
            DataRowView drv = e.Item.DataItem as DataRowView;
            repPartnersRow.DataSource = GetPartnersContent(Convert.ToInt32(drv.Row["RowId"]));
            repPartnersRow.DataBind();
        }
        catch (Exception ex)
        {
            Log.WriteError(ex);
        }
    }

    /// <summary>
    /// Calculates the no of rows to be displayed on the webpage based on the contents.
    /// </summary>
    /// <returns></returns>
    private DataTable GetRows()
    {
        DataTable DTSource = new DataTable();
        int contentCount = 0;
        var getPartenrs = M3TDataManager.GetPartners();
        if (getPartenrs != null)
            contentCount = getPartenrs.Count;
        int TotalRows = contentCount / 4;
        int lastrow = contentCount % 4;
        if (lastrow > 0)
            TotalRows++;
        DTSource.Columns.Add("RowId");
        for (int i = 1; i <= TotalRows; i++)
        {
            DTSource.Rows.Add(i);
        }
        return DTSource;
    }

    /// <summary>
    /// helper method to get partners instances
    /// </summary>
    /// <returns></returns>
    private DataTable GetPartnersContent(int rowID)
    {
        DataTable DTSource = new DataTable();
        DataTable sortedDT = new DataTable();
        XmlDocument contentXML = new XmlDocument();
        int counter = 0;
        int row = 0;
        DTSource.Columns.Add("imgSRC");
        DTSource.Columns.Add("companyName");
        DTSource.Columns.Add("tagline");
        DTSource.Columns.Add("teaser");
        DTSource.Columns.Add("alt");
        DTSource.Columns.Add("url");
        DTSource.Columns.Add("cssClassText");
        DTSource.Columns.Add("dateCreated");
        DTSource.Columns.Add("counter");
        DTSource.Columns.Add("RowId");

        var contents = M3TDataManager.GetPartners();
        if (contents != null && contents.Count > 0)
        {
            foreach (Ektron.Cms.ContentData contentData in contents)
            {
                try
                {
                    contentXML.LoadXml(contentData.Html);
                    XmlNodeList xnList = contentXML.SelectNodes("/root");
                    string tagline = xnList[0]["tagline"].InnerXml;
                    string teaser = xnList[0]["teaser"].InnerXml;
                    string companyName = xnList[0]["companyName"].InnerXml.Replace(" ", "");
                    string xml = contentXML.SelectSingleNode("/root/image").InnerXml;
                    string imgSRC = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                    string alt = Regex.Match(xml, "<img.+?alt=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                    xml = contentXML.SelectSingleNode("/root/url").InnerXml;
                    string url = Regex.Match(xml, "<a.+?href=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;

                    string dateCreated = contentData.DateCreated.ToString();

                    string cssClassWrapper = "";
                    counter++;
                    if (counter == 1 || ((((counter - 1) % 4)) == 0))
                    {

                        cssClassWrapper = "alpha";
                    }
                    else if (counter % 4 == 0)
                    {
                        cssClassWrapper = "omega";
                    }
                    row = (counter - 1) / 4;
                    row = row + 1;
                    if (row == rowID)
                        DTSource.Rows.Add(imgSRC, companyName, tagline, teaser, alt, url, cssClassWrapper, counter, row);
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }
            DataView DVPSMMSort = DTSource.DefaultView;

            //DVPSMMSort.Sort = "counter desc";
            sortedDT = DVPSMMSort.ToTable();
        }
        return sortedDT;
    }   
}