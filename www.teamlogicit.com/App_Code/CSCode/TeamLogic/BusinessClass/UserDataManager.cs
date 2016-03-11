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
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace TeamLogic.CMS
{
    /// <summary>
    /// Summary description for SirSpeedyUserManager
    /// </summary>
    public class UserDataManager
    {
        private static string CookieName = HttpContext.Current.Request.Url.Host + "FransID";
        private static string adminToolConnectionString = ConfigurationManager.ConnectionStrings["SirSpeedyAdminTool.DbConnection"].ConnectionString;

        public UserDataManager()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// This method is used to retrieve banner content data for Local/National
        /// </summary>
        /// <returns>banner content list</returns>
        public static List<ContentData> GetBannerContent()
        {
            List<ContentData> dataList = null;
            long bannerSmartFormId = ConfigHelper.GetValueLong("HomeBannerSmartFormID");
            if (FransDataManager.IsFranchiseSelected())
            {
                var bannerIds = FransDataManager.GetFransBannerIds();
                if (bannerIds != null && bannerIds.Any())
                    dataList = ContentHelper.GetContentByIds(bannerIds, bannerSmartFormId);
                else
                    dataList = GetNationalBannerContent(bannerSmartFormId);
            }
            else
            {
                dataList = GetNationalBannerContent(bannerSmartFormId);
            }
            return dataList;
        }
        
        /// <summary>
        /// This method is used to retrieve partners content data for Local/National
        /// </summary>
        /// <returns></returns>
        public static List<ContentData> GetPartnersContent()
        {
            List<ContentData> dataList = null;
            long partnersSFId = ConfigHelper.GetValueLong("PartnersSmartFormID");
            if (FransDataManager.IsFranchiseSelected())
            {
                var ids = FransDataManager.GetFransPartnersIds();
                if (ids != null && ids.Any())
                    dataList = ContentHelper.GetContentByIds(ids, partnersSFId);
                else
                    dataList = GetNationalPartnersContent(partnersSFId);
            }
            else
            {
                dataList = GetNationalPartnersContent(partnersSFId);
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
            var nationalMediaLinks = GetNationalSocialMediaLinks(); ;
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
        /// This method is used to retrieve userData of specify email address
        /// </summary>
        /// <returns></returns>
        public static CustomUserData getUserInfoData(string emailAdd)
        {
            CustomUserData centerData = null;
            if (!string.IsNullOrEmpty(emailAdd))
            {
                try
                {
                    string cacheKey = "SirSpeedy-GetUserData" + emailAdd;
                    bool dataExistInCache = ApplicationCache.IsExist(cacheKey);

                    if (!dataExistInCache)
                    {
                        using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                        {
                            string procName = "Get_User_Data";

                            using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                            {
                                sqlCommand.CommandText = procName;
                                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                                sqlCommand.Parameters.Clear();
                                //sqlCommand.Parameters.Add("@CenterId", SqlDbType.NVarChar).Value = centerId;
                                sqlCommand.Parameters.Add("@Email", SqlDbType.NVarChar).Value = emailAdd;
                                sqlConnection.Open();
                                var reader = sqlCommand.ExecuteReader();

                                while (reader.Read())
                                {
                                    centerData = new CustomUserData()
                                    {
                                        CenterId = reader["Center_Id"] is DBNull ? null : reader["Center_Id"].ToString(),
                                        UserFirstName = reader["First_Name"] is DBNull ? null : reader["First_Name"].ToString(),
                                        UserLastName = reader["Last_Name"] is DBNull ? null : reader["Last_Name"].ToString(),
                                        UserCompanyName = reader["Company_Name"] is DBNull ? null : reader["Company_Name"].ToString(),
                                        UserEmail = reader["Email"] is DBNull ? null : reader["Email"].ToString(),
                                        UserJobTitle = reader["Job_Title"] is DBNull ? null : reader["Job_Title"].ToString(),
                                        UserPhoneNumber = reader["Phone_Number"] is DBNull ? null : reader["Phone_Number"].ToString(),
                                        UserWebsite = reader["Website"] is DBNull ? null : reader["Website"].ToString()
                                    };
                                }
                            }
                        }
                        if (centerData != null)
                            ApplicationCache.Insert(cacheKey, centerData, ConfigHelper.GetValueLong("smallCacheInterval"));
                    }
                    else
                    {
                        var cacheData = ApplicationCache.GetValue(cacheKey);
                        if (cacheData != null)
                            centerData = (CustomUserData)cacheData;
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }
            return centerData;
        }


        #region *** Private Methods ***

        /// <summary>
        /// This method is used to get National banner content
        /// </summary>
        /// <returns></returns>
        private static List<ContentData> GetNationalBannerContent(long bannerSmartFormId)
        {
            List<ContentData> dataList = null;
            long nationaBannerTaxId = ConfigHelper.GetValueLong("NationalBannerTaxonomyId");            
            if (nationaBannerTaxId > 0)
            {
                ContentTaxonomyCriteria taxCriteria = new ContentTaxonomyCriteria();
                taxCriteria.AddFilter(ContentTaxonomyProperty.Id, CriteriaFilterOperator.EqualTo, nationaBannerTaxId);
                taxCriteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, bannerSmartFormId);                
                dataList = ContentHelper.GetListByCriteria(taxCriteria);
            }
            return dataList;
        }


        /// <summary>
        /// This method is used to get National partenrs content
        /// </summary>
        /// <returns></returns>
        private static List<ContentData> GetNationalPartnersContent(long smartFormId)
        {
            List<ContentData> dataList = null;
            long taxId = ConfigHelper.GetValueLong("NationalPartnersTaxId");
            if (taxId > 0)
            {
                ContentTaxonomyCriteria taxCriteria = new ContentTaxonomyCriteria();
                taxCriteria.AddFilter(ContentTaxonomyProperty.Id, CriteriaFilterOperator.EqualTo, taxId);
                taxCriteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, smartFormId);
                dataList = ContentHelper.GetListByCriteria(taxCriteria);
            }
            return dataList;
        }

        /// <summary>
        /// This method is used to read the social media links from workarea
        /// </summary>
        /// <returns></returns>
        private static SocialMedia GetNationalSocialMediaLinks()
        {
            SocialMedia data = null;
            long contentId = ConfigHelper.GetValueLong("SocialMediaContentId");
            if (contentId > 0)
            {
                var cData = ContentHelper.GetContentById(contentId);
                if (cData != null && !string.IsNullOrEmpty(cData.Html))
                {
                    try
                    {
                        data = new SocialMedia();
                        XDocument xDoc = XDocument.Parse(cData.Html);

                        if (xDoc.XPathSelectElement("/root/FaceBook/url/a") != null && xDoc.XPathSelectElement("/root/FaceBook/image/img") != null)
                        {
                            data.FaceBookUrl = xDoc.XPathSelectElement("/root/FaceBook/url/a").Attribute("href").Value;
                            data.FaceBookImgPath = xDoc.XPathSelectElement("/root/FaceBook/image/img").Attribute("src").Value;
                        }
                        if (xDoc.XPathSelectElement("/root/Twitter/url/a") != null && xDoc.XPathSelectElement("/root/Twitter/image/img") != null)
                        {
                            data.TwitterUrl = xDoc.XPathSelectElement("/root/Twitter/url/a").Attribute("href").Value;
                            data.TwitterImgPath = xDoc.XPathSelectElement("/root/Twitter/image/img").Attribute("src").Value;
                        }
                        if (xDoc.XPathSelectElement("/root/GooglePlus/url/a") != null && xDoc.XPathSelectElement("/root/GooglePlus/image/img") != null)
                        {
                            data.GooglePlusUrl = xDoc.XPathSelectElement("/root/GooglePlus/url/a").Attribute("href").Value;
                            data.GooglePlusImgPath = xDoc.XPathSelectElement("/root/GooglePlus/image/img").Attribute("src").Value;
                        }
                        if (xDoc.XPathSelectElement("/root/LinkedIn/url/a") != null && xDoc.XPathSelectElement("/root/LinkedIn/image/img") != null)
                        {
                            data.LinkedInUrl = xDoc.XPathSelectElement("/root/LinkedIn/url/a").Attribute("href").Value;
                            data.LinkedInImgPath = xDoc.XPathSelectElement("/root/LinkedIn/image/img").Attribute("src").Value;
                        }
                        if (xDoc.XPathSelectElement("/root/StumbleUpon/url/a") != null && xDoc.XPathSelectElement("/root/StumbleUpon/image/img") != null)
                        {
                            data.StumbleUponUrl = xDoc.XPathSelectElement("/root/StumbleUpon/url/a").Attribute("href").Value;
                            data.StumbleUponImgPath = xDoc.XPathSelectElement("/root/StumbleUpon/image/img").Attribute("src").Value;
                        }
                        if (xDoc.XPathSelectElement("/root/Flickr/url/a") != null && xDoc.XPathSelectElement("/root/Flickr/image/img") != null)
                        {
                            data.FlickrUrl = xDoc.XPathSelectElement("/root/Flickr/url/a").Attribute("href").Value;
                            data.FlickrImgPath = xDoc.XPathSelectElement("/root/Flickr/image/img").Attribute("src").Value;
                        }
                        if (xDoc.XPathSelectElement("/root/YouTube/url/a") != null && xDoc.XPathSelectElement("/root/YouTube/image/img") != null)
                        {
                            data.YouTubeUrl = xDoc.XPathSelectElement("/root/YouTube/url/a").Attribute("href").Value;
                            data.YouTubeImgPath = xDoc.XPathSelectElement("/root/YouTube/image/img").Attribute("src").Value;
                        }
                        if (xDoc.XPathSelectElement("/root/ITInflections/url/a") != null && xDoc.XPathSelectElement("/root/ITInflections/image/img") != null)
                        {
                            data.ITInflectionsUrl = xDoc.XPathSelectElement("/root/ITInflections/url/a").Attribute("href").Value;
                            data.ITInflectionsImgPath = xDoc.XPathSelectElement("/root/ITInflections/image/img").Attribute("src").Value;
                        }
                    }
                    catch(Exception ex)
                    {
                        Log.WriteError(ex);
                    }
                }
            }
            return data;
        }


        #endregion
    }
}