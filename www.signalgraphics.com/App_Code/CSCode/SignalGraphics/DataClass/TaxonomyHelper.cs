using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ektron.Cms;
using System.Configuration;
using Ektron.Cms.Organization;
using Ektron.Cms.Framework.Organization;
using Ektron.Cms.Common;
using Ektron.Cms.API.Content;

namespace SignalGraphics.CMS
{
    /// <summary>
    /// Summary description for TaxonomyHelper
    /// </summary>
    public class TaxonomyHelper
    {
        public static TaxonomyManager taxonomyManager = new TaxonomyManager(Ektron.Cms.Framework.ApiAccessMode.Admin);

        public TaxonomyHelper()
        {
            //constructor
        }

        /// <summary>
        /// TaxonomySortOrder 
        /// </summary>
        public enum TaxonomySortOrder
        {            
            DateCreated,
            DateModified,
            Title            
        }

        /// <summary>
        /// This method is used to get taxonomy tree by Id
        /// </summary>
        /// <param name="taxonomyId">taxonomy id</param>
        /// <param name="depth">taxonomy sub levels</param>
        /// <param name="includeItems">include content</param>
        /// <returns>TaxonomyData</returns>
        public static TaxonomyData GetTaxonomyTree(long taxonomyId, Int32 depth, bool includeItems)
        {
            TaxonomyData taxonomyData = taxonomyManager.GetTree(taxonomyId, depth, includeItems, new PagingInfo(), Ektron.Cms.Common.EkEnumeration.TaxonomyType.Content, EkEnumeration.TaxonomyItemsSortOrder.taxonomy_item_display_order);
            return taxonomyData;
        }

        /// <summary>
        /// This method is used to get the taxonomy data only with out content html
        /// </summary>
        /// <param name="taxonomyId">user selected taxonomy Id</param>
        /// <param name="TaxonomySortOrder">the order content should be read from db</param>
        /// <param name="cache">is caching required</param>
        /// <returns>TaxonomyData object</returns>
        public static List<Ektron.Cms.TaxonomyItemData> GetTaxonomyItemDataById(long taxonomyId, TaxonomySortOrder orderType = TaxonomySortOrder.DateModified)
        {
            List<Ektron.Cms.TaxonomyItemData> taxonomyItemList = null;
            if (taxonomyId > 0)
            {
                try
                {
                    TaxonomyItemCriteria criteria = new TaxonomyItemCriteria();
                    criteria.AddFilter(TaxonomyItemProperty.TaxonomyId, CriteriaFilterOperator.EqualTo, taxonomyId);
                    criteria.OrderByDirection = EkEnumeration.OrderByDirection.Descending;

                    switch (orderType)
                    {
                        case TaxonomySortOrder.DateCreated:
                            criteria.OrderByField = TaxonomyItemProperty.DateCreated;
                            break;
                        case TaxonomySortOrder.DateModified:
                            criteria.OrderByField = TaxonomyItemProperty.DateModified;
                            break;
                        case TaxonomySortOrder.Title:
                            criteria.OrderByField = TaxonomyItemProperty.Title;
                            break;
                        default:
                            criteria.OrderByField = TaxonomyItemProperty.Title;
                            break;
                    }

                    TaxonomyItemManager taxonomyItemManager = new TaxonomyItemManager();
                    taxonomyItemList = taxonomyItemManager.GetList(criteria);
                }
                catch (Exception ex)
                {
                    Ektron.Cms.EkException.LogException("Taxonomy helper class method 'GetTaxonomyDataById' throw exception: " + ex.Message);
                    return null;
                }
            }
            return taxonomyItemList;
        }


        /// <summary>
        /// This method is used to get taxonomy by Id
        /// </summary>
        /// <param name="taxonomyId">taxonomy id</param>
        /// <returns>taxonomy data</returns>
        public static TaxonomyData GetItem(long taxonomyId)
        {
            TaxonomyData tData = taxonomyManager.GetItem(taxonomyId);
            return tData;
        }


        /// <summary>
        /// This method is used to get taxonomy data by criteria object
        /// </summary>
        /// <param name="taxonomyCriteria">TaxonomyCriteria obj</param>
        /// <returns>taxonomyData as list</returns>
        public static List<TaxonomyData> GetTaxonomyData(TaxonomyCriteria taxonomyCriteria)
        {
            List<TaxonomyData> dataList= null;
            if (taxonomyCriteria != null)
            {
                dataList = taxonomyManager.GetList(taxonomyCriteria);
            }
            return dataList;
        } 
       

        /// <summary>
        /// This method is used to get the Taxonomy name for a given content item
        /// </summary>
        /// <param name="contentId"></param>
        /// <returns></returns>
        public static string GetTaxonomyNameByContentId(long contentId)
        {
            string taxonomyName = string.Empty;
            if (contentId > 0)
            {
                string cacheKey = "SirSpeedy-GetTaxonomyNameByContentId" + contentId;
                bool dataExistInCache = ApplicationCache.IsExist(cacheKey);

                if (!dataExistInCache)
                {
                    Taxonomy taxonomyAPI = new Taxonomy();
                    var taxonomyBaseData = taxonomyAPI.ReadAllAssignedCategory(contentId);
                    if (taxonomyBaseData != null && taxonomyBaseData.Length > 0)
                    {
                        taxonomyName = taxonomyBaseData[0].Name;
                        ApplicationCache.Insert(cacheKey, taxonomyName, ConfigHelper.GetValueLong("longCacheInterval"));
                    }
                }
                else
                {
                    var cacheData = ApplicationCache.GetValue(cacheKey);
                    if (cacheData != null)
                        taxonomyName = (string)cacheData;
                }
            }
            return taxonomyName;
        }
        
        public static long GetTaxonomyIdByContentId(long contentId)
        {
            long taxonomyId = 0;
            if (contentId > 0)
            {
                Taxonomy taxonomyAPI = new Taxonomy();
                var taxonomyBaseData = taxonomyAPI.ReadAllAssignedCategory(contentId);
                if (taxonomyBaseData != null && taxonomyBaseData.Length > 0)
                {
                    taxonomyId = taxonomyBaseData[0].Id;
                }
            }
            return taxonomyId;
        }    
    
    }
}