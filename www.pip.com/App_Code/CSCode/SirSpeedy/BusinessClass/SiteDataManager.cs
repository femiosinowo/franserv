using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ektron.Cms;
using Ektron.Cms.Content;
using Ektron.Cms.Common;
using System.Xml.Linq;
using System.Xml.XPath;
using Ektron.Cms.Instrumentation;
using FlickrNet;
using System.Configuration;
using Ektron.Cms.Organization;
using System.Xml;

namespace SirSpeedy.CMS
{
    /// <summary>
    /// Summary description for SiteDataManager
    /// </summary>
    public class SiteDataManager
    {
        public SiteDataManager()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// This method is used to get the National company info
        /// </summary>
        /// <returns></returns>
        public static AboutNational GetNationalCompanyInfo(bool refreshCache = false)
        {
            string cacheKey = "PIP.SiteDataManager.GetNationalCompanyInfo";
            AboutNational aboutNationalData = HttpContext.Current.Cache[cacheKey] as AboutNational;

            if (aboutNationalData == null || refreshCache)
            {
                long cId = ConfigHelper.GetValueLong("AboutSirSpeedyCId");
                long SmartFormXMLConfig = ConfigHelper.GetValueLong("CompanyInfoSmartFormID");

                ContentCriteria criteria = new ContentCriteria();
                criteria.AddFilter(ContentProperty.Id, CriteriaFilterOperator.EqualTo, cId);
                criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, SmartFormXMLConfig);
                var contents = ContentHelper.GetListByCriteria(criteria);

                if (contents != null && contents.Count > 0)
                {
                    var contentData = contents.SingleOrDefault();
                    aboutNationalData = new AboutNational();
                    XmlDocument contentXML = new XmlDocument();
                    contentXML.LoadXml(contentData.Html);
                    XmlNodeList xnList = contentXML.SelectNodes("/root");

                    if (xnList[0]["title"] != null)
                        aboutNationalData.Title = xnList[0]["title"].InnerXml;
                    if (xnList[0]["teaser"] != null)
                        aboutNationalData.Teaser = xnList[0]["teaser"].InnerXml;
                    if (xnList[0]["contentImage/img"] != null)
                        aboutNationalData.ContentImagePath = xnList[0]["contentImage/img"].Attributes["src"].Value;
                    if (xnList[0]["content"] != null)
                        aboutNationalData.Description = xnList[0]["content"].InnerXml;
                    if (xnList[0]["tagline"] != null)
                        aboutNationalData.Tagline = xnList[0]["tagline"].InnerXml;
                    if (xnList[0]["statement"] != null)
                        aboutNationalData.Statement = xnList[0]["statement"].InnerXml;
                    if (xnList[0]["videoSRC/a"] != null)
                        aboutNationalData.VideoSRC = xnList[0]["videoSRC/a"].Attributes["href"].Value;
                    if (xnList[0]["disclaimer"] != null)
                        aboutNationalData.Disclaimer = xnList[0]["disclaimer"].InnerXml;
                    if (xnList[0]["companyLogo/img"] != null)
                        aboutNationalData.CompanyLogoImgPath = xnList[0]["companyLogo/img"].Attributes["src"].Value;
                    if (xnList[0]["address1"] != null)
                        aboutNationalData.AddressLine1 = xnList[0]["address1"].InnerXml;
                    if (xnList[0]["address2"] != null)
                        aboutNationalData.AddressLine2 = xnList[0]["address2"].InnerXml;
                    if (xnList[0]["city"] != null)
                        aboutNationalData.City = xnList[0]["city"].InnerXml;
                    if (xnList[0]["state"] != null)
                        aboutNationalData.State = xnList[0]["state"].InnerXml;
                    if (xnList[0]["zipcode"] != null)
                        aboutNationalData.Zipcode = xnList[0]["zipcode"].InnerXml;
                    if (xnList[0]["phone"] != null)
                        aboutNationalData.Phone = xnList[0]["phone"].InnerXml;
                    if (xnList[0]["fax"] != null)
                        aboutNationalData.Fax = xnList[0]["fax"].InnerXml;
                    if (xnList[0]["email"] != null)
                        aboutNationalData.GeneralEmail = xnList[0]["email"].InnerXml;
                    if (xnList[0]["requestAQuoteEmail"] != null)
                        aboutNationalData.RequestToQuoteEmail = xnList[0]["requestAQuoteEmail"].InnerXml;
                    if (xnList[0]["sendAFileEmail"] != null)
                        aboutNationalData.SendAFileEmail = xnList[0]["sendAFileEmail"].InnerXml;
                    if (xnList[0]["whitePaperDownloadEmail"] != null)
                        aboutNationalData.WhilePaperDownloadEmail = xnList[0]["whitePaperDownloadEmail"].InnerXml;
                    if (xnList[0]["subscriptionEmail"] != null)
                        aboutNationalData.SubscriptionEmail = xnList[0]["subscriptionEmail"].InnerXml;
                    if (xnList[0]["daysOperation"] != null)
                        aboutNationalData.DaysOfOperation = xnList[0]["daysOperation"].InnerXml;
                    if (xnList[0]["hoursOperation"] != null)
                        aboutNationalData.HoursOfOperation = xnList[0]["hoursOperation"].InnerXml;
                    if (xnList[0]["url/a"] != null)
                        aboutNationalData.URL = xnList[0]["url/a"].Attributes["href"].Value;
                    if (xnList[0]["SEOBrandName"] != null)
                        aboutNationalData.SEOMetaTitleBrandName = xnList[0]["SEOBrandName"].InnerXml;

                    if (xnList[0]["CenterBannerTitle"] != null)
                        aboutNationalData.LocalCenterBannerTitle = xnList[0]["CenterBannerTitle"].InnerXml;
                    if (xnList[0]["CenterBannerSubTitle"] != null)
                        aboutNationalData.LocalCenterBannerSubTitle = xnList[0]["CenterBannerSubTitle"].InnerXml;
                    if (xnList[0]["CenterContentTitle"] != null)
                        aboutNationalData.LocalCenterContentTitle = xnList[0]["CenterContentTitle"].InnerXml;
                    if (xnList[0]["CenterContentTagLine"] != null)
                        aboutNationalData.LocalCenterContentTagLine = xnList[0]["CenterContentTagLine"].InnerXml;
                }
                if (aboutNationalData != null)
                    HttpContext.Current.Cache.Insert(cacheKey, aboutNationalData, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
            }
            return aboutNationalData;
        }


