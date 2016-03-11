using Ektron.Cms;
using Ektron.Cms.Framework.Content;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using SirSpeedy.CMS;
using Ektron.Cms.Instrumentation;
using Figleaf;

public partial class UserControls_HomePageCaseStudies : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable DTCaseStudiesSliderSource = GetCaseStudiesSliderContent();
            UxCaseStudiesSlider.DataSource = DTCaseStudiesSliderSource;
            UxCaseStudiesSlider.DataBind();
        }
    }

    private DataTable GetCaseStudiesSliderContent()
    {
        string fransId = FransDataManager.IsFranchiseSelected() ? FransDataManager.GetFranchiseId() : "0";
        string cacheKey = String.Format("Pip:UserControlsHomePageCaseStudies:GetCaseStudiesSliderContent:FranchiseId={0}",
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
            DTSource.Columns.Add("imgSRC");
            DTSource.Columns.Add("counter");
            DTSource.Columns.Add("hreftext");
            DTSource.Columns.Add("dateCreated");

            var contents = SiteDataManager.GetCaseStudies();
            if (contents != null && contents.Count > 0)
            {
                foreach (Ektron.Cms.ContentData contentData in contents)
                {
                    try
                    {
                        contentXML.LoadXml(contentData.Html);

                        string hreftext = contentData.Quicklink;
                        string dateCreated = contentData.DateModified.ToString();

                        XmlNodeList xnList = contentXML.SelectNodes("/root");
                        string title = xnList[0]["title"].InnerXml;
                        string desc = xnList[0]["desc"].InnerXml;
                        string isBig = xnList[0]["isBig"].InnerXml;
                        string xml = contentXML.SelectSingleNode("/root/image").InnerXml;
                        string imgSRC =
                            Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                        counter++;

                        DTSource.Rows.Add(title, desc, imgSRC, counter, hreftext, dateCreated);
                    }
                    catch (Exception ex)
                    {
                        Log.WriteError(ex);
                    }
                }
                DataView DVPSMMSort = DTSource.DefaultView;
                DVPSMMSort.Sort = "dateCreated desc";
                sortedDT = DVPSMMSort.ToTable();

                CacheBase.Put(cacheKey, sortedDT, CacheDuration.For24Hr);
            }
        }
        return sortedDT;
    }
}