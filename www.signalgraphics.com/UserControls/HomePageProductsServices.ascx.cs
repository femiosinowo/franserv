﻿using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Xml;
using Figleaf;
using SignalGraphics.CMS;
using Ektron.Cms.Instrumentation;

public partial class UserControls_HomePageProductsServices : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable DTPSSliderSource = GetPSContent();
            UxProdAndServicesSlider.DataSource = DTPSSliderSource;
            UxProdAndServicesSlider.DataBind();
        }
    }

    private DataTable GetPSContent()
    {
        string fransId = FransDataManager.IsFranchiseSelected() ? FransDataManager.GetFranchiseId() : "0";
        string cacheKey = String.Format("SignalGraphics:HomePageProductsServices:GetPSContent:FranchiseId={0}",
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
            DTSource.Columns.Add("url");
            DTSource.Columns.Add("content");
            DTSource.Columns.Add("teaser");
            DTSource.Columns.Add("tagline");
            DTSource.Columns.Add("hrefText");
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
                        string subtitle = xnList[0]["subtitle"].InnerXml;
                        string content = xnList[0]["content"].InnerXml;
                        string teaser = xnList[0]["teaser"].InnerXml;
                        string tagline = xnList[0]["tagline"].InnerXml;

                        string hrefText = contentData.Quicklink;


                        string xml = contentXML.SelectSingleNode("/root/imageSlider").InnerXml;
                        string imgSliderSRC =
                            Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                        string url =
                            Regex.Match(xml, "<a.+?href=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                        //not sure about this
                        counter++;

                        DTSource.Rows.Add(title, subtitle, imgSliderSRC, url, content, teaser, tagline, hrefText,
                            counter);

                        DataView DVPSMMSort = DTSource.DefaultView;

                        DVPSMMSort.Sort = "title asc";

                        sortedDT = DVPSMMSort.ToTable();
                    }
                    catch (Exception ex)
                    {
                        Log.WriteError(ex);
                    }
                }


                CacheBase.Put(cacheKey, sortedDT, CacheDuration.For24Hr);
            }
        }
        return sortedDT;
    }    

}