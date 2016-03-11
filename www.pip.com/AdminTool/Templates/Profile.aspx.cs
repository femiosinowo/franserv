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
using System.Drawing;


public partial class AdminTool_Templates_Profile : System.Web.UI.Page
{
    UserAPI userApi = new UserAPI();
	Ektron.Cms.API.User.User uApi = new Ektron.Cms.API.User.User();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            long userId = 0;
            if (uApi.IsAdmin())
                profileLink.HRef = "/admintool/templates/AllProfiles.aspx";
            else
                profileLink.HRef = "/admintool/templates/AllCenterProfiles.aspx";

            if (!string.IsNullOrEmpty(Request.QueryString["userid"]))
            {
                long.TryParse(Request.QueryString["userid"], out userId);
                btnAddProfile.Visible = false;
                passwordSection.Visible = false;
                addCenterTitle.Visible = false;
                btnUpdateProfile.Visible = true;
                editCenterTitle.Visible = true;
                lblEmailUsername.InnerHtml = "Email";
                this.FillData(userId);
            }
            else
            {
                btnUpdateProfile.Visible = false;
                isActiveSection.Visible = false;
                ddlRole.SelectedIndex = 0;
                FillCenterDropDown(0);
            }

            //get the centerId
            if (userApi.UserId > 0)
            {
                var udata = CommunityUserHelper.GetUserByUserId(userApi.UserId);
                if (udata != null && udata.Id > 0)
                {
                    var centerUsers = AdminToolManager.GetAllLocalAdmins();
                    var userData = centerUsers.Where(x => x.UserName.ToLower() == udata.Username.ToLower()).FirstOrDefault();
                    if (userData != null)
                    {
                        hddnCenterId.Value = userData.FransId;

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

    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRole.SelectedIndex > -1)
        {
            if (ddlRole.SelectedIndex == 3)
            {
                passwordSection.Visible = false;
                lblUserName.Visible = false;
            }
            else
            {
                passwordSection.Visible = true;
                lblUserName.Visible = true;
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
                if (AdminToolManager.IsUserNameExist(txtEmail.Text.ToLower().Trim()) || CommunityUserHelper.IsCmsUserExist(txtEmail.Text.ToLower().Trim()))
                {
                    lblError.Text = "A User already Exist with the selected UserName. Please select a different UserName.";
                    lblError.Visible = true;
                    return;
                }               


                string profileImgPath = "";
                if (imageUpload.HasFiles)
                {
                    bool isImageDimensionsValid = false;
                    string imgPath = GetCmsImagePath(out isImageDimensionsValid);

                    if (isImageDimensionsValid == false && imgPath == "")
                    {
                        lblError.Text = "Selected Image dimensions are smaller than supported demensions of 453 * 453. Please select new image.";
                        lblError.Visible = true;
                        return;
                    }

                    profileImgPath = imgPath;
                }
                
                string encryptedPassword = Encryptdata(txtPassword.Text);
                string selectedCenter = ddlCenterId.SelectedItem.Value;
                string role = ddlRole.SelectedItem.Value;
                string bio = txtBio.Value.Replace("'", "''");

                if (role == "Center Manager" || role == "Center Owner")
                {
                    var selectedCenterData = FransDataManager.GetFransData(selectedCenter);
                    long cmsUserId = CommunityUserHelper.AddCmsMembershipUser(txtFirstName.Text, txtLastName.Text, txtEmail.Text, txtPassword.Text, txtEmail.Text, selectedCenter);
                    if (cmsUserId > 0)
                    {
                        CommunityUserHelper.AddUserToCommunityGroup(cmsUserId, selectedCenterData.CmsCommunityGroupId, selectedCenterData.FransId, selectedCenterData.PhoneNumber);
                        var status = AdminToolManager.AddUserToCenter(selectedCenter, txtFirstName.Text, txtLastName.Text, txtWorkPhone.Text,
                              txtOfficePhone.Text, txtFax.Text, txtMobile.Text, txtEmail.Text, ddlGender.SelectedItem.Value,
                              txtIMScreenName.Text, ddlIMList.SelectedItem.Value, txtTitle.Text, bio, profileImgPath,
                              ddlRole.SelectedItem.Value, txtEmail.Text, encryptedPassword, txtCompany.Text);

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
                              txtIMScreenName.Text, ddlIMList.SelectedItem.Value, txtTitle.Text, bio, profileImgPath,
                              ddlRole.SelectedItem.Value, txtEmail.Text, encryptedPassword, txtCompany.Text);
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
                lblError.Text = "Exception Adding Profile.";
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
        if (Page.IsValid)
        {
            try
            {
                lblError.Text = "";
                //if (AdminToolManager.IsUserNameExist(txtUserName.Text.Trim()))
                //{

                long userId;
                long.TryParse(Request.QueryString["userid"], out userId);

                var allUsers = AdminToolManager.GetAllUsers();
                var currentUserData = allUsers.Where(x => x.EmployeeId == userId).FirstOrDefault();
                if (currentUserData == null)
                {
                    return;
                }

                string profileImgPath = "";
                if (imageUpload.HasFiles)
                {
                    bool isImageDimensionsValid = false;
                    string imgPath = GetCmsImagePath(out isImageDimensionsValid);

                    if (isImageDimensionsValid == false && imgPath == "")
                    {
                        lblError.Text = "Selected Image dimensions are smaller than supported demensions of 453 * 453. Please select new image.";
                        lblError.Visible = true;
                        return;
                    }                        

                    profileImgPath = imgPath;
                }
                else if(chkClearCurrentImg.Checked == true && userImage.Src != "#")
                {
                    profileImgPath = "";
                }
                else if (userImage.Src != "#")
                {
                    profileImgPath = userImage.Src;
                }
                //string encryptedPassword = Encryptdata(txtPassword.Text);
                string selectedCenter = ddlCenterId.SelectedItem.Value;
                string role = ddlRole.SelectedItem.Value;
                string bio = txtBio.Value.Replace("'", "''");
                int isActive = 1;
                if (chkIsActive.Checked)
                {
                    isActive = 1;
                    CommunityUserHelper.UnLockUserAccount(txtEmail.Text);
                }
                else
                {
                    isActive = 0;
                    CommunityUserHelper.LockUserAccount(txtEmail.Text);
                }

                if (role == "Center Manager" || role == "Center Owner")
                {
                    //check if local amdin is changing the center for a local admin user
                    //only super admin can change the cenyter for local admins.                   
                    if (uApi.UserId > 0 && uApi.IsAdmin() == false)
                    {
                        if (currentUserData.FransId != selectedCenter)
                        {
                            lblError.Text = "You do Not have the permissions to change the Center Name.";
                            lblError.Visible = true;
                            return;
                        }
                    }

                    //if (!CommunityUserHelper.IsCmsUserExist(txtEmail.Text.Trim()))
                    //{
                    //    long cmsUserId = CommunityUserHelper.AddCmsMembershipUser(txtFirstName.Text, txtLastName.Text, txtEmail.Text, txtPassword.Text, txtEmail.Text, selectedCenter);
                    //}

                    var status = AdminToolManager.UpdateCenterUser(selectedCenter, txtFirstName.Text, txtLastName.Text, txtWorkPhone.Text,
                                  txtOfficePhone.Text, txtFax.Text, txtMobile.Text, txtEmail.Text, ddlGender.SelectedItem.Value,
                                  txtIMScreenName.Text, ddlIMList.SelectedItem.Value, txtTitle.Text, bio, profileImgPath,
                                  role, currentUserData.UserName, txtCompany.Text, userId, isActive);
                    if (status)
                    {
                        pnlAddProfile.Visible = false;
                        pnlEditProfileMsg.Visible = true;
                        FransDataManager.GetAllEmployee(selectedCenter, true);
                        FransDataManager.GetFransWorkareaData(selectedCenter, true);
                    }
                }
                else
                {
                    var status = AdminToolManager.UpdateCenterUser(selectedCenter, txtFirstName.Text, txtLastName.Text, txtWorkPhone.Text,
                             txtOfficePhone.Text, txtFax.Text, txtMobile.Text, txtEmail.Text, ddlGender.SelectedItem.Value,
                             txtIMScreenName.Text, ddlIMList.SelectedItem.Value, txtTitle.Text, bio, profileImgPath,
                             role, currentUserData.UserName, txtCompany.Text, userId, isActive);
                    if (status)
                    {
                        pnlAddProfile.Visible = false;
                        pnlEditProfileMsg.Visible = true;
                        FransDataManager.GetAllEmployee(selectedCenter, true);
                        FransDataManager.GetFransWorkareaData(selectedCenter, true);
                    }
                }
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
                lblError.Text = "Exception Updating Profile.";
            }
        }
    }

    private void FillData(long userId)
    {
        this.FillCenterDropDown(userId);
        var allUsers = AdminToolManager.GetAllUsers();
        var userData = allUsers.Where(x => x.EmployeeId == userId).FirstOrDefault();
        if (userData != null)
        {
            txtFirstName.Text = userData.FirstName;
            txtLastName.Text = userData.LastName;
            ddlGender.SelectedItem.Text = userData.Gender;
            ddlGender.SelectedItem.Value = userData.Gender;
            txtWorkPhone.Text = userData.WorkPhone;
            txtMobile.Text = userData.MobileNumber;
            txtFax.Text = userData.FaxPhone;
            txtEmail.Text = userData.Email;
            if (userData.IMService != "")
            {
                ddlIMList.SelectedValue = userData.IMService;
            }
            txtIMScreenName.Text = userData.IMScreenName;
            txtBio.Value = userData.Bio.Replace("''", "'");
            txtTitle.Text = userData.Title;
            txtCompany.Text = userData.Company;
            if (!string.IsNullOrEmpty(userData.PicturePath))
            {
                userImage.Src = userData.PicturePath;
                userImage.Visible = true;
            }

            if (!string.IsNullOrEmpty(userData.Roles))
            {
                ddlRole.SelectedValue = userData.Roles;
                if (userData.Roles == "Center User")
                    lblUserName.Visible = false;
            }
            else
            {
                ddlRole.SelectedIndex = 0;
            }

            //txtUserName.Text = userData.UserName;
            if (!string.IsNullOrEmpty(userData.FransId))//&& userData.FransId != "arlingtonva999" && userData.FransId != "missionviejoca002")
            {
                ddlCenterId.SelectedValue = userData.FransId;
            }
            if (userData.IsActive == 1)
                chkIsActive.Checked = true;
            else
                chkIsActive.Checked = false;
        }
    }

    private void FillCenterDropDown(long employeeId)
    {
        var allCenters = FransDataManager.GetAllFransLocationDataList(true);
        if (allCenters != null && allCenters.Any())
        {
            //as per client hide the following location from the page
            var centerList = from c in allCenters
                             //where c.FransId != "arlingtonva999" && c.FransId != "missionviejoca002"
                             select new
                             {
                                 CenterName = c.CenterName,
                                 CenterId = c.FransId
                             };
            if (centerList != null)
            {
                if (uApi.IsAdmin())
                {
                    centerList = centerList.OrderBy(x => x.CenterName);
                    ddlCenterId.DataSource = centerList;
                    ddlCenterId.DataTextField = "CenterName";
                    ddlCenterId.DataValueField = "CenterId";
                    ddlCenterId.DataBind();
                }
                else
                {
                    if (employeeId == 0)
                        employeeId = GetCurrentEmployeeId();

                    var allUsers = AdminToolManager.GetAllUsers();
                    var userData = allUsers.Where(x => x.EmployeeId == employeeId).FirstOrDefault();
                    if (userData != null)
                    {
                        var localCenterData = centerList.Where(x => x.CenterId.ToLower().Equals(userData.FransId.ToLower())).FirstOrDefault();
                        if (localCenterData != null)
                        {
                            ddlCenterId.Items.Add(new ListItem(localCenterData.CenterName, localCenterData.CenterId));
                        }
                    }
                }

            }
            ListItem itemDDLCenter = new ListItem("-Select One-", "-Select One-");
            ddlCenterId.Items.Insert(0, itemDDLCenter);
        }
    }

    /// <summary>
    /// this metod is used to upload the image to cms
    /// </summary>
    /// <returns></returns>
    private string GetCmsImagePath(out bool imageStatus)
    {
        imageStatus = false;
        string imgPath = string.Empty;
        long folderId = ConfigHelper.GetValueLong("UploadedImagesFolderId");
        Ektron.Cms.API.Library libraryAPI = new Ektron.Cms.API.Library();
        LibraryManager libraryManager = new LibraryManager(Ektron.Cms.Framework.ApiAccessMode.Admin);
        LibraryConfigData lib_setting_data = libraryAPI.GetLibrarySettings(folderId);

        HttpPostedFile postedFile = imageUpload.PostedFile;
        string strFilename = Server.MapPath(lib_setting_data.ImageDirectory) + Path.GetFileName(postedFile.FileName);

        MemoryStream stream = null;
        byte[] byteArray = null;
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

        LibraryData libraryData = new LibraryData()
        {
            Title = Path.GetFileNameWithoutExtension(imageUpload.FileName),
            ParentId = folderId,
            FileName = strFilename.ToLower(),
            LanguageId = 1033,
            LibraryType = Ektron.Cms.LibraryType.images,
            TypeId = 1,
            ContentType = 7,
            Type = "images",
            File = byteArray
        };

        var resultData = libraryManager.Add(libraryData);
        if (resultData != null && resultData.Id > 0)
        {
            int calculatedWidth = 0;
            int calculatedHeight = 0;
            System.Drawing.Image myImage = System.Drawing.Image.FromFile(Server.MapPath(resultData.FileName));
            calculatedWidth = myImage.Width;
            calculatedHeight = myImage.Height;
            myImage.Dispose();
            //image dimensions validations
            if (calculatedWidth < 453 || calculatedHeight < 453)
            {
                libraryManager.Delete(resultData.Id);
                if (File.Exists(Server.MapPath(resultData.FileName)))
                    File.Delete(Server.MapPath(resultData.FileName));
                imgPath = "";
                imageStatus = false;
            }
            else
            {
                imgPath = resultData.FileName;
                imageStatus = true;
            }
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


    private long GetCurrentEmployeeId()
    {
        long eId = 0;
        if (userApi.UserId > 0)
        {
            var udata = CommunityUserHelper.GetUserByUserId(userApi.UserId);
            if (udata != null && udata.Id > 0)
            {
                var centerUsers = AdminToolManager.GetAllLocalAdmins();
                var userData = centerUsers.Where(x => x.UserName.ToLower() == udata.Username.ToLower()).FirstOrDefault();
                if (userData != null)
                {
                    eId = userData.EmployeeId;
                }
            }
        }
        return eId;
    }
}