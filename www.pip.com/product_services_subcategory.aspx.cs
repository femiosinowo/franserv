using System;
using System.Data;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using Ektron.Cms;
using Ektron.Cms.Framework.Content;
using System.Text.RegularExpressions;
using SirSpeedy.CMS;
using Ektron.Cms.Content;
using Ektron.Cms.Common;
using Ektron.Cms.Instrumentation;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Web;

public partial class product_services_subcategory : PageBase
{
    long categoryId = 0;
    protected List<long> SubCategoryIds = new List<long>();
    public string MainImagePath = string.Empty;

    /// <summary>
    /// page init event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Init(object sender, EventArgs e)
    {
        cbPSSubCatHeaderImage.DefaultContentID = ConfigHelper.GetValueLong("PandSsubCategoryBannerImgCId");
        cbPSSubCatHeaderImage.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbPSSubCatHeaderImage.Fill();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetCategoryId();
            GetPSSubCategoryList();
           
            UxProdServSubCategoryDetail.DataSource = GetPSSubCategoryContent();
            UxProdServSubCategoryDetail.DataBind();
        }
        find_location_div.Visible = !FransDataManager.IsFranchiseSelected();
    }

    protected void UxProdServSubCategoryDetail_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        var uxBlogList = (Repeater)e.Item.FindControl("uxBlogList");

        long contentCategoryID = categoryId; // Convert.ToInt64(Request.QueryString["CategoryId"]);

        string categoryName = getCategoryName(contentCategoryID);

        //var blogsData = BlogsDataManager.GetRssFeedByCategoryName(categoryName, 4);
        //when there is no blogs data related to the categoryName, get the latest 4 by default
        //if (blogsData != null && blogsData.Any())
       // {
            var blogsData = BlogsDataManager.GetRssFeed(4);
            uxBlogList.DataSource = blogsData;
            uxBlogList.DataBind();
       // }        

        var UxSubProductLinks = (Repeater)e.Item.FindControl("UxSubProductLinks");

        UxSubProductLinks.DataSource = GetPSSubContent(SubCategoryIds);
        UxSubProductLinks.DataBind();

        //var UxProductSubLinksTitle = (Repeater)e.Item.FindControl("UxProductSubLinksTitle");

        //UxProductSubLinksTitle.DataSource = GetPSCategoryTitle();
        //UxProductSubLinksTitle.DataBind();
    }

    private string getCategoryName(long categoryid)
    {
        string categoryName = string.Empty;
        XmlDocument contentXML = new XmlDocument();

        var contents = SiteDataManager.GetProductAndServices();
        foreach (Ektron.Cms.ContentData contentData in contents)
        {
            if (contentData.Id == categoryid)
            {
                try
                {
                    contentXML.LoadXml(contentData.Html);

                    XmlNodeList xnList = contentXML.SelectNodes("/root");
                    categoryName = xnList[0]["title"].InnerXml;

                    return categoryName;

                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }
        }
        return categoryName;
    }

    private DataTable GetPSSubCategoryContent()
    {
        //TO DO: null checking
        long contentID = Convert.ToInt64(Request.QueryString["id"]);
        DataTable DTSource = new DataTable();
        DataTable sortedDT = new DataTable();
        long folderID = ConfigHelper.GetValueLong("ProductsAndServicesSubCategoriesFolderID");
        XmlDocument contentXML = new XmlDocument();
        long PSSmartFormXMLConfig = ConfigHelper.GetValueLong("ProductsAndServicesSubCatergorySmartFormID");
        long SmartFormXMLConfig = PSSmartFormXMLConfig; //your smartform xml config mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm
        int counter = 0;

        DTSource.Columns.Add("CategoryTitle");
        DTSource.Columns.Add("title");
        DTSource.Columns.Add("subtitle");
        DTSource.Columns.Add("pageDescription");
        DTSource.Columns.Add("imageSRC");
        DTSource.Columns.Add("url");
        DTSource.Columns.Add("content");
        DTSource.Columns.Add("teaser");
        DTSource.Columns.Add("tagline");
        DTSource.Columns.Add("clearDiv");
        DTSource.Columns.Add("counter");

        //get all contents in the specified folder
        ContentCriteria criteria = new ContentCriteria();
        criteria.AddFilter(ContentProperty.FolderId, CriteriaFilterOperator.EqualTo, folderID);
        criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, SmartFormXMLConfig);
        var contents = ContentHelper.GetListByCriteria(criteria);

        if (contents != null && contents.Count > 0)
        {
            foreach (Ektron.Cms.ContentData contentData in contents)
            {
                if (contentID == contentData.Id)
                {
                    try
                    {
                        contentXML.LoadXml(contentData.Html);
                        XmlNodeList xnList = contentXML.SelectNodes("/root");

                        string categoryTitle = getCategoryName(categoryId);
                        string title = xnList[0]["title"].InnerXml;
                        string subtitle = xnList[0]["subtitle"].InnerXml;
                        string pageDescription = xnList[0]["pageDescription"].InnerXml;
                        string content = xnList[0]["content"].InnerXml;
                        string teaser = xnList[0]["teaser"].InnerXml;
                        string tagline = xnList[0]["tagline"].InnerXml;

                        string xml = contentXML.SelectSingleNode("/root/image").InnerXml;
                        string imgSRC = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                        string domainName = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
                        MainImagePath = "http://" + domainName + imgSRC;

                        string url = contentData.Quicklink;

                        counter++;

                        string clearDiv = "";

                        if (counter % 3 == 0)
                        {
                            clearDiv = "<div class=\"clear\"></div>";
                        }
                        DTSource.Rows.Add(categoryTitle, title, subtitle, pageDescription, imgSRC, url, content, teaser, tagline, clearDiv, counter);
                    }
                    catch (Exception ex)
                    {
                        Log.WriteError(ex);
                    }
                }
            }
        }

        DataView DVPSMMSort = DTSource.DefaultView;

        DVPSMMSort.Sort = "counter asc";

        sortedDT = DVPSMMSort.ToTable();

        return sortedDT;
    }

    private void GetPSSubCategoryList()
    {
        DataTable DTSource = new DataTable();
        DataTable sortedDT = new DataTable();
        XmlDocument contentXML = new XmlDocument();
        long contentID = categoryId; // Convert.ToInt64(Request.QueryString["CategoryId"]);

        var contents = SiteDataManager.GetProductAndServices();
        foreach (Ektron.Cms.ContentData contentData in contents)
        {
            if (contentData.Id == contentID)
            {
                try
                {
                    contentXML.LoadXml(contentData.Html);

                    if (FransDataManager.IsFranchiseSelected())
                    {
                        Dictionary<long, List<long>> localPSInfo = SiteDataManager.GetProductAndServicesLocalInfo();

                        if (localPSInfo[contentID] != null && localPSInfo.ContainsKey(contentID))
                        {
                            SubCategoryIds = localPSInfo[contentID];
                        }
                    }
                    else
                    {

                        XmlNodeList xnList2 = contentXML.SelectNodes("/root/ViewPort/ViewPortContent");
                        if (xnList2.Count > 0)
                        {
                            foreach (XmlNode xn in xnList2)
                            {
                                if (xn.Attributes["datavalue_displayvalue"].Value != null)
                                {
                                    string subcategoryId = xn.InnerXml;

                                    SubCategoryIds.Add(Convert.ToInt64(subcategoryId));
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }
        }
    }

    private DataTable GetPSCategoryTitle()
    {
        DataTable DTSource = new DataTable();
        DataTable sortedDT = new DataTable();
        XmlDocument contentXML = new XmlDocument();
        long contentID = categoryId; // Convert.ToInt64(Request.QueryString["CategoryId"]);

        DTSource.Columns.Add("title");

        var contents = SiteDataManager.GetProductAndServices();
        foreach (Ektron.Cms.ContentData contentData in contents)
        {
            if (contentData.Id == contentID)
            {
                try
                {
                    contentXML.LoadXml(contentData.Html);

                    XmlNodeList xnList = contentXML.SelectNodes("/root");
                    string title = xnList[0]["title"].InnerXml;

                    DTSource.Rows.Add(title);

                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }
        }

        return DTSource;
    }

    private DataTable GetPSSubContent(List<long> SubCatIds)
    {
        DataTable DTSource = new DataTable();
        DataTable sortedDT = new DataTable();
        long folderID = ConfigHelper.GetValueLong("ProductsAndServicesSubCategoriesFolderID");
        XmlDocument contentXML = new XmlDocument();
        long PSSmartFormXMLConfig = ConfigHelper.GetValueLong("ProductsAndServicesSubCatergorySmartFormID");
        long SmartFormXMLConfig = PSSmartFormXMLConfig; //your smartform xml config mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm
        long contentCategoryID = categoryId;// Convert.ToInt64(Request.QueryString["CategoryId"]);
        int counter = 0;

        DTSource.Columns.Add("title");
        DTSource.Columns.Add("subtitle");
        DTSource.Columns.Add("imageSRC");
        DTSource.Columns.Add("url");
        DTSource.Columns.Add("content");
        DTSource.Columns.Add("teaser");
        DTSource.Columns.Add("tagline");
        DTSource.Columns.Add("clearDiv");
        DTSource.Columns.Add("cssClass");
        DTSource.Columns.Add("counter");

        var contents = SiteDataManager.GetProductAndServices_SubCategoryContent(SubCatIds);
        if (contents != null && contents.Count > 0)
        {
            foreach (Ektron.Cms.ContentData contentData in contents)
            {
                if (SubCatIds.Contains(contentData.Id))
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

                        string xml = contentXML.SelectSingleNode("/root/image").InnerXml;
                        string imgSRC = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                        if (counter == 0)
                        {
                            string domainName = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
                            MainImagePath = "http://" + domainName + imgSRC;
                        }
                        string url = contentData.Quicklink;

                        counter++;

                        string clearDiv = "";

                        if (counter % 3 == 0)
                        {
                            clearDiv = "<div class=\"clear\"></div>";
                        }

                        string cssClass = string.Empty;
                        if (counter == 1 || ((((counter - 1) % 3)) == 0))
                        {
                            cssClass = "alpha";
                        }
                        else if (counter % 3 == 0)
                        {
                            cssClass = "omega";
                        }


                        DTSource.Rows.Add(title, subtitle, imgSRC, url, content, teaser, tagline, clearDiv, cssClass, counter);


                    }
                    catch (Exception ex)
                    {
                        Log.WriteError(ex);
                    }
                }
            }
        }

        DataView DVPSMMSort = DTSource.DefaultView;
        DVPSMMSort.Sort = "counter asc";
        sortedDT = DVPSMMSort.ToTable();
        return sortedDT;
    }

    private void GetCategoryId()
    {
        try
        {
            string url = Request.RawUrl;
            string modifiedURL = url.Remove(url.Length - 1, 1).Remove(0, 1);
            string categoryURL = modifiedURL.Remove(modifiedURL.LastIndexOf("/"), modifiedURL.Length - modifiedURL.LastIndexOf("/")) + "/";
            if (!string.IsNullOrEmpty(categoryURL))
            {
                var cc = new Ektron.Cms.Settings.UrlAliasing.DataObjects.AliasCriteria();
                cc.AddFilter(Ektron.Cms.Settings.UrlAliasing.DataObjects.AliasProperty.Alias, CriteriaFilterOperator.EqualTo, categoryURL);
                var list = AliasHelper.GetList(cc);
                if (list != null && list.Count > 0)
                {
                    categoryId = list.FirstOrDefault().TargetId;
                }
            }
        }
        catch (Exception ex)
        {
            Log.WriteError(ex);
            Log.WriteError("Exception extracting category id on product_services_subcategory.aspx.cx");
        }
    }

}