using System;
using System.Data;
using System.Xml;
using Ektron.Cms;
using Ektron.Cms.Framework.Content;
using System.Text.RegularExpressions;
using SignalGraphics.CMS;
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

    protected void Page_Load(object sender, EventArgs e)
    {
        cbFindLocationText.DefaultContentID = ConfigHelper.GetValueLong("PSFindLocationCId");
        cbFindLocationText.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbFindLocationText.Fill();

        if (!Page.IsPostBack)
        {
            PSContent = GetPSContent();
            UxPSIndex.DataSource = PSContent;
            UxPSIndex.DataBind();                        
        }

        if (FransDataManager.IsFranchiseSelected())
            findLocationNational.Visible = false;
    }    

    private static List<Int64> FromString(string value)
    {
        return new List<Int64>(
           value
            .Split(new char[] { ',', '[', ']' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(s => Convert.ToInt64(s))
        );
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
                    if(subcategoryIds.Any())
                    {
                      var subCategoryData =  SiteDataManager.GetProductAndServices_SubCategoryContent(subcategoryIds);
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

                    DTSource.Rows.Add(title, subtitle, imgSliderSRC, iconGrey, iconWhite, iconRed, hrefId, url, content,
                        teaser, quotesByName, quotesByOrganization, statement, tagline, subcategoryIds, catId, subcategoryTitle, subcategoryTagline, subcategoryImageSRC, counter);
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

                        string hrefText = contentData.Quicklink;

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
            //DVPSMMSort.Sort = "title asc";
            sortedDT = DVPSMMSort.ToTable();
        }
        return sortedDT;
    }
        
}