<%@ WebHandler Language="C#" Class="GetLocationsByAddress" %>

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

public class RequestAddressData
{
    public string FileType
    { get; set; }
    public string UploadedFileIds
    { get; set; }
    public string FransId
    { get; set; }
}


public class GetLocationsByAddress : IHttpHandler, System.Web.SessionState.IRequiresSessionState 
{
    string sharedDriveUserName = System.Configuration.ConfigurationManager.AppSettings["SISUploadedUserName"];
    string sharedDrivePassword = System.Configuration.ConfigurationManager.AppSettings["SISUploadedPassword"];
    string sharedDriveDomain = System.Configuration.ConfigurationManager.AppSettings["SISUploadedDomain"];

    public void ProcessRequest(HttpContext context)
    {
        string jsonResponse = string.Empty;
        jsonResponse = GetData();

        context.Response.Clear();
        context.Response.ClearHeaders();
        context.Response.AddHeader("Content-Type", "text/plain");
        context.Response.ContentType = "text/plain";
        context.Response.Write(jsonResponse);
        context.Response.End();
    }

    private string GetData()
    {
        string json = string.Empty;
        var javaScriptSerializer = new JavaScriptSerializer();
        RequestAddressData reqData = new RequestAddressData();
        try
        {
            using (var inputStream = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
            {
                reqData = javaScriptSerializer.Deserialize<RequestAddressData>(inputStream.ReadToEnd());
            }
            string fileType = reqData.FileType;
            string uploadedFileIds = reqData.UploadedFileIds;
            string centerId = reqData.FransId;
            json = IsFileExist(fileType, uploadedFileIds, centerId);       
        }
        catch (Exception ex)
        {
            Log.WriteError(ex);
            json = "Error checking file on server.";
        }
        return json;
    }


    private string IsFileExist(string fileType, string uploadedFileIds, string centerId)
    {
        string json = string.Empty;
        if ((!string.IsNullOrEmpty(fileType)) && (!string.IsNullOrEmpty(uploadedFileIds)))
        {            
            try
            {
                string strFileIDs = uploadedFileIds;
                string[] arrFileIDs = strFileIDs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (arrFileIDs != null && arrFileIDs.Length > 0)
                {
                    foreach (string strFileID in arrFileIDs)
                    {
                        var fileName = HttpContext.Current.Session["fileName_" + strFileID] as string;
                        string dateStamp = DateTime.Now.ToString("MMddyyyyhhmm");
                        string filePath = SirSpeedyUtility.SISUploadedVirtualFolderPath;
                        if (!string.IsNullOrEmpty(centerId))
                            filePath = filePath + centerId + "\\" + fileType + "\\" + dateStamp;
                        else
                            filePath = filePath + "National" + "\\" + fileType + "\\" + dateStamp;

                        using (new Impersonator(sharedDriveUserName, sharedDriveDomain, sharedDrivePassword))
                        {
                            filePath = filePath + "\\" + fileName;

                            if (File.Exists(filePath))
                            {
                                json = "File '" + fileName + "' already exist on the server. Please try to upload a file with different file name.";
                                //clear out saved file info
                                foreach (string strFileID2 in arrFileIDs)
                                {
                                    HttpContext.Current.Session.Remove("fileSize_" + strFileID);
                                    HttpContext.Current.Session.Remove("fileContents_" + strFileID2);
                                    HttpContext.Current.Session.Remove("fileExtension_" + strFileID2);
                                    HttpContext.Current.Session.Remove("fileName_" + strFileID2);
                                }
                                break;
                            }                            
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Log.WriteError(ex.Message + " StackTrace:" + ex.StackTrace);
                json = "File already exist on the server.";
            }
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