using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TeamLogic.CMS;

public partial class UserControls_JobSearch : System.Web.UI.UserControl
{
    public bool IsLocalSite
    { get; set; }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            cbHowToApplyText.DefaultContentID = ConfigHelper.GetValueLong("JobsHowToApplyContentId");
            cbHowToApplyText.CacheInterval = ConfigHelper.GetValueLong("smallCacheInterval");
            cbHowToApplyText.Fill();

            this.GetAllActiveJobs();
        }
    }

    protected void ListPager_PreRender(object sender, EventArgs e)
    {
        var searchKeyword = txtkeyword.Text.Trim();
        var searchLocation = txtLocation.Text.Trim();
        this.GetAllActiveJobs(searchKeyword, searchLocation);
    }   

    protected void btnSearchKeyword_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            var searchKeyword = txtkeyword.Text.Trim();
            var searchLocation = txtLocation.Text.Trim();
            this.GetAllActiveJobs(searchKeyword, searchLocation);
            if (Request.Url.AbsolutePath.IndexOf("search_careers") <= -1)
                System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "appendLocationId", "<script type=\"text/javascript\">window.location.href = window.location.href + \"#search_careers\";</script>", false);
        }
    }

    protected void btnSearchLocation_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            var searchKeyword = txtkeyword.Text.Trim();
            var searchLocation = txtLocation.Text.Trim();
            this.GetAllActiveJobs(searchKeyword, searchLocation);
            if (Request.Url.AbsolutePath.IndexOf("search_careers") <= -1)
                System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "appendLocationId", "<script type=\"text/javascript\">window.location.href = window.location.href + \"#search_careers\";</script>", false);
        }
    }

    private void GetAllActiveJobs(string keyword = "", string location = "")
    {
        List<JobPost> jobList = null;

        //if (IsLocalSite)
        //    jobList = FransDataManager.GetAllJobsForLocal();
        //else
        jobList = FransDataManager.GetAllJobsForNational();

        string jobUrl = "/apply-to-job/";
        if (FransDataManager.IsFranchiseSelected())
            jobUrl = "/" + FransDataManager.GetFranchiseId() + jobUrl;
            

        if (jobList != null && jobList.Any())
        {
            var customList = from job in jobList
                              select new
                              {
                                  Title = job.IsPartTime ? job.Title + " - " + this.FormatJobType(job.IsPartTime, job.IsFullTime) : job.Title,
                                  Id = job.JobId,
                                  ProfileType = job.ProfileType,
                                  JobURL = jobUrl + "?jobid=" + job.JobId,
                                  Location = job.Location,
                                  PostedActualDate = job.DatePosted,
                                  PostedDate = job.DatePosted.ToString("MMM. dd, yyyy"),
                                  JobType = this.FormatJobType(job.IsPartTime, job.IsFullTime),
                                  JobDescription = HttpUtility.HtmlDecode(job.Description),
                                  ContactFirstName = FransDataManager.GetFransData(job.CenterId) != null ? FransDataManager.GetFransData(job.CenterId).ContactFirstName : "",
                                  ContactLastName = FransDataManager.GetFransData(job.CenterId) != null ? FransDataManager.GetFransData(job.CenterId).ContactLastName : "",
                                  WorkPhoneNumber = FransDataManager.GetFransData(job.CenterId) != null ? FransDataManager.GetFransData(job.CenterId).PhoneNumber : "",
                                  AddressLine1 = FransDataManager.GetFransData(job.CenterId) != null ? FransDataManager.GetFransData(job.CenterId).Address1 : "",
                                  City = FransDataManager.GetFransData(job.CenterId) != null ? FransDataManager.GetFransData(job.CenterId).City : "",
                                  State = FransDataManager.GetFransData(job.CenterId) != null ? FransDataManager.GetFransData(job.CenterId).State : "",
                                  ZipCode = FransDataManager.GetFransData(job.CenterId) != null ? FransDataManager.GetFransData(job.CenterId).Zipcode : "",
                                  GoogleMapImage = string.Format(FransDataManager.GoogleStaticLargeImagePath, "http://" + Request.ServerVariables["SERVER_NAME"], GetCenterLatitude(FransDataManager.GetFransData(job.CenterId)), GetCenterLongitude(FransDataManager.GetFransData(job.CenterId))),
                                  HowToAppyText = this.GetHowTOApplyText()
                              };

            if (keyword != "" && location != "")
            {
                keyword = keyword.ToLower();
                location = location.ToLower();
                customList = customList.Where(x => x.Title.ToLower().IndexOf(keyword) > -1 ||
                                              x.JobDescription.ToLower().IndexOf(keyword) > -1 ||
                                              x.ProfileType.ToLower().IndexOf(keyword) > -1 ||
                                              x.JobType.ToLower().IndexOf(keyword) > -1 ||
                                              x.Location.ToLower().IndexOf(keyword) > -1)
                                              .Where(x => x.Location.ToLower().IndexOf(location) > -1).ToList();
            }
            else if (keyword != "")
            {
                keyword = keyword.ToLower();
                customList = customList.Where(x => x.Title.ToLower().IndexOf(keyword) > -1 ||
                                              x.JobDescription.ToLower().IndexOf(keyword) > -1 ||
                                              x.ProfileType.ToLower().IndexOf(keyword) > -1 ||
                                              x.JobType.ToLower().IndexOf(keyword) > -1 ||
                                              x.Location.ToLower().IndexOf(keyword) > -1).ToList();
            }
            else if (location != "")
            {
                location = location.ToLower();
                customList = customList.Where(x => x.Location.ToLower().IndexOf(location) > -1).ToList();
            }

            if (customList != null)
            {
                customList = customList.OrderByDescending(x => x.PostedActualDate).ToList();
            }

            lvJobs.DataSource = customList.ToArray();
            lvJobs.DataBind();
        }
    }

    private string FormatJobType(bool isPartTime, bool isFullTime)
    {
        string result = "Full Time"; //default
        if((isPartTime) && (isFullTime))
        {
            result = "Full Time/Part Time";
        }
        else if((!isPartTime) && (isFullTime))
        {
            result = "Full Time";
        }
        else if((isPartTime) && (!isFullTime))
        {
            result = "Part Time";
        }

        return result;
    }

    private string GetHowTOApplyText()
    {
        if(cbHowToApplyText != null && cbHowToApplyText.EkItem != null)
        {
            return cbHowToApplyText.EkItem.Html;
        }
        return "";
    }
      
    private string GetCenterLatitude(FransData fransData)
    {
        string data = "";
        {
            if (fransData != null && fransData.Latitude != null)
                data = fransData.Latitude;
        }
        return data;
    }

    private string GetCenterLongitude(FransData fransData)
    {
        string data = "";
        {
            if (fransData != null && fransData.Longitude != null)
                data = fransData.Longitude;
        }
        return data;
    }
}