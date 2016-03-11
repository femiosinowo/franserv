<%@ WebHandler Language="C#" Class="SaveConsultationData" %>

using System;
using System.Web;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

using Ektron.Newtonsoft.Json;
using Ektron.Cms.Instrumentation;
using TeamLogic.CMS;

public class ConsultationFormData
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string CompanyName { get; set; }    
    public string HowCanweHelp { get; set; }
    public string SignUp { get; set; }
    public string CenterId { get; set; }
}

public class SaveConsultationData : IHttpHandler
{   
    public void ProcessRequest(HttpContext context)
    {
        string jsonResponse = string.Empty;
        jsonResponse = SaveData();

        context.Response.Clear();
        context.Response.ClearHeaders();
        context.Response.AddHeader("Content-Type", "text/plain");
        context.Response.ContentType = "text/plain";
        context.Response.Write(jsonResponse);
        context.Response.End();
    }

    private string SaveData()
    {
        string json = string.Empty;
        var javaScriptSerializer = new JavaScriptSerializer();
        ConsultationFormData reqData = new ConsultationFormData();
        try
        {
            using (var inputStream = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
            {
                reqData = javaScriptSerializer.Deserialize<ConsultationFormData>(inputStream.ReadToEnd());
            }                       

            if (reqData != null && !string.IsNullOrEmpty(reqData.CenterId) && !string.IsNullOrEmpty(reqData.EmailAddress) &&
                !string.IsNullOrEmpty(reqData.FirstName) && !string.IsNullOrEmpty(reqData.LastName) && !string.IsNullOrEmpty(reqData.CompanyName))
            {
                bool signUp = false;
                signUp = reqData.SignUp == "true" ? true : false;
                FransDataManager.SaveUserRequestConsultationData(reqData.CenterId, reqData.FirstName, reqData.LastName,
                 reqData.EmailAddress, reqData.CompanyName, reqData.PhoneNumber, reqData.HowCanweHelp, signUp);
            }
        }
        catch (Exception ex)
        {
            Log.WriteError(ex.Message + " : " + ex.InnerException + " : " + ex.StackTrace);
            json = "Sorry, an error has occurred saving your data.";
        }
        return json;
    } 
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}