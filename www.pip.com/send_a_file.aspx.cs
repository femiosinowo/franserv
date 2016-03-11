using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SirSpeedy.CMS;
using Ektron.Cms.Instrumentation;
using System.IO;
using Ektron.Cms.Content;
using Ektron.Cms.Framework.Content;
using System.Text;
using System.Configuration;
using Ektron.Cms;

public partial class login_national : System.Web.UI.Page
{
    public string BoxClientId = "'" + ConfigurationManager.AppSettings["BoxAPIClientId"] + "'";
    public string BoxSecretSecret = "'" + ConfigurationManager.AppSettings["BoxAPIClientSecret"] + "'";
    public string GoogleAPIClientId = "'" + ConfigurationManager.AppSettings["GoogleAPIClientId"] + "'";
    public string GoogleDriveAPIDeveloperKey = "'" + ConfigurationManager.AppSettings["GoogleDriveAPIDeveloperKey"] + "'";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            UserAPI userApi = new UserAPI();

            //if user session is expired or no data found then re-directing the user to home page
            if (Session["username"] == null && Session["useremail"] == null &&
                Session["externalLogin"] == null && Session["twitterLogin"] == null && userApi.UserId <= 0)
            {
                var fransId = FransDataManager.GetFranchiseId();
                if (!string.IsNullOrEmpty(fransId))
                    Response.Redirect("/" + fransId + "/");
                else
                    Response.Redirect("/");
            }

            UserData uData = null;
            if (userApi.UserId > 0)
            {
                uData = CommunityUserHelper.GetUserByUserId(userApi.UserId);
            }

            lblUserName.Text = (HttpContext.Current.Session["username"] != null) ? HttpContext.Current.Session["username"].ToString() : String.Empty;
            lblJobTitle.Text = (HttpContext.Current.Session["userJobTitle"] != null) ? HttpContext.Current.Session["userJobTitle"].ToString() : String.Empty;
            lblCompanyName.Text = (HttpContext.Current.Session["userCompanyName"] != null) ? HttpContext.Current.Session["userCompanyName"].ToString() : String.Empty;
            lblPhoneNumber.Text = (HttpContext.Current.Session["userPhoneNumber"] != null) ? HttpContext.Current.Session["userPhoneNumber"].ToString() : String.Empty;
            this.GetCenterDetails();
            this.GetDirectionsForGoogleMap();

            //if (Session["username"] != null)
            //{
            //    fullName = Session["username"].ToString();
            //}
            if (HttpContext.Current.Session["userFirstName"] != null)
            {
                fname.Value = HttpContext.Current.Session["userFirstName"].ToString();
            }
            else if (uData != null)
            {
                fname.Value = uData.FirstName;
            }
            if (HttpContext.Current.Session["userLastName"] != null)
            {
                lname.Value = HttpContext.Current.Session["userLastName"].ToString();
            }
            else if (uData != null)
            {
                lname.Value = uData.LastName;
                lblUserName.Text = uData.FirstName + " " + uData.LastName;
            }
            if (HttpContext.Current.Session["useremail"] != null)
            {
                email.Value = HttpContext.Current.Session["useremail"].ToString();
            }
            else if (uData != null)
            {
                email.Value = uData.Email;
            }
            if (HttpContext.Current.Session["userCenterId"] != null)
            {
                selectedCenter.Value = HttpContext.Current.Session["userCenterId"].ToString();
            }
            else
            {
                selectedCenter.Value = FransDataManager.GetFranchiseId();
            }
            if (HttpContext.Current.Session["userPhoneNumber"] != null)
            {
                phone.Value = HttpContext.Current.Session["userPhoneNumber"].ToString();
            }
            if (HttpContext.Current.Session["userJobTitle"] != null)
            {
                job_title.Value = HttpContext.Current.Session["userJobTitle"].ToString();
            }
            if (HttpContext.Current.Session["userCompanyName"] != null)
            {
                company.Value = HttpContext.Current.Session["userCompanyName"].ToString();
            }
            if (HttpContext.Current.Session["userWebsite"] != null)
            {
                webSite.Value = HttpContext.Current.Session["userWebsite"].ToString();
            }
        }
    }

    /// <summary>
    /// on file upload event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Upload_NationalComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
    {
        string fileExt = e.FileName.Substring(e.FileName.LastIndexOf('.'));
        if (fileExt.ToLower() != ".exe" && fileExt.ToLower() != ".bat" && fileExt.ToLower() != ".cmd")
        {
            Session["fileSize_" + e.FileId] = e.FileSize;
            Session["fileContents_" + e.FileId] = e.GetContents();
            Session["fileExtension_" + e.FileId] = e.FileName.Substring(e.FileName.LastIndexOf('.'));
            Session["fileName_" + e.FileId] = e.FileName;  
        }
    }
    
    /// <summary>
    /// process the center information
    /// </summary>
    private void GetCenterDetails()
    {
        string centerId = Request.QueryString["centerId"];
        if (!string.IsNullOrEmpty(centerId))
        {
            string googleMapImageSrc = "http://maps.googleapis.com/maps/api/staticmap?size=315x130&zoom=15&markers=icon:{0}/images/location-map-marker.png|{1},{2}&style=feature:landscape|color:0xe9e9e9&style=feature:poi|element:geometry|color:0xd8d8d8&sensor=true";
            var centerLocationList = FransDataManager.GetAllFransLocationDataList();
            if (centerLocationList != null && centerLocationList.Any())
            {
                var centerDetails = centerLocationList.Where(x => x.FransId == centerId).FirstOrDefault();
                if (centerDetails != null)
                {
                    ltrCenterAddress.Text = "<li id=\"location_address\">" + centerDetails.Address1 + "</li><li id=\"location_address2\">" + centerDetails.City + ", " + centerDetails.State + " " + centerDetails.Zipcode + "</li>";
                    ltrPhone.Text = centerDetails.PhoneNumber;
                    ltrFax.Text = centerDetails.FaxNumber;
                    ltrEmail.Text = "<a href=\"mailto:" + centerDetails.Email + "\">" + centerDetails.Email + "</a>";
                    ltrWorkingHours.Text = centerDetails.HoursOfOperation;
                    googleMapImage.Src = string.Format(googleMapImageSrc, "http://" + Request.ServerVariables["SERVER_NAME"], centerDetails.Latitude, centerDetails.Longitude);
                }
            }
        }
    }

    /// <summary>
    /// this method is used to update server name & directions for google map
    /// </summary>
    private void GetDirectionsForGoogleMap()
    {
        string centerId = Request.QueryString["centerId"];
        var fransData = FransDataManager.GetFransData(centerId);
        if (fransData != null)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(fransData.Address1);
            if (!string.IsNullOrEmpty(fransData.Address2))
                sb.Append(" " + fransData.Address2);
            sb.Append(" " + fransData.City);
            sb.Append(" " + fransData.State);
            sb.Append("-" + fransData.Zipcode);
            sb.Append(" " + fransData.Country);
            string address = sb.ToString().Replace(" ", "+");
            //viewDirectionDesktop.HRef = string.Format(FransDataManager.GoogleViewDirectionApiDesktop, address);
            //viewDirectionMobile.HRef = string.Format(FransDataManager.GoogleViewDirectionApiMobile, address);
        }
    }
}