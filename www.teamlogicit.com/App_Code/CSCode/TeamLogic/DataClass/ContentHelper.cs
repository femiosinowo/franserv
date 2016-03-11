using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ektron.Cms;
using Ektron.Cms.Common;
using Ektron.Cms.Content;
using System.Configuration;
using Ektron.Cms.Organization;
using Ektron.Cms.Instrumentation;
using Ektron.Cms.Framework.Organization;
using Ektron.Cms.Framework.Content;
using Ektron.Cms.Framework.Settings.UrlAliasing;

namespace TeamLogic.CMS
{
    /// <summary>
    /// Summary description for ContentHelper
    /// </summary>
    public class ContentHelper
    {
        private static ContentManager contentManager = new ContentManager(Ektron.Cms.Framework.ApiAccessMode.Admin);

        public ContentHelper()
        {
            //constructor
        }

        /// <summary>
        /// TaxonomySortOrder 
        /// </summary>
        public enum ContentSortOrder
        {
            DateCreated,
            DateModified,
            Title
        }
        
        /// <summary>
        /// This method is used to get the content by id
        /// </summary>
        /// <param name="id">content or page id</param>
        /// <param name="returnMetaData">metadata</param>
        /// <returns>ContentData</returns>
        public static ContentData GetContentById(long id, bool returnMetaData = false)
        {
            ContentData cData = contentManager.GetItem(id, returnMetaData);
            return cData;
        }

        /// <summary>
        /// This method is used to get content by ids
        /// </summary>
        /// <param name="ids">content ids</param>
        /// <param name="smartFormId">optional parameter smartformId</param>
        /// <param name="returnMetaData">optional parameter retturn metadata</param>
        /// <returns>ContentData list</returns>
        public static List<ContentData> GetContentByIds(List<long> ids, long smartFormId = 0, bool returnMetaData = false)
        {
            ContentCriteria contentCriteria = new ContentCriteria();
            contentCriteria.AddFilter(ContentProperty.Id, CriteriaFilterOperator.In, ids);
            if (smartFormId > 0)
                contentCriteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, smartFormId);
            if (returnMetaData)
                contentCriteria.ReturnMetadata = true;
            var dataList = contentManager.GetList(contentCriteria);
            return dataList;
        }


        /// <summary>
        /// This method is used to get the staged content by id
        /// </summary>
        /// <param name="id">content or page id</param>
        /// <param name="returnMetaData">metadata</param>
        /// <returns>ContentData</returns>
        public static ContentData GetStagedContentById(long id)
        {           
            Ektron.Cms.API.Content.Content content = new Ektron.Cms.API.Content.Content();
            ContentData cData = content.GetContent(id, ContentAPI.ContentResultType.Staged);           
            return cData;
        }

        /// <summary>
        /// This method is used to get all the content by folder name
        /// </summary>
        /// <param name="folderName">string folder name</param>
        /// <returns>content list object</returns>
        public static List<ContentData> GetContentByFolderName(string folderName)
        {
            Ektron.Cms.Content.ContentCriteria criteria = new Ektron.Cms.Content.ContentCriteria();
			criteria.AddFilter(ContentProperty.FolderName, CriteriaFilterOperator.EqualTo, folderName);
            List<ContentData> listContent = contentManager.GetList(criteria);
            return listContent;
        }
        
        /// <summary>
        /// This method is used to get the content url alias name
        /// </summary>
        /// <param name="contentData">content data object</param>
        /// <returns>alias name</returns>
        public static string GetContentUrlAlias(ContentData contentData)
        {
            try
            {
                CommonAliasManager commonAliasManager = new CommonAliasManager();
                string contentAlias = commonAliasManager.GetContentAlias(contentData.Id);
                if (String.IsNullOrEmpty(contentAlias))
                {
                    contentAlias = contentData.Quicklink;
                }
                return contentAlias;
            }
            catch(Exception ex)
            {
                Log.WriteError(ex);
                return string.Empty;
            }
        }


        /// <summary>
        /// this method is used to get the content alias name by content id
        /// </summary>
        /// <param name="contentId">content id</param>
        /// <returns>alias name</returns>
        public static string GetContentUrlAlias(long contentId)
        {
            CommonAliasManager commonAliasManager = new CommonAliasManager();
            string contentAlias = commonAliasManager.GetContentAlias(contentId);
            return contentAlias;
        }

