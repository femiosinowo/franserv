﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SirSpeedy.CMS;
using System.Text;
using Ektron.Cms.Instrumentation;

public partial class UserControls_CenterDataWithSocialMediaLinks : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {            
            GetSocialIcons();
        }

        if(FransDataManager.IsFranchiseSelected())
        {
            GetCenterData();
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
                sb.Append("<ul class=\"sidebar_social_media\">");
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
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
            ltrSocialIcons.Text = sb.ToString();
        }
    }

    private void GetCenterData()
    {        
        var fd = FransDataManager.GetFransData();
        if (fd != null)
        {
            localInfoData.Visible = true;
            ltrPhone.Text = fd.PhoneNumber;
            if (!string.IsNullOrEmpty(fd.FaxNumber))
                ltrFax.Text = fd.FaxNumber;
            ltrEmail.Text = "<a href='mailto:" + fd.Email + "'>" + fd.Email + "</a>";
            string address2 = fd.Address2 != null && fd.Address2 != string.Empty ? fd.Address2 + "<br/>" : "";
            ltrAddress1.Text = fd.Address1 + " " + address2;
            ltrAddress2.Text = fd.City + ",  " + fd.State + " " + fd.Zipcode;            
        }
    }

}