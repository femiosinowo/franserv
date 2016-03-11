using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ektron.Cms;
using Ektron.Cms.API.User;

using SignalGraphics.CMS;

public partial class AdminTool_admin : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAPI userApi = new UserAPI();
        if (userApi.UserId == 0 && userApi.IsLoggedIn == false)
        {
            Response.Redirect("/AdminTool/Templates/login.aspx");
        }
    }
}
