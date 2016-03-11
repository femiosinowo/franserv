using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TeamLogic.CMS;
using Ektron.Cms.Instrumentation;
using Ektron.Cms.Content;
using System.IO;
using Ektron.Cms.Framework.Content;
using System.Text;

public partial class apply_to_job : PageBase
{
    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["jobid"]))
            {
                long jobId;
                long.TryParse(Request.QueryString["jobid"], out jobId);
                this.GetJobDetails(jobId);
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
        //Session["jobFileContents_" + e.FileId] = e.GetContents();
        //Session["jobFileExtension_" + e.FileId] = e.FileName.Substring(e.FileName.LastIndexOf('.'));
        Session["jobFileName_" + e.FileId] = e.FileName;
        ajaxFileUploadResume.SaveAs(Server.MapPath("/resumes/") + e.FileName);
    }

    /// <summary>
    /// send application button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if(Page.IsValid)
        {
            long jobId;
            long.TryParse(Request.QueryString["jobid"], out jobId);
            string ektronUploadedFilesIds = string.Empty;
            if (!string.IsNullOrEmpty(hdnResumeIDs.Value))
            {
                string resumeFilePath = "";
                try
                {                    
                    string strFileIDs = hdnResumeIDs.Value;
                    string[] arrFileIDs = strFileIDs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (arrFileIDs != null && arrFileIDs.Length > 0)
                    {
                        foreach (string strFileID in arrFileIDs)
                        {
                            //var fileData = (byte[])Session["jobFileContents_" + strFileID];
                            var fileName = Session["jobFileName_" + strFileID] as string;
                            //var fileExtension = Session["jobFileExtension_" + strFileID] as string;

                            if (fileName != null)
                            {
                                //string title = fileName.Remove(fileName.IndexOf(fileExtension), fileExtension.Length);
                                //ContentAssetData contentAssetData = new ContentAssetData()
                                //{
                                //    FolderId = uploadedFolderId,
                                //    Title = title,
                                //    Teaser = "",
                                //    File = fileData,
                                //    LanguageId = 1033,
                                //    AssetData = new Ektron.Cms.Common.AssetData()
                                //    {
                                //        FileName = Path.GetFileName(fileName)
                                //    }
                                //};

                                //var contentData = assetManager.Add(contentAssetData);
                                //if (contentData != null)
                                //    ektronUploadedFilesIds += contentData.Id + ",";

                                //Session.Remove("jobFileContents_" + strFileID);
                                //Session.Remove("jobFileExtension_" + strFileID);
                                string domainName = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
                                resumeFilePath = "http://" + domainName + "/resumes/" + fileName.Replace(" ", "%20");
                                Session.Remove("jobFileName_" + strFileID);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }

                var fransId = FransDataManager.GetFranchiseId();
                string jobUrl = Request.Url.AbsoluteUri;                

                //save data to custom db
                FransDataManager.SaveJobApplicationData(jobId, hddnCenterId.Value, txtFirstName.Text, txtLastName.Text, txtCity.Text,
                    txtStateName.Text, txtZip.Text, txtEmail.Text, resumeFilePath, coverNotes.InnerText, jobUrl, resumeFilePath);
                
                if (!string.IsNullOrEmpty(fransId))
                    Response.Redirect("/" + fransId + "/thank-you/?type=jobapp&centerId=" + hddnCenterId.Value);
                else
                    Response.Redirect("/thank-you/?type=jobapp&centerId=" + hddnCenterId.Value);
            }
        }
    }

    private void GetJobDetails(long jobId)
    {
        var jobList = FransDataManager.GetAllJobsForNational();
        if (jobList != null && jobList.Any())
        {
            var jobDetail = jobList.Where(x => x.JobId == jobId).FirstOrDefault();
            if (jobDetail != null && jobDetail.JobId > 0)
            {
                pnlNoResult.Visible = false;
                plnJobDetails.Visible = true;
                ltrJobTitle.Text = jobDetail.Title;
                ltrJobProfileType_location.Text = jobDetail.ProfileType + " / " + jobDetail.Location;
                hddnCenterId.Value = jobDetail.CenterId;
            }
            else
            {
                pnlNoResult.Visible = true;
                plnJobDetails.Visible = false;
            }
        }
    }
}