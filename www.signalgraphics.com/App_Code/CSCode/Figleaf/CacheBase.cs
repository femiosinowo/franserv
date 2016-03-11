using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Ektron.Cms;
using Ektron.Cms.Framework;
using Ektron.Cms.Framework.Content;
using Ektron.Cms.Instrumentation;
using Webinar.ContentTypes;

// ReSharper disable CheckNamespace
namespace Figleaf {
// ReSharper restore CheckNamespace

    /// <summary>
    /// Summary description for Cache
    /// </summary>
    public class CacheBase {
        private static readonly bool CacheEnabled;
        private static int for5min;
        private static int for15min;
        private static int for30min;
        private static int for1hr;
        private static int for2hr;
        private static int for6hr;
        private static int for12hr;
        private static int for24hr;

        static CacheBase() {
            CacheEnabled = (ConfigurationManager.AppSettings["ek_CacheControls"] == "1");

            for5min     = int.Parse(ConfigurationManager.AppSettings["CacheLevel1"]);
            for15min    = int.Parse(ConfigurationManager.AppSettings["CacheLevel2"]);
            for30min    = int.Parse(ConfigurationManager.AppSettings["CacheLevel3"]);
            for1hr      = int.Parse(ConfigurationManager.AppSettings["CacheLevel4"]);
            for2hr      = int.Parse(ConfigurationManager.AppSettings["CacheLevel5"]);
            for6hr      = int.Parse(ConfigurationManager.AppSettings["CacheLevel6"]);
            for12hr     = int.Parse(ConfigurationManager.AppSettings["CacheLevel7"]);
            for24hr     = int.Parse(ConfigurationManager.AppSettings["CacheLevel8"]);
        }

        public static T Get<T>(string cacheKey) {
            if(!CacheEnabled || HttpRuntime.Cache[cacheKey] == null) return default(T);
            return (T)HttpRuntime.Cache[cacheKey];
        }

        public static void Put<T>(string cacheKey, T input, CacheDuration duration) {
            if(!CacheEnabled) return;
            if (Equals(input, default(T))) return;
            HttpRuntime.Cache.Insert(cacheKey, input, null, DateTime.Now.AddSeconds(GetSeconds(duration)), System.Web.Caching.Cache.NoSlidingExpiration);
        }

        public static string GetContentTitle(long contentId) {
            try {
                String cacheKey = String.Format("Figleaf:ContentID={0}:GetContentTitle:CacheBase", contentId);
                if (HttpRuntime.Cache[cacheKey] != null) return (string)HttpRuntime.Cache[cacheKey];
            
                ContentManager cManager = new ContentManager();
                ContentData cData = cManager.GetItem(contentId);
                if (cData != null) {
                    if(CacheEnabled) {
                        HttpRuntime.Cache.Insert(cacheKey, cData.Title, null, DateTime.Now.AddSeconds(for1hr), System.Web.Caching.Cache.NoSlidingExpiration);
                    }
                    return cData.Title;
                }
            }
            catch(Exception ex) {
                Log.WriteError("CacheBase exception!: "+ ex);
            }
            return "";
        }

        public static ContentData GetContent(long contentId) {
            try {
                String cacheKey = String.Format("Figleaf:ContentID={0}:GetContent:CacheBase", contentId);
                if (HttpRuntime.Cache[cacheKey] != null) return (ContentData)HttpRuntime.Cache[cacheKey];

                ContentManager capi = new ContentManager();
                ContentData cdata = capi.GetItem(contentId);
                if(CacheEnabled && cdata != null) {
                    HttpRuntime.Cache.Insert(cacheKey, cdata, null, DateTime.Now.AddSeconds(for1hr), System.Web.Caching.Cache.NoSlidingExpiration);
                }
                return cdata;
            }
            catch(Exception ex) {
                Log.WriteError("CacheBase exception!: " + ex);
                return new ContentData();
            }
        }

