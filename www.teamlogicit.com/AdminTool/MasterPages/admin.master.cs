using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ektron.Cms;
using Ektron.Cms.API.User;
using TeamLogic.CMS;

public partial class AdminTool_admin : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAPI userApi = new UserAPI();
        if (userApi.UserId == 0 && userApi.IsLoggedIn == false)
        {
            Response.Redirect("/AdminTool/Templates/login.aspx");
        }

       long userId = userApi.UserId;
       var userData = CommunityUserHelper.GetUserByUserId(userId);
       if (userData != null && userData.Id > 0)
       {
           if (userData.IsMemberShip == false)
               uxSuperAdminLeftNav.Visible = true;
           else
               uxLocalAdminLeftNav.Visible = true;
       }
    }
}
