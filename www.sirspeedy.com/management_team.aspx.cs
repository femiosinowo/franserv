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
using Figleaf;
using SirSpeedy.CMS;
using System.Text.RegularExpressions;
using Ektron.Cms.Instrumentation;

public partial class _management_team : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            UxManagementTeam.DataSource = GetManagementTeamContent();
            UxManagementTeam.DataBind();

            UxManagementTeamSlider.DataSource = GetManagementTeamContent();
            UxManagementTeamSlider.DataBind();
        }
    }

    private DataTable GetManagementTeamContent()
    {
        string fransId = FransDataManager.IsFranchiseSelected() ? FransDataManager.GetFranchiseId() : "0";
        string cacheKey = String.Format("Sirspeedy:management_team:GetManagementTeamContent:FranchiseId={0}",
            fransId);

        DataTable sortedDT = CacheBase.Get<DataTable>(cacheKey);

        if (sortedDT == default(DataTable) || sortedDT.Rows.Count == 0)
        {

            DataTable DTSource = new DataTable();
            // DataTable sortedDT = new DataTable();
            XmlDocument contentXML = new XmlDocument();
            int counter = 0;

            DTSource.Columns.Add("firstName");
            DTSource.Columns.Add("lastName");
            DTSource.Columns.Add("jobTitle");
            DTSource.Columns.Add("abstract");
            DTSource.Columns.Add("bio");
            DTSource.Columns.Add("gender");
            DTSource.Columns.Add("socialMedia");
            DTSource.Columns.Add("imageSRC");
            DTSource.Columns.Add("cssClass");
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
                            string imgSRC =
                                Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1]
                                    .Value;
                            string urlxml = contentXML.SelectSingleNode("/root/linkedInUrl").InnerXml;
                            string linkedInUrl = (urlxml != null)
                                ? Regex.Match(urlxml, "<a.+?href=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1
                                    ].Value
                                : String.Empty; //not sure about this
                            urlxml = contentXML.SelectSingleNode("/root/twitterUrl").InnerXml;
                            string twitterUrl = (urlxml != null)
                                ? Regex.Match(urlxml, "<a.+?href=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1
                                    ].Value
                                : String.Empty;
                            urlxml = contentXML.SelectSingleNode("/root/facebookUrl").InnerXml;
                            string facebookUrl = (urlxml != null)
                                ? Regex.Match(urlxml, "<a.+?href=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1
                                    ].Value
                                : String.Empty;

                            string socialMedia = string.Empty;
                            if (!String.IsNullOrWhiteSpace(linkedInUrl))
                            {
                                socialMedia += "<a href=\"" + linkedInUrl +
                                               "\" target=\"_blank\"><img src=\"/images/social-icons/linkedin.png\" alt=\"LinkedIn\"></a>&nbsp";
                            }
                            if (!String.IsNullOrWhiteSpace(twitterUrl))
                            {
                                socialMedia += "<a href=\"" + twitterUrl +
                                               "\" target=\"_blank\"><img src=\"/images/social-icons/twitter.png\" alt=\"Twitter\"></a>&nbsp";
                            }
                            if (!String.IsNullOrWhiteSpace(facebookUrl))
                            {
                                socialMedia += "<a href=\"" + facebookUrl +
                                               "\" target=\"_blank\"><img src=\"/images/social-icons/facebook.png\" alt=\"Facebook\"></a>&nbsp";
                            }

                            counter++;

                            string cssClass = "";

                            if (counter == 1 || ((((counter - 1)%4)) == 0))
                            {
                                cssClass = "alpha";
                            }
                            else if (counter%4 == 0)
                            {
                                cssClass = "omega";
                            }

                            DTSource.Rows.Add(firstName, lastName, jobTitle, abstractText, bio, gender, socialMedia,
                                imgSRC, cssClass, counter);


                        }
                    }
                    catch (Exception ex)
                    {
                        Log.WriteError(ex);
                    }
                }
                DataView DVPSMMSort = DTSource.DefaultView;

                DVPSMMSort.Sort = "counter asc";

                sortedDT = DVPSMMSort.ToTable();
                CacheBase.Put(cacheKey, sortedDT, CacheDuration.For24Hr);
            }
        }
        return sortedDT;
    }

    
}