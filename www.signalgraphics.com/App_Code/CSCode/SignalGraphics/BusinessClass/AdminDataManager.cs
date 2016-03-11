using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Xml.XPath;

using Ektron.Cms;
using Ektron.Cms.Content;
using Ektron.Cms.Common;
using Ektron.Cms.Instrumentation;

namespace SignalGraphics.CMS
{
    /// <summary>
    /// Summary description for FransDataManager
    /// </summary>
    public class AdminDataManager
    {
        public AdminDataManager()
        {
            //
            // TODO: Add constructor logic here
            //
        } 
   
        public static AboutNational GetNationalAdminData()
        {
            return SiteDataManager.GetNationalCompanyInfo();       
        }

        /// <summary>
        /// This method is used to get National banner content
        /// </summary>
        /// <returns></returns>
        public static List<ContentData> GetNationalBannerContent(long bannerSmartFormId)
        {
            List<ContentData> dataList = null;
            long nationaBannerTaxId = ConfigHelper.GetValueLong("NationalBannerTaxonomyId");
            if (nationaBannerTaxId > 0)
            {
                //get all items from taxonomy
                var taxItems = TaxonomyHelper.GetTaxonomyTree(nationaBannerTaxId, 1, true);
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
                cfc.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, bannerSmartFormId);
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
            }            
            return dataList;
        }


