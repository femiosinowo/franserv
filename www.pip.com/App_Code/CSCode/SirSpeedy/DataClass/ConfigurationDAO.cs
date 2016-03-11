using System;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Configuration;
using System.Collections.Generic;

using Ektron.Cms;
using Ektron.Cms.Common;
using Ektron.Cms.Content;
using Ektron.Cms.Framework.Content;

namespace SirSpeedy.CMS
{
    /// <summary>
    /// This class is used to retrieve configuration values stored in the CMS or in the web.config.
    /// To work properly to retrieve it from the CMS, it relies on the following:
    ///  - There must be a content block in the CMS with a SmarttForm XML structure as follows:
    ///    <root><Item><Label></Label><Value></Value></Item>...<Item><Label></Label><Value></Value></Item></root>
    ///  - There must be a valid entry in the web.config's appSettings for "ConfigurationFolderName"
    /// </summary>
    public class ConfigurationDAO
    {
        /// <summary>
        /// Gets Value from Key
        /// </summary>
        /// <param name="key">Key Name Requested</param>        
        /// <returns>Configuration Value</returns>
        public static string GetConfigKeyValue(string key, bool refreshCache = true)
        {
            string returnValue = string.Empty;
            string configFolderName = ConfigurationManager.AppSettings["ConfigurationFolderName"];
            if (!string.IsNullOrEmpty(configFolderName))
            {
                string baseKey = string.Format(configFolderName + "{0}", key);
                returnValue = HttpContext.Current != null ? (string)HttpContext.Current.Cache[baseKey] : "";
                if (string.IsNullOrEmpty(returnValue) && refreshCache)
                {
                    //if site cache is enabled
                    if (Convert.ToInt32(ConfigurationManager.AppSettings["ek_CacheControls"]) > 0)
                    {
                        InitializeConfigurationCache(configFolderName);
                        returnValue = HttpContext.Current != null ? (string)HttpContext.Current.Cache[baseKey] : "";
                    }
                    else
                    {
                        returnValue = GetConfigValue(key, configFolderName);
                    }
                }
            }
            return returnValue;
        }

        /// <summary>
        /// Initializes  Cache Configuration
        /// </summary>
        /// <param name="languageID">Current Language</param>
        public static void InitializeConfigurationCache(string folderName)
        {
            //// Read Configuation folder and process applicable XMLConfigurationID 
            ContentCriteria criteria = new ContentCriteria();
            criteria.AddFilter(ContentProperty.FolderName, CriteriaFilterOperator.EqualTo, folderName);
            List<ContentData> listContent = ContentHelper.GetListByCriteria(criteria);
            if (listContent != null && listContent.Any())
            {
                string configSmartFormId = ConfigurationManager.AppSettings["ConfigurationSmartFormId"];
                if (!string.IsNullOrEmpty(configSmartFormId))
                {
                    long smartFormId;
                    long.TryParse(configSmartFormId, out smartFormId);
                    foreach (ContentData cd in listContent)
                    {
                        if (smartFormId > 0 && cd.XmlConfiguration != null && cd.XmlConfiguration.Id == smartFormId)
                            ParseEntryContent(cd);
                    }
                }
            }
        }       

        /// <summary>
        /// Cache Name/Value pairs
        /// </summary>
        /// <param name="cd">Smart Form XML</param>       
        private static void ParseEntryContent(ContentData cd)
        {
            if (cd != null && cd.Html != string.Empty)
            {
                ////Cache Name/Value Pairs
                XDocument xdoc = XDocument.Parse(cd.Html);
                IEnumerable<XElement> childList = from nd in xdoc.Root.Elements()
                                                  select nd;

                CacheEntries(childList);
            }
        }

        /// <summary>
        /// XCaches list of strings
        /// </summary>
        /// <param name="entries">XDocument with smart form content</param>       
        private static void CacheEntries(IEnumerable<XElement> entries)
        {
            //// Cache each entry
            string configFolderName = ConfigurationManager.AppSettings["ConfigurationFolderName"];
            double minutesToCache = 1; // 1 minute by default such that master page components can load
            Int32 defaultCacheInterval;
            Int32.TryParse(ConfigurationManager.AppSettings["ConfigurationSettingsCacheValue"], out defaultCacheInterval);
            if (defaultCacheInterval > 0)
            {
                minutesToCache = defaultCacheInterval;
            }

            string baseKey = string.Empty;
            foreach (XElement node in entries)
            {
                IEnumerable<XElement> subnodes = from snd in node.Elements()
                                                 select snd;
                baseKey = string.Format(configFolderName + "{0}", subnodes.ElementAt(0).Value);
                if (HttpContext.Current != null)
                    HttpContext.Current.Cache.Insert(baseKey, subnodes.ElementAt(1).Value, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("/web.config")), DateTime.Now.AddMinutes(minutesToCache), System.Web.Caching.Cache.NoSlidingExpiration);
            }
        }


        /// <summary>
        /// this method is used to get the config value directly from database
        /// </summary>
        /// <param name="key">string key</param>
        /// <returns>string value</returns>
        private static string GetConfigValue(string key, string folderName)
        {
            string val = string.Empty;
            string configSmartFormId = ConfigurationManager.AppSettings["ConfigurationSmartFormId"];
            if ((!string.IsNullOrEmpty(configSmartFormId)) && (!string.IsNullOrEmpty(key)) && (!string.IsNullOrEmpty(folderName)))
            {
                ContentCriteria criteria = new ContentCriteria();
                criteria.AddFilter(ContentProperty.FolderName, CriteriaFilterOperator.EqualTo, folderName);
                criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, configSmartFormId);
                List<ContentData> cList = ContentHelper.GetListByCriteria(criteria);
                if (cList != null && cList.Count > 0)
                {
                    foreach (ContentData data in cList)
                    {
                        XDocument xdoc = XDocument.Parse(data.Html);
                        IEnumerable<XElement> childList = from nd in xdoc.Root.Elements()
                                                          select nd;
                        if (childList != null)
                        {
                            foreach (XElement node in childList)
                            {
                                IEnumerable<XElement> subnodes = from snd in node.Elements()
                                                                 select snd;
                                if (subnodes.ElementAt(0).Value == key)
                                {
                                    return subnodes.ElementAt(1).Value;
                                }
                            }
                        }
                    }
                }
            }
            return val;
        }        
    }
}
