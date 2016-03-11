<%@ WebHandler Language="C#" Class="GetCenterLOcations" %>

using System;
using System.Web;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Collections.Generic;

using Ektron.Cms;
using TeamLogic.CMS;
using Ektron.Newtonsoft.Json;
using Ektron.Cms.Instrumentation;

public class RequestData
{
    public string continentCode
    { get; set; }
}


public class GetCenterLOcations : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string jsonResponse = string.Empty;
        jsonResponse = GetAllLocations();
                
        context.Response.ContentType = "text/plain";
        context.Response.Write(jsonResponse);
        context.Response.End();
    }


    private string GetAllLocations()
    {
        string json = string.Empty;
        string continentCode = string.Empty;
        var javaScriptSerializer = new JavaScriptSerializer();
        RequestData reqData = new RequestData();
        try
        {
            using (var inputStream = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
            {
                continentCode = inputStream.ReadToEnd();
                reqData = javaScriptSerializer.Deserialize<RequestData>(continentCode);
            }

            if (reqData != null && !string.IsNullOrEmpty(reqData.continentCode))
            {
                string cacheKey = "FransLoactionsByContinents" + reqData.continentCode;
                bool dataExistInCache = ApplicationCache.IsExist(cacheKey);

                if (!dataExistInCache)
                {
                    var getAllLocations = FransDataManager.GetAllFransLocationDataList();
                    if (getAllLocations != null && getAllLocations.Any())
                    {
                        var getFilteredList = getAllLocations.Where(x => x.ContinentCode == reqData.continentCode && x.FransId.ToLower() != "missionviejoca002" && x.FransId.ToLower() != "arlingtonva999").ToList();
                        if (getFilteredList != null && getFilteredList.Any())
                        {
                            var filteredData = FilterCenterData(getFilteredList);
                            json = javaScriptSerializer.Serialize(filteredData);
                            if (json != string.Empty)
                                ApplicationCache.Insert(cacheKey, json, ConfigHelper.GetValueLong("longCacheInterval"));
                        }
                    }
                }
                else
                {
                    var cacheData = ApplicationCache.GetValue(cacheKey);
                    if (cacheData != null)
                        json = (string)cacheData;
                }
            }
        }
        catch(Exception ex)
        {
            Log.WriteError(ex);
        }
        return json;
    }   
           
    
    private List<FransData> FilterCenterData(List<FransData> data)
    {
        List<FransData> responseData = new List<FransData>();
        if (data.Exists(x => x.ContinentCode == "AS"))
        {
            FransData item = null;
            foreach(var loc in data)
            {
                item = new FransData();
                item = loc;
                if (item.Country == "AE")
                    item.State = "";
                responseData.Add(item);
            }
        }
        else
        {
            responseData = data;
        }
        return responseData;
    }
    
    
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}