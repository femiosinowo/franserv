using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SignalGraphics.CMS;
using Ektron.Cms;
using System.IO;
using Ektron.Cms.Framework.Content;

public partial class AdminTool_Templates_Why_we_are_diff : System.Web.UI.Page
{
    UserAPI userApi = new UserAPI();
    string centerId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            txtBannerSubTitle.Value = "We pride ourselves as a leader in the business services market.";
            txtContentTagLine.Value = "We’re the perfect partner for all your business and marketing communication needs.";
            if (userApi.UserId > 0)
            {
                var udata = CommunityUserHelper.GetUserByUserId(userApi.UserId);
                if (udata != null && udata.Id > 0)
                {
                    var centerUsers = AdminToolManager.GetAllLocalAdmins();
                    var userData = centerUsers.Where(x => x.UserName.ToLower() == udata.Username.ToLower()).FirstOrDefault();
                    if (userData != null)
                    {
                        centerId = hdnCenterId.Value = userData.FransId;
                        this.FillData(centerId);
                    }
                }
            }
        }

        if (!string.IsNullOrEmpty(hdnCenterId.Value))
        {
            var centerData = FransDataManager.GetFransData(hdnCenterId.Value);
            if (centerData != null)
            {
                centerInfo.Visible = true;
                lblCenterName.Text = centerData.CenterName;
                lblCenterId.Text = centerData.FransId;
            }
        }
    }

    protected void btnAddContent_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblError.Text = "";
            var data = new WhyWeAreDiff();
            data.CenterId = hddnCenterId.Value;
            data.BannerTitle = txtBannerTitle.Text;
            data.BannerSubTitle = txtBannerSubTitle.Value;
            data.ContentTitle = txtContentTitle.Text;
            data.ContentTagLine = txtContentTagLine.Value;
            data.ContentDescription = txtDescription.Text.Replace("'", "''");
            data.VideoLink = txtVideoLink.Text;
            data.VideoStatementText = txtVideoStatementText.Text.Replace("'", "''");

            string profileImgPath = "";
            if (imageUpload.HasFiles)
                profileImgPath = GetCmsImagePath();
            else if (imgPicture.Src != "#")
                profileImgPath = imgPicture.Src;
            data.Image_Path = profileImgPath;
            var status = AdminToolManager.AddUpdateWhyWeAreDiffContent(data);
            if (status > 0)
            {
                updateContentMsg.Visible = true;
                this.FillData(hdnCenterId.Value);
            }
            else
            {
                lblError.Text = "Sorry, an error occured saving Center data.";
            }
        }
    }
    
    private void FillData(string centerId)
    {
        hddnCenterId.Value = centerId;
        var data = FransDataManager.GetWhyWeAreDiffContent(centerId, true);
        if(data != null)
        {
            txtBannerTitle.Text = data.BannerTitle;
            txtBannerSubTitle.Value = data.BannerSubTitle;
            txtContentTitle.Text = data.ContentTitle;
            //txtContentTagLine.Value = data.ContentTagLine;
            txtDescription.Text = data.ContentDescription.Replace("''", "'");
            txtVideoLink.Text = data.VideoLink;
            txtVideoStatementText.Text = data.VideoStatementText.Replace("''", "'");
            if(!string.IsNullOrEmpty(data.Image_Path))
            {
                imgPicture.Src = data.Image_Path;
                imgPicture.Visible = true;
            }
        }
    }

    private string GetCmsImagePath()
    {
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
            imgPath = libraryData.FileName;
        }
        return imgPath;
    }
}