<%@ WebHandler Language="C#" Class="SaveJobApplicationData" %>

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

public class JobApplicationData
{
    public string JobId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string EmailAddress { get; set; }
    public string SelectedCenter { get; set; }
    public string CoverNotes { get; set; }
    public string JobUrl { get; set; }
    public string UploadedFilesIds { get; set; }    
}


public class SaveJobApplicationData : IHttpHandler, System.Web.SessionState.IRequiresSessionState 
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
        JobApplicationData reqData = new JobApplicationData();
        using (var inputStream = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
        {
            reqData = javaScriptSerializer.Deserialize<JobApplicationData>(inputStream.ReadToEnd());
        }

        long jobId = reqData.JobId != null ? long.Parse(reqData.JobId) : 0;
        string firstName = reqData.FirstName;
        string lastName = reqData.LastName;
        string email = reqData.EmailAddress;
        string selectedCenter = reqData.SelectedCenter;
        string city = reqData.City;
        string state = reqData.State;
        string zipCode = reqData.ZipCode;
        string coverNotes = reqData.CoverNotes;
        string jobUrl = reqData.JobUrl;       
        string hdnFileIDs = reqData.UploadedFilesIds;
        string ektronUploadedFilesIds = string.Empty;
        string userSelectedFiles = string.Empty;

        try
        {
            string strFileIDs = hdnFileIDs;
            string[] arrFileIDs = strFileIDs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (arrFileIDs != null && arrFileIDs.Length > 0)
            {
                string domainName = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
                foreach (string strFileID in arrFileIDs)
                {
                    var fileData = (byte[])HttpContext.Current.Session["jobfileContents_" + strFileID];
                    var fileName = HttpContext.Current.Session["jobfileName_" + strFileID];
                    var fileExtension = HttpContext.Current.Session["jobfileExtension_" + strFileID];

                    if (fileData != null && fileExtension != null && fileName != null)
                    {
                        try
                        {
                            string filePath = SirSpeedyUtility.SISUploadedVirtualFolderPath;
                            if (!string.IsNullOrEmpty(selectedCenter))
                                filePath = filePath + selectedCenter + "\\resumes";
                            else
                                filePath = filePath + "National" + "\\resumes";

                            using (new Impersonator(sharedDriveUserName, sharedDriveDomain, sharedDrivePassword))
                            {
                                if (!(Directory.Exists(filePath)))
                                {
                                    Directory.CreateDirectory(filePath);
                                }
                                filePath = filePath + "\\" + fileName;

                                FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
                                fileStream.Write(fileData, 0, fileData.Length);
                                fileStream.Close();
                                ektronUploadedFilesIds += "<a target=\"_blank\" href=\"http://" + domainName + "/downloadFile.aspx?filepath=" + filePath + "\">" + fileName + "</a>, ";
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.WriteError(ex);
                        }

                        HttpContext.Current.Session.Remove("jobfileContents_" + strFileID);
                        HttpContext.Current.Session.Remove("jobfileExtension_" + strFileID);
                        HttpContext.Current.Session.Remove("jobfileName_" + strFileID);
                    }
                }
            }

            //save data to custom db
            FransDataManager.SaveJobApplicationData(jobId, selectedCenter, firstName, lastName, city,
                state, zipCode, email, ektronUploadedFilesIds, coverNotes, jobUrl, ektronUploadedFilesIds);

        }
        catch (Exception ex)
        {
            Log.WriteError(ex.Message + " : " + ex.StackTrace);
            json = "Sorry, an error has occured saving your data.";
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