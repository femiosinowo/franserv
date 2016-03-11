﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TeamLogic.CMS;
using System.Text;
using Ektron.Cms.Instrumentation;


public partial class UserControls_HomePageLocations : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetFranchiseDetails();
            GetSocialIcons();
        }
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
            string directionsLink = "https://www.google.com/maps?daddr=q={0}";
            hiddenCenterLat.Value = fd.Latitude;
            hiddenCenterLong.Value = fd.Longitude;

            contactinfo.Append("<p class='location_address'>");
            contactinfo.Append(fd.Address1);
            if(!string.IsNullOrEmpty(fd.Address2))
            {
                contactinfo.Append("<br/>");
                contactinfo.Append(fd.Address2);
            }
            contactinfo.Append("<br/>");
            contactinfo.Append(fd.City + "  " + fd.State + " " + fd.Zipcode);
            contactinfo.Append("</p>");
            litLocAddress.Text = contactinfo.ToString();

            contactinfo.Clear();
            contactinfo.Append("<ul>");
            if (!string.IsNullOrEmpty(fd.PhoneNumber))
                contactinfo.Append("<li class='location-icon-phone'><span>" + fd.PhoneNumber.Replace("-", ".") + "</span></li>");


            if (!string.IsNullOrEmpty(fd.FaxNumber))
                contactinfo.Append("<li class='location-icon-fax'><span>" + fd.FaxNumber.Replace("-", ".") + "</span></li>");

            if (!string.IsNullOrEmpty(fd.Email))
                contactinfo.Append("<li class='location-icon-email'><span><a href='mailto:" + fd.Email + "'>" + fd.Email + "</a></span></li>");

            contactinfo.Append("</ul>");
            litLocContact.Text = contactinfo.ToString();

            if (!string.IsNullOrEmpty(fd.HoursOfOperation))
            {
                contactinfo.Clear();
                contactinfo.Append("<p class=\"store_hours\">");
                contactinfo.Append("<span>Store Hours:</span>");
                string[] workingHours = fd.HoursOfOperation.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (workingHours != null)
                {
                    foreach (string h in workingHours)
                    {
                        contactinfo.Append(h + "<br/>");
                    }
                }
                contactinfo.Append("</p>");
                ltrWorkingHours.Text = contactinfo.ToString();
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(fd.Address1);
            if (!string.IsNullOrEmpty(fd.Address2))
                sb.Append(" " + fd.Address2);
            sb.Append(" " + fd.City);
            sb.Append(" " + fd.State);
            sb.Append("-" + fd.Zipcode);
            sb.Append(" " + fd.Country);
            string address = sb.ToString().Replace(" ", "+");
            directions_lb.HRef = string.Format(directionsLink, address);

        }
    }

   /// <summary>
    /// This method is used to get social media icons
    /// </summary>
    private void GetSocialIcons()
    {
        var socialIconsData = SiteDataManager.GetSocialMediaLinks();
        if (socialIconsData != null)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("<ul>");
                if (socialIconsData.FaceBookUrl != null)
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.FaceBookUrl + "\"><img alt=\"Facebook\" src=\"" + socialIconsData.FaceBookImgPath + "\"/></a></li>");
                if (socialIconsData.TwitterUrl != null)
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.TwitterUrl + "\"><img alt=\"Twitter\" src=\"" + socialIconsData.TwitterImgPath + "\"/></a></li>");
                if (socialIconsData.GooglePlusUrl != null)
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.GooglePlusUrl + "\"><img alt=\"Google Plus\" src=\"" + socialIconsData.GooglePlusImgPath + "\"/></a></li>");
                if (socialIconsData.LinkedInUrl != null)
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.LinkedInUrl + "\"><img alt=\"LinkedIn\" src=\"" + socialIconsData.LinkedInImgPath + "\"/></a></li>");
                if (socialIconsData.StumbleUponUrl != null)
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.StumbleUponUrl + "\"><img alt=\"Stumble Upon\" src=\"" + socialIconsData.StumbleUponImgPath + "\"/></a></li>");
                if (socialIconsData.FlickrUrl != null)
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.FlickrUrl + "\"><img alt=\"Flickr\" src=\"" + socialIconsData.FlickrImgPath + "\"/></a></li>");
                if (socialIconsData.YouTubeUrl != null)
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.YouTubeUrl + "\"><img alt=\"You Tube\" src=\"" + socialIconsData.YouTubeImgPath + "\"/></a></li>");
                if (socialIconsData.ITInflectionsUrl != null)
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.ITInflectionsUrl + "\"><img alt=\"Marketing Tango\" src=\"" + socialIconsData.ITInflectionsUrl + "\"/></a></li>");
                sb.Append("</ul>");
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
            ltrSocialIcons.Text = sb.ToString();
        }
    }
}