        public static ContentData GetContentAdmin(long contentId) {
            try {
                String cacheKey = String.Format("Figleaf:ContentID={0}:GetContentAdmin:CacheBase", contentId);
                if (HttpRuntime.Cache[cacheKey] != null) return (ContentData)HttpRuntime.Cache[cacheKey];

                ContentManager capi = new ContentManager(ApiAccessMode.Admin);
                ContentData cdata = capi.GetItem(contentId);
                if (CacheEnabled && cdata != null) {
                    HttpRuntime.Cache.Insert(cacheKey, cdata, null, DateTime.Now.AddSeconds(for1hr), System.Web.Caching.Cache.NoSlidingExpiration);
                }
                return cdata;
            }
            catch(Exception ex) {
                Log.WriteError("CacheBase exception!: " + ex);
                return new ContentData();
            }
        }

        public static ContentType<T> GetContentTyped<T>(long contentId) {
            ContentData cd = GetContent(contentId);
            return Make<T>(cd);
        }

        private static ContentType<T> Make<T>(ContentData contentItem) {
            try {
                T smartForm = (T) EkXml.Deserialize(typeof(T), contentItem.Html);

                ContentType<T> contentType = new ContentType<T> {SmartForm = smartForm, Content = contentItem};

                return contentType;
            }
            catch(Exception ex) {
                Log.WriteError("CacheBase exception!: " + ex);
                return null;
            }
        }

        public static FolderData GetFolder(long folderId) {
            try {
                String cacheKey = String.Format("Figleaf:FolderID={0}:GetFolder:CacheBase", folderId);
                if (HttpRuntime.Cache[cacheKey] != null) return (FolderData)HttpRuntime.Cache[cacheKey];

                var folderManager = new Ektron.Cms.Framework.Organization.FolderManager();
                FolderData fData = folderManager.GetItem(folderId);
                if(fData == null) return null; // TODO : Log the warning!!
                if(CacheEnabled) {
                    HttpRuntime.Cache.Insert(cacheKey, fData, null, DateTime.Now.AddSeconds(for1hr), System.Web.Caching.Cache.NoSlidingExpiration);
                }
                return fData;
            }
            catch(Exception ex) {
                Log.WriteError("CacheBase exception!: " + ex);
                return new FolderData();
            }
        }

        public static FolderData GetFolderAdmin(long folderId) {
            try {
                String cacheKey = String.Format("Figleaf:FolderID={0}:GetFolderAdmin:CacheBase", folderId);
                if (HttpRuntime.Cache[cacheKey] != null) return (FolderData)HttpRuntime.Cache[cacheKey];

                var folderManager = new Ektron.Cms.Framework.Organization.FolderManager(ApiAccessMode.Admin);
                FolderData fData = folderManager.GetItem(folderId);
                if(CacheEnabled) {
                    HttpRuntime.Cache.Insert(cacheKey, fData, null, DateTime.Now.AddSeconds(for1hr), System.Web.Caching.Cache.NoSlidingExpiration);
                }
                return fData;
            }
            catch(Exception ex) {
                  Log.WriteError("CacheBase exception!: "+ ex);
                return new FolderData();
            }
        }

        public static List<FolderData> GetFoldersAdmin(FolderCriteria folderCriteria) {
            try {
                String cacheKey = String.Format("Figleaf:FolderCriteriaID={0}:GetFoldersAdmin:CacheBase", folderCriteria.ToCacheKey());
                if (HttpRuntime.Cache[cacheKey] != null) return (List<FolderData>)HttpRuntime.Cache[cacheKey];

                var folderManager = new Ektron.Cms.Framework.Organization.FolderManager(ApiAccessMode.Admin);
                List<FolderData> fData = folderManager.GetList(folderCriteria);
                if(CacheEnabled) {
                    HttpRuntime.Cache.Insert(cacheKey, fData, null, DateTime.Now.AddSeconds(for1hr), System.Web.Caching.Cache.NoSlidingExpiration);
                }
                return fData;
            }
            catch(Exception ex) {
                  Log.WriteError("CacheBase exception!: "+ ex);
                return new List<FolderData>();
            }
        }

