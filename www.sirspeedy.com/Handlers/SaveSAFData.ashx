<%@ WebHandler Language="C#" Class="SaveSAFData" %>

using System;
using System.Web;
using System.Text;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Collections.Generic;

using Ektron.Cms;
using SirSpeedy.CMS;
using Ektron.Newtonsoft.Json;
using Ektron.Cms.Instrumentation;

public class SAFData
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string SelectedCenter { get; set; }
    public string PhoneNumber { get; set; }
    public string JobTitle { get; set; }
    public string CompanyName { get; set; }    
    public string ProjectName { get; set; }
    public string ProjectQuantity { get; set; }
    public string ProjectDueDate { get; set; }
    public string ProjectDescription { get; set; }
    public string UploadedFilesIds { get; set; }
    public string ExternalFileLinks { get; set; }
}


public class SaveSAFData : IHttpHandler, System.Web.SessionState.IRequiresSessionState 
{
    string sharedDriveUserName = System.Configuration.ConfigurationManager.AppSettings["SISUploadedUserName"];
    string sharedDrivePassword = System.Configuration.ConfigurationManager.AppSettings["SISUploadedPassword"];
    string sharedDriveDomain = System.Configuration.ConfigurationManager.AppSettings["SISUploadedDomain"];

    public void ProcessRequest(HttpContext context)
    {
        string jsonResponse = string.Empty;
        jsonResponse = SendData();

        context.Response.Clear();
        context.Response.ClearHeaders();
        context.Response.AddHeader("Content-Type", "text/plain");
        context.Response.ContentType = "text/plain";
        context.Response.Write(jsonResponse);
        context.Response.End();
    }

