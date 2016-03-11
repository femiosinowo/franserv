using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TeamLogic.CMS;
using Ektron.Cms;
using Ektron.Cms.Common;
using Ektron.Cms.Settings.UrlAliasing.DataObjects;
using Ektron.Cms.Framework.Settings.UrlAliasing;
using System.Configuration;
using System.Xml.Linq;
using System.Xml.XPath;
using Ektron.Cms.Framework.Content;

public partial class TestCreateCenterContent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }  
    //    string key = "";
    //    if (id > 0)
    //    {
    //        long modelSiteConfigKetCID;
    //        string cacheKey = "SirSpeedymodelSiteConfigContent";
    //        long.TryParse(ConfigurationManager.AppSettings["ModelSiteConfigKeysCId"], out modelSiteConfigKetCID);
    //        ContentData modelSiteConfigContent = new ContentData();
            
    //        bool dataExistInCache = ApplicationCache.IsExist(cacheKey);
    //        if (!dataExistInCache)
    //        {
    //            modelSiteConfigContent = ContentHelper.GetContentById(modelSiteConfigKetCID);
    //            if (modelSiteConfigContent != null && modelSiteConfigContent.Id > 0)
    //                ApplicationCache.Insert(cacheKey, modelSiteConfigContent, ConfigHelper.GetValueLong("longCacheInterval"));
    //        }
    //        else
    //        {
    //            var cacheData = ApplicationCache.GetValue(cacheKey);
    //            if (cacheData != null)
    //                modelSiteConfigContent = (ContentData)cacheData;
    //        }

    //        if (modelSiteConfigContent != null && modelSiteConfigContent.Id > 0)
    //        {
    //            XDocument xDoc = XDocument.Parse(modelSiteConfigContent.Html);
    //            var items = xDoc.XPathSelectElements("root/Item");

    //            var filterData = (from item in xDoc.Descendants("Item")
    //                              where item.Element("Value").Value == id.ToString()
    //                              select item).FirstOrDefault();
    //            if (filterData != null)
    //            {
    //                key = filterData.Element("Label").Value;
    //            }
    //        }
    //    }
    //    return key;
    //}
}