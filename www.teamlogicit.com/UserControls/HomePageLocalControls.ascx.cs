using Ektron.Cms.Instrumentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;
using TeamLogic.CMS;

public partial class UserControls_HomePageLocalControls : System.Web.UI.UserControl
{
    public string WhyWorkBackgroundImage = "/images/why_work_bg.jpg";
    public string ITInflectionBackgroundImage = "/images/briefs_whitepapers_bg.jpg";
    public string LetsConnectBackgroundImage = "/images/lets_connect_bg.jpg";

    protected void Page_Init(object sender, EventArgs e)
    {
        cbITInflectionImg.DefaultContentID = ConfigHelper.GetValueLong("HomePageITInflectionsImgCId");
        cbITInflectionImg.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbITInflectionImg.Fill();

        cbLetsConnect.DefaultContentID = ConfigHelper.GetValueLong("HomePageLocalLetsConnectCId");
        cbLetsConnect.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbLetsConnect.Fill();

        if (cbITInflectionImg.EkItem != null && !string.IsNullOrEmpty(cbITInflectionImg.EkItem.Image) && cbITInflectionImg.EkItem.Image.ToLower().IndexOf("workarea") <= -1)
            ITInflectionBackgroundImage = cbITInflectionImg.EkItem.Image;

        if (cbLetsConnect.EkItem != null && !string.IsNullOrEmpty(cbLetsConnect.EkItem.Image) && cbLetsConnect.EkItem.Image.ToLower().IndexOf("workarea") <= -1)
            LetsConnectBackgroundImage = cbLetsConnect.EkItem.Image;
    }


    /// <summary>
    /// page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.GetSocialMediaIcons();
            this.GetFranchiseDetails();
            this.GetWorkWithUsContent();
            this.GetITSourceContent();
            this.GetRSSFeed();
        }

        if (FransDataManager.IsFranchiseSelected())
        {
            var fransData = FransDataManager.GetFransData();
            if (fransData != null)
            {
                ltrPhoneNumber2.Text = "<a id=\"local-phone\" href=\"tel:'" + fransData.PhoneNumber + "'\">" + fransData.PhoneNumber + "</a>";
            }

            var fransThirdPartyData = FransDataManager.GetFransThirdPartyData();
            if (fransThirdPartyData != null &&
                fransThirdPartyData.SocialMediaData != null &&
                !string.IsNullOrEmpty(fransThirdPartyData.SocialMediaData.ITInflectionsUrl))
            {
                blogsLink.HRef = fransThirdPartyData.SocialMediaData.ITInflectionsUrl;
            }
        }

        why_work.Attributes.Add("data-image", "/" + WhyWorkBackgroundImage);
        why_work.Attributes.Add("data-image-mobile", "/" + WhyWorkBackgroundImage);
        briefs_whitepapers.Attributes.Add("data-image", ITInflectionBackgroundImage);
        briefs_whitepapers.Attributes.Add("data-image-mobile", ITInflectionBackgroundImage);
        lets_connect.Attributes.Add("data-image", LetsConnectBackgroundImage);
        lets_connect.Attributes.Add("data-image-mobile", LetsConnectBackgroundImage);
    }


    private void GetSocialMediaIcons()
    {
        var socialIconsData = SiteDataManager.GetSocialMediaLinks();
        if (socialIconsData != null)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("<ul>");
                if (!string.IsNullOrEmpty(socialIconsData.FaceBookImgPath) && !string.IsNullOrEmpty(socialIconsData.FaceBookUrl))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.FaceBookUrl + "\"><img alt=\"Facebook\" src=\"/images/social-icons/green_facebook_icon.png\" width=\"32\" height=\"32\" /></a></li>");
                if (!string.IsNullOrEmpty(socialIconsData.TwitterImgPath) && !string.IsNullOrEmpty(socialIconsData.TwitterUrl))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.TwitterUrl + "\"><img alt=\"Twitter\" src=\"/images/social-icons/green_twitter_icon.png\" width=\"32\" height=\"32\" /></a></li>");
                if (!string.IsNullOrEmpty(socialIconsData.GooglePlusImgPath) && !string.IsNullOrEmpty(socialIconsData.GooglePlusUrl))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.GooglePlusUrl + "\"><img alt=\"Google Plus\" src=\"/images/social-icons/green_gplus_icon.png\" width=\"32\" height=\"32\" /></a></li>");
                if (!string.IsNullOrEmpty(socialIconsData.LinkedInImgPath) && !string.IsNullOrEmpty(socialIconsData.LinkedInUrl))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.LinkedInUrl + "\"><img alt=\"LinkedIn\" src=\"/images/social-icons/green_linkedin_icon.png\" width=\"32\" height=\"32\" /></a></li>");
                if (!string.IsNullOrEmpty(socialIconsData.StumbleUponImgPath) && !string.IsNullOrEmpty(socialIconsData.StumbleUponUrl))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.StumbleUponUrl + "\"><img alt=\"Stumble Upon\" src=\"/images/social-icons/green_stumbleupon_icon.png\" width=\"32\" height=\"32\" /></a></li>");
                if (!string.IsNullOrEmpty(socialIconsData.FlickrImgPath) && !string.IsNullOrEmpty(socialIconsData.FlickrUrl))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.FlickrUrl + "\"><img alt=\"Flickr\" src=\"/images/social-icons/green_flickr_icon.png\" width=\"32\" height=\"32\" /></a></li>");
                if (!string.IsNullOrEmpty(socialIconsData.YouTubeImgPath) && !string.IsNullOrEmpty(socialIconsData.YouTubeUrl))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.YouTubeUrl + "\"><img alt=\"You Tube\" src=\"/images/social-icons/green_youtube_icon.png\" width=\"32\" height=\"32\" /></a></li>");
                if (!string.IsNullOrEmpty(socialIconsData.ITInflectionsImgPath) && !string.IsNullOrEmpty(socialIconsData.ITInflectionsUrl))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.ITInflectionsUrl + "\"><img alt=\"IT Inflections\" src=\"/images/social-icons/green_it_icon.png\" width=\"32\" height=\"32\" /></a></li>");
                sb.Append("</ul>");
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
            ltrSocialLinks.Text = sb.ToString();
        }
    }


    /// <summary>
    /// Some location lat & long are strong headed - so I need to hard code them :)
    /// </summary>
    private Dictionary<string, string> LatitudeLong(string FransId)
    {

        Dictionary<string, string> latlong = new Dictionary<string, string>();
        switch (FransId)
        {
            case "bentonvillear401":
                latlong.Add("Latitude", "36.41313226755679");
                latlong.Add("Longitude", "-94.22008037567139");
                break;
        }

        return latlong;

    }
    /// <summary>
    /// Gets the Franchise Data
    /// </summary>
    private void GetFranchiseDetails()
    {
        var fd = FransDataManager.GetFransData();
        StringBuilder contactinfo = new StringBuilder();

        if (fd != null)
        {
            Dictionary<string, string> latlong = LatitudeLong(fd.FransId);
            if (latlong.Count > 0)
            {
                hiddenCenterLat.Value = latlong["Latitude"];
                hiddenCenterLong.Value = latlong["Longitude"];
            }
            else
            {
                hiddenCenterLat.Value = fd.Latitude;
                hiddenCenterLong.Value = fd.Longitude;
            }
             
            var whyWeAreDiff = FransDataManager.GetWhyWeAreDiffContent(fd.FransId);
            if (whyWeAreDiff != null)
            {                
                ltrDecription.Text = "<p>" + whyWeAreDiff.ContentTagLine + "</p>";
            }

            contactinfo.Append("<li class=\"address\">");
            contactinfo.Append(fd.Address1);
            if (!string.IsNullOrEmpty(fd.Address2))
            {
                contactinfo.Append(", ");
                contactinfo.Append(fd.Address2);
            }
            contactinfo.Append(", ");
            contactinfo.Append(fd.City + ",  " + fd.State + " " + fd.Zipcode);
            contactinfo.Append("</li>");
            litLocAddress.Text = contactinfo.ToString();

            ltrPhoneNumber.Text = "<li class=\"telephone\"><a href=\"tel:+" + fd.PhoneNumber + "\">" + fd.PhoneNumber + "</a></li>";
            ltrEmailAddress.Text = "<li class=\"email\"><a href='mailto:" + fd.Email + "'>" + fd.Email + "</a></li>";
        }
    }
    
    private void GetWorkWithUsContent()
    {
        long itSourcesCId = ConfigHelper.GetValueLong("HomePageWhyWorkWithUsCId");
        var cData = ContentHelper.GetContentById(itSourcesCId);
        if (cData != null && cData.Html != string.Empty)
        {
            try
            {
                if (!string.IsNullOrEmpty(cData.Image) && cData.Image.ToLower().IndexOf("workarea") <= -1)
                    WhyWorkBackgroundImage = cData.Image;

                StringBuilder sb = new StringBuilder();
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(cData.Html);

                sb.Append("<div class=\"why_work_content caption\">");
                if (xDoc.SelectSingleNode("/root/title") != null)
                    sb.Append(xDoc.SelectSingleNode("/root/title").InnerXml);
                sb.Append("<div class=\"clear_text\"></div>");
                if (xDoc.SelectSingleNode("/root/description") != null)
                    sb.Append("<p>" + xDoc.SelectSingleNode("/root/description").InnerXml + "</p>");

                string link = xDoc.SelectSingleNode("/root/link/a") != null ? xDoc.SelectSingleNode("/root/link/a").Attributes["href"].Value : "#";
                sb.Append(" <a href=\"" + link + "\">Learn More</a>");
                sb.Append("</div>");
                if (xDoc.SelectSingleNode("/root/videoLink") != null)
                    sb.Append("<div class=\"why_work_video\"><iframe src=\"" + xDoc.SelectSingleNode("/root/videoLink").InnerXml + "\" frameborder=\"0\" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe></div>");

                ltrWorkWithUs.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
        }
    }

    private void GetITSourceContent()
    {
        long itSourcesCId = ConfigHelper.GetValueLong("HomePagelocalOurServicesCId");
        var cData = ContentHelper.GetContentById(itSourcesCId);
        if (cData != null && cData.Html != string.Empty)
        {
            try
            {
                XDocument xDoc = XDocument.Parse(cData.Html);
                ltrSectionTitle.Text = xDoc.XPathSelectElement("/root/sectionTitle").Value;
                var xelements = xDoc.XPathSelectElements("/root/Content");
                if (xelements != null && xelements.Any())
                {
                    var itSolutionItems = from obj in xelements
                                          select new
                                          {
                                              Title = obj.XPathSelectElement("title") != null ? obj.XPathSelectElement("title").Value : string.Empty,
                                              SubTitle = obj.XPathSelectElement("description") != null ? obj.XPathSelectElement("description").ToString() : string.Empty,
                                              ImagePath = obj.XPathSelectElement("image/img") != null ? obj.XPathSelectElement("image/img").Attribute("src").Value : string.Empty,
                                              Link = obj.XPathSelectElement("link/a") != null ? obj.XPathSelectElement("link/a").Attribute("href").Value : "#"
                                          };

                    lvOurServices.DataSource = itSolutionItems;
                    lvOurServices.DataBind();
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
        }
    }
    
    private void GetRSSFeed()
    {
        var blogsData = BlogsDataManager.GetRssFeed(4);
        lvITInflectionRssFeed.DataSource = blogsData;
        lvITInflectionRssFeed.DataBind();
    }

}