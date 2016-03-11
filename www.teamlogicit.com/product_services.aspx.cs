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
using System.Linq;

public partial class product_services : PageBase
{
    protected int rowNumber = 0;

    protected DataTable PSContent = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PSContent = GetPSContent();
            UxPSIndex.DataSource = PSContent;
            UxPSIndex.DataBind();

            DataTable DTCaseStudiesSliderSource = GetCaseStudiesSliderContent();
            UxCaseStudiesSlider.DataSource = DTCaseStudiesSliderSource;
            UxCaseStudiesSlider.DataBind();
        }
    }

    protected void UxPSIndex_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
    {
        List<long> SubCategoryIds = new List<long>();

        string tempSubCatIds = PSContent.Rows[rowNumber]["subcategoryIds"].ToString();

        long CatId = Convert.ToInt64(PSContent.Rows[rowNumber]["categoryId"]);

        SubCategoryIds = FromString(tempSubCatIds);

        //Get Inner Repeater
        Repeater UxLargerSlider = (Repeater)e.Item.FindControl("UxLargerSlider");

        Repeater UxThumbSlider = (Repeater)e.Item.FindControl("UxThumbSlider");

        DataTable SubCategoryDataIds = GetSubCategoryData(SubCategoryIds);
        if (SubCategoryDataIds.Rows.Count > 0)
        {
            UxLargerSlider.DataSource = SubCategoryDataIds;
            UxThumbSlider.DataSource = SubCategoryDataIds;

            UxLargerSlider.DataBind();
            UxThumbSlider.DataBind();
        }
        else
        {
            UxLargerSlider.DataSource = GetCategoryImageData(CatId);
            UxLargerSlider.DataBind();
            //UxThumbSlider.DataSource = GetPSContent();
        }
        rowNumber++;
    }

    private static List<Int64> FromString(string value)
    {
        return new List<Int64>(
           value
            .Split(new char[] { ',', '[', ']' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(s => Convert.ToInt64(s))
        );
    }

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
        DTSource.Columns.Add("tagline");
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
                        string tagline = xnList[0]["tagline"].InnerXml;
                        if (tagline.Length > 150)
                        {
                            tagline = tagline.Substring(0, 150);
                        }
                        string hrefText = "/product-services-subcategory/?sid=" + contentData.Id;                        
            

                        string xml = contentXML.SelectSingleNode("/root/image").InnerXml;
                        string imgSRC = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                        counter++;

                        DTSource.Rows.Add(title, imgSRC, tagline, hrefText,counter); 
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
        DTSource.Columns.Add("iconGrey");
        DTSource.Columns.Add("iconWhite");
        DTSource.Columns.Add("iconRed");
        DTSource.Columns.Add("hrefId");
        DTSource.Columns.Add("hrefText");
        DTSource.Columns.Add("content");
        DTSource.Columns.Add("teaser");
        DTSource.Columns.Add("quotesBy_Name");
        DTSource.Columns.Add("quotesBy_Organization");
        DTSource.Columns.Add("statement");
        DTSource.Columns.Add("tagline");
        DTSource.Columns.Add("subcategoryIds");
        DTSource.Columns.Add("categoryId");
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
                    string subtitle = xnList[0]["subtitle"].InnerXml;
                    string content = xnList[0]["content"].InnerXml;
                    string teaser = xnList[0]["teaser"].InnerXml;//content.Substring(0, 450).TrimEnd() + "..."; //xnList[0]["teaser"].InnerXml;
                    string tagline = xnList[0]["tagline"].InnerXml;
                    string quotesByName = xnList[0]["quotesByName"].InnerXml;
                    if (!String.IsNullOrWhiteSpace(quotesByName))
                    {
                        quotesByName += ", ";
                    }
                    string quotesByOrganization = xnList[0]["quotesByOrganization"].InnerXml;
                    string statement = xnList[0]["statement"].InnerXml;
                    long catId = contentData.Id;

                    XmlNodeList xnList2 = contentXML.SelectNodes("/root/ViewPort/ViewPortContent");
                    string subcategoryIds = "";
                    int tempcounter = 1;
                    if (xnList2.Count > 0)
                    {
                        foreach (XmlNode xn in xnList2)
                        {
                            if (xn.Attributes["datavalue_displayvalue"].Value != null)
                            {
                                string subcategoryId = xn.InnerXml;

                                if (tempcounter == 1)
                                {
                                    subcategoryIds += subcategoryId;
                                }
                                else if (tempcounter <= xnList2.Count)
                                {
                                    subcategoryIds += "," + subcategoryId;
                                }

                                tempcounter++;
                            }
                        }
                    }
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

                    string url = "/product-services-category/?sid=" + contentData.Id; //contentData.Quicklink;
                    counter++;

                    DTSource.Rows.Add(title, subtitle, imgSliderSRC, iconGrey, iconWhite, iconRed, hrefId, url, content, teaser, quotesByName, quotesByOrganization, statement, tagline, subcategoryIds, catId, counter);
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

    private DataTable GetCategoryImageData(long CategoryId)
    {
        DataTable DTSource = new DataTable();
        DataTable sortedDT = new DataTable();
        XmlDocument contentXML = new XmlDocument();
        int counter = 0;

        DTSource.Columns.Add("title");
        DTSource.Columns.Add("imageSRC");
        DTSource.Columns.Add("tagline");
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
                        string tagline = xnList[0]["tagline"].InnerXml;
                        if(tagline.Length > 150){
                            tagline = tagline.Substring(0, 150);
                        }

                        string hrefText = "/product-services-category/?sid=" + contentData.Id;
                        //string hrefText = contentData.Quicklink;

                        string imgSRC = "";
                        if (contentXML.SelectSingleNode("/root/imageSlider") != null)
                        {
                            string xml = contentXML.SelectSingleNode("/root/imageSlider").InnerXml;
                            imgSRC = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                        }
                        counter++;

                        DTSource.Rows.Add(title, imgSRC, tagline, hrefText, counter);
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