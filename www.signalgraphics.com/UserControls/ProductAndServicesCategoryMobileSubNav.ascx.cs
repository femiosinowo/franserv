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

public partial class UserControls_ProductAndServicesCategorySubNav : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RptPSCategoryMobileSubNav.DataSource = GetPSContent();
            RptPSCategoryMobileSubNav.DataBind();
        }
    }

    private DataTable GetPSContent()
    {
        string fransId = FransDataManager.IsFranchiseSelected() ? FransDataManager.GetFranchiseId() : "0";
        string cacheKey = String.Format("SignalGraphics:UserControls_ProductAndServicesCategorySubNav:GetPSContent:FranchiseId={0}",
            fransId);

        DataTable sortedDT = CacheBase.Get<DataTable>(cacheKey);

        if (sortedDT == default(DataTable) || sortedDT.Rows.Count == 0)
        {
            DataTable DTSource = new DataTable();
            //DataTable sortedDT = new DataTable();
            XmlDocument contentXML = new XmlDocument();
            int counter = 0;

            DTSource.Columns.Add("title");
            DTSource.Columns.Add("subtitle");
            DTSource.Columns.Add("imageSliderSRC");
            DTSource.Columns.Add("iconSmallGrey");
            DTSource.Columns.Add("iconSmallRed");
            DTSource.Columns.Add("iconSmallWhite");
            DTSource.Columns.Add("headerIconSmallWhite");
            DTSource.Columns.Add("hrefId");
            DTSource.Columns.Add("url");
            DTSource.Columns.Add("content");
            DTSource.Columns.Add("teaser");
            DTSource.Columns.Add("quotesBy_Name");
            DTSource.Columns.Add("quotesBy_Organization");
            DTSource.Columns.Add("statement");
            DTSource.Columns.Add("tagline");
            DTSource.Columns.Add("subcategory");
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
                        string hrefId = title.Trim().Replace(" ", "");
                        ;
                        string subtitle = xnList[0]["subtitle"].InnerXml;
                        string content = xnList[0]["content"].InnerXml;
                        string teaser = content.Substring(0, 450).TrimEnd() + "...";
                        string tagline = xnList[0]["tagline"].InnerXml;
                        string quotesByName = xnList[0]["quotesByName"].InnerXml;
                        string quotesByOrganization = xnList[0]["quotesByOrganization"].InnerXml;
                        string statement = xnList[0]["statement"].InnerXml;

                        //XmlNodeList xnList2 = contentXML.SelectNodes("/root/ViewPort");
                        //List<long> SubCategoryIds = new List<long>();
                        //foreach (XmlNode xn in xnList2)
                        //{
                        //    string subcategoryId = xn["ViewPortContent"].InnerXml;

                        //    SubCategoryIds.Add(Convert.ToInt64(subcategoryId));
                        //}

                        string xml = "";
                        string imgSliderSRC = "";
                        string iconSmallWhite = "";
                        string iconSmallGrey = "";
                        string iconSmallRed = "";
                        string headerIconSmallWhite = "";
                        if (contentXML.SelectSingleNode("/root/imageSlider") != null)
                        {
                            xml = contentXML.SelectSingleNode("/root/imageSlider").InnerXml;
                            imgSliderSRC =
                                Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1]
                                    .Value;
                        }
                        if (contentXML.SelectSingleNode("/root/iconSmallWhite") != null)
                        {
                            xml = contentXML.SelectSingleNode("/root/iconSmallWhite").InnerXml;
                            iconSmallWhite =
                                Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1]
                                    .Value;
                        }
                        if (contentXML.SelectSingleNode("/root/iconSmallGrey") != null)
                        {
                            xml = contentXML.SelectSingleNode("/root/iconSmallGrey").InnerXml;
                            iconSmallGrey =
                                Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1]
                                    .Value;
                        }
                        if (contentXML.SelectSingleNode("/root/iconSmallRed") != null)
                        {
                            xml = contentXML.SelectSingleNode("/root/iconSmallRed").InnerXml;
                            iconSmallRed =
                                Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1]
                                    .Value;
                        }
                        if (contentXML.SelectSingleNode("/root/headerIconSmallWhite") != null)
                        {
                            xml = contentXML.SelectSingleNode("/root/headerIconSmallWhite").InnerXml;
                            headerIconSmallWhite =
                                Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1]
                                    .Value;
                        }
                        string url = contentData.Quicklink;
                        counter++;

                        DTSource.Rows.Add(title, subtitle, imgSliderSRC, iconSmallGrey, iconSmallRed, iconSmallWhite,
                            headerIconSmallWhite, hrefId, url, content, teaser, quotesByName, quotesByOrganization,
                            statement, tagline, "", counter);
                    }
                    catch (Exception ex)
                    {
                        Log.WriteError(ex);
                    }
                }
                DataView DVPSMMSort = DTSource.DefaultView;
                //DVPSMMSort.Sort = "title asc";
                sortedDT = DVPSMMSort.ToTable();
                CacheBase.Put(cacheKey, sortedDT, CacheDuration.For24Hr);
            }
        }
        return sortedDT;
    }
}