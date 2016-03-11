using Ektron.Cms;
using Ektron.Cms.Common;
using Ektron.Cms.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SirSpeedy.CMS
{
    /// <summary>
    /// Summary description for M3TDataManager
    /// </summary>
    public class M3TDataManager
    {
        public M3TDataManager()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// This method is used to get the List of Products and Services Content Data for Nation or Local
        /// </summary>
        /// <returns></returns>
        public static List<ContentData> GetProductAndServices()
        {
            return SiteDataManager.GetProductAndServices();
        }

        public static List<ContentData> GetProductAndServicesSideContent(bool refreshCache = false)
        {
            List<ContentData> dataList = null;
            long folderID = ConfigHelper.GetValueLong("ProductsAndServicesMegamenuFolderID");
            long PSSmartFormXMLConfig = ConfigHelper.GetValueLong("ProductsAndServicesMMSideSmartFormID");
            string cacheKey = "Sirspeedy.M3T.GetProductAndServicesSideContent";
            ContentCriteria criteria = new ContentCriteria();
            if (FransDataManager.IsFranchiseSelected())
            {
                string fransId = FransDataManager.GetFranchiseId();
                cacheKey = cacheKey + fransId;
                dataList = HttpContext.Current.Cache[cacheKey] as List<ContentData>;
                if (dataList == null || dataList.Count == 0 || refreshCache)
                {
                    //for now display the side content same as national
                    criteria.AddFilter(ContentProperty.FolderId, CriteriaFilterOperator.EqualTo, folderID);
                    criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, PSSmartFormXMLConfig);
                    dataList = ContentHelper.GetListByCriteria(criteria);

                    if (dataList != null && dataList.Count > 0)
                        HttpContext.Current.Cache.Insert(cacheKey, dataList, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            else
            {
                dataList = HttpContext.Current.Cache[cacheKey] as List<ContentData>;
                if (dataList == null || dataList.Count == 0 || refreshCache)
                {
                    criteria.AddFilter(ContentProperty.FolderId, CriteriaFilterOperator.EqualTo, folderID);
                    criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, PSSmartFormXMLConfig);
                    dataList = ContentHelper.GetListByCriteria(criteria);

                    if (dataList != null && dataList.Count > 0)
                        HttpContext.Current.Cache.Insert(cacheKey, dataList, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            return dataList;
        }

        public static List<ContentData> GetCompanyInfoContent(bool refreshCache = false)
        {
            List<ContentData> dataList = null;
            long folderID = ConfigHelper.GetValueLong("NationalCompanyInfoFolderID");
            long SmartFormXMLConfig = ConfigHelper.GetValueLong("CompanyInfoSmartFormID");
            string cacheKey = "Sirspeedy.M3T.GetCompanyInfoContent";
            if (FransDataManager.IsFranchiseSelected())
            {
                string fransId = FransDataManager.GetFranchiseId();
                cacheKey = cacheKey + fransId;
                dataList = HttpContext.Current.Cache[cacheKey] as List<ContentData>;
                if (dataList == null || dataList.Count == 0 || refreshCache)
                {
                    //get the national about us content
                    ContentCriteria criteria = new ContentCriteria();
                    criteria.AddFilter(ContentProperty.FolderId, CriteriaFilterOperator.EqualTo, folderID);
                    criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, SmartFormXMLConfig);
                    dataList = ContentHelper.GetListByCriteria(criteria);

                    if (dataList != null && dataList.Count > 0)
                        HttpContext.Current.Cache.Insert(cacheKey, dataList, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            else
            {
                dataList = HttpContext.Current.Cache[cacheKey] as List<ContentData>;
                if (dataList == null || dataList.Count == 0 || refreshCache)
                {
                    ContentCriteria criteria = new ContentCriteria();
                    criteria.AddFilter(ContentProperty.FolderId, CriteriaFilterOperator.EqualTo, folderID);
                    criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, SmartFormXMLConfig);
                    dataList = ContentHelper.GetListByCriteria(criteria);

                    if (dataList != null && dataList.Count > 0)
                        HttpContext.Current.Cache.Insert(cacheKey, dataList, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            return dataList;
        }

        public static List<ContentData> GetPartners(bool refreshCache = false)
        {
            List<ContentData> dataList = null;
            long partnersTaxId = ConfigHelper.GetValueLong("NationalPartnersTaxId");
            long partnersSFId = ConfigHelper.GetValueLong("PartnersSmartFormID");
            string cacheKey = "Sirspeedy.M3T.GetPartners";
            if (FransDataManager.IsFranchiseSelected())
            {
                string fransId = FransDataManager.GetFranchiseId();
                cacheKey = cacheKey + fransId;
                dataList = HttpContext.Current.Cache[cacheKey] as List<ContentData>;
                if (dataList == null || dataList.Count == 0 || refreshCache)
                {
                    List<long> cIds = new List<long>();
                    var workAreaData = FransDataManager.GetFransWorkareaData();
                    if (workAreaData != null)
                    {
                        cIds = workAreaData.PartnersContentIds;
                        if (cIds != null && cIds.Any())
                        {
                            var contentCriteria = new ContentCriteria();
                            //var contentCriteria = new ContentCriteria(ContentProperty.DateModified, EkEnumeration.OrderByDirection.Descending);
                            contentCriteria.AddFilter(ContentProperty.Id, CriteriaFilterOperator.In, cIds);
                            contentCriteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, partnersSFId);
                            contentCriteria.OrderByField = ContentProperty.DateModified;
                            dataList = ContentHelper.GetListByCriteria(contentCriteria);
                            //if (dataList != null && dataList.Any())
                            //    dataList = dataList.Take(count).ToList();
                        }
                    }

                    if (dataList != null && dataList.Count > 0)
                        HttpContext.Current.Cache.Insert(cacheKey, dataList, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            else
            {
                dataList = HttpContext.Current.Cache[cacheKey] as List<ContentData>;
                if (dataList == null || dataList.Count == 0 || refreshCache)
                {
                    var taxItems = TaxonomyHelper.GetTaxonomyTree(partnersTaxId, 1, true);
                    List<long> cIds = new List<long>();
                    if (taxItems != null && taxItems.TaxonomyItems != null)
                    {
                        foreach (var tItem in taxItems.TaxonomyItems)
                        {
                            cIds.Add(tItem.Id);
                        }
                    }

                    var cfc = new ContentCriteria();
                    cfc.AddFilter(ContentProperty.Id, CriteriaFilterOperator.In, cIds);
                    cfc.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, partnersSFId);
                    //cfc.PagingInfo = new Ektron.Cms.PagingInfo(FransDataManager.GetCustomApiPageSize(), 1);
                    var dList = ContentHelper.GetListByCriteria(cfc);
                    if (dList != null && dList.Any())
                    {
                        dataList = new List<ContentData>();
                        foreach (var id in cIds)
                        {
                            var cData = dList.Where(x => x.Id == id).FirstOrDefault();
                            if (cData != null)
                                dataList.Add(cData);
                        }
                    }

                    //var ctc = new ContentTaxonomyCriteria(ContentProperty.DateModified, EkEnumeration.OrderByDirection.Descending);
                    //ctc.AddFilter(partnersTaxId);
                    //ctc.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, partnersSFId);
                    //dataList = ContentHelper.GetListByCriteria(ctc);
                    ////if (dataList != null && dataList.Any())
                    ////    dataList = dataList.Take(count).ToList();

                    if (dataList != null && dataList.Count > 0)
                        HttpContext.Current.Cache.Insert(cacheKey, dataList, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
                }

            }
            return dataList;
        }

        public static List<ContentData> GetManagementTeamContent(bool refreshCache = false)
        {
            List<ContentData> dataList = null;
            long managementTeamTaxId = ConfigHelper.GetValueLong("ManagementTeamNationalTaxId");
            long managementTeamSFId = ConfigHelper.GetValueLong("ManagementTeamSmartFormID");
            string cacheKey = "Sirspeedy.M3T.GetManagementTeamContent";
            if (FransDataManager.IsFranchiseSelected())
            {
                //commenting out the local team code bez the employees are not managed inside Ektron workarea

                //List<long> cIds = new List<long>();
                //var workAreaData = FransDataManager.GetFransWorkareaData();
                //if (workAreaData != null)
                //{
                //    cIds = workAreaData.CenterEmployeesTeam;
                //    if (cIds != null && cIds.Any())
                //    {
                //        var contentCriteria = new ContentCriteria(ContentProperty.DateModified, EkEnumeration.OrderByDirection.Descending);
                //        contentCriteria.AddFilter(ContentProperty.Id, CriteriaFilterOperator.In, cIds);
                //        contentCriteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, managementTeamSFId);
                //        contentCriteria.OrderByField = ContentProperty.DateModified;
                //        dataList = ContentHelper.GetListByCriteria(contentCriteria);
                //        if (dataList != null && dataList.Any())
                //            dataList = dataList.ToList();
                //    }
                //}
            }
            else
            {
                dataList = HttpContext.Current.Cache[cacheKey] as List<ContentData>;
                if (dataList == null || dataList.Count == 0 || refreshCache)
                {

                    var taxItems = TaxonomyHelper.GetTaxonomyTree(managementTeamTaxId, 1, true);
                    List<long> cIds = new List<long>();
                    if (taxItems != null && taxItems.TaxonomyItems != null)
                    {
                        foreach (var tItem in taxItems.TaxonomyItems)
                        {
                            cIds.Add(tItem.Id);
                        }
                    }

                    var cfc = new ContentCriteria();
                    cfc.AddFilter(ContentProperty.Id, CriteriaFilterOperator.In, cIds);
                    cfc.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, managementTeamSFId);
                    //cfc.PagingInfo = new Ektron.Cms.PagingInfo(FransDataManager.GetCustomApiPageSize(), 1);
                    var dList = ContentHelper.GetListByCriteria(cfc);
                    if (dList != null && dList.Any())
                    {
                        dataList = new List<ContentData>();
                        foreach (var id in cIds)
                        {
                            var cData = dList.Where(x => x.Id == id).FirstOrDefault();
                            if (cData != null)
                                dataList.Add(cData);
                        }
                    }

                    //var ctc = new ContentTaxonomyCriteria(ContentProperty.DateModified, EkEnumeration.OrderByDirection.Descending);
                    //ctc.AddFilter(managementTeamTaxId);
                    //ctc.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, managementTeamSFId);
                    //dataList = ContentHelper.GetListByCriteria(ctc);
                    //if (dataList != null && dataList.Any())
                    //    dataList = dataList.ToList();


                    if (dataList != null && dataList.Count > 0)
                        HttpContext.Current.Cache.Insert(cacheKey, dataList, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            return dataList;
        }

        public static List<ContentData> GetNews(int count = 2, bool refreshCache = false)
        {
            List<ContentData> dataList = null;
            long newsTaxId = ConfigHelper.GetValueLong("AllNewsNationalTaxId");
            long newsSFId = ConfigHelper.GetValueLong("NewsSmartFormID");
            string cacheKey = "Sirspeedy.M3T.GetNews" + count;
            if (FransDataManager.IsFranchiseSelected())
            {
                string fransId = FransDataManager.GetFranchiseId();
                cacheKey = cacheKey + fransId;
                dataList = HttpContext.Current.Cache[cacheKey] as List<ContentData>;
                if (dataList == null || dataList.Count == 0 || refreshCache)
                {
                    List<long> cIds = new List<long>();
                    var workAreaData = FransDataManager.GetFransWorkareaData();
                    if (workAreaData != null)
                    {
                        cIds = workAreaData.NewsContentIds;
                        if (cIds != null && cIds.Any())
                        {
                            var contentCriteria = new ContentCriteria(ContentProperty.DateModified, EkEnumeration.OrderByDirection.Descending);
                            contentCriteria.AddFilter(ContentProperty.Id, CriteriaFilterOperator.In, cIds);
                            contentCriteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, newsSFId);
                            contentCriteria.OrderByField = ContentProperty.DateModified;
                            dataList = ContentHelper.GetListByCriteria(contentCriteria);
                            if (dataList != null && dataList.Any())
                                dataList = dataList.ToList();
                        }
                    }

                    if (dataList != null && dataList.Count > 0)
                        HttpContext.Current.Cache.Insert(cacheKey, dataList, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            else
            {
                dataList = HttpContext.Current.Cache[cacheKey] as List<ContentData>;
                if (dataList == null || dataList.Count == 0 || refreshCache)
                {
                    var ctc = new ContentTaxonomyCriteria(ContentProperty.DateModified, EkEnumeration.OrderByDirection.Descending);
                    ctc.AddFilter(newsTaxId);
                    ctc.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, newsSFId);
                    dataList = ContentHelper.GetListByCriteria(ctc);
                    if (dataList != null && dataList.Any())
                        dataList = dataList.ToList();

                    if (dataList != null && dataList.Count > 0)
                        HttpContext.Current.Cache.Insert(cacheKey, dataList, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            return dataList;
        }

        public static List<ContentData> GetInTheMediaContent(bool refreshCache = false)
        {
            List<ContentData> dataList = null;
            long inTheMediaTaxId = ConfigHelper.GetValueLong("AllInTheMediaNationalTaxId");
            long inTheMediaSFId = ConfigHelper.GetValueLong("InTheMediaSmartFormID");
            string cacheKey = "Sirspeedy.M3T.GetInTheMediaContent";
            if (FransDataManager.IsFranchiseSelected())
            {
                string fransId = FransDataManager.GetFranchiseId();
                cacheKey = cacheKey + fransId;
                dataList = HttpContext.Current.Cache[cacheKey] as List<ContentData>;
                if (dataList == null || dataList.Count == 0 || refreshCache)
                {
                    List<long> cIds = new List<long>();
                    var workAreaData = FransDataManager.GetFransWorkareaData();
                    if (workAreaData != null && workAreaData.InTheMediaContentIds != null && workAreaData.InTheMediaContentIds.Any())
                    {
                        cIds = workAreaData.InTheMediaContentIds;
                        if (cIds != null && cIds.Any())
                        {
                            var contentCriteria = new ContentCriteria(ContentProperty.DateModified, EkEnumeration.OrderByDirection.Descending);
                            contentCriteria.AddFilter(ContentProperty.Id, CriteriaFilterOperator.In, cIds);
                            contentCriteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, inTheMediaSFId);
                            contentCriteria.OrderByField = ContentProperty.DateModified;
                            dataList = ContentHelper.GetListByCriteria(contentCriteria);
                            if (dataList != null && dataList.Any())
                                dataList = dataList.ToList();
                        }
                    }

                    if (dataList != null && dataList.Count > 0)
                        HttpContext.Current.Cache.Insert(cacheKey, dataList, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            else
            {
                dataList = HttpContext.Current.Cache[cacheKey] as List<ContentData>;
                if (dataList == null || dataList.Count == 0 || refreshCache)
                {
                    var ctc = new ContentTaxonomyCriteria(ContentProperty.DateModified, EkEnumeration.OrderByDirection.Descending);
                    ctc.AddFilter(inTheMediaTaxId);
                    ctc.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, inTheMediaSFId);
                    dataList = ContentHelper.GetListByCriteria(ctc);
                    if (dataList != null && dataList.Any())
                        dataList = dataList.ToList();

                    if (dataList != null && dataList.Count > 0)
                        HttpContext.Current.Cache.Insert(cacheKey, dataList, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            return dataList;
        }

        public static List<ContentData> GetBriefsAndWhitePapersContent(bool refreshCache = false)
        {
            List<ContentData> dataList = null;
            long briefWhitepapersTaxId = ConfigHelper.GetValueLong("BriefAndWhitepapersNationalTaxId");
            long briefWhitepapersSFId = ConfigHelper.GetValueLong("BriefsAndWhitepapersSmartFormID");
            string cacheKey = "Sirspeedy.M3T.GetBriefsAndWhitePapersContent";
            if (FransDataManager.IsFranchiseSelected())
            {
                string fransId = FransDataManager.GetFranchiseId();
                cacheKey = cacheKey + fransId;
                dataList = HttpContext.Current.Cache[cacheKey] as List<ContentData>;
                if (dataList == null || dataList.Count == 0 || refreshCache)
                {
                    List<long> cIds = new List<long>();
                    var workAreaData = FransDataManager.GetFransWorkareaData();
                    if (workAreaData != null && workAreaData.BriefsWhitePapersContentIds != null && workAreaData.BriefsWhitePapersContentIds.Any())
                    {
                        cIds = workAreaData.BriefsWhitePapersContentIds;
                        if (cIds != null && cIds.Any())
                        {
                            var contentCriteria = new ContentCriteria(ContentProperty.DateModified, EkEnumeration.OrderByDirection.Descending);
                            contentCriteria.AddFilter(ContentProperty.Id, CriteriaFilterOperator.In, cIds);
                            contentCriteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, briefWhitepapersSFId);
                            contentCriteria.OrderByField = ContentProperty.DateModified;
                            dataList = ContentHelper.GetListByCriteria(contentCriteria);
                            if (dataList != null && dataList.Any())
                                dataList = dataList.ToList();
                        }
                    }

                    if (dataList != null && dataList.Count > 0)
                        HttpContext.Current.Cache.Insert(cacheKey, dataList, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            else
            {
                dataList = HttpContext.Current.Cache[cacheKey] as List<ContentData>;
                if (dataList == null || dataList.Count == 0 || refreshCache)
                {
                    var ctc = new ContentTaxonomyCriteria(ContentProperty.DateModified, EkEnumeration.OrderByDirection.Descending);
                    ctc.AddFilter(briefWhitepapersTaxId);
                    ctc.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, briefWhitepapersSFId);
                    dataList = ContentHelper.GetListByCriteria(ctc);
                    if (dataList != null && dataList.Any())
                        dataList = dataList.ToList();

                    if (dataList != null && dataList.Count > 0)
                        HttpContext.Current.Cache.Insert(cacheKey, dataList, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            return dataList;
        }

        public static List<ContentData> GetCaseStudiesContent(bool refreshCache = false)
        {
            List<ContentData> dataList = null;
            long caseStudiesTaxId = ConfigHelper.GetValueLong("CaseStudiesNationalTaxId");
            long caseStudiesSFId = ConfigHelper.GetValueLong("CaseStudiesSmartFormID");
            string cacheKey = "Sirspeedy.M3T.GetCaseStudiesContent";
            if (FransDataManager.IsFranchiseSelected())
            {
                string fransId = FransDataManager.GetFranchiseId();
                cacheKey = cacheKey + fransId;
                dataList = HttpContext.Current.Cache[cacheKey] as List<ContentData>;
                if (dataList == null || dataList.Count == 0 || refreshCache)
                {
                    List<long> cIds = new List<long>();
                    var workAreaData = FransDataManager.GetFransWorkareaData();
                    if (workAreaData != null && workAreaData.CaseStudiesContentIds != null && workAreaData.CaseStudiesContentIds.Any())
                    {
                        cIds = workAreaData.CaseStudiesContentIds;
                        if (cIds != null && cIds.Any())
                        {
                            var contentCriteria = new ContentCriteria(ContentProperty.DateModified, EkEnumeration.OrderByDirection.Descending);
                            contentCriteria.AddFilter(ContentProperty.Id, CriteriaFilterOperator.In, cIds);
                            contentCriteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, caseStudiesSFId);
                            contentCriteria.OrderByField = ContentProperty.DateModified;
                            dataList = ContentHelper.GetListByCriteria(contentCriteria);
                            if (dataList != null && dataList.Any())
                                dataList = dataList.ToList();
                        }
                    }

                    if (dataList != null && dataList.Count > 0)
                        HttpContext.Current.Cache.Insert(cacheKey, dataList, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            else
            {
                dataList = HttpContext.Current.Cache[cacheKey] as List<ContentData>;
                if (dataList == null || dataList.Count == 0 || refreshCache)
                {
                    var ctc = new ContentTaxonomyCriteria(ContentProperty.DateModified, EkEnumeration.OrderByDirection.Descending);
                    ctc.AddFilter(caseStudiesTaxId);
                    ctc.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, caseStudiesSFId);
                    dataList = ContentHelper.GetListByCriteria(ctc);
                    if (dataList != null && dataList.Any())
                        dataList = dataList.ToList();

                    if (dataList != null && dataList.Count > 0)
                        HttpContext.Current.Cache.Insert(cacheKey, dataList, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            return dataList;
        }

        public static List<ContentData> GetJobProfiles(bool refreshCache = false)
        {
            List<ContentData> dataList = null;
            long folderID = ConfigHelper.GetValueLong("DepartmentsFolderID");//("JobProfilesFolderID");
            long SmartFormXMLConfig = ConfigHelper.GetValueLong("orgDepartmentSmartFormID");
            string cacheKey = "Sirspeedy.M3T.GetJobProfiles";
            if (FransDataManager.IsFranchiseSelected())
            {
                string fransId = FransDataManager.GetFranchiseId();
                cacheKey = cacheKey + fransId;
                dataList = HttpContext.Current.Cache[cacheKey] as List<ContentData>;
                if (dataList == null || dataList.Count == 0 || refreshCache)
                {
                    //todo: for now show the profiles on the local site also
                    ContentCriteria criteria = new ContentCriteria();
                    criteria.AddFilter(ContentProperty.FolderId, CriteriaFilterOperator.EqualTo, folderID);
                    criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, SmartFormXMLConfig);
                    dataList = ContentHelper.GetListByCriteria(criteria);

                    if (dataList != null && dataList.Count > 0)
                        HttpContext.Current.Cache.Insert(cacheKey, dataList, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            else
            {
                dataList = HttpContext.Current.Cache[cacheKey] as List<ContentData>;
                if (dataList == null || dataList.Count == 0 || refreshCache)
                {
                    ContentCriteria criteria = new ContentCriteria();
                    criteria.AddFilter(ContentProperty.FolderId, CriteriaFilterOperator.EqualTo, folderID);
                    criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, SmartFormXMLConfig);
                    dataList = ContentHelper.GetListByCriteria(criteria);

                    if (dataList != null && dataList.Count > 0)
                        HttpContext.Current.Cache.Insert(cacheKey, dataList, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            return dataList;
        }

        public static long GetSecondaryNavId()
        {
            long menuId = 0;
            if (FransDataManager.IsFranchiseSelected())
                menuId = ConfigHelper.GetValueLong("LocalSecondaryNavId");
            else
                menuId = ConfigHelper.GetValueLong("NationalSecondaryNavId");
            return menuId;
        }
    }
}