        /// <summary>
        /// This method is used to retrieve banner content data for Local/National
        /// </summary>
        /// <returns>banner content list</returns>
        public static List<ContentData> GetBannerContent(bool refreshCache = false)
        {
            List<ContentData> dataList = null;
            long bannerSmartFormId = ConfigHelper.GetValueLong("HomeBannerSmartFormID");
            string cacheKey = "PIP.GetBannerContent";
            if (FransDataManager.IsFranchiseSelected())
            {
                string fransId = FransDataManager.GetFranchiseId();
                cacheKey = cacheKey + fransId;
                dataList = HttpContext.Current.Cache[cacheKey] as List<ContentData>;
                if (dataList == null || dataList.Count == 0 || refreshCache)
                {
                    var bannerIds = FransDataManager.GetFransBannerIds();
                    if (bannerIds != null && bannerIds.Any())
                    {
                        var cfc = new ContentCriteria();
                        cfc.AddFilter(ContentProperty.Id, CriteriaFilterOperator.In, bannerIds);
                        cfc.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, bannerSmartFormId);
                        cfc.PagingInfo = new Ektron.Cms.PagingInfo(FransDataManager.GetCustomApiPageSize(), 1);
                        var dList = ContentHelper.GetListByCriteria(cfc);

                        ///sort again bez Ektorn api is automatically getting sorted by ContentId.
                        if (dList != null && dList.Any())
                        {
                            dataList = new List<ContentData>();
                            foreach (var id in bannerIds)
                            {
                                var cData = dList.Where(x => x.Id == id).FirstOrDefault();
                                if (cData != null)
                                    dataList.Add(cData);
                            }
                        }
                    }
                    else
                    {
                        dataList = AdminDataManager.GetNationalBannerContent(bannerSmartFormId);
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
                    dataList = AdminDataManager.GetNationalBannerContent(bannerSmartFormId);
                    if (dataList != null && dataList.Count > 0)
                        HttpContext.Current.Cache.Insert(cacheKey, dataList, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            return dataList;
        }
        
        /// <summary>
        /// This method is used to retrieve partners content data for Local/National
        /// </summary>
        /// <returns></returns>
        public static List<ContentData> GetPartnersContent(bool refreshCache = false)
        {
            List<ContentData> dataList = null;
            long partnersSFId = ConfigHelper.GetValueLong("PartnersSmartFormID");
            string cacheKey = "PIP.GetPartnersContent";
            if (FransDataManager.IsFranchiseSelected())
            {
                string fransId = FransDataManager.GetFranchiseId();
                cacheKey = cacheKey + fransId;
                dataList = HttpContext.Current.Cache[cacheKey] as List<ContentData>;
                if (dataList == null || dataList.Count == 0 || refreshCache)
                {
                    var ids = FransDataManager.GetFransPartnersIds();
                    if (ids != null && ids.Any())
                    {
                        var cfc = new ContentCriteria();
                        cfc.AddFilter(ContentProperty.Id, CriteriaFilterOperator.In, ids);
                        cfc.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, partnersSFId);
                        cfc.PagingInfo = new Ektron.Cms.PagingInfo(FransDataManager.GetCustomApiPageSize(), 1);
                        var dList = ContentHelper.GetListByCriteria(cfc);

                        ///sort again bez Ektorn api is automatically getting sorted by ContentId.
                        if (dList != null && dList.Any())
                        {
                            dataList = new List<ContentData>();
                            foreach (var id in ids)
                            {
                                var cData = dList.Where(x => x.Id == id).FirstOrDefault();
                                if (cData != null)
                                    dataList.Add(cData);
                            }
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
                    dataList = AdminDataManager.GetNationalPartnersContent(partnersSFId);
                    if (dataList != null && dataList.Count > 0)
                        HttpContext.Current.Cache.Insert(cacheKey, dataList, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            return dataList;
        }

        /// <summary>
        /// This method is used to retrieve Case Studies content data for Local/National
        /// </summary>
        /// <returns></returns>
        public static List<ContentData> GetCaseStudiesContentForHomePage(bool refreshCache = false)
        {
            List<ContentData> dataList = null;
            long caseStudiesSFId = ConfigHelper.GetValueLong("CaseStudiesSmartFormID");
            string cacheKey = "PIP.GetCaseStudiesContentForHomePage";
            if (FransDataManager.IsFranchiseSelected())
            {
                string fransId = FransDataManager.GetFranchiseId();
                cacheKey = cacheKey + fransId;
                dataList = HttpContext.Current.Cache[cacheKey] as List<ContentData>;
                if (dataList == null || dataList.Count == 0 || refreshCache)
                {
                    List<long> ids = new List<long>();
                    var workareaData = FransDataManager.GetFransWorkareaData();
                    if (workareaData != null)
                    {
                        ids = workareaData.CaseStudiesContentIds;
                    }

                    if (ids != null && ids.Any())
                        dataList = ContentHelper.GetContentByIds(ids, caseStudiesSFId);
                    else
                        dataList = AdminDataManager.GetNationalHomePage_CaseStudiesContent(caseStudiesSFId);

                    if (dataList != null && dataList.Count > 0)
                        HttpContext.Current.Cache.Insert(cacheKey, dataList, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            else
            {
                dataList = HttpContext.Current.Cache[cacheKey] as List<ContentData>;
                if (dataList == null || dataList.Count == 0 || refreshCache)
                {
                    dataList = AdminDataManager.GetNationalHomePage_CaseStudiesContent(caseStudiesSFId);
                    if (dataList != null && dataList.Count > 0)
                        HttpContext.Current.Cache.Insert(cacheKey, dataList, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            return dataList;
        }
        

        /// <summary>
        /// This method is used to retrieve social media icons link
        /// </summary>
        /// <returns></returns>
        public static SocialMedia GetSocialMediaLinks()
        {
            SocialMedia sMediaData = null;
            var nationalMediaLinks = AdminDataManager.GetNationalSocialMediaLinks();
            if (FransDataManager.IsFranchiseSelected())
            {
                sMediaData = FransDataManager.GetFransSocialMediaLinks(nationalMediaLinks);                
            }
            else
            {
                sMediaData = nationalMediaLinks;
            }
            return sMediaData;
        }


        /// <summary>
        /// This method is used to get flickr data
        /// </summary>
        /// <returns></returns>
        public static PhotoCollection GetFlickrData(int count)
        {
            PhotoCollection data = null;
            var userId = FransDataManager.GetFlickUserId();
            data = FlickrManager.GetFlickrImages(userId, count);
            //if (data == null || data.Count == 0)
            //{
            //    //get national flickr data
            //    userId = ConfigurationManager.AppSettings["defaultFlickrUser"];
            //    data = FlickrManager.GetFlickrImages(userId, count);
            //}
            return data;
        }


        /// <summary>
        /// This method is used to get flickr Sets data
        /// </summary>
        /// <returns></returns>
        public static List<Photoset> GetFlickrPhotoSetData()
        {
            List<Photoset> data = null;            
            if (FransDataManager.IsFranchiseSelected())
            {
                List<string> selectedSetIds = new List<string>();
                var thirdPartyData = FransDataManager.GetFransThirdPartyData();
                if (thirdPartyData != null)
                {
                    selectedSetIds = thirdPartyData.SelectedPhotoSetIds;
                    var userId = thirdPartyData.FlickrUserId;
                    if (selectedSetIds != null && selectedSetIds.Any() && userId != string.Empty)
                    {
                        data = new List<Photoset>();
                        Photoset item = null;
                        foreach (var setId in selectedSetIds)
                        {
                            item = FlickrManager.GetFlickrPhotoSetData(userId, setId);
                            data.Add(item);
                        }
                    }
                }
            }
            return data;
        }

        /// <summary>
        /// This method is used to get flickr set's photos
        /// </summary>
        /// <returns></returns>
        public static PhotosetPhotoCollection GetFlickrPhotoSetPhotoData(string photoSetId)
        {
            PhotosetPhotoCollection data = null;
            data = FlickrManager.GetFlickrPhotoSetsImages(photoSetId);
            //if (data == null || data.Count == 0)
            //{
            //    //get national flickr data
            //    userId = ConfigurationManager.AppSettings["defaultFlickrUser"];
            //    data = FlickrManager.GetFlickrPhotoSets(userId, count);
            //}
            return data;
        }


        /// <summary>
        /// This method is udes to get the Product & Services taxonomy tree for Nation or Local
        /// </summary>
        /// <returns></returns>
        //public static List<TaxonomyData> GetProductServicesTaxList()
        //{
        //    List<TaxonomyData> taxList = null;
        //    if (FransDataManager.IsFranchiseSelected())
        //    {
        //        List<long> taxIds = new List<long>();
        //        var workAreaData = FransDataManager.GetFransWorkareaData();
        //        if (workAreaData != null)
        //        {
        //            var productServicesList = workAreaData.ProductAndServices;
        //            if (productServicesList != null && productServicesList.Any())
        //            {
        //                foreach (var t in productServicesList)
        //                {
        //                    taxIds.Add(t.MainCategoryId);
        //                }

        //                if (taxIds != null && taxIds.Any())
        //                {
        //                    var taxCriteria = new TaxonomyCriteria();
        //                    taxCriteria.AddFilter(TaxonomyProperty.Id, CriteriaFilterOperator.In, taxIds);
        //                    taxList = TaxonomyHelper.GetTaxonomyData(taxCriteria);
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        long productsAndServicesCatId = ConfigHelper.GetValueLong("NationalProductAndServicesTaxId");
        //        var taxTree = TaxonomyHelper.GetTaxonomyTree(productsAndServicesCatId, 1, false);
        //        if (taxTree != null && taxTree.HasChildren)
        //        {
        //            taxList = taxTree.Taxonomy.ToList();
        //        }
        //    }
        //    return taxList;
        //}

        /// <summary>
        /// This method is used to get the List of Products and Services Content Data Local
        /// </summary>
        /// <returns></returns>
        public static Dictionary<long, List<long>> GetProductAndServicesLocalInfo()
        {
            if (FransDataManager.IsFranchiseSelected())
            {
                Dictionary<long, List<long>> PSDictionary = new Dictionary<long, List<long>>();
                var workAreaData = FransDataManager.GetFransWorkareaData();
                if (workAreaData != null && workAreaData.ProductAndServices != null)
                {
                    var psData = workAreaData.ProductAndServices;
                    foreach (var ps in psData)
                    {
                        PSDictionary.Add(ps.MainCategoryId, ps.SubCategoryContentId);
                    }

                    if (PSDictionary != null && PSDictionary.Any())
                    {
                        return PSDictionary;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// This method is used to get the List of Products and Services Content Data for Nation or Local
        /// </summary>
        /// <returns></returns>
        public static List<ContentData> GetProductAndServices(bool refreshCache = false)
        {
            List<ContentData> dataList = null;
            long nationalProductsAndServicesTaxId = ConfigHelper.GetValueLong("NationalProductAndServicesTaxId");
            //long productsAndServicesFolderId = ConfigHelper.GetValueLong("ProductsAndServicesCategoriesFolderID");
            long productsAndServicesSFId = ConfigHelper.GetValueLong("ProductsAndServicesSmartFormID");
            string cacheKey = "PIP.GetProductAndServices";
            if (FransDataManager.IsFranchiseSelected())
            {
                string fransId = FransDataManager.GetFranchiseId();
                cacheKey = cacheKey + fransId;
                dataList = HttpContext.Current.Cache[cacheKey] as List<ContentData>;
                if (dataList == null || dataList.Count == 0 || refreshCache)
                {
                    List<long> cIds = new List<long>();
                    Dictionary<long, List<long>> PSDictionary = new Dictionary<long, List<long>>();
                    var workAreaData = FransDataManager.GetFransWorkareaData();
                    if (workAreaData != null && workAreaData.ProductAndServices != null)
                    {
                        var psData = workAreaData.ProductAndServices;
                        foreach (var ps in psData)
                        {
                            cIds.Add(ps.MainCategoryId);
                            if (PSDictionary.ContainsKey(ps.MainCategoryId) == false)
                                PSDictionary.Add(ps.MainCategoryId, ps.SubCategoryContentId);
                        }

                        if (cIds != null && cIds.Any())
                        {
                            var contentCriteria = new ContentCriteria();
                            contentCriteria.AddFilter(ContentProperty.Id, CriteriaFilterOperator.In, cIds);
                            contentCriteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, productsAndServicesSFId);
                            contentCriteria.PagingInfo = new Ektron.Cms.PagingInfo(FransDataManager.GetCustomApiPageSize(), 1);
                            var dList = ContentHelper.GetListByCriteria(contentCriteria);

                            ///sort again bez Ektorn api is automatically getting sorted by ContentId.
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
                    //get all items from taxonomy
                    var taxItems = TaxonomyHelper.GetTaxonomyTree(nationalProductsAndServicesTaxId, 1, true);
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
                    cfc.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, productsAndServicesSFId);
                    cfc.PagingInfo = new Ektron.Cms.PagingInfo(FransDataManager.GetCustomApiPageSize(), 1);
                    var dList = ContentHelper.GetListByCriteria(cfc);

                    ///sort again bez Ektorn api is automatically getting sorted by ContentId.
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

                    if (dataList != null && dataList.Count > 0)
                        HttpContext.Current.Cache.Insert(cacheKey, dataList, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            return dataList;
        }

        public static List<ContentData> GetProductAndServices_SubCategoryContent(List<long> subCatIds, bool refreshCache = false)
        {
            List<ContentData> subCateogyList = null;
            if (subCatIds != null && subCatIds.Any())
            {

                string cacheKey = "PIP.SitedataMngr.GetProductAndServices_SubCategoryContent";
                foreach (var id in subCatIds)
                    cacheKey += id.ToString();

                if (FransDataManager.IsFranchiseSelected())
                {
                    string fransId = FransDataManager.GetFranchiseId();
                    cacheKey = cacheKey + fransId;
                }

                subCateogyList = HttpContext.Current.Cache[cacheKey] as List<ContentData>;
                if (subCateogyList == null || subCateogyList.Count == 0 || refreshCache)
                {
                    subCateogyList = new List<ContentData>();
                    long PSSmartFormXMLConfig = ConfigHelper.GetValueLong("ProductsAndServicesSubCatergorySmartFormID");
                    //get all contents in the specified folder
                    ContentCriteria criteria = new ContentCriteria();
                    criteria.AddFilter(ContentProperty.Id, CriteriaFilterOperator.In, subCatIds);
                    criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, PSSmartFormXMLConfig);
                    var contents = ContentHelper.GetListByCriteria(criteria);

                    if (contents != null && contents.Count > 0)
                    {
                        //sort again to bring back the content in admin selected order                       
                        foreach (var id in subCatIds)
                        {
                            var cData = contents.Where(x => x.Id == id).FirstOrDefault();
                            if (cData != null)
                            {
                                subCateogyList.Add(cData);
                            }
                        }

                        if (subCateogyList != null && subCateogyList.Count > 0)
                            HttpContext.Current.Cache.Insert(cacheKey, subCateogyList, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
                    }
                }
            }
            return subCateogyList;
        }

        /// <summary>
        /// This method is used to get briefs & whitepapers for home page supporting both local & nation site
        /// </summary>
        /// <param name="count">no of content, default=2</param>
        /// <returns></returns>
        public static List<ContentData> GetBriefsAnsWhitePapersForHomePage(int count = 2)
        {
            List<ContentData> dataList = null;
            long briefsAnsWhitePapersTaxId = ConfigHelper.GetValueLong("NationalBriefsWhitePaperTaxId");
            long briefsAnsWhitePapersSFId = ConfigHelper.GetValueLong("BriefsAndWhitepapersSmartFormID");
            if (FransDataManager.IsFranchiseSelected())
            {
                string fransId = FransDataManager.GetFranchiseId();
                string cacheKey = "PIP.GetBriefsAnsWhitePapersForHomePage" + fransId + count;
                dataList = HttpContext.Current.Cache[cacheKey] as List<ContentData>;
                if (dataList == null || dataList.Count == 0)
                {
                    List<long> cIds = new List<long>();
                    var workAreaData = FransDataManager.GetFransWorkareaData();
                    if (workAreaData != null && workAreaData.BriefsWhitePapersContentIds != null)
                    {
                        cIds = workAreaData.BriefsWhitePapersContentIds;
                        if (cIds != null && cIds.Any())
                        {
                            var contentCriteria = new ContentCriteria(ContentProperty.DateModified, EkEnumeration.OrderByDirection.Descending);
                            contentCriteria.AddFilter(ContentProperty.Id, CriteriaFilterOperator.In, cIds);
                            contentCriteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, briefsAnsWhitePapersSFId);
                            contentCriteria.OrderByField = ContentProperty.DateModified;
                            dataList = ContentHelper.GetListByCriteria(contentCriteria);
                            if (dataList != null && dataList.Any())
                                dataList = dataList.Take(count).ToList();
                        }
                    }

                    if (dataList != null && dataList.Count > 0)
                        HttpContext.Current.Cache.Insert(cacheKey, dataList, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            else
            {
                string cacheKey = "PIP.GetBriefsAnsWhitePapersForHomePage" + count;
                dataList = HttpContext.Current.Cache[cacheKey] as List<ContentData>;
                if (dataList == null || dataList.Count == 0)
                {
                    var ctc = new ContentTaxonomyCriteria(ContentProperty.DateModified, EkEnumeration.OrderByDirection.Descending);
                    ctc.AddFilter(briefsAnsWhitePapersTaxId);
                    ctc.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, briefsAnsWhitePapersSFId);
                    dataList = ContentHelper.GetListByCriteria(ctc);
                    if (dataList != null && dataList.Any())
                        dataList = dataList.Take(count).ToList();

                    if (dataList != null && dataList.Count > 0)
                        HttpContext.Current.Cache.Insert(cacheKey, dataList, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            return dataList;
        }

        /// <summary>
        /// This method is udes to get the Partner taxonomy tree for Nation or Local
        /// </summary>
        /// <returns></returns>
        public static List<TaxonomyData> GetPartnersTaxList(bool refreshCache = false)
        {
            List<TaxonomyData> taxList = null;
            string cacheKey = "PIP.GetPartnersTaxList";
            if (FransDataManager.IsFranchiseSelected())
            {
                string fransId = FransDataManager.GetFranchiseId();
                cacheKey = cacheKey + fransId;
                taxList = HttpContext.Current.Cache[cacheKey] as List<TaxonomyData>;
                if (taxList == null || taxList.Count == 0 || refreshCache)
                {
                    List<long> taxIds = new List<long>();
                    var workAreaData = FransDataManager.GetFransWorkareaData();
                    if (workAreaData != null)
                    {
                        var partnersList = workAreaData.PartnersContentIds;
                        if (partnersList != null && partnersList.Any())
                        {
                            foreach (var t in partnersList)
                            {
                                taxIds.Add(t);
                            }

                            if (taxIds != null && taxIds.Any())
                            {
                                var taxCriteria = new TaxonomyCriteria();
                                taxCriteria.AddFilter(TaxonomyProperty.Id, CriteriaFilterOperator.In, taxIds);
                                taxList = TaxonomyHelper.GetTaxonomyData(taxCriteria);
                            }
                        }
                    }

                    if (taxList != null && taxList.Count > 0)
                        HttpContext.Current.Cache.Insert(cacheKey, taxList, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            else
            {
                taxList = HttpContext.Current.Cache[cacheKey] as List<TaxonomyData>;
                if (taxList == null || taxList.Count == 0 || refreshCache)
                {
                    long partnersCatId = ConfigHelper.GetValueLong("NationalPartnersTaxId");
                    var taxTree = TaxonomyHelper.GetTaxonomyTree(partnersCatId, 1, false);
                    if (taxTree != null && taxTree.HasChildren)
                    {
                        taxList = taxTree.Taxonomy.ToList();
                    }

                    if (taxList != null && taxList.Count > 0)
                        HttpContext.Current.Cache.Insert(cacheKey, taxList, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            return taxList;
        }

        /// <summary>
        /// This method is used to get the List of Partner Content Data for Nation or Local
        /// </summary>
        /// <returns></returns>
        public static List<ContentData> GetPartners()
        {
            return GetPartnersContent();
        }

        /// <summary>
        /// This method is used to get the News taxonomy tree for Nation or Local
        /// </summary>
        /// <returns></returns>
        public static List<TaxonomyData> GetNewsTaxList(bool refreshCache = false)
        {
            List<TaxonomyData> taxList = null;
            string cacheKey = "PIP.GetNewsTaxList";
            if (FransDataManager.IsFranchiseSelected())
            {
                string fransId = FransDataManager.GetFranchiseId();
                cacheKey = cacheKey + fransId;
                taxList = HttpContext.Current.Cache[cacheKey] as List<TaxonomyData>;
                if (taxList == null || taxList.Count == 0 || refreshCache)
                {
                    List<long> taxIds = new List<long>();
                    var workAreaData = FransDataManager.GetFransWorkareaData();
                    if (workAreaData != null)
                    {
                        var newsList = workAreaData.NewsContentIds;
                        if (newsList != null && newsList.Any())
                        {
                            foreach (var t in newsList)
                            {
                                taxIds.Add(t);
                            }

                            if (taxIds != null && taxIds.Any())
                            {
                                var taxCriteria = new TaxonomyCriteria();
                                taxCriteria.AddFilter(TaxonomyProperty.Id, CriteriaFilterOperator.In, taxIds);
                                taxList = TaxonomyHelper.GetTaxonomyData(taxCriteria);
                            }
                        }
                    }

                    if (taxList != null && taxList.Count > 0)
                        HttpContext.Current.Cache.Insert(cacheKey, taxList, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            else
            {
                taxList = HttpContext.Current.Cache[cacheKey] as List<TaxonomyData>;
                if (taxList == null || taxList.Count == 0 || refreshCache)
                {
                    long partnersCatId = ConfigHelper.GetValueLong("AllNewsNationalTaxId");
                    var taxTree = TaxonomyHelper.GetTaxonomyTree(partnersCatId, 1, false);
                    if (taxTree != null && taxTree.HasChildren)
                    {
                        taxList = taxTree.Taxonomy.ToList();
                    }

                    if (taxList != null && taxList.Count > 0)
                        HttpContext.Current.Cache.Insert(cacheKey, taxList, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            return taxList;
        }

        /// <summary>
        /// This method is used to get the List of News Content Data for Nation or Local
        /// </summary>
        /// <returns></returns>
        public static List<ContentData> GetNews(bool refreshCache = false)
        {
            List<ContentData> dataList = null;
            long newsTaxId = ConfigHelper.GetValueLong("AllNewsNationalTaxId");
            long newsSFId = ConfigHelper.GetValueLong("NewsSmartFormID");
            string cacheKey = "PIP.GetNews";
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

        /// <summary>
        /// This method is used to get the In The Media taxonomy tree for Nation or Local
        /// </summary>
        /// <returns></returns>
        public static List<TaxonomyData> GetInTheMediaTaxList(bool refreshCache = false)
        {
            List<TaxonomyData> taxList = null;
            string cacheKey = "PIP.GetInTheMediaTaxList";
            if (FransDataManager.IsFranchiseSelected())
            {
                string fransId = FransDataManager.GetFranchiseId();
                cacheKey = cacheKey + fransId;
                taxList = HttpContext.Current.Cache[cacheKey] as List<TaxonomyData>;
                if (taxList == null || taxList.Count == 0 || refreshCache)
                {
                    List<long> taxIds = new List<long>();
                    var workAreaData = FransDataManager.GetFransWorkareaData();
                    if (workAreaData != null)
                    {
                        var inTheMediaList = workAreaData.InTheMediaContentIds;
                        if (inTheMediaList != null && inTheMediaList.Any())
                        {
                            foreach (var t in inTheMediaList)
                            {
                                taxIds.Add(t);
                            }

                            if (taxIds != null && taxIds.Any())
                            {
                                var taxCriteria = new TaxonomyCriteria();
                                taxCriteria.AddFilter(TaxonomyProperty.Id, CriteriaFilterOperator.In, taxIds);
                                taxList = TaxonomyHelper.GetTaxonomyData(taxCriteria);
                            }
                        }
                    }

                    if (taxList != null && taxList.Count > 0)
                        HttpContext.Current.Cache.Insert(cacheKey, taxList, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            else
            {
                taxList = HttpContext.Current.Cache[cacheKey] as List<TaxonomyData>;
                if (taxList == null || taxList.Count == 0 || refreshCache)
                {
                    long inTheMediaCatId = ConfigHelper.GetValueLong("AllInTheMediaNationalTaxId");
                    var taxTree = TaxonomyHelper.GetTaxonomyTree(inTheMediaCatId, 1, false);
                    if (taxTree != null && taxTree.HasChildren)
                    {
                        taxList = taxTree.Taxonomy.ToList();
                    }

                    if (taxList != null && taxList.Count > 0)
                        HttpContext.Current.Cache.Insert(cacheKey, taxList, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            return taxList;
        }

        /// <summary>
        /// This method is used to get the List of In The Media Content Data for Nation or Local
        /// </summary>
        /// <returns></returns>
        public static List<ContentData> GetInTheMedia(bool refreshCache = false)
        {
            List<ContentData> dataList = null;
            long inTheMediaTaxId = ConfigHelper.GetValueLong("AllInTheMediaNationalTaxId");
            long inTheMediaSFId = ConfigHelper.GetValueLong("InTheMediaSmartFormID");
            string cacheKey = "PIP.GetInTheMedia";
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

        /// <summary>
        /// This method is used to get the Management Team taxonomy tree for Nation or Local
        /// </summary>
        /// <returns></returns>
        //public static List<TaxonomyData> GetManagementTeamTaxList()
        //{
        //    List<TaxonomyData> taxList = null;
        //    if (FransDataManager.IsFranchiseSelected())
        //    {
        //        List<long> taxIds = new List<long>();
        //        var workAreaData = FransDataManager.GetFransWorkareaData();
        //        if (workAreaData != null)
        //        {
        //            var employeeList = workAreaData.EmployeeContentIds;
        //            if (employeeList != null && employeeList.Any())
        //            {
        //                foreach (var t in employeeList)
        //                {
        //                    taxIds.Add(t);
        //                }

        //                if (taxIds != null && taxIds.Any())
        //                {
        //                    var taxCriteria = new TaxonomyCriteria();
        //                    taxCriteria.AddFilter(TaxonomyProperty.Id, CriteriaFilterOperator.In, taxIds);
        //                    taxList = TaxonomyHelper.GetTaxonomyData(taxCriteria);
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        long managementTeamCatId = ConfigHelper.GetValueLong("ManagementTeamNationalTaxId");
        //        var taxTree = TaxonomyHelper.GetTaxonomyTree(managementTeamCatId, 1, false);
        //        if (taxTree != null && taxTree.HasChildren)
        //        {
        //            taxList = taxTree.Taxonomy.ToList();
        //        }
        //    }
        //    return taxList;
        //}

        /// <summary>
        /// This method is used to get the List of Management Team Content Data for Nation or Local
        /// </summary>
        /// <returns></returns>
        public static List<ContentData> GetManagementTeam(bool refreshCache = false)
        {
            List<ContentData> dataList = null;
            long managementTeamTaxId = ConfigHelper.GetValueLong("ManagementTeamNationalTaxId");
            long managementTeamSFId = ConfigHelper.GetValueLong("ManagementTeamSmartFormID");
            if (FransDataManager.IsFranchiseSelected() == false)
            {
                string cacheKey = "PIP.GetManagementTeam";
                dataList = HttpContext.Current.Cache[cacheKey] as List<ContentData>;
                if (dataList == null || dataList.Count == 0 || refreshCache)
                {

                    //get all items from taxonomy
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
                    cfc.PagingInfo = new Ektron.Cms.PagingInfo(FransDataManager.GetCustomApiPageSize(), 1);
                    var dList = ContentHelper.GetListByCriteria(cfc);

                    ///sort again bez Ektorn api is automatically getting sorted by ContentId.
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

                    if (dataList != null && dataList.Count > 0)
                        HttpContext.Current.Cache.Insert(cacheKey, dataList, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            return dataList;
        }


        /// <summary>
        /// This method is used to get the Case Studies taxonomy tree for Nation or Local
        /// </summary>
        /// <returns></returns>
        public static List<TaxonomyData> GetCaseStudiesTaxList(bool refreshCache = false)
        {
            List<TaxonomyData> taxList = null;
            string cacheKey = "PIP.GetCaseStudiesTaxList";
            if (FransDataManager.IsFranchiseSelected())
            {
                string fransId = FransDataManager.GetFranchiseId();
                cacheKey = cacheKey + fransId;
                taxList = HttpContext.Current.Cache[cacheKey] as List<TaxonomyData>;
                if (taxList == null || taxList.Count == 0 || refreshCache)
                {
                    List<long> taxIds = new List<long>();
                    var workAreaData = FransDataManager.GetFransWorkareaData();
                    if (workAreaData != null)
                    {
                        var caseStudiesList = workAreaData.CaseStudiesContentIds;
                        if (caseStudiesList != null && caseStudiesList.Any())
                        {
                            foreach (var t in caseStudiesList)
                            {
                                taxIds.Add(t);
                            }

                            if (taxIds != null && taxIds.Any())
                            {
                                var taxCriteria = new TaxonomyCriteria();
                                taxCriteria.AddFilter(TaxonomyProperty.Id, CriteriaFilterOperator.In, taxIds);
                                taxList = TaxonomyHelper.GetTaxonomyData(taxCriteria);
                            }
                        }
                    }

                    if (taxList != null && taxList.Count > 0)
                        HttpContext.Current.Cache.Insert(cacheKey, taxList, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            else
            {
                taxList = HttpContext.Current.Cache[cacheKey] as List<TaxonomyData>;
                if (taxList == null || taxList.Count == 0 || refreshCache)
                {
                    long managementTeamCatId = ConfigHelper.GetValueLong("CaseStudiesNationalTaxId");
                    var taxTree = TaxonomyHelper.GetTaxonomyTree(managementTeamCatId, 1, false);
                    if (taxTree != null && taxTree.HasChildren)
                    {
                        taxList = taxTree.Taxonomy.ToList();
                    }

                    if (taxList != null && taxList.Count > 0)
                        HttpContext.Current.Cache.Insert(cacheKey, taxList, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            return taxList;
        }
        
        /// <summary>
        /// This method is used to get the List of Case Studies Content Data for Nation or Local
        /// </summary>
        /// <returns></returns>
        public static List<ContentData> GetCaseStudies(bool refreshCache = false)
        {
            List<ContentData> dataList = null;
            long caseStudiesTaxId = ConfigHelper.GetValueLong("CaseStudiesNationalTaxId");
            long caseStudiesSFId = ConfigHelper.GetValueLong("CaseStudiesSmartFormID");
            string cacheKey = "PIP.GetCaseStudies";
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

        /// <summary>
        /// This method is used to get the Case Studies taxonomy tree for Nation or Local
        /// </summary>
        /// <returns></returns>
        public static List<TaxonomyData> GetBriefAndWhitePapersTaxList(bool refreshCache = false)
        {
            List<TaxonomyData> taxList = null;
            string cacheKey = "PIP.GetBriefAndWhitePapersTaxList";
            if (FransDataManager.IsFranchiseSelected())
            {
                string fransId = FransDataManager.GetFranchiseId();
                cacheKey = cacheKey + fransId;
                taxList = HttpContext.Current.Cache[cacheKey] as List<TaxonomyData>;
                if (taxList == null || taxList.Count == 0 || refreshCache)
                {
                    List<long> taxIds = new List<long>();
                    var workAreaData = FransDataManager.GetFransWorkareaData();
                    if (workAreaData != null)
                    {
                        var briefWhitepapersList = workAreaData.BriefsWhitePapersContentIds;
                        if (briefWhitepapersList != null && briefWhitepapersList.Any())
                        {
                            foreach (var t in briefWhitepapersList)
                            {
                                taxIds.Add(t);
                            }

                            if (taxIds != null && taxIds.Any())
                            {
                                var taxCriteria = new TaxonomyCriteria();
                                taxCriteria.AddFilter(TaxonomyProperty.Id, CriteriaFilterOperator.In, taxIds);
                                taxList = TaxonomyHelper.GetTaxonomyData(taxCriteria);
                            }
                        }
                    }

                    if (taxList != null && taxList.Count > 0)
                        HttpContext.Current.Cache.Insert(cacheKey, taxList, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            else
            {
                taxList = HttpContext.Current.Cache[cacheKey] as List<TaxonomyData>;
                if (taxList == null || taxList.Count == 0 || refreshCache)
                {
                    long briefWhitepapersCatId = ConfigHelper.GetValueLong("BriefAndWhitepapersNationalTaxId");
                    var taxTree = TaxonomyHelper.GetTaxonomyTree(briefWhitepapersCatId, 1, false);
                    if (taxTree != null && taxTree.HasChildren)
                    {
                        taxList = taxTree.Taxonomy.ToList();
                    }

                    if (taxList != null && taxList.Count > 0)
                        HttpContext.Current.Cache.Insert(cacheKey, taxList, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            return taxList;
        }

        /// <summary>
        /// This method is used to get the List of Case Studies Content Data for Nation or Local
        /// </summary>
        /// <returns></returns>
        public static List<ContentData> GetBriefsAndWhitePapers(bool refreshCache = false)
        {
            List<ContentData> dataList = null;
            long briefWhitepapersTaxId = ConfigHelper.GetValueLong("BriefAndWhitepapersNationalTaxId");
            long briefWhitepapersSFId = ConfigHelper.GetValueLong("BriefsAndWhitepapersSmartFormID");
            string cacheKey = "PIP.GetBriefsAndWhitePapers";
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
               

        public static TaxonomyData GetCareeersTaxonomyTree(bool refreshCache = false)
        {
            TaxonomyData tData = null;
            long profileTaxId = ConfigHelper.GetValueLong("ManageCarrersTaxId");
            string cacheKey = "PIP.GetCareeersTaxonomyTree" + profileTaxId;
            tData = HttpContext.Current.Cache[cacheKey] as TaxonomyData;
            if (tData == null || refreshCache)
            {
                tData = TaxonomyHelper.GetTaxonomyTree(profileTaxId, 1, false);
                if (tData != null)
                    HttpContext.Current.Cache.Insert(cacheKey, tData, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
            }
            return tData;
        }

        public static ContentData GetProfileContentByTaxId(long taxonomyId, bool refreshCache = false)
        {
            ContentData cData = null;
            long profileTaxId = ConfigHelper.GetValueLong("ManageCarrersTaxId");
            string cacheKey = "PIP.GetProfileContentById" + taxonomyId;
            cData = HttpContext.Current.Cache[cacheKey] as ContentData;
            if (cData == null || refreshCache)
            {
                long profileSFId = ConfigHelper.GetValueLong("ProfileTypeSmartFormId");
                var cc = new ContentTaxonomyCriteria();
                cc.AddFilter(taxonomyId);
                cc.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, profileSFId);
                var list = ContentHelper.GetListByCriteria(cc);
                if (list != null && list.Any())
                {
                    cData = list.FirstOrDefault();
                    if (cData != null)
                        HttpContext.Current.Cache.Insert(cacheKey, cData, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            return cData;
        }
    }
}