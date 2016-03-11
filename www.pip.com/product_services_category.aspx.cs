using System;
using System.Data;
using System.Xml;
using Ektron.Cms;
using Ektron.Cms.Framework.Content;
using System.Text.RegularExpressions;
using SirSpeedy.CMS;
using Ektron.Cms.Content;
using Ektron.Cms.Common;
using Ektron.Cms.Instrumentation;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Drawing.Printing;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Web;

public partial class product_services_category : PageBase
{
    protected List<long> SubCategoryIds = new List<long>();
    public string MainImagePath = string.Empty;
    public string findLocationBackgroundImage = "/images/sub_find_location_bg.jpg";

    /// <summary>
    /// page init event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Init(object sender, EventArgs e)
    {
        cbPSHeaderImage.DefaultContentID = ConfigHelper.GetValueLong("PandSCategoryBannerImgCId");
        cbPSHeaderImage.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbPSHeaderImage.Fill();

        cbFindLocation.DefaultContentID = ConfigHelper.GetValueLong("ProductAndServicesFindLocationCId");
        cbFindLocation.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbFindLocation.Fill();

        if (cbFindLocation.EkItem != null && !string.IsNullOrEmpty(cbFindLocation.EkItem.Image) && cbFindLocation.EkItem.Image.ToLower().IndexOf("workarea") <= -1)
            findLocationBackgroundImage = cbFindLocation.EkItem.Image;

        findLocationSection.Attributes.Add("data-image", findLocationBackgroundImage);
        findLocationSection.Attributes.Add("data-image-mobile", findLocationBackgroundImage);
        findLocationSection.Attributes.Add("style", "background-image: url('" + findLocationBackgroundImage + "');");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            UxProdServDetail.DataSource = GetPSContent();
            UxProdServDetail.DataBind();
        }
        
    }

    protected void UxProdServDetail_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        var uxBlogList = (Repeater)e.Item.FindControl("uxBlogList");

        var uxSubCategorySlider = (Repeater)e.Item.FindControl("uxSubCategorySlider");

        var UxSubProductLinks = (Repeater)e.Item.FindControl("UxSubProductLinks");

        //TO DO: null checking
        long contentCategoryID = Convert.ToInt64(Request.QueryString["id"]);

        string categoryName = getCategoryName(contentCategoryID);

        //var blogsData = BlogsDataManager.GetRssFeedByCategoryName(categoryName, 4);

        //when there is no blogs data related to the categoryName, get the latest 4 by default
        //if (blogsData != null && blogsData.Count == 0)
       // {
           var blogsData = BlogsDataManager.GetRssFeed(4);
       // }
        uxBlogList.DataSource = blogsData;
        uxBlogList.DataBind();

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
   
    private DataTable GetPSContent()
    {
        DataTable DTSource = new DataTable();
        DataTable sortedDT = new DataTable();
        XmlDocument contentXML = new XmlDocument();

        //TO DO: null checking
        long contentID = Convert.ToInt64(Request.QueryString["id"]);

        int counter = 0;

        DTSource.Columns.Add("title");
        DTSource.Columns.Add("pageDescription");
        DTSource.Columns.Add("subtitle");
        DTSource.Columns.Add("imageSRC");
        //DTSource.Columns.Add("iconGrey");
        //DTSource.Columns.Add("iconWhite");
        //DTSource.Columns.Add("iconRed");
        DTSource.Columns.Add("hrefId");
        DTSource.Columns.Add("url");
        DTSource.Columns.Add("content");
        DTSource.Columns.Add("teaser");
        //DTSource.Columns.Add("quotesBy_Name");
        //DTSource.Columns.Add("quotesBy_Organization");
        //DTSource.Columns.Add("statement");
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
                //string quotesByName = xnList[0]["quotesByName"].InnerXml;
                //string quotesByOrganization = xnList[0]["quotesByOrganization"].InnerXml;
                //string statement = xnList[0]["statement"].InnerXml;

                if (FransDataManager.IsFranchiseSelected())
                {
                    findAlocationDiv.Visible = false;
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

                string xml = "";
                string imgSliderSRC = "";
                //string iconGrey = "";
                //string iconRed = "";
                //string iconWhite = "";

                if (contentXML.SelectSingleNode("/root/imageSlider") != null)
                {
                    xml = contentXML.SelectSingleNode("/root/imageSlider").InnerXml;
                    imgSliderSRC = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;                    
                }
                //if (contentXML.SelectSingleNode("/root/iconLarge") != null)
                //{
                //    xml = contentXML.SelectSingleNode("/root/iconLarge").InnerXml;
                //    iconGrey = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                //}
                //if (contentXML.SelectSingleNode("/root/iconSmallRed") != null)
                //{
                //    xml = contentXML.SelectSingleNode("/root/iconSmallRed").InnerXml;
                //    iconRed = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                //}
                //if (contentXML.SelectSingleNode("/root/headerIconSmallWhite") != null)
                //{
                //    xml = contentXML.SelectSingleNode("/root/headerIconSmallWhite").InnerXml;
                //    iconWhite = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                //}

                string url = contentData.Quicklink;
                counter++;

                DTSource.Rows.Add(title, pageDescription, subtitle, imgSliderSRC, hrefId, url, content, teaser, tagline, "", counter, "", "");

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
        long contentCategoryID = Convert.ToInt64(Request.QueryString["id"]);

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
                        string learnMoreButton = "<a class=\"cta-button-text cta-button-wrap white-orange-btn\" href=\"" + url + "\"><span>Learn More</span></a>";
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

                        DTSource.Rows.Add(title, subtitle, imgSRC, url, content, teaser, tagline, clearDiv, learnMoreButton, cssClass, counter);

                       
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

}