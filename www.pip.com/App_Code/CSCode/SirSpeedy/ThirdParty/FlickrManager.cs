using Ektron.Cms.Instrumentation;
using FlickrNet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SirSpeedy.CMS
{
    /// <summary>
    /// Summary description for FlickrManager
    /// </summary>
    public class FlickrManager
    {
        private static string apiKey = ConfigurationManager.AppSettings["FlickrApiKey"];
        private static string secretKey = ConfigurationManager.AppSettings["FlickrShardSecret"];
        private static Flickr flickr = new Flickr(apiKey, secretKey);

        public FlickrManager()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// This method is used to get the photos from Flickr
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="photoCount">no of photos requested</param>
        /// <returns>photo collection object</returns>
        public static PhotoCollection GetFlickrImages(string userId, int photoCount)
        {
            PhotoCollection result = null;
            try
            {
                string cacheKey = "SirSpeedyFlickr" + userId + photoCount;
                bool dataExistInCache = ApplicationCache.IsExist(cacheKey);

                if (dataExistInCache == false)
                {
                    if (!string.IsNullOrEmpty(userId))
                    {
                        flickr.InstanceCacheDisabled = true;
                        FlickrNet.PhotoSearchOptions options = new FlickrNet.PhotoSearchOptions(); 
                        options.UserId = userId;
                        if (photoCount <= 0)
                            options.PerPage = 20;
                        else
                            options.PerPage = photoCount;
                        options.Page = 1;
                        options.SortOrder = FlickrNet.PhotoSearchSortOrder.DatePostedDescending;
                        result = flickr.PhotosSearch(options);

                        var photoDataInfo = flickr.PhotosGetInfo("14130307464", secretKey);
                        if (photoDataInfo != null)
                        {

                        }

                        if (result != null && result.Any())
                            ApplicationCache.Insert(cacheKey, result, ConfigHelper.GetValueLong("longCacheInterval"));
                    }
                }
                else
                {
                    var cacheData = ApplicationCache.GetValue(cacheKey);
                    if (cacheData != null)
                        result = (PhotoCollection)cacheData;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
            return result;
        }


        /// <summary>
        /// This method is used to get photo sets list
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>PhotosetCollection object</returns>
        public static PhotosetCollection GetFlickrPhotoSets(string userId, int count)
        {
            PhotosetCollection setList = null;
            try
            {
                string cacheKey = "SirSpeedyFlickr-PhotoSetList" + userId + count;
                bool dataExistInCache = ApplicationCache.IsExist(cacheKey);

                if (!dataExistInCache)
                {
                    if (!string.IsNullOrEmpty(userId))
                    {
                        var userImages = GetFlickrImages(userId, 1);
                        setList = flickr.PhotosetsGetList(userId, 1, count);
                        if (setList != null && setList.Any())
                            ApplicationCache.Insert(cacheKey, setList, ConfigHelper.GetValueLong("longCacheInterval"));
                    }
                }
                else
                {
                    var cacheData = ApplicationCache.GetValue(cacheKey);
                    if (cacheData != null)
                        setList = (PhotosetCollection)cacheData;
                }
            }
            catch(Exception ex)
            {
                Log.WriteError(ex);
            }
            return setList;
        }

        /// <summary>
        /// This method is used to get specif phot set data
        /// </summary>
        /// <param name="photoSetId"></param>
        /// <returns>PhotosetPhotoCollection object</returns>
        public static Photoset GetFlickrPhotoSetData(string userId, string photoSetId)
        {
            Photoset photoSetData = null;
            if (!string.IsNullOrEmpty(photoSetId))
            {
                try
                {
                    string cacheKey = "SirSpeedyFlickr-PhotoSetData" + photoSetId;
                    bool dataExistInCache = ApplicationCache.IsExist(cacheKey);

                    if (!dataExistInCache)
                    {
                        var userImages = GetFlickrImages(userId, 1);
                        photoSetData = flickr.PhotosetsGetInfo(photoSetId);
                        if (photoSetData != null)
                            ApplicationCache.Insert(cacheKey, photoSetData, ConfigHelper.GetValueLong("longCacheInterval"));
                    }
                    else
                    {
                        var cacheData = ApplicationCache.GetValue(cacheKey);
                        if (cacheData != null)
                            photoSetData = (Photoset)cacheData;
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }
            return photoSetData;
        }


        /// <summary>
        /// This method is used to get images for a given photo set
        /// </summary>
        /// <param name="photoSetId"></param>
        /// <returns>PhotosetPhotoCollection object</returns>
        public static PhotosetPhotoCollection GetFlickrPhotoSetsImages(string photoSetId)
        {
            PhotosetPhotoCollection photoList = null;
            if (!string.IsNullOrEmpty(photoSetId))
            {
                try
                {
                    photoList = flickr.PhotosetsGetPhotos(photoSetId);
                }
                catch(Exception ex)
                {
                    Log.WriteError(ex);
                }
            }
            return photoList;
        }

        public static PhotoInfo GetFlickrPhotoInfo(string photoId)
        {
            PhotoInfo photoData = null;
            if (!string.IsNullOrEmpty(photoId))
            {
                try
                {
                    photoData = flickr.PhotosGetInfo(photoId);
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }
            return photoData;
        }
    
    }
}