using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ektron.FileSync.Common;
using Ektron.Cms.Sync.Client;

/// <summary>
/// This class provides the CloudSyncParams for all cloud command.  It is used in SyncHandler.ashx and CloudDeploymentHandler.ashx
/// </summary>
public class CloudParamWrapper
{
    Ektron.Cms.CommonApi _commonApi = null;
    public CloudParamWrapper()
    {
        _commonApi = new Ektron.Cms.CommonApi();
    }
    public SyncProfileActions GetParam(long relationId)
    {
        Relationship relationship = Relationship.GetRelationship(relationId);
        return GetParams(relationship, relationId);
    }
    public SyncProfileActions GetParams(Relationship relationship, long profileId)
    {
        SyncProfileActions cloudSyncParams = new SyncProfileActions();
        cloudSyncParams.ExternalArgs = relationship.ExternalArgs;
        //Cloud Params
        //cloudSyncParams.RemoteDBConnectionString = relationship.RemoteSite.ConnectionString;
        cloudSyncParams.SyncDirection = Ektron.FileSync.Common.SyncDirection.upload;
        foreach (Profile p in relationship.Profiles)
        {
            if (p.Id == profileId)
            {
                switch (p.Direction)
                {
                    case SyncClientSyncDirection.Bidirectional:
                        cloudSyncParams.SyncDirection = Ektron.FileSync.Common.SyncDirection.bidirectional;
                        break;
                    case SyncClientSyncDirection.Download:
                        cloudSyncParams.SyncDirection = Ektron.FileSync.Common.SyncDirection.download;
                        break;
                }
            }
        }

        //Base Params
        cloudSyncParams.ForcePreInit = false;
        cloudSyncParams.IsDownloadUpload = false;
        cloudSyncParams.RemoteScheduleId = 0;
        cloudSyncParams.SyncBin = false;
        cloudSyncParams.SyncID = relationship.Id.ToString();
        cloudSyncParams.UserId = _commonApi.UserId;
        cloudSyncParams.PreviewMode = false;
        if (relationship.DefaultProfile.SynchronizeDatabase == true)
        {
            cloudSyncParams.SyncDatabase = true;
            cloudSyncParams.SyncAssetLibrary = true;
            cloudSyncParams.SyncAssets = true;
            cloudSyncParams.SyncPrivateAssets = true;
            cloudSyncParams.SyncUploadedImages = true;
            cloudSyncParams.SyncUploadedFiles = true;
        }
        else
        {
            cloudSyncParams.SyncDatabase = false;
            cloudSyncParams.SyncAssetLibrary = false;
            cloudSyncParams.SyncAssets = false;
            cloudSyncParams.SyncPrivateAssets = false;
            cloudSyncParams.SyncUploadedImages = false;
            cloudSyncParams.SyncUploadedFiles = false;
        }

        cloudSyncParams.SyncTemplates = relationship.DefaultProfile.SynchronizeTemplates;

        cloudSyncParams.SyncWorkArea = false;
        cloudSyncParams.ScopeFilter = new EkFileSyncScopeFilter(); // ?
        cloudSyncParams.AssetLibraryPath = relationship.LocalSite.AssetLibraryPath;
        cloudSyncParams.AssetsPath = relationship.LocalSite.AssetsPath;
        cloudSyncParams.LocalSiteConnString = relationship.LocalSite.ConnectionString;// Environment.MachineName;//?
        cloudSyncParams.PrivateAssetsPath = relationship.LocalSite.PrivateAssetsPath;
        cloudSyncParams.SitePath = relationship.LocalSite.SitePath;
        cloudSyncParams.SiteUrl = relationship.LocalSite.ServiceEndpoint;//.SiteUrl;
        cloudSyncParams.SourceServerName = relationship.LocalSite.SiteAddress;
        cloudSyncParams.UploadedFilesPath = relationship.LocalSite.UploadedFilesPath;
        cloudSyncParams.UploadedImagesPath = relationship.LocalSite.UploadedImagesPath;
        cloudSyncParams.WebSitePath = relationship.LocalSite.SitePath;
        cloudSyncParams.WorkareaPath = relationship.LocalSite.WorkareaPath;
        cloudSyncParams.RemoteAssetLibraryPath = relationship.RemoteSite.AssetLibraryPath;
        cloudSyncParams.RemoteAssetsPath = relationship.RemoteSite.AssetsPath;
        cloudSyncParams.RemotePrivateAssetsPath = relationship.RemoteSite.PrivateAssetsPath;
        cloudSyncParams.RemoteScheduleId = 0;
        cloudSyncParams.RemoteSiteConnString = relationship.RemoteSite.ConnectionString;
        cloudSyncParams.RemoteSitePath = relationship.RemoteSite.SitePath;
        cloudSyncParams.RemoteUploadedFilesPath = relationship.RemoteSite.UploadedFilesPath;
        cloudSyncParams.RemoteUploadedImagesPath = relationship.RemoteSite.UploadedImagesPath;
        cloudSyncParams.RemoteUrl = relationship.RemoteSite.ServiceEndpoint;
        cloudSyncParams.RemoteWebSitePath = "";
        cloudSyncParams.RemoteWorkareaPath = "";
        cloudSyncParams.RemoteServerName = "";
        cloudSyncParams.SyncStartTime = DateTime.Now;
        if (relationship.DefaultProfile.Scope.Contains(SyncDBScope.custom))
        {
            cloudSyncParams.DBScope = new List<SyncDBScope>() { SyncDBScope.custom };
            cloudSyncParams.SyncDatabase = true;
            cloudSyncParams.SyncAssetLibrary = false;
            cloudSyncParams.SyncAssets = false;
            cloudSyncParams.SyncPrivateAssets = false;
            cloudSyncParams.SyncUploadedImages = false;
            cloudSyncParams.SyncUploadedFiles = false;
            cloudSyncParams.SyncTemplates = false;
        }
        else
        {
            cloudSyncParams.DBScope = relationship.DefaultProfile.Scope;
            if (cloudSyncParams.DBScope.Count == null || cloudSyncParams.DBScope.Count == 0)
                cloudSyncParams.DBScope = new List<SyncDBScope>() { SyncDBScope.ektron };
        }
        cloudSyncParams.ConflictResolutionPolicy = ConflictResolution.SourceWins;
        return cloudSyncParams;
    }
}
