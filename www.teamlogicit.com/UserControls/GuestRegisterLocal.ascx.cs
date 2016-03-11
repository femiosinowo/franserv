using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TeamLogic.CMS;
using System.Text;
using Ektron.Cms.Framework.Content;
using System.IO;
using Ektron.Cms.Content;
using Ektron.Cms.Instrumentation;
using System.Configuration;

public partial class UserControls_GuestRegisterLocal : System.Web.UI.UserControl
{
    public string BoxClientId = "'" + ConfigurationManager.AppSettings["BoxAPIClientId"] + "'";
    public string BoxSecretSecret = "'" + ConfigurationManager.AppSettings["BoxAPIClientSecret"] + "'";
    public string GoogleAPIClientId = "'" + ConfigurationManager.AppSettings["GoogleAPIClientId"] + "'";
    public string GoogleDriveAPIDeveloperKey = "'" + ConfigurationManager.AppSettings["GoogleDriveAPIDeveloperKey"] + "'";

    protected void Page_Load(object sender, EventArgs e)
    {
        string eventTarget = Convert.ToString(Request.Params.Get("__EVENTTARGET"));
        string eventArgument = Convert.ToString(Request.Params.Get("__EVENTARGUMENT"));

        if (eventArgument == "onClickSubmit")
            btnGuestRegisterSubmit_Click(sender, e);

        if (!Page.IsPostBack)
        {
            this.GetFranchiseDetails();
            this.GetDirectionsForGoogleMap();

        }
    }

    /// <summary>
    /// send file button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGuestRegisterSubmit_Click(object sender, EventArgs e)
    {
        Page.Validate("GuestRegister");

        if (!Page.IsValid)
        {
            // do something when it is not valid - Print Message
        }
        else
        {
            string firstName = fname.Text;
            string lastName = lname.Text;
            string emailAddress = email.Text;
            string selectedCenter = FransDataManager.GetFranchiseId();//txtRegisterChooseLocation.Text;
            string phoneNumber = phone.Text;
            string jobTitle = job_title.Text;
            string companyName = company.Text;
            string projectName = project_name.Text;
            string projectQuantity = project_quantity.Text;
            string projectDueDate = project_due_date.Text;
            string projectDescription = project_desc.Value;
            string ektronUploadedFilesIds = string.Empty;
            string externalFileLinks = string.Empty;

            if (!string.IsNullOrEmpty(hdnFileLinks.Value))
            {
                externalFileLinks = hdnFileLinks.Value;
            }

            if (!string.IsNullOrEmpty(hdnFileIDs.Value))
            {
                try
                {
                    long uploadedFolderId = ConfigHelper.GetValueLong("UploadedFilesFolderId");
                    AssetManager assetManager = new AssetManager(Ektron.Cms.Framework.ApiAccessMode.Admin);
                    string strFileIDs = hdnFileIDs.Value;
                    string[] arrFileIDs = strFileIDs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (arrFileIDs != null && arrFileIDs.Length > 0)
                    {
                        foreach (string strFileID in arrFileIDs)
                        {
                            var fileData = (byte[])Session["fileContents_" + strFileID];
                            var fileName = Session["fileName_" + strFileID] as string;
                            var fileExtension = (string)Session["fileExtension_" + strFileID];

                            if (fileData != null && fileExtension != null && fileName != null)
                            {
                                string title = fileName.Remove(fileName.IndexOf(fileExtension), fileExtension.Length);
                                ContentAssetData contentAssetData = new ContentAssetData()
                                {
                                    FolderId = uploadedFolderId,
                                    Title = title,
                                    Teaser = "",
                                    File = fileData,
                                    LanguageId = 1033,
                                    AssetData = new Ektron.Cms.Common.AssetData()
                                    {
                                        FileName = Path.GetFileName(fileName)
                                    }
                                };

                                var contentData = assetManager.Add(contentAssetData);
                                if (contentData != null)
                                    ektronUploadedFilesIds += contentData.Id + ",";
                                Session.Remove("fileContents_" + strFileID);
                                Session.Remove("fileExtension_" + strFileID);
                                Session.Remove("fileName_" + strFileID);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }

            }
            //save data to custom db
            bool isSaved = FransDataManager.SaveSendAFileData(selectedCenter, firstName, lastName, jobTitle, companyName, emailAddress, phoneNumber, projectName, projectDescription, projectQuantity, ektronUploadedFilesIds, externalFileLinks);

            if (isSaved)
            {
                Response.Redirect("/thank-you/?type=sendafile&centerId=" + selectedCenter);
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
                contactinfo.Append("<li class='contact-icon-email'><span><a href='" + fd.Email + "'>" + fd.Email + "</a></span></li>");

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
        Session["fileContents_" + e.FileId] = e.GetContents();
        Session["fileExtension_" + e.FileId] = e.FileName.Substring(e.FileName.LastIndexOf('.'));
        Session["fileName_" + e.FileId] = e.FileName;
    }
}