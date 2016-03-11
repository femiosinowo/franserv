<%@ WebHandler Language="C#" Class="SaveSubscribeData" %>

using System;
using System.Web;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

using Ektron.Newtonsoft.Json;
using Ektron.Cms.Instrumentation;
using TeamLogic.CMS;

public class SubscribeFormData
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string CompanyName { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string CenterId { get; set; }
}

public class SaveSubscribeData : IHttpHandler {      

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
        SubscribeFormData reqData = new SubscribeFormData();
        try
        {
            using (var inputStream = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
            {
                reqData = javaScriptSerializer.Deserialize<SubscribeFormData>(inputStream.ReadToEnd());
            }

            string onlineType = " ";
            string printType = " ";

            if (reqData != null && !string.IsNullOrEmpty(reqData.CenterId) && !string.IsNullOrEmpty(reqData.EmailAddress) &&
                !string.IsNullOrEmpty(reqData.FirstName) && !string.IsNullOrEmpty(reqData.LastName) && !string.IsNullOrEmpty(reqData.CompanyName))
            {
                FransDataManager.SaveUserSubscribeData(reqData.CenterId, printType, onlineType, reqData.FirstName, reqData.LastName,
                 reqData.EmailAddress, reqData.Address, reqData.City, reqData.State, reqData.ZipCode, reqData.CompanyName, reqData.PhoneNumber);
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