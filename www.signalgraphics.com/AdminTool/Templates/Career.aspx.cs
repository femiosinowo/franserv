using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SignalGraphics.CMS;
using Ektron.Cms;
using Ektron.Cms.Instrumentation;
using System.Xml.Linq;
using System.Xml.XPath;
using Ektron.Cms.Common;

public partial class AdminTool_Templates_Career : System.Web.UI.Page
{
    UserAPI userApi = new UserAPI();
    string centerId;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (userApi.UserId > 0)
            {
                var udata = CommunityUserHelper.GetUserByUserId(userApi.UserId);
                if (udata != null && udata.Id > 0)
                {
                    var centerUsers = AdminToolManager.GetAllLocalAdmins();
                    var userData = centerUsers.Where(x => x.UserName.ToLower() == udata.Username.ToLower()).FirstOrDefault();
                    if (userData != null)
                    {
                        centerId = userData.FransId;
                        hddnCenterId.Value = centerId;
                        //this.FillLangDDL();
                        this.FillCareerDDL();
                        this.FillDefaultJobRadioBtnList();

                        if (!string.IsNullOrEmpty(Request.QueryString["jobid"]))
                        {
                            long jobId;
                            long.TryParse(Request.QueryString["jobid"], out jobId);
                            jobPostedDate.Visible = false;
                            //populateNationalJob.Visible = false;
                            this.FillData(jobId);
                        }
                        else
                        {
                            var allCenters = FransDataManager.GetAllFransLocationDataList();
                            var centerData = allCenters.Where(x => x.FransId == centerId).FirstOrDefault();
                            if (centerData != null)
                                txtLocation.Text = centerData.City + ", " + centerData.State;
                            datePosted.Value = DateTime.Now;
                            dateExpire.Value = DateTime.Now.AddMonths(1);
                        }
                    }
                }
            }
        }
    }

    protected void btnAddJobPost_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblError.Text = "";
            JobPost data = new JobPost();
            data.Title = txtJobTitle.Text;
            data.Location = txtLocation.Text;
            data.ProfileType = ddlCareerType.SelectedValue;
            //data.Language = ddlLang.SelectedValue;
            data.IsFullTime = chkFullTime.Checked;
            data.IsPartTime = chkPartTime.Checked;
            data.Description = txtJobDescription.Text.Replace("'", "''");
            //set job posted value
            if (datePosted.Text == "")
                data.DatePosted = DateTime.Now;
            else
                data.DatePosted = datePosted.Value;

            //set job expire value
            if (dateExpire.Text == "")
                data.DateExpire = DateTime.Now.AddMonths(1);
            else
                data.DateExpire = dateExpire.Value;

            if (dateExpire.Value < datePosted.Value)
            {
                lblError.Text = "Job expired date must be greater than job posted date.";
                lblError.Visible = true;
                return;
            }


            long jobId;
            if (!string.IsNullOrEmpty(Request.QueryString["jobid"]))
            {
                long.TryParse(Request.QueryString["jobid"], out jobId);
                if (jobId > 0)
                {
                    data.JobId = jobId;
                    var status = AdminToolManager.UpdateJobPost(data);
                    if (status > 0)
                    {
                        pnlSaveMessage.Visible = true;
                        pnlJobProfile.Visible = false;
                        //refresh cache
                        FransDataManager.GetAllActiveJobs(true);
                    }
                    else
                    {
                        lblError.Text = "Sorry, an error has occured while saving the Job Post data. Please try again later";
                        lblError.Visible = true;
                    }
                }
            }
            else
            {
                data.CenterId = hddnCenterId.Value;
                var status = AdminToolManager.AddJobPostData(data);
                if (status > 0)
                {
                    pnlSaveMessage.Visible = true;
                    pnlJobProfile.Visible = false;
                    //refresh cache
                    FransDataManager.GetAllActiveJobs(true);
                }
                else
                {
                    lblError.Text = "Sorry, an error has occured while saving the Job Post data. Please try again later";
                    lblError.Visible = true;
                }
            }
        }
    }

    protected void nationalJObDesc_CheckedChanged(object sender, EventArgs e)
    {
        string selectedJobDescVal = radioButtonList.SelectedValue;
        long selectedId;
        long.TryParse(selectedJobDescVal, out selectedId);
        txtJobDescription.Text = GetNationalDefaultText(selectedId);
    }

    private void FillData(long jobId)
    {
        var allJobsList = FransDataManager.GetAllActiveJobs(true);
        if (allJobsList != null && allJobsList.Any())
        {
            var jobPost = allJobsList.Where(x => x.JobId == jobId).FirstOrDefault();
            if (jobPost != null)
            {
                txtJobTitle.Text = jobPost.Title;
                txtLocation.Text = jobPost.Location;
                if (!string.IsNullOrEmpty(jobPost.ProfileType))
                {
                    ddlCareerType.SelectedItem.Text = jobPost.ProfileType;
                    ddlCareerType.SelectedItem.Value = jobPost.ProfileType;
                }

                if (jobPost.IsFullTime)
                    chkFullTime.Checked = true;
                if (jobPost.IsPartTime)
                    chkPartTime.Checked = true;
                txtJobDescription.Text = HttpUtility.HtmlDecode(jobPost.Description.Replace("''", "'"));
                datePosted.Value = jobPost.DatePosted;
                dateExpire.Value = jobPost.DateExpire;
            }
        }
    }

    private void FillCareerDDL()
    {
        long careerTaxTreeId = ConfigHelper.GetValueLong("JobCareersTaxId");
        var taxTree = TaxonomyHelper.GetTaxonomyTree(careerTaxTreeId, 1, false);
        if (taxTree != null && taxTree.Taxonomy.Length > 0)
        {
            ListItem item = new ListItem("-Select One-", "-Select One-");
            item.Selected = true;
            ddlCareerType.Items.Add(item);

            foreach (var t in taxTree.Taxonomy)
            {
                item = new ListItem();
                item.Text = t.Name;
                item.Value = t.Name;
                ddlCareerType.Items.Add(item);
            }
        }
    }

    private void FillDefaultJobRadioBtnList()
    {
        long defaultJobFId = ConfigHelper.GetValueLong("JobDescriptionFolderId");
        var cc = new Ektron.Cms.Content.ContentCriteria();
        cc.AddFilter(ContentProperty.FolderId, CriteriaFilterOperator.EqualTo, defaultJobFId);
        var contentList = ContentHelper.GetListByCriteria(cc);
        if (contentList != null && contentList.Any())
        {
            radioButtonList.Items.Clear();
            ListItem item = null;
            foreach (var c in contentList)
            {
                item = new ListItem();
                item.Value = c.Id.ToString();
                item.Text = "<a target=\"_blank\" href=\"/admintool/templates/content.aspx?id=" + c.Id + "\">" + c.Title + "</a>";
                radioButtonList.Items.Add(item);
            }
            item = new ListItem("none", "0");
            radioButtonList.Items.Add(item);
        }
    }

    private string GetNationalDefaultText(long cId)
    {
        string html = string.Empty;
        if (cId > 0)
        {
            var cData = ContentHelper.GetContentById(cId);
            if (cData != null && cData.Html != string.Empty)
            {
                html = cData.Html;
            }
        }
        return html;
    }

    //private void FillLangDDL()
    //{
    //    var langListData = AdminToolManager.GetAllCenterLanguages();
    //    if (langListData != null && langListData.Any())
    //    {
    //        ListItem item = new ListItem("-Select One-", "-Select One-");
    //        item.Selected = true;
    //        ddlLang.Items.Add(item);

    //        foreach (var l in langListData)
    //        {
    //            item = new ListItem();
    //            item.Text = l.LangName;
    //            item.Value = l.LangName;
    //            ddlLang.Items.Add(item);
    //        }
    //    }
    //}

}