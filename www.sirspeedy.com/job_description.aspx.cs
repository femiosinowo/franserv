using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SirSpeedy.CMS;

public partial class job_description : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if(!string.IsNullOrEmpty(Request.QueryString["jobid"]))
            {
                long jobId;
                long.TryParse(Request.QueryString["jobid"], out jobId);
                this.GetJobDetails(jobId);                
            }
        }

        //cbHowToApplyText.DefaultContentID = ConfigHelper.GetValueLong("JobsHowToApplyContentId");
        //cbHowToApplyText.CacheInterval = ConfigHelper.GetValueLong("smallCacheInterval");
        //cbHowToApplyText.Fill();

        if (!string.IsNullOrEmpty(Request.QueryString["id"]))
        {
            long tagLineCId;
            long.TryParse(Request.QueryString["id"], out tagLineCId);
            var tablineContentData = ContentHelper.GetContentById(tagLineCId);
            if ((tablineContentData != null) &&
                (!string.IsNullOrEmpty(tablineContentData.Image)) &&
                (!tablineContentData.Image.ToLower().Contains("workarea")))
            {
                string imagePath = tablineContentData.Image;
                if (imagePath.StartsWith("/") == false)
                    imagePath = "/" + imagePath;
                subpage_tagline_wrapper.Attributes.Add("style", "background-image: url('" + imagePath + "')");
            }
        }
    }

    private void GetJobDetails(long jobId)
    {
        var jobList = FransDataManager.GetAllJobsForNational();
        if(jobList != null && jobList.Any())
        {
            var jobDetail = jobList.Where(x => x.JobId == jobId).FirstOrDefault();
            if(jobDetail != null && jobDetail.JobId > 0)
            {
                ltrJobTitle.Text = jobDetail.Title;
                ltrJobProfileType_location.Text = jobDetail.ProfileType + " / " + jobDetail.Location;
                ltrJobPostedDate.Text = jobDetail.DatePosted.ToString("MMMM dd, yyyy");
                ltrJobDescription.Text = HttpUtility.HtmlDecode(jobDetail.Description);
                GetCenterDetails(jobDetail.CenterId);

                var fransId = FransDataManager.GetFranchiseId();
                if (!string.IsNullOrEmpty(fransId))
                    applyJobLink.HRef = applyJobLink2.HRef = "/" + fransId + "/apply-to-job/?jobid=" + jobId;
                else
                    applyJobLink.HRef = applyJobLink2.HRef = "/apply-to-job/?jobid=" + jobId;
            }
        }
    }
    
    private void GetCenterDetails(string centerId)
    {
        var centerDetails = FransDataManager.GetFransData(centerId);
        if (centerDetails != null)
        {
            string address2 = centerDetails.Address2 != null && centerDetails.Address2 != string.Empty ? centerDetails.Address2 + "<br/>" : "";
            ltrCenterAddress.Text = centerDetails.Address1 + "<br />" + address2 + centerDetails.City + ", " + centerDetails.State + " " + centerDetails.Zipcode;
            if ((!string.IsNullOrEmpty(centerDetails.ContactFirstName)) && (!string.IsNullOrEmpty(centerDetails.ContactLastName)))
            {
                ltrCenterContactName.Text = centerDetails.ContactFirstName + " " + centerDetails.ContactLastName;
            }
            ltrCenterContactNumber.Text = centerDetails.PhoneNumber;
            ltrCenterEmailAdress.Text = "<a href=\"mailto:" + centerDetails.Email + "\">" + centerDetails.Email + "</a>";
            googleMapImage.Src = string.Format(FransDataManager.GoogleStaticLargeImagePath, "http://" + Request.ServerVariables["SERVER_NAME"], centerDetails.Latitude, centerDetails.Longitude);
        }
    }
}