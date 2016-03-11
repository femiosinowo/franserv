using Ektron.Cms;
using Ektron.Cms.Framework.User;
using SirSpeedy.CMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ektron.Crypto;

public partial class AdminTool_Templates_ForgotPassword : System.Web.UI.Page
{
    private UserAPI userApi = new UserAPI();

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "PIP Admin tool : Forgot Password";

        if (!IsPostBack)
            FillCenterDropDown();
    }

    private void FillCenterDropDown()
    {
        var allCenters = FransDataManager.GetAllFransLocationDataList(true);
        if (allCenters != null && allCenters.Any())
        {
            var currentCenterId = Request.QueryString["CenterId"] != null ? Request.QueryString["CenterId"].ToString() : "";
            //as per client hide the following location from the page
            var centerList = from c in allCenters
                             where c.FransId != currentCenterId
                             select new
                             {
                                 CenterName = c.CenterName,
                                 CenterId = c.FransId
                             };
            if (centerList != null)
            {
                centerList = centerList.OrderBy(x => x.CenterName);
                ddlCenterId.DataSource = centerList;
                ddlCenterId.DataTextField = "CenterId";
                ddlCenterId.DataValueField = "CenterId";
                ddlCenterId.DataBind();
            }
        }
        var item = new ListItem();
        item.Text = "-Select One-";
        item.Value = "-Select One-";
        item.Selected = true;
        ddlCenterId.Items.Insert(0, item);
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

    public UserData GetUserDataByUserName(string strUserName)
    {
        UserData userData = null;
        if (!string.IsNullOrEmpty(strUserName.SafeTrim()))
        {
            userData = userApi.GetUserByUsername(strUserName.SafeTrim());
        }
        return userData;
    }

    protected void btnForgotPassword_Click(object sender, EventArgs e)
    {
        lblNewReq.Text = "";
        errMessage.Text = "";
        List<Employee> userListData = AdminToolManager.GetAllLocalAdmins();
        string userName = txtUserName.Text.ToLower();
        string selectedCenter = ddlCenterId.SelectedValue.ToLower();

        var userData = GetUserDataByUserName(userName);
        //check if admin
        if (userData != null && userData.Id > 0 && userData.IsMemberShip == false)
        {
            lblNewReq.Text = "Super admin Password can't be changed in the admin tool. Please login as CMS admin in Ektron workarea and change the password. If you're admin account is locked then login as builtin account.";
            pnlNewRequest.Visible = true;
            ChangePassword.Visible = false;

            //pnlNewRequest.Visible = false;
            //ChangePassword.Visible = true;
            //return;
        }
        else if (userData != null && userData.Id > 0 && userData.IsMemberShip == true)
        {
            var userCenterData = userListData.Where(x => x.FransId.ToLower().Equals(selectedCenter) && x.UserName.ToLower().Equals(userName)).FirstOrDefault();
            if (userCenterData != null)
            {
                pnlNewRequest.Visible = false;
                ChangePassword.Visible = true;
            }
            else
            {
                lblNewReq.Text = "User Not found in the selected center. Please check the selected Center Name again.";
                pnlNewRequest.Visible = true;
                ChangePassword.Visible = false;
            }
        }
        else if (userData == null || userData.Id <= 0)
        {
            lblNewReq.Text = "User Not found in the system.";
            pnlNewRequest.Visible = true;
            ChangePassword.Visible = false;
        }

    }

    protected void btnChangePassword_Click(object sender, EventArgs e)
    {
        try
        {
            successMessagePln.Visible = false;
            successMessage.Text = "";
            SetNewPasswordByUserName(txtUserName.Text.ToLower(), txtNewPassword.Text);
        }
        catch (Exception ex)
        {
            errMessage.Text = ex.Message;
        }
    }

    public string GetPassword(long UserId)
    {
        string userPassword = "";
        UserManager userMgr = new UserManager(Ektron.Cms.Framework.ApiAccessMode.Admin);
        UserData udata = userMgr.GetItem(UserId);
        if (udata != null)
            userPassword = udata.Password;

        return userPassword;
    }

    public void SetNewPasswordByUserName(string txtUserName, string strNewPassWord)
    {
        Ektron.Cms.API.User.User userApi = new Ektron.Cms.API.User.User();
        UserData udata = userApi.GetUserByUsername(txtUserName);

        if (String.Compare(strNewPassWord, txtConfirmPassword.Text) != 0)
        {
            errMessage.Text = udata.FirstName + " Your Confirmation Password doesn't match with the new password";
            return;
        }
        udata.Password = strNewPassWord;
        userApi.UpdateUser(udata);
        successMessagePln.Visible = true;
        successMessage.Text = " Password Changed Successfully";
    }
}