        /// <summary>
        /// This method is used to get National partenrs content
        /// </summary>
        /// <returns></returns>
        public static List<ContentData> GetNationalPartnersContent(long smartFormId)
        {
            List<ContentData> dataList = null;
            long taxId = ConfigHelper.GetValueLong("NationalPartnersTaxId");
            if (taxId > 0)
            {
                //get all items from taxonomy
                var taxItems = TaxonomyHelper.GetTaxonomyTree(taxId, 1, true);
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
                cfc.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, smartFormId);
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
            }
            return dataList;
        }

        /// <summary>
        /// This method is used to read the social media links from workarea
        /// </summary>
        /// <returns></returns>
        public static SocialMedia GetNationalSocialMediaLinks(bool refreshCache = false)
        {
            SocialMedia data = null;
            long contentId = ConfigHelper.GetValueLong("SocialMediaContentId");
            if (contentId > 0)
            {
                try
                {
                    string cacheKey = "SignalGraphics_AdminDataManager_GetNationalSocialMediaLinks";
                    bool dataExistInCache = ApplicationCache.IsExist(cacheKey);

                    if (!dataExistInCache || refreshCache)
                    {
                        var cData = ContentHelper.GetContentById(contentId);
                        if (cData != null && !string.IsNullOrEmpty(cData.Html))
                        {
                            data = new SocialMedia();
                            XDocument xDoc = XDocument.Parse(cData.Html);

                            var mediaNodes = xDoc.XPathSelectElements("/root/SocialMedia/Media");
                            foreach (var node in mediaNodes)
                            {
                                try
                                {
                                    if (node.XPathSelectElement("mediaType").Value == "FaceBook" && node.XPathSelectElement("mediaImage/img") != null)
                                    {
                                        if (node.XPathSelectElement("mediaLink/a") != null)
                                            data.FaceBookUrl = node.XPathSelectElement("mediaLink/a").Attribute("href").Value;
                                        data.FaceBookImgPath = node.XPathSelectElement("mediaImage/img").Attribute("src").Value;
                                    }
                                    else if (node.XPathSelectElement("mediaType").Value == "Twitter" && node.XPathSelectElement("mediaImage/img") != null)
                                    {
                                        if (node.XPathSelectElement("mediaLink/a") != null)
                                            data.TwitterUrl = node.XPathSelectElement("mediaLink/a").Attribute("href").Value;
                                        data.TwitterImgPath = node.XPathSelectElement("mediaImage/img").Attribute("src").Value;
                                    }
                                    else if (node.XPathSelectElement("mediaType").Value == "GooglePlus" && node.XPathSelectElement("mediaImage/img") != null)
                                    {
                                        if (node.XPathSelectElement("mediaLink/a") != null)
                                            data.GooglePlusUrl = node.XPathSelectElement("mediaLink/a").Attribute("href").Value;
                                        data.GooglePlusImgPath = node.XPathSelectElement("mediaImage/img").Attribute("src").Value;
                                    }
                                    else if (node.XPathSelectElement("mediaType").Value == "LinkedIn" && node.XPathSelectElement("mediaImage/img") != null)
                                    {
                                        if (node.XPathSelectElement("mediaLink/a") != null)
                                            data.LinkedInUrl = node.XPathSelectElement("mediaLink/a").Attribute("href").Value;
                                        data.LinkedInImgPath = node.XPathSelectElement("mediaImage/img").Attribute("src").Value;
                                    }
                                    else if (node.XPathSelectElement("mediaType").Value == "StumbleUpon" && node.XPathSelectElement("mediaImage/img") != null)
                                    {
                                        if (node.XPathSelectElement("mediaLink/a") != null)
                                            data.StumbleUponUrl = node.XPathSelectElement("mediaLink/a").Attribute("href").Value;
                                        data.StumbleUponImgPath = node.XPathSelectElement("mediaImage/img").Attribute("src").Value;
                                    }
                                    else if (node.XPathSelectElement("mediaType").Value == "Flickr" && node.XPathSelectElement("mediaImage/img") != null)
                                    {
                                        if (node.XPathSelectElement("mediaLink/a") != null)
                                            data.FlickrUrl = node.XPathSelectElement("mediaLink/a").Attribute("href").Value;
                                        data.FlickrImgPath = node.XPathSelectElement("mediaImage/img").Attribute("src").Value;
                                    }
                                    else if (node.XPathSelectElement("mediaType").Value == "YouTube" && node.XPathSelectElement("mediaImage/img") != null)
                                    {
                                        if (node.XPathSelectElement("mediaLink/a") != null)
                                            data.YouTubeUrl = node.XPathSelectElement("mediaLink/a").Attribute("href").Value;
                                        data.YouTubeImgPath = node.XPathSelectElement("mediaImage/img").Attribute("src").Value;
                                    }
                                    else if (node.XPathSelectElement("mediaType").Value == "MarketingTango" && node.XPathSelectElement("mediaImage/img") != null)
                                    {
                                        if (node.XPathSelectElement("mediaLink/a") != null)
                                            data.MarketingTangoUrl = node.XPathSelectElement("mediaLink/a").Attribute("href").Value;
                                        data.MarketingTangoImgPath = node.XPathSelectElement("mediaImage/img").Attribute("src").Value;
                                    }
                                }
                                catch(Exception ex)
                                {
                                    Log.WriteError(ex.Message + ":" + ex.StackTrace);
                                }
                            }                            
                            if (data != null)
                                ApplicationCache.Insert(cacheKey, data, ConfigHelper.GetValueLong("longCacheInterval"));
                        }
                    }
                    else
                    {
                        var cacheData = ApplicationCache.GetValue(cacheKey);
                        if (cacheData != null)
                            data = (SocialMedia)cacheData;
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex.Message + ":" + ex.StackTrace);
                }
            }
            return data;
        }
        
        /// <summary>
        /// This method is used to get National partenrs content
        /// </summary>
        /// <returns></returns>
        public static List<ContentData> GetNationalHomePage_CaseStudiesContent(long smartFormId)
        {
            List<ContentData> dataList = null;
            long homepageCaseStudiesFId = ConfigHelper.GetValueLong("HomepageCaseStudiesFId");
            if (homepageCaseStudiesFId > 0)
            {
                ContentCriteria cCriteria = new ContentCriteria(ContentProperty.DateModified, EkEnumeration.OrderByDirection.Descending);
                cCriteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, smartFormId);
                cCriteria.AddFilter(ContentProperty.FolderId, CriteriaFilterOperator.EqualTo, homepageCaseStudiesFId);
                dataList = ContentHelper.GetListByCriteria(cCriteria);
            }
            return dataList;
        }

        /// <summary>
        /// This method is used to calcuate national address longitude & latidude
        /// </summary>
        /// <param name="nationalData">national address data obj</param>
        /// <returns></returns>
        public static FransData GetNationalCompleteAddress(FransData nationalData)
        {
            FransData nationalUpdatedData = null;
            if(nationalData != null)
            {
                string cacheKey = "SignalGraphicsMain-NationalAddressData";
                bool dataExistInCache = ApplicationCache.IsExist(cacheKey);

                if (!dataExistInCache)
                {
                    nationalUpdatedData = nationalData;
                    string addressLat = string.Empty;
                    string addressLong = string.Empty;
                    FransDataManager.GetCenterGeoLocation(nationalData, out addressLat, out addressLong);
                    nationalUpdatedData.Latitude = addressLat;
                    nationalUpdatedData.Longitude = addressLong;
                    if (nationalUpdatedData != null && addressLat != string.Empty && addressLong != string.Empty)
                        ApplicationCache.Insert(cacheKey, nationalUpdatedData, ConfigHelper.GetValueLong("longCacheInterval"));
                }
                else
                {
                    var cacheData = ApplicationCache.GetValue(cacheKey);
                    if (cacheData != null)
                        nationalUpdatedData = (FransData)cacheData;
                }
            }
            return nationalUpdatedData;
        }
    }
}