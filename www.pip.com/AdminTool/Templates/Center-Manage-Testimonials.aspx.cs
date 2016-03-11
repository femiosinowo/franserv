using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SirSpeedy.CMS;
using Ektron.Cms;
using Ektron.Cms.Instrumentation;

public partial class AdminTool_Templates_Center_Manage_Testimonials : System.Web.UI.Page
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
                    var centerUsers = AdminToolManager.GetAllUsers();
                    var userData = centerUsers.Where(x => x.UserName.ToLower() == udata.Username.ToLower()).FirstOrDefault();
                    if (userData != null)
                    {
                        centerId = userData.FransId;
                        hddnCenterId.Value = centerId;
                        this.BindGrid(centerId);
                    }
                }
            }
        }

        if (!string.IsNullOrEmpty(hddnCenterId.Value))
        {
            var centerData = FransDataManager.GetFransData(hddnCenterId.Value);
            if (centerData != null)
            {
                centerInfo.Visible = true;
                lblCenterName.Text = centerData.CenterName;
                lblCenterId.Text = centerData.FransId;
            }
        }
    }

    //protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    GridView1.PageIndex = e.NewPageIndex;
    //    this.BindGrid(hddnCenterId.Value);
    //}

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            var index = e.RowIndex;
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
            var tId = row.Cells[5].Text;

            if (tId != string.Empty)
            {
                int id;
                int.TryParse(tId, out id);
                var status = AdminToolManager.DeleteTestimonial(id);               
                this.BindGrid(hddnCenterId.Value);
            }
        }
        catch (Exception ex)
        {
            Log.WriteError(ex);
        }
    }

    private void BindGrid(string centerId)
    {
        var allTestimonialsList = FransDataManager.GetAllTestimonials(centerId, true);
        if (allTestimonialsList != null && allTestimonialsList.Any())
        {
            GridView1.DataSource = allTestimonialsList;
            GridView1.DataBind();
        }
    }

}