        /// <summary>
        /// This method is used to get content data list by content criteria
        /// </summary>
        /// <param name="criteria">ContentCriteria</param>
        /// <returns>ContentData list</returns>
        public static List<ContentData> GetListByCriteria(ContentCriteria criteria)
        {
            List<ContentData> listContent = contentManager.GetList(criteria);
            return listContent;
        }


        /// <summary>
        /// This method is used to get content data list by content criteria
        /// </summary>
        /// <param name="criteria">ContentCriteria</param>
        /// <returns>ContentData list</returns>
        public static List<ContentData> GetListByCriteria(ContentTaxonomyCriteria taxCriteria)
        {
            List<ContentData> listContent = contentManager.GetList(taxCriteria);
            return listContent;
        }


        /// <summary>
        /// This method is used to get content data list by content criteria
        /// </summary>
        /// <param name="criteria">ContentCriteria</param>
        /// <returns>ContentData list</returns>
        public static List<ContentData> GetListByCriteria(ContentCollectionCriteria collectionCriteria)
        {
            List<ContentData> listContent = contentManager.GetList(collectionCriteria);
            return listContent;
        }


        /// <summary>
        /// This method is used to get content data list by content meta criteria
        /// </summary>
        /// <param name="criteria">ContentMetadataCriteria</param>
        /// <returns>ContentData list</returns>
        public static List<ContentData> GetListByCriteria(ContentMetadataCriteria criteria)
        {
            List<ContentData> listContent = contentManager.GetList(criteria);
            return listContent;
        }

        /// <summary>
        /// This method is used to Add content data
        /// </summary>
        /// <param name="cData">ContentData</param>
        /// <returns>ContentData</returns>
        public static ContentData AddContent(ContentData cData)
        {
            ContentData resultData = contentManager.Add(cData);
            return resultData;
        }

        /// <summary>
        /// This method is used to Update content data
        /// </summary>
        /// <param name="cData">ContentData</param>
        /// <returns>ContentData</returns>
        public static ContentData UpdateContent(ContentData cData)
        {
            ContentData resultData = contentManager.Update(cData);
            return resultData;
        }

        /// <summary>
        /// This method is used to assign content item to taxonomy
        /// </summary>
        /// <param name="contentId">content id</param>
        /// <param name="taxonomyId">taxonomy id</param>
        public static void AssignTaxonomy(long contentId, long taxonomyId)
        {
            if (contentId > 0 && taxonomyId > 0)
                contentManager.AssignTaxonomy(contentId, taxonomyId);           
        }        


        /// <summary>
        /// This method is used to get only metadata values without gettting the actual content
        /// </summary>
        /// <param name="id">content id</param>
        /// <returns>content metadata properties</returns>
        public static CustomAttributeList GetContentMetaData(long id)
        {
            Ektron.Cms.API.Metadata mapi = new Ektron.Cms.API.Metadata();
            CustomAttributeList cList = mapi.GetContentMetadataList(id);
            return cList;
        }

        /// <summary>
        /// This method is used to get the asset path
        /// </summary>
        /// <param name="cData">asset content data</param>
        /// <returns>string asset path</returns>
        public static string GetAssetPath(ContentData cData)
        {
            string path = "";
            if (cData != null)
            {
                Ektron.Cms.API.Content.Content contentAPI = new Ektron.Cms.API.Content.Content();
                path = contentAPI.EkContentRef.GetViewUrl(cData.ContType, cData.AssetData.Id);//.Replace(Page.Request.Url.Scheme + "://" + Page.Request.Url.Authority, "");
                try
                {
                    if (path != "" && path.ToLower().Contains("assets"))
                        path = path.Remove(0, path.IndexOf("/assets"));
                }
                catch (Exception ex) 
                {
                    Log.WriteError(ex);
                    return string.Empty;
                }
            }
            return path;
        }


        public static long CopyEktContent(long contentId, long folderId, long langid, bool isPublished)
        {
            long cId = 0;
            if (contentId > 0 & folderId > 0)
                cId = contentManager.CopyContent(contentId, folderId, langid, isPublished);
            return cId;
        }

    }
}