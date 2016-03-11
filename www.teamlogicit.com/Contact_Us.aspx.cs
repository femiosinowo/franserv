using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TeamLogic.CMS;
using System.Text;
using Ektron.Cms.Instrumentation;

public partial class Contact_Us : PageBase
{
    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetDirectionsForGoogleMap();
            GetSocialMediaIcons();
            GetFranchiseDetails();
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
            StringBuilder contactinfo = new StringBuilder();
            contactinfo.Append("<li class=\"address\">");
            contactinfo.Append(fd.Address1 + ", <br/>");
            if (!string.IsNullOrEmpty(fd.Address2))
            {
                contactinfo.Append(fd.Address2 + ", <br/>");
            }
            contactinfo.Append(fd.City + ",  " + fd.State + " " + fd.Zipcode);
            contactinfo.Append("</li>");
            litLocAddress.Text = contactinfo.ToString();

            ltrPhoneNumber.Text = "<li class=\"telephone\"><a href=\"tel:+" + fd.PhoneNumber + "\">" + fd.PhoneNumber + "</a></li>";
            ltrEmailAddress.Text = "<li class=\"email\"><a href='mailto:" + fd.Email + "'>" + fd.Email + "</a></li>";
        }
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
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.FaceBookUrl + "\"><img alt=\"Facebook\" src=\"/images/social-icons/green_54_facebook.png\" /></a></li>");
                if (!string.IsNullOrEmpty(socialIconsData.TwitterImgPath) && !string.IsNullOrEmpty(socialIconsData.TwitterUrl))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.TwitterUrl + "\"><img alt=\"Twitter\" src=\"/images/social-icons/green_54_twitter.png\" /></a></li>");
                if (!string.IsNullOrEmpty(socialIconsData.GooglePlusImgPath) && !string.IsNullOrEmpty(socialIconsData.GooglePlusUrl))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.GooglePlusUrl + "\"><img alt=\"Google Plus\" src=\"/images/social-icons/green_54_gplus.png\" /></a></li>");
                if (!string.IsNullOrEmpty(socialIconsData.LinkedInImgPath) && !string.IsNullOrEmpty(socialIconsData.LinkedInUrl))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.LinkedInUrl + "\"><img alt=\"LinkedIn\" src=\"/images/social-icons/green_54_linkedin.png\" /></a></li>");
                if (!string.IsNullOrEmpty(socialIconsData.StumbleUponImgPath) && !string.IsNullOrEmpty(socialIconsData.StumbleUponUrl))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.StumbleUponUrl + "\"><img alt=\"Stumble Upon\" src=\"/images/social-icons/green_54_stumbleupon.png\" /></a></li>");
                if (!string.IsNullOrEmpty(socialIconsData.FlickrImgPath) && !string.IsNullOrEmpty(socialIconsData.FlickrUrl))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.FlickrUrl + "\"><img alt=\"Flickr\" src=\"/images/social-icons/green_54_flickr.png\" /></a></li>");
                if (!string.IsNullOrEmpty(socialIconsData.YouTubeImgPath) && !string.IsNullOrEmpty(socialIconsData.YouTubeUrl))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.YouTubeUrl + "\"><img alt=\"You Tube\" src=\"/images/social-icons/green_54_youtube.png\" /></a></li>");
                if (!string.IsNullOrEmpty(socialIconsData.ITInflectionsImgPath) && !string.IsNullOrEmpty(socialIconsData.ITInflectionsUrl))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.ITInflectionsUrl + "\"><img alt=\"IT Inflections\" src=\"/images/social-icons/green_54_it.png\" /></a></li>");
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
    /// this method is used to update server name & directions for google map
    /// </summary>
    private void GetDirectionsForGoogleMap()
    {        
        var fransData = FransDataManager.GetFransData();
        if (fransData != null)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<p class='info-address'>" + fransData.Address1 + ", <br/>");
            if (!string.IsNullOrEmpty(fransData.Address2))
                sb.Append(fransData.Address2 + ", <br>");           
            sb.Append(" " + fransData.City);
            sb.Append(", " + fransData.State);
            sb.Append(" " + fransData.Zipcode);
            sb.Append(" " + fransData.Country);
            sb.Append(".</p>");
            sb.Append("<p class='info-address'>" + fransData.PhoneNumber + "</p>");
            //string address = sb.ToString().Replace(" ", "+");
            hiddenCenterAddress.Value = sb.ToString();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblError.Text = "";
            var centerId = FransDataManager.GetFranchiseId();
            var status = FransDataManager.SaveContactUsRequest(centerId, txtFirstName.Text, txtLastName.Text, contact_message.Value, txtEmail.Text);
            var fransId = FransDataManager.GetFranchiseId();
            if (!string.IsNullOrEmpty(fransId))
                Response.Redirect("/" + fransId + "/thank-you/?type=contactUs");
            else
                Response.Redirect("/thank-you/?type=contactUs");
        }
    }
}