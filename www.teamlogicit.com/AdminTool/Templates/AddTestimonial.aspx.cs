using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TeamLogic.CMS;
using Ektron.Cms;
using Ektron.Cms.Instrumentation;
using Ektron.Cms.Framework.Content;
using System.IO;

public partial class AdminTool_Templates_AddTestimonial : System.Web.UI.Page
{
    UserAPI userApi = new UserAPI();
    string centerId;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (userApi.UserId > 0)
            {
                var udata = CommunityUserHelper.GetUserByUserId(userApi.UserId);
                if (udata != null && udata.Id > 0)
                {
                    var centerUsers = AdminToolManager.GetAllLocalAdmins();
                    var userData = centerUsers.Where(x => x.UserName.ToLower() == udata.Username.ToLower()).FirstOrDefault();
                    if (userData != null)
                    {
                        centerId = userData.FransId;
                        hddnCenterId.Value = centerId;                                         

                        if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                        {
                            int id;
                            int.TryParse(Request.QueryString["id"], out id);
                            this.FillData(id, centerId);
                        }                        
                    }
                }
            }
        }
    }

    protected void btnAddTestimonial_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblError.Text = "";
            centerId = hddnCenterId.Value;
            bool isNational = false;
            Testimonials tData = new Testimonials();

            tData.Title = txtTitle.Text;
            tData.FirstName = txtFirstName.Text;
            tData.LastName = txtLastName.Text;
            tData.Organization = txtCompany.Text;
            tData.EmailAddress = txtEmail.Text;
            tData.PhoneNumber = txtPhone.Text;
            tData.Statement = txtStatement.Value.Replace("'", "''");
            if (imgUpload.HasFiles)
                tData.PicturePath = GetCmsImagePath();
            else if (testimonialImg.Src != "#")
                tData.PicturePath = testimonialImg.Src;
            tData.Center_Id = centerId;           
            tData.IsAboutUs = chkAboutUs.Checked;
            tData.IsManageIT = chkManageITServices.Checked;
            tData.IsProjectExpert = chkConsultingProjects.Checked;

            var status = -1;
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                int id;
                int.TryParse(Request.QueryString["id"], out id);

                var allTestimonialsList = FransDataManager.GetAllTestimonials(centerId, true);
                if (allTestimonialsList != null && allTestimonialsList.Any())
                {
                    var testimonialData = allTestimonialsList.Where(x => x.TestimonialId == id).FirstOrDefault();
                    if (testimonialData != null && testimonialData.TestimonialId > 0)
                    {
                        isNational = testimonialData.IsNational != null ? testimonialData.IsNational : false;
                    }
                }
                tData.TestimonialId = id;
                tData.IsNational = isNational;
                status = AdminToolManager.UpdateLocalTestimonial(tData);
            }
            else
            {
                status = AdminToolManager.AddTestimonial(tData);
            }

            if (status > 0)
            {
                pnlAddTestimonialMsg.Visible = true;
                //clear cache
                FransDataManager.GetAllTestimonials(centerId, true);
                if(!string.IsNullOrEmpty(tData.PicturePath))
                {
                    testimonialImg.Src = tData.PicturePath;
                    testimonialImg.Visible = true;
                }
            }
            else
            {
                lblError.Text = "Sorry, an error has occurred saving Testimonial data.";
            }
        }
    }

    private void FillData(int id, string centerId)
    {
        if (id > 0)
        {
            var allTestimonialsList = FransDataManager.GetAllTestimonials(centerId, true);
            if (allTestimonialsList != null && allTestimonialsList.Any())
            {
                var tData = allTestimonialsList.Where(x => x.TestimonialId == id).FirstOrDefault();
                if (tData != null && tData.TestimonialId > 0)
                {
                    txtTitle.Text = tData.Title;
                    txtFirstName.Text = tData.FirstName;
                    txtLastName.Text = tData.LastName;
                    txtCompany.Text = tData.Organization;
                    txtEmail.Text = tData.EmailAddress;
                    txtPhone.Text = tData.PhoneNumber;
                    if (!string.IsNullOrEmpty(tData.PicturePath)) { 
                        testimonialImg.Src = tData.PicturePath;
                        testimonialImg.Visible = true;
                    }
                    txtStatement.Value = HttpUtility.HtmlDecode(tData.Statement.Replace("''", "'"));
                    chkAboutUs.Checked = tData.IsAboutUs != null ? tData.IsAboutUs : false;
                    chkManageITServices.Checked = tData.IsManageIT != null ? tData.IsManageIT : false;
                    chkConsultingProjects.Checked = tData.IsProjectExpert != null ? tData.IsProjectExpert : false;
                }
            }
        }
    }

    /// <summary>
    /// this metod is used to upload the image to cms
    /// </summary>
    /// <returns></returns>
    private string GetCmsImagePath()
    {
        string imgPath = string.Empty;
        LibraryManager libraryManager = new LibraryManager(Ektron.Cms.Framework.ApiAccessMode.Admin);
        long folderId = ConfigHelper.GetValueLong("UploadedFilesFolderId");
        Ektron.Cms.API.Library libraryAPI = new Ektron.Cms.API.Library();

        LibraryConfigData lib_setting_data = libraryAPI.GetLibrarySettings(folderId);

        MemoryStream stream = null;
        byte[] byteArray = null;
        HttpPostedFile postedFile = imgUpload.PostedFile;
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
            Title = imgUpload.FileName,
            ParentId = folderId,
            LibraryType = Ektron.Cms.LibraryType.images,
            FileName = strFilename,
            LanguageId = 1033,
            ContentType = (int)Ektron.Cms.Common.EkEnumeration.CMSContentType.LibraryItem,
            File = byteArray
        };

        var resultData = libraryManager.Add(libraryData);
        if (resultData != null && resultData.Id > 0)
        {
            imgPath = resultData.FileName;
        }

        return imgPath;
    }

}