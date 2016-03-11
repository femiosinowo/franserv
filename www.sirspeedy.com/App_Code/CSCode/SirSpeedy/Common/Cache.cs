using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ektron.Cms;
using System.Configuration;

namespace SirSpeedy.CMS
{
    ///<summary> 
    /// This class provides a mechanism to store objects for the lifetime of an application pool 
    /// 
    /// Methods are static and generic, so no instance variable is required 
    /// and the desired object type is used without the need for type casting. 
    /// 
    /// Note that the cache entry Id is of type System.Uri, which may be 
    /// constructed inline with the method call. 
    /// </summary> 
    public class ApplicationCache
    {
        private const string APPLICATION_CACHE_COMPONENT = "ApplicationCache";
        private static readonly object _padLock = new object();
        private static readonly bool bEnableCache = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ek_CacheControls"]) > 0 ? true : false;
        
        protected ApplicationCache() 
        { }
        
        protected static bool ContextIsValid
        {
            get { return (System.Web.HttpContext.Current != null); }
        }

        /// <summary> 
        /// Adds (or updates) an object of type T to the request cache. 
        /// This method is overloaded. See Also: 
        /// * Insert(String,T) 
        /// * Insert(String,T,String) 
        /// * Insert(String,T,Double) 
        /// </summary> 
        /// <param name="key"> 
        /// The unique identifier for the cached object. 
        /// </param> 
        /// <param name="item"> 
        /// The object, of type T, to add to the cache 
        /// </param> 
        public static void Insert(string key, object item)
        {
            try
            {
                if (!ContextIsValid || !bEnableCache || item == null)
                    return;
                Invalidate(key);
                System.Web.
                HttpContext.Current.Cache.Insert(key, item);
            }
            catch (Exception ex)
            {
                EkException.LogException(ex, System.Diagnostics.EventLogEntryType.Error);
                EkException.LogException("XO-->> ApplicationCache.Insert::CacheKey-->>" + key);
            }
        }

        /// <summary> 
        /// Adds (or updates) an object of type T to the request cache. 
        /// This method is overloaded. See Also: 
        /// * Insert(String,T) 
        /// * Insert(String,T,String) 
        /// * Insert(String,T,Double) 
        /// </summary> 
        /// <param name="key"> 
        /// The unique identifier for the cached object. 
        /// </param> 
        /// <param name="item"> 
        /// The object, of type T, to add to the cache 
        /// </param> 
        /// <param name="dependency"> 
        /// Dependency relationship between an item stored in an ASP.NET application's Cache object and a file, of type string 
        /// </param> 
        public static void Insert(string key, object item, string dependency)
        {
            try
            {
                if (!ContextIsValid || !bEnableCache || item == null)
                    return;
                Invalidate(key);
                System.Web.
                HttpContext.Current.Cache.Insert(key, item, new System.Web.Caching.CacheDependency(dependency));
            }
            catch (Exception ex)
            {
                EkException.LogException(ex, System.Diagnostics.EventLogEntryType.Error);
                EkException.LogException("XO-->> ApplicationCache.Insert::CacheKey-->>" + key + "_" + "Dependency-->>" + dependency);
            }
        }

        /// <summary> 
        /// Adds (or updates) an object of type T to the request cache. 
        /// This method is overloaded. See Also: 
        /// * Insert(String,T) 
        /// * Insert(String,T,String) 
        /// * Insert(String,T,Double) 
        /// </summary> 
        /// <param name="key"> 
        /// The unique identifier for the cached object. 
        /// </param> 
        /// The object, of type T, to add to the cache 
        /// </param> 
        /// <param name="seconds"> 
        /// The time at which the inserted object expires and is removed from the cache,of type double 
        /// </param> 
        public static void Insert(string key, object item, double seconds)
        {
            try
            {
                if (!ContextIsValid || !bEnableCache || item == null)
                    return;
                lock (_padLock)
                {
                    if (seconds > 0.0)
                    {
                        Invalidate(key);
                        System.Web.
                        HttpContext.Current.Cache.Insert(key, item, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(seconds), System.Web.Caching.Cache.NoSlidingExpiration);
                    }
                }
            }
            catch (Exception ex)
            {
                EkException.LogException(ex, System.Diagnostics.EventLogEntryType.Error);
                EkException.LogException("XO-->> ApplicationCache.Insert::CacheKey->>" + key + "_" + "CacheInterval-->>" + seconds.ToString());
            }
        }

