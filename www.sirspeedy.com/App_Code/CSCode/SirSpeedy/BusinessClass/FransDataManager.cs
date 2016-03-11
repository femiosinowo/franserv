using Ektron.Cms.Common;
using Ektron.Cms.Content;
using Ektron.Cms.Framework.Content;
using Ektron.Cms.Instrumentation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;

namespace SirSpeedy.CMS
{
    /// <summary>
    /// Summary description for FransDataManager
    /// </summary>
    public class FransDataManager
    {
        private static string GoogleMapAPIKey = ConfigurationManager.AppSettings["GoogleMapApiKey"];
        private static string CookieName = HttpContext.Current.Request.Url.Host + "FransID";
        private static string adminToolConnectionString = ConfigurationManager.ConnectionStrings["SirSpeedyAdminTool.DbConnection"].ConnectionString;
        public static string GoogleStaticImagePath = "http://maps.googleapis.com/maps/api/staticmap?size=315x130&zoom=15&markers=icon:{0}/images/location-map-marker.png|{1},{2}&style=feature:landscape|color:0xe9e9e9&style=feature:poi|element:geometry|color:0xd8d8d8&sensor=true";
        public static string GoogleStaticLargeImagePath = "http://maps.googleapis.com/maps/api/staticmap?size=336x206&zoom=15&markers=icon:{0}/images/location-map-marker.png|{1},{2}&style=feature:landscape|color:0xe9e9e9&style=feature:poi|element:geometry|color:0xd8d8d8&sensor=true";
        public static string GoogleViewDirectionApiDesktop = "https://www.google.com/maps/embed/v1/place?q={0}&key=" + GoogleMapAPIKey + "&zoom=15";
        public static string GoogleViewDirectionApiMobile = "https://www.google.com/maps?daddr=q={0}";
        private static string LocalFransCacheKey = "SirSpeedy.LocalFransSite";

        public FransDataManager()
        {
            //
            // TODO: Add constructor logic here
            //
        }
            
