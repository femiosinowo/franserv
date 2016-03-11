using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SirSpeedy.CMS;
using System.Configuration;
using Ektron.Cms.Framework.Content;
using Ektron.Cms.Content;
using Ektron.Cms;
using Ektron.Cms.Common;
using System.Xml.Linq;
using System.Xml.XPath;
using Ektron.Cms.Instrumentation;
using FlickrNet;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Web.Script.Serialization;
using System.Data;
using Ektron.Cms.Settings.UrlAliasing.DataObjects;


public partial class test : System.Web.UI.Page
{
    private static string adminToolConnectionString = ConfigurationManager.ConnectionStrings["PIPAdminTool.DbConnection"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        //long environmentMsgTemplateId = 70;
        //long privacyTemplateId = 71;
        //long termsTemplateId = 72;

        //string environmentMsgTitle = "environmental message";
        //string privacyTitle = "privacy policy";
        //string termsTitle = "terms and conditions";

        //string environmentMsgTemplate = "EnvironmentalMessage";
        //string privacyTemplate = "Privacy";
        //string termsTemplate = "TermsAndConditions";

        //var LegacyCM = new Ektron.Cms.API.Content.Content();
        //var LM = new LibraryManager();

        //var allCenters = FransDataManager.GetAllFransLocationDataList();
        //if(allCenters != null && allCenters.Any())
        //{
        //    foreach(var c in allCenters)
        //    {
        //        ContentCriteria cc = new ContentCriteria();
        //        cc.AddFilter(ContentProperty.FolderName, CriteriaFilterOperator.EqualTo, c.FransId);
        //        cc.PagingInfo = new PagingInfo(1000);
        //        var contentList = ContentHelper.GetListByCriteria(cc);

        //        if(contentList != null && contentList.Any())
        //        {
        //            var envirmContent = contentList.Where(x => x.Title.ToLower().Equals(environmentMsgTitle)).FirstOrDefault();
        //            if(envirmContent != null && envirmContent.Id > 0)
        //            {
        //                LegacyCM.EkContentRef.UpdateTemplate(envirmContent.Id, 1033, environmentMsgTemplateId); // content id, language, template id

        //                var LibData = LM.GetLibraryItemByContentId(envirmContent.Id); // content id
        //                LibData.FileName = LibData.FileName.ToLower().Replace("content", "EnvironmentalMessage");
        //                LM.Update(LibData);

        //                var ac = new AliasCriteria();
        //                ac.AddFilter(AliasProperty.Alias, Ektron.Cms.Common.CriteriaFilterOperator.EqualTo, envirmContent.Quicklink);
        //                var list = AliasHelper.GetList(ac);
        //                if (list != null && list.Any())
        //                {
        //                    var defaultAlis = list.FirstOrDefault();
        //                    string modifiedTarget = defaultAlis.TargetURL.ToLower().Replace("content", environmentMsgTemplate);

        //                    defaultAlis.TargetURL = modifiedTarget;
        //                    AliasHelper.Update(defaultAlis);
        //                }
        //            }

        //            var privacyContent = contentList.Where(x => x.Title.ToLower().Equals(privacyTitle)).FirstOrDefault();
        //            if (privacyContent != null && privacyContent.Id > 0)
        //            {
        //                LegacyCM.EkContentRef.UpdateTemplate(privacyContent.Id, 1033, privacyTemplateId); // content id, language, template id

        //                var LibData = LM.GetLibraryItemByContentId(privacyContent.Id); // content id
        //                LibData.FileName = LibData.FileName.ToLower().Replace("content", "Privacy");
        //                LM.Update(LibData);

        //                var ac = new AliasCriteria();
        //                ac.AddFilter(AliasProperty.Alias, Ektron.Cms.Common.CriteriaFilterOperator.EqualTo, privacyContent.Quicklink);
        //                var list = AliasHelper.GetList(ac);
        //                if (list != null && list.Any())
        //                {
        //                    var defaultAlis = list.FirstOrDefault();
        //                    string modifiedTarget = defaultAlis.TargetURL.ToLower().Replace("content", privacyTemplate);

        //                    defaultAlis.TargetURL = modifiedTarget;
        //                    AliasHelper.Update(defaultAlis);
        //                }
        //            }

        //            var termsContent = contentList.Where(x => x.Title.ToLower().Equals(termsTitle)).FirstOrDefault();
        //            if (termsContent != null && termsContent.Id > 0)
        //            {
        //                LegacyCM.EkContentRef.UpdateTemplate(termsContent.Id, 1033, termsTemplateId); // content id, language, template id

        //                var LibData = LM.GetLibraryItemByContentId(termsContent.Id); // content id
        //                LibData.FileName = LibData.FileName.ToLower().Replace("content", "TermsAndConditions");
        //                LM.Update(LibData);

        //                var ac = new AliasCriteria();
        //                ac.AddFilter(AliasProperty.Alias, Ektron.Cms.Common.CriteriaFilterOperator.EqualTo, termsContent.Quicklink);
        //                var list = AliasHelper.GetList(ac);
        //                if (list != null && list.Any())
        //                {
        //                    var defaultAlis = list.FirstOrDefault();
        //                    string modifiedTarget = defaultAlis.TargetURL.ToLower().Replace("content", termsTemplate);

        //                    defaultAlis.TargetURL = modifiedTarget;
        //                    AliasHelper.Update(defaultAlis);
        //                }
        //            }
        //        }
        //    }
        //}     


        StringBuilder sb = new StringBuilder();
        bool clearCache = false;
        string centerId = "";

        if (Request.QueryString.HasKeys())
        {
            if (!string.IsNullOrEmpty(Request.QueryString["cache"]))
            {
                bool.TryParse(Request.QueryString["cache"], out clearCache);
            }

            if (!string.IsNullOrEmpty(Request.QueryString["centerid"]))
            {
                centerId = Request.QueryString["centerid"];
            }

            var nationalData = GetNationalSocialMediaLinks();
            if (nationalData != null)
            {
                var socialIconsData = nationalData;
                sb.Append("<ul class=\"small_social_media\">");
                if ((!string.IsNullOrEmpty(socialIconsData.FaceBookUrl)) && (!string.IsNullOrEmpty(socialIconsData.FaceBookImgPath)))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.FaceBookUrl + "\"><img alt=\"Facebook\" src=\"" + socialIconsData.FaceBookImgPath + "\"/></a></li>");
                if ((!string.IsNullOrEmpty(socialIconsData.TwitterUrl)) && (!string.IsNullOrEmpty(socialIconsData.TwitterImgPath)))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.TwitterUrl + "\"><img alt=\"Twitter\" src=\"" + socialIconsData.TwitterImgPath + "\"/></a></li>");
                if ((!string.IsNullOrEmpty(socialIconsData.GooglePlusUrl)) && (!string.IsNullOrEmpty(socialIconsData.GooglePlusImgPath)))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.GooglePlusUrl + "\"><img alt=\"Google Plus\" src=\"" + socialIconsData.GooglePlusImgPath + "\"/></a></li>");
                if ((!string.IsNullOrEmpty(socialIconsData.LinkedInUrl)) && (!string.IsNullOrEmpty(socialIconsData.LinkedInImgPath)))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.LinkedInUrl + "\"><img alt=\"LinkedIn\" src=\"" + socialIconsData.LinkedInImgPath + "\"/></a></li>");
                if ((!string.IsNullOrEmpty(socialIconsData.StumbleUponUrl)) && (!string.IsNullOrEmpty(socialIconsData.StumbleUponImgPath)))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.StumbleUponUrl + "\"><img alt=\"Stumble Upon\" src=\"" + socialIconsData.StumbleUponImgPath + "\"/></a></li>");
                if ((!string.IsNullOrEmpty(socialIconsData.FlickrUrl)) && (!string.IsNullOrEmpty(socialIconsData.FlickrImgPath)))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.FlickrUrl + "\"><img alt=\"Flickr\" src=\"" + socialIconsData.FlickrImgPath + "\"/></a></li>");
                if ((!string.IsNullOrEmpty(socialIconsData.YouTubeUrl)) && (!string.IsNullOrEmpty(socialIconsData.YouTubeImgPath)))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.YouTubeUrl + "\"><img alt=\"You Tube\" src=\"" + socialIconsData.YouTubeImgPath + "\"/></a></li>");
                if ((!string.IsNullOrEmpty(socialIconsData.MarketingTangoUrl)) && (!string.IsNullOrEmpty(socialIconsData.MarketingTangoImgPath)))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.MarketingTangoUrl + "\"><img alt=\"Marketing Tango\" src=\"" + socialIconsData.MarketingTangoImgPath + "\"/></a></li>");
                sb.Append("</ul>");
                ltrNational.Text = sb.ToString();
            }

            var fransData = FransDataManager.GetFransThirdPartyData(centerId, clearCache);
            if (fransData != null && fransData.SocialMediaData != null)
            {
                sb = new StringBuilder();
                var socialIconsData = fransData.SocialMediaData;
                sb.Append("<ul class=\"small_social_media\">");
                if ((!string.IsNullOrEmpty(socialIconsData.FaceBookUrl)))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.FaceBookUrl + "\"><img alt=\"Facebook\" src=\"" + nationalData.FaceBookImgPath + "\"/></a></li>");
                if ((!string.IsNullOrEmpty(socialIconsData.TwitterUrl)))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.TwitterUrl + "\"><img alt=\"Twitter\" src=\"" + nationalData.TwitterImgPath + "\"/></a></li>");
                if ((!string.IsNullOrEmpty(socialIconsData.GooglePlusUrl)))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.GooglePlusUrl + "\"><img alt=\"Google Plus\" src=\"" + nationalData.GooglePlusImgPath + "\"/></a></li>");
                if ((!string.IsNullOrEmpty(socialIconsData.LinkedInUrl)))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.LinkedInUrl + "\"><img alt=\"LinkedIn\" src=\"" + nationalData.LinkedInImgPath + "\"/></a></li>");
                if ((!string.IsNullOrEmpty(socialIconsData.StumbleUponUrl)))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.StumbleUponUrl + "\"><img alt=\"Stumble Upon\" src=\"" + nationalData.StumbleUponImgPath + "\"/></a></li>");
                if ((!string.IsNullOrEmpty(socialIconsData.FlickrUrl)))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.FlickrUrl + "\"><img alt=\"Flickr\" src=\"" + nationalData.FlickrImgPath + "\"/></a></li>");
                if ((!string.IsNullOrEmpty(socialIconsData.YouTubeUrl)))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.YouTubeUrl + "\"><img alt=\"You Tube\" src=\"" + nationalData.YouTubeImgPath + "\"/></a></li>");
                if ((!string.IsNullOrEmpty(socialIconsData.MarketingTangoUrl)))
                    sb.Append("<li><a target=\"_blank\" href=\"" + socialIconsData.MarketingTangoUrl + "\"><img alt=\"Marketing Tango\" src=\"" + nationalData.MarketingTangoImgPath + "\"/></a></li>");
                sb.Append("</ul>");
                ltrLocal.Text = sb.ToString();
            }
        }
    }

    private SocialMedia GetNationalSocialMediaLinks()
    {
        SocialMedia data = null;
        long contentId = ConfigHelper.GetValueLong("SocialMediaContentId");
        if (contentId > 0)
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
                    catch (Exception ex)
                    {
                        Log.WriteError(ex.Message + ":" + ex.StackTrace);
                    }
                }
            }
        }
        return data;
    }

    public FransThirdPartyData GetFransThirdPartyData(string centerId, bool refresh = false)
    {
        FransThirdPartyData fransThirdPartyData = null;

        if (!string.IsNullOrEmpty(centerId))
        {
            using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
            {
                string procName = "Get_All_Center_ThirdParty_Data";
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
        }

        return fransThirdPartyData;
    }

    private List<string> GetStringIdsFromString(string idsText)
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

}