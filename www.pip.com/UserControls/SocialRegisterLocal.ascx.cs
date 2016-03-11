using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SirSpeedy.CMS;
using System.Text;
using Ektron.Cms.Framework.Content;
using Ektron.Cms.Content;
using Ektron.Cms.Instrumentation;
using System.IO;
using System.Configuration;

public partial class UserControls_SocialRegisterLocal : System.Web.UI.UserControl
{
    public string BoxClientId = "'" + ConfigurationManager.AppSettings["BoxAPIClientId"] + "'";
    public string BoxSecretSecret = "'" + ConfigurationManager.AppSettings["BoxAPIClientSecret"] + "'";
    public string GoogleAPIClientId = "'" + ConfigurationManager.AppSettings["GoogleAPIClientId"] + "'";
    public string GoogleDriveAPIDeveloperKey = "'" + ConfigurationManager.AppSettings["GoogleDriveAPIDeveloperKey"] + "'";

    /// <summary>
    /// page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        DateFormField1.DefaultFormID = ConfigHelper.GetValueLong("DateFieldFormId");

        if (!Page.IsPostBack)
        {
            //if user session is expired or no data found then re-directing the user to home page
            if (Session["username"] == null && Session["useremail"] == null &&
                Session["externalLogin"] == null && Session["twitterLogin"] == null)
            {
                var fransId = FransDataManager.GetFranchiseId();
                if (!string.IsNullOrEmpty(fransId))
                    Response.Redirect("/" + fransId + "/");
                else
                    Response.Redirect("/");
            }

            GetFranchiseDetails();
            GetDirectionsForGoogleMap();
            selectedCenter.Value = FransDataManager.GetFranchiseId();

            lblUserName.Text = (Session["username"] != null) ? Session["username"].ToString() : String.Empty;
            if (Session["twitterLogin"] != null && Session["twitterLogin"].ToString().Equals("false"))
            {
                lblUserEmail.Text = txtEmail.Text = (Session["useremail"] != null) ? Session["useremail"].ToString() : String.Empty;
            }
            else if (Session["twitterLogin"] != null && Session["twitterLogin"].ToString().Equals("true"))
            {
                emailFieldSection.Attributes.Add("style", "display:block;");
                lblUserEmail.Visible = false;
            }
            else
            {
                emailFieldSection.Attributes.Add("style", "display:block;");
                lblUserEmail.Visible = false;
            }

            string fullName = String.Empty;
            string firstName = String.Empty;
            string lastName = String.Empty;
            if (Session["username"] != null)
            {
                fullName = Session["username"].ToString();
            }
            var nameArray = fullName.Split(' ');
            if (nameArray.Length == 2)
            {
                firstName = nameArray[0];
                lastName = nameArray[1];
            }
            else if (nameArray.Length > 2)
            {
                firstName = nameArray[0];
                lastName = nameArray[nameArray.Length - 1];
            }
            else if (nameArray.Length == 1)
            {
                firstName = nameArray[0];
            }

            fname.Value = firstName;
            lname.Value = lastName;
            lblUserName.Text = firstName + " " + lastName;
        }
    }
    
    /// <summary>
    /// Gets the Franchise Data
    /// </summary>
    private void GetFranchiseDetails()
    {
        var fd = FransDataManager.GetFransData();
        if (fd != null)
        {
            StringBuilder contactinfo = new StringBuilder();
            contactinfo.Append("<ul class='contact_info'>");
            contactinfo.Append("<li class='contact-icon-location'>");
            contactinfo.Append("<span>");
            contactinfo.Append(fd.Address1);
            contactinfo.Append("<br>");
            contactinfo.Append(fd.City + "  " + fd.State + " " + fd.Zipcode);
            contactinfo.Append("</span>");
            contactinfo.Append("</li>");
            contactinfo.Append("</ul>");
            contactinfo.Append("<hr />");

            contactinfo.Append("<ul class='contact_info'>");
            if (!string.IsNullOrEmpty(fd.PhoneNumber))
                contactinfo.Append("<li class='contact-icon-phone'><span>" + fd.PhoneNumber + "</span></li>");

            if (!string.IsNullOrEmpty(fd.FaxNumber))
                contactinfo.Append("<li class='contact-icon-fax'><span>" + fd.FaxNumber + "</span></li>");

            if (!string.IsNullOrEmpty(fd.Email))
                contactinfo.Append("<li class='contact-icon-email'><span><a href='mailto:" + fd.Email + "'>" + fd.Email + "</a></span></li>");

            contactinfo.Append("</ul>");
            contactinfo.Append("<hr />");

            contactinfo.Append("<ul class='contact_info'>");
            contactinfo.Append("<li class='contact-icon-hours'>");
            contactinfo.Append("<span>Hours<br />");
            contactinfo.Append(fd.HoursOfOperation);
            contactinfo.Append("</span>");
            contactinfo.Append("</li>");
            contactinfo.Append("</ul>");

            litFranchiseContactInfo.Text = contactinfo.ToString();
            googleMapImage.Src = string.Format(FransDataManager.GoogleStaticLargeImagePath, "http://" + Request.ServerVariables["SERVER_NAME"], fd.Latitude, fd.Longitude);
        }
    }

    /// <summary>
    /// this method is used to get directions for google map
    /// </summary>
    private void GetDirectionsForGoogleMap()
    {
        var fransData = FransDataManager.GetFransData();
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
            viewDirectionDesktop.HRef = string.Format(FransDataManager.GoogleViewDirectionApiDesktop, address);
            viewDirectionMobile.HRef = string.Format(FransDataManager.GoogleViewDirectionApiMobile, address);
        }
    }

    /// <summary>
    /// on file upload event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Upload_LocalComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
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
        
}