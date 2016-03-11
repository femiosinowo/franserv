using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SirSpeedy.CMS;
using System.IO.Compression;
using HtmlAgilityPack;
using System.IO;
using Ektron.Cms.Instrumentation;

public partial class AdminTool_Templates_DownloadZipFiles : System.Web.UI.Page
{
    string sharedDriveUserName = System.Configuration.ConfigurationManager.AppSettings["SISUploadedUserName"];
    string sharedDrivePassword = System.Configuration.ConfigurationManager.AppSettings["SISUploadedPassword"];
    string sharedDriveDomain = System.Configuration.ConfigurationManager.AppSettings["SISUploadedDomain"];
    Ektron.Cms.API.User.User uApi = new Ektron.Cms.API.User.User();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString.HasKeys())
        {
            try
            {
                long id = 0;
                string type = "";
                string backURL = "";
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                    long.TryParse(Request.QueryString["id"], out id);

                if (!string.IsNullOrEmpty(Request.QueryString["type"]))
                    type = Request.QueryString["type"].ToLower();

                if (type == "saf")
                {
                    if (uApi.IsAdmin())
                        backURL = "/admintool/templates/manage-all-sent-files.aspx";
                    else
                        backURL = "/admintool/templates/manage-all-center-sent-files.aspx";
                    this.ProcessSAF(id);
                }
                else if (type == "raq")
                {
                    if (uApi.IsAdmin())
                        backURL = "/admintool/templates/manage-all-quotes.aspx";
                    else
                        backURL = "/admintool/templates/manage-all-center-quotes.aspx";
                    this.ProcessRAQ(id);
                }
                backLink.HRef = backURL;        
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.Message + ":::" + ex.StackTrace);
            }
        }        
    }

    private void ProcessSAF(long id)
    {
        var safData = AdminToolManager.GetSendAFileInfoById(id);
        if (safData != null && !string.IsNullOrEmpty(safData.UploadedFileId))
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(safData.UploadedFileId);

            var hrefList = doc.DocumentNode.SelectNodes("//a")
                  .Select(p => p.GetAttributeValue("href", "not found"))
                  .ToList();
            //read all files path
            if (hrefList != null && hrefList.Any())
            {
                string dateStamp = DateTime.Now.ToString("MMddyyyyhhmm");
                string folderPath = SirSpeedyUtility.SISUploadedVirtualFolderPath;
                if (!string.IsNullOrEmpty(safData.CenterId))
                    folderPath = folderPath + safData.CenterId + "\\sentfiles\\" + safData.Email + "_" + id + "_Combined";
                else
                    folderPath = folderPath + "National" + "\\sentfiles\\" + safData.Email + "_" + id + "_Combined";
                string zipFolderPath = folderPath + ".zip";
                                
                using (new Impersonator(sharedDriveUserName, sharedDriveDomain, sharedDrivePassword))
                {
                    if (!(File.Exists(zipFolderPath)))
                    {                        
                        //create folder if not found
                        if (!(Directory.Exists(folderPath)))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        //copy files to new folder
                        foreach (string path in hrefList)
                        {
                            string formattedPath = path.Remove(0, path.IndexOf("filepath=") + 9);
                            formattedPath = HttpUtility.UrlDecode(formattedPath);

                            string fileName = Path.GetFileName(formattedPath);
                            string filePath = folderPath + "\\" + fileName;
                            var fileData = File.ReadAllBytes(formattedPath);
                            if (fileData != null)
                            {
                                FileStream newFileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
                                newFileStream.Write(fileData, 0, fileData.Length);
                                newFileStream.Close();
                            }
                        }
                        //create zip
                        ZipFile.CreateFromDirectory(folderPath, zipFolderPath);
                    }
                }

                frame1.Src = "/downloadFile.aspx?filepath=" + zipFolderPath;
                pnlZipReady.Visible = true;
            }
        }
    }

    private void ProcessRAQ(long id)
    {
        var raqData = AdminToolManager.GetRequestToQuotesById(id);
        if (raqData != null && !string.IsNullOrEmpty(raqData.UploadedFileId))
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(raqData.UploadedFileId);

            var hrefList = doc.DocumentNode.SelectNodes("//a")
                  .Select(p => p.GetAttributeValue("href", "not found"))
                  .ToList();

            //read all files path
            if (hrefList != null && hrefList.Any())
            {
                string dateStamp = DateTime.Now.ToString("MMddyyyyhhmm");
                string folderPath = SirSpeedyUtility.SISUploadedVirtualFolderPath;
                if (!string.IsNullOrEmpty(raqData.CenterId))
                    folderPath = folderPath + raqData.CenterId + "\\requestquotefiles\\" + raqData.Email + "_" + id + "_Combined";
                else
                    folderPath = folderPath + "National" + "\\requestquotefiles\\" + raqData.Email + "_" + id + "_Combined";
                string zipFolderPath = folderPath + ".zip";

                using (new Impersonator(sharedDriveUserName, sharedDriveDomain, sharedDrivePassword))
                {
                    if (!(File.Exists(zipFolderPath)))
                    {
                        //create folder if not found
                        if (!(Directory.Exists(folderPath)))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        //copy files to new folder
                        foreach (string path in hrefList)
                        {
                            string formattedPath = path.Remove(0, path.IndexOf("filepath=") + 9);
                            formattedPath = HttpUtility.UrlDecode(formattedPath);

                            string fileName = Path.GetFileName(formattedPath);
                            string filePath = folderPath + "\\" + fileName;
                            var fileData = File.ReadAllBytes(formattedPath);
                            if (fileData != null)
                            {
                                FileStream newFileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
                                newFileStream.Write(fileData, 0, fileData.Length);
                                newFileStream.Close();
                            }
                        }
                        //create zip
                        ZipFile.CreateFromDirectory(folderPath, zipFolderPath);
                    }
                }
                frame1.Src = "/downloadFile.aspx?filepath=" + zipFolderPath;
                pnlZipReady.Visible = true;
            }
        }
    }     
}