using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SignalGraphics.CMS;
using System.Text;
using Ektron.Cms;

public partial class register : PageBase
{

    UserAPI userApi = new UserAPI();

    /// <summary>
    /// page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (FransDataManager.IsFranchiseSelected())
            {
                pnlLocalRegister.Visible = true;
                pnlNationalRegister.Visible = false;
                this.GetFranchiseDetails();
                this.GetDirectionsForGoogleMap();
            }
            else
            {
                pnlNationalRegister.Visible = true;
                pnlLocalRegister.Visible = false;
                viewDirectionRegisterDesktop.Value = FransDataManager.GoogleViewDirectionApiDesktop;
                viewDirectionRegisterMobile.Value = FransDataManager.GoogleViewDirectionApiMobile;
            }
        }
    }


    /// <summary>
    /// button click event for Local register site user
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLocalRegister_Click(object sender, EventArgs e)
    {
        string fransId;
        Page.Validate("LocalRegister");

        if (!Page.IsValid)
        {
            // do something when it is not valid - Print Message
        }
        else
        {
            GetFranchiseId(out fransId);
            bool isSaved = false;
            string email = local_email.Text;
            string firstName = local_fname.Text;
            string lastName = local_lname.Text;
            string selectedCenter = fransId; //"washingtondc170"; //
            string phoneNumber = local_phone.Text;
            string jobTitle = local_job_title.Text;
            string companyName = local_company.Text;
            string companyWebsite = local_website.Text;

            string username = firstName + " " + lastName;
            string password = local_password.Text;
            string password_confirm = local_confirm_password.Text;

            if (password.Equals(password_confirm))
            {

                var selectedCenterData = FransDataManager.GetFransData(selectedCenter);
                bool isCMSUserSaved = false;
                long cmsUserId = CommunityUserHelper.AddCmsMembershipUser(firstName, lastName, email, password, email, selectedCenter);
                if (cmsUserId > 0)
                {
                    isCMSUserSaved = true;
                    CommunityUserHelper.AddUserToCommunityGroup(cmsUserId, selectedCenterData.CmsCommunityGroupId, selectedCenterData.FransId, selectedCenterData.PhoneNumber);
                    var uData = userApi.logInUser(email, password, Request.ServerVariables["SERVER_NAME"], "", "");
                    userApi.SetAuthenticationCookie(uData);                       
                }
                else
                {
                    isCMSUserSaved = false;
                    lblEmailExistLocal.Visible = true;
                    lblEmailExistLocal.Text = "Username " + email + " already exists";
                    return;
                }

                isSaved = FransDataManager.SaveUserRegisterationData(selectedCenter, firstName, lastName, jobTitle, companyName, email, phoneNumber, companyWebsite);
                if (isSaved && isCMSUserSaved)
                {
                    HttpContext.Current.Session.Add("username", username);
                    HttpContext.Current.Session.Add("useremail", email);
                    HttpContext.Current.Session.Add("userFirstName", firstName);
                    HttpContext.Current.Session.Add("userLastName", lastName);
                    HttpContext.Current.Session.Add("userJobTitle", jobTitle);
                    HttpContext.Current.Session.Add("userCompanyName", companyName);
                    HttpContext.Current.Session.Add("userPhoneNumber", phoneNumber);
                    HttpContext.Current.Session.Add("userCenterId", selectedCenter);
                    HttpContext.Current.Session.Add("userWebsite", companyWebsite);
                    HttpContext.Current.Session.Add("externalLogin", "false");

                    if (!string.IsNullOrEmpty(fransId))
                        Response.Redirect("/" + fransId + "/send-a-file/?centerId=" + selectedCenter, false);
                    else
                        Response.Redirect("/send-a-file/?centerId=" + selectedCenter);
                }
            }
        }
    }

    /// <summary>
    /// button click event for National register site user
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNationalRegister_Click(object sender, EventArgs e)
    {
        Page.Validate("NationalRegister");

        if (!Page.IsValid)
        {
            // do something when it is not valid - Print Message
        }
        else
        {
            bool isSaved = false;
            string email = national_email.Text;
            string firstName = national_fname.Text;
            string lastName = national_lname.Text;
            string selectedCenter = txtRegisterChooseLocation.Text; //"washingtondc170"; //
            string phoneNumber = national_phone.Text;
            string jobTitle = national_job_title.Text;
            string companyName = national_company.Text;
            string companyWebsite = national_website.Text;

            string username = firstName + " " + lastName;
            string password = national_password.Text;
            string password_confirm = national_confirm_password.Text;

            if (password.Equals(password_confirm))
            {

                //save data to Ektron Membership DataTable
                //var udata1 = userApi.GetUserByUsername(email);
                //if (udata1 == null && udata1.Id <= 0)
                //{
                //UserData newUser = new UserData();
                //newUser.DisplayName = username;
                //newUser.FirstName = firstName;
                //newUser.LastName = lastName;
                //newUser.Email = email;
                //newUser.Password = password;

                //try
                //{
                //    userApi.AddNewUser(newUser);
                //}
                //catch { }
                //}
                //else
                //{

                //}

                var selectedCenterData = FransDataManager.GetFransData(selectedCenter);
                bool isCMSUserSaved = false;
                long cmsUserId = CommunityUserHelper.AddCmsMembershipUser(firstName, lastName, email, password, email, selectedCenter);
                if (cmsUserId > 0)
                {
                    isCMSUserSaved = true;
                    CommunityUserHelper.AddUserToCommunityGroup(cmsUserId, selectedCenterData.CmsCommunityGroupId, selectedCenterData.FransId, selectedCenterData.PhoneNumber);
                    var uData = userApi.logInUser(email, password, Request.ServerVariables["SERVER_NAME"], "", "");
                    userApi.SetAuthenticationCookie(uData);
                }
                else
                {
                    isCMSUserSaved = false;
                    lblEmailExists.Visible = true;
                    lblEmailExists.Text = "Username " + email + " already exists";
                    return;
                }

                //save data to custom db 
                isSaved = FransDataManager.SaveUserRegisterationData(selectedCenter, firstName, lastName, jobTitle, companyName, email, phoneNumber, companyWebsite);

                if (isSaved && isCMSUserSaved)
                {
                    HttpContext.Current.Session.Add("username", username);
                    HttpContext.Current.Session.Add("useremail", email);
                    HttpContext.Current.Session.Add("userFirstName", firstName);
                    HttpContext.Current.Session.Add("userLastName", lastName);
                    HttpContext.Current.Session.Add("userJobTitle", jobTitle);
                    HttpContext.Current.Session.Add("userCompanyName", companyName);
                    HttpContext.Current.Session.Add("userPhoneNumber", phoneNumber);
                    HttpContext.Current.Session.Add("userCenterId", selectedCenter);
                    HttpContext.Current.Session.Add("userWebsite", companyWebsite);
                    HttpContext.Current.Session.Add("externalLogin", "false");
                    Response.Redirect("/send-a-file/?centerId=" + selectedCenter, false);
                    //Response.Redirect("/thank_you.aspx?type=register&centerId=" + selectedCenter);
                }
            }
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

            if (!string.IsNullOrEmpty(fd.PhoneNumber))
                contactinfo.Append("<li class='contact-icon-phone'><span>" + fd.PhoneNumber + "</span></li>");

            if (!string.IsNullOrEmpty(fd.FaxNumber))
                contactinfo.Append("<li class='contact-icon-fax'><span>" + fd.FaxNumber + "</span></li>");

            if (!string.IsNullOrEmpty(fd.Email))
                contactinfo.Append("<li class='contact-icon-email'><span><a href='mailto:" + fd.Email + "'>" + fd.Email + "</a></span></li>");

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
    /// get Franchise Id for Local Register
    /// </summary>
    private void GetFranchiseId(out string fransId)
    {
        var franchise = FransDataManager.GetFransData();
        fransId = string.Empty;
        if (franchise != null)
        {
            fransId = franchise.FransId;
        }
        
    }

}