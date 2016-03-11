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
using System.IO;
using Ektron.Cms.Instrumentation;
using System.Configuration;

public partial class UserControls_SocialRegisterNational : System.Web.UI.UserControl
{
    public string BoxClientId = "'" + ConfigurationManager.AppSettings["BoxAPIClientId"] + "'";
    public string BoxSecretSecret = "'" + ConfigurationManager.AppSettings["BoxAPIClientSecret"] + "'";
    public string GoogleAPIClientId = "'" + ConfigurationManager.AppSettings["GoogleAPIClientId"] + "'";
    public string GoogleDriveAPIDeveloperKey = "'" + ConfigurationManager.AppSettings["GoogleDriveAPIDeveloperKey"] + "'";
    
    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        DateFormField1.DefaultFormID = ConfigHelper.GetValueLong("DateFieldFormId");

        viewDirectionRegisterDesktop.Value = FransDataManager.GoogleViewDirectionApiDesktop;
        viewDirectionRegisterMobile.Value = FransDataManager.GoogleViewDirectionApiMobile;

        cbSendAFileSideTitle.DefaultContentID = ConfigHelper.GetValueLong("SAFSideTitleContentId");
        cbSendAFileSideTitle.CacheInterval = ConfigHelper.GetValueLong("smallCacheInterval");
        cbSendAFileSideTitle.Fill();

        cbSendAFileSideSubTitle.DefaultContentID = ConfigHelper.GetValueLong("SAFSideSubTitleContentId");
        cbSendAFileSideSubTitle.CacheInterval = ConfigHelper.GetValueLong("smallCacheInterval");
        cbSendAFileSideSubTitle.Fill();

        cbSendAFileDescription.DefaultContentID = ConfigHelper.GetValueLong("SAFSideDescriptionContentId");
        cbSendAFileDescription.CacheInterval = ConfigHelper.GetValueLong("smallCacheInterval");
        cbSendAFileDescription.Fill();

        //Page.FindControl("MessageLabel").EnableViewState = true;
        //Page.FindControl("MessageLabel").Visible = false;
        
        //foreach (var crntSession in Session)
        //{
        //    Response.Write(string.Concat(crntSession, "=", Session[crntSession.ToString()]) + "<br />");
        //}

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
        
}