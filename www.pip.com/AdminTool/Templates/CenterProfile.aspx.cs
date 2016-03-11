using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SirSpeedy.CMS;
using Ektron.Cms.Instrumentation;
using System.Text;
using Ektron.Cms.Framework.Content;
using Ektron.Cms;
using System.IO;


public partial class AdminTool_Templates_Profile : System.Web.UI.Page
{
    UserAPI userApi = new UserAPI();
    string centerId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            if(Request.QueryString.HasKeys())
            {
                if(!string.IsNullOrEmpty(Request.QueryString["userid"]))
                {
                    long userId;
                    long.TryParse(Request.QueryString["userid"], out userId);
                    btnAddProfile.Visible = false;
                    btnUpdateProfile.Visible = true;
                    userName.Visible = false;
                    this.FillData(userId);
                }
                else
                {
                    btnUpdateProfile.Visible = false;
                    ddlRole.SelectedIndex = 0;
                }
            }
            else
            {
                btnUpdateProfile.Visible = false;
                ddlRole.SelectedIndex = 0;
            }

            if (userApi.UserId > 0)
            {
                var udata = CommunityUserHelper.GetUserByUserId(userApi.UserId);
                if (udata != null && udata.Id > 0)
                {
                    var centerUsers = AdminToolManager.GetAllLocalAdmins();
                    var userData = centerUsers.Where(x => x.UserName == udata.Username).FirstOrDefault();
                    if (userData != null)
                    {
                        centerId = userData.FransId;
                        hddnCenterId.Value = centerId;
                    }
                }
            }
        }

        if (!string.IsNullOrEmpty(hddnCenterId.Value))
        {
            var centerData = FransDataManager.GetFransData(hddnCenterId.Value);
            if (centerData != null)
            {
                centerInfo.Visible = true;
                lblCenterName.Text = centerData.CenterName;
                lblCenterId.Text = centerData.FransId;
            }
        }
    }

    protected void btnAddProfile_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                lblError.Text = "";
                if (AdminToolManager.IsUserNameExist(txtUserName.Text.Trim()) || CommunityUserHelper.IsCmsUserExist(txtUserName.Text.Trim()))
                {
                    lblError.Text = "A User already Exist with the selected UserName. Please select a different UserName.";
                    lblError.Visible = true;
                    return;
                }

                string profileImgPath = "";
                if (imageUpload.HasFiles)
                    profileImgPath = GetCmsImagePath();
                string encryptedPassword = Encryptdata(txtPassword.Text);
                string selectedCenter = hddnCenterId.Value;
                string role = ddlRole.SelectedItem.Value;

                if (role == "Center Manager" || role == "Center Owner")
                {
                    var selectedCenterData = FransDataManager.GetFransData(selectedCenter);
                    long cmsUserId = CommunityUserHelper.AddCmsMembershipUser(txtFirstName.Text, txtLastName.Text, txtUserName.Text, txtPassword.Text, txtEmail.Text, selectedCenter);
                    if (cmsUserId > 0)
                    {
                        CommunityUserHelper.AddUserToCommunityGroup(cmsUserId, selectedCenterData.CmsCommunityGroupId, selectedCenterData.FransId, selectedCenterData.PhoneNumber);
                        var status = AdminToolManager.AddUserToCenter(selectedCenter, txtFirstName.Text, txtLastName.Text, txtWorkPhone.Text,
                              txtOfficePhone.Text, txtFax.Text, txtMobile.Text, txtEmail.Text, ddlGender.SelectedItem.Value,
                              txtIMScreenName.Text, ddlIMList.SelectedItem.Value, txtTitle.Text, txtBio.Value, profileImgPath,
                              ddlRole.SelectedItem.Value, txtUserName.Text, encryptedPassword, txtCompany.Text);

                        if (status)
                        {
                            pnlAddProfile.Visible = false;
                            pnlAddProfileMsg.Visible = true;
                            FransDataManager.GetAllEmployee(selectedCenter, true);
                            FransDataManager.GetFransWorkareaData(selectedCenter, true);
                        }
                        else
                        {
                            lblError.Visible = true;
                            lblError.Text = "Sorry!!! An error has occured creating a Profile";
                        }

                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "Sorry!!! An error has occured creating a Profile";
                    }

                }
                else
                {
                   var status = AdminToolManager.AddUserToCenter(selectedCenter, txtFirstName.Text, txtLastName.Text, txtWorkPhone.Text,
                             txtOfficePhone.Text, txtFax.Text, txtMobile.Text, txtEmail.Text, ddlGender.SelectedItem.Value,
                             txtIMScreenName.Text, ddlIMList.SelectedItem.Value, txtTitle.Text, txtBio.Value, profileImgPath,
                             ddlRole.SelectedItem.Value, txtUserName.Text, encryptedPassword, txtCompany.Text);
                    if (status)
                    {
                        pnlAddProfile.Visible = false;
                        pnlAddProfileMsg.Visible = true;
                        FransDataManager.GetAllEmployee(selectedCenter, true);
                        FransDataManager.GetFransWorkareaData(selectedCenter, true);
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "Sorry!!! An error has occured creating a Profile";
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
                lblError.Visible = true;
                lblError.Text = ex.Message;
            }
        }
    }

    /// <summary>
    /// update profile
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdateProfile_Click(object sender, EventArgs e)
    {
        if(Page.IsValid)
        {
            try
            {
                lblError.Text = "";
                //if (AdminToolManager.IsUserNameExist(txtUserName.Text.Trim()))
                //{

                long userId;
                long.TryParse(Request.QueryString["userid"], out userId);
                    string profileImgPath = "";
                    if (imageUpload.HasFiles)
                        profileImgPath = GetCmsImagePath();
                    else if(userImage.Src != "#")
                        profileImgPath = userImage.Src;
                    //string encryptedPassword = Encryptdata(txtPassword.Text);
                    string selectedCenter = hddnCenterId.Value;
                    string role = ddlRole.SelectedItem.Value;
                    int isActive = 1;
                    if (chkIsActive.Checked)
                        isActive = 1;
                    else
                        isActive = 0;

                    if (role == "Center Manager" || role == "Center Owner")
                    {
                        //if (!CommunityUserHelper.IsCmsUserExist(txtUserName.Text.Trim()))
                        //{
                        //    long cmsUserId = CommunityUserHelper.AddCmsMembershipUser(txtFirstName.Text, txtLastName.Text, txtUserName.Text, txtPassword.Text, txtEmail.Text, selectedCenter);
                        //}

                        AdminToolManager.UpdateCenterUser(selectedCenter, txtFirstName.Text, txtLastName.Text, txtWorkPhone.Text,
                                    txtOfficePhone.Text, txtFax.Text, txtMobile.Text, txtEmail.Text, ddlGender.SelectedItem.Value,
                                    txtIMScreenName.Text, ddlIMList.SelectedItem.Value, txtTitle.Text, txtBio.Value, profileImgPath,
                                    ddlRole.SelectedItem.Value, txtUserName.Text, txtCompany.Text, userId, isActive);
                        pnlAddProfile.Visible = false;
                        pnlEditProfileMsg.Visible = true;
                    }
                    else
                    {
                        AdminToolManager.UpdateCenterUser(selectedCenter, txtFirstName.Text, txtLastName.Text, txtWorkPhone.Text,
                                 txtOfficePhone.Text, txtFax.Text, txtMobile.Text, txtEmail.Text, ddlGender.SelectedItem.Value,
                                 txtIMScreenName.Text, ddlIMList.SelectedItem.Value, txtTitle.Text, txtBio.Value, profileImgPath,
                                 ddlRole.SelectedItem.Value, txtUserName.Text, txtCompany.Text, userId, isActive);
                        pnlAddProfile.Visible = false;
                        pnlEditProfileMsg.Visible = true;
                    }
                    FransDataManager.GetAllEmployee(selectedCenter, true);
                    FransDataManager.GetFransWorkareaData(selectedCenter, true);
                //}
                //else
                //{
                //    lblError.Visible = true;
                //    lblError.Text = "No User found with provided information.";
                //}
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
                lblError.Visible = true;
                lblError.Text = ex.Message;
            }
        }
    }

    private void FillData(long userId)
    {        
        var allUsers = AdminToolManager.GetAllUsers();
        var userData = allUsers.Where(x => x.EmployeeId == userId).FirstOrDefault();
        if (userData != null)
        {
            txtFirstName.Text = userData.FirstName;
            txtLastName.Text = userData.LastName;
            ddlGender.SelectedItem.Value = userData.Gender;
            txtWorkPhone.Text = userData.WorkPhone;
            txtMobile.Text = userData.MobileNumber;
            txtFax.Text = userData.FaxPhone;
            txtEmail.Text = userData.Email;
            if (userData.IMService != "")
            {                
                ddlIMList.Items.FindByValue(userData.IMService).Selected = true;
            }
            txtIMScreenName.Text = userData.IMScreenName;
            txtBio.Value = userData.Bio;
            txtTitle.Text = userData.Title;
            txtCompany.Text = userData.Company;
            if (!string.IsNullOrEmpty(userData.PicturePath))
            {
                userImage.Src = userData.PicturePath;
                userImage.Visible = true;
            }                
            
            if (!string.IsNullOrEmpty(userData.Roles))
                ddlRole.Items.FindByValue(userData.Roles).Selected = true;
            else
                ddlRole.SelectedIndex = 0;
            txtUserName.Text = userData.UserName;            
            if (userData.IsActive == 1)
                chkIsActive.Checked = true;
            else
                chkIsActive.Checked = false;
        }
    }    

    /// <summary>
    /// this metod is used to upload the image to cms
    /// </summary>
    /// <returns></returns>
    private string GetCmsImagePath()
    {
        string imgPath = string.Empty;
        LibraryManager libraryManager = new LibraryManager();
        long folderId = ConfigHelper.GetValueLong("UploadedFilesFolderId");
        Ektron.Cms.API.Library libraryAPI = new Ektron.Cms.API.Library();

        LibraryConfigData lib_setting_data = libraryAPI.GetLibrarySettings(folderId);

        MemoryStream stream = null;
        byte[] byteArray = null;
        HttpPostedFile postedFile = imageUpload.PostedFile;
        int fileLength = postedFile.ContentLength;
        byte[] fileData = new byte[fileLength];
        postedFile.InputStream.Read(fileData, 0, fileLength);
        if (fileData.Length > 0)
        {
            stream = new MemoryStream(fileData);
            byteArray = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(byteArray, 0, (int)stream.Length);

        }
        string strFilename = "";
        strFilename = Server.MapPath(lib_setting_data.ImageDirectory) + Path.GetFileName(postedFile.FileName);

        LibraryData libraryData = new LibraryData()
        {
            Title = imageUpload.FileName,            
            ParentId = folderId,
            LibraryType=Ektron.Cms.LibraryType.images,
            FileName = strFilename,
            LanguageId = 1033,
            ContentType = (int)Ektron.Cms.Common.EkEnumeration.CMSContentType.LibraryItem,
            File = byteArray
        };

        var resultData = libraryManager.Add(libraryData);
        if(resultData != null && resultData.Id > 0)
        {
            imgPath = resultData.FileName;
        }       

        return imgPath;
    }

    /// <summary>
    /// Function is used to encrypt the password
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    private string Encryptdata(string password)
    {
        string strmsg = string.Empty;
        byte[] encode = new byte[password.Length];
        encode = Encoding.UTF8.GetBytes(password);
        strmsg = Convert.ToBase64String(encode);
        return strmsg;
    }

    /// <summary>
    /// Function is used to Decrypt the password
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    private string Decryptdata(string encryptpwd)
    {
        string decryptpwd = string.Empty;
        UTF8Encoding encodepwd = new UTF8Encoding();
        Decoder Decode = encodepwd.GetDecoder();
        byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
        int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
        char[] decoded_char = new char[charCount];
        Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
        decryptpwd = new String(decoded_char);
        return decryptpwd;
    }
    
}