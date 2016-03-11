using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Figleaf;
using SirSpeedy.CMS;
using System.Xml;
using Ektron.Cms.Content;
using Ektron.Cms.Common;
using System.Text.RegularExpressions;
using Ektron.Cms.Instrumentation;
using Ektron.Cms.Framework.Settings.UrlAliasing;

public partial class case_studies : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DataTable test = GetCaseStudiesContent();
            lvCaseStudiesPhotos.DataSource = GetCaseStudiesContent();
            lvCaseStudiesPhotos.DataBind();

            //for (int i = 0; i < test.Rows.Count; i++)
            //{
            //    string counter = test.Rows[i]["counter"].ToString();
            //    string index = i.ToString();
            //    string cssClass = test.Rows[i]["cssClass"].ToString();

            //    Response.Write(string.Concat("index= ", index + ", " + "counter= " + counter + ", cssClass= " + cssClass + "<br />"));
            //}
        }
    }

    private DataTable GetCaseStudiesContent()
    {
        string fransId = FransDataManager.IsFranchiseSelected() ? FransDataManager.GetFranchiseId() : "0";
        string cacheKey = String.Format("Sirspeedy:case_studies:GetCaseStudiesContent:FranchiseId={0}",
            fransId);

        DataTable sortedDT = CacheBase.Get<DataTable>(cacheKey);

        if (sortedDT == default(DataTable) || sortedDT.Rows.Count == 0)
        {
            DataTable DTSource = new DataTable();
            // DataTable sortedDT = new DataTable();
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
                sortedDT = DVPSMMSort.ToTable();
                CacheBase.Put(cacheKey, sortedDT, CacheDuration.For24Hr);
            }
        }
        return sortedDT;
    }

}