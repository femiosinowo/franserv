using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TeamLogic.CMS;

public partial class UserControls_newJobSearch : System.Web.UI.UserControl
{
    public bool IsLocalSite
    { get; set; }
    
    public bool DisplayLocalJobs { get; set; }
    protected string _TableID;
    protected string _domModel;
    protected void Page_Load(object sender, EventArgs e)
    {
        _TableID = DisplayLocalJobs ? "center-jobs1" : "center-jobs";
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

 

    private void GetAllActiveJobs(string keyword = "", string location = "")
    {
        string centerid = String.Empty;
        List<JobPost> jobList = null;

        //if (IsLocalSite)
        //    jobList = FransDataManager.GetAllJobsForLocal();
        //else
        jobList = FransDataManager.GetAllJobsForNational();

        string jobUrl = "/apply-to-job/";
        if (FransDataManager.IsFranchiseSelected())
        {
            centerid = FransDataManager.GetFranchiseId();
            jobUrl = "/" + centerid + jobUrl;
        }

        if (jobList != null && jobList.Any() && jobList.Count >0)
        
        {
            var customList = from job in jobList
                              select new
                              {
                                  Title = job.IsPartTime ? job.Title + " - " + this.FormatJobType(job.IsPartTime, job.IsFullTime) : job.Title,
                                  Id = job.JobId,
                                  ProfileType = job.ProfileType,
                                  JobURL = jobUrl + "?jobid=" + job.JobId,
                                  Location = job.Location,
                                  CenterId=job.CenterId,
                                  PostedActualDate = job.DatePosted,
                                  PostedDate = job.DatePosted.ToString("MMM dd, yyyy"),
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
                if (DisplayLocalJobs)
                {
                    customList =
                        customList.Where(x => x.CenterId.ToLower().Equals(centerid.ToLower()))
                            .OrderByDescending(x => x.PostedActualDate)
                            .ToList();
                    FransData franchisedata = FransDataManager.GetFransData(centerid);
                    if (franchisedata != null)
                        JobLocationLiteral1.Text = String.Format("<font color=\"#008752\">{0}, {1}</font>", franchisedata.City, franchisedata.State);

                    txtLocation.Visible = false;
                    txtkeyword.Visible = false;
                    btnSearchKeyword.Visible = false;
                    btnSearchLocation.Visible = false;
                    _domModel = "<\"top\">rt<\"bottom\"><\"clear\">";

                }
                else
                {
                    customList = customList.OrderByDescending(x => x.PostedActualDate).ToList();
                    JobLocationLiteral1.Text = "<font color=\"#008752\">ALL</font>";
                    _domModel = "<\"top\">rt<\"bottom\"p><\"clear\">";
                }
                CareersHeaderLabel1.Text = String.Format("{0} Careers", JobLocationLiteral1.Text);
            }

            var listarray = customList.ToArray();
            if (listarray.Any())
            {
                lvJobs.DataSource = listarray;
                lvJobs.DataBind();
            }
            else
            {
                this.Visible = false; //prevent control from displaying if there are no jobs.
            }
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