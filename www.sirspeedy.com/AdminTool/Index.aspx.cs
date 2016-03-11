using Ektron.Cms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SirSpeedy.CMS;

public partial class AdminTool_Templates_Index : System.Web.UI.Page
{
    UserAPI userApi = new UserAPI();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (userApi.UserId > 0)
        {
            var userData =  CommunityUserHelper.GetUserByUserId(userApi.UserId);
            if (userData != null && userData.IsMemberShip == false)
            {
                pnlSuperAdmin.Visible = true;
                pnlLocalAdmin.Visible = false;
            }
            else
            {
                pnlLocalAdmin.Visible = true;
                pnlSuperAdmin.Visible = false;
            }
        }
    }
}