using System;
using Ektron.Cms.Framework.User;
using Ektron.Cms;
using Ektron.Crypto;
using System.Collections.Generic;
using SignalGraphics.CMS;
using System.Security.Cryptography;
using System.IO;
using System.Text;

public partial class AdminTool_Templates_ChangePassword : System.Web.UI.Page
{
    private UserAPI userApi = new UserAPI();

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Signal Graphics Admin tool : Change Password";
    }

    protected void btnChangePassword_Click(object sender, EventArgs e)
    {
        try
        {
            successMessagePln.Visible = false;
            successMessage.Text = " ";
            errMessage.Text = "";
            string userName = txtUserName.Text;
            var userData = GetUserDataByUserName(userName);
            if (userData != null && userData.Id > 0)
            {
                SetNewPasswordByUserName(userName, txtCurrentPassword.Text, txtNewPassword.Text);
            }
            else
            {
                errMessage.Text = "No user found with provided information";
            }
        }
        catch (Exception ex)
        {
            errMessage.Text = ex.Message;
        }
    }

    public UserData GetUserDataByUserName(string strUserName)
    {
        UserData userData = null;
        if (!string.IsNullOrEmpty(strUserName.SafeTrim()))
        {
            userData = userApi.GetUserByUsername(strUserName.SafeTrim());
        }
        return userData;
    }

    public long GetUserIdByUserName(string strUserName)
    {
        if (!string.IsNullOrEmpty(strUserName.SafeTrim()))
        {
            UserData userData = userApi.GetUserByUsername(strUserName.SafeTrim());
            if (userData != null)
                return userData.Id;
        }
        return 0;
    }

    public void SetNewPasswordByUserName(string strUserName, string strOldPassword, string strNewPassWord)
    {

        Ektron.Cms.API.User.User uApi = new Ektron.Cms.API.User.User();
        UserData udata = uApi.LogInUser(strUserName, strOldPassword, Request.ServerVariables["SERVER_NAME"].ToString());
        if (udata != null && udata.Id > 0)
        {
            if (String.Compare(strNewPassWord, txtConfirmPassword.Text) != 0)
            {
                errMessage.Text = udata.FirstName + " Your Confirmation Password doesn't match with the new password";
                return;
            }
            udata.Password = strNewPassWord;
            uApi.UpdateUser(udata);
            successMessagePln.Visible = true;
            successMessage.Text = " Password Changed Successfully";
        }
        else
        {
            errMessage.Text = udata.FirstName + " Your Current Password is in correct.";
        }
    }     

}