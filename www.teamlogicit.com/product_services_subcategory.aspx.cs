using System;
using System.Data;
using System.Xml;
using Ektron.Cms;
using Ektron.Cms.Framework.Content;
using System.Text.RegularExpressions;
using TeamLogic.CMS;
using Ektron.Cms.Content;
using Ektron.Cms.Common;
using Ektron.Cms.Instrumentation;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;

public partial class product_services_subcategory : PageBase
{
    protected List<long> SubCategoryIds = new List<long>();

    protected List<long> RelatedCaseStudiesIds = new List<long>();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetPSSubCategoryList();
            UxHeaderTagline.DataSource = GetPSSubCategoryContent();
            UxHeaderTagline.DataBind();

            UxHeaderTagline.DataBind();
            UxProdServSubCategoryDetail.DataSource = GetPSSubCategoryContent();
            UxProdServSubCategoryDetail.DataBind();

            UxProdServSubCategoryDetail.DataBind();
            uxCaseStudiesSlider.DataSource = GetCaseStudiesSliderContent();
            uxCaseStudiesSlider.DataBind();
        }
    }

    protected void UxProdServSubCategoryDetail_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        var uxBlogList = (Repeater)e.Item.FindControl("uxBlogList");

        long contentCategoryID = Convert.ToInt64(Request.QueryString["CategoryId"]);

        string categoryName = getCategoryName(contentCategoryID);

        var blogsData = BlogsDataManager.GetRssFeedByCategoryName(categoryName, 4);
        //when there is no blogs data related to the categoryName, get the latest 4 by default
        if (blogsData.Count == 0)
        {
            blogsData = BlogsDataManager.GetRssFeed(4);
        }
        uxBlogList.DataSource = blogsData;
        uxBlogList.DataBind();

        var UxSubProductLinks = (Repeater)e.Item.FindControl("UxSubProductLinks");

        UxSubProductLinks.DataSource = GetPSSubContent(SubCategoryIds);
        UxSubProductLinks.DataBind();

        var UxProductSubLinksTitle = (Repeater)e.Item.FindControl("UxProductSubLinksTitle");

        UxProductSubLinksTitle.DataSource = GetPSCategoryTitle();
        UxProductSubLinksTitle.DataBind();
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
        long contentID = Convert.ToInt64(Request.QueryString["sid"]); 
        DataTable DTSource = new DataTable();
        DataTable sortedDT = new DataTable();
        long folderID = ConfigHelper.GetValueLong("ProductsAndServicesSubCategoriesFolderID");
        XmlDocument contentXML = new XmlDocument();
        long PSSmartFormXMLConfig = ConfigHelper.GetValueLong("ProductsAndServicesSubCatergorySmartFormID");
        long SmartFormXMLConfig = PSSmartFormXMLConfig; //your smartform xml config mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm
        int counter = 0;

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

                        string title = xnList[0]["title"].InnerXml;
                        string subtitle = xnList[0]["subtitle"].InnerXml;
                        string pageDescription = xnList[0]["pageDescription"].InnerXml;
                        string content = xnList[0]["content"].InnerXml;
                        string teaser = xnList[0]["teaser"].InnerXml;
                        string tagline = xnList[0]["tagline"].InnerXml;

                        string xml = contentXML.SelectSingleNode("/root/image").InnerXml;
                        string imgSRC = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;

                        string url = "/product-services-subcategory/?sid=" + contentData.Id;

                        counter++;

                        string clearDiv = "";

                        if (counter % 3 == 0)
                        {
                            clearDiv = "<div class=\"clear\"></div>";
                        }


                        DTSource.Rows.Add(title, subtitle, pageDescription, imgSRC, url, content, teaser, tagline, clearDiv, counter);


                    }
                    catch (Exception ex)
                    {
                        Log.WriteError(ex);
                    }
                }
            }
        }

        DataView DVPSMMSort = DTSource.DefaultView;

        //DVPSMMSort.Sort = "counter asc";

        sortedDT = DVPSMMSort.ToTable();

        return sortedDT;
    }

    private List<Int64> GetRelatedCaseStudiesId(long PSContentID)
    {
        XmlDocument contentXML = new XmlDocument();
        List<Int64> RelatedCaseStudiesIds = new List<Int64>();

        var contents = SiteDataManager.GetCaseStudies();
        foreach (Ektron.Cms.ContentData contentData in contents)
        {
            try
            {
                contentXML.LoadXml(contentData.Html);
                XmlNodeList xnList = contentXML.SelectNodes("/root/PSCategories/PSCategoryContent");
                if (xnList.Count > 0)
                {
                    List<Int64> tempPSContentIds = new List<Int64>();
                    foreach (XmlNode xn in xnList)
                    {
                        if (xn.Attributes["datavalue_displayvalue"].Value != null)
                        {
                            long tempPSContentId = Convert.ToInt64(xn.InnerXml);

                            tempPSContentIds.Add(tempPSContentId);
                        }
                    }


                    if (tempPSContentIds.Contains(PSContentID))
                    {
                        RelatedCaseStudiesIds.Add(contentData.Id);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
        }
        return RelatedCaseStudiesIds;
    }

    private void GetPSSubCategoryList()
    {
        DataTable DTSource = new DataTable();
        DataTable sortedDT = new DataTable();
        XmlDocument contentXML = new XmlDocument();
        long contentID = Convert.ToInt64(Request.QueryString["CategoryId"]);

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

                    RelatedCaseStudiesIds = GetRelatedCaseStudiesId(contentID);

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
        long contentID = Convert.ToInt64(Request.QueryString["CategoryId"]);

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
        long contentCategoryID = Convert.ToInt64(Request.QueryString["CategoryId"]);
        int counter = 0;

        DTSource.Columns.Add("title");
        DTSource.Columns.Add("subtitle");
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

                        string url = "/product-services-subcategory/?sid=" + contentData.Id + "&CategoryId=" + contentCategoryID;

                        counter++;

                        string clearDiv = "";

                        if (counter % 3 == 0)
                        {
                            clearDiv = "<div class=\"clear\"></div>";
                        }


                        DTSource.Rows.Add(title, subtitle, imgSRC, url, content, teaser, tagline, clearDiv, counter);


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

    private DataTable GetCaseStudiesSliderContent()
    {
        DataTable DTSource = new DataTable();
        DataTable sortedDT = new DataTable();
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

                    string hreftext = "/case-studies-details/?sid=" + contentData.Id;
                    string dateCreated = contentData.DateModified.ToString();

                    XmlNodeList xnList = contentXML.SelectNodes("/root");
                    string title = xnList[0]["title"].InnerXml;
                    string desc = xnList[0]["desc"].InnerXml;
                    string isBig = xnList[0]["isBig"].InnerXml;
                    string xml = contentXML.SelectSingleNode("/root/image").InnerXml;
                    string imgSRC = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
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
        }
        return sortedDT;
    }

    protected void EmailBtn_Click(object sender, EventArgs e)
    {

        //get the page to be emailed
        StringWriter str_wrt = new StringWriter();
        HtmlTextWriter html_wrt = new HtmlTextWriter(str_wrt);
        Page.RenderControl(html_wrt);
        String HTML = str_wrt.ToString();
    }
}