    private string SendData()
    {
        string json = string.Empty;
        var javaScriptSerializer = new JavaScriptSerializer();
        SAFData reqData = new SAFData();
        try
        {
            using (var inputStream = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
            {
                reqData = javaScriptSerializer.Deserialize<SAFData>(inputStream.ReadToEnd());
            }

            string firstName = reqData.FirstName;
            string lastName = reqData.LastName;
            string emailAddress = reqData.EmailAddress;
            string selectedCenter = reqData.SelectedCenter;
            string phoneNumber = reqData.PhoneNumber;
            string jobTitle = reqData.JobTitle;
            string companyName = reqData.CompanyName;
            string projectName = reqData.ProjectName;
            string projectQuantity = reqData.ProjectQuantity;
            string projectDueDate = reqData.ProjectDueDate;
            string projectDescription = reqData.ProjectDescription;
            string hdnFileIDs = reqData.UploadedFilesIds;
            string ektronUploadedFilesPath = string.Empty;
            string externalFileLinks = reqData.ExternalFileLinks;
            string userSelectedFiles = string.Empty;
            HttpContext currentContext = HttpContext.Current;

            if (String.IsNullOrWhiteSpace(selectedCenter)) selectedCenter = FransDataManager.GetFranchiseId();
            bool isFileUploaded = false;
            if (!string.IsNullOrEmpty(externalFileLinks))
            {
                //externalFileLinks = reqData.ExternalFileLinks;
                userSelectedFiles = reqData.ExternalFileLinks;
                isFileUploaded = true;
            }
            if (!string.IsNullOrEmpty(hdnFileIDs))
            {
                string strFileIDs = hdnFileIDs;
                string[] arrFileIDs = strFileIDs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (arrFileIDs != null && arrFileIDs.Length > 0)
                {
                    int filesSize = 0;
                    foreach (string strFileID in arrFileIDs)
                    {
                        int size;
                        if (HttpContext.Current.Session["fileSize_" + strFileID] != null)
                        {
                            int.TryParse(HttpContext.Current.Session["fileSize_" + strFileID].ToString(), out size);
                            filesSize += size;
                        }
                    }

                    int KB = 1024;
                    int MB = 1024 * KB;
                    int GB = 1024 * MB;
                    if (filesSize > GB)
                    {
                        foreach (string strFileID in arrFileIDs)
                        {
                            HttpContext.Current.Session.Remove("fileSize_" + strFileID);
                            HttpContext.Current.Session.Remove("fileContents_" + strFileID);
                            HttpContext.Current.Session.Remove("fileExtension_" + strFileID);
                            HttpContext.Current.Session.Remove("fileName_" + strFileID);
                        }
                        return json = "Uploaded Files size exceeds 1GB.";
                    }

                    string domainName = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
                    foreach (string strFileID in arrFileIDs)
                    {
                        var fileData = HttpContext.Current.Session["fileContents_" + strFileID] as byte[];
                        var fileName = HttpContext.Current.Session["fileName_" + strFileID] as string;
                        var fileExtension = HttpContext.Current.Session["fileExtension_" + strFileID] as string;

                        if (fileData != null && fileExtension != null && fileName != null)
                        {
                            try
                            {
                                string emailFilePath = "";
                                string filePath = SirSpeedyUtility.SISUploadedVirtualFolderPath;
                                string dateStamp = DateTime.Now.ToString("MMddyyyyhhmm");
                                string fransId = FransDataManager.GetFranchiseId();
                                if (!string.IsNullOrEmpty(fransId))
                                    filePath = filePath + fransId + "\\sentfiles\\" + dateStamp;
                                else
                                    filePath = filePath + "National" + "\\sentfiles\\" + dateStamp;
                                emailFilePath = filePath;

                                using (new Impersonator(sharedDriveUserName, sharedDriveDomain, sharedDrivePassword))
                                {
                                    if (!(Directory.Exists(filePath)))
                                    {
                                        Directory.CreateDirectory(filePath);
                                    }
                                    filePath = filePath + "\\" + fileName;
                                    emailFilePath = emailFilePath + "\\" + HttpUtility.UrlEncode(fileName);

                                    FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
                                    fileStream.Write(fileData, 0, fileData.Length);
                                    fileStream.Close();
                                    string encodedURL = "http://" + domainName + "/downloadFile.aspx?filepath=" + emailFilePath;
                                    ektronUploadedFilesPath += "<a target=\"_blank\" href=\"" + encodedURL + "\">" + fileName + "</a>, ";
                                }
                                userSelectedFiles += fileName + ", ";
                            }
                            catch (Exception ex)
                            {
                                Log.WriteError(ex.Message + " : " + ex.InnerException + " : " + ex.StackTrace);
                            }

                            HttpContext.Current.Session.Remove("fileContents_" + strFileID);
                            HttpContext.Current.Session.Remove("fileExtension_" + strFileID);
                            HttpContext.Current.Session.Remove("fileName_" + strFileID);
                            HttpContext.Current.Session.Remove("fileSize_" + strFileID);
                            isFileUploaded = true;
                        }
                    }
                }
            }

            if (isFileUploaded == false)
            {
                return json = "Please upload a file.";
            }
            //save data to custom db
            bool isSaved = FransDataManager.SaveSendAFileData(selectedCenter, firstName, lastName, jobTitle, companyName, emailAddress, phoneNumber, projectName, projectDescription, projectQuantity, ektronUploadedFilesPath, externalFileLinks, projectDueDate);
            if (!isSaved)
            {
                json = "Sorry, an error has occurred saving your data.";
                Log.WriteError(String.Format("Unable to save data. Center={0}:Firstname={1}:Lastname={2}:Title={3}:Company={4}:Email={5}:Phone={6}:ProjectName={7}:projectDescription={8}:projectQuantity={9}:DueDate={10}:ExternalFileLinks={11}", selectedCenter, firstName, lastName, jobTitle, companyName, emailAddress, phoneNumber, projectName, projectDescription, projectQuantity, projectDueDate, externalFileLinks));
            }
            else
            {
                string formattedLinks = ektronUploadedFilesPath;
                //var FilesList = userSelectedFiles.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                //if (FilesList != null && FilesList.Any())
                //{
                //    foreach (var link in FilesList)
                //        formattedLinks += "<a href=\"" + link + "\">" + link + "</a>, ";
                //}

                //start a new thread to decrease the wait time
                System.Threading.Thread emailThread = new System.Threading.Thread(delegate()
                {
                    if (HttpContext.Current == null)
                        HttpContext.Current = currentContext;
                    
                    string SAFfromEmailAddress = ConfigHelper.GetValueString("SAFfromEmailAddress");
                    SendEmailConfirmation.SendAFileConfirmationMessage(SAFfromEmailAddress, firstName, lastName,
                                        jobTitle, phoneNumber, emailAddress, projectName, projectQuantity, projectDueDate,
                                        selectedCenter, formattedLinks, null);
                });
                emailThread.IsBackground = true;
                emailThread.Start();
            }
        }
        catch (Exception ex)
        {
            Log.WriteError(ex.Message + " : " + ex.InnerException + " : " + ex.StackTrace);
            json = "Sorry, an error has occurred saving your data.";
        }
        return json;
    }        

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }  
     

}