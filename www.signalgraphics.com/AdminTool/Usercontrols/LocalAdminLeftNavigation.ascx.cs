using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ektron.Cms;
using SignalGraphics.CMS;

public partial class AdminTool_Usercontrols_LeftNavigation : System.Web.UI.UserControl
{
    UserAPI userApi = new UserAPI();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (userApi.UserId > 0)
        {
            var udata = CommunityUserHelper.GetUserByUserId(userApi.UserId);
            if (udata != null && udata.Id > 0)
            {
                if (udata.IsMemberShip == true)
                {
                    var centerUsers = AdminToolManager.GetAllLocalAdmins();
                    var userData = centerUsers.Where(x => x.UserName.ToLower() == udata.Username.ToLower()).FirstOrDefault();
                    if (userData != null)
                    {
                        lblRole.Text = userData.Roles;
                        centerManageLinks.Visible = true;
                        this.CheckPermissions(udata.Username.ToLower(), userData.Roles);
                    }
                    else
                    {
                        Response.Redirect("/AdminTool/templates/logout.aspx?logout=1");
                    }
                }
            }
        }
    }
    
     private void CheckPermissions(string userName, string role)
     {
        var permissionsSet = AdminToolManager.GetUserPermission(role, userName);
        if (permissionsSet != null)
        {
            if (permissionsSet.Manage_Center_Information)
                centerInfo.Visible = true;
            if (permissionsSet.Manage_Why_weare_diff)
                whyWeAreDiff.Visible = true;
            if (permissionsSet.Manage_Promotions)
                promotions.Visible = true;
            if (permissionsSet.Manage_Partners)
                partners.Visible = true;
            if (permissionsSet.Manage_Testimonials)
                testimonials.Visible = true;
            if (permissionsSet.Manage_Careers)
                careers.Visible = true;
            if (permissionsSet.Manage_Local_Team)
                myTeam.Visible = true;
            if (permissionsSet.Manage_User_Profiles)
                allProfiles.Visible = true;
            if (permissionsSet.Manage_Sent_Files)
                sendAFile.Visible = true;
            if (permissionsSet.Manage_Request_A_Quotes)
                requestAQuote.Visible = true;
        }
        else
        {
           Response.Redirect("/AdminTool/templates/logout.aspx?logout=1");
        }
    }    
}