        /// <summary>
        /// Gets Value from Key
        /// </summary>
        /// <param name="key">Key Name Requested</param>      
        /// <returns>object Value</returns>
        public static object GetValue(string key)
        {
            object returnValue = null;
            if (!string.IsNullOrEmpty(key))
            {
                returnValue = HttpContext.Current.Cache.Get(key);
            }
            return returnValue;
        }


        /// <summary> 
        /// Removes an object from the request cache. 
        /// </summary> 
        /// <param name="key"> 
        /// The unique identifier for the cached object. 
        /// </param> 
        public static void Invalidate(string key)
        {
            try
            {
                if (ContextIsValid)
                    System.Web.
                    HttpContext.Current.Cache.Remove(key);
            }
            catch (Exception ex)
            {
                EkException.LogException(ex, System.Diagnostics.EventLogEntryType.Error);
                EkException.LogException("XO-->> ApplicationCache.Invalidate::CacheKey->>" + key);
            }
        }

        public static bool IsExist(string key)
        {
            return HttpContext.Current.Cache[key] != null;              
        }

        /// <summary> 
        /// Retrieves an object of type T from the request cache. 
        /// </summary> 
        /// <param name="key"> 
        /// The unique identifier for the cached object. 
        /// </param> 
        /// <returns> 
        /// An object, of type T, from the cache. May be a default 
        /// value (such as null, depending on the type) if the object does 
        /// not exist in the cache 
        /// </returns> 
        public static bool Get<T>(string key, out T value)
        {
            try
            {                
                if (bEnableCache && ContextIsValid && IsExist(key))
                {
                    value = (T)System.Web.
                    HttpContext.Current.Cache.Get(key);
                    return true;
                }
                else
                {
                    value =
                    default(T);
                    return false;
                }
            }
            catch (Exception ex)
            {
                EkException.LogException(ex, System.Diagnostics.EventLogEntryType.Error);
                EkException.LogException("XO-->> ApplicationCache.Get::CacheKey->>" + key);
                value =
                default(T);
                return false;
            }
        }
    }

    /// <summary> 
    /// This class provides access to Ektron global Cache keys 
    /// </summary> 
    public class CachingHelper
    {
        private const string EKTRON_CACHE_PREFIX = "urn:XOCom:";
        public struct CacheKeys
        {
            // public const string SiteList = "EktronSiteList"; 
            // public const string MultiSiteList = "EktronMultiSiteList"; 
        }

        public static bool IsCachingON()
        {
            if (Convert.ToInt32(ConfigurationManager.AppSettings["ek_CacheControls"]) > 0)
            {
                return true;
            }
            return false;
        }

        public static string MakeUniqueKey(string key)
        {
            if (null == key)
                throw new Exception("XO-->> Null value passed to CachingHelper::MakeUniqueKey()");
            string result = key.Replace('_', ':');
            return (result.StartsWith(EKTRON_CACHE_PREFIX) ? result : EKTRON_CACHE_PREFIX + result.TrimStart(':'));
        }

        public static string MakeUniqueKey(string key, string component)
        {
            if (null == key || null == component)
                throw new Exception("XO ->> Null value passed to CachingHelper::MakeUniqueKey()");
            string formattedComponent = component.Replace('_', ':');
            if (!formattedComponent.EndsWith(":"))
                formattedComponent +=
                ":";
            return MakeUniqueKey(formattedComponent + key);
        }
    }

}