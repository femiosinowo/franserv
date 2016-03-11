using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SirSpeedy.CMS;
using System.Configuration;
using Ektron.Cms.Framework.Content;
using Ektron.Cms.Content;
using Ektron.Cms;
using Ektron.Cms.Common;
using System.Xml.Linq;
using System.Xml.XPath;
using Ektron.Cms.Instrumentation;
using FlickrNet;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Web.Script.Serialization;
using System.Data;
using Ektron.Cms.User;


public partial class test : System.Web.UI.Page
{
    Ektron.Cms.Framework.User.UserManager uMngr = new Ektron.Cms.Framework.User.UserManager(Ektron.Cms.Framework.ApiAccessMode.Admin);
    Ektron.Cms.API.User.User uApi = new Ektron.Cms.API.User.User();
    private static string adminToolConnectionString = ConfigurationManager.ConnectionStrings["SirSpeedyAdminTool.DbConnection"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void btnResetPassword_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblError1.Text = "";
            lblStatus.Text = "";
            string userName = txtUsername.Text.Trim();
            var uc = new Ektron.Cms.User.UserCriteria();
            uc.AddFilter(UserProperty.UserName, CriteriaFilterOperator.EqualTo, userName);
            var userList = uMngr.GetList(uc);

            if (userList != null && userList.Any())
            {
                var userData = userList[0];
                //un-lock user
                uMngr.UnlockUser(userData.Id);
                //reset password
                var newPassword = uMngr.ResetPassword(userName);
                lblStatus.Text = "Your account is un-locked and check your email for temp password.";
                pnl1.Visible = false;
                pnl2.Visible = false;
                pnl3.Visible = false;
                pnl4.Visible = true;
            }
            else
            {
                lblError1.Text = "No User found with supplied user name.";
            }
        }
    }
    
    protected void btnUnlockAccount_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblError2.Text = "";
            lblStatus.Text = "";
            string userName = txtUsername.Text.Trim();
            var uc = new Ektron.Cms.User.UserCriteria();
            uc.AddFilter(UserProperty.UserName, CriteriaFilterOperator.EqualTo, userName);
            var userList = uMngr.GetList(uc);

            if (userList != null && userList.Any())
            {
                var userData = userList[0];
                //un-lock user
                uMngr.UnlockUser(userData.Id);
                lblStatus.Text = "Your account is unlocked successfully.";
                pnl1.Visible = false;
                pnl2.Visible = false;
                pnl3.Visible = false;
                pnl4.Visible = true;
            }
            else
            {
                lblError2.Text = "No User found with supplied user name.";
            }
        }
    }

    protected void btnChangePassword_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblError3.Text = "";
            lblStatus.Text = "";
            string userName = txtUsername.Text.Trim();
            string oldPassword = txtCurrentPassword.Text;
            string newPassword = txtNewPassword.Text;   

            var uc = new Ektron.Cms.User.UserCriteria();
            uc.AddFilter(UserProperty.UserName, CriteriaFilterOperator.EqualTo, userName);
            var userList = uMngr.GetList(uc);

            if (userList != null && userList.Any())
            {
                var userData = userList[0];                             
                UserData udata = uApi.LogInUser(userName, oldPassword, Request.ServerVariables["SERVER_NAME"].ToString());
                if (udata != null && udata.Id > 0)
                {
                    //un-lock user
                    uMngr.UnlockUser(userData.Id);
                    //update password
                    udata.Password = newPassword;
                    uApi.UpdateUser(udata);
                    lblStatus.Text = "Your account is unlocked and password is changed successfully.";
                    pnl1.Visible = false;
                    pnl2.Visible = false;
                    pnl3.Visible = false;
                    pnl4.Visible = true;
                }
                else
                {
                    lblError3.Text = "Your Current Password is in correct.";
                }
            }
            else
            {
                lblError3.Text = "No User found with supplied user name.";
            }
        }
    }
}