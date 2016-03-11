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
using System.Linq;

public partial class product_services : System.Web.UI.Page
{
    protected int rowNumber = 0;

    protected DataTable PSContent = new DataTable();

    /// <summary>
    /// page init event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Init(object sender, EventArgs e)
    {
        cbPSHeaderImage.DefaultContentID = ConfigHelper.GetValueLong("PSHeaderImageContentId");
        cbPSHeaderImage.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbPSHeaderImage.Fill();

        cbFindLocationText.DefaultContentID = ConfigHelper.GetValueLong("ProductAndServicesFindLocationCId");
        cbFindLocationText.CacheInterval = ConfigHelper.GetValueLong("smallCacheInterval");
        cbFindLocationText.Fill();

        if (cbFindLocationText.EkItem != null && !string.IsNullOrEmpty(cbFindLocationText.EkItem.Image) && cbFindLocationText.EkItem.Image.ToLower().IndexOf("workarea") <= -1)
        {
            findLocationText.Attributes.Add("data-image", cbFindLocationText.EkItem.Image);
            findLocationText.Attributes.Add("data-image-mobile", cbFindLocationText.EkItem.Image);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.BodyClass += " products-services ";
        if (!Page.IsPostBack)
        {
            //GetFindLocationLibraryImage();
            PSContent = GetPSContent();
            UxPSIndex.DataSource = PSContent;
            UxPSIndex.DataBind();
        }
    }

    //protected void UxPSIndex_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
    //{
    //    List<long> SubCategoryIds = new List<long>();

    //    string tempSubCatIds = PSContent.Rows[rowNumber]["subcategoryIds"].ToString();

    //    long CatId = Convert.ToInt64(PSContent.Rows[rowNumber]["categoryId"]);

    //    SubCategoryIds = FromString(tempSubCatIds);

    //    //Get Inner Repeater
    //    Repeater UxLargerSlider = (Repeater)e.Item.FindControl("UxLargerSlider");

    //    Repeater UxThumbSlider = (Repeater)e.Item.FindControl("UxThumbSlider");

    //    DataTable SubCategoryDataIds = GetSubCategoryData(SubCategoryIds);
    //    if (SubCategoryDataIds.Rows.Count > 0)
    //    {
    //        UxLargerSlider.DataSource = SubCategoryDataIds;
    //        UxThumbSlider.DataSource = SubCategoryDataIds;

    //        UxLargerSlider.DataBind();
    //        UxThumbSlider.DataBind();
    //    }
    //    else
    //    {
    //        UxLargerSlider.DataSource = GetCategoryImageData(CatId);
    //        UxLargerSlider.DataBind();
    //        //UxThumbSlider.DataSource = GetPSContent();
    //    }
    //    rowNumber++;
    //}

    //private static List<Int64> FromString(string value)
    //{
    //    return new List<Int64>(
    //       value
    //        .Split(new char[] { ',', '[', ']' }, StringSplitOptions.RemoveEmptyEntries)
    //        .Select(s => Convert.ToInt64(s))
    //    );
    //}

    private DataTable GetSubCategoryData(List<long> subCategoryIds)
    {
        DataTable DTSource = new DataTable();
        DataTable sortedDT = new DataTable();
        long folderID = ConfigHelper.GetValueLong("ProductsAndServicesSubCategoriesFolderID");
        XmlDocument contentXML = new XmlDocument();
        long PSSmartFormXMLConfig = ConfigHelper.GetValueLong("ProductsAndServicesSubCatergorySmartFormID");
        long SmartFormXMLConfig = PSSmartFormXMLConfig; //your smartform xml config mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm
        int counter = 0;

        DTSource.Columns.Add("title");
        DTSource.Columns.Add("imageSRC");
        DTSource.Columns.Add("caption");
        DTSource.Columns.Add("hrefText");
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
                if (subCategoryIds.Contains(contentData.Id))
                {
                    try
                    {
                        contentXML.LoadXml(contentData.Html);
                        XmlNodeList xnList = contentXML.SelectNodes("/root");
                        string title = xnList[0]["title"].InnerXml;
                        string caption = xnList[0]["caption"].InnerXml;
                        string hrefText = contentData.Quicklink;                 
            

                        string xml = contentXML.SelectSingleNode("/root/image").InnerXml;
                        string imgSRC = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                        counter++;

                        DTSource.Rows.Add(title, imgSRC, caption, hrefText,counter); 
                    }
                    catch (Exception ex)
                    {
                        Log.WriteError(ex);
                    }
                }
            }
            DataView DVPSMMSort = DTSource.DefaultView;
            DVPSMMSort.Sort = "title asc";
            sortedDT = DVPSMMSort.ToTable();
        }
        return sortedDT;
    }

    private DataTable GetPSContent()
    {
        DataTable DTSource = new DataTable();
        DataTable sortedDT = new DataTable();
        XmlDocument contentXML = new XmlDocument();
        int counter = 0;

        DTSource.Columns.Add("title");
        DTSource.Columns.Add("subtitle");
        DTSource.Columns.Add("imageSRC");
        //DTSource.Columns.Add("iconGrey");
        //DTSource.Columns.Add("iconWhite");
       // DTSource.Columns.Add("iconRed");
        DTSource.Columns.Add("hrefId");
        DTSource.Columns.Add("hrefText");
        DTSource.Columns.Add("content");
        DTSource.Columns.Add("teaser");
        DTSource.Columns.Add("caption");
        //DTSource.Columns.Add("quotesBy_Name");
        //DTSource.Columns.Add("quotesBy_Organization");
        DTSource.Columns.Add("statement");
        DTSource.Columns.Add("tagline");
        DTSource.Columns.Add("subcategoryIds");
        DTSource.Columns.Add("categoryId");
        DTSource.Columns.Add("subcategoryTitle");
        DTSource.Columns.Add("subcategoryTagline");
        DTSource.Columns.Add("subcategoryImageSRC");
        DTSource.Columns.Add("counter");

        var contents = SiteDataManager.GetProductAndServices();
        if (contents != null && contents.Count > 0)
        {
            foreach (Ektron.Cms.ContentData contentData in contents)
            {
                try
                {
                    contentXML.LoadXml(contentData.Html);
                    string imgSliderSRC = "";
                    XmlNodeList xnList = contentXML.SelectNodes("/root");
                    string title = xnList[0]["title"].InnerXml;
                    string hrefId = title.Trim().Replace(" ", "");
                    string subtitle = (xnList[0]["subtitle"] != null) ? xnList[0]["subtitle"].InnerXml : "";
                    string content = (xnList[0]["content"] != null) ? xnList[0]["content"].InnerXml : "";
                    string teaser = (xnList[0]["teaser"] != null) ? xnList[0]["teaser"].InnerXml : "";
                    string caption = (xnList[0]["caption"] != null) ? xnList[0]["caption"].InnerXml : "";
                    string tagline = (xnList[0]["tagline"] != null) ? xnList[0]["tagline"].InnerXml : "";
                    //string quotesByName = xnList[0]["quotesByName"].InnerXml;
                    //string quotesByOrganization = xnList[0]["quotesByOrganization"].InnerXml;
                    string statement =(xnList[0]["statement"]!= null) ? xnList[0]["statement"].InnerXml:"";
                    string xml = (xnList[0]["imageSlider"] != null) ? xnList[0]["imageSlider"].InnerXml : "";
                    if (!string.IsNullOrEmpty(xml))
                        imgSliderSRC = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;

                    long catId = contentData.Id;
                    
                    XmlNodeList xnList2 = contentXML.SelectNodes("/root/ViewPort/ViewPortContent");
                    List<long> subcategoryIds = new List<long>();
                    if (xnList2.Count > 0)
                    {
                        foreach (XmlNode xn in xnList2)
                        {
                            if (xn.Attributes["datavalue_displayvalue"].Value != null)
                            {
                                string subcategoryId = xn.InnerXml;
                                subcategoryIds.Add(long.Parse(subcategoryId));
                                break;
                            }
                        }
                    }

                    string subcategoryTitle = "";
                    string subcategoryTagline = "";
                    string subcategoryImageSRC = "";
                    bool subCategoryContentExist = false;
                    if (subcategoryIds.Any())
                    {
                        var subCategoryData = SiteDataManager.GetProductAndServices_SubCategoryContent(subcategoryIds);
                        if (subCategoryData != null && subCategoryData.Any())
                        {
                            XmlDocument contentSubCategoryXML = new XmlDocument();
                            var defaultContent = subCategoryData.FirstOrDefault();
                            try
                            {
                                contentSubCategoryXML.LoadXml(defaultContent.Html);
                                XmlNodeList xnSubCategoryList = contentXML.SelectNodes("/root");
                                subcategoryTitle = xnSubCategoryList[0]["title"].InnerXml;
                                subcategoryTagline = xnSubCategoryList[0]["tagline"].InnerXml;
                                if (subcategoryTagline.Length > 150)
                                {
                                    subcategoryTagline = subcategoryTagline.Substring(0, 150);
                                }

                                string xml2 = contentXML.SelectSingleNode("/root/imageSlider").InnerXml;
                                subcategoryImageSRC = Regex.Match(xml2, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                            }
                            catch (Exception ex)
                            {
                                Log.WriteError(ex);
                            }
                            subCategoryContentExist = true;
                        }
                    }

                    if (!subCategoryContentExist)
                    {
                        subcategoryTitle = title;
                        subcategoryTagline = tagline;
                        subcategoryImageSRC = imgSliderSRC;
                    }

                    string url = contentData.Quicklink;
                    counter++;

                    DTSource.Rows.Add(title, subtitle, imgSliderSRC, hrefId, url, content, teaser, caption, statement, tagline, subcategoryIds, catId, subcategoryTitle, subcategoryTagline, subcategoryImageSRC, counter);
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }
            DataView DVPSMMSort = DTSource.DefaultView;
            //DVPSMMSort.Sort = "title asc";
            sortedDT = DVPSMMSort.ToTable();
        }
        return sortedDT;
    }

    private DataTable GetCategoryImageData(long CategoryId)
    {
        DataTable DTSource = new DataTable();
        DataTable sortedDT = new DataTable();
        XmlDocument contentXML = new XmlDocument();
        int counter = 0;

        DTSource.Columns.Add("title");
        DTSource.Columns.Add("imageSRC");
        DTSource.Columns.Add("caption");
        DTSource.Columns.Add("hrefText");
        DTSource.Columns.Add("counter");

        var contents = SiteDataManager.GetProductAndServices();
        if (contents != null && contents.Count > 0)
        {
            foreach (Ektron.Cms.ContentData contentData in contents)
            {
                if (CategoryId == contentData.Id)
                {
                    try
                    {
                        contentXML.LoadXml(contentData.Html);
                        XmlNodeList xnList = contentXML.SelectNodes("/root");
                        string title = xnList[0]["title"].InnerXml;
                        string caption = xnList[0]["caption"].InnerXml;


                        string hrefText = contentData.Quicklink;

                        string imgSRC = "";
                        if (contentXML.SelectSingleNode("/root/imageSlider") != null)
                        {
                            string xml = contentXML.SelectSingleNode("/root/imageSlider").InnerXml;
                            imgSRC = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                        }
                        counter++;

                        DTSource.Rows.Add(title, imgSRC, caption, hrefText, counter);
                    }
                    catch (Exception ex)
                    {
                        Log.WriteError(ex);
                    }
                }
            }
            DataView DVPSMMSort = DTSource.DefaultView;
            DVPSMMSort.Sort = "title asc";
            sortedDT = DVPSMMSort.ToTable();
        }
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
                    string hreftext = contentData.Quicklink;
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

    //private void GetFindLocationLibraryImage()
    //{
    //    try
    //    {
    //        long findlocationImageID = ConfigHelper.GetValueLong("FindLocationImageID");
    //        var libraryManager = new LibraryManager();
    //        string imgUrl = string.Empty;
    //        var libItem = libraryManager.GetItem(findlocationImageID);
    //        if (libItem != null)
    //        {
    //            img.BackImageUrl = libItem.FileName;
    //            img.Height = 297;
    //        }
    //    }
    //    catch(Exception ex)
    //    {

    //    }
    //}
}