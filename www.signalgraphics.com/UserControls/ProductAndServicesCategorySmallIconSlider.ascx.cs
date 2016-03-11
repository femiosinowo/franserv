﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using SignalGraphics.CMS;
using System.Data;
using Ektron.Cms.Content;
using Ektron.Cms.Common;
using System.Text.RegularExpressions;
using Ektron.Cms.Instrumentation;
using Figleaf;

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
        
        //TO DO: null checking
        long contentCategoryID;
        long.TryParse(Request.QueryString["CategoryId"], out contentCategoryID);

        long contentSubCategoryID;
        long.TryParse(Request.QueryString["id"], out contentSubCategoryID);

        int counter = 0;

        string fransId = FransDataManager.IsFranchiseSelected() ? FransDataManager.GetFranchiseId() : "0";

        string cacheKey = String.Format("SignalGraphics:UserControls_ProductAndServicesCategoryIconSlider:GetPSTopSlider:ContentID={0}:CategoryID={1}:FranchiseId={2}", contentSubCategoryID, contentCategoryID, fransId);

        DataTable sortedDT = CacheBase.Get<DataTable>(cacheKey);

        if (sortedDT == default(DataTable) || sortedDT.Rows.Count == 0)
        {
            DataTable DTSource = new DataTable();
            // DataTable sortedDT = new DataTable();
            XmlDocument contentXML = new XmlDocument();



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
                        string url = contentData.Quicklink;

                        string xml = "";
                        string imgSliderGreySRC = "";
                        if (contentXML.SelectSingleNode("/root/iconLarge") != null)
                        {
                            xml = contentXML.SelectSingleNode("/root/iconLarge").InnerXml;
                            imgSliderGreySRC =
                                Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1]
                                    .Value;
                        }

                        string imgSliderRedSRC = "";
                        if (contentXML.SelectSingleNode("/root/iconSmallRed") != null)
                        {
                            xml = contentXML.SelectSingleNode("/root/iconSmallRed").InnerXml;
                            imgSliderRedSRC =
                                Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1]
                                    .Value;
                        }
                        counter++;

                        if ((contentData.Id == contentCategoryID && contentCategoryID != long.MinValue) ||
                            (contentCategoryID == long.MinValue && contentData.Id == contentSubCategoryID))
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
                //DVPSMMSort.Sort = "title asc";
                sortedDT = DVPSMMSort.ToTable();
                CacheBase.Put(cacheKey, sortedDT, CacheDuration.For24Hr);
            }
        }
        return sortedDT;
    }
}