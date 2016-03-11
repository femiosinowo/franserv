using Ektron.Cms.Common;
using Ektron.Cms.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TeamLogic.CMS;
using Ektron.Cms.Instrumentation;
using System.Xml;
using System.Text;

public partial class UserControls_AboutUsLocal : System.Web.UI.UserControl
{
    public string JoinOurTeamBackgroundImage = "/images/join_team_bg.jpg";

    protected void Page_Init(object sender, EventArgs e)
    {
        cbOurTeamCompnayInfo.DefaultContentID = ConfigHelper.GetValueLong("AboutUsLocalOurTeamCompnayInfoCId");
        cbOurTeamCompnayInfo.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbOurTeamCompnayInfo.Fill();

        cbJoinOurTeam.DefaultContentID = ConfigHelper.GetValueLong("AboutUsLocalOurTeamCId");
        cbJoinOurTeam.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbJoinOurTeam.Fill();

        if (cbJoinOurTeam.EkItem != null && !string.IsNullOrEmpty(cbJoinOurTeam.EkItem.Image) && cbJoinOurTeam.EkItem.Image.ToLower().IndexOf("workarea") <= -1)
            JoinOurTeamBackgroundImage = cbJoinOurTeam.EkItem.Image;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            GetCompanyInfoNational();
            GetCompanyLocationContent();
            GetOurTeamInfo();
        }

        joinTeamImg.Attributes.Add("data-image", JoinOurTeamBackgroundImage);
        joinTeamImg.Attributes.Add("data-image-mobile", JoinOurTeamBackgroundImage);        
    }

    private void GetCompanyInfoNational()
    {
        var fd = FransDataManager.GetFransData();
        if (fd != null)
        {
            var whyWeAreDiff = FransDataManager.GetWhyWeAreDiffContent(fd.FransId);
            if (whyWeAreDiff != null)
            {
                ltrTitle.Text = whyWeAreDiff.ContentTitle;

                //here is a quick fix & hack for the production issue
                string formattedDes1 = whyWeAreDiff.ContentDescription;
                formattedDes1 = formattedDes1.Replace("<div>", "<span>");
                formattedDes1 = formattedDes1.Replace("</div>", "</span>");
                formattedDes1 = formattedDes1.Replace("<p>", "<span>");
                formattedDes1 = formattedDes1.Replace("</p>", "</span>");
                ltrDescription1.Text = formattedDes1;

                string formattedDes2 = whyWeAreDiff.VideoStatementText;
                formattedDes2 = formattedDes2.Replace("<div>", "<span>");
                formattedDes2 = formattedDes2.Replace("</div>", "</span>");
                formattedDes2 = formattedDes2.Replace("<p>", "<span>");
                formattedDes2 = formattedDes2.Replace("</p>", "</span>");
                ltrDescription2.Text = formattedDes2;
            }
        }
    }

    private void GetCompanyLocationContent()
    {
        var fd = FransDataManager.GetFransData();
        if (fd != null)
        {
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

            var socialIconsData = SiteDataManager.GetSocialMediaLinks();
            if (socialIconsData != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<ul>");
                if (!string.IsNullOrEmpty(socialIconsData.FaceBookImgPath))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.FaceBookUrl + "\"><img alt=\"Facebook\" src=\"/images/social-icons/green_54_facebook.png\"/></a></li>");
                if (!string.IsNullOrEmpty(socialIconsData.TwitterImgPath))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.TwitterUrl + "\"><img alt=\"Twitter\" src=\"/images/social-icons/green_54_twitter.png\"/></a></li>");
                if (!string.IsNullOrEmpty(socialIconsData.GooglePlusImgPath))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.GooglePlusUrl + "\"><img alt=\"Google Plus\" src=\"/images/social-icons/green_54_gplus.png\"/></a></li>");
                if (!string.IsNullOrEmpty(socialIconsData.LinkedInImgPath))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.LinkedInUrl + "\"><img alt=\"LinkedIn\" src=\"/images/social-icons/green_54_linkedin.png\"/></a></li>");
                if (!string.IsNullOrEmpty(socialIconsData.StumbleUponImgPath))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.StumbleUponUrl + "\"><img alt=\"Stumble Upon\" src=\"/images/social-icons/green_54_stumbleupon.png\"/></a></li>");
                if (!string.IsNullOrEmpty(socialIconsData.FlickrImgPath))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.FlickrUrl + "\"><img alt=\"Flickr\" src=\"/images/social-icons/green_54_flickr.png\"/></a></li>");
                if (!string.IsNullOrEmpty(socialIconsData.YouTubeImgPath))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.YouTubeUrl + "\"><img alt=\"You Tube\" src=\"/images/social-icons/green_54_youtube.png\"/></a></li>");
                if (!string.IsNullOrEmpty(socialIconsData.ITInflectionsImgPath))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.ITInflectionsUrl + "\"><img alt=\"Marketing Tango\" src=\"/images/social-icons/green_54_IT.png\"/></a></li>");
                sb.Append("</ul>");

                ltrSocialIcons.Text = sb.ToString();
            }

            hiddenCenterLat.Value = fd.Latitude;
            hiddenCenterLong.Value = fd.Longitude;
        }
    }

    private void GetOurTeamInfo()
    {
        var centerEmployeeIds = FransDataManager.GetFransEmployeeIds();
        if (centerEmployeeIds != null && centerEmployeeIds.Any())
        {
            ourTeamHeadLine.Visible = true;
            List<Employee> allEmployees = new List<Employee>();
            for (int i = 0; i < centerEmployeeIds.Count; i++ )
            {
                var employeeData = GetEmployeeById(centerEmployeeIds[i]);
                if (employeeData != null)
                {
                    employeeData.IsActive = i + 1;
                    allEmployees.Add(employeeData);
                }
            }
            lvOurTeam.DataSource = allEmployees;
            lvOurTeam.DataBind();
                      
        }
    }
   
    private string GetAboutTeamLogic()
    {
        string companyInfo = "";       
        if(cbOurTeamCompnayInfo != null && cbOurTeamCompnayInfo.EkItem != null)
        {
            companyInfo = cbOurTeamCompnayInfo.EkItem.Html;
        }
        return companyInfo;
    }

    private Employee GetEmployeeById(long employeeId)
    {
        Employee e = null;
        if (employeeId > 0)
        {
            return FransDataManager.GetEmployeeById(employeeId);
        }
        return e;
    }

    public string GetEmployeeImage(string picturePath, string gender)
    {
        string image = "";

        if (!string.IsNullOrEmpty(picturePath))
            return picturePath;
        if (gender == "Male")
            image = "/images/photos/our-team-placeholder-male.png";
        if (gender == "Female")
            image = "/images/photos/our-team-placeholder-female.png";

        return image;
    }
        
}