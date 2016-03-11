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
using SirSpeedy.CMS;
using System.Text.RegularExpressions;
using Ektron.Cms.Instrumentation;
using Figleaf;

public partial class _management_team : System.Web.UI.Page
{
    protected int rowNumberPub = 0;

    /// <summary>
    /// page init event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Init(object sender, EventArgs e)
    {
        cbMgmtTeamSideContent.DefaultContentID = ConfigHelper.GetValueLong("ManagementTeamSideContentID");
        cbMgmtTeamSideContent.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbMgmtTeamSideContent.Fill();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.BodyClass += " mgmt-team ";

        if (!Page.IsPostBack)
        {
            UxManagementTeamRows.DataSource = GetRows();
            UxManagementTeamRows.DataBind();
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
        var getMgmtTeam = M3TDataManager.GetManagementTeamContent();
        if (getMgmtTeam != null)
            contentCount = getMgmtTeam.Count;
        int TotalRows = contentCount / 3;
        int lastrow = contentCount % 3;
        if (lastrow > 0)
            TotalRows++;
        DTSource.Columns.Add("RowId");
        for (int i = 1; i <= TotalRows; i++)
        {
            DTSource.Rows.Add(i);
        }
        return DTSource;
    }

    private DataTable GetManagementTeamContent(int rowID)
    {
        string fransId = FransDataManager.IsFranchiseSelected() ? FransDataManager.GetFranchiseId() : "0";
        string cacheKey = String.Format("Pip:management_team:GetManagementTeamContent:FranchiseId={0}:Rowid={1}",
            fransId, rowID);

        DataTable sortedDT = CacheBase.Get<DataTable>(cacheKey);

        if (sortedDT == default(DataTable) || sortedDT.Rows.Count == 0)
        {
            DataTable DTSource = new DataTable();
            // DataTable sortedDT = new DataTable();
            XmlDocument contentXML = new XmlDocument();
            int counter = 0;
            int row = 0;

            DTSource.Columns.Add("firstName");
            DTSource.Columns.Add("lastName");
            DTSource.Columns.Add("jobTitle");
            DTSource.Columns.Add("abstract");
            DTSource.Columns.Add("bio");
            DTSource.Columns.Add("gender");
            DTSource.Columns.Add("socialMedia");
            DTSource.Columns.Add("imageSRC");
            DTSource.Columns.Add("cssClass");
            DTSource.Columns.Add("RowId");
            DTSource.Columns.Add("counter");

            var contents = SiteDataManager.GetManagementTeam();
            if (contents != null && contents.Count > 0)
            {
                foreach (Ektron.Cms.ContentData contentData in contents)
                {
                    try
                    {
                        contentXML.LoadXml(contentData.Html);

                        XmlNodeList xnList = contentXML.SelectNodes("/root");
                        if (xnList != null && xnList.Count > 0)
                        {
                            string firstName = xnList[0]["firstName"].InnerXml;
                            string lastName = xnList[0]["lastName"].InnerXml;
                            string jobTitle = xnList[0]["jobTitle"].InnerXml;
                            string gender = xnList[0]["gender"].InnerXml;
                            string abstractText = xnList[0]["abstract"].InnerXml;
                            string bio = xnList[0]["bio"].InnerXml;

                            string xml = contentXML.SelectSingleNode("/root/mediumImage").InnerXml;
                            string imgSRC = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                            string urlxml = contentXML.SelectSingleNode("/root/linkedInUrl").InnerXml;
                            string linkedInUrl = (urlxml != null) ? Regex.Match(urlxml, "<a.+?href=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value : String.Empty; //not sure about this
                            urlxml = contentXML.SelectSingleNode("/root/twitterUrl").InnerXml;
                            string twitterUrl = (urlxml != null) ? Regex.Match(urlxml, "<a.+?href=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value : String.Empty;
                            urlxml = contentXML.SelectSingleNode("/root/facebookUrl").InnerXml;
                            string facebookUrl = (urlxml != null) ? Regex.Match(urlxml, "<a.+?href=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value : String.Empty;

                            string socialMedia = string.Empty;
                            if (!String.IsNullOrWhiteSpace(linkedInUrl))
                            {
                                socialMedia += "<a href=\"" + linkedInUrl + "\" target=\"_blank\"><img src=\"/images/social-icons/linkedin.png\" alt=\"LinkedIn\"></a>&nbsp";
                            }
                            if (!String.IsNullOrWhiteSpace(twitterUrl))
                            {
                                socialMedia += "<a href=\"" + twitterUrl + "\" target=\"_blank\"><img src=\"/images/social-icons/twitter.png\" alt=\"Twitter\"></a>&nbsp";
                            }
                            if (!String.IsNullOrWhiteSpace(facebookUrl))
                            {
                                socialMedia += "<a href=\"" + facebookUrl + "\" target=\"_blank\"><img src=\"/images/social-icons/facebook.png\" alt=\"Facebook\"></a>&nbsp";
                            }

                            counter++;

                            string cssClass = "";

                            if (counter == 1 || ((((counter - 1) % 3)) == 0))
                            {
                                cssClass = "alpha";
                            }
                            else if (counter % 3 == 0)
                            {
                                cssClass = "omega";
                            }

                            row = (counter - 1) / 3;
                            row = row + 1;
                            if (row == rowID)
                                DTSource.Rows.Add(firstName, lastName, jobTitle, abstractText, bio, gender, socialMedia, imgSRC, cssClass, row, counter);


                        }
                    }
                    catch (Exception ex)
                    {
                        Log.WriteError(ex);
                    }
                }
                DataView DVPSMMSort = DTSource.DefaultView;

                //DVPSMMSort.Sort = "counter asc";

                sortedDT = DVPSMMSort.ToTable();
                CacheBase.Put(cacheKey, sortedDT, CacheDuration.For24Hr);
            }
        }
        return sortedDT;
    }

    protected void UxManagementTeamRow_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
    {
        try
        {
            //Get Inner Repeater
            Repeater UxManagementTeamRow = null;
            UxManagementTeamRow = (Repeater)e.Item.FindControl("UxManagementTeamRow");
            DataRowView drv = e.Item.DataItem as DataRowView;
            UxManagementTeamRow.DataSource = GetManagementTeamContent(Convert.ToInt32(drv.Row["RowId"]));
            UxManagementTeamRow.DataBind();
        }
        catch (Exception ex)
        {
            Log.WriteError(ex);
        }
    }
        
}