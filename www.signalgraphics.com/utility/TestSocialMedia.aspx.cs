using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SignalGraphics.CMS;
using System.Configuration;
using Ektron.Cms.Framework.Content;
using Ektron.Cms.Content;
using Ektron.Cms;
using Ektron.Cms.Common;
using System.Xml.Linq;
using System.Xml.XPath;
using Ektron.Cms.Instrumentation;
using FlickrNet;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Web.Script.Serialization;


public partial class TestSocialMedia : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        Ektron.Cms.API.User.User uApi = new Ektron.Cms.API.User.User();
        if (uApi.UserId > 0 && uApi.IsAdmin())
        {
            pnlAdmin.Visible = true;
            pnlAnonymous.Visible = false;
        }
        else
        {
            pnlAnonymous.Visible = true;
            pnlAdmin.Visible = false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        Ektron.Cms.API.User.User uApi = new Ektron.Cms.API.User.User();
        bool clearCache = false;
        string centerId = "";

        if (Request.QueryString.HasKeys() && uApi.UserId > 0 && uApi.IsAdmin())
        {
            if (!string.IsNullOrEmpty(Request.QueryString["cache"]))
            {
                bool.TryParse(Request.QueryString["cache"], out clearCache);
            }

            if (!string.IsNullOrEmpty(Request.QueryString["centerid"]))
            {
                centerId = Request.QueryString["centerid"];
            }

            var nationalData = AdminDataManager.GetNationalSocialMediaLinks(clearCache);
            if (nationalData != null)
            {
                var socialIconsData = nationalData;
                sb.Append("<ul class=\"small_social_media\">");
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
                ltrNational.Text = sb.ToString();
            }

            var fransData = FransDataManager.GetFransThirdPartyData(centerId, clearCache);
            if (fransData != null && fransData.SocialMediaData != null)
            {
                sb = new StringBuilder();
                var socialIconsData = fransData.SocialMediaData;
                sb.Append("<ul class=\"small_social_media\">");
                if ((!string.IsNullOrEmpty(socialIconsData.FaceBookUrl)))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.FaceBookUrl + "\"><img alt=\"Facebook\" src=\"" + nationalData.FaceBookImgPath + "\"/></a></li>");
                if ((!string.IsNullOrEmpty(socialIconsData.TwitterUrl)))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.TwitterUrl + "\"><img alt=\"Twitter\" src=\"" + nationalData.TwitterImgPath + "\"/></a></li>");
                if ((!string.IsNullOrEmpty(socialIconsData.GooglePlusUrl)))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.GooglePlusUrl + "\"><img alt=\"Google Plus\" src=\"" + nationalData.GooglePlusImgPath + "\"/></a></li>");
                if ((!string.IsNullOrEmpty(socialIconsData.LinkedInUrl)))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.LinkedInUrl + "\"><img alt=\"LinkedIn\" src=\"" + nationalData.LinkedInImgPath + "\"/></a></li>");
                if ((!string.IsNullOrEmpty(socialIconsData.StumbleUponUrl)))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.StumbleUponUrl + "\"><img alt=\"Stumble Upon\" src=\"" + nationalData.StumbleUponImgPath + "\"/></a></li>");
                if ((!string.IsNullOrEmpty(socialIconsData.FlickrUrl)))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.FlickrUrl + "\"><img alt=\"Flickr\" src=\"" + nationalData.FlickrImgPath + "\"/></a></li>");
                if ((!string.IsNullOrEmpty(socialIconsData.YouTubeUrl)))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.YouTubeUrl + "\"><img alt=\"You Tube\" src=\"" + nationalData.YouTubeImgPath + "\"/></a></li>");
                if ((!string.IsNullOrEmpty(socialIconsData.MarketingTangoUrl)))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.MarketingTangoUrl + "\"><img alt=\"Marketing Tango\" src=\"" + nationalData.MarketingTangoImgPath + "\"/></a></li>");
                sb.Append("</ul>");
                ltrLocal.Text = sb.ToString();
            }
        }
    }

}