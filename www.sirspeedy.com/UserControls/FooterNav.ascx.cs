using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ektron.Cms;
using SirSpeedy.CMS;
using System.Text;
using Ektron.Cms.Instrumentation;
using System.Xml;
using System.Data;
using System.Text.RegularExpressions;
using Figleaf;
using Ektron.Cms.Organization;

public partial class UserControls_FooterNav : System.Web.UI.UserControl
{
    string _fransId = "0";

    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        _fransId = FransDataManager.IsFranchiseSelected() ? FransDataManager.GetFranchiseId() : "0";
        if (FransDataManager.IsFranchiseSelected())
        {
            cbHaveQuestion.DefaultContentID = ConfigHelper.GetValueLong("localFooterHaveQuestCId");
            cbHaveQuestion.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
            cbHaveQuestion.Fill();

            cbSubscribeNow.DefaultContentID = ConfigHelper.GetValueLong("localFooterSubscribeNowCId");
            cbSubscribeNow.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
            cbSubscribeNow.Fill();
           
            pnlLocal.Visible = true;
            pnlNational.Visible = false;
        }
        else
        {
            cbFindLocation.DefaultContentID = ConfigHelper.GetValueLong("nationalFooterFindLoctionCId");
            cbFindLocation.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
            cbFindLocation.Fill();

            cbFransOpport.DefaultContentID = ConfigHelper.GetValueLong("nationalFooterFransOpportCId");
            cbFransOpport.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
            cbFransOpport.Fill();
           
            pnlLocal.Visible = false;
            pnlNational.Visible = true;
        }

        if (!IsPostBack)
        {
            string phoneNumber = FransDataManager.GetContactNumber();
            ltrPhoneNUmber.Text = "<div class=\"cta-button-wrap purple\"><a href=\"tel:" + phoneNumber + "\" class=\"cta-button-text\"><span>CALL " + phoneNumber + "</span></a></div>";
            this.GetProductsAndServices();
            this.GetSecondaryNav();
            this.GetTwitterUrl();
        }
    }

    /// <summary>
    /// helper method to read product & services categories
    /// </summary>
    private void GetProductsAndServices()
    {

        var productServicesData = SiteDataManager.GetProductAndServices(); //SiteDataManager.GetProductServicesTaxList();
        if (productServicesData != null && productServicesData.Count > 0)
        {
            //long mainProductAndServicesId = ConfigHelper.GetValueLong("ProductAndServicesTaxonomyId");
            //var taxItem = TaxonomyHelper.GetItem(mainProductAndServicesId);
            //ltrProductServicesTitle.Text = taxItem.Name;
            lvfooterProductsServices.DataSource = GetPSNationalContent();//productServicesData;
            lvfooterProductsServices.DataBind();
        }       
    }

    /// <summary>
    /// helper method to read product & services categories title and link to landing page
    /// </summary>
    private DataTable GetPSNationalContent()
    {
        string cacheKey = String.Format("Sirspeedy:UserControls_FooterNav:GetPSNationalContent:FranchiseId={0}",
            _fransId);

        DataTable sortedDT = CacheBase.Get<DataTable>(cacheKey);

        if (sortedDT == default(DataTable) || sortedDT.Rows.Count == 0)
        {
            DataTable DTSource = new DataTable();            
            XmlDocument contentXML = new XmlDocument();
            int counter = 0;

            DTSource.Columns.Add("title");
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
                        counter++;

                        DTSource.Rows.Add(title, url, counter);
                    }
                    catch (Exception ex)
                    {
                        Log.WriteError(ex);
                    }
                }
                DataView DVPSMMSort = DTSource.DefaultView;
                DVPSMMSort.Sort = "title asc";
                sortedDT = DVPSMMSort.ToTable();
                CacheBase.Put(cacheKey, sortedDT, CacheDuration.For24Hr);
            }
        }
        return sortedDT;
    }

    /// <summary>
    ///to read all the secondary menu items
    /// </summary>
    private void GetSecondaryNav()
    {
        long secondaryMenuId = 0;
        if (FransDataManager.IsFranchiseSelected())
            secondaryMenuId = ConfigHelper.GetValueLong("LocalSecondaryNavId");
        else
            secondaryMenuId = ConfigHelper.GetValueLong("NationalSecondaryNavId");

        var secondaryMenuData = GetMenuData(secondaryMenuId);
        if (secondaryMenuData != null && secondaryMenuData.Items.Count > 0)
        {
            lvFooterSecondaryNav.DataSource = secondaryMenuData.Items;
            lvFooterSecondaryNav.DataBind();
        }
    }

    /// <summary>
    /// this method is used to get twitter url
    /// </summary>
    private void GetTwitterUrl()
    {
        if(FransDataManager.IsFranchiseSelected())
        {
            string fransTwitterUrl = FransDataManager.GetFransTwitterUrl();
            if (!string.IsNullOrEmpty(fransTwitterUrl))
            {
                twitterWidget.HRef = fransTwitterUrl;
            }
        }        
    }

    private MenuData GetMenuData(long secondaryNavId)
    {
        MenuData data = null;
        string cacheKey = String.Format("Sirspeedy:UserControls_FooterNav:GetMenuData:FranchiseId={0}{1}",
            _fransId, secondaryNavId);

        data = CacheBase.Get<MenuData>(cacheKey);
        if (data == null && secondaryNavId > 0)
        {        
            data = MenuHelper.GetMenuTree(secondaryNavId);
            CacheBase.Put(cacheKey, data, CacheDuration.For24Hr);
        }
        return data;
    }
    
    public string FormatLink(string url, string title, string className)
    {
        if ((!string.IsNullOrEmpty(url)) && (!url.StartsWith("/")))
            url = "/" + url;

        string formattedLink = "<a href=\"" + url + "\" class=\"lvl-2-title\">" + title + "</a>";
        if ((!string.IsNullOrEmpty(className)) && (className.Trim().ToLower().Equals("inactivelink")))
            formattedLink = title;
        return formattedLink;
    }
}