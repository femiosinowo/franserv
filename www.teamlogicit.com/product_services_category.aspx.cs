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
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Drawing.Printing;
using System.Drawing;
using System.IO;
using System.Web.UI;

public partial class product_services_category : PageBase
{
    protected List<long> SubCategoryIds = new List<long>();

    protected List<long> RelatedCaseStudiesIds = new List<long>();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            UxHeaderTagline.DataSource = GetPSContent();
            UxHeaderTagline.DataBind();

            UxProdServDetail.DataSource = GetPSContent();
            UxProdServDetail.DataBind();

            DataTable PSSubSideContent = new DataTable();
            if (SubCategoryIds.Count > 0)
            {
                PSSubSideContent = GetPSSubContent(SubCategoryIds);
            }
            else
            {
                PSSubSideContent = GetPSContent();
            }

            uxSubCategorySlider.DataSource = PSSubSideContent;
            uxSubCategorySlider.DataBind();

            if (SubCategoryIds.Count > 0)
            {
                UxSubProductLinks.DataSource = PSSubSideContent;

                UxSubProductLinks.DataBind();
            }

            uxCaseStudiesSlider.DataSource = GetCaseStudiesSliderContent();
            uxCaseStudiesSlider.DataBind();
        }
        
    }

    protected void UxProdServDetail_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        var uxBlogList = (Repeater)e.Item.FindControl("uxBlogList");

        //TO DO: null checking
        long contentCategoryID = Convert.ToInt64(Request.QueryString["sid"]);

        string categoryName = getCategoryName(contentCategoryID);

        var blogsData = BlogsDataManager.GetRssFeedByCategoryName(categoryName, 4);

        //when there is no blogs data related to the categoryName, get the latest 4 by default
        if (blogsData.Count == 0)
        {
            blogsData = BlogsDataManager.GetRssFeed(4);
        }
        uxBlogList.DataSource = blogsData;
        uxBlogList.DataBind();
    }

    private string getCategoryName(long categoryid)
    {
        string categoryName = string.Empty;
        XmlDocument contentXML = new XmlDocument();

        var contentData = ContentHelper.GetContentById(categoryid);
        if (contentData != null && contentData.Html != string.Empty)
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
        return categoryName;
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

    private DataTable GetPSContent()
    {
        DataTable DTSource = new DataTable();
        DataTable sortedDT = new DataTable();
        XmlDocument contentXML = new XmlDocument();

        //TO DO: null checking
        long contentID = Convert.ToInt64(Request.QueryString["sid"]);

        int counter = 0;

        DTSource.Columns.Add("title");
        DTSource.Columns.Add("pageDescription");
        DTSource.Columns.Add("subtitle");
        DTSource.Columns.Add("imageSRC");
        DTSource.Columns.Add("iconGrey");
        DTSource.Columns.Add("iconWhite");
        DTSource.Columns.Add("iconRed");
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
        DTSource.Columns.Add("clearDiv");
        DTSource.Columns.Add("LearnMoreButton");

        var contentData = ContentHelper.GetContentById(contentID);
        if (contentData != null && contentData.Html != string.Empty)
        {
            try
            {
                contentXML.LoadXml(contentData.Html);
                XmlNodeList xnList = contentXML.SelectNodes("/root");
                string title = xnList[0]["title"].InnerXml;
                string hrefId = title.Trim().Replace(" ", ""); ;
                string subtitle = xnList[0]["subtitle"].InnerXml;
                string pageDescription = xnList[0]["pageDescription"].InnerXml;
                string content = xnList[0]["content"].InnerXml;
                string teaser = xnList[0]["teaser"].InnerXml;
                string tagline = xnList[0]["tagline"].InnerXml;
                string quotesByName = xnList[0]["quotesByName"].InnerXml;
                string quotesByOrganization = xnList[0]["quotesByOrganization"].InnerXml;
                string statement = xnList[0]["statement"].InnerXml;

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

                RelatedCaseStudiesIds = GetRelatedCaseStudiesId(contentData.Id);
                string xml = "";
                string imgSliderSRC = "";
                string iconGrey = "";
                string iconRed = "";
                string iconWhite = "";

                if (contentXML.SelectSingleNode("/root/imageSlider") != null)
                {
                    xml = contentXML.SelectSingleNode("/root/imageSlider").InnerXml;
                    imgSliderSRC = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                }
                if (contentXML.SelectSingleNode("/root/iconLarge") != null)
                {
                    xml = contentXML.SelectSingleNode("/root/iconLarge").InnerXml;
                    iconGrey = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                }
                if (contentXML.SelectSingleNode("/root/iconSmallRed") != null)
                {
                    xml = contentXML.SelectSingleNode("/root/iconSmallRed").InnerXml;
                    iconRed = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                }
                if (contentXML.SelectSingleNode("/root/headerIconSmallWhite") != null)
                {
                    xml = contentXML.SelectSingleNode("/root/headerIconSmallWhite").InnerXml;
                    iconWhite = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                }

                string url = "/product-services-category/?sid=" + contentData.Id;
                counter++;

                DTSource.Rows.Add(title, pageDescription, subtitle, imgSliderSRC, iconGrey, iconWhite, iconRed, hrefId, url, content, teaser, quotesByName, quotesByOrganization, statement, tagline, "", counter, "", "");

                DataView DVPSMMSort = DTSource.DefaultView;
                DVPSMMSort.Sort = "title asc";
                sortedDT = DVPSMMSort.ToTable();
            
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
        }      

        return sortedDT;
    }

    private DataTable GetPSSubContent(List<long> SubCatIds)
    {
        DataTable DTSource = new DataTable();
        DataTable sortedDT = new DataTable();
        long folderID = ConfigHelper.GetValueLong("ProductsAndServicesSubCategoriesFolderID");
        XmlDocument contentXML = new XmlDocument();
        long PSSmartFormXMLConfig = ConfigHelper.GetValueLong("ProductsAndServicesSubCatergorySmartFormID");
        long SmartFormXMLConfig = PSSmartFormXMLConfig; //your smartform xml config mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm

        //TO DO: null checking
        long contentCategoryID = Convert.ToInt64(Request.QueryString["sid"]);

        int counter = 0;

        DTSource.Columns.Add("title");
        DTSource.Columns.Add("subtitle");
        DTSource.Columns.Add("imageSRC");
        DTSource.Columns.Add("url");
        DTSource.Columns.Add("content");
        DTSource.Columns.Add("teaser");
        DTSource.Columns.Add("tagline");
        DTSource.Columns.Add("clearDiv");
        DTSource.Columns.Add("LearnMoreButton");
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

                        string learnMoreButton = "<div class=\"cta-button-wrap purple small\"><a class=\"cta-button-text\" href=\""+ url +"\"><span>Learn More</span></a></div>";

                        string clearDiv = "";

                        if (counter % 3 == 0)
                        {
                            clearDiv = "<div class=\"clear\"></div>";
                        }

                        DTSource.Rows.Add(title, subtitle, imgSRC, url, content, teaser, tagline, clearDiv, learnMoreButton, counter);

                       
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
}