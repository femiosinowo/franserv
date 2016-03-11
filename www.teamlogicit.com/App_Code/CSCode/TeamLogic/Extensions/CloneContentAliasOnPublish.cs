using System;
using System.Web;
using System.Linq;
using Ektron.Cms;
using Ektron.Cms.Common;
using Ektron.Cms.Extensibility;
using Ektron.Cms.Extensibility.Content;
using Ektron.Cms.Instrumentation;
using System.Configuration;
using Ektron.Cms.Settings.UrlAliasing.DataObjects;


namespace TeamLogic.CMS
{
    public class CloneContentAliasOnPublish : ContentStrategy
    {
        public override void OnAfterPublishContent(ContentData contentdata, CmsEventArgs eventArgs)
        {
            try
            {
                if (contentdata.XmlConfiguration != null && contentdata.XmlConfiguration.Id > 0 && IsValidContentType(contentdata.XmlConfiguration.Id))
                {
                    UpdateContentAlias(contentdata.Id, contentdata.Quicklink);
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
                Ektron.Cms.EkException.LogException("CloneContentAlias OnPublish extension failed for content id:" + contentdata.Id);
            }
            base.OnAfterPublishContent(contentdata, eventArgs);
        }

        private bool IsValidContentType(long smartFormId)
        {
            bool status = false;

            long newsSmartFormId = ConfigHelper.GetValueLong("NewsSmartFormID");
            long whitePapersSmartFormId = ConfigHelper.GetValueLong("BriefsAndWhitepapersSmartFormID");
            long caseStudiesSmartFormId = ConfigHelper.GetValueLong("CaseStudiesSmartFormID");
            long projectExpertSmartFormId = ConfigHelper.GetValueLong("ProjectExpertsSmartFormId");
            long itSolutionSmartFormId = ConfigHelper.GetValueLong("ITSolutionSmartFormId");

            if (smartFormId == newsSmartFormId)
                status = true;
            else if (smartFormId == whitePapersSmartFormId)
                status = true;
            else if (smartFormId == caseStudiesSmartFormId)
                status = true;
            else if (smartFormId == projectExpertSmartFormId)
                status = true;
            else if (smartFormId == itSolutionSmartFormId)
                status = true;

            return status;
        }

        private void UpdateContentAlias(long contentId, string aliasName)
        {
            Log.WriteInfo("CloneContentAliasOnPublish::Request came in to ADD alia names for all the local centers with the national alias name " + aliasName + " and for the content id: " + contentId + "");
            AliasCriteria aCriteria = new AliasCriteria();
            aCriteria.AddFilter(AliasProperty.TargetId, CriteriaFilterOperator.EqualTo, contentId);
            aCriteria.PagingInfo = new PagingInfo(FransDataManager.GetCustomApiPageSize());
            var aliasList = AliasHelper.GetList(aCriteria);
            if (aliasList != null && aliasList.Any())
            {
                string contentPrimaryAlias = string.Empty;
                var defaultAliasContent = aliasList.Where(x => x.IsDefault).FirstOrDefault();
                if (defaultAliasContent != null && defaultAliasContent.Id > 0)
                    contentPrimaryAlias = defaultAliasContent.Alias;
                else
                    return;

                long modelSiteFolderId;
                long.TryParse(ConfigurationManager.AppSettings["ModelSiteFolderId"], out modelSiteFolderId);

                long localCentersMainFolderId = ConfigHelper.GetValueLong("LocalCentersMainFolderId");
                var mainFolderData = FolderHelper.GetTree(localCentersMainFolderId);
                if (mainFolderData != null && mainFolderData.ChildFolders != null)
                {
                    if (aliasList.Count < 2)
                    {
                        //add new content alias with ref to all local centers
                        Log.WriteInfo("CloneContentAliasOnPublish:: No local center alias names were NOT found for the nationa alias " + aliasName + " and for the content id: " + contentId + ". Currently working on creating all new aliases");
                        foreach (var f in mainFolderData.ChildFolders)
                        {
                            if (f.Id != modelSiteFolderId)
                            {
                                string centerContentAlias = f.Name + "/" + contentPrimaryAlias;
                                AliasHelper.AddManualAlias(centerContentAlias, contentId, false);
                                Log.WriteInfo("CloneContentAliasOnPublish:: creating new Alias '" + centerContentAlias + "' name for the content id " + contentId + "");
                            }
                        }
                    }
                    else if (aliasList.Count > 1)
                    {
                        //update all 
                        Log.WriteInfo("CloneContentAliasOnPublish:: local center alias names were FOUND for the nationa alias " + aliasName + " and for the content id: " + contentId + ". Currently working on updating all new aliases");

                        foreach (var f in mainFolderData.ChildFolders)
                        {
                            string modifiedAlias = f.Name + "/" + contentPrimaryAlias;
                            if (f.Id != modelSiteFolderId)
                            {
                                var centerSpecificAlias = aliasList.Where(x => x.Alias.ToLower().StartsWith(f.Name.ToLower())).FirstOrDefault();
                                if (centerSpecificAlias != null && centerSpecificAlias.Id > 0)
                                {
                                    if (modifiedAlias != centerSpecificAlias.Alias)
                                    {
                                        AliasHelper.Delete(centerSpecificAlias.Id);
                                        AliasHelper.AddManualAlias(modifiedAlias, contentId, false);
                                        Log.WriteInfo("CloneContentAliasOnPublish::  Alias '" + modifiedAlias + "' name update for the content id " + contentId + "");
                                    }
                                }
                                else
                                {
                                    AliasHelper.AddManualAlias(modifiedAlias, contentId, false);
                                    Log.WriteInfo("CloneContentAliasOnPublish:: creating new Alias '" + modifiedAlias + "' name for the content id " + contentId + "");
                                }
                            }
                        }                        
                    }
                }
            }
        }
    } 

}