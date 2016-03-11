using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using Ektron.Cms;
using Ektron.Cms.Instrumentation;
using System.Text;

namespace SignalGraphics.CMS
{

    /// <summary>
    /// Summary description for AdminToolManager
    /// </summary>
    public class AdminToolManager
    {
        private static string adminToolConnectionString = ConfigurationManager.ConnectionStrings["SignalGraphicsAdminTool.DbConnection"].ConnectionString;

        public AdminToolManager()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// this method id used to get all available countries
        /// admin tool methods don't require caching but this data is also used on Find location page 
        /// </summary>
        /// <returns></returns>
        public static List<CountryData> GetAllCountryList()
        {
            List<CountryData> countryList = null;
            try
            {
                string cacheKey = "SignalGraphicsMain-AllCountryDataFromdb";
                bool dataExistInCache = ApplicationCache.IsExist(cacheKey);

                if (!dataExistInCache)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                    {
                        string procName = "Get_Country_List";

                        using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                        {
                            sqlCommand.CommandText = procName;
                            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                            sqlConnection.Open();
                            var reader = sqlCommand.ExecuteReader();
                            countryList = new List<CountryData>();

                            while (reader.Read())
                            {
                                var t = new CountryData()
                                {
                                    CountryCode = reader["Country_Code"] is DBNull ? null : reader["Country_Code"].ToString(),
                                    CountryName = reader["Country_Name"] is DBNull ? null : reader["Country_Name"].ToString()
                                };
                                countryList.Add(t);
                            }
                        }
                    }
                    if (countryList != null && countryList.Any())
                        ApplicationCache.Insert(cacheKey, countryList, ConfigHelper.GetValueLong("longCacheInterval"));
                }
                else
                {
                    var cacheData = ApplicationCache.GetValue(cacheKey);
                    if (cacheData != null)
                        countryList = (List<CountryData>)cacheData;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }

            return countryList;
        }

