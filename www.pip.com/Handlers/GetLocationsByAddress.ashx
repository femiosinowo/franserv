<%@ WebHandler Language="C#" Class="GetLocationsByAddress" %>

using System;
using System.Web;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Collections.Generic;

using Ektron.Cms;
using SirSpeedy.CMS;
using Ektron.Newtonsoft.Json;
using Ektron.Cms.Instrumentation;

public class RequestAddressData
{
    public string Address
    { get; set; }
    public string Distance
    { get; set; }
    public bool AllUsLocations
    { get; set; }
}


public class GetLocationsByAddress : IHttpHandler
{

   public void ProcessRequest(HttpContext context)
    {
        string jsonResponse = string.Empty;
        jsonResponse = GetData();

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
            string address = reqData.Address;
            string distance = reqData.Distance;
            int radius;
            int.TryParse(reqData.Distance, out radius);
            bool isInitialLoad = reqData.AllUsLocations;
            json = GetLocations(isInitialLoad, address, distance);       
        }
        catch (Exception ex)
        {
            Log.WriteError(ex);
        }
        return json;
    }


    private string GetLocations(bool isInitialLoad, string address, string distance)
    {
        string json = string.Empty;
        string[] arrAddress = new string[2];
        var javaScriptSerializer = new JavaScriptSerializer();

        double givLat = 0;
        double givLon = 0;

        List<FransData> allLocations = FransDataManager.GetAllFransLocationDataList();
        List<FransData> results = new List<FransData>();

        if (isInitialLoad)
        {
            string cacheKey = "PIPMain-GetAllFransLocationDataList";
            bool dataExistInCache = ApplicationCache.IsExist(cacheKey);
            if (!dataExistInCache)
            {
                foreach (FransData fd in allLocations)
                {
                    //as per client hide the following location from the page
                    if (fd.Country == address && fd.FransId.ToLower() != "missionviejoca002")
                    {
                        givLat = double.Parse(fd.Latitude);
                        givLon = double.Parse(fd.Longitude);
                        results.Add(fd);
                    }
                }
                json = javaScriptSerializer.Serialize(results);
                if (json != string.Empty)
                    ApplicationCache.Insert(cacheKey, json, ConfigHelper.GetValueLong("longCacheInterval"));
            }
            else
            {
                var cacheData = ApplicationCache.GetValue(cacheKey);
                if (cacheData != null)
                    json = (string)cacheData;
            }
            return json;
        }

        //calculate the latitude & longitude for the requested address.
        this.GetLongLatOfAddress(address, out givLat, out givLon);

        int raduis;
        int.TryParse(distance, out raduis);

        if (!string.IsNullOrEmpty(distance) && raduis > 0)
        {
            results = GetUserSelectedRaduisLocations(allLocations, raduis.ToString(), givLat, givLon);

            //if result is empty then try 5 new attempts
            if (results == null || results.Count <= 0)
            {                
                for (int k = 0; k < 5; k++)
                {
                    raduis = IncrementRaduis(raduis);
                    results = GetUserSelectedRaduisLocations(allLocations, raduis.ToString(), givLat, givLon);
                    if (results != null && results.Any())
                        break;
                }
            }

            if (results != null && results.Any())
            {
                results = results.OrderBy(x => x.Miles).ToList();
                json = javaScriptSerializer.Serialize(results);
            }
        }

        return json;
    }


    private List<FransData> GetUserSelectedRaduisLocations(List<FransData> allLocations, string distance, double givLat, double givLon)
    {
        List<FransData> dataList = new List<FransData>();

        for (int i = 0; i < allLocations.Count; i++)
        {
            var radlat1 = Math.PI * givLat / 180;
            var radlat2 = Math.PI * double.Parse(allLocations[i].Latitude) / 180;
            var radlon1 = Math.PI * givLon / 180;
            var radlon2 = Math.PI * double.Parse(allLocations[i].Longitude) / 180;
            var theta = givLon - double.Parse(allLocations[i].Longitude);
            var radtheta = Math.PI * theta / 180;
            var dist = Math.Sin(radlat1) * Math.Sin(radlat2) + Math.Cos(radlat1) * Math.Cos(radlat2) * Math.Cos(radtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;
            // if (unit=="K") { dist = dist * 1.609344 }
            //if (unit=="N") { dist = dist * 0.8684 }
            dist = dist * 0.8684;
            if (dist <= int.Parse(distance))
            {
                if (!dataList.Contains(allLocations[i]) && allLocations[i].FransId.ToLower() != "missionviejoca002")
                {
                    allLocations[i].Miles = Math.Round(dist, 2);
                    dataList.Add(allLocations[i]);
                }
            }
        }
        return dataList;
    }

    private double GetLocationDistance(FransData centerData, double lon, double lat)
    {
        double distance = 0;
        if (centerData != null)
        {
            var radlat1 = Math.PI * lat / 180;
            var radlat2 = Math.PI * double.Parse(centerData.Latitude) / 180;
            var radlon1 = Math.PI * lon / 180;
            var radlon2 = Math.PI * double.Parse(centerData.Longitude) / 180;
            var theta = lon - double.Parse(centerData.Longitude);
            var radtheta = Math.PI * theta / 180;
            var dist = Math.Sin(radlat1) * Math.Sin(radlat2) + Math.Cos(radlat1) * Math.Cos(radlat2) * Math.Cos(radtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;
            distance = dist * 0.8684;            
        }
        return distance;
    }

    private int IncrementRaduis(int currentRaduis)
    {
        int raduis = currentRaduis;
        if (currentRaduis < 6)
            raduis = 30;
        else if (currentRaduis < 31)
            raduis = 55;
        else if (currentRaduis < 56)
            raduis = 80;
        else if (currentRaduis < 81)
            raduis = 105;
        else if (currentRaduis < 106)
            raduis = 130;
        else if (currentRaduis < 131)
            raduis = 155;
        else if (currentRaduis < 156)
            raduis = 180;
        else if (currentRaduis < 181)
            raduis = 205;
        else if (currentRaduis < 206)
            raduis = 1000;
        else if (currentRaduis < 1001)
            raduis = 2000;
        else if (currentRaduis < 2001)
            raduis = 3000;
        else if (currentRaduis < 30001)
            raduis = 4000;
        else
            raduis = 20000;
        return raduis;
    }


    private void GetLongLatOfAddress(string address, out double lat, out double lon)
    {
        lat = 0;
        lon = 0;
        if (address != "")
        {
            try
            {
                string baseUri = Utility.GetGoogleMapGeocodeUri(address);
                using (System.Net.WebClient wc = new System.Net.WebClient())
                {
                    string result = wc.DownloadString(baseUri);
                    var xmlElm = System.Xml.Linq.XElement.Parse(result);
                    var status = (from elm in xmlElm.Descendants()
                                  where
                                      elm.Name == "status"
                                  select elm).FirstOrDefault();
                    if (status.Value.ToLower() == "ok")
                    {
                        var resultLat = (from elm in xmlElm.Descendants()
                                         where
                                             elm.Name == "lat"
                                         select elm).FirstOrDefault();
                        lat = double.Parse(resultLat.Value);
                        var resultLon = (from elm in xmlElm.Descendants()
                                         where
                                             elm.Name == "lng"
                                         select elm).FirstOrDefault();
                        lon = double.Parse(resultLon.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }   

}