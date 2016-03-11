using System;
using Ektron.Cms.Instrumentation;
using System.Data;
using System.Xml;
using System.Text.RegularExpressions;

using TeamLogic.CMS;

public partial class it_solutions_index : PageBase
{
    public string LetsConnectBackgroundImage = "/images/lets_connect_bg.jpg";

    protected void Page_Init(object sender, EventArgs e)
    {
        cbHowWeWork.DefaultContentID = ConfigHelper.GetValueLong("ITSolutionsHowWeWorkCId");
        cbHowWeWork.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbHowWeWork.Fill();        

        if(FransDataManager.IsFranchiseSelected())
        {
            cbLetsConnect.DefaultContentID = ConfigHelper.GetValueLong("ITSolutionsLetsConnectCId");
            cbLetsConnect.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
            cbLetsConnect.Fill();
        }
        else
        {
            cbLetsConnect.DefaultContentID = ConfigHelper.GetValueLong("ITSolutionsLetsConnectNationalCId");
            cbLetsConnect.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
            cbLetsConnect.Fill();
        }

        if (cbLetsConnect.EkItem != null && !string.IsNullOrEmpty(cbLetsConnect.EkItem.Image) && cbLetsConnect.EkItem.Image.ToLower().IndexOf("workarea") <= -1)
            LetsConnectBackgroundImage = cbLetsConnect.EkItem.Image;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dtSolutionsContent = this.GetSolutionsContent();
            lvSolutionsContent.DataSource = dtSolutionsContent;
            lvSolutionsContent.DataBind();

            DataTable dtCompleteSolutionsContent = this.GetCompleteSolutionsContent();
            lvCompleteSolutionsContent.DataSource = dtCompleteSolutionsContent;
            lvCompleteSolutionsContent.DataBind();            
        }

        lets_connect.Attributes.Add("data-image", LetsConnectBackgroundImage);
        lets_connect.Attributes.Add("data-image-mobile", LetsConnectBackgroundImage);
    }   

    private DataTable GetSolutionsContent()
    {
        DataTable DTSource = new DataTable();
        DataTable sortedDT = new DataTable();
        XmlDocument contentXML = new XmlDocument();
        int counter = 1;
        DTSource.Columns.Add("title");
        DTSource.Columns.Add("desc");        
        DTSource.Columns.Add("imgSRC");
        DTSource.Columns.Add("hreftext");
        DTSource.Columns.Add("cssClass");
        DTSource.Columns.Add("startHTML");
        DTSource.Columns.Add("endHTML");

        var contents = SiteDataManager.GetSolutionsContent();
        if (contents != null && contents.Count > 0)
        {
            foreach (Ektron.Cms.ContentData contentData in contents)
            {
                try
                {
                    string startHTML = "<div class=\"container_24\"><div class=\"row grid_24 clearfix\">";
                    string endHTML = "";
                    string cssClass = "suffix";
                    if (counter % 2 == 0)
                    {
                        startHTML = "";
                        endHTML = "</div><div class=\"clear\"></div></div><div class=\"divider full_width clearfix\"></div>";
                        cssClass = "prefix";
                    }                    
                    contentXML.LoadXml(contentData.Html);
                    string contentId = contentData.Id.ToString();
                    string hreftext = contentData.Quicklink;                    
                    XmlNodeList xnList = contentXML.SelectNodes("/root");
                    string title = xnList[0]["title"].InnerXml;
                    string desc = xnList[0]["description"].InnerXml;
                    string xml = contentXML.SelectSingleNode("/root/OurSolutionImage").InnerXml;
                    string imgSRC = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                    counter++;
                    string url = hreftext;                                                        
                    DTSource.Rows.Add(title, desc, imgSRC, hreftext,cssClass, startHTML, endHTML);
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }           
        }
        return DTSource;
    }
    
    private DataTable GetCompleteSolutionsContent()
    {
        DataTable DTSource = new DataTable();
        DataTable sortedDT = new DataTable();
        XmlDocument contentXML = new XmlDocument();
        int counter = 1;
        DTSource.Columns.Add("title");
        DTSource.Columns.Add("desc");
        DTSource.Columns.Add("imgSRC");
        DTSource.Columns.Add("hreftext");
        DTSource.Columns.Add("cssClass");
        DTSource.Columns.Add("startHTML");
        DTSource.Columns.Add("endHTML");

        var contents = SiteDataManager.GetCompleteSolutionsContent();
        if (contents != null && contents.Count > 0)
        {
            foreach (Ektron.Cms.ContentData contentData in contents)
            {
                try
                {
                    string startHTML = "";
                    string endHTML = "";
                    string cssClass = "";
                    if (counter % 4 == 0)
                    {                     
                        endHTML = "</div>";
                        cssClass = "omega";
                    }

                    if (counter % 4 == 1)
                    {
                        startHTML = "<div class=\"complete_grid row_0" + counter + "\">";                      
                    }

                    contentXML.LoadXml(contentData.Html);
                    string contentId = contentData.Id.ToString();
                    string hreftext = contentData.Quicklink;
                    XmlNodeList xnList = contentXML.SelectNodes("/root");
                    string title = xnList[0]["title"].InnerXml;
                    string desc = xnList[0]["description"].InnerXml;
                    string xml = contentXML.SelectSingleNode("/root/CompleteSolutionImage").InnerXml;
                    string imgSRC = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                    counter++;
                    string url = hreftext;
                    DTSource.Rows.Add(title, desc, imgSRC, hreftext, cssClass, startHTML, endHTML);
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }
        }
        return DTSource;
    }
}