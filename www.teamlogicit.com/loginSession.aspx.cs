using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using TeamLogic.CMS;

public partial class testlogin : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod(EnableSession = true)]
    public static void addSession(String username, String useremail)
    {
        HttpContext.Current.Session.Add("username", username);
        HttpContext.Current.Session.Add("useremail", useremail);
        HttpContext.Current.Session.Add("twitterLogin", "false");
        HttpContext.Current.Session.Add("externalLogin", "true");
    }
}