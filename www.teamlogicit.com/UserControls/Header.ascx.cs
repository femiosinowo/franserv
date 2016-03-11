using System;
using System.Linq;
using System.Xml.XPath;
using System.Xml.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

using TeamLogic.CMS;
using Ektron.Cms.Instrumentation;

public partial class UserControls_Header : System.Web.UI.UserControl
{
    private FransData centerData;
    protected void Page_Init(object sender, EventArgs e)
    {
        cbLogo.DefaultContentID = ConfigHelper.GetValueLong("SiteLogoContent");
        cbLogo.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbLogo.Fill();

        cbFindLocation.DefaultContentID = ConfigHelper.GetValueLong("NationalSiteHeaderFindLocation");
        cbFindLocation.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbFindLocation.Fill();

        //cbFransOpport.DefaultContentID = ConfigHelper.GetValueLong("FranchiseOpportunityContentId");
        //cbFransOpport.CacheInterval = ConfigHelper.GetValueLong("smallCacheInterval");
        //cbFransOpport.Fill();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {



            if (FransDataManager.IsFranchiseSelected())
            {
                centerData = FransDataManager.GetFransData();
                if (centerData != null)
                {
                    ltrContactNumber.Text = "<a href=\"tel:" + centerData.PhoneNumber + "\"><img alt=\"icon\" src=\"/images/location_nav_icon_local.png\" /><span>Let's Connect<span>" + centerData.PhoneNumber + "</span></span></a>";
                    if (!String.IsNullOrWhiteSpace(centerData.ClientLoginUrl))
                    {
                        ClientLoginURLHyperLink1.NavigateUrl = centerData.ClientLoginUrl;
                        ClientLoginURLHyperLink1.Visible = true;
                    }
                }
                ltrContactNumber.Visible = true;
                long localHeaderNavId = ConfigHelper.GetValueLong("LocalHeaderNavId");
                long localMainNavId = ConfigHelper.GetValueLong("MainNavLocalId");
                this.LoadHeaderMenu(localHeaderNavId);
                this.LoadMainMenu(localMainNavId);

            }
            else if (!string.IsNullOrEmpty(Request.QueryString["centerid"]))
            {
                string centerid = Request.QueryString["centerid"].ToString();
                centerData = FransDataManager.GetFransData(centerid);
                if (centerData != null)
                {
                  //  Response.Write(centerData.CenterName);
                    ltrContactNumber.Text = "<a href=\"tel:" + centerData.PhoneNumber + "\"><img alt=\"icon\" src=\"/images/location_nav_icon_local.png\" /><span>Let's Connect<span>" + centerData.PhoneNumber + "</span></span></a>";
                    if (!String.IsNullOrWhiteSpace(centerData.ClientLoginUrl))
                    {
                        ClientLoginURLHyperLink1.NavigateUrl = centerData.ClientLoginUrl;
                        ClientLoginURLHyperLink1.Visible = true;
                    }
                }
                ltrContactNumber.Visible = true;
                long localHeaderNavId = ConfigHelper.GetValueLong("LocalHeaderNavId");
                long localMainNavId = ConfigHelper.GetValueLong("MainNavLocalId");
                this.LoadHeaderMenu2(localHeaderNavId, centerid);
                this.LoadMainMenu2(localMainNavId, centerid);
            }
            else
            {
                cbFindLocation.Visible = true;
                long nationalHeaderNavId = ConfigHelper.GetValueLong("NationalHeaderNavId");
                long nationalMainNavId = ConfigHelper.GetValueLong("MainNavNationalId");
                this.LoadHeaderMenu(nationalHeaderNavId);
                this.LoadMainMenu(nationalMainNavId);
            }
        }
    }


 
    private void LoadHeaderMenu(long menuId)
    {
        try
        {

            headerMenu.DefaultMenuID = menuId;
            headerMenu.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
            headerMenu.Fill();

            if (headerMenu.XmlDoc != null)
            {
                string menuXml = headerMenu.XmlDoc.InnerXml;
                XDocument xDoc = XDocument.Parse(menuXml);
                var menuItems = from item in xDoc.XPathSelectElement("MenuDataResult/Item").Descendants("Item")
                                select new
                                {
                                    Title = item.XPathSelectElement("ItemTitle").Value,
                                    Description = item.XPathSelectElement("ItemType").Value == "ExternalLink" ? (item.XPathSelectElement("ItemDescription") != null ? item.XPathSelectElement("ItemDescription").Value : "") : (item.XPathSelectElement("Menu/Description") != null ? item.XPathSelectElement("Menu/Description").Value : ""),
                                    Link = item.XPathSelectElement("ItemType").Value == "ExternalLink" ? FormatBlogURL(item.XPathSelectElement("ItemLink").Value) : item.XPathSelectElement("Menu/Link").Value,
                                    Target = item.XPathSelectElement("ItemType").Value == "ExternalLink" ? "_blank" : "_self"
                                };
                lvHeaderMenuItems.DataSource = menuItems;
                lvHeaderMenuItems.DataBind();
            }

            var socialIconsData = SiteDataManager.GetSocialMediaLinks();
            if (socialIconsData != null)
            {
                if (!string.IsNullOrEmpty(socialIconsData.FaceBookUrl))
                    fbLink.HRef = fbHeadLink.HRef = socialIconsData.FaceBookUrl;
                if (!string.IsNullOrEmpty(socialIconsData.TwitterUrl))
                    twitterLink.HRef = twHeadLink.HRef = socialIconsData.TwitterUrl;
                if (!string.IsNullOrEmpty(socialIconsData.LinkedInUrl))
                    linkedInLink.HRef = inHeadLink.HRef = socialIconsData.LinkedInUrl;
            }
        }
        catch(Exception ex)
        {
            Log.WriteError(ex);
        }
    }

    private void LoadMainMenu(long menuId)
    {
        try
        {
            mainNav.DefaultMenuID = menuId;
            mainNav.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
            mainNav.Fill();

            if (mainNav.XmlDoc != null)
            {
                string menuXml = mainNav.XmlDoc.InnerXml;
                XDocument xDoc = XDocument.Parse(menuXml);
                var menuItems = from item in xDoc.Descendants("Menu")
                                where item.Element("Description").Value.Contains("hidden") == false
                                select new
                                {
                                    Title = item.Element("Title").Value,
                                    Description = item.Element("Description") != null ? item.Element("Description").Value : "",
                                    Link = item.Element("Link").Value
                                };
                if (Request.Browser.IsMobileDevice)
                {
                    if (centerData != null)
                    {
                        if (!String.IsNullOrWhiteSpace(centerData.ClientLoginUrl))
                        {
                            menuItems =
                                menuItems.Concat(new[]
                                {
                                    new
                                    {
                                        Title = "Client Login",
                                        Description = "Client Login Link",
                                        Link = centerData.ClientLoginUrl
                                    }
                                });
                        }
                    }
                }
                lvMainMenuItems.DataSource = menuItems;
                lvMainMenuItems.DataBind();
            }
        }
        catch(Exception ex)
        {
            Log.WriteError(ex);
        }
    }

    private string FormatBlogURL(string menuLink)
    {
        string link = menuLink;
        if (link.ToLower().Equals("http://www.itinflections.com/"))
        {
            var fransThirdPartyData = FransDataManager.GetFransThirdPartyData();
            if (fransThirdPartyData != null &&
                fransThirdPartyData.SocialMediaData != null &&
                !string.IsNullOrEmpty(fransThirdPartyData.SocialMediaData.ITInflectionsUrl))
            {
                link = fransThirdPartyData.SocialMediaData.ITInflectionsUrl;
            }
        }
        return link;
    }

    protected void lvMainMenuUtems_LayoutCreated(object sender, EventArgs e)
    {
        string centerId = FransDataManager.GetFranchiseId();
        HyperLink rcLink = (HyperLink)lvMainMenuItems.FindControl("RequestConsultationLink1");
        if (rcLink != null)
            rcLink.NavigateUrl = String.Format("http://{0}/requestconsultationmobile.aspx?centerid=" + centerId, Request.ServerVariables["HTTP_HOST"]);
    }

    private void LoadMainMenu2(long menuId, string centerid)
    {
        try
        {
            mainNav.DefaultMenuID = menuId;
            mainNav.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
            mainNav.Fill();

            if (mainNav.XmlDoc != null)
            {
                string menuXml = mainNav.XmlDoc.InnerXml;
                XDocument xDoc = XDocument.Parse(menuXml);
                var menuItems = from item in xDoc.Descendants("Menu")
                                where item.Element("Description").Value.Contains("hidden") == false
                                select new
                                {
                                    Title = item.Element("Title").Value,
                                    Description = item.Element("Description") != null ? item.Element("Description").Value : "",
                                    Link = centerid + item.Element("Link").Value
                                };
                if (Request.Browser.IsMobileDevice)
                {
                    if (centerData != null)
                    {
                        if (!String.IsNullOrWhiteSpace(centerData.ClientLoginUrl))
                        {
                            menuItems =
                                menuItems.Concat(new[]
                                {
                                    new
                                    {
                                        Title = "Client Login",
                                        Description = "Client Login Link",
                                        Link = centerData.ClientLoginUrl
                                    }
                                });
                        }
                    }
                }
                lvMainMenuItems.DataSource = menuItems;
                lvMainMenuItems.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.WriteError(ex);
        }
    }

    private void LoadHeaderMenu2(long menuId, string centerid)
    {
        try
        {

            headerMenu.DefaultMenuID = menuId;
            headerMenu.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
            headerMenu.Fill();

            if (headerMenu.XmlDoc != null)
            {
                string menuXml = headerMenu.XmlDoc.InnerXml;
                XDocument xDoc = XDocument.Parse(menuXml);
                var menuItems = from item in xDoc.XPathSelectElement("MenuDataResult/Item").Descendants("Item")
                                select new
                                {
                                    Title = item.XPathSelectElement("ItemTitle").Value,
                                    Description = item.XPathSelectElement("ItemType").Value == "ExternalLink" ? (item.XPathSelectElement("ItemDescription") != null ? item.XPathSelectElement("ItemDescription").Value : "") : (item.XPathSelectElement("Menu/Description") != null ? item.XPathSelectElement("Menu/Description").Value : ""),
                                    Link = centerid + item.XPathSelectElement("ItemType").Value == "ExternalLink" ? FormatBlogURL(item.XPathSelectElement("ItemLink").Value) : item.XPathSelectElement("Menu/Link").Value,
                                    Target = item.XPathSelectElement("ItemType").Value == "ExternalLink" ? "_blank" : "_self"
                                };
                lvHeaderMenuItems.DataSource = menuItems;
                lvHeaderMenuItems.DataBind();
            }

            var socialIconsData = SiteDataManager.GetSocialMediaLinks();
            if (socialIconsData != null)
            {
                if (!string.IsNullOrEmpty(socialIconsData.FaceBookUrl))
                    fbLink.HRef = fbHeadLink.HRef = socialIconsData.FaceBookUrl;
                if (!string.IsNullOrEmpty(socialIconsData.TwitterUrl))
                    twitterLink.HRef = twHeadLink.HRef = socialIconsData.TwitterUrl;
                if (!string.IsNullOrEmpty(socialIconsData.LinkedInUrl))
                    linkedInLink.HRef = inHeadLink.HRef = socialIconsData.LinkedInUrl;
            }
        }
        catch (Exception ex)
        {
            Log.WriteError(ex);
        }
    }

}