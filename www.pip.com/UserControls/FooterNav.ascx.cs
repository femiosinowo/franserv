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
            ltrPhoneNUmber.Text = "<div class=\"cta-button-wrap white-btn\"><a href=\"tel:" + phoneNumber + "\" class=\"cta-button-text white-btn\"><span>CALL " + phoneNumber + "</span></a></div>";

            this.GetProductsAndServices();
            this.GetSecondaryNav();
            this.GetSocialIcons();
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
        DataTable DTSource = new DataTable();
        DataTable sortedDT = new DataTable();
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

        string cacheKey = String.Format("Pip:UserControls_FooterNav:GetSecondaryNav:FranchiseId={0}:MenuId={1}",
            _fransId, secondaryMenuId);

        string menuHtml = CacheBase.Get<string>(cacheKey);
        if (string.IsNullOrEmpty(menuHtml))
        {
            var secondaryMenuData = MenuHelper.GetMenuTree(secondaryMenuId);
            if (secondaryMenuData != null && secondaryMenuData.Items.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                var aboutUsNav = secondaryMenuData.Items.Where(x => x.Description.ToLower().Contains("aboutus")).FirstOrDefault();
                if (aboutUsNav != null && aboutUsNav.Items != null)
                {
                    sb.Append("<div class=\"top_footer_section_2_col\">");
                    sb.Append("<h4>" + aboutUsNav.Text + "</h4>");
                    sb.Append("<ul>");
                    foreach (var nav in aboutUsNav.Items)
                        sb.Append("<li><a href=\"/" + nav.Href + "\">" + nav.Text + "</a></li>");
                    sb.Append("</ul>");
                    sb.Append("</div>");
                }

                sb.Append("<div class=\"top_footer_section_2_col\">");
                foreach (var subNav in secondaryMenuData.Items)
                {
                    if (subNav.Description.ToLower().Contains("aboutus") == false)
                    {
                        sb.Append("<h4>" + subNav.Text + "</h4>");
                        sb.Append("<ul>");
                        foreach (var nav in subNav.Items)
                            sb.Append("<li><a href=\"/" + nav.Href + "\">" + nav.Text + "</a></li>");
                        sb.Append("</ul>");
                    }
                }
                sb.Append("</div>");
                CacheBase.Put(cacheKey, sb.ToString(), CacheDuration.For24Hr);
                ltrSubNav.Text = sb.ToString();
            }
        }        

        ltrSubNav.Text = menuHtml;
    }

    /// <summary>
    /// This method is used to get social medai icons
    /// </summary>
    private void GetSocialIcons()
    {
        var socialIconsData = SiteDataManager.GetSocialMediaLinks();
        if (socialIconsData != null)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("<ul class=\"social_media\">");
                if ((!string.IsNullOrEmpty(socialIconsData.FaceBookUrl)) && (!string.IsNullOrEmpty(socialIconsData.FaceBookImgPath)))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.FaceBookUrl + "\"><img alt=\"Facebook\" src=\"" + socialIconsData.FaceBookImgPath + "\"/></a></li>");
                if ((!string.IsNullOrEmpty(socialIconsData.TwitterUrl)) && (!string.IsNullOrEmpty(socialIconsData.TwitterImgPath)))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.TwitterUrl + "\"><img alt=\"Twitter\" src=\"" + socialIconsData.TwitterImgPath + "\"/></a></li>");
                if ((!string.IsNullOrEmpty(socialIconsData.GooglePlusUrl)) && (!string.IsNullOrEmpty(socialIconsData.GooglePlusImgPath)))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.GooglePlusUrl + "\"><img alt=\"Google Plus\" src=\"" + socialIconsData.GooglePlusImgPath + "\"/></a></li>");
                if ((!string.IsNullOrEmpty(socialIconsData.LinkedInUrl)) && (!string.IsNullOrEmpty(socialIconsData.LinkedInImgPath)))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.LinkedInUrl + "\"><img alt=\"LinkedIn\" src=\"" + socialIconsData.LinkedInImgPath + "\"/></a></li>");
                if ((!string.IsNullOrEmpty(socialIconsData.StumbleUponUrl)) && (!string.IsNullOrEmpty(socialIconsData.StumbleUponImgPath)))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.StumbleUponUrl + "\"><img alt=\"Stumble Upon\" src=\"" + socialIconsData.StumbleUponImgPath + "\"/></a></li>");
                if ((!string.IsNullOrEmpty(socialIconsData.FlickrUrl)) && (!string.IsNullOrEmpty(socialIconsData.FlickrImgPath)))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.FlickrUrl + "\"><img alt=\"Flickr\" src=\"" + socialIconsData.FlickrImgPath + "\"/></a></li>");
                if ((!string.IsNullOrEmpty(socialIconsData.YouTubeUrl)) && (!string.IsNullOrEmpty(socialIconsData.YouTubeImgPath)))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.YouTubeUrl + "\"><img alt=\"You Tube\" src=\"" + socialIconsData.YouTubeImgPath + "\"/></a></li>");
                if ((!string.IsNullOrEmpty(socialIconsData.MarketingTangoUrl)) && (!string.IsNullOrEmpty(socialIconsData.MarketingTangoImgPath)))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.MarketingTangoUrl + "\"><img alt=\"Marketing Tango\" src=\"" + socialIconsData.MarketingTangoImgPath + "\"/></a></li>");
                sb.Append("</ul>");
            }
            catch(Exception ex)
            {
                Log.WriteError(ex);
            }
            ltrSocialIcons.Text = sb.ToString();
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

}