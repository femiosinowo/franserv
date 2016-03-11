using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SirSpeedy.CMS;

public partial class UserControls_JobSearch : System.Web.UI.UserControl
{
    public bool IsLocalSite
    { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.GetAllActiveJobs();
        }
    }

    private void GetAllActiveJobs()
    {
        List<JobPost> jobList = null;
        string fransId = FransDataManager.GetFranchiseId();

        //if (IsLocalSite)
        //    jobList = FransDataManager.GetAllJobsForLocal();
        //else
        jobList = FransDataManager.GetAllJobsForNational();

        if (jobList != null && jobList.Any())
        {
            var customList = from job in jobList
                             select new
                             {
                                 Title = job.IsPartTime ? job.Title + " - " + this.FormatJobType(job.IsPartTime, job.IsFullTime) : job.Title,
                                 Id = job.JobId,
                                 href = fransId != string.Empty ? "/" + fransId + "/job-description/?jobid=" + job.JobId : "/job-description/?jobid=" + job.JobId,
                                 ProfileType = job.ProfileType,
                                 Location = job.Location,
                                 PostedActualDate = job.DatePosted,
                                 PostedDate = job.DatePosted.ToString("MMM. dd, yyyy"),
                                 JobType = this.FormatJobType(job.IsPartTime, job.IsFullTime)
                             };

            if (customList != null)
            {
                customList = customList.OrderBy(x => x.PostedActualDate).Reverse().ToList();
            }

            lvJobs.DataSource = customList;
            lvJobs.DataBind();
        }
    }

    private string FormatJobType(bool isPartTime, bool isFullTime)
    {
        string result = "Full Time"; //default
        if ((isPartTime) && (isFullTime))
        {
            result = "Full Time/Part Time";
        }
        else if ((!isPartTime) && (isFullTime))
        {
            result = "Full Time";
        }
        else if ((isPartTime) && (!isFullTime))
        {
            result = "Part Time";
        }

        return result;
    }

}