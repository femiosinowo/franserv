using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

public partial class testlogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod(EnableSession = true)]
    public static void addSession(String username, String useremail)
    {
		ClearUserSession();
		
        HttpContext.Current.Session.Add("username", username);
        HttpContext.Current.Session.Add("useremail", useremail);
        HttpContext.Current.Session.Add("twitterLogin", "false");
        HttpContext.Current.Session.Add("externalLogin", "true");
    }
	
	private static void ClearUserSession()
    {
        HttpContext.Current.Session.Remove("username");
        HttpContext.Current.Session.Remove("userFirstName");
        HttpContext.Current.Session.Remove("userLastName");
        HttpContext.Current.Session.Remove("useremail");
        HttpContext.Current.Session.Remove("userJobTitle");
        HttpContext.Current.Session.Remove("userCompanyName");
        HttpContext.Current.Session.Remove("userPhoneNumber");
        HttpContext.Current.Session.Remove("userCenterId");
        HttpContext.Current.Session.Remove("externalLogin");
        HttpContext.Current.Session.Remove("twitterLogin");
    }
}