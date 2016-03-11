using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SignalGraphics.CMS;
using Ektron.Cms;
using Ektron.Cms.Instrumentation;

public partial class AdminTool_Templates_Manage_Careers : System.Web.UI.Page
{    
    string centerId;
    UserAPI userApi = new UserAPI();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var udata = CommunityUserHelper.GetUserByUserId(userApi.UserId);
            if (udata != null && udata.Id > 0)
            {
                var centerUsers = AdminToolManager.GetAllLocalAdmins();
                var userData = centerUsers.Where(x => x.UserName.ToLower() == udata.Username.ToLower()).FirstOrDefault();
                if (userData != null)
                {
                    centerId = userData.FransId;
                    hdnCenterId.Value = centerId;
                    this.BindGrid(centerId);
                }
            }                      
        }

        if (!string.IsNullOrEmpty(hdnCenterId.Value))
        {
            var centerData = FransDataManager.GetFransData(hdnCenterId.Value);
            if (centerData != null)
            {
                centerInfo.Visible = true;
                lblCenterName.Text = centerData.CenterName;
                lblCenterId.Text = centerData.FransId;
            }
        }
    }
       
    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        this.BindGrid(hdnCenterId.Value);
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            var index = e.RowIndex;
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
            var jobId = row.Cells[7].Text;

            if (jobId != string.Empty)
            {
                long id;
                long.TryParse(jobId, out id);
                var status = AdminToolManager.DeleteJobPostData(id);
                this.BindGrid(hdnCenterId.Value);
            }
        }
        catch (Exception ex)
        {
            Log.WriteError(ex);
        }
    }

    private void BindGrid(string centerId)
    {
        var jobListData = AdminToolManager.GetAllJobs();
        if (jobListData != null && jobListData.Any() && !string.IsNullOrEmpty(centerId))
        {
            var centerjobList = jobListData.Where(x => x.CenterId == centerId).ToList();
            GridView1.DataSource = centerjobList;
            GridView1.DataBind();
        }
    }
    
}