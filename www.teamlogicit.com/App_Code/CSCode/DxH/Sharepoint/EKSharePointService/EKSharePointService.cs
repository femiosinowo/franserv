using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Ektron.Cms.Framework.Content;
using EkSharePoint;
using System.Web.Services.Protocols;
using Ektron.Cms.Content;
using Ektron.Cms.Common;
using Ektron.Cms;
using Ektron.Cms.User;
using System.Data.SqlClient;
using System.Data;
using Ektron.Cms.DataIO;

namespace EKSharePoint
{
    /// <summary>
    /// Summary description for EkSharepointService
    /// </summary>
    [WebService(Namespace = "http://www.ektron.com/webservices/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class EKSharePointService : System.Web.Services.WebService
    {
        #region constructors

        /// <summary>
        /// Constructor logic goes here 
        /// </summary>
        public EKSharePointService() { }

        #endregion

        #region properties

        private AssetManager assetManager = null;
        public UserAuthHeader userauthHeader;

        #endregion

        #region publicmethods

        /// <summary>
        /// Adds an Asset to Cms
        /// </summary>
        /// <param name="contentassetData"></param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("userauthHeader", Direction = SoapHeaderDirection.InOut)]
        public long AddAsset(string title, string filename, long folderId, int languageId, long externalTypeid, byte[] filestream, ContentMetaData[] potentialMeta)
        {



            try
            {
                assetManager = new AssetManager();
                assetManager.ContentLanguage = languageId;
                if (string.IsNullOrEmpty(userauthHeader.AuthenticationToken))
                    throw new SoapException("User not logged in", SoapException.ClientFaultCode);

                this.ImpersonateUser(userauthHeader.AuthenticationToken, assetManager.RequestInformation);

                ContentAssetData contentassetdata = new ContentAssetData()
                {
                    FolderId = folderId,
                    Title = title,
                    File = filestream,
                    LanguageId = languageId,
                    ExternalTypeId = externalTypeid,
                    AssetData = new AssetData { FileName = filename },
                    MetaData = potentialMeta
                };

                return assetManager.Add(contentassetdata).Id;
            }
            catch (Exception ex)
            {
                throw new SoapException("Error adding an asset:" + ex.Message, SoapException.ClientFaultCode);
            }
        }

        /// <summary>
        /// Updates an Asset in Cms
        /// </summary>
        /// <param name="contentassetData"></param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("userauthHeader", Direction = SoapHeaderDirection.InOut)]
        public void UpdateAsset(string title, string filename, long contentid, int languageId, byte[] filestream, ContentMetaData[] potentialMeta)
        {
            try
            {
                assetManager = new AssetManager();
                assetManager.ContentLanguage = languageId;
                if (string.IsNullOrEmpty(userauthHeader.AuthenticationToken))
                    throw new SoapException("User not logged in", SoapException.ClientFaultCode);

                this.ImpersonateUser(userauthHeader.AuthenticationToken, assetManager.RequestInformation);

                ContentAssetData contentassetdata = assetManager.GetItem(contentid, true);

                if (contentassetdata == null)
                    throw new SoapException("Error Updating Asset", SoapException.ClientFaultCode);

                contentassetdata.Title = title;
                contentassetdata.File = filestream;
                contentassetdata.AssetData.FileName = filename;
                contentassetdata.LanguageId = languageId;
                contentassetdata.MetaData = potentialMeta;

                assetManager.Update(contentassetdata);
            }
            catch (Exception ex)
            {
                throw new SoapException("Error updating an asset:" + ex.Message, SoapException.ClientFaultCode);
            }
        }

        /// <summary>
        /// Gets the content id related to the asset filename
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="folderid"></param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("userauthHeader", Direction = SoapHeaderDirection.InOut)]
        public long GetAssetContentID(string filename, long folderid, int languageid)
        {

            if (string.IsNullOrEmpty(userauthHeader.AuthenticationToken))
                throw new SoapException("User not logged in", SoapException.ClientFaultCode);

            assetManager = new AssetManager();
            assetManager.ContentLanguage = languageid;
            this.ImpersonateUser(userauthHeader.AuthenticationToken, assetManager.RequestInformation);

            EkContent ekcontentinstance = new EkContent(assetManager.RequestInformation);
            Ektron.ASM.AssetConfig.AssetData assetdata = ekcontentinstance.GetAssetDataBasedOnFileName(filename, folderid);

            if (assetdata == null)
                return 0;
            else
                return Convert.ToInt64(assetdata.ID);

        }

        /// <summary>
        /// Gets the quick link of the asset
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="title"></param>
        /// <param name="contentid"></param>
        /// <param name="folderid"></param>
        /// <param name="filestream"></param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("userauthHeader", Direction = SoapHeaderDirection.InOut)]
        public string GetAssetContentQuickLink(string filename, string title, int languageid, long contentid, long folderid, byte[] filestream)
        {
            if (string.IsNullOrEmpty(userauthHeader.AuthenticationToken))
                throw new SoapException("User not logged in", SoapException.ClientFaultCode);

            assetManager = new AssetManager();
            assetManager.ContentLanguage = languageid;
            this.ImpersonateUser(userauthHeader.AuthenticationToken, assetManager.RequestInformation);

            if (contentid > 0)
            {
                ContentAssetData contentassetdata = assetManager.GetItem(contentid, true);

                if (contentassetdata == null)
                    throw new SoapException("Error Updating Asset", SoapException.ClientFaultCode);

                contentassetdata.Title = title;
                contentassetdata.File = filestream;
                contentassetdata.AssetData.FileName = filename;
                contentassetdata.LanguageId = languageid;

                return assetManager.GetItem((assetManager.Update(contentassetdata).Id)).Quicklink;
            }
            else
            {
                ContentAssetData contentassetdata = new ContentAssetData()
                {
                    FolderId = folderid,
                    Title = title,
                    File = filestream,
                    LanguageId = languageid,
                    AssetData = new AssetData { FileName = filename }
                };

                return assetManager.GetItem((assetManager.Add(contentassetdata).Id)).Quicklink;
            }
        }


        /// <summary>
        /// Updates the sharepoint list  smartform
        /// </summary>
        /// <param name="listtitle"></param>
        [WebMethod]
        [SoapHeader("userauthHeader", Direction = SoapHeaderDirection.InOut)]
        public void UpdateEKSharePointListSmartformData(string listtitle)
        {
            try
            {
                if (string.IsNullOrEmpty(userauthHeader.AuthenticationToken))
                    throw new SoapException("User not logged in", SoapException.ClientFaultCode);

                assetManager = new AssetManager();
                this.ImpersonateUser(userauthHeader.AuthenticationToken, assetManager.RequestInformation);

                EkContentRW ekcontent = new EkContentRW(assetManager.RequestInformation);
                ekcontent.UpdateEktronSPListSmartformData(listtitle);
            }
            catch (Exception ex)
            {
                throw new SoapException("Error updating eksharepointlistsmartformdata:" + ex.Message, SoapException.ClientFaultCode);
            }
        }

        /// <summary>
        /// Updates the folder with proper smartform id
        /// </summary>
        /// <param name="folderid"></param>
        /// <param name="smartformid"></param>
        [WebMethod]
        [SoapHeader("userauthHeader", Direction = SoapHeaderDirection.InOut)]
        public void UpdateFolderXmlConfig(long folderid, long smartformid)
        {
            try
            {
                if (string.IsNullOrEmpty(userauthHeader.AuthenticationToken))
                    throw new SoapException("User not logged in", SoapException.ClientFaultCode);

                assetManager = new AssetManager();
                this.ImpersonateUser(userauthHeader.AuthenticationToken, assetManager.RequestInformation);

                EkContentRW ekcontent = new EkContentRW(assetManager.RequestInformation);

                long existingsmartformid = ekcontent.GetEnabledXmlConfigsByFolder(folderid).Where(p => p.Equals(smartformid)).FirstOrDefault();

                if (existingsmartformid == null || existingsmartformid != smartformid)
                    ekcontent.AssignXmlConfigToFolder(folderid, smartformid, false);
            }
            catch (Exception ex)
            {
                throw new SoapException("Error updating folderxmlconfig:" + ex.Message, SoapException.ClientFaultCode);
            }

        }

        #endregion

        #region private methods

        /// <summary>
        /// Impersonating the Inbound Connection User of the DxH.
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="requestInformation"></param>
        private void ImpersonateUser(string authenticationToken, EkRequestInformation requestInformation)
        {
            UserData user = null;
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                user = UserContext.GetCurrentUser();
                if (user.AuthenticationToken != authenticationToken)
                    user = null;
            }
            if (user == null || string.IsNullOrEmpty(user.AuthenticationToken))
            {
                IUser userManager = ObjectFactory.GetUser(requestInformation.Clone() as EkRequestInformation);
                userManager.RequestInformation.CallerId = EkConstants.InternalAdmin;
                userManager.RequestInformation.UserId = EkConstants.InternalAdmin;
                user = userManager.GetItemByToken(authenticationToken);
            }
            if (user != null)
            {
                requestInformation.UserId = user.Id;
                requestInformation.CallerId = user.Id;
                requestInformation.AuthenticationToken = authenticationToken;
                requestInformation.ClientEktGUID = user.SessionId.ToString();
            }

            if (requestInformation.UserId == 0)
                throw new SoapException("User not logged in", SoapException.ClientFaultCode);
        }

        #endregion
    }
}

