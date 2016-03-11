using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TeamLogic.CMS;
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

        string eventTarget = Convert.ToString(Request.Params.Get("__EVENTTARGET"));
        string eventArgument = Convert.ToString(Request.Params.Get("__EVENTARGUMENT"));

        if (eventArgument == "onClickSubmit")
            btnSocialRegisterSubmit_Click(sender, e);

        //foreach (var crntSession in Session)
        //{
        //    Response.Write(string.Concat(crntSession, "=", Session[crntSession.ToString()]) + "<br />");
        //}

        lblUserName.Text = (Session["username"] != null) ? Session["username"].ToString() : String.Empty;
        if (Session["twitterLogin"] != null && Session["twitterLogin"].ToString().Equals("false"))
        {
            lblUserEmail.Text = (Session["useremail"] != null) ? Session["useremail"].ToString() : String.Empty;
        }
        else if (Session["twitterLogin"] != null && Session["twitterLogin"].ToString().Equals("true"))
        {
            email.Visible = true;
            lblUserEmail.Visible = false;
        }
    }

    /// <summary>
    /// on file upload event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Upload_NationalComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
    {
        Session["fileContents_" + e.FileId] = e.GetContents();
        Session["fileExtension_" + e.FileId] = e.FileName.Substring(e.FileName.LastIndexOf('.'));
        Session["fileName_" + e.FileId] = e.FileName;
    }

    /// <summary>
    /// send file button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSocialRegisterSubmit_Click(object sender, EventArgs e)
    {
        Page.Validate("SocialRegister");

        if (!Page.IsValid)
        {
            // do something when it is not valid - Print Message
        }
        else
        {
            string fullName = String.Empty;
            string email = String.Empty;
            if (Session["username"] != null)
            {
                fullName = Session["username"].ToString();
            }
            var nameArray = fullName.Split(' ');
            string firstName = String.Empty;
            string lastName = String.Empty;
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
            if (Session["useremail"] != null)
            {
                email = Session["useremail"].ToString();
            }
            string selectedCenter = txtRegisterChooseLocation.Text;
            string phoneNumber = phone.Text;
            string jobTitle = job_title.Text;
            string companyName = company.Text;
            string companyWebsite = website.Text;
            string projectName = project_name.Text;
            string projectQuantity = project_quantity.Text;
            string projectDueDate = project_due_date.Value;
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
            bool isSaved = FransDataManager.SaveSendAFileData(selectedCenter, firstName, lastName, jobTitle, companyName, email, phoneNumber, projectName, projectDescription, projectQuantity, ektronUploadedFilesIds, externalFileLinks);

            if (isSaved)
            {
                Response.Redirect("/thank-you/?type=sendafile&centerId=" + selectedCenter);
            }

        }
    }
}