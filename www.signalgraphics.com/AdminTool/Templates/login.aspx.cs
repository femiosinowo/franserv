using System;
using System.Web.UI;
using Ektron.Cms;
using Ektron.Cms.Instrumentation;
using System.Linq;
using SignalGraphics.CMS;


public partial class AdminTool_login : System.Web.UI.Page
{
    UserAPI userApi = new UserAPI();
    Ektron.Cms.API.User.User uApi = new Ektron.Cms.API.User.User();

    protected void Page_Load(object sender, EventArgs e)
    {
	    Page.Title = "Signal Graphics Admin tool : Login";
        if (!Page.IsPostBack)
        {
            if (userApi.IsLoggedIn && userApi.UserId > 0)
            {
                Response.Redirect("/AdminTool/index.aspx");
            }
        }
    }

    protected void btnLoginUser_Click(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            string userName = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();

            var udata = CommunityUserHelper.GetUserByUserName(userName);
            if (udata != null && udata.Id > 0)
            {
                udata = userApi.logInUser(userName, password, Request.ServerVariables["SERVER_NAME"], "", "");
                if (udata.Id == 0)
                {
                    lblError.Visible = true;
                    lblError.Text = "Login failed. Please Check Your Username and Password";
                }
                else
                {
                    userApi.SetAuthenticationCookie(udata);

                    if (!uApi.IsAdmin())
                        SetPermissions(udata.Username);
                    Response.Redirect("/AdminTool/index.aspx", true);
                }
            }
            else
            {
                lblError.Text = "Error: No user found with provided credentials";
            }
        }
        catch (Exception ex)
        {
            Log.WriteError(ex);
            lblError.Text = ex.Message;
            lblError.Visible = true;
        }
    }

    private void SetPermissions(string userName)
    {
        var centerUsers = AdminToolManager.GetAllUsers();
        var userData = centerUsers.Where(x => x.UserName.ToLower() == userName.Trim().ToLower()).FirstOrDefault();
        if (userData != null)
        {
            AdminToolManager.GetUserPermission(userData.Roles, userName, true);
        }
    }
}