using Ektron.Cms.Instrumentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminTool_Templates_logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       if(Request.QueryString.HasKeys())
        {
            if(!string.IsNullOrEmpty(Request.QueryString["logout"]))
            {
                LogOut();
            }
        }
    }

    protected void btnLogOut_Click(object sender, EventArgs e)
    {
        LogOut();
    }
	
	private void LogOut()
    {
        Ektron.Cms.UserData udata = new Ektron.Cms.UserData();
        try
        {
            Ektron.Cms.CommonApi appUI = new Ektron.Cms.CommonApi();
            Ektron.Cms.User.EkUser userObj = appUI.EkUserRef;
            bool bRet;
            bRet = userObj.LogOutUser(appUI.UserId, appUI.GetCookieValue("site_id"));
            if (!bRet)
            {
                HttpCookie cookEcm = Response.Cookies.Get("ecm");
                cookEcm.Expires = DateTime.Now;
                Response.Cookies.Add(cookEcm);
                HttpCookie cookEcmGUID = Response.Cookies.Get("EktGUID");
                cookEcmGUID.Expires = DateTime.Now;
                Response.Cookies.Add(cookEcmGUID);
                Response.Redirect("/AdminTool/templates/login.aspx");
            }
        }
        catch (Exception ex)
        {
            Log.WriteError(ex);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("/AdminTool/index.aspx");
    }

}