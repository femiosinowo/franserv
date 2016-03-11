<%@ WebHandler Language="C#" Class="SaveREQData" %>

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

public class RAQData
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string SelectedCenter { get; set; }
    public string PhoneNumber { get; set; }
    public string JobTitle { get; set; }
    public string CompanyName { get; set; }    
    public string ProjectName { get; set; }
    public string ProjectBudget { get; set; }
    public string DueDate { get; set; }    
    public string ProjectDescription { get; set; }
    public string UploadedFilesIds { get; set; }
    public string EmailOptIn { get; set; }
}


public class SaveREQData : IHttpHandler, System.Web.SessionState.IRequiresSessionState 
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
        RAQData reqData = new RAQData();
        using (var inputStream = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
        {
            reqData = javaScriptSerializer.Deserialize<RAQData>(inputStream.ReadToEnd());
        }

        string firstName = reqData.FirstName;
        string lastName = reqData.LastName;
        string email = reqData.EmailAddress;
        string selectedCenter = reqData.SelectedCenter;
        string phoneNumber = reqData.PhoneNumber;
        string jobTitle = reqData.JobTitle;
        string companyName = reqData.CompanyName;
        string projectName = reqData.ProjectName;
        string projectDescription = reqData.ProjectDescription;
        string projectBudget = reqData.ProjectBudget;
        string duedate = reqData.DueDate;
        string hdnFileIDs = reqData.UploadedFilesIds;
        string ektronUploadedFilesPath = string.Empty;
        string userSelectedFiles = string.Empty;
        string emailOptIn = reqData.EmailOptIn;
        
        HttpContext currentContext = HttpContext.Current;

        try
        {
            if (!string.IsNullOrEmpty(hdnFileIDs))
            {
                string strFileIDs = hdnFileIDs;
                string[] arrFileIDs = strFileIDs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (arrFileIDs != null && arrFileIDs.Length > 0)
                {
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
                                    filePath = filePath + fransId + "\\requestquotefiles\\" + dateStamp;
                                else
                                    filePath = filePath + "National" + "\\requestquotefiles\\" + dateStamp;
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
                        }
                    }
                }
            }

            //save data to custom db
            FransDataManager.SaveRequestToQuoteData(selectedCenter, firstName, lastName, jobTitle,
                companyName, email, phoneNumber, projectName, projectDescription, projectBudget,
                ektronUploadedFilesPath, duedate, emailOptIn);

            //start a new thread to decrease the wait time
            System.Threading.Thread emailThread = new System.Threading.Thread(delegate()
            {
                if (HttpContext.Current == null)
                    HttpContext.Current = currentContext;
                
                string REQfromEmailAddress = ConfigHelper.GetValueString("RAQfromEmailAddress");
                SendEmailConfirmation.SendAFileConfirmationMessage(REQfromEmailAddress, firstName, lastName, jobTitle, phoneNumber,
                    email, projectName, "", duedate, selectedCenter, ektronUploadedFilesPath, emailOptIn);
            });
            emailThread.IsBackground = true;
            emailThread.Start();

            // Response.Redirect("/thank-you/?type=requestaquote&centerId=" + selectedCenter);

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