        public static bool IsCenterExist(string centerId)
        {
            bool status = true;
            try
            {
                int count = 0;
                string adminToolConnectionString = ConfigurationManager.ConnectionStrings["SignalGraphicsAdminTool.DbConnection"].ConnectionString;
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string insertQuery = "select count(*) from centerdata where center_id = '" + centerId + "'";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = insertQuery;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        var reader = sqlCommand.ExecuteReader();

                        while (reader.Read())
                        {
                            count = reader[0] is DBNull ? 0 : int.Parse(reader[0].ToString());
                        }
                    }
                }
                if (count > 0)
                    status = true;
                else
                    status = false;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
                status = false;
            }
            return status;
        }

        public static bool AddCenter(string Center_Id, string Center_Name, string Center_Address1, string Center_Address2, string Center_City, string Center_State, string Center_ZipCode, string Center_Country, string Center_Phone, string Center_Fax, string Center_Email, string Center_SendFile_Email, string Center_Request_Quote_Email, string Center_Working_Hours, DateTime Center_created_date, string Center_Latitude, string Center_Longitude, string Center_State_Full_Name, string Center_Contact_First_Name, string Center_Contact_Last_Name, long cmsGroupId, string franservId, string whitePaperDownloadEmail, string jobApplicationEmail, string subscriptionEmail)
        {
            bool status = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string procName = "Add_Center";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = procName;
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.Parameters.Clear();
                        sqlCommand.Parameters.Add("@Center_Id", SqlDbType.NVarChar).Value = Center_Id;
                        sqlCommand.Parameters.Add("@Center_Name", SqlDbType.NVarChar).Value = Center_Name;
                        sqlCommand.Parameters.Add("@Center_Address1", SqlDbType.NVarChar).Value = Center_Address1;
                        sqlCommand.Parameters.Add("@Center_Address2", SqlDbType.NVarChar).Value = Center_Address2;
                        sqlCommand.Parameters.Add("@Center_City", SqlDbType.NVarChar).Value = Center_City;
                        sqlCommand.Parameters.Add("@Center_State", SqlDbType.NVarChar).Value = Center_State;
                        sqlCommand.Parameters.Add("@Center_ZipCode", SqlDbType.NVarChar).Value = Center_ZipCode;
                        sqlCommand.Parameters.Add("@Center_Country", SqlDbType.NVarChar).Value = Center_Country;
                        sqlCommand.Parameters.Add("@Center_Phone", SqlDbType.NVarChar).Value = Center_Phone;
                        sqlCommand.Parameters.Add("@Center_Fax", SqlDbType.NVarChar).Value = Center_Fax;
                        sqlCommand.Parameters.Add("@Center_Email", SqlDbType.NVarChar).Value = Center_Email;
                        sqlCommand.Parameters.Add("@Center_SendFile_Email", SqlDbType.NVarChar).Value = Center_SendFile_Email;
                        sqlCommand.Parameters.Add("@Center_Request_Quote_Email", SqlDbType.NVarChar).Value = Center_Request_Quote_Email;
                        sqlCommand.Parameters.Add("@Center_Working_Hours", SqlDbType.NVarChar).Value = Center_Working_Hours;
                        sqlCommand.Parameters.Add("@Center_created_date", SqlDbType.DateTime).Value = Center_created_date;
                        sqlCommand.Parameters.Add("@Center_Latitude", SqlDbType.NVarChar).Value = Center_Latitude;
                        sqlCommand.Parameters.Add("@Center_Longitude", SqlDbType.NVarChar).Value = Center_Longitude;
                        sqlCommand.Parameters.Add("@Center_State_Full_Name", SqlDbType.NVarChar).Value = Center_State_Full_Name;
                        sqlCommand.Parameters.Add("@Center_Contact_First_Name", SqlDbType.NVarChar).Value = Center_Contact_First_Name;
                        sqlCommand.Parameters.Add("@Center_Contact_Last_Name", SqlDbType.NVarChar).Value = Center_Contact_Last_Name;
                        sqlCommand.Parameters.Add("@Center_Cms_GroupId", SqlDbType.BigInt).Value = cmsGroupId;
                        sqlCommand.Parameters.Add("@Center_Franserv_Id", SqlDbType.NVarChar).Value = franservId;
                        sqlCommand.Parameters.Add("@Center_WhitePaper_Download_Email", SqlDbType.NVarChar).Value = whitePaperDownloadEmail;
                        sqlCommand.Parameters.Add("@Center_Job_Application_Email", SqlDbType.NVarChar).Value = jobApplicationEmail;
                        sqlCommand.Parameters.Add("@Center_Subscription_Email", SqlDbType.NVarChar).Value = subscriptionEmail;
                        sqlConnection.Open();
                        var reader = sqlCommand.ExecuteNonQuery();
                    }
                }
                status = true;                
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
                status = false;
            }
            return status;
        }

        public static bool DisableCenter(string CenterId)
        {
            bool status = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string query = "Delete from centerdata where Center_Id = '" + CenterId + "'";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        var returnData = sqlCommand.ExecuteNonQuery();
                        if (returnData != null)
                            status = returnData.ToString() == "1" ? true : false;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);               
            }
            return status;
        }

        public static bool UpdateCenter(string Center_Id, string Center_Name, string Center_Address1, string Center_Address2, string Center_City, string Center_State, string Center_ZipCode, string Center_Country, string Center_Phone, string Center_Fax, string Center_Email, string Center_SendFile_Email, string Center_Request_Quote_Email, string Center_Working_Hours, DateTime Center_created_date, string Center_Latitude, string Center_Longitude, string Center_State_Full_Name, string Center_Contact_First_Name, string Center_Contact_Last_Name, long cmsGroupId, int isCenterActive, string whitePaperDownloadEmail, string jobApplicationEmail, string subscriptionEmail)
        {
            bool status = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string procName = "Update_Center";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = procName;
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.Parameters.Clear();
                        sqlCommand.Parameters.Add("@Center_Id", SqlDbType.NVarChar).Value = Center_Id;
                        sqlCommand.Parameters.Add("@Center_Name", SqlDbType.NVarChar).Value = Center_Name;
                        sqlCommand.Parameters.Add("@Center_Address1", SqlDbType.NVarChar).Value = Center_Address1;
                        sqlCommand.Parameters.Add("@Center_Address2", SqlDbType.NVarChar).Value = Center_Address2;
                        sqlCommand.Parameters.Add("@Center_City", SqlDbType.NVarChar).Value = Center_City;
                        sqlCommand.Parameters.Add("@Center_State", SqlDbType.NVarChar).Value = Center_State;
                        sqlCommand.Parameters.Add("@Center_ZipCode", SqlDbType.NVarChar).Value = Center_ZipCode;
                        sqlCommand.Parameters.Add("@Center_Country", SqlDbType.NVarChar).Value = Center_Country;
                        sqlCommand.Parameters.Add("@Center_Phone", SqlDbType.NVarChar).Value = Center_Phone;
                        sqlCommand.Parameters.Add("@Center_Fax", SqlDbType.NVarChar).Value = Center_Fax;
                        sqlCommand.Parameters.Add("@Center_Email", SqlDbType.NVarChar).Value = Center_Email;
                        sqlCommand.Parameters.Add("@Center_SendFile_Email", SqlDbType.NVarChar).Value = Center_SendFile_Email;
                        sqlCommand.Parameters.Add("@Center_Request_Quote_Email", SqlDbType.NVarChar).Value = Center_Request_Quote_Email;
                        sqlCommand.Parameters.Add("@Center_Working_Hours", SqlDbType.NVarChar).Value = Center_Working_Hours;
                        sqlCommand.Parameters.Add("@Center_deleted", SqlDbType.Int).Value = isCenterActive;
                        sqlCommand.Parameters.Add("@Center_created_date", SqlDbType.DateTime).Value = Center_created_date;
                        sqlCommand.Parameters.Add("@Center_Latitude", SqlDbType.NVarChar).Value = Center_Latitude;
                        sqlCommand.Parameters.Add("@Center_Longitude", SqlDbType.NVarChar).Value = Center_Longitude;
                        sqlCommand.Parameters.Add("@Center_State_Full_Name", SqlDbType.NVarChar).Value = Center_State_Full_Name;
                        sqlCommand.Parameters.Add("@Center_Contact_First_Name", SqlDbType.NVarChar).Value = Center_Contact_First_Name;
                        sqlCommand.Parameters.Add("@Center_Contact_Last_Name", SqlDbType.NVarChar).Value = Center_Contact_Last_Name;
                        sqlCommand.Parameters.Add("@Center_Cms_GroupId", SqlDbType.BigInt).Value = cmsGroupId;
                        sqlCommand.Parameters.Add("@Center_WhitePaper_Download_Email", SqlDbType.NVarChar).Value = whitePaperDownloadEmail;
                        sqlCommand.Parameters.Add("@Center_Job_Application_Email", SqlDbType.NVarChar).Value = jobApplicationEmail;
                        sqlCommand.Parameters.Add("@Center_Subscription_Email", SqlDbType.NVarChar).Value = subscriptionEmail;
                        sqlConnection.Open();
                        var reader = sqlCommand.ExecuteNonQuery();
                    }
                }
                status = true;                
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
                status = false;
            }
            return status;
        }

        public static int UpdateNewsIds(string centerId, string newsIds)
        {
            int status = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string query = "update Center_Workarea_Data set Center_News_Ids = '" + newsIds + "' where Center_Id = '" + centerId + "'";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        status = sqlCommand.ExecuteNonQuery();
                    }
                }
				
				if (status <= 0)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                    {
                        //first record
                        string insertQuery = "insert into Center_Workarea_Data values ('" + centerId + "', '" + newsIds + "','0','0','0', '0', '0', '" + null + "','0','0')";
                        using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                        {
                            sqlCommand.CommandText = insertQuery;
                            sqlCommand.CommandType = System.Data.CommandType.Text;
                            sqlConnection.Open();
                            status = sqlCommand.ExecuteNonQuery();
                        }
                    }
                }				
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
                status = -1;
            }
            return status;
        }

        public static int UpdateProductAndServices(string centerId, string productServicesXml)
        {
            int status = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string query = "update Center_Workarea_Data set Center_Product_Services = '" + productServicesXml + "' where Center_Id = '" + centerId + "'";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        status = sqlCommand.ExecuteNonQuery();
                    }
                }

                if (status <= 0)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                    {
                        //first record
                        string insertQuery = "insert into Center_Workarea_Data values ('" + centerId + "', '0','0','0','0', '0', '0','" + productServicesXml + "','0','0')";
                        using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                        {
                            sqlCommand.CommandText = insertQuery;
                            sqlCommand.CommandType = System.Data.CommandType.Text;
                            sqlConnection.Open();
                            status = sqlCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
                status = -1;
            }
            return status;
        }

        public static int UpdateBannerIds(string centerId, string bannerIds)
        {
            int status = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string query = "update Center_Workarea_Data set Center_Banners_Ids = '" + bannerIds + "' where Center_Id = '" + centerId + "'";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        status = sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
                status = -1;
            }
            return status;
        }

        public static int UpdateCaseStudyIds(string centerId, string caseStudyIds)
        {
            int status = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string query = "update Center_Workarea_Data set Center_Case_Studies_Ids = '" + caseStudyIds + "' where Center_Id = '" + centerId + "'";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        status = sqlCommand.ExecuteNonQuery();
                    }
                }

                if (status <= 0)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                    {
                        //first record
                        string insertQuery = "insert into Center_Workarea_Data values ('" + centerId + "', '0','0','0','0', '" + caseStudyIds + "', '0', '" + null + "','0','0')";
                        using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                        {
                            sqlCommand.CommandText = insertQuery;
                            sqlCommand.CommandType = System.Data.CommandType.Text;
                            sqlConnection.Open();
                            status = sqlCommand.ExecuteNonQuery();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
                status = -1;
            }
            return status;
        }
        
        public static int UpdatePartnersIds(string centerId, string partnersIds)
        {
            int status = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string query = "update Center_Workarea_Data set Center_Partners_Ids = '" + partnersIds + "' where Center_Id = '" + centerId + "'";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        status = sqlCommand.ExecuteNonQuery();
                    }
                }

                if (status <= 0)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                    {
                        //first record
                        string insertQuery = "insert into Center_Workarea_Data values ('" + centerId + "', '0','0','" + partnersIds + "','0', '0', '0', '" + null + "','0','0')";
                        using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                        {
                            sqlCommand.CommandText = insertQuery;
                            sqlCommand.CommandType = System.Data.CommandType.Text;
                            sqlConnection.Open();
                            status = sqlCommand.ExecuteNonQuery();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
                status = -1;
            }
            return status;
        }

        public static int UpdateAllInMediaIds(string centerId, string mediaIds)
        {
            int status = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string query = "update Center_Workarea_Data set Center_All_In_The_Media_Ids = '" + mediaIds + "' where Center_Id = '" + centerId + "'";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        status = sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
                status = -1;
            }
            return status;
        }

        public static int UpdateBriefsWhitePapersIds(string centerId, string briefsWhitePapersIds)
        {
            int status = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string query = "update Center_Workarea_Data set Center_Briefs_WhitePapers_Ids = '" + briefsWhitePapersIds + "' where Center_Id = '" + centerId + "'";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        status = sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
                status = -1;
            }
            return status;
        }    
        
        public static int UpdateThirdPartyData(string centerId, string flickrUserId, string twitterFeedUrl, string  socialMediaXml)
        {
            int status = -1;
            try
            {

                if (centerId == "" || flickrUserId == "" || twitterFeedUrl == "" || socialMediaXml == "")
                    return -1;

                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string query = "update Center_ThirdParty_Data " +
                                   " set Center_Flickr_UserName ='" + flickrUserId + "', " +
                                   " Center_Twitter_Url='" + twitterFeedUrl + "', " +
                                   " Center_Social_Media_Links='" + socialMediaXml + "' " +
                                   " where Center_Id = '" + centerId + "' ";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        status = sqlCommand.ExecuteNonQuery();
                    }
                }

                if (status <= 0)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                    {
                        //first record
                        string insertQuery = "insert into Center_ThirdParty_Data " +
                                              " values ('" + centerId + "', '" + socialMediaXml + "', '" + flickrUserId + "', '0', '" + twitterFeedUrl + "')";
                        using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                        {
                            sqlCommand.CommandText = insertQuery;
                            sqlCommand.CommandType = System.Data.CommandType.Text;
                            sqlConnection.Open();
                            status = sqlCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
                status = -1;
            }
            return status;
        }

        public static int UpdateShopIds(string centerId, string shopIds)
        {
            int status = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string query = "update Center_Workarea_Data set Center_Shop_Ids = '" + shopIds + "' where Center_Id = '" + centerId + "'";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        status = sqlCommand.ExecuteNonQuery();
                    }
                }


                if (status <= 0)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                    {
                        //first record
                        string insertQuery = "insert into Center_Workarea_Data values ('" + centerId + "', '0','0','0','0', '0', '0', '" + null + "','0','" + shopIds + "')";
                        using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                        {
                            sqlCommand.CommandText = insertQuery;
                            sqlCommand.CommandType = System.Data.CommandType.Text;
                            sqlConnection.Open();
                            status = sqlCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
                status = -1;
            }
            return status;
        }

        public static bool IsUserNameExist(string userName)
        {
            bool status = true;
            try
            {
                if (userName == "")
                    return true;

                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string query = "select count (*) from Center_Users where UserName = '" + userName.Trim() + "' "; //and IsActive = 1 ";                                
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        var count = sqlCommand.ExecuteNonQuery();

                        if (count > 0)
                            status = true;
                        else
                            status = false;
                    }
                }                
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);               
            }
            return status;
        }

        public static bool AddUserToCenter(string Center_Id, string firstName, string lastName, string workPhone, string officePhone, string fax, string mobile, string email, string gender, string imScreenName, string imServiceType, string title, string bio, string picturePath, string role, string userName, string password, string company)
        {
            bool status = false;

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string procName = "Add_Profile";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = procName;
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.Parameters.Clear();
                        sqlCommand.Parameters.Add("@Center_Id", SqlDbType.NVarChar).Value = Center_Id;
                        sqlCommand.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = userName;
                        sqlCommand.Parameters.Add("@First_Name", SqlDbType.NVarChar).Value = firstName;
                        sqlCommand.Parameters.Add("@Last_Name", SqlDbType.NVarChar).Value = lastName;
                        sqlCommand.Parameters.Add("@User_Password", SqlDbType.NVarChar).Value = password;
                        sqlCommand.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;
                        sqlCommand.Parameters.Add("@Title", SqlDbType.NVarChar).Value = title;
                        sqlCommand.Parameters.Add("@Mobile_Number", SqlDbType.NVarChar).Value = mobile;
                        sqlCommand.Parameters.Add("@Work_PhoneNumber", SqlDbType.NVarChar).Value = workPhone;
                        sqlCommand.Parameters.Add("@Fax_PhoneNumber", SqlDbType.NVarChar).Value = fax;
                        sqlCommand.Parameters.Add("@IM_Screen_Name", SqlDbType.NVarChar).Value = imScreenName;
                        sqlCommand.Parameters.Add("@IM_Service", SqlDbType.NVarChar).Value = imServiceType;
                        sqlCommand.Parameters.Add("@Company", SqlDbType.NVarChar).Value = company;
                        sqlCommand.Parameters.Add("@Bio", SqlDbType.NVarChar).Value = bio;
                        sqlCommand.Parameters.Add("@Picture", SqlDbType.NVarChar).Value = picturePath;
                        sqlCommand.Parameters.Add("@Gender", SqlDbType.NVarChar).Value = gender ;
                        sqlCommand.Parameters.Add("@Roles", SqlDbType.NVarChar).Value = role;
                        sqlCommand.Parameters.Add("@IsActive", SqlDbType.NVarChar).Value = 1;
                        sqlCommand.Parameters.Add("@Date_Created", SqlDbType.DateTime).Value = DateTime.Now;
                        sqlCommand.Parameters.Add("@Date_Modifies", SqlDbType.DateTime).Value = DateTime.Now;    
                        sqlConnection.Open();
                        var reader = sqlCommand.ExecuteNonQuery();
                    }
                }
                status = true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
                status = false;
            }
            return status;
        }

        public static bool UpdateCenterUser(string Center_Id, string firstName, string lastName, string workPhone, string officePhone, string fax, string mobile, string email, string gender, string imScreenName, string imServiceType, string title, string bio, string picturePath, string role, string userName, string company, long userId, int isActive)
        {
            bool status = false;

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string procName = "Update_Profile";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = procName;
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.Parameters.Clear();
                        sqlCommand.Parameters.Add("@Center_Id", SqlDbType.NVarChar).Value = Center_Id;
                        sqlCommand.Parameters.Add("@UserId", SqlDbType.BigInt).Value = userId;
                        //sqlCommand.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = userName;
                        sqlCommand.Parameters.Add("@First_Name", SqlDbType.NVarChar).Value = firstName;
                        sqlCommand.Parameters.Add("@Last_Name", SqlDbType.NVarChar).Value = lastName;
                        //sqlCommand.Parameters.Add("@User_Password", SqlDbType.NVarChar).Value = password;
                        sqlCommand.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;
                        sqlCommand.Parameters.Add("@Title", SqlDbType.NVarChar).Value = title;
                        sqlCommand.Parameters.Add("@Mobile_Number", SqlDbType.NVarChar).Value = mobile;
                        sqlCommand.Parameters.Add("@Work_PhoneNumber", SqlDbType.NVarChar).Value = workPhone;
                        sqlCommand.Parameters.Add("@Fax_PhoneNumber", SqlDbType.NVarChar).Value = fax;
                        sqlCommand.Parameters.Add("@IM_Screen_Name", SqlDbType.NVarChar).Value = imScreenName;
                        sqlCommand.Parameters.Add("@IM_Service", SqlDbType.NVarChar).Value = imServiceType;
                        sqlCommand.Parameters.Add("@Bio", SqlDbType.NVarChar).Value = bio;
                        sqlCommand.Parameters.Add("@Picture", SqlDbType.NVarChar).Value = picturePath;
                        sqlCommand.Parameters.Add("@Gender", SqlDbType.NVarChar).Value = gender;
                        sqlCommand.Parameters.Add("@Roles", SqlDbType.NVarChar).Value = role;
                        sqlCommand.Parameters.Add("@IsActive", SqlDbType.NVarChar).Value = isActive;                       
                        sqlCommand.Parameters.Add("@Date_Modifies", SqlDbType.DateTime).Value = DateTime.Now;
                        sqlCommand.Parameters.Add("@Company", SqlDbType.NVarChar).Value = company;
                        sqlConnection.Open();
                        var reader = sqlCommand.ExecuteNonQuery();
                    }
                }
                status = true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
                status = false;
            }
            return status;
        }

        public static int DeleteCenterUser(long userId)
        {
            int status = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string query = "Delete from Center_Users where UserId = " + userId + "";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        status = sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
                status = -1;
            }
            return status;
        }

        public static List<Employee> GetAllLocalAdmins()
        {
            List<Employee> usersList = new List<Employee>();
            using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
            {
                string procName = "Get_All_Local_Admins";
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = procName;
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    var reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        Employee d = new Employee()
                        {
                            FransId = reader["Center_Id"] is DBNull ? null : reader["Center_Id"].ToString(),
                            EmployeeId = reader["UserId"] is DBNull ? 0 : long.Parse(reader["UserId"].ToString()),
                            UserName = reader["UserName"] is DBNull ? null : reader["UserName"].ToString(),
                            FirstName = reader["First_Name"] is DBNull ? null : reader["First_Name"].ToString(),
                            LastName = reader["Last_Name"] is DBNull ? null : reader["Last_Name"].ToString(),
                            MobileNumber = reader["Mobile_Number"] is DBNull ? null : reader["Mobile_Number"].ToString(),
                            WorkPhone = reader["Work_PhoneNumber"] is DBNull ? null : reader["Work_PhoneNumber"].ToString(),
                            FaxPhone = reader["Fax_PhoneNumber"] is DBNull ? null : reader["Fax_PhoneNumber"].ToString(),
                            Email = reader["Email"] is DBNull ? null : reader["Email"].ToString(),
                            Title = reader["Title"] is DBNull ? null : reader["Title"].ToString(),
                            IMScreenName = reader["IM_Screen_Name"] is DBNull ? null : reader["IM_Screen_Name"].ToString(),
                            IMService = reader["IM_Service"] is DBNull ? null : reader["IM_Service"].ToString(),
                            Bio = reader["Bio"] is DBNull ? null : reader["Bio"].ToString(),
                            Company = reader["Company"] is DBNull ? null : reader["Company"].ToString(),
                            PicturePath = reader["Picture"] is DBNull ? null : reader["Picture"].ToString(),
                            Gender = reader["Gender"] is DBNull ? null : reader["Gender"].ToString(),
                            Roles = reader["Roles"] is DBNull ? null : reader["Roles"].ToString(),
                            IsActive = reader["IsActive"] is DBNull ? 1 : int.Parse(reader["IsActive"].ToString()),
                        };
                        usersList.Add(d);
                    }
                }
            }

            return usersList;
        }

        public static List<Employee> GetAllUsers()
        {
            List<Employee> usersList = new List<Employee>();
            using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
            {
                string procName = "Get_All_Users";
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = procName;
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    var reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        Employee d = new Employee()
                        {
                            FransId = reader["Center_Id"] is DBNull ? null : reader["Center_Id"].ToString(),
                            EmployeeId = reader["UserId"] is DBNull ? 0 : long.Parse(reader["UserId"].ToString()),
                            UserName = reader["UserName"] is DBNull ? null : reader["UserName"].ToString(),
                            FirstName = reader["First_Name"] is DBNull ? null : reader["First_Name"].ToString(),
                            LastName = reader["Last_Name"] is DBNull ? null : reader["Last_Name"].ToString(),
                            MobileNumber = reader["Mobile_Number"] is DBNull ? null : reader["Mobile_Number"].ToString(),
                            WorkPhone = reader["Work_PhoneNumber"] is DBNull ? null : reader["Work_PhoneNumber"].ToString(),
                            FaxPhone = reader["Fax_PhoneNumber"] is DBNull ? null : reader["Fax_PhoneNumber"].ToString(),
                            Email = reader["Email"] is DBNull ? null : reader["Email"].ToString(),
                            Title = reader["Title"] is DBNull ? null : reader["Title"].ToString(),
                            IMScreenName = reader["IM_Screen_Name"] is DBNull ? null : reader["IM_Screen_Name"].ToString(),
                            IMService = reader["IM_Service"] is DBNull ? null : reader["IM_Service"].ToString(),
                            Bio = reader["Bio"] is DBNull ? null : reader["Bio"].ToString(),
                            Company = reader["Company"] is DBNull ? null : reader["Company"].ToString(),
                            PicturePath = reader["Picture"] is DBNull ? null : reader["Picture"].ToString(),
                            Gender = reader["Gender"] is DBNull ? null : reader["Gender"].ToString(),
                            Roles = reader["Roles"] is DBNull ? null : reader["Roles"].ToString(),
                            IsActive = reader["IsActive"] is DBNull ? 1 : int.Parse(reader["IsActive"].ToString()),
                            FranservId = reader["FranservId"] is DBNull ? string.Empty : reader["FranservId"].ToString()
                        };
                        usersList.Add(d);
                    }
                }
            }

            return usersList;
        }

        public static AdminScreenPermissions GetUserPermission(string userRole, string username, bool refresh =false)
        {
            AdminScreenPermissions data = null;
            try
            {
                string cacheKey = "SignalGraphicsMain-FransWorkareaData" + username;
                bool dataExistInCache = ApplicationCache.IsExist(cacheKey);
                if (!dataExistInCache || refresh == true)
                {

                    using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                    {
                        string procName = "Get_User_Permissions";
                        using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                        {
                            sqlCommand.CommandText = procName;
                            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                            sqlCommand.Parameters.Clear();
                            sqlCommand.Parameters.Add("@Admin_Role", SqlDbType.NVarChar).Value = userRole;
                            sqlConnection.Open();
                            var reader = sqlCommand.ExecuteReader();

                            while (reader.Read())
                            {
                                data = new AdminScreenPermissions()
                                {
                                    Manage_Sent_Files = reader["Manage_Sent_Files"] is DBNull ? false : (reader["Manage_Sent_Files"].ToString() == "1" ? true : false),
                                    Manage_Request_A_Quotes = reader["Manage_Request_A_Quotes"] is DBNull ? false : (reader["Manage_Request_A_Quotes"].ToString() == "1" ? true : false),
                                    Manage_Center_Information = reader["Manage_Center_Information"] is DBNull ? false : (reader["Manage_Center_Information"].ToString() == "1" ? true : false),
                                    Manage_Why_weare_diff = reader["Manage_Why_weare_diff"] is DBNull ? false : (reader["Manage_Why_weare_diff"].ToString() == "1" ? true : false),
                                    Manage_Portfolio = reader["Manage_Portfolio"] is DBNull ? false : (reader["Manage_Portfolio"].ToString() == "1" ? true : false),
                                    Manage_Banners = reader["Manage_Banners"] is DBNull ? false : (reader["Manage_Banners"].ToString() == "1" ? true : false),
                                    Manage_Partners = reader["Manage_Partners"] is DBNull ? false : (reader["Manage_Partners"].ToString() == "1" ? true : false),
                                    Manage_Promotions = reader["Manage_Promotions"] is DBNull ? false : (reader["Manage_Promotions"].ToString() == "1" ? true : false),
                                    Manage_Careers = reader["Manage_Careers"] is DBNull ? false : (reader["Manage_Careers"].ToString() == "1" ? true : false),
                                    Manage_Testimonials = reader["Manage_Testimonials"] is DBNull ? false : (reader["Manage_Testimonials"].ToString() == "1" ? true : false),
                                    Manage_Local_Team = reader["Manage_Local_Team"] is DBNull ? false : (reader["Manage_Local_Team"].ToString() == "1" ? true : false),
                                    Manage_User_Profiles = reader["Manage_User_Profiles"] is DBNull ? false : (reader["Manage_User_Profiles"].ToString() == "1" ? true : false)
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
                        data = (AdminScreenPermissions)cacheData;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
                data = null;
            }
            return data;
        }

        public static int UpdateM3NewsIds(string centerId, string newsIds)
        {
            int status = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string query = "update Center_M3T_Data set Center_News_Ids = '" + newsIds + "' where Center_Id = '" + centerId + "'";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        status = sqlCommand.ExecuteNonQuery();
                    }
                }

                if (status <= 0)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                    {
                        //first record
                        string insertQuery = "insert into Center_M3T_Data values ('" + centerId + "', '" + newsIds + "','0','0','0','0')";
                        using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                        {
                            sqlCommand.CommandText = insertQuery;
                            sqlCommand.CommandType = System.Data.CommandType.Text;
                            sqlConnection.Open();
                            status = sqlCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
                status = -1;
            }
            return status;
        }

        public static int UpdateM3CaseStudiesIds(string centerId, string caseStudiesIds)
        {
            int status = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string query = "update Center_M3T_Data set Center_Case_Studies_Ids = '" + caseStudiesIds + "' where Center_Id = '" + centerId + "'";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        status = sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
                status = -1;
            }
            return status;
        }

        public static int UpdateM3AllInMediaIds(string centerId, string allInMediaIds)
        {
            int status = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string query = "update Center_M3T_Data set Center_All_In_The_Media_Ids = '" + allInMediaIds + "' where Center_Id = '" + centerId + "'";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        status = sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
                status = -1;
            }
            return status;
        }

        public static int UpdateM3BriefsWhitePapersIds(string centerId, string briefsWhitePapersIds)
        {
            int status = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string query = "update Center_M3T_Data set Center_Briefs_WhitePapers_Ids = '" + briefsWhitePapersIds + "' where Center_Id = '" + centerId + "'";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        status = sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
                status = -1;
            }
            return status;
        }
        
        public static int UpdateM3OurTeamIds(string centerId, string employeeIds)
        {
            int status = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string query = "update Center_M3T_Data set Center_Our_Team = '" + employeeIds + "' where Center_Id = '" + centerId + "'";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        status = sqlCommand.ExecuteNonQuery();
                    }
                }
                
                if (status <= 0)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                    {
                        //first record
                        string insertQuery = "insert into Center_M3T_Data values ('" + centerId + "', '0','0','0','0','" + employeeIds + "')";
                        using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                        {
                            sqlCommand.CommandText = insertQuery;
                            sqlCommand.CommandType = System.Data.CommandType.Text;
                            sqlConnection.Open();
                            status = sqlCommand.ExecuteNonQuery();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
                status = -1;
            }
            return status;
        }
        
        public static int UpdateM3FlickrPhotoSets(string centerId, string photoSetIds)
        {
            int status = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string query = "update Center_ThirdParty_Data set Center_Portfolio_PhotoSetIds = '" + photoSetIds + "' where Center_Id = '" + centerId + "'";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        status = sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
                status = -1;
            }
            return status;
        }
        
        public static bool IsLanguageExist(string langName)
        {
            bool status = true;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string query = "select Lang_Id from Center_Languages where Lang_Name like '" + langName + "'";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        var returnData = sqlCommand.ExecuteReader();
                        if(returnData != null && returnData.HasRows)
                            status = true;                     
                        else
                            status = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);                
            }
            return status;
        }

        public static List<CenterLanguage> GetAllCenterLanguages()
        {
            List<CenterLanguage> data = new List<CenterLanguage>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string query = "select * from Center_Languages";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        var reader = sqlCommand.ExecuteReader();
                        if (reader != null && reader.HasRows)
                        {
                            CenterLanguage item = null;
                            while (reader.Read())
                            {
                                item = new CenterLanguage();
                                if (reader.GetValue(0) != null)
                                    item.LangId = long.Parse(reader.GetValue(0).ToString());
                                if (reader.GetValue(1) != null)
                                    item.LangName = reader.GetValue(1).ToString();
                                data.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);               
            }
            return data;
        }

        public static int AddCenterLanguage(string langName)
        {
            int status = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string query = "insert into Center_Languages values('" + langName + "')";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        status = sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
                status = -1;
            }
            return status;
        }

        public static int DeleteCenterLanguage(long langId)
        {
            int status = -1;
            if (langId > 0)
            {
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                    {
                        string query = "delete from Center_Languages where Lang_Id = " + langId + "";
                        using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                        {
                            sqlCommand.CommandText = query;
                            sqlCommand.CommandType = System.Data.CommandType.Text;
                            sqlConnection.Open();
                            status = sqlCommand.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                    status = -1;
                }
            }
            return status;
        }

        public static List<RequestToQuote> GetAllRequestToQuotes(string centerId)
        {
            List<RequestToQuote> dataList = new List<RequestToQuote>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string query;
                    if (!string.IsNullOrEmpty(centerId))
                        query = "select * from Center_Request_Quote where Center_Id = '" + centerId + "' ";
                    else
                        query = "select * from Center_Request_Quote";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        var reader = sqlCommand.ExecuteReader();
                        if (reader != null && reader.HasRows)
                        {
                            RequestToQuote item = null;
                            while (reader.Read())
                            {
                                item = new RequestToQuote();
                                if (reader.GetValue(0) != null)
                                    item.QuoteId = long.Parse(reader.GetValue(0).ToString());
                                if (reader.GetValue(1) != null)
                                    item.CenterId = reader.GetValue(1).ToString();
                                if (reader.GetValue(2) != null)
                                    item.FirstName = reader.GetValue(2).ToString();
                                if (reader.GetValue(3) != null)
                                    item.LastName = reader.GetValue(3).ToString();
                                if (reader.GetValue(4) != null)
                                    item.Email = reader.GetValue(4).ToString();
                                if (reader.GetValue(5) != null)
                                    item.JobTitle = reader.GetValue(5).ToString();
                                if (reader.GetValue(6) != null)
                                    item.CompanyName = reader.GetValue(6).ToString();
                                if (reader.GetValue(7) != null)
                                    item.WebSite = reader.GetValue(7).ToString();
                                if (reader.GetValue(8) != null)
                                    item.MobileNumber = reader.GetValue(8).ToString();
                                if (reader.GetValue(9) != null)
                                    item.ProjectName = reader.GetValue(9).ToString();
                                if (reader.GetValue(10) != null)
                                    item.ProjectDescription = reader.GetValue(10).ToString();
                                if (reader.GetValue(11) != null)
                                    item.UploadedFileId = reader.GetValue(11).ToString();
                                if (reader.GetValue(12) != null)
                                    item.ProjectBudget = reader.GetValue(12).ToString();
                                if (reader.GetValue(13) != null)
                                    item.DateSubmitted = DateTime.Parse(reader.GetValue(13).ToString());
                                if (reader.GetValue(14) != null)
                                {
                                    string domain = reader.GetValue(14).ToString();
                                    if (domain != "")
                                        item.ServerDomain = reader.GetValue(14).ToString();
                                    else
                                        item.ServerDomain = "http://author.SignalGraphics.com";
                                }
                                else
                                {
                                    item.ServerDomain = "http://author.SignalGraphics.com";
                                }
                                dataList.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
            return dataList;
        }

        public static List<SendAFile> GetAllSendAFileInfo(string centerId)
        {
            List<SendAFile> dataList = new List<SendAFile>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string query;
                    string sendAFileIdTempStr = String.Empty;
                    if (!string.IsNullOrEmpty(centerId))
                        query = "select * from Center_Send_A_File where Center_Id = '" + centerId + "' ";
                    else
                        query = "select * from Center_Send_A_File";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        var reader = sqlCommand.ExecuteReader();
                        if (reader != null && reader.HasRows)
                        {
                            SendAFile item = null;
                            while (reader.Read())
                            {
                                item = new SendAFile();
                                if (reader.GetValue(0) != null)
                                {
                                    item.SendFileId = long.Parse(reader.GetValue(0).ToString());
                                    sendAFileIdTempStr = item.SendFileId.ToString();
                                }
                                if (reader.GetValue(1) != null)
                                    item.CenterId = reader.GetValue(1).ToString();
                                if (reader.GetValue(2) != null)
                                    item.FirstName = reader.GetValue(2).ToString();
                                if (reader.GetValue(3) != null)
                                    item.LastName = reader.GetValue(3).ToString();
                                if (reader.GetValue(4) != null)
                                    item.Email = reader.GetValue(4).ToString();
                                if (reader.GetValue(5) != null)
                                    item.JobTitle = reader.GetValue(5).ToString();
                                if (reader.GetValue(6) != null)
                                    item.CompanyName = reader.GetValue(6).ToString();
                                if (reader.GetValue(7) != null)
                                    item.WebSite = reader.GetValue(7).ToString();
                                if (reader.GetValue(8) != null)
                                    item.MobileNumber = reader.GetValue(8).ToString();
                                if (reader.GetValue(9) != null)
                                    item.ProjectName = reader.GetValue(9).ToString();
                                if (reader.GetValue(10) != null)
                                    item.ProjectDescription = reader.GetValue(10).ToString();
                                if (reader.GetValue(11) != null)
                                    item.UploadedFileId = reader.GetValue(11).ToString();
                                if (reader.GetValue(12) != null)
                                    item.ProjectQuantity = reader.GetValue(12).ToString();
                                if (reader.GetValue(13) != null)
                                    item.DateSubmitted = DateTime.Parse(reader.GetValue(13).ToString());
                                if (reader.GetValue(14) != null)
                                {
                                    string domain = reader.GetValue(14).ToString();
                                    if (domain != "")
                                        item.ServerDomain = reader.GetValue(14).ToString();
                                    else
                                        item.ServerDomain = "http://author.SignalGraphics.com";
                                }
                                else
                                {
                                    item.ServerDomain = "http://author.SignalGraphics.com";
                                }
                                item.ProjectDueDate = reader.GetValue(15).ToString();
                                item.UploadedFileExternalLinks = GetAllThirdPartyUploadFileLinks(sendAFileIdTempStr);
                                dataList.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
            return dataList;
        }

        public static string GetAllThirdPartyUploadFileLinks(string sendAFileId)
        {
            List<ThirdPartyUploadFileLinks> dataList = new List<ThirdPartyUploadFileLinks>();

            string externalLinks = String.Empty;

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string query;
                    if (!string.IsNullOrEmpty(sendAFileId))
                        query = "select * from Third_Party_Upload_Files where Send_A_File_Id = " + Convert.ToInt32(sendAFileId);
                    else
                        query = "select * from Third_Party_Upload_Files";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        var reader = sqlCommand.ExecuteReader();
                        if (reader != null && reader.HasRows)
                        {
                            ThirdPartyUploadFileLinks item = null;
                            while (reader.Read())
                            {
                                item = new ThirdPartyUploadFileLinks();
                                if (reader.GetValue(0) != null)
                                    item.ThirdPartyUploadId = long.Parse(reader.GetValue(0).ToString());
                                if (reader.GetValue(1) != null)
                                    item.SendFileId = long.Parse(reader.GetValue(1).ToString());
                                if (reader.GetValue(2) != null)
                                    item.FileLink = reader.GetValue(2).ToString();
                                if (string.IsNullOrWhiteSpace(externalLinks))
                                {
                                    externalLinks = item.FileLink;
                                }
                                else
                                {
                                    externalLinks += ", " +  item.FileLink;
                                }
                                dataList.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
            return externalLinks;
        }

        public static int DeleteRequestToQuote(long quoteId)
        {
            int status = -1;
            if (quoteId > 0)
            {
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                    {
                        string query = "delete from Center_Request_Quote where Quote_Id = " + quoteId + "";
                        using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                        {
                            sqlCommand.CommandText = query;
                            sqlCommand.CommandType = System.Data.CommandType.Text;
                            sqlConnection.Open();
                            status = sqlCommand.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                    status = -1;
                }
            }
            return status;
        }

        public static int DeleteSendAFileRecord(long sFileId)
        {
            int status = -1;
            if (sFileId > 0)
            {
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                    {
                        string query = "delete from Center_Send_A_File where Send_File_Id = " + sFileId + "";
                        using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                        {
                            sqlCommand.CommandText = query;
                            sqlCommand.CommandType = System.Data.CommandType.Text;
                            sqlConnection.Open();
                            status = sqlCommand.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                    status = -1;
                }
            }
            return status;
        }

        private static string FormateDMSId(string ids)
        {
            string id = "";
            if (!string.IsNullOrEmpty(ids))
            {
                string[] idsArray = ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if(idsArray != null && idsArray.Length > 0)
                {
                    return idsArray[0];
                }
            }
            return id;
        }

        public static int AddUpdateWhyWeAreDiffContent(WhyWeAreDiff data)
        {
            int status = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string query = "update Why_We_Diff_Content set Banner_Title ='" + data.BannerTitle + "', Banner_SubTitle= '" + data.BannerSubTitle + "', Content_Title= '" + data.ContentTitle + "', Content_TagLine= '" + data.ContentTagLine + "', Content_Description= '" + data.ContentDescription + "', Video_Link='" + data.VideoLink + "', Video_Statement_Text='" + data.VideoStatementText + "', Date_Modified = '" + DateTime.Now + "', Picute_Path='" + data.Image_Path + "' where Center_Id ='" + data.CenterId + "'";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        status = sqlCommand.ExecuteNonQuery();
                    }
                }

                if (status <= 0)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                    {
                        string insertQuery = "insert into Why_We_Diff_Content values ('" + data.CenterId + "', '" + data.BannerTitle + "', '" + data.BannerSubTitle + "', '" + data.ContentTitle + "', '" + data.ContentTagLine + "', '" + data.ContentDescription + "', '" + data.VideoLink + "', '" + data.VideoStatementText + "', '" + DateTime.Now + "', '" + DateTime.Now + "', '" + data.Image_Path + "')";
                        using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                        {
                            sqlCommand.CommandText = insertQuery;
                            sqlCommand.CommandType = System.Data.CommandType.Text;
                            sqlConnection.Open();
                            status = sqlCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
            return status;
        }

        public static int AddJobPostData(JobPost data)
        {
            int status = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    int isFullTime = data.IsFullTime ? 1 : 0;
                    int isPartTime = data.IsPartTime ? 1 : 0;

                    string insertQuery = "insert into Center_Careers values ('" + data.CenterId + "', '" + data.ProfileType + "', '" + data.Title + "', '" + data.Location + "', '" + data.Description + "', '" + data.Language + "', '" + data.DatePosted + "', '" + data.DateExpire + "', '0', '" + isPartTime + "', '" + isFullTime + "')";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = insertQuery;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
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

        public static int UpdateJobPost(JobPost data)
        {
            int status = -1;
            try
            {
                if (data.JobId > 0)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                    {
                        int isFullTime = data.IsFullTime ? 1 : 0;
                        int isPartTime = data.IsPartTime ? 1 : 0;

                        string query = "update Center_Careers set Job_Profile_Type ='" + data.ProfileType + "', Job_Title= '" + data.Title + "', Job_Location= '" + data.Location + "', Job_Description= '" + data.Description + "', Job_Languague= '" + data.Language + "', Job_Is_PartTime='" + isPartTime + "', Job_Is_FullTime='" + isFullTime + "', Job_Expired_Date='" + data.DateExpire + "' where Job_Id ='" + data.JobId + "'";
                        using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                        {
                            sqlCommand.CommandText = query;
                            sqlCommand.CommandType = System.Data.CommandType.Text;
                            sqlConnection.Open();
                            status = sqlCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
            return status;
        }

        public static int DeleteJobPostData(long jobId)
        {
            int status = -1;
            try
            {
                if (jobId > 0)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                    {
                        string insertQuery = "delete from Center_Careers where Job_Id = " + jobId + "";
                        using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                        {
                            sqlCommand.CommandText = insertQuery;
                            sqlCommand.CommandType = System.Data.CommandType.Text;
                            sqlConnection.Open();
                            status = sqlCommand.ExecuteNonQuery();
                        }
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
        /// obsolete
        /// </summary>       
        public static int AddPromotion(string promoXml)
        {
            int status = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string insertQuery = "insert into National_Manage_Promotions values ('" + promoXml + "', '" + DateTime.Now + "')";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = insertQuery;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
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
        /// obsolete
        /// </summary> 
        public static int UpdatePromotion(string promoXml, long promoId)
        {
            int status = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string insertQuery = "update National_Manage_Promotions set Promotion_Data = '" + promoXml + "' where Promotion_id = '" + promoId + "'";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = insertQuery;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
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
        /// obsolete method
        /// </summary>
        public static int DeletePromotion(long promotionId)
        {
            int status = -1;
            try
            {
                if (promotionId > 0)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                    {
                        string insertQuery = "delete from National_Manage_Promotions where Promotion_id = " + promotionId + "";
                        using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                        {
                            sqlCommand.CommandText = insertQuery;
                            sqlCommand.CommandType = System.Data.CommandType.Text;
                            sqlConnection.Open();
                            status = sqlCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
            return status;
        }

        public static int IsPromoAssigned(string centerId)
        {
            int status = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string insertQuery = "select count(*) from Center_Manage_Promotions where Center_Id = '" + centerId + "'";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = insertQuery;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        var reader = sqlCommand.ExecuteReader();
                        if (reader != null && reader.HasRows)
                        {                            
                            while (reader.Read())
                            {                               
                                if (reader.GetValue(0) != null)
                                    status = int.Parse(reader.GetValue(0).ToString());                                                             
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }

            return status;
        }

        public static int AssignCenterPromotions(string centerId, string assignedPromoIds)
        {
            int status = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string insertQuery = "insert into Center_Manage_Promotions values ('" + centerId + "', '" + assignedPromoIds + "', '0', null, null, null)";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = insertQuery;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
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
        /// update center promo data
        /// to update assignedPromoIds, send selectedPromoIds as empty string
        /// to update selectedPromoIds, send assignedPromoIds as empty string    
        /// to update both selectedPromoIds & assignedPromoIds then provide the ids for both the properties 
        public static int UpdateCenterPromotions(string centerId, string assignedPromoIds, string selectedPromoIds, string image1Link, string image2Link, DateTime expireDate)
        {
            int status = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string insertQuery = "";
                    if (selectedPromoIds != "" && assignedPromoIds != "")
                        insertQuery = "update Center_Manage_Promotions set Promotion_Available_Ids = '" + assignedPromoIds + "', Promotion_Selected_Ids ='" + selectedPromoIds + "' where Center_Id = '" + centerId + "'";
                    else if (assignedPromoIds != "")
                        insertQuery = "update Center_Manage_Promotions set Promotion_Available_Ids = '" + assignedPromoIds + "' where Center_Id = '" + centerId + "'";
                    else if (selectedPromoIds != "")
                        insertQuery = "update Center_Manage_Promotions set Promotion_Selected_Ids ='" + selectedPromoIds + "', Center_Promo_Image11_Link = '" + image1Link + "', Center_Promo_Image12_Link = '" + image2Link + "', Center_Promo_Expire_Date = '" + expireDate.ToString("yyyy/MM/dd") + "' where Center_Id = '" + centerId + "'";
                    
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = insertQuery;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
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

        public static Promotion GetPromotionData(string centerId)
        {
            Promotion pData = null;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string query = "select Center_Promo_Image11_Link, Center_Promo_Image12_Link, Center_Promo_Expire_Date from Center_Manage_Promotions where Center_Id = '" + centerId + "'";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        var reader = sqlCommand.ExecuteReader();
                        if (reader != null && reader.HasRows)
                        {
                            pData = new Promotion();
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != null)
                                    pData.PromoImage1Link = reader.GetValue(0) != null ? reader.GetValue(0).ToString() : null;
                                if (reader.GetValue(1) != null)
                                    pData.PromoImage2Link = reader.GetValue(1) != null ? reader.GetValue(1).ToString() : null;
                                if (reader.GetValue(2) != null)
                                    pData.ExpireDate = reader.GetValue(2).ToString() != string.Empty ? DateTime.Parse(reader.GetValue(2).ToString()) : DateTime.MinValue;
                            }                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
            return pData;
        }

        /// <summary>
        /// obsolete method
        /// </summary>
        /// <param name="promoId"></param>
        /// <returns></returns>
        public static int UpdateCenterPromosAfterDelete(long promoId)
        {
            int status = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string query = "select Center_Id from Center_Manage_Promotions where Promotion_Available_Ids like '%"+promoId+"%'";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        var reader = sqlCommand.ExecuteReader();
                        if (reader != null && reader.HasRows)
                        {
                            List<string> centerIds = new List<string>();
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != null)
                                {
                                    string centerId = reader.GetValue(0).ToString();
                                    if(centerId != "")
                                    {
                                        centerIds.Add(centerId);
                                    }                                    
                                }
                            }

                            if (centerIds != null && centerIds.Any())
                            {
                                foreach(var c in centerIds)
                                {
                                    string updatedAssignedIds = "";
                                    var assignedPromoDataList = AdminToolManager.GetAssignedCenterPromotions(c);
                                    if (assignedPromoDataList.Any(x => x == promoId))
                                    {                                        
                                        foreach (var p in assignedPromoDataList)
                                        {
                                            if(p != promoId)
                                            {
                                                updatedAssignedIds += p + ",";
                                            }
                                        }                                        
                                    }

                                    string updatedSelectedIds = "";
                                    var selectedPromoDataList = AdminToolManager.GetSelectedCenterPromotions(c);
                                    if (selectedPromoDataList != null && selectedPromoDataList.Any())
                                    {
                                        foreach (var p in selectedPromoDataList)
                                        {
                                            if (p != promoId)
                                            {
                                                updatedSelectedIds += p + ",";
                                            }
                                        }
                                    }

                                    if (updatedAssignedIds == "")
                                        updatedAssignedIds = "0";

                                    if (updatedSelectedIds == "")
                                        updatedSelectedIds = "0";

                                    //update center Data
                                    AdminToolManager.UpdateCenterPromotions(c, updatedAssignedIds, updatedSelectedIds, null, null, DateTime.MinValue);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
            return status;
        }
        
        public static List<long> GetAssignedCenterPromotions(string centerId)
        {
            List<long> assignedPromoList = null;
            string result = "";
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string query = "select Promotion_Available_Ids from Center_Manage_Promotions where Center_Id = '" + centerId + "'";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        var reader = sqlCommand.ExecuteReader();
                        if (reader != null && reader.HasRows)
                        {                            
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != null)
                                    result = reader.GetValue(0).ToString();                                
                            }

                            if(result != "")
                            {
                                assignedPromoList = new List<long>();
                                var stringIds = result.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                if (stringIds != null && stringIds.Length > 0)
                                {                                   
                                    foreach (var sId in stringIds)
                                    {
                                        long promoId;
                                        long.TryParse(sId, out promoId);
                                        if (promoId > 0)
                                            assignedPromoList.Add(promoId);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }

            return assignedPromoList;
        }

        public static List<long> GetSelectedCenterPromotions(string centerId)
        {
            List<long> selectedPromoList = null;
            string result = "";
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string query = "select Promotion_Selected_Ids from Center_Manage_Promotions where Center_Id = '" + centerId + "'";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        var reader = sqlCommand.ExecuteReader();
                        if (reader != null && reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != null)
                                    result = reader.GetValue(0).ToString();
                            }

                            if(result != "")
                            {
                                selectedPromoList = new List<long>();
                                var stringIds = result.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                if (stringIds != null && stringIds.Length > 0)
                                {
                                    foreach (var sId in stringIds)
                                    {
                                        long promoId;
                                        long.TryParse(sId, out promoId);
                                        if (promoId > 0)
                                            selectedPromoList.Add(promoId);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
            return selectedPromoList;
        }
        
        public static int AddTestimonial(Testimonials data)
        {
            int status = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string insertQuery = "insert into Testimonials values ('" + data.Center_Id + "', '" + data.Statement + "', '" + data.FirstName + "', '" + data.LastName + "', '" + data.Organization + "', '" + DateTime.Now + "', '" + data.Title + "', '" + DateTime.Now + "', '" + data.PicturePath + "')";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = insertQuery;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
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

        public static int UpdateTestimonial(Testimonials data)
        {
            int status = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string insertQuery = "update Testimonials set Testimonial='" + data.Statement + "', FirstName='" + data.FirstName + "', LastName='" + data.LastName + "', Organization='" + data.Organization + "', Title='" + data.Title + "', Date_Modified='" + DateTime.Now + "', Testimonial_Image='" + data.PicturePath + "' where id = '" + data.TestimonialId + "'";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = insertQuery;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
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

        public static int DeleteTestimonial(int tId)
        {
            int status = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string insertQuery = "delete from Testimonials where id = " + tId + "";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = insertQuery;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
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
        
        public static bool IsTeamAssigned(string centerId)
        {
            bool status = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string insertQuery = "select count(*) from Center_Workarea_Data where Center_Id = '" + centerId + "'";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = insertQuery;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        var reader = sqlCommand.ExecuteReader();
                        if (reader != null && reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != null)
                                    status = reader.GetValue(0).ToString() == "1" ? true : false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
            return status;
        }

        public static List<Employee> GetSeletedCenterUsers(string centerId)
        {
            List<Employee> selectedEmployeeList = null;
            string result = "";
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string query = "select Center_Employee_Ids from Center_Workarea_Data where Center_Id = '" + centerId + "'";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        var reader = sqlCommand.ExecuteReader();
                        if (reader != null && reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != null)
                                    result = reader.GetValue(0).ToString();
                            }

                            if (result != "")
                            {
                                selectedEmployeeList = new List<Employee>();
                                var stringIds = result.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                if (stringIds != null && stringIds.Length > 0)
                                {
                                    foreach (var sId in stringIds)
                                    {
                                        long userId;
                                        long.TryParse(sId, out userId);
                                        var employeeData = FransDataManager.GetEmployeeById(userId);
                                        if (employeeData != null)
                                            selectedEmployeeList.Add(employeeData);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
            return selectedEmployeeList;
        }

        public static int AddTeamMembers(string centerId, string employeeIds)
        {
            int status = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string insertQuery = "insert into Center_Workarea_Data values ('" + centerId + "', '0','0','0','0', '0', '0','<ArrayOfProductsAndServices xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'><ProductsAndServices /></ArrayOfProductsAndServices>','" + employeeIds + "','0')";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = insertQuery;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
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

        public static int UpdateTeamMembers(string centerId, string employeeIds)
        {
            int status = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string insertQuery = "update Center_Workarea_Data set Center_Employee_Ids='" + employeeIds + "' where Center_Id = '" + centerId + "'";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = insertQuery;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
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
        
        public static List<long> GetSelectedPSpageWhitePapers(string centerId)
        {
            List<long> selectedWPList = null;
            string result = "";
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string query = "select Selected_Whitepapers_Ids from Whitepapers_On_ProductAndServices_Page where Center_Id = '" + centerId + "'";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        var reader = sqlCommand.ExecuteReader();
                        if (reader != null && reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != null)
                                    result = reader.GetValue(0).ToString();
                            }

                            if (result != "")
                            {
                                selectedWPList = new List<long>();
                                var stringIds = result.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                if (stringIds != null && stringIds.Length > 0)
                                {
                                    foreach (var sId in stringIds)
                                    {
                                        long cId;
                                        long.TryParse(sId, out cId);
                                        if (cId > 0)
                                            selectedWPList.Add(cId);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
            return selectedWPList;
        }

        public static bool IsPSpageWhitePapersAssigned(string centerId)
        {
            bool status = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string insertQuery = "select count(*) from Whitepapers_On_ProductAndServices_Page where Center_Id = '" + centerId + "'";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = insertQuery;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        var reader = sqlCommand.ExecuteReader();
                        if (reader != null && reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != null)
                                    status = reader.GetValue(0).ToString() == "1" ? true : false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }

            return status;
        }

        public static int AddPSpageWhitePapers(string centerId, string whitePaperCids)
        {
            int status = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string insertQuery = "insert into Whitepapers_On_ProductAndServices_Page values ('" + centerId + "', '" + whitePaperCids + "')";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = insertQuery;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
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

        public static int UpdatePSpageWhitePapers(string centerId, string whitePaperCids)
        {
            int status = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string insertQuery = "update Whitepapers_On_ProductAndServices_Page set Selected_Whitepapers_Ids='" + whitePaperCids + "' where Center_Id = '" + centerId + "'";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = insertQuery;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
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
        
        public static List<long> GetAvailableBanners(string centerId)
        {
            List<long> selectedBannerList = null;
            string result = "";
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string query = "select Selected_Banner_Ids from Center_Avaliable_BannerIds where Center_Id = '" + centerId + "'";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        var reader = sqlCommand.ExecuteReader();
                        if (reader != null && reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != null)
                                    result = reader.GetValue(0).ToString();
                            }

                            if (result != "")
                            {
                                selectedBannerList = new List<long>();
                                var stringIds = result.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                if (stringIds != null && stringIds.Length > 0)
                                {
                                    foreach (var sId in stringIds)
                                    {
                                        long cId;
                                        long.TryParse(sId, out cId);
                                        if (cId > 0)
                                            selectedBannerList.Add(cId);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
            return selectedBannerList;
        }

        public static bool IsBannersAssigned(string centerId)
        {
            bool status = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string insertQuery = "select count(*) from Center_Avaliable_BannerIds where Center_Id = '" + centerId + "'";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = insertQuery;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        var reader = sqlCommand.ExecuteReader();
                        if (reader != null && reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != null)
                                    status = reader.GetValue(0).ToString() == "1" ? true : false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }

            return status;
        }

        public static int AddAvailableBanners(string centerId, string bannerCids)
        {
            int status = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string insertQuery = "insert into Center_Avaliable_BannerIds values ('" + centerId + "', '" + bannerCids + "')";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = insertQuery;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
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

        public static int UpdateAvailableBanners(string centerId, string bannerCids)
        {
            int status = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string insertQuery = "update Center_Avaliable_BannerIds set Selected_Banner_Ids='" + bannerCids + "' where Center_Id = '" + centerId + "'";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = insertQuery;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
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
        
        public static List<long> GetAvailablePandS(string centerId)
        {
            List<long> selectedPSList = null;
            string result = "";
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string query = "select Selected_ProductServices_Ids from Center_Avaliable_ProductServicesIds where Center_Id = '" + centerId + "'";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        var reader = sqlCommand.ExecuteReader();
                        if (reader != null && reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != null)
                                    result = reader.GetValue(0).ToString();
                            }

                            if (result != "")
                            {
                                selectedPSList = new List<long>();
                                var stringIds = result.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                if (stringIds != null && stringIds.Length > 0)
                                {
                                    foreach (var sId in stringIds)
                                    {
                                        long cId;
                                        long.TryParse(sId, out cId);
                                        if (cId > 0)
                                            selectedPSList.Add(cId);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
            return selectedPSList;
        }

        public static bool IsPandSAssigned(string centerId)
        {
            bool status = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string insertQuery = "select count(*) from Center_Avaliable_ProductServicesIds where Center_Id = '" + centerId + "'";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = insertQuery;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        var reader = sqlCommand.ExecuteReader();
                        if (reader != null && reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != null)
                                    status = reader.GetValue(0).ToString() == "1" ? true : false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }

            return status;
        }

        public static int AddAvailablePandS(string centerId, string psCids)
        {
            int status = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string insertQuery = "insert into Center_Avaliable_ProductServicesIds values ('" + centerId + "', '" + psCids + "')";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = insertQuery;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
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

        public static int UpdateAvailablePandS(string centerId, string psCids)
        {
            int status = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string insertQuery = "update Center_Avaliable_ProductServicesIds set Selected_ProductServices_Ids='" + psCids + "' where Center_Id = '" + centerId + "'";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = insertQuery;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
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

        public static bool CloneCenter(string selectedCenter, string currentCenter)
        {
            bool status = false;
            try
            {
                StringBuilder sb = new StringBuilder();
                //P&S
                var selectedCenterPS = GetAvailablePandS(selectedCenter);
                if(selectedCenterPS != null && selectedCenterPS.Any())
                {
                    foreach (var ps in selectedCenterPS)
                        sb.Append(ps + ",");

                    if (IsPandSAssigned(currentCenter))
                        UpdateAvailablePandS(currentCenter, sb.ToString());
                    else
                        AddAvailablePandS(currentCenter, sb.ToString());
                }

                //Thidparty data
                var thirdPartyData = FransDataManager.GetFransThirdPartyData(selectedCenter, true);
                if (thirdPartyData != null)
                {                    
                    var socialMediaData = thirdPartyData.SocialMediaData;
                    string socialMediaXml = Ektron.Cms.EkXml.Serialize(typeof(SocialMedia), socialMediaData);
                    AdminToolManager.UpdateThirdPartyData(currentCenter, thirdPartyData.FlickrUserId, thirdPartyData.TwitterUrl, socialMediaXml);
                }

                //Banners
                var selectedCenterBanners = GetAvailableBanners(selectedCenter);
                if (selectedCenterBanners != null && selectedCenterBanners.Any())
                {
                    sb = new StringBuilder();
                    foreach (var b in selectedCenterBanners)
                        sb.Append(b + ",");

                    if (IsBannersAssigned(currentCenter))
                        UpdateAvailableBanners(currentCenter, sb.ToString());
                    else
                        AddAvailableBanners(currentCenter, sb.ToString());
                }

                //Case Studies
                var selectedCenterWorkareaData = FransDataManager.GetFransWorkareaData(selectedCenter, true);
                if (selectedCenterWorkareaData != null && selectedCenterWorkareaData.CaseStudiesContentIds != null)
                {
                    sb = new StringBuilder();
                    foreach (var cs in selectedCenterWorkareaData.CaseStudiesContentIds)
                        sb.Append(cs + ",");
                    UpdateCaseStudyIds(currentCenter, sb.ToString());
                }

                //Partners
                if (selectedCenterWorkareaData != null && selectedCenterWorkareaData.PartnersContentIds != null)
                {
                    sb = new StringBuilder();
                    foreach (var p in selectedCenterWorkareaData.PartnersContentIds)
                        sb.Append(p + ",");
                  UpdatePartnersIds(currentCenter, sb.ToString());
                }

                //News
                if (selectedCenterWorkareaData != null && selectedCenterWorkareaData.NewsContentIds != null)
                {
                    sb = new StringBuilder();
                    foreach (var n in selectedCenterWorkareaData.NewsContentIds)
                        sb.Append(n + ",");
                   UpdateNewsIds(currentCenter, sb.ToString());
                }

                //All in the Media
                if (selectedCenterWorkareaData != null && selectedCenterWorkareaData.InTheMediaContentIds != null)
                {
                    sb = new StringBuilder();
                    foreach (var m in selectedCenterWorkareaData.InTheMediaContentIds)
                        sb.Append(m + ",");
                    UpdateAllInMediaIds(currentCenter, sb.ToString());                    
                }


                //Briefs & White Papers
                if (selectedCenterWorkareaData != null && selectedCenterWorkareaData.BriefsWhitePapersContentIds != null)
                {
                    sb = new StringBuilder();
                    foreach (var bwp in selectedCenterWorkareaData.BriefsWhitePapersContentIds)
                        sb.Append(bwp + ",");
                  UpdateBriefsWhitePapersIds(currentCenter, sb.ToString());
                }

                //Promotions
                var centerAssignedPromos = GetAssignedCenterPromotions(selectedCenter);
                if (centerAssignedPromos != null && centerAssignedPromos.Any())
                {
                    sb = new StringBuilder();
                    foreach (var p in centerAssignedPromos)
                        sb.Append(p + ",");
                    var promoStatus = AdminToolManager.IsPromoAssigned(currentCenter);
                    if (promoStatus <= 0)
                        AdminToolManager.AssignCenterPromotions(currentCenter, sb.ToString());
                    else
                        AdminToolManager.UpdateCenterPromotions(currentCenter, sb.ToString(), "", null, null, DateTime.MinValue);
                }

                //Shops
                if (selectedCenterWorkareaData != null && selectedCenterWorkareaData.ShopContentIds != null)
                {
                    sb = new StringBuilder();
                    foreach (var s in selectedCenterWorkareaData.ShopContentIds)
                        sb.Append(s + ",");
                    UpdateShopIds(currentCenter, sb.ToString());
                }

                //work on center selected P&S, Banners, Promotions

                //P&S
                if (selectedCenterWorkareaData != null && selectedCenterWorkareaData.ProductAndServices != null)
                {
                    string psXml = Ektron.Cms.EkXml.Serialize(typeof(List<ProductsAndServices>), selectedCenterWorkareaData.ProductAndServices);
                    UpdateProductAndServices(currentCenter, psXml);
                }

                //Banners
                if (selectedCenterWorkareaData != null && selectedCenterWorkareaData.BannerContentIds != null)
                {
                    sb = new StringBuilder();
                    foreach (var b in selectedCenterWorkareaData.BannerContentIds)
                        sb.Append(b + ",");
                    UpdateBannerIds(currentCenter, sb.ToString());
                }

                //Promotions
                var selectedCenterSelectedPromos = GetSelectedCenterPromotions(selectedCenter);
                if (selectedCenterSelectedPromos != null && selectedCenterSelectedPromos.Any())
                {
                    sb = new StringBuilder();
                    foreach (var p in selectedCenterSelectedPromos)
                        sb.Append(p + ",");
                    UpdateCenterPromotions(currentCenter, "", sb.ToString(), null, null, DateTime.MinValue);
                }

                //assign all the mega menu data
                var m3Data = FransDataManager.GetFransM3TData(selectedCenter, true);
                
                //News
                var menuNews = m3Data.NewsContentIds;
                if(menuNews != null && menuNews.Any())
                {
                    sb = new StringBuilder();
                    foreach (var n in menuNews)
                        sb.Append(n + ",");
                    UpdateM3NewsIds(currentCenter, sb.ToString());
                }

                //CaseStudies
                var menuCaseStudies = m3Data.CaseStudiesContentIds;
                if (menuCaseStudies != null && menuCaseStudies.Any())
                {
                    sb = new StringBuilder();
                    foreach (var cs in menuCaseStudies)
                        sb.Append(cs + ",");
                    UpdateM3CaseStudiesIds(currentCenter, sb.ToString());
                }

                //AllInMedia
                var menuAllInMedia = m3Data.AllInTheMediaIds;
                if (menuAllInMedia != null && menuAllInMedia.Any())
                {
                    sb = new StringBuilder();
                    foreach (var m in menuAllInMedia)
                        sb.Append(m + ",");
                    UpdateM3AllInMediaIds(currentCenter, sb.ToString());
                }

                //BriefsWhitepapers
                var menuBriefsWhitepapers = m3Data.BriefsWhitePapersContentIds;
                if (menuBriefsWhitepapers != null && menuBriefsWhitepapers.Any())
                {
                    sb = new StringBuilder();
                    foreach (var bwp in menuBriefsWhitepapers)
                        sb.Append(bwp + ",");
                    UpdateM3BriefsWhitePapersIds(currentCenter, sb.ToString());
                }                


                status = true;
            }
            catch(Exception ex)
            {
                Log.WriteError(ex);
            }
            return status;
        }

        public static List<Subscribtion> GetAllCenterSubscription(string centerId)
        {
            List<Subscribtion> list = new List<Subscribtion>();
            if (!string.IsNullOrEmpty(centerId))
            {
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                    {
                        string procName = "Get_All_Active_Subscribtion";
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
                                Subscribtion s = new Subscribtion()
                                {
                                    SubscribtionId = reader["S_Id"] is DBNull ? 0 : long.Parse(reader["S_Id"].ToString()),
                                    CenterId = reader["Center_Id"] is DBNull ? null : reader["Center_Id"].ToString(),
                                    OnlineType = reader["Subscribe_Online"] is DBNull ? null : reader["Subscribe_Online"].ToString(),
                                    PrintType = reader["Subscribe_Print"] is DBNull ? null : reader["Subscribe_Print"].ToString(),
                                    FirstName = reader["First_Name"] is DBNull ? null : reader["First_Name"].ToString(),
                                    LastName = reader["Last_Name"] is DBNull ? null : reader["Last_Name"].ToString(),
                                    Email = reader["Email_Address"] is DBNull ? null : reader["Email_Address"].ToString(),
                                    Address1 = reader["User_Address1"] is DBNull ? null : reader["User_Address1"].ToString(),
                                    Address2 = reader["User_Address2"] is DBNull ? null : reader["User_Address2"].ToString(),
                                    City = reader["User_City"] is DBNull ? null : reader["User_City"].ToString(),
                                    State = reader["User_State"] is DBNull ? null : reader["User_State"].ToString(),
                                    Zipcode = reader["User_Zipcode"] is DBNull ? null : reader["User_Zipcode"].ToString(),
                                    DatePosted = reader["Date_Submitted"] is DBNull ? DateTime.MinValue : DateTime.Parse(reader["Date_Submitted"].ToString())
                                };
                                list.Add(s);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }
            return list;
        }

        public static int DeleteSubscription(long sId)
        {
            int status = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string query = "Delete from Center_User_Subscribe where S_Id = " + sId + "";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        status = sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
                status = -1;
            }
            return status;
        }
        
        public static bool UpdateShopContentURL(string centerId, long contentId, string url, string isActive = "0")
        {
            bool status = false;
            try
            {
                int count = 0;
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string insertQuery = "select COUNT(*) from Center_Shop_Data where Center_id = '" + centerId + "' and Shop_Content_Id = " + contentId + "";
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = insertQuery;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlConnection.Open();
                        var firstColumn = sqlCommand.ExecuteScalar();
                        if (firstColumn != null)
                        {
                            int columnCount = int.Parse(firstColumn.ToString());
                            if (columnCount > 0)
                                count = 1;
                        }
                    }
                }

                if (count <= 0)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                    {
                        string insertQuery = "Insert into Center_Shop_Data values('" + centerId + "', " + contentId + ", '" + url + "', " + isActive + ")";
                        using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                        {
                            sqlCommand.CommandText = insertQuery;
                            sqlCommand.CommandType = System.Data.CommandType.Text;
                            sqlConnection.Open();
                            count = sqlCommand.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                    {
                        string insertQuery = "update Center_Shop_Data set Shop_URL = '" + url + "', IsActiveShop = " + isActive + " where Shop_Content_Id = " + contentId + " and Center_id = '" + centerId + "'";
                        using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                        {
                            sqlCommand.CommandText = insertQuery;
                            sqlCommand.CommandType = System.Data.CommandType.Text;
                            sqlConnection.Open();
                            count = sqlCommand.ExecuteNonQuery();
                        }
                    }
                }
                if (count > 0)
                    status = true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
            return status;
        }


        public static List<JobPost> GetAllJobs()
        {
            List<JobPost> jobList = null;
            try
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
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
            return jobList;
        }


        public static RequestToQuote GetRequestToQuotesById(long id)
        {
            RequestToQuote data = null;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string query = "select * from Center_Request_Quote where Quote_Id = @QuoteId ";                   
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.Parameters.Clear();
                        sqlCommand.Parameters.Add("@QuoteId", SqlDbType.BigInt).Value = id;
                        sqlConnection.Open();
                        var reader = sqlCommand.ExecuteReader();
                        if (reader != null && reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                data = new RequestToQuote();
                                if (reader.GetValue(0) != null)
                                    data.QuoteId = long.Parse(reader.GetValue(0).ToString());
                                if (reader.GetValue(1) != null)
                                    data.CenterId = reader.GetValue(1).ToString();
                                if (reader.GetValue(2) != null)
                                    data.FirstName = reader.GetValue(2).ToString();
                                if (reader.GetValue(3) != null)
                                    data.LastName = reader.GetValue(3).ToString();
                                if (reader.GetValue(4) != null)
                                    data.Email = reader.GetValue(4).ToString();
                                if (reader.GetValue(5) != null)
                                    data.JobTitle = reader.GetValue(5).ToString();
                                if (reader.GetValue(6) != null)
                                    data.CompanyName = reader.GetValue(6).ToString();
                                if (reader.GetValue(7) != null)
                                    data.WebSite = reader.GetValue(7).ToString();
                                if (reader.GetValue(8) != null)
                                    data.MobileNumber = reader.GetValue(8).ToString();
                                if (reader.GetValue(9) != null)
                                    data.ProjectName = reader.GetValue(9).ToString();
                                if (reader.GetValue(10) != null)
                                    data.ProjectDescription = reader.GetValue(10).ToString();
                                if (reader.GetValue(11) != null)
                                    data.UploadedFileId = reader.GetValue(11).ToString();
                                if (reader.GetValue(12) != null)
                                    data.ProjectBudget = reader.GetValue(12).ToString();
                                if (reader.GetValue(13) != null)
                                    data.DateSubmitted = DateTime.Parse(reader.GetValue(13).ToString());
                                if (reader.GetValue(14) != null)
                                {
                                    string domain = reader.GetValue(14).ToString();
                                    if (domain != "")
                                        data.ServerDomain = reader.GetValue(14).ToString();
                                    else
                                        data.ServerDomain = "http://author.SignalGraphics.com";
                                }
                                else
                                {
                                    data.ServerDomain = "http://author.SignalGraphics.com";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
            return data;
        }

        public static SendAFile GetSendAFileInfoById(long id)
        {
            SendAFile data = null;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string query = "select * from Center_Send_A_File where Send_File_Id = @SAFId ";                   
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.Parameters.Clear();
                        sqlCommand.Parameters.Add("@SAFId", SqlDbType.BigInt).Value = id;
                        sqlConnection.Open();
                        var reader = sqlCommand.ExecuteReader();
                        if (reader != null && reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                data = new SendAFile();
                                if (reader.GetValue(0) != null)
                                {
                                    data.SendFileId = long.Parse(reader.GetValue(0).ToString());
                                }
                                if (reader.GetValue(1) != null)
                                    data.CenterId = reader.GetValue(1).ToString();
                                if (reader.GetValue(2) != null)
                                    data.FirstName = reader.GetValue(2).ToString();
                                if (reader.GetValue(3) != null)
                                    data.LastName = reader.GetValue(3).ToString();
                                if (reader.GetValue(4) != null)
                                    data.Email = reader.GetValue(4).ToString();
                                if (reader.GetValue(5) != null)
                                    data.JobTitle = reader.GetValue(5).ToString();
                                if (reader.GetValue(6) != null)
                                    data.CompanyName = reader.GetValue(6).ToString();
                                if (reader.GetValue(7) != null)
                                    data.WebSite = reader.GetValue(7).ToString();
                                if (reader.GetValue(8) != null)
                                    data.MobileNumber = reader.GetValue(8).ToString();
                                if (reader.GetValue(9) != null)
                                    data.ProjectName = reader.GetValue(9).ToString();
                                if (reader.GetValue(10) != null)
                                    data.ProjectDescription = reader.GetValue(10).ToString();
                                if (reader.GetValue(11) != null)
                                    data.UploadedFileId = reader.GetValue(11).ToString();
                                if (reader.GetValue(12) != null)
                                    data.ProjectQuantity = reader.GetValue(12).ToString();
                                if (reader.GetValue(13) != null)
                                    data.DateSubmitted = DateTime.Parse(reader.GetValue(13).ToString());
                                if (reader.GetValue(14) != null)
                                {
                                    string domain = reader.GetValue(14).ToString();
                                    if (domain != "")
                                        data.ServerDomain = reader.GetValue(14).ToString();
                                    else
                                        data.ServerDomain = "http://author.SignalGraphics.com";
                                }
                                else
                                {
                                    data.ServerDomain = "http://author.SignalGraphics.com";
                                }
                                data.ProjectDueDate = reader.GetValue(15).ToString();
                                data.UploadedFileExternalLinks = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
            return data;
        }

    }

}