        public static CustomAttributeList GetMetaList(long contentID) {
            try {
                String cacheKey = String.Format("Figleaf:ContentID={0}:GetMetaList:CacheBase", contentID);
                if (HttpRuntime.Cache[cacheKey] != null) return (CustomAttributeList)HttpRuntime.Cache[cacheKey];

                Ektron.Cms.API.Metadata md = new Ektron.Cms.API.Metadata();
                CustomAttributeList cal = md.GetContentMetadataList(contentID);
                if(CacheEnabled) {
                    HttpRuntime.Cache.Insert(cacheKey, cal, null, DateTime.Now.AddSeconds(for1hr), System.Web.Caching.Cache.NoSlidingExpiration);
                }
                return cal;
            }
            catch(Exception ex) {
                  Log.WriteError("CacheBase exception!: "+ ex);
                return new CustomAttributeList();
            }
        }

        public static int GetSeconds(CacheDuration duration) {
            switch(duration) {
                case CacheDuration.For5Min:     return for5min;
                case CacheDuration.For15Min:    return for15min;
                case CacheDuration.For30Min:    return for30min;
                case CacheDuration.For1Hr:      return for1hr;
                case CacheDuration.For2Hr:      return for2hr;
                case CacheDuration.For6Hr:      return for6hr;
                case CacheDuration.For12Hr:     return for12hr;
                case CacheDuration.For24Hr:     return for24hr;
                default: return 0;
            }
        }
    }

    public class CacheCleaner {
        public static void ExpireByContentID(long contentID) {
            try {
                var query = from DictionaryEntry cacheItem in HttpRuntime.Cache 
                            where cacheItem.Key.ToString().Contains("ContentID=" + contentID) 
                               || cacheItem.Key.ToString().Contains("Ektron:urn:Ektron:ApplicationCache:contentblock:" + contentID) 
                            select cacheItem;
                Clean(query);
            }
            catch(Exception ex) {
                Log.WriteError("CacheBase exception!: " + ex);
            }
        }

        public static void ExpireAllContent() {
            try {
                var query = from DictionaryEntry cacheItem in HttpRuntime.Cache 
                            where cacheItem.Key.ToString().Contains("ContentID=") 
                               || cacheItem.Key.ToString().Contains("Ektron:urn:Ektron:ApplicationCache:contentblock:") 
                            select cacheItem;
                Clean(query);
            }
            catch(Exception ex) {
                Log.WriteError("CacheBase exception!: " + ex);
            }
        }

        public static void ExpireAllTaxonomy() {
            try {
                var query = from DictionaryEntry cacheItem in HttpRuntime.Cache 
                            where cacheItem.Key.ToString().Contains("TaxonomyID=") 
                            select cacheItem;
                Clean(query);
            }
            catch(Exception ex) {
                Log.WriteError("CacheBase exception!: " + ex);
            }
        }

        public static void ExpireAllFolder() {
            try {
                var query = from DictionaryEntry cacheItem in HttpRuntime.Cache 
                            where cacheItem.Key.ToString().Contains("FolderID=") 
                            select cacheItem;
                Clean(query);
            }
            catch(Exception ex) {
                Log.WriteError("CacheBase exception!: " + ex);
            }
        }

        public static void ExpireAllMenu() {
            try {
                var query = from DictionaryEntry cacheItem in HttpRuntime.Cache 
                            where cacheItem.Key.ToString().Contains("MenuID=") 
                            select cacheItem;
                Clean(query);
            }
            catch(Exception ex) {
                Log.WriteError("CacheBase exception!: " + ex);
            }
        }

        public static void ExpireAllCriteria()
        {
            try
            {
                var query = from DictionaryEntry cacheItem in HttpRuntime.Cache
                            where cacheItem.Key.ToString().Contains("Criteria=")
                            select cacheItem;
                Clean(query);
            }
            catch (Exception ex)
            {
                Log.WriteError("CacheBase exception!: " + ex);
            }
        }
        public static void ExpireAll() {
            try {
                foreach (DictionaryEntry entry in HttpRuntime.Cache) {
                    HttpRuntime.Cache.Remove(entry.Key.ToString());
                }
            }
            catch(Exception ex) {
                Log.WriteError("CacheBase exception!: " + ex);
            }
        }

        private static void Clean(IEnumerable<DictionaryEntry> list) {
            foreach (var dictionaryEntry in list) {
                HttpRuntime.Cache.Remove(dictionaryEntry.Key.ToString());
            }
        }

    }
}
