using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using TeamLogic.CMS;
using System.Data;
using Ektron.Cms.Content;
using Ektron.Cms.Common;
using System.Text.RegularExpressions;
using Ektron.Cms.Instrumentation;

public partial class UserControls_ProductAndServicesCategoryIconSlider : System.Web.UI.UserControl
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
        DataTable DTSource = new DataTable();
        DataTable sortedDT = new DataTable();
        XmlDocument contentXML = new XmlDocument();

        //TO DO: null checking
        long contentCategoryID = (Request.QueryString["CategoryId"] != null) ? Convert.ToInt64(Request.QueryString["CategoryId"]) : long.MinValue;
        long contentSubCategoryID = (Request.QueryString["sid"] != null)? Convert.ToInt64(Request.QueryString["sid"]) : long.MinValue;

        int counter = 0;

        DTSource.Columns.Add("title");
        DTSource.Columns.Add("iconSmall");
        DTSource.Columns.Add("url");
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
                    string url = "/product-services-category/?sid=" + contentData.Id;

                    string xml = "";
                    string imgSliderGreySRC = "";
                    if (contentXML.SelectSingleNode("/root/iconLarge") != null)
                    {
                        xml = contentXML.SelectSingleNode("/root/iconLarge").InnerXml;
                        imgSliderGreySRC = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                    }

                    string imgSliderRedSRC = "";
                    if (contentXML.SelectSingleNode("/root/iconSmallRed") != null)
                    {
                        xml = contentXML.SelectSingleNode("/root/iconSmallRed").InnerXml;
                        imgSliderRedSRC = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                    }
                    counter++;

                    if ((contentData.Id == contentCategoryID && contentCategoryID != long.MinValue) || (contentCategoryID == long.MinValue && contentData.Id == contentSubCategoryID))
                    {
                        DTSource.Rows.Add(title, imgSliderRedSRC, url, counter);
                    }
                    else
                    {
                        DTSource.Rows.Add(title, imgSliderGreySRC, url, counter);
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }
            DataView DVPSMMSort = DTSource.DefaultView;
            DVPSMMSort.Sort = "title asc";
            sortedDT = DVPSMMSort.ToTable();
        }
        return sortedDT;
    }
}
