using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SirSpeedy.CMS;
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
        Session["jobFileContents_" + e.FileId] = e.GetContents();
        Session["jobFileExtension_" + e.FileId] = e.FileName.Substring(e.FileName.LastIndexOf('.'));
        Session["jobFileName_" + e.FileId] = e.FileName;
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
                ltrJobProfileType_location.Text = "<span>Posted on: " + jobDetail.DatePosted.ToString("MM/dd/yyyy") + "</span>  |  <span>" + jobDetail.ProfileType + "</span>  |  <span>" + jobDetail.Location + "</span>";
                hddnCenterId.Value = jobDetail.CenterId;
                hddnJobId.Value = jobId.ToString();

                var fransId = FransDataManager.GetFranchiseId();
                if (!string.IsNullOrEmpty(fransId))
                    jobDescription.HRef = "/" + fransId + "/job-description/?jobid=" + jobId;
                else
                    jobDescription.HRef = "/job-description/?jobid=" + jobId;
            }
            else
            {
                pnlNoResult.Visible = true;
                plnJobDetails.Visible = false;
            }
        }
    }
}