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
using Figleaf;

public partial class UserControls_ProductAndServicesIconTopSlider : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UxPSHeaderSlider.DataSource = GetPSTopSlider();
            UxPSHeaderSlider.DataBind();
        }
    }

    private DataTable GetPSTopSlider()
    {
        string fransId = FransDataManager.IsFranchiseSelected() ? FransDataManager.GetFranchiseId() : "0";
        string cacheKey = String.Format("Pip:UserControls_ProductAndServicesIconTopSlider:GetPSTopSlider:FranchiseId={0}",
            fransId);

        DataTable sortedDT = CacheBase.Get<DataTable>(cacheKey);

        if (sortedDT == default(DataTable) || sortedDT.Rows.Count == 0)
        {
            DataTable DTSource = new DataTable();
            // DataTable sortedDT = new DataTable();
            XmlDocument contentXML = new XmlDocument();
            int counter = 0;

            DTSource.Columns.Add("title");
            //DTSource.Columns.Add("iconLarge");
            DTSource.Columns.Add("hrefId");
            DTSource.Columns.Add("counter");

            var contents = SiteDataManager.GetProductAndServices();
            if (contents != null && contents.Count > 0)
            {
                foreach (Ektron.Cms.ContentData contentData in contents)
                {
                    try
                    {
                        contentXML.LoadXml(contentData.Html);
                        XmlNodeList xnList = contentXML.SelectNodes("/root");
                        string title = xnList[0]["title"].InnerXml;
                        string trimTitle = title.Trim().Replace(" ", "");
                        string hrefId = "#" + trimTitle;

                        //string imgSliderSRC = "";
                        //if (contentXML.SelectSingleNode("/root/iconLarge") != null)
                        //{
                        //    string xml = contentXML.SelectSingleNode("/root/iconLarge").InnerXml;
                        //    imgSliderSRC = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                        //}
                        counter++;
                        DTSource.Rows.Add(title, hrefId, counter);
                    }
                    catch (Exception ex)
                    {
                        Log.WriteError(ex);
                    }
                }
                DataView DVPSMMSort = DTSource.DefaultView;
                DVPSMMSort.Sort = "title asc";
                sortedDT = DVPSMMSort.ToTable();
                CacheBase.Put(cacheKey, sortedDT, CacheDuration.For24Hr);
            }
        }
        return sortedDT;
    }
}