        /// <summary>
        /// This method is used to check is loacl site is in session and create a entry in data cache
        /// </summary>
        /// <returns>local center Id</returns>
        public static string CreateActiveFransSession()
        {
            string fransId = string.Empty;
            try
            {
                string url = HttpContext.Current.Request.RawUrl;
                var pathNames = url.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                if (pathNames != null && pathNames.Length > 0)
                {
                    var reqCenterId = pathNames[0].ToLower();
                    if ((!string.IsNullOrEmpty(reqCenterId)) && (!reqCenterId.ToLower().Contains(".aspx")))
                    {
                        var allCentersList = FransDataManager.GetAllFransLocationDataList();
                        var reqCenterData = allCentersList.Where(x => x.FransId.ToLower() == reqCenterId).FirstOrDefault();
                        if (reqCenterData != null && reqCenterData.FransId != string.Empty)
                        {
                            fransId = reqCenterData.FransId;
                            //string key = LocalFransCacheKey + reqCenterData.FransId;
                            //var dataExsitInCache = ApplicationCache.IsExist(key);
                            //var cacheData = ApplicationCache.GetValue(key) as FransData;
                            //if (dataExsitInCache == false && cacheData == null)
                            //{
                            //    ApplicationCache.Insert(key, reqCenterData, ConfigHelper.GetValueLong("smallCacheInterval"));
                            //}
                            //else
                            //{
                            //    FransData centerData = null;
                            //    var cacheData = ApplicationCache.GetValue(key);
                            //    if (cacheData != null)
                            //        centerData = (FransData)cacheData;
                            //    if (centerData != null && centerData.FransId != string.Empty)
                            //        fransId = centerData.FransId;
                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
                Log.WriteError(ex.StackTrace);
            }
            return fransId;
        }        

        public static string GetFranchiseId()
        {
            return CreateActiveFransSession();
        }

        public static bool IsFranchiseSelected()
        {
            return IsFransSessionActive();           
        } 
           
        public static List<long> GetFransBannerIds()
        {
            List<long> ids = null;
            var data = GetFransWorkareaData();
            if (data != null)
            {
                ids = data.BannerContentIds;
            }
            return ids;
        }        
    
        public static string GetContactNumber()
        {
            string number = ""; //have to get a default number

            var fransData = GetFransData();
            if (fransData != null)
                number = fransData.PhoneNumber;

            return number;
        }
        
        public static List<long> GetFransPartnersIds()
        {
            List<long> ids = null;
            var data = GetFransWorkareaData();
            if (data != null)
            {
                ids = data.PartnersContentIds;
            }
            return ids;
        }

        public static List<long> GetFransEmployeeIds()
        {
            List<long> ids = null;
            var data = GetFransWorkareaData();
            if (data != null)
            {
                ids = data.CenterEmployeesTeam;
            }
            return ids;
        }

        public static Partners GetPartners(Partners localPartners)
        {
            Partners responseData = localPartners;
            var data = GetFransPartnersIds();
            if (data.Count > 0)
            {
                Partners partner = new Partners();

                if (partner != null)
                {
                    if (!string.IsNullOrEmpty(partner.Center_Id))
                        responseData.Center_Id = partner.Center_Id;
                    if (partner.Partner_Id != null)
                        responseData.Partner_Id = partner.Partner_Id;
                    if (!string.IsNullOrEmpty(partner.Partner_Name))
                        responseData.Partner_Name = partner.Partner_Name;
                    if (!string.IsNullOrEmpty(partner.Partner_Tagline))
                        responseData.Partner_Tagline = partner.Partner_Tagline;
                    if (!string.IsNullOrEmpty(partner.Partner_Teaser))
                        responseData.Partner_Teaser = partner.Partner_Teaser;
                    if (!string.IsNullOrEmpty(partner.Partner_Website))
                        responseData.Partner_Website = partner.Partner_Website;
                    if (!string.IsNullOrEmpty(partner.Partner_Image_Id))
                        responseData.Partner_Image_Id = partner.Partner_Image_Id;
                    if (!string.IsNullOrEmpty(partner.Partner_Start_Date))
                        responseData.Partner_Start_Date = partner.Partner_Start_Date;
                    if (!string.IsNullOrEmpty(partner.Partner_End_Date))
                        responseData.Partner_End_Date = partner.Partner_End_Date;
                }
            }

            return responseData;
        }

        public static Testimonials GetTestimonials(Testimonials localTestimonialsData)
        {
            Testimonials responseData = localTestimonialsData;
            var data = GetFransTestimonialData();
            if (!string.IsNullOrEmpty(data))
            {
                Testimonials testimonials = new Testimonials();

                if (testimonials != null)
                {
                    if (!string.IsNullOrEmpty(testimonials.Center_Id))
                        responseData.Center_Id = testimonials.Center_Id;
                    if (!string.IsNullOrEmpty(testimonials.Statement))
                        responseData.Statement = testimonials.Statement;
                    if (!string.IsNullOrEmpty(testimonials.FirstName))
                        responseData.FirstName = testimonials.FirstName;
                    if (!string.IsNullOrEmpty(testimonials.LastName))
                        responseData.LastName = testimonials.LastName;
                    if (!string.IsNullOrEmpty(testimonials.Organization))
                        responseData.Organization = testimonials.Organization;
                    if (!string.IsNullOrEmpty(testimonials.Created_Date))
                        responseData.Created_Date = testimonials.Created_Date;
                }
            }

            return responseData;
        }

        public static Employee GetEmployee(Employee localEmployee)
        {
            Employee responseData = localEmployee;
            var data = GetFransEmployeeIds();
            if (data.Count > 0)
            {
                Employee employee = new Employee();

                if (employee != null)
                {
                    if (!string.IsNullOrEmpty(employee.FransId))
                        responseData.FransId = employee.FransId;
                    if (!string.IsNullOrEmpty(employee.UserName))
                        responseData.UserName = employee.UserName;
                    if (!string.IsNullOrEmpty(employee.FirstName))
                        responseData.FirstName = employee.FirstName;
                    if (!string.IsNullOrEmpty(employee.LastName))
                        responseData.LastName = employee.LastName;
                    if (!string.IsNullOrEmpty(employee.MobileNumber))
                        responseData.MobileNumber = employee.MobileNumber;
                    if (!string.IsNullOrEmpty(employee.WorkPhone))
                        responseData.WorkPhone = employee.WorkPhone;
                    if (!string.IsNullOrEmpty(employee.FaxPhone))
                        responseData.FaxPhone = employee.FaxPhone;
                    if (!string.IsNullOrEmpty(employee.Email))
                        responseData.Email = employee.Email;
                    if (!string.IsNullOrEmpty(employee.Title))
                        responseData.Title = employee.Title;
                    if (!string.IsNullOrEmpty(employee.Bio))
                        responseData.Bio = employee.Bio;
                    if (!string.IsNullOrEmpty(employee.PicturePath))
                        responseData.PicturePath = employee.PicturePath;
                    if (!string.IsNullOrEmpty(employee.Gender))
                        responseData.Gender = employee.Gender;
                    if (!string.IsNullOrEmpty(employee.Roles))
                        responseData.Roles = employee.Roles;
                    if (!string.IsNullOrEmpty(employee.IsActive.ToString()))
                        responseData.IsActive = employee.IsActive;
                }
            }

            return responseData;
        }
        
        public static SocialMedia GetFransSocialMediaLinks(SocialMedia nationalSMData)
        {
            SocialMedia responseData = null;
            var localSocialLinks = GetFransSocialMediaData();
            if (localSocialLinks != null)
            {
                responseData = new SocialMedia();
                //read all image path from National Data
                responseData.FaceBookImgPath = nationalSMData.FaceBookImgPath;
                responseData.FlickrImgPath = nationalSMData.FlickrImgPath;
                responseData.GooglePlusImgPath = nationalSMData.GooglePlusImgPath;
                responseData.LinkedInImgPath = nationalSMData.LinkedInImgPath;
                responseData.MarketingTangoImgPath = nationalSMData.MarketingTangoImgPath;
                responseData.StumbleUponImgPath = nationalSMData.StumbleUponImgPath;
                responseData.TwitterImgPath = nationalSMData.TwitterImgPath;
                responseData.YouTubeImgPath = nationalSMData.YouTubeImgPath;                

                //FB
                responseData.FaceBookUrl = localSocialLinks.FaceBookUrl;
                //Flickr
                responseData.FlickrUrl = localSocialLinks.FlickrUrl;
                //Google+
                responseData.GooglePlusUrl = localSocialLinks.GooglePlusUrl;
                //Linkedin
                responseData.LinkedInUrl = localSocialLinks.LinkedInUrl;
                //MarketingTango
                responseData.MarketingTangoUrl = localSocialLinks.MarketingTangoUrl;
                //StumbleUpon
                responseData.StumbleUponUrl = localSocialLinks.StumbleUponUrl;
                //Twitter
                responseData.TwitterUrl = localSocialLinks.TwitterUrl;
                //YouTube
                responseData.YouTubeUrl = localSocialLinks.YouTubeUrl;
            }
            return responseData;
        }

        /// <summary>
        /// This method is used to get the current local flickr userid
        /// </summary>
        /// <returns></returns>
        public static string GetFlickUserId()
        {
            string userId = string.Empty;
            var data = GetFransThirdPartyData();
            if (data != null)
                userId = data.FlickrUserId;

            return userId;
        }
        
        /// <summary>
        /// This method is used to get all franchise data in the system
        /// </summary>
        /// <returns></returns>
        public static List<FransData> GetAllFransLocationDataList(bool refreshCache = false)
        {
            List<FransData> dataList = new List<FransData>();
            dataList = GetAllCenterData(refreshCache);

            return dataList;
        }

        public static FransData GetFransData(string fransId = "")
        {
            FransData fransData = null;
            if (fransId == "")
                fransId = GetFranchiseId();
            if (!string.IsNullOrEmpty(fransId))
            {
                var getAllCenterData = GetAllCenterData();
                if (getAllCenterData != null && getAllCenterData.Any())
                {
                    fransData = getAllCenterData.Where(x => x.FransId.ToLower() == fransId.ToLower()).FirstOrDefault();
                }
            }
            return fransData;
        }

        public static FransWorkareaData GetFransWorkareaData(string fransId = "", bool refresh = false)
        {
            FransWorkareaData fransWorkAreaData = null;
            if (fransId == "")
                fransId = GetFranchiseId();
            if (!string.IsNullOrEmpty(fransId))
            {
                try
                {
                    string cacheKey = "SirSpeedyMain-FransWorkareaData" + fransId;
                    bool dataExistInCache = ApplicationCache.IsExist(cacheKey);

                    if (!dataExistInCache || refresh == true)
                    {
                        using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                        {
                            string procName = "Get_All_Center_Workarea_Data";
                            using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                            {
                                sqlCommand.CommandText = procName;
                                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                                sqlCommand.Parameters.Clear();
                                sqlCommand.Parameters.Add("@CenterId", SqlDbType.NVarChar).Value = fransId;
                                sqlConnection.Open();
                                var reader = sqlCommand.ExecuteReader();

                                if (reader != null && reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        fransWorkAreaData = new FransWorkareaData()
                                        {
                                            BannerContentIds = reader["Center_Banners_Ids"] is DBNull ? null : GetIdsFromString(reader["Center_Banners_Ids"].ToString()),
                                            NewsContentIds = reader["Center_News_Ids"] is DBNull ? null : GetIdsFromString(reader["Center_News_Ids"].ToString()),
                                            //CaseStudiesContentIds = reader["Center_Case_Studies_Ids"] is DBNull ? null : GetIdsFromString(reader["Center_Case_Studies_Ids"].ToString()),
                                            //InTheMediaContentIds = reader["Center_All_In_The_Media_Ids"] is DBNull ? null : GetIdsFromString(reader["Center_All_In_The_Media_Ids"].ToString()),
                                            PartnersContentIds = reader["Center_Partners_Ids"] is DBNull ? null : GetIdsFromString(reader["Center_Partners_Ids"].ToString()),
                                            //BriefsWhitePapersContentIds = reader["Center_Briefs_WhitePapers_Ids"] is DBNull ? null : GetIdsFromString(reader["Center_Briefs_WhitePapers_Ids"].ToString()),
                                            CenterEmployeesTeam = reader["Center_Employee_Ids"] is DBNull ? null : GetIdsFromString(reader["Center_Employee_Ids"].ToString()),
                                            ProductAndServices = reader["Center_Product_Services"] is DBNull ? null : Ektron.Cms.EkXml.Deserialize(typeof(List<ProductsAndServices>), reader["Center_Product_Services"].ToString()) as List<ProductsAndServices>,
                                            ShopContentIds = reader["Center_Shop_Ids"] is DBNull ? null : GetIdsFromString(reader["Center_Shop_Ids"].ToString())
                                        };
                                    }
                                }
                            }
                        }
                        if (fransWorkAreaData == null)
                            fransWorkAreaData = new FransWorkareaData();

                        //fransWorkAreaData.NewsContentIds = GetLocalCentersNewsIds();
                        fransWorkAreaData.CaseStudiesContentIds = GetLocalCentersCaseStudiesIds();
                        fransWorkAreaData.InTheMediaContentIds = GetLocalCentersInTheMediaIds();
                        fransWorkAreaData.BriefsWhitePapersContentIds = GetLocalCentersWhitePapersIds();

                        if (fransWorkAreaData != null)
                            ApplicationCache.Insert(cacheKey, fransWorkAreaData, ConfigHelper.GetValueLong("longCacheInterval"));
                    }
                    else
                    {
                        var cacheData = ApplicationCache.GetValue(cacheKey);
                        if (cacheData != null)
                            fransWorkAreaData = (FransWorkareaData)cacheData;
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }

            return fransWorkAreaData;
        }

        public static FransThirdPartyData GetFransThirdPartyData(string centerId = "", bool refresh = false)
        {
            FransThirdPartyData fransThirdPartyData = null;
            string fransId;
            if (centerId == "")
                fransId = GetFranchiseId();
            else
                fransId = centerId;

            if (!string.IsNullOrEmpty(fransId))
            {
                try
                {
                    string cacheKey = "SirSpeedyMain-FransThirdPartyData" + fransId;
                    bool dataExistInCache = ApplicationCache.IsExist(cacheKey);

                    if (!dataExistInCache || refresh)
                    {
                        using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                        {
                            string procName = "Get_All_Center_ThirdParty_Data";
                            using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                            {
                                sqlCommand.CommandText = procName;
                                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                                sqlCommand.Parameters.Clear();
                                sqlCommand.Parameters.Add("@CenterId", SqlDbType.NVarChar).Value = fransId;
                                sqlConnection.Open();
                                var reader = sqlCommand.ExecuteReader();

                                while (reader.Read())
                                {
                                    fransThirdPartyData = new FransThirdPartyData()
                                    {
                                        FlickrUserId = reader["Center_Flickr_UserName"] is DBNull ? null : reader["Center_Flickr_UserName"].ToString(),
                                        TwitterUrl = reader["Center_Twitter_Url"] is DBNull ? null : reader["Center_Twitter_Url"].ToString(),
                                        SelectedPhotoSetIds = reader["Center_Portfolio_PhotoSetIds"] is DBNull ? null : GetStringIdsFromString(reader["Center_Portfolio_PhotoSetIds"].ToString()),
                                        SocialMediaData = reader["Center_Social_Media_Links"] is DBNull ? null : Ektron.Cms.EkXml.Deserialize(typeof(SocialMedia), reader["Center_Social_Media_Links"].ToString()) as SocialMedia,
                                    };
                                }
                            }
                        }
                        if (fransThirdPartyData != null)
                            ApplicationCache.Insert(cacheKey, fransThirdPartyData, ConfigHelper.GetValueLong("longCacheInterval"));
                    }
                    else
                    {
                        var cacheData = ApplicationCache.GetValue(cacheKey);
                        if (cacheData != null)
                            fransThirdPartyData = (FransThirdPartyData)cacheData;
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }

            return fransThirdPartyData;
        }

        public static FransM3TData GetFransM3TData(string fransId = "", bool refresh = false)
        {
            FransM3TData fransM3TData = null;
            if (string.IsNullOrEmpty(fransId))
                fransId = GetFranchiseId();          

            if (!string.IsNullOrEmpty(fransId))
            {
                try
                {
                    string cacheKey = "SirSpeedyMain-FransM3TData" + fransId;
                    bool dataExistInCache = ApplicationCache.IsExist(cacheKey);

                    if (!dataExistInCache || refresh)
                    {
                        using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                        {
                            string procName = "Get_All_Center_M3T_Data";
                            using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                            {
                                sqlCommand.CommandText = procName;
                                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                                sqlCommand.Parameters.Clear();
                                sqlCommand.Parameters.Add("@CenterId", SqlDbType.NVarChar).Value = fransId;
                                sqlConnection.Open();
                                var reader = sqlCommand.ExecuteReader();

                                while (reader.Read())
                                {
                                    fransM3TData = new FransM3TData()
                                    {
                                        //NewsContentIds = reader["Center_News_Ids"] is DBNull ? null : GetIdsFromString(reader["Center_News_Ids"].ToString()),
                                        //AllInTheMediaIds = reader["Center_All_In_The_Media_Ids"] is DBNull ? null : GetIdsFromString(reader["Center_All_In_The_Media_Ids"].ToString()),
                                        //CaseStudiesContentIds = reader["Center_Case_Studies_Ids"] is DBNull ? null : GetIdsFromString(reader["Center_Case_Studies_Ids"].ToString()),
                                        OurTeamEmployeeIds = reader["Center_Our_Team"] is DBNull ? null : GetIdsFromString(reader["Center_Our_Team"].ToString()),
                                        //BriefsWhitePapersContentIds = reader["Center_Briefs_WhitePapers_Ids"] is DBNull ? null : GetIdsFromString(reader["Center_Briefs_WhitePapers_Ids"].ToString())
                                    };
                                }
                            }
                        }

                        if (fransM3TData == null)
                            fransM3TData = new FransM3TData();

                        fransM3TData.NewsContentIds = GetLocalCentersNewsIds();
                        fransM3TData.CaseStudiesContentIds = GetLocalCentersCaseStudiesIds();
                        fransM3TData.AllInTheMediaIds = GetLocalCentersInTheMediaIds();
                        fransM3TData.BriefsWhitePapersContentIds = GetLocalCentersWhitePapersIds();

                        if (fransM3TData != null)
                            ApplicationCache.Insert(cacheKey, fransM3TData, ConfigHelper.GetValueLong("longCacheInterval"));
                    }
                    else
                    {
                        var cacheData = ApplicationCache.GetValue(cacheKey);
                        if (cacheData != null)
                            fransM3TData = (FransM3TData)cacheData;
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }

            return fransM3TData;
        }
        
        public static string GetFransTwitterUrl()
        {
            string url = string.Empty;
            var thirdPartyData = GetFransThirdPartyData();
            if (thirdPartyData != null)
                url = thirdPartyData.TwitterUrl;

            return url;
        }

        /// <summary>
        /// This method is used to calculate the geolocations based on the center data
        /// </summary>
        /// <param name="centerData">center data</param>
        /// <param name="latitude">out parameter</param>
        /// <param name="longitude">out parameter</param>
        public static void GetCenterGeoLocation(FransData centerData, out string latitude, out string longitude)
        {
            //var googleUrl = "http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false";

            latitude = string.Empty;
            longitude = string.Empty;
            if ((centerData != null) && (!string.IsNullOrEmpty(centerData.Address1)) &&
                (!string.IsNullOrEmpty(centerData.City)) && (!string.IsNullOrEmpty(centerData.State)) &&
                (!string.IsNullOrEmpty(centerData.Zipcode)))
            {
                try
                {
                    StringBuilder sbRequestArress = new StringBuilder();
                    sbRequestArress.Append(centerData.Address1);
                    //if (!string.IsNullOrEmpty(centerData.Address2))
                    //    sbRequestArress.Append(", " + centerData.Address2);

                    sbRequestArress.Append(", " + centerData.City);
                    if (centerData.Country != "AE") //UAE, dubai
                        sbRequestArress.Append(", " + centerData.State);
                    if (centerData.Zipcode != "00000")
                        sbRequestArress.Append("-" + centerData.Zipcode);
                    sbRequestArress.Append(", " + centerData.Country);
                    sbRequestArress = sbRequestArress.Replace(" ", "+");

                    string requestUrl = Utility.GetGoogleMapGeocodeUri(sbRequestArress.ToString());

                    WebRequest request = WebRequest.Create(requestUrl);
                    using (WebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                        {
                            DataSet dsResult = new DataSet();
                            dsResult.ReadXml(reader);
                            if (dsResult != null && dsResult.Tables != null)
                            {
                                foreach (DataRow row in dsResult.Tables["result"].Rows)
                                {
                                    string geometry_id = dsResult.Tables["geometry"].Select("result_id = " + row["result_id"].ToString())[0]["geometry_id"].ToString();
                                    DataRow location = dsResult.Tables["location"].Select("geometry_id = " + geometry_id)[0];
                                    latitude = location["lat"].ToString();
                                    longitude = location["lng"].ToString();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }
        }

        /// <summary>
        /// this method is used to get all jobs from the curent selected center or frans id.
        /// </summary>
        /// <returns></returns>
        public static List<JobPost> GetAllJobsForLocal()
        {
            List<JobPost> jobList = null;            
            var allJobList = GetAllActiveJobs();
            if (allJobList != null && allJobList.Any())
            {               
                string  fransId = GetFranchiseId();
                jobList = allJobList.Where(x => x.CenterId == fransId).ToList();
            }
            return jobList;
        }
        
        /// <summary>
        /// this method is used to get all latest jobs from all location
        /// </summary>
        /// <returns></returns>
        public static List<JobPost> GetAllJobsForNational()
        {
            return GetAllActiveJobs(); 
        }
        
        /// <summary>
        /// This method is used to save the end users input for request to quote data
        /// </summary>       
        public static void SaveRequestToQuoteData(string centerId, string firstName, string lastName, string jobTitle, string companyName, string email, string phoneNumber, string projectName, string projectDescription, string projectBudget, string ektronUploadedFilesIds, string duedate, string email_opt_in)
        {
            try
            {
                //send email
                SendRequestToQuoteInfoToCenter(centerId, firstName, lastName, jobTitle, companyName, email, phoneNumber, projectName, projectDescription, projectBudget, ektronUploadedFilesIds, duedate, email_opt_in);

                string domainName = "http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string procName = "Save_Request_Quote_Data";

                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = procName;
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.Parameters.Clear();
                        sqlCommand.Parameters.Add("@CenterId", SqlDbType.NVarChar).Value = centerId;
                        sqlCommand.Parameters.Add("@First_Name", SqlDbType.NVarChar).Value = firstName;
                        sqlCommand.Parameters.Add("@Last_Name", SqlDbType.NVarChar).Value = lastName;
                        sqlCommand.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;
                        sqlCommand.Parameters.Add("@Job_Title", SqlDbType.NVarChar).Value = jobTitle;
                        sqlCommand.Parameters.Add("@Company_Name", SqlDbType.NVarChar).Value = companyName;
                        sqlCommand.Parameters.Add("@WebSite", SqlDbType.NVarChar).Value = "";
                        sqlCommand.Parameters.Add("@Mobile_Number", SqlDbType.NVarChar).Value = phoneNumber;
                        sqlCommand.Parameters.Add("@Project_Name", SqlDbType.NVarChar).Value = projectName;
                        sqlCommand.Parameters.Add("@Project_Description", SqlDbType.NVarChar).Value = projectDescription;
                        sqlCommand.Parameters.Add("@Uploaded_File_Ids", SqlDbType.NVarChar).Value = ektronUploadedFilesIds;
                        sqlCommand.Parameters.Add("@Project_Budget", SqlDbType.NVarChar).Value = projectBudget;
                        sqlCommand.Parameters.Add("@Domain", SqlDbType.NVarChar).Value = domainName;
                        sqlCommand.Parameters.Add("@Due_Date", SqlDbType.NVarChar).Value = duedate;
                        sqlCommand.Parameters.Add("@Email_Opt_In", SqlDbType.Int).Value = email_opt_in.ToLower().Equals("false")?0:1;
                        sqlConnection.Open();
                        var reader = sqlCommand.ExecuteNonQuery();                        
                    }
                }
            }
            catch(Exception ex)
            {
                Log.WriteError(ex.Message + " : " + ex.InnerException + " : " + ex.StackTrace);
            }
        }

        /// <summary>
        /// This method is used to save the end users job application data
        /// </summary>        
        public static void SaveJobApplicationData(long jobId, string centerId, string firstName, string lastName, string city, string state, string zipcode, string email, string resumeAssetId, string coverNotes, string jobApplicationUrl, string resumeUrl)
        {
            try
            {                
                //send email
                SendEmailApplicationJob(centerId, firstName, lastName, city, state, zipcode, email, coverNotes, jobApplicationUrl, resumeUrl);

                string domainName = "http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string procName = "Save_Job_Application_Data";

                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = procName;
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.Parameters.Clear();
                        sqlCommand.Parameters.Add("@JobId", SqlDbType.NVarChar).Value = jobId;
                        sqlCommand.Parameters.Add("@CenterId", SqlDbType.NVarChar).Value = centerId;
                        sqlCommand.Parameters.Add("@First_Name", SqlDbType.NVarChar).Value = firstName;
                        sqlCommand.Parameters.Add("@Last_Name", SqlDbType.NVarChar).Value = lastName;
                        sqlCommand.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;
                        sqlCommand.Parameters.Add("@City", SqlDbType.NVarChar).Value = city;
                        sqlCommand.Parameters.Add("@State", SqlDbType.NVarChar).Value = state;
                        sqlCommand.Parameters.Add("@Zipcode", SqlDbType.NVarChar).Value = "";
                        sqlCommand.Parameters.Add("@ResumeAssetId", SqlDbType.NVarChar).Value = resumeAssetId;
                        sqlCommand.Parameters.Add("@CoverNotes", SqlDbType.NVarChar).Value = coverNotes;
                        sqlCommand.Parameters.Add("@Domain", SqlDbType.NVarChar).Value = domainName;  
                        sqlConnection.Open();
                        var reader = sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.Message + ":" + ex.StackTrace);
            }
        }

        /// <summary>
        /// This method is used to save the end users input for request to quote data
        /// </summary>       
        //public static void SaveSendAFileData(string centerId, string firstName, string lastName, string jobTitle, string companyName, string email, string phoneNumber, string projectName, string projectDescription, string projectQuantity, string ektronUploadedFilesIds, string externalFileLinks)
        public static bool SaveSendAFileData(string centerId, string firstName, string lastName, string jobTitle, string companyName, string email, string phoneNumber, string projectName, string projectDescription, string projectQuantity, string ektronUploadedFilesIds, string externalFileLinks, string projectDueDate)    
        {
            try
            {
                //send email
                SendSendAFileInfoToCenter(centerId, firstName, lastName, jobTitle, companyName, email, phoneNumber, projectName, projectDescription, projectQuantity, ektronUploadedFilesIds, externalFileLinks, projectDueDate);

                string domainName = "http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string procName = "Save_Send_A_File_Data";

                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = procName;
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.Parameters.Clear();
                        sqlCommand.Parameters.Add("@Center_Id", SqlDbType.NVarChar).Value = centerId;
                        sqlCommand.Parameters.Add("@First_Name", SqlDbType.NVarChar).Value = firstName;
                        sqlCommand.Parameters.Add("@Last_Name", SqlDbType.NVarChar).Value = lastName;
                        sqlCommand.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;
                        sqlCommand.Parameters.Add("@Job_Title", SqlDbType.NVarChar).Value = jobTitle;
                        sqlCommand.Parameters.Add("@Company_Name", SqlDbType.NVarChar).Value = companyName;
                        sqlCommand.Parameters.Add("@WebSite", SqlDbType.NVarChar).Value = "";
                        sqlCommand.Parameters.Add("@Mobile_Number", SqlDbType.NVarChar).Value = phoneNumber;
                        sqlCommand.Parameters.Add("@Project_Name", SqlDbType.NVarChar).Value = projectName;
                        sqlCommand.Parameters.Add("@Project_Description", SqlDbType.NVarChar).Value = projectDescription;
                        sqlCommand.Parameters.Add("@Uploaded_File_Ids", SqlDbType.NVarChar).Value = ektronUploadedFilesIds;
                        sqlCommand.Parameters.Add("@Project_Quantity", SqlDbType.NVarChar).Value = projectQuantity;
                        sqlCommand.Parameters.Add("@Domain", SqlDbType.NVarChar).Value = domainName;
                        sqlCommand.Parameters.Add("@Project_Due_Date", SqlDbType.NVarChar).Value = projectDueDate;
                        sqlConnection.Open();
                        var reader = sqlCommand.ExecuteNonQuery();
                    }
                }

                int sendAFileId = getMaxSendAFileId();

                var externalFileLinksArray = externalFileLinks.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string externalFileLink in externalFileLinksArray)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                    {
                        string procName = "Save_Third_Party_Links";

                        using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                        {
                            sqlCommand.CommandText = procName;
                            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                            sqlCommand.Parameters.Clear();
                            sqlCommand.Parameters.Add("@Send_A_File_Id", SqlDbType.BigInt).Value = sendAFileId;
                            sqlCommand.Parameters.Add("@File_Link", SqlDbType.NVarChar).Value = externalFileLink;
                            sqlConnection.Open();
                            var reader = sqlCommand.ExecuteNonQuery();
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.Message + " : " + ex.InnerException + " : " + ex.StackTrace);
            }

            return false;
        }


        /// <summary>
        /// This method is used to save the end users input for request to quote data
        /// </summary>       
        public static bool SaveUserRegisterationData(string centerId, string firstName, string lastName, string jobTitle, string companyName, string email, string phoneNumber, string website)
        {
            //SqlConnection conn = new SqlConnection(adminToolConnectionString);

            try
            {
                //send email
                SendRegisterToCenter(centerId, firstName, lastName, jobTitle, companyName, email, phoneNumber, website);

                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string procName = "Save_Register_Data";

                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = procName;
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.Parameters.Clear();
                        sqlCommand.Parameters.Add("@Center_Id", SqlDbType.NVarChar).Value = centerId;
                        sqlCommand.Parameters.Add("@First_Name", SqlDbType.NVarChar).Value = firstName;
                        sqlCommand.Parameters.Add("@Last_Name", SqlDbType.NVarChar).Value = lastName;
                        sqlCommand.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;
                        sqlCommand.Parameters.Add("@Job_Title", SqlDbType.NVarChar).Value = jobTitle;
                        sqlCommand.Parameters.Add("@Company_Name", SqlDbType.NVarChar).Value = companyName;
                        sqlCommand.Parameters.Add("@WebSite", SqlDbType.NVarChar).Value = website;
                        sqlCommand.Parameters.Add("@Phone_Number", SqlDbType.NVarChar).Value = phoneNumber;
                        sqlConnection.Open();
                        var reader = sqlCommand.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
            return false;
        }

        /// <summary>
        /// This method is used to save the user subscription data
        /// </summary>       
        public static void SaveUserSubscribeData(string centerId, string printType, string onlineType, string firstName, string lastName, string email, string address, string city, string state, string zipcode)
        {
            try
            {
                //send email
                SendUserSubscribeDataInfoToCenter(centerId, printType, onlineType, firstName, lastName, email, address, city, state, zipcode);

                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string procName = "Add_New_Subscribtion";

                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = procName;
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.Parameters.Clear();
                        sqlCommand.Parameters.Add("@Center_Id", SqlDbType.NVarChar).Value = centerId;
                        sqlCommand.Parameters.Add("@First_Name", SqlDbType.NVarChar).Value = firstName;
                        sqlCommand.Parameters.Add("@Last_Name", SqlDbType.NVarChar).Value = lastName;
                        sqlCommand.Parameters.Add("@Email_Address", SqlDbType.NVarChar).Value = email;
                        sqlCommand.Parameters.Add("@User_Address1", SqlDbType.NVarChar).Value = address;
                        sqlCommand.Parameters.Add("@User_Address2", SqlDbType.NVarChar).Value = "";
                        sqlCommand.Parameters.Add("@User_City", SqlDbType.NVarChar).Value = city;
                        sqlCommand.Parameters.Add("@User_State", SqlDbType.NVarChar).Value = state;
                        sqlCommand.Parameters.Add("@User_Zipcode", SqlDbType.NVarChar).Value = zipcode;
                        sqlCommand.Parameters.Add("@Subscribe_Online", SqlDbType.NVarChar).Value = onlineType;
                        sqlCommand.Parameters.Add("@Subscribe_Print", SqlDbType.NVarChar).Value = printType;
                        sqlConnection.Open();
                        var reader = sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
        }


        /// <summary>
        /// This method is used to get center about us content data
        /// </summary>
        /// <param name="centerId">centerId</param>
        /// <returns></returns>
        public static AboutCenter GetAboutUsContent(string centerId)
        {
            AboutCenter centerData = null;
            if (!string.IsNullOrEmpty(centerId))
            {
                try
                {
                    string cacheKey = "SirSpeedyMain-GetAboutUsContent" + centerId;
                    bool dataExistInCache = ApplicationCache.IsExist(cacheKey);

                    if (!dataExistInCache)
                    {                       
                        using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                        {
                            string procName = "Get_About_Us_Content";

                            using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                            {
                                sqlCommand.CommandText = procName;
                                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                                sqlConnection.Open();
                                var reader = sqlCommand.ExecuteReader();

                                while (reader.Read())
                                {
                                    centerData = new AboutCenter()
                                    {
                                        Title = reader["Title"] is DBNull ? null : reader["Title"].ToString(),
                                        SubTitle = reader["SubTitle"] is DBNull ? null : reader["SubTitle"].ToString(),
                                        ImagePath = reader["Center_Logo_Id"] is DBNull ? null : GetImagePath(reader["Center_Logo_Id"].ToString()),
                                        Description = reader["Center_Description"] is DBNull ? null : reader["Center_Description"].ToString(),
                                        VideoSRC = reader["Video_Link"] is DBNull ? null : reader["Video_Link"].ToString(),
                                        Statement = reader["Statement_Text"] is DBNull ? null : reader["Statement_Text"].ToString(),
                                        Disclaimer = reader["Disclaimer"] is DBNull ? null : reader["Disclaimer"].ToString()
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
                            centerData = (AboutCenter)cacheData;
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }
            return centerData;
        }

        public static List<Employee> GetAllEmployee(string fransId = "", bool refreshCache = false)
        {
            List<Employee> employeeList = new List<Employee>();

            try
            {
                if (fransId == "")
                    fransId = FransDataManager.GetFranchiseId();

                string cacheKey = "SirSpeedyMain-AllEmployeeDataFromdb" + fransId;
                bool dataExistInCache = ApplicationCache.IsExist(cacheKey);

                if (!dataExistInCache || refreshCache)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                    {
                        string procName = "Get_All_Center_Employees_Data";

                        using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                        {
                            sqlCommand.CommandText = procName;
                            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                            sqlCommand.Parameters.Clear();
                            sqlCommand.Parameters.Add("@CenterId", SqlDbType.NVarChar).Value = fransId;
                            sqlConnection.Open();
                            var reader = sqlCommand.ExecuteReader();

                            while (reader.Read())
                            {
                                Employee t = new Employee()
                                {
                                    EmployeeId = Convert.ToInt64(reader["UserId"]),
                                    FransId = reader["Center_Id"] is DBNull ? null : reader["Center_Id"].ToString(),
                                    UserName = reader["UserName"] is DBNull ? null : reader["UserName"].ToString(),
                                    FirstName = reader["First_Name"] is DBNull ? null : reader["First_Name"].ToString(),
                                    LastName = reader["Last_Name"] is DBNull ? null : reader["Last_Name"].ToString(),
                                    MobileNumber = reader["Mobile_Number"] is DBNull ? null : reader["Mobile_Number"].ToString(),
                                    WorkPhone = reader["Work_PhoneNumber"] is DBNull ? null : reader["Work_PhoneNumber"].ToString(),
                                    FaxPhone = reader["Fax_PhoneNumber"] is DBNull ? null : reader["Fax_PhoneNumber"].ToString(),
                                    Email = reader["Email"] is DBNull ? null : reader["Email"].ToString(),
                                    Title = reader["Title"] is DBNull ? null : reader["Title"].ToString(),
                                    Bio = reader["Bio"] is DBNull ? null : reader["Bio"].ToString(),
                                    PicturePath = reader["Picture"] is DBNull ? null : reader["Picture"].ToString(),
                                    Gender = reader["Gender"] is DBNull ? null : reader["Gender"].ToString(),
                                    Roles = reader["Roles"] is DBNull ? null : reader["Roles"].ToString(),
                                    IsActive = Convert.ToInt32(reader["IsActive"])
                                };
                                employeeList.Add(t);
                            }
                        }
                    }
                    if (employeeList != null && employeeList.Any())
                        ApplicationCache.Insert(cacheKey, employeeList, ConfigHelper.GetValueLong("longCacheInterval"));
                }
                else
                {
                    var cacheData = ApplicationCache.GetValue(cacheKey);
                    if (cacheData != null)
                        employeeList = (List<Employee>)cacheData;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
            return employeeList;
        }

        public static Employee GetEmployeeById(long employeeId, bool refreshCache = false)
        {
            Employee employeeData = null;
            try
            {
                string cacheKey = "SirSpeedyMain-GetEmployeeDataFromdb" + employeeId;
                bool dataExistInCache = ApplicationCache.IsExist(cacheKey);

                if (!dataExistInCache || refreshCache)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                    {
                        string procName = "Get_Center_User";

                        using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                        {
                            sqlCommand.CommandText = procName;
                            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                            sqlCommand.Parameters.Clear();
                            sqlCommand.Parameters.Add("@UserId", SqlDbType.NVarChar).Value = employeeId;
                            sqlConnection.Open();
                            var reader = sqlCommand.ExecuteReader();

                            while (reader.Read())
                            {
                                employeeData = new Employee()
                                {
                                    EmployeeId = Convert.ToInt64(reader["UserId"]),
                                    FransId = reader["Center_Id"] is DBNull ? null : reader["Center_Id"].ToString(),
                                    UserName = reader["UserName"] is DBNull ? null : reader["UserName"].ToString(),
                                    FirstName = reader["First_Name"] is DBNull ? null : reader["First_Name"].ToString(),
                                    LastName = reader["Last_Name"] is DBNull ? null : reader["Last_Name"].ToString(),
                                    MobileNumber = reader["Mobile_Number"] is DBNull ? null : reader["Mobile_Number"].ToString(),
                                    WorkPhone = reader["Work_PhoneNumber"] is DBNull ? null : reader["Work_PhoneNumber"].ToString(),
                                    FaxPhone = reader["Fax_PhoneNumber"] is DBNull ? null : reader["Fax_PhoneNumber"].ToString(),
                                    Email = reader["Email"] is DBNull ? null : reader["Email"].ToString(),
                                    Title = reader["Title"] is DBNull ? null : reader["Title"].ToString(),
                                    Bio = reader["Bio"] is DBNull ? null : reader["Bio"].ToString(),
                                    PicturePath = reader["Picture"] is DBNull ? null : reader["Picture"].ToString(),
                                    Gender = reader["Gender"] is DBNull ? null : reader["Gender"].ToString(),
                                    Roles = reader["Roles"] is DBNull ? null : reader["Roles"].ToString(),
                                    IsActive = Convert.ToInt32(reader["IsActive"])
                                };                                
                            }
                        }
                    }
                    if (employeeData != null)
                        ApplicationCache.Insert(cacheKey, employeeData, ConfigHelper.GetValueLong("longCacheInterval"));
                }
                else
                {
                    var cacheData = ApplicationCache.GetValue(cacheKey);
                    if (cacheData != null)
                        employeeData = cacheData as Employee;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
            return employeeData;
        }

        public static int AddCmsGroupToCenter(string centerId, long groupId)
        {
            int status = -1;
            using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
            {
                string query = "update CenterData set Center_Cms_GroupId = '" + groupId + "' where Center_Id = '" + centerId + "'";
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = query;
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlConnection.Open();
                    status = sqlCommand.ExecuteNonQuery();
                }
            }

            return status;
        }

        /// <summary>
        /// this method is used to read all the jobs post from custom db
        /// </summary>
        /// <returns></returns>
        public static List<JobPost> GetAllActiveJobs(bool refesh= false)
        {
            List<JobPost> jobList = null;
            try
            {
                string cacheKey = "SirSpeedyMain-GetAllJobsFromCustomdb";
                bool dataExistInCache = ApplicationCache.IsExist(cacheKey);

                if (!dataExistInCache || refesh)
                {
                    jobList = new List<JobPost>();
                    using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                    {
                        string procName = "Get_All_Active_Jobs";

                        using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                        {
                            sqlCommand.CommandText = procName;
                            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                            sqlConnection.Open();
                            var reader = sqlCommand.ExecuteReader();

                            while (reader.Read())
                            {
                                JobPost j = new JobPost()
                                {
                                    JobId = reader["Job_Id"] is DBNull ? 0 : long.Parse(reader["Job_Id"].ToString()),
                                    CenterId = reader["Center_Id"] is DBNull ? null : reader["Center_Id"].ToString(),
                                    Title = reader["Job_Title"] is DBNull ? null : reader["Job_Title"].ToString(),
                                    ProfileType = reader["Job_Profile_Type"] is DBNull ? null : reader["Job_Profile_Type"].ToString(),
                                    Location = reader["Job_Location"] is DBNull ? null : reader["Job_Location"].ToString(),
                                    Description = reader["Job_Description"] is DBNull ? null : reader["Job_Description"].ToString(),
                                    DatePosted = reader["Job_Posted_Date"] is DBNull ? DateTime.MinValue : DateTime.Parse(reader["Job_Posted_Date"].ToString()),
                                    DateExpire = reader["Job_Expired_Date"] is DBNull ? DateTime.MinValue : DateTime.Parse(reader["Job_Expired_Date"].ToString()),
                                    IsPartTime = reader["Job_Is_PartTime"] is DBNull ? false : (reader["Job_Is_PartTime"].ToString() != string.Empty && reader["Job_Is_PartTime"].ToString() == "1" ? true : false),
                                    IsFullTime = reader["Job_Is_FullTime"] is DBNull ? false : (reader["Job_Is_FullTime"].ToString() != string.Empty && reader["Job_Is_FullTime"].ToString() == "1" ? true : false)
                                };
                                jobList.Add(j);
                            }
                        }
                    }
                    if (jobList != null && jobList.Any())
                        ApplicationCache.Insert(cacheKey, jobList, ConfigHelper.GetValueLong("longCacheInterval"));
                }
                else
                {
                    var cacheData = ApplicationCache.GetValue(cacheKey);
                    if (cacheData != null)
                        jobList = (List<JobPost>)cacheData;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
            return jobList;
        }
        

        public static List<Shop> GetAllShopsData(string centerId, bool refesh = false)
        {
            List<Shop> shopList = null;
            try
            {
                string cacheKey = "SirSpeedyMain-GetAllShopsDataFromCustomdb" + centerId;
                bool dataExistInCache = ApplicationCache.IsExist(cacheKey);

                if (!dataExistInCache || refesh)
                {
                    shopList = new List<Shop>();
                    using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                    {
                        string procName = "Get_All_Shop_CenterData";
                        using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                        {
                            sqlCommand.CommandText = procName;
                            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                            sqlConnection.Open();
                            sqlCommand.Parameters.Clear();
                            sqlCommand.Parameters.Add("@Center_Id", SqlDbType.NVarChar).Value = centerId;
                            var reader = sqlCommand.ExecuteReader();

                            while (reader.Read())
                            {
                                Shop j = new Shop()
                                {
                                    ContentId = reader["Shop_Content_Id"] is DBNull ? 0 : long.Parse(reader["Shop_Content_Id"].ToString()),
                                    Link = reader["Shop_URL"] is DBNull ? null : reader["Shop_URL"].ToString(),
                                    IsActive = reader["IsActiveShop"] is DBNull ? false : (int.Parse(reader["IsActiveShop"].ToString()) >= 1 ? true : false)
                                };
                                shopList.Add(j);
                            }
                        }
                    }
                    if (shopList != null && shopList.Any())
                        ApplicationCache.Insert(cacheKey, shopList, ConfigHelper.GetValueLong("longCacheInterval"));
                }
                else
                {
                    var cacheData = ApplicationCache.GetValue(cacheKey);
                    if (cacheData != null)
                        shopList = (List<Shop>)cacheData;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
            return shopList;
        }

        public static WhyWeAreDiff GetWhyWeAreDiffContent(string centerId, bool refresh = false)
        {
            WhyWeAreDiff data = null;
            try
            {
                string cacheKey = "SirSpeedyMain-GetWhyWeAreDiffContent" + centerId;
                bool dataExistInCache = ApplicationCache.IsExist(cacheKey);

                if (!dataExistInCache || refresh)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                    {
                        string procName = "Get_Why_We_Are_Diff_Content";

                        using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                        {
                            sqlCommand.CommandText = procName;
                            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                            sqlCommand.Parameters.Clear();
                            sqlCommand.Parameters.Add("@CenterId", SqlDbType.NVarChar).Value = centerId;
                            sqlConnection.Open();
                            var reader = sqlCommand.ExecuteReader();

                            while (reader.Read())
                            {
                                data = new WhyWeAreDiff()
                                {
                                    WhyWeAreDiffId = reader["Why_We_Diff_Cid"] is DBNull ? 0 : long.Parse(reader["Why_We_Diff_Cid"].ToString()),
                                    BannerTitle = reader["Banner_Title"] is DBNull ? null : reader["Banner_Title"].ToString(),
                                    BannerSubTitle = reader["Banner_SubTitle"] is DBNull ? null : reader["Banner_SubTitle"].ToString(),
                                    ContentTitle = reader["Content_Title"] is DBNull ? null : reader["Content_Title"].ToString(),
                                    ContentTagLine = reader["Content_TagLine"] is DBNull ? null : reader["Content_TagLine"].ToString(),
                                    ContentDescription = reader["Content_Description"] is DBNull ? null : reader["Content_Description"].ToString(),
                                    VideoLink = reader["Video_Link"] is DBNull ? null : reader["Video_Link"].ToString(),
                                    VideoStatementText = reader["Video_Statement_Text"] is DBNull ? null : reader["Video_Statement_Text"].ToString(),
                                    DateCreated = reader["Date_Created"] is DBNull ? DateTime.MinValue : DateTime.Parse(reader["Date_Created"].ToString()),
                                    DateModified = reader["Date_Modified"] is DBNull ? DateTime.MinValue : DateTime.Parse(reader["Date_Modified"].ToString()),
                                    Image_Path = reader["Picute_Path"] is DBNull ? null : reader["Picute_Path"].ToString()
                                };                        
                            }
                        }
                    }
                    if (data != null)
                        ApplicationCache.Insert(cacheKey, data, ConfigHelper.GetValueLong("longCacheInterval"));
                }
                else
                {
                    var cacheData = ApplicationCache.GetValue(cacheKey);
                    if (cacheData != null)
                        data = (WhyWeAreDiff)cacheData;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
            return data;
        }

        public static List<Testimonials> GetAllTestimonials(string centerId, bool refresh = false)
        {
            List<Testimonials> testimonialList = new List<Testimonials>();
            try
            {
                string cacheKey = "SirSpeedyMain-AllTestimonialsDataFromdb" + centerId;
                bool dataExistInCache = ApplicationCache.IsExist(cacheKey);
                if (!dataExistInCache || refresh)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                    {
                        string procName = "Get_All_Testimonials";
                        using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                        {
                            sqlCommand.CommandText = procName;
                            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                            sqlCommand.Parameters.Clear();
                            sqlCommand.Parameters.Add("@CenterId", SqlDbType.NVarChar).Value = centerId;
                            sqlConnection.Open();
                            var reader = sqlCommand.ExecuteReader();

                            while (reader.Read())
                            {
                                Testimonials t = new Testimonials()
                                {
                                    Title = reader["Title"] is DBNull ? null : reader["Title"].ToString(),
                                    Center_Id = reader["Center_Id"] is DBNull ? null : reader["Center_Id"].ToString(),
                                    FirstName = reader["FirstName"] is DBNull ? null : reader["FirstName"].ToString(),
                                    LastName = reader["LastName"] is DBNull ? null : reader["LastName"].ToString(),
                                    Organization = reader["Organization"] is DBNull ? null : reader["Organization"].ToString(),
                                    Statement = reader["Testimonial"] is DBNull ? null : reader["Testimonial"].ToString(),
                                    Created_Date = reader["Created_Date"] is DBNull ? null : reader["Created_Date"].ToString(),
                                    TestimonialId = reader["id"] is DBNull ? 0 : int.Parse(reader["id"].ToString()),
                                    PicturePath = reader["Testimonial_Image"] is DBNull ? null : reader["Testimonial_Image"].ToString()
                                };
                                testimonialList.Add(t);
                            }
                        }
                    }
                    if (testimonialList != null && testimonialList.Any())
                        ApplicationCache.Insert(cacheKey, testimonialList, ConfigHelper.GetValueLong("longCacheInterval"));
                }
                else
                {
                    var cacheData = ApplicationCache.GetValue(cacheKey);
                    if (cacheData != null)
                        testimonialList = (List<Testimonials>)cacheData;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
            return testimonialList;
        }

        public static List<Promotion> GetSelectedPromos(string centerId, bool refresh = false)
        {
            List<Promotion> promosList = null;
            try
            {
                string cacheKey = "SirSpeedyMain-SeletedPromoIdsDataFromdb" + centerId;
                bool dataExistInCache = ApplicationCache.IsExist(cacheKey);

                if (!dataExistInCache || refresh)
                {
                    promosList = new List<Promotion>();
                    using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                    {
                        string procName = "Get_Center_Promotion_Data";
                        using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                        {
                            sqlCommand.CommandText = procName;
                            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;                                                    
                            sqlCommand.Parameters.Clear();
                            sqlCommand.Parameters.Add("@Center_Id", SqlDbType.NVarChar).Value = centerId;
                            sqlConnection.Open();
                            var reader = sqlCommand.ExecuteReader();

                            while (reader.Read())
                            {
                                Promotion p = new Promotion()
                                {
                                    PromotionId = Convert.ToInt64(reader["Promotion_Cid"]),
                                    PromoImage1Link = reader["Center_Promo_Image11_Link"] is DBNull ? "" : reader["Center_Promo_Image11_Link"].ToString(),
                                    PromoImage2Link = reader["Center_Promo_Image12_Link"] is DBNull ? "" : reader["Center_Promo_Image12_Link"].ToString(),
                                    SelectedPromoIds = reader["Promotion_Selected_Ids"] is DBNull ? null : GetIdsFromString(reader["Promotion_Selected_Ids"].ToString()),
                                    ExpireDate = reader["Center_Promo_Expire_Date"] is DBNull || reader["Center_Promo_Expire_Date"].ToString() == string.Empty ? DateTime.MinValue : DateTime.Parse(reader["Center_Promo_Expire_Date"].ToString())
                                };
                                promosList.Add(p);
                            }
                        }
                    }
                    if (promosList != null && promosList.Any())
                        ApplicationCache.Insert(cacheKey, promosList, ConfigHelper.GetValueLong("longCacheInterval"));
                }
                else
                {
                    var cacheData = ApplicationCache.GetValue(cacheKey);
                    if (cacheData != null)
                        promosList = (List<Promotion>)cacheData;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
            return promosList;
        }

        public static List<long> GetPSpageWhitePapersIds(string centerId, bool refresh = false)
        {
            List<long> cIds = new List<long>();
            try
            {
                string cacheKey = "SirSpeedyMain-PSpageWhitePapersIds" + centerId;
                bool dataExistInCache = ApplicationCache.IsExist(cacheKey);
                if (!dataExistInCache || refresh)
                {
                    cIds = AdminToolManager.GetSelectedPSpageWhitePapers(centerId);
                    if (cIds != null && cIds.Any())
                        ApplicationCache.Insert(cacheKey, cIds, ConfigHelper.GetValueLong("longCacheInterval"));
                }
                else
                {
                    var cacheData = ApplicationCache.GetValue(cacheKey);
                    if (cacheData != null)
                        cIds = (List<long>)cacheData;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
            return cIds;
        }

        public static int SaveContactUsRequest(string centerId, string firstName, string lastName, string userComments, string email)
        {
            int status = -1;
            try
            {
                //send email
                SendContactUsInfoToCenter(centerId, firstName, lastName, userComments, email);

                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string procName = "Add_Contact_Us_Request";

                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = procName;
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.Parameters.Clear();
                        sqlCommand.Parameters.Add("@Center_Id", SqlDbType.NVarChar).Value = centerId;
                        sqlCommand.Parameters.Add("@First_Name", SqlDbType.NVarChar).Value = firstName;
                        sqlCommand.Parameters.Add("@Last_Name", SqlDbType.NVarChar).Value = lastName;
                        sqlCommand.Parameters.Add("@User_Comments", SqlDbType.NVarChar).Value = userComments;
                        sqlCommand.Parameters.Add("@Email_Address", SqlDbType.NVarChar).Value = email;                        
                        sqlCommand.Parameters.Add("@Date_Submitted", SqlDbType.DateTime).Value = DateTime.Now;
                        sqlConnection.Open();
                        status = sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
            return status;
        }

        /// <summary>
        /// this method is used to get custom page size and overwrite the default Ektron page size of 50
        /// </summary>
        /// <returns>int page size</returns>
        public static int GetCustomApiPageSize()
        {
            int pageSize = 50; //default
            try
            {
                int.TryParse(ConfigurationManager.AppSettings["EktronContentApiCustomPageSize"], out pageSize);
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
            return pageSize;
        }


        #region *** Private Methods ***

        private static HttpCookie GetFranchiseCookie()
        {
            HttpCookie fransCookie = HttpContext.Current.Request.Cookies[CookieName];
            return fransCookie;
        }        
              
        private static SocialMedia GetFransSocialMediaData()
        {
            SocialMedia mediaData = null;
            var fransId = GetFranchiseId();
            var getFransThirdPartyData = GetFransThirdPartyData(fransId);
            if (getFransThirdPartyData != null)
            {
                mediaData = getFransThirdPartyData.SocialMediaData;
            }
            return mediaData;
        }

        private static string GetFransTestimonialData()
        {
            string jsonData = string.Empty;
            var fransId = GetFranchiseId();
            //$todo make the custom db call and get all the details
            //$todo: add it to cache

            return jsonData;
        }

        private static List<Partners> GetAllPartners()
        {
            List<Partners> partnersList = new List<Partners>();

            try
            {
                //todo: get fransid and make the cahe with ref to franid bez different center will have different testimonials
                string cacheKey = "SirSpeedyMain-AllPartnersDataFromdb";
                bool dataExistInCache = ApplicationCache.IsExist(cacheKey);

                if (!dataExistInCache)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                    {
                        string procName = "Get_All_Center_Partners_Data";

                        using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                        {
                            sqlCommand.CommandText = procName;
                            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                            //todo: change the proc defination to accept the fransid as param                            
                            //sqlCommand.Parameters.Clear();
                            //sqlCommand.Parameters.Add("@CenterId", SqlDbType.NVarChar).Value = fransId;
                            sqlConnection.Open();
                            var reader = sqlCommand.ExecuteReader();

                            while (reader.Read())
                            {
                                Partners t = new Partners()
                                {
                                    Partner_Id = Convert.ToInt64(reader["Partner_Id"]),
                                    Center_Id = reader["Center_Id"] is DBNull ? null : reader["Center_Id"].ToString(),
                                    Partner_Name = reader["Partner_Name"] is DBNull ? null : reader["Partner_Name"].ToString(),
                                    Partner_Tagline = reader["Partner_Tagline"] is DBNull ? null : reader["Partner_Tagline"].ToString(),
                                    Partner_Teaser = reader["Partner_Teaser"] is DBNull ? null : reader["Partner_Teaser"].ToString(),
                                    Partner_Website = reader["Partner_Website"] is DBNull ? null : reader["Partner_Website"].ToString(),
                                    Partner_Image_Id = reader["Partner_Image_Id"] is DBNull ? null : reader["Partner_Image_Id"].ToString(),
                                    Partner_Start_Date = reader["Partner_Start_Date"] is DBNull ? null : reader["Partner_Start_Date"].ToString(),
                                    Partner_End_Date = reader["Partner_End_Date"] is DBNull ? null : reader["Partner_End_Date"].ToString()
                                };
                                partnersList.Add(t);
                            }
                        }
                    }
                    if (partnersList != null && partnersList.Any())
                        ApplicationCache.Insert(cacheKey, partnersList, ConfigHelper.GetValueLong("longCacheInterval"));
                }
                else
                {
                    var cacheData = ApplicationCache.GetValue(cacheKey);
                    if (cacheData != null)
                        partnersList = (List<Partners>)cacheData;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
            return partnersList;
        }
        
        private static List<FransData> GetAllCenterData(bool refreshCache = false)
        {
            List<FransData> centerList = new List<FransData>();
            try
            {
                string cacheKey = "SirSpeedyMain-AllCenterDataFromdb";
                bool dataExistInCache = ApplicationCache.IsExist(cacheKey);

                if (!dataExistInCache || refreshCache)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                    {
                        string procName = "Get_All_Center_Data";

                        using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                        {
                            sqlCommand.CommandText = procName;
                            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                            sqlConnection.Open();
                            var reader = sqlCommand.ExecuteReader();

                            while (reader.Read())
                            {
                                FransData d = new FransData()
                                {
                                    FransId = reader["Center_Id"] is DBNull ? null : reader["Center_Id"].ToString(),
                                    CenterName = reader["Center_Name"] is DBNull ? null : reader["Center_Name"].ToString(),
                                    PhoneNumber = reader["Center_Phone"] is DBNull ? null : reader["Center_Phone"].ToString(),
                                    FaxNumber = reader["Center_Fax"] is DBNull ? null : reader["Center_Fax"].ToString(),
                                    Email = reader["Center_Email"] is DBNull ? null : reader["Center_Email"].ToString(),
                                    ContactFirstName = reader["Center_Contact_First_Name"] is DBNull ? null : reader["Center_Contact_First_Name"].ToString(),
                                    ContactLastName = reader["Center_Contact_Last_Name"] is DBNull ? null : reader["Center_Contact_Last_Name"].ToString(),
                                    SendAFileEmail = reader["Center_SendFile_Email"] is DBNull ? null : reader["Center_SendFile_Email"].ToString(),
                                    RequestAQuoteEmail = reader["Center_Request_Quote_Email"] is DBNull ? null : reader["Center_Request_Quote_Email"].ToString(),
                                    Address1 = reader["Center_Address1"] is DBNull ? null : reader["Center_Address1"].ToString(),
                                    Address2 = reader["Center_Address2"] is DBNull ? null : reader["Center_Address2"].ToString(),
                                    City = reader["Center_City"] is DBNull ? null : reader["Center_City"].ToString(),
                                    State = reader["Center_State"] is DBNull ? null : reader["Center_State"].ToString(),
                                    StateFullName = reader["Center_State_Full_Name"] is DBNull ? null : reader["Center_State_Full_Name"].ToString(),
                                    Zipcode = reader["Center_ZipCode"] is DBNull ? null : reader["Center_ZipCode"].ToString(),
                                    Country = reader["Center_Country"] is DBNull ? null : reader["Center_Country"].ToString(),
                                    HoursOfOperation = reader["Center_Working_Hours"] is DBNull ? null : reader["Center_Working_Hours"].ToString(),
                                    Latitude = reader["Center_Latitude"] is DBNull ? null : reader["Center_Latitude"].ToString(),
                                    Longitude = reader["Center_Longitude"] is DBNull ? null : reader["Center_Longitude"].ToString(),
                                    ContinentCode = reader["Country_Continental_Code"] is DBNull ? null : reader["Country_Continental_Code"].ToString(),
                                    CmsCommunityGroupId = reader["Center_Cms_GroupId"] is DBNull ? 0 : long.Parse(reader["Center_Cms_GroupId"].ToString()),
                                    FranservId = reader["Center_Franserv_Id"] is DBNull ? null : reader["Center_Franserv_Id"].ToString(),
                                    WhitePaperDownloadEmail = reader["Center_WhitePaper_Download_Email"] is DBNull ? null : reader["Center_WhitePaper_Download_Email"].ToString(),
                                    JobApplicationEmail = reader["Center_Job_Application_Email"] is DBNull ? null : reader["Center_Job_Application_Email"].ToString(),
                                    SubscriptionEmail = reader["Center_Subscription_Email"] is DBNull ? null : reader["Center_Subscription_Email"].ToString()
                                };
                                centerList.Add(d);
                            }
                        }
                    }
                    if (centerList != null && centerList.Any())
                        ApplicationCache.Insert(cacheKey, centerList, ConfigHelper.GetValueLong("longCacheInterval"));
                }
                else
                {
                    var cacheData = ApplicationCache.GetValue(cacheKey);
                    if (cacheData != null)
                        centerList = (List<FransData>)cacheData;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
            return centerList;
        }
               
        
        /// <summary>
        /// This method is used to exract content ids from string of ids
        /// </summary>
        /// <param name="idsText">database entry of string ids</param>
        /// <returns>generic list of long</returns>
        private static List<long> GetIdsFromString(string idsText)
        {
            List<long> resultIds = null;
            if(!string.IsNullOrEmpty(idsText))
            {
                try
                {
                    string[] ids = idsText.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (ids != null && ids.Length > 0)
                    {
                        resultIds = new List<long>();
                        foreach (var id in ids)
                        {
                            long cId;
                            long.TryParse(id, out cId);
                            if (cId > 0)
                                resultIds.Add(cId);
                        }
                    }
                }
                catch(Exception ex)
                {
                    Log.WriteError(ex);
                }
            }
            return resultIds;
        }

        /// <summary>
        /// This method is used to exract content ids from string of ids
        /// </summary>
        /// <param name="idsText">database entry of string ids</param>
        /// <returns>generic list of string</returns>
        private static List<string> GetStringIdsFromString(string idsText)
        {
            List<string> resultIds = null;
            if (!string.IsNullOrEmpty(idsText))
            {
                try
                {
                    resultIds = idsText.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();                    
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }
            return resultIds;
        }
        
        /// <summary>
        /// helper method to build email data and send to center and copy user
        /// </summary>        
        private static void SendRequestToQuoteInfoToCenter(string centerId, string firstName, string lastName, string jobTitle, string companyName, string email, string phoneNumber, string projectName, string projectDescription, string projectBudget, string ektronUploadedFilesIds, string duedate, string emailOptin)
        {
            string adminEmail = string.Empty;
            string domainName = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
            string siteUrl = string.Empty;
            if (!string.IsNullOrEmpty(centerId))
            {
                var centerData = FransDataManager.GetFransData(centerId);
                if (centerData != null)
                    adminEmail = centerData.RequestAQuoteEmail;
                siteUrl = "http://" + domainName + "/" + centerId + "/";
            }
            else
            {
                var nationalData = SiteDataManager.GetNationalCompanyInfo();
                if (nationalData != null)
                {
                    adminEmail = nationalData.RequestToQuoteEmail;
                }
                siteUrl = "http://" + domainName + "/";
            }

            if (string.IsNullOrEmpty(adminEmail))
            {
                return;
            }

            StringBuilder sbEmailBody = new StringBuilder();

            sbEmailBody.Append("Hi,");
            sbEmailBody.Append("<br/>");
            sbEmailBody.Append("<p>Following data has been submitted for Requesting a Quote: </p>");
            sbEmailBody.Append("<table>");
            sbEmailBody.Append("<tr><td>FirstName: </td><td>" + firstName + "</td></tr>");
            sbEmailBody.Append("<tr><td>LastName: </td><td>" + lastName + "</td></tr>");
            sbEmailBody.Append("<tr><td>JobTitle: </td><td>" + jobTitle + "</td></tr>");
            sbEmailBody.Append("<tr><td>CompanyName: </td><td>" + companyName + "</td></tr>");
            sbEmailBody.Append("<tr><td>Email: </td><td>" + email + "</td></tr>");
            sbEmailBody.Append("<tr><td>PhoneNumber: </td><td>" + phoneNumber + "</td></tr>");
            sbEmailBody.Append("<tr><td>ProjectName: </td><td>" + projectName + "</td></tr>");

            if (!string.IsNullOrEmpty(ektronUploadedFilesIds))
            {
                sbEmailBody.Append("<tr>");
                sbEmailBody.Append("<td>Uploaded Files: </td>");
                sbEmailBody.Append("<td>" + ektronUploadedFilesIds + "</td>");
                sbEmailBody.Append("</tr>");
            }

            sbEmailBody.Append("<tr><td>ProjectDescription: </td><td>" + projectDescription + "</td></tr>");
            sbEmailBody.Append("<tr><td>Due Date: </td><td>" + duedate + "</td></tr>");
            if (!String.IsNullOrWhiteSpace(emailOptin))
                sbEmailBody.AppendFormat("Email Opt In: {0}<br/>", emailOptin);
            sbEmailBody.Append("</table>");

            string REQfromEmailAddress = ConfigHelper.GetValueString("RAQfromEmailAddress");

            //start a new thread to decrease the wait time
            Thread emailThread = new Thread(delegate()
            {                
                Utility.SendEmail(adminEmail, REQfromEmailAddress, "", sbEmailBody.ToString(), "Request a Quote", firstName + " " + lastName);
            });
            emailThread.IsBackground = true;
            emailThread.Start();
        }

        /// <summary>
        /// helper method to build email data and send to center and copy user
        /// </summary>        
        private static void SendSendAFileInfoToCenter(string centerId, string firstName, string lastName, string jobTitle, string companyName, string email, string phoneNumber, string projectName, string projectDescription, string projectQuantity, string ektronUploadedFilesIds, string uploadExternalLinks, string projectDueDate)
        {
            string adminEmail = string.Empty;
            string sendAFileEmail = string.Empty;
            string domainName = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
            string siteUrl = string.Empty;
            if (!string.IsNullOrEmpty(centerId))
            {
                var centerData = FransDataManager.GetFransData(centerId);
                if (centerData != null)
                {
                    adminEmail = centerData.Email;
                    sendAFileEmail = centerData.SendAFileEmail;
                    siteUrl = "http://" + domainName + "/" + centerId + "/";
                }
            }
            else
            {
                var nationalData = SiteDataManager.GetNationalCompanyInfo();
                if (nationalData != null)
                {
                    adminEmail = nationalData.GeneralEmail;
                    sendAFileEmail = nationalData.SendAFileEmail;
                }
                siteUrl = "http://" + domainName + "/";
            }

            if (string.IsNullOrEmpty(adminEmail))
            {
                return;
            }

            StringBuilder sbEmailBody = new StringBuilder();
            sbEmailBody.Append("Hi,");
            sbEmailBody.Append("<br/>");
            sbEmailBody.Append("<p>Following data has been submitted for Send A File: </p>");
            sbEmailBody.Append("<table>");
            sbEmailBody.Append("<tr><td>FirstName: </td><td>" + firstName + "</td></tr>");
            sbEmailBody.Append("<tr><td>LastName: </td><td>" + lastName + "</td></tr>");
            sbEmailBody.Append("<tr><td>JobTitle: </td><td>" + jobTitle + "</td></tr>");
            sbEmailBody.Append("<tr><td>CompanyName: </td><td>" + companyName + "</td></tr>");
            sbEmailBody.Append("<tr><td>Email: </td><td>" + email + "</td></tr>");
            sbEmailBody.Append("<tr><td>PhoneNumber: </td><td>" + phoneNumber + "</td></tr>");
            sbEmailBody.Append("<tr><td>ProjectName: </td><td>" + projectName + "</td></tr>");
            sbEmailBody.Append("<tr><td>Project Due Date: </td><td>" + projectDueDate + "</td></tr>");

            if (!string.IsNullOrEmpty(ektronUploadedFilesIds))
            {
                sbEmailBody.Append("<tr>");
                sbEmailBody.Append("<td>Uploaded Files: </td>");
                sbEmailBody.Append("<td>" + ektronUploadedFilesIds + "</td>");
                sbEmailBody.Append("</tr>");
            }

            if (!string.IsNullOrEmpty(uploadExternalLinks))
            {
                sbEmailBody.Append("<tr>");
                sbEmailBody.Append("<td>Uploaded Files through External App: </td>");
                sbEmailBody.Append("<td>");
                var links = uploadExternalLinks.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var link in links)
                {
                    sbEmailBody.Append("<a href=\"" + link + "\">" + link + "</a><br/>");
                }
                sbEmailBody.Append("</td>");
                sbEmailBody.Append("</tr>");
            }

            sbEmailBody.Append("<tr><td>ProjectDescription: </td><td>" + projectDescription + "</td></tr>");
            sbEmailBody.Append("<tr><td>ProjectQuantity: </td><td>" + projectQuantity + "</td></tr>");
            sbEmailBody.Append("</table>");

            string SAFfromEmailAddress = ConfigHelper.GetValueString("SAFfromEmailAddress");

            //start a new thread to decrease the wait time
            Thread emailThread = new Thread(delegate()
            {                
                Utility.SendEmail(sendAFileEmail, SAFfromEmailAddress, "", sbEmailBody.ToString(), "New Send A File Submission", firstName + " " + lastName, adminEmail);
            });
            emailThread.IsBackground = true;
            emailThread.Start();
        }


        /// <summary>
        /// helper method to build email user registration data and send to center and copy user
        /// </summary>        
        private static void SendRegisterToCenter(string centerId, string firstName, string lastName, string jobTitle, string companyName, string email, string phoneNumber, string website)
        {
            if (string.IsNullOrEmpty(centerId))
            {
                return;
            }

            var centerData = GetFransData(centerId);
            if (centerData != null && !string.IsNullOrEmpty(centerData.Email))
            {
                var centerEmail = centerData.Email;
                string siteUrl = "http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
                StringBuilder sbEmailBody = new StringBuilder();

                sbEmailBody.Append("Hi,");
                sbEmailBody.Append("<br/>");
                sbEmailBody.Append("<p>Following data has been submitted for User Registration: </p>");
                sbEmailBody.Append("<table>");
                sbEmailBody.Append("<tr><td>FirstName: </td><td>" + firstName + "</td></tr>");
                sbEmailBody.Append("<tr><td>LastName: </td><td>" + lastName + "</td></tr>");
                sbEmailBody.Append("<tr><td>JobTitle: </td><td>" + jobTitle + "</td></tr>");
                sbEmailBody.Append("<tr><td>CompanyName: </td><td>" + companyName + "</td></tr>");
                sbEmailBody.Append("<tr><td>Email: </td><td>" + email + "</td></tr>");
                sbEmailBody.Append("<tr><td>PhoneNumber: </td><td>" + phoneNumber + "</td></tr>");
                sbEmailBody.Append("<tr><td>Website: </td><td>" + website + "</td></tr>");
                sbEmailBody.Append("</table>");

                Utility.SendEmail(centerEmail, email, "", sbEmailBody.ToString(), "A New Public User Registration To Your Center", firstName + " " + lastName);
            }
        }

        /// <summary>
        /// helper method to build email for job application submitted
        /// </summary>        
        private static void SendEmailApplicationJob(string centerId, string firstName, string lastName, string city, string stateName, string zipCode, string email, string coverNotes, string jobApplicationUrl, string resumeUrl)
        {
            if (string.IsNullOrEmpty(centerId))
            {
                return;
            }

            var centerData = GetFransData(centerId);
            if (centerData != null && !string.IsNullOrEmpty(centerData.JobApplicationEmail))
            {
                var centerEmail = centerData.JobApplicationEmail;
                StringBuilder sbEmailBody = new StringBuilder();

                sbEmailBody.Append("Hi,");
                sbEmailBody.Append("<br/>");
                sbEmailBody.Append("<p>Following data has been submitted for Job Application: </p>");
                sbEmailBody.Append("<table>");
                sbEmailBody.Append("<tr><td>Job Application URL: </td><td>" + jobApplicationUrl + "</td></tr>");
                sbEmailBody.Append("<tr><td>FirstName: </td><td>" + firstName + "</td></tr>");
                sbEmailBody.Append("<tr><td>LastName: </td><td>" + lastName + "</td></tr>");
                sbEmailBody.Append("<tr><td>City: </td><td>" + city + "</td></tr>");
                sbEmailBody.Append("<tr><td>State: </td><td>" + stateName + "</td></tr>");
                sbEmailBody.Append("<tr><td>ZipCode: </td><td>" + zipCode + "</td></tr>");
                sbEmailBody.Append("<tr><td>Email: </td><td>" + email + "</td></tr>");
                sbEmailBody.Append("<tr><td>Cover Notes: </td><td>" + coverNotes + "</td></tr>");
                sbEmailBody.Append("<tr><td>Resume Copy: </td><td>" + resumeUrl + "</td></tr>");
                sbEmailBody.Append("</table>");

                //start a new thread to decrease the wait time
                Thread emailThread = new Thread(delegate()
                {
                    Utility.SendEmail(centerEmail, email, "", sbEmailBody.ToString(), "Job Application", firstName + " " + lastName);
                });
                emailThread.IsBackground = true;
                emailThread.Start();

                //send email to applicant
                SendJobThankYouMessage(firstName, lastName, email, jobApplicationUrl);
            }
        }
  
        private static void SendJobThankYouMessage(string firstName, string lastName, string email, string jobApplicationUrl)
        {
            StringBuilder sbEmailBody = new StringBuilder();
            long contentId = ConfigHelper.GetValueLong("ApplyForJobThanksContentId");
            var contentData = ContentHelper.GetContentById(contentId);

            if (contentData != null)
            {
                sbEmailBody.Append("Hi " + firstName + " " + lastName + ",");
                sbEmailBody.Append("<br/>");
                sbEmailBody.Append(contentData.Html);
                sbEmailBody.Append("<br/>");
                sbEmailBody.Append("Thank You<br/> SirSpeedy.com");

                Utility.SendEmail(email, "NoReply@SirSpeedy.com", "", sbEmailBody.ToString(), "Thank You");
            }
        }

        private static void SendContactUsInfoToCenter(string centerId, string firstName, string lastName, string userComments, string userEmail)
        {
            if (string.IsNullOrEmpty(centerId))
                return;

            var centerData = GetFransData(centerId);
            if (centerData != null && !string.IsNullOrEmpty(centerData.Email))
            {
                var centerEmail = centerData.Email;
                StringBuilder sbEmailBody = new StringBuilder();

                sbEmailBody.Append("Hi,");
                sbEmailBody.Append("<br/>");
                sbEmailBody.Append("<p>Following data has been submitted through Contact Us form: </p>");
                sbEmailBody.Append("<table>");
                sbEmailBody.Append("<tr><td>FirstName: </td><td>" + firstName + "</td></tr>");
                sbEmailBody.Append("<tr><td>LastName: </td><td>" + lastName + "</td></tr>");
                sbEmailBody.Append("<tr><td>Comments: </td><td>" + userComments + "</td></tr>");                
                sbEmailBody.Append("<tr><td>Email: </td><td>" + userEmail + "</td></tr>");               
                sbEmailBody.Append("</table>");

                Utility.SendEmail(centerEmail, userEmail, "", sbEmailBody.ToString(), "Contact Us Request", firstName + " " + lastName);
            }
        }

        private static void SendUserSubscribeDataInfoToCenter(string centerId, string printType, string onlineType, string firstName, string lastName, string email, string address, string city, string state, string zipcode)
        {
            if (string.IsNullOrEmpty(centerId))
                return;

            var centerData = GetFransData(centerId);
            if (centerData != null && !string.IsNullOrEmpty(centerData.SubscriptionEmail))
            {
                var centerEmail = centerData.SubscriptionEmail;
                StringBuilder sbEmailBody = new StringBuilder();

                sbEmailBody.Append("Hi,");
                sbEmailBody.Append("<br/>");
                sbEmailBody.Append("<p>Following data has been submitted through Subscription form: </p>");
                sbEmailBody.Append("<table>");
                sbEmailBody.Append("<tr><td>FirstName: </td><td>" + firstName + "</td></tr>");
                sbEmailBody.Append("<tr><td>LastName: </td><td>" + lastName + "</td></tr>");                
                sbEmailBody.Append("<tr><td>Email: </td><td>" + email + "</td></tr>");

                sbEmailBody.Append("<tr><td>Online Option: </td><td>" + onlineType + "</td></tr>");
                sbEmailBody.Append("<tr><td>Print Option: </td><td>" + printType + "</td></tr>");
                sbEmailBody.Append("<tr><td>Address: </td><td>" + address + "</td></tr>");
                sbEmailBody.Append("<tr><td>City: </td><td>" + city + "</td></tr>");
                sbEmailBody.Append("<tr><td>State: </td><td>" + state + "</td></tr>");
                sbEmailBody.Append("<tr><td>ZipCode: </td><td>" + zipcode + "</td></tr>");
                sbEmailBody.Append("</table>");

                Utility.SendEmail(centerEmail, email, "", sbEmailBody.ToString(), "Subscribe Request", firstName + " " + lastName);
            }
        }

        private static string GetImagePath(string imgLibraryId)
        {
            string imagePath = "";
            long id;
            long.TryParse(imgLibraryId, out id);
            if(id > 0)
            {
                //todo: work on getting path
            }
            return imagePath;
        }

        private static int getMaxSendAFileId()
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["SirSpeedyAdminTool.DbConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionstring);
            string sqlCount = "select MAX(t0.Send_File_Id) as maxtoolid from Center_Send_A_File t0 ";
            SqlCommand sqlcmd = new SqlCommand();
            SqlDataReader reader = null;
            sqlcmd = new SqlCommand(sqlCount, connection);
            int newID = 0;
            try
            {
                connection.Open();
                reader = sqlcmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();

                    if (reader["maxtoolid"] != null)
                    {
                        newID = (Convert.ToInt32(reader["maxtoolid"]));
                    }

                    return newID;
                }
            }
            catch { }
            finally
            {
                sqlcmd.Dispose();
                connection.Close();

            }
            return newID;
        }


        /// <summary>
        /// As per initial requirement this content was part of Admin tool and then requirement changed
        /// </summary>
        /// <returns></returns>
        private static List<long> GetLocalCentersNewsIds()
        {
            long localNewsTaxId = ConfigHelper.GetValueLong("AllNewsLocalTaxId");
            long newsSFId = ConfigHelper.GetValueLong("NewsSmartFormID");
            string cacheKey = "GetLocalCentersNewsIds" + localNewsTaxId + newsSFId;
            List<long> ids = HttpContext.Current.Cache[cacheKey] as List<long>;
            if (ids == null || ids.Count == 0)
            {                
                var localNewsCategory = TaxonomyHelper.GetItem(localNewsTaxId);
                if (localNewsCategory != null && localNewsCategory.Id > 0)
                {
                    ContentTaxonomyCriteria criteria = new ContentTaxonomyCriteria(ContentProperty.DateModified, EkEnumeration.OrderByDirection.Descending);
                    criteria.AddFilter(localNewsCategory.Id);
                    criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, newsSFId);
                    criteria.PagingInfo = new Ektron.Cms.PagingInfo(GetCustomApiPageSize(), 1);
                    var newsCList = ContentHelper.GetListByCriteria(criteria);

                    if (newsCList != null && newsCList.Any())
                    {
                        ids = new List<long>();
                        foreach (var c in newsCList)
                            ids.Add(c.Id);
                    }
                }

                if (ids != null && ids.Count > 0)
                    HttpContext.Current.Cache.Insert(cacheKey, ids, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
            }
            return ids;
        }

        /// <summary>
        /// As per initial requirement this content was part of Admin tool and then requirement changed
        /// </summary>
        /// <returns></returns>
        private static List<long> GetLocalCentersWhitePapersIds()
        {            
            long localbriefsWhitePaperTaxId = ConfigHelper.GetValueLong("BriefAndWhitepapersLocalTaxId");
            long briefWhitepapersSFId = ConfigHelper.GetValueLong("BriefsAndWhitepapersSmartFormID");

            string cacheKey = "GetLocalCentersWhitePapersIds" + localbriefsWhitePaperTaxId + briefWhitepapersSFId;
            List<long> ids = HttpContext.Current.Cache[cacheKey] as List<long>;
            if (ids == null || ids.Count == 0)
            {
                var localbwpCategory = TaxonomyHelper.GetItem(localbriefsWhitePaperTaxId);
                if (localbwpCategory != null && localbwpCategory.Id > 0)
                {
                    ContentTaxonomyCriteria criteria = new ContentTaxonomyCriteria(ContentProperty.DateModified, EkEnumeration.OrderByDirection.Descending);
                    criteria.AddFilter(localbwpCategory.Id);
                    criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, briefWhitepapersSFId);
                    criteria.PagingInfo = new Ektron.Cms.PagingInfo(GetCustomApiPageSize(), 1);
                    var bwpCList = ContentHelper.GetListByCriteria(criteria);
                    if (bwpCList != null && bwpCList.Any())
                    {
                        ids = new List<long>();
                        foreach (var c in bwpCList)
                            ids.Add(c.Id);
                    }
                }

                if (ids != null && ids.Count > 0)
                    HttpContext.Current.Cache.Insert(cacheKey, ids, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
            }
            return ids;
        }

        /// <summary>
        /// As per initial requirement this content was part of Admin tool and then requirement changed
        /// </summary>
        /// <returns></returns>
        private static List<long> GetLocalCentersInTheMediaIds()
        {
            long allInMediaTaxId = ConfigHelper.GetValueLong("InMediaLocalTaxId");
            long allInMediaSFId = ConfigHelper.GetValueLong("InMediaSmartFormID");
            string cacheKey = "GetLocalCentersInTheMediaIds" + allInMediaTaxId + allInMediaSFId;
            List<long> ids = HttpContext.Current.Cache[cacheKey] as List<long>;
            if (ids == null || ids.Count == 0)
            {
                var localAllInMediaCategory = TaxonomyHelper.GetItem(allInMediaTaxId);
                if (localAllInMediaCategory != null && localAllInMediaCategory.Id > 0)
                {
                    ContentTaxonomyCriteria criteria = new ContentTaxonomyCriteria(ContentProperty.DateModified, EkEnumeration.OrderByDirection.Descending);
                    criteria.AddFilter(localAllInMediaCategory.Id);
                    criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, allInMediaSFId);
                    criteria.PagingInfo = new Ektron.Cms.PagingInfo(GetCustomApiPageSize(), 1);
                    var allInMediaCList = ContentHelper.GetListByCriteria(criteria);

                    if (allInMediaCList != null && allInMediaCList.Any())
                    {
                        ids = new List<long>();
                        foreach (var c in allInMediaCList)
                            ids.Add(c.Id);
                    }
                }

                if (ids != null && ids.Count > 0)
                    HttpContext.Current.Cache.Insert(cacheKey, ids, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
            } 
            return ids;
        }

        /// <summary>
        /// As per initial requirement this content was part of Admin tool and then requirement changed
        /// </summary>
        /// <returns></returns>
        private static List<long> GetLocalCentersCaseStudiesIds()
        {
            long localcaseStudiesId = ConfigHelper.GetValueLong("CaseStudiesLocalTaxId");
            long caseStudiesSFId = ConfigHelper.GetValueLong("CaseStudiesSmartFormID");

            string cacheKey = "GetLocalCentersCaseStudiesIds" + localcaseStudiesId + caseStudiesSFId;
            List<long> ids = HttpContext.Current.Cache[cacheKey] as List<long>;
            if (ids == null || ids.Count == 0)
            {
                var localCaseStudiesCategory = TaxonomyHelper.GetItem(localcaseStudiesId);
                if (localCaseStudiesCategory != null && localCaseStudiesCategory.Id > 0)
                {
                    ContentTaxonomyCriteria criteria = new ContentTaxonomyCriteria(ContentProperty.DateModified, EkEnumeration.OrderByDirection.Descending);
                    criteria.AddFilter(localCaseStudiesCategory.Id);
                    criteria.PagingInfo = new Ektron.Cms.PagingInfo(GetCustomApiPageSize(), 1);
                    criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, caseStudiesSFId);
                    var caseStudiesCList = ContentHelper.GetListByCriteria(criteria);
                    if (caseStudiesCList != null && caseStudiesCList.Any())
                    {
                        ids = new List<long>();
                        foreach (var c in caseStudiesCList)
                            ids.Add(c.Id);
                    }
                }

                if (ids != null && ids.Count > 0)
                    HttpContext.Current.Cache.Insert(cacheKey, ids, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("web.config")), DateTime.Now.AddSeconds(ConfigHelper.GetValueLong("longCacheInterval")), System.Web.Caching.Cache.NoSlidingExpiration);
            } 
            return ids;
        }
               
        /// <summary>
        /// This method is used to check if local franchise is selected
        /// </summary>
        /// <returns></returns>
        private static bool IsFransSessionActive()
        {
            bool status = false;
            try
            {
                string url = HttpContext.Current.Request.RawUrl;
                var pathNames = url.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                if (pathNames != null && pathNames.Length > 0)
                {
                    var reqCenterId = pathNames[0].ToLower();
                    if ((!string.IsNullOrEmpty(reqCenterId)) && (!reqCenterId.ToLower().Contains(".aspx")))
                    {
                        var allCentersList = FransDataManager.GetAllFransLocationDataList();
                        var reqCenterData = allCentersList.Where(x => x.FransId.ToLower() == reqCenterId).FirstOrDefault();
                        if (reqCenterData != null && reqCenterData.FransId != string.Empty)
                        {
                             return true;
                            //status = ApplicationCache.IsExist(LocalFransCacheKey + reqCenterData.FransId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
                Log.WriteError(ex.StackTrace);
            }
            return status;
        }       

        #endregion

    }
}