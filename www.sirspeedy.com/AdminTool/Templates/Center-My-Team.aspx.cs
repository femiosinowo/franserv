using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ektron.Cms;
using SirSpeedy.CMS;
using Ektron.Cms.Instrumentation;

public partial class AdminTool_Templates_Center_My_Team : System.Web.UI.Page
{
    UserAPI userApi = new UserAPI();
    string centerId;

    protected void Page_Load(object sender, EventArgs e)
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
                    FillCenterEmployees(centerId);
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

    protected void btnMyTeam_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (hddnSelectedEmployees.Value != "")
            {
                lblError.Text = "";
                var centerId = hddnCenterId.Value;
                if (AdminToolManager.IsTeamAssigned(centerId))
                    AdminToolManager.UpdateTeamMembers(centerId, hddnSelectedEmployees.Value);
                else
                    AdminToolManager.AddTeamMembers(centerId, hddnSelectedEmployees.Value);

                FransDataManager.GetAllEmployee(centerId, true);
                FransDataManager.GetFransWorkareaData(centerId, true);

                this.FillCenterEmployees(centerId);
                pnlSaveTeamMsg.Visible = true;
            }
        }
    }

    private void FillCenterEmployees(string centerId)
    {
        var centerUsers = FransDataManager.GetAllEmployee(centerId);
        if(AdminToolManager.IsTeamAssigned(centerId))
        {
            var selectedUsers = AdminToolManager.GetSeletedCenterUsers(centerId);
            if (selectedUsers != null && selectedUsers.Any())
            {
                List<Employee> sortedList = new List<Employee>();
                //add the selected items on the top
                foreach (var e in selectedUsers)
                {
                    var cData = centerUsers.Where(x => x.EmployeeId == e.EmployeeId).FirstOrDefault();
                    if (cData != null)
                        sortedList.Add(cData);
                }
                //add non-selected items after selected items
                foreach (var e in centerUsers)
                {
                    if (!sortedList.Contains(e))
                        sortedList.Add(e);
                }


                var userList = from u in sortedList
                                  select new
                                  {
                                      AvailableTeam = selectedUsers.Exists(x => x.EmployeeId == u.EmployeeId) ? "" : "<div class=\"drag t1\"><span  id='" + u.EmployeeId + "'>" + u.FirstName + " " + u.LastName + "</span></div>",
                                      SelectedTeam = selectedUsers.Exists(x => x.EmployeeId == u.EmployeeId) ? "<div class=\"drag t1\"><span id='" + u.EmployeeId + "'>" + u.FirstName + " " + u.LastName + "</span></div>" : ""
                                  };
                lvCenterTeam.DataSource = userList;
                lvCenterTeam.DataBind();
            }
            else
            {
                var userList = from u in centerUsers
                               select new
                               {
                                   AvailableTeam = "<div class=\"drag t1\"><span  id='" + u.EmployeeId + "'>" + u.FirstName + " " + u.LastName + "</span></div>",
                                   SelectedTeam = ""
                               };
                lvCenterTeam.DataSource = userList;
                lvCenterTeam.DataBind();
            }
        }
        else
        {
            var userList = from u in centerUsers
                              select new
                              {
                                  AvailableTeam = "<div class=\"drag t1\"><span  id='" + u.EmployeeId + "'>" + u.FirstName + " " + u.LastName + "</span></div>",
                                  SelectedTeam = ""
                              };
            lvCenterTeam.DataSource = userList;
            lvCenterTeam.DataBind();
        }
    }
}