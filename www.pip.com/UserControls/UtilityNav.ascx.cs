using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SirSpeedy.CMS;
using System.Text;
using Ektron.Cms.Instrumentation;
using System.Web.Script.Serialization;

public partial class UserControls_UtilityNav : System.Web.UI.UserControl
{  
    /// <summary>
    /// page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        cbFransOpport.DefaultContentID = ConfigHelper.GetValueLong("FranchiseOpportunityContentId");
        cbFransOpport.CacheInterval = ConfigHelper.GetValueLong("smallCacheInterval");
        cbFransOpport.Fill();

        SendAFile();

        if (!Page.IsPostBack)
        {
            if (FransDataManager.IsFranchiseSelected() || !string.IsNullOrEmpty(Request.QueryString["FransId"]))
            {
                pnlLocal.Visible = true;
                pnlLocalLeft.Visible = true;
                // uxRequestLocal.Visible = true;
                pnlNational.Visible = false;
                pnlNationalLeft.Visible = false;
                franchise_ops.Visible = false;
                // uxRequestNational.Visible = false;
                ltrContactUnmber.Text = FransDataManager.GetContactNumber();
                HyperLink2.NavigateUrl = String.Format("http://{0}/{1}/requestquote/", Request.ServerVariables["SERVER_NAME"],
                    FransDataManager.GetFranchiseId());
                var fransThirdPartyData = FransDataManager.GetFransThirdPartyData();
                if (fransThirdPartyData != null &&
                    fransThirdPartyData.SocialMediaData != null &&
                    !string.IsNullOrEmpty(fransThirdPartyData.SocialMediaData.MarketingTangoUrl))
                {
                    blogsLink.HRef = fransThirdPartyData.SocialMediaData.MarketingTangoUrl;
                }
            }
            else

            {
                pnlNationalLeft.Visible = true;
                pnlLocalLeft.Visible = false;
                pnlNational.Visible = true;
                franchise_ops.Visible = true;
                pnlNational.Visible = true;
                // uxRequestLocal.Visible = false;
                pnlLocal.Visible = false;
                // GetFranchiseDetails();
            }
            this.GetSocialIcons();

        }
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
                //images are driven by css
                sb.Append("<ul>");
                //sb.Append("<li class=\"um-facebook\"><a target=\"_blank\" href=\"" + socialIconsData.FaceBookUrl + "\"><span></span></a></li>");
                //sb.Append("<li class=\"um-twitter\"><a target=\"_blank\" href=\"" + socialIconsData.TwitterUrl + "\"><span></span></a></li>");
                //sb.Append("<li class=\"um-google-plus\"><a target=\"_blank\" href=\"" + socialIconsData.GooglePlusUrl + "\"><span></span></a></li>");
                //sb.Append("<li class=\"um-linkedin\"><a target=\"_blank\" href=\"" + socialIconsData.LinkedInUrl + "\"><span></span></a></li>");
                //sb.Append("<li class=\"um-stumble-upon\"><a target=\"_blank\" href=\"" + socialIconsData.StumbleUponUrl + "\"><span></span></a></li>");
                //sb.Append("<li class=\"um-flickr\"><a target=\"_blank\" href=\"" + socialIconsData.FlickrUrl + "\"><span></span></a></li>");
                //sb.Append("<li class=\"um-youtube\"><a target=\"_blank\" href=\"" + socialIconsData.YouTubeUrl + "\"><span></span></a></li>");
                //sb.Append("<li class=\"um-marketing-tango\"><a target=\"_blank\" href=\"" + socialIconsData.MarketingTangoUrl + "\"><span></span></a></li>");
                if (!String.IsNullOrWhiteSpace(socialIconsData.FaceBookUrl))
                sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.FaceBookUrl + "\"><img alt=\"Facebook\" src=\"/images/social-icons/facebook.png\"></a></li>");

                if (!String.IsNullOrWhiteSpace(socialIconsData.TwitterUrl))
                sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.TwitterUrl + "\"><img alt=\"Twitter\" src=\"/images/social-icons/twitter.png\"></a></li>");

                if (!String.IsNullOrWhiteSpace(socialIconsData.GooglePlusUrl))
                sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.GooglePlusUrl + "\"><img alt=\"Google Plus\" src=\"/images/social-icons/google-plus.png\"></a></li>");

                if (!String.IsNullOrWhiteSpace(socialIconsData.LinkedInUrl))
                sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.LinkedInUrl + "\"><img alt=\"LinkedIn\" src=\"/images/social-icons/linked-in.png\"></a></li>");

                if (!String.IsNullOrWhiteSpace(socialIconsData.StumbleUponUrl))
                sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.StumbleUponUrl + "\"><img alt=\"Stumble Upon\" src=\"/images/social-icons/stumble-upon.png\"></a></li>");


                if (!String.IsNullOrWhiteSpace(socialIconsData.FlickrUrl))
                sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.FlickrUrl + "\"><img alt=\"Flickr\" src=\"/images/social-icons/flickr.png\"></a></li>");

                if (!String.IsNullOrWhiteSpace(socialIconsData.YouTubeUrl))
                sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.YouTubeUrl + "\"><img alt=\"You Tube\" src=\"/images/social-icons/you-tube.png\"></a></li>");

                if (!String.IsNullOrWhiteSpace(socialIconsData.MarketingTangoUrl))
                sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.MarketingTangoUrl + "\"><img alt=\"Marketing Tango\" src=\"/images/social-icons/marketing-tango.png\"></a></li>");
                sb.Append("</ul>");
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
            ltrSocialIcons.Text = sb.ToString();

        }
    }
   
    private void SendAFile()
    {
        Ektron.Cms.UserAPI userApi = new Ektron.Cms.UserAPI();

        if (HttpContext.Current.Session["username"] != null || HttpContext.Current.Session["useremail"] != null ||
            HttpContext.Current.Session["externalLogin"] != null || HttpContext.Current.Session["twitterLogin"] != null ||
            userApi.UserId > 0)
        {
            // uxSendAFile.Visible = false;
            //  temp.Attributes.Add("class", "user_logged_in");

            string centerid = String.Empty;
            if (Session["userCenterId"] != null)
                centerid = Session["userCenterId"].ToString();

            string safLink = (!String.IsNullOrWhiteSpace(centerid)) ? String.Format("?centerId={0}", centerid) : String.Empty;
            //if (!String.IsNullOrWhiteSpace(centerid)) centerid = centerid + "/";

            if ((userApi.UserId > 0) && (!String.IsNullOrWhiteSpace(centerid)))
            {
                //if user is logged in and local is selected
                nationalsendafilelink.Attributes.Add("href", String.Format("/{0}/send-a-file/{1}", centerid, safLink));
                localsendafilelink.Attributes.Add("href", String.Format("/{0}/send-a-file/{1}", centerid, safLink));
            }
            else if ((userApi.UserId > 0) && (String.IsNullOrWhiteSpace(centerid)))
            {
                //if user is logged in and local is not selected
                nationalsendafilelink.Attributes.Add("href", String.Format("/send-a-file/"));
                localsendafilelink.Attributes.Add("href", String.Format("/send-a-file/"));
            }
            else if ((Session["username"] != null || Session["useremail"] != null || Session["externalLogin"] != null) && (!String.IsNullOrWhiteSpace(centerid)))
            {
                //third party login & local center is selected
                nationalsendafilelink.Attributes.Add("href", String.Format("/{0}/social-send-a-file/{1}", centerid, safLink));
                localsendafilelink.Attributes.Add("href", String.Format("/{0}/social-send-a-file/{1}", centerid, safLink));
            }
            else if (Session["username"] != null || Session["useremail"] != null || Session["externalLogin"] != null)
            {
                //third party login & no local center is selected
                nationalsendafilelink.Attributes.Add("href", String.Format("/social-send-a-file/"));
                localsendafilelink.Attributes.Add("href", String.Format("/social-send-a-file/"));
            }
            else
            {
                //default to national saf page
                nationalsendafilelink.Attributes.Add("href", String.Format("/send-a-file/{0}", safLink));
                localsendafilelink.Attributes.Add("href", String.Format("/send-a-file/{0}", safLink));
            }
            nationalsendafilelink.CssClass = String.Empty;
            localsendafilelink.CssClass = String.Empty;

        }
        else
        {
            localsendafilelink.Attributes.Add("href", "#send_file");
            nationalsendafilelink.Attributes.Add("href", "#send_file");
        }
    }
}