using System;
using System.Web;
using System.Linq;

using Ektron.Cms;
using Ektron.Cms.Common;
using Ektron.Cms.Extensibility;
using Ektron.Cms.Extensibility.Content;
using Ektron.Cms;
using Ektron.Cms.Instrumentation;
using System.Configuration;
using Ektron.Cms.Settings.UrlAliasing.DataObjects;

namespace SignalGraphics.CMS
{
    public class UpdateClonedSiteContentOnPublish : ContentStrategy
    {
        public override void OnAfterPublishContent(ContentData contentdata, CmsEventArgs eventArgs)
        {
            try
            {
                long modelSiteFolderId;
                long.TryParse(ConfigurationManager.AppSettings["ModelSiteFolderId"], out modelSiteFolderId);

                Ektron.Cms.ContentAPI cApi = new ContentAPI();                
                var contentData = ContentHelper.GetContentById(contentdata.Id, true);
                if (contentData != null && contentData.Id > 0 && contentData.FolderId == modelSiteFolderId)
                {
                    var contentModelKeyName = SignalGraphicsUtility.GetModelSiteConfigKey(contentData.Id);
                    if (!string.IsNullOrEmpty(contentModelKeyName))
                    {
                        long siteModelConfigMetaId = ConfigHelper.GetValueLong("ModelSiteContentIdKeyMetaDataId");
                        var cc = new Ektron.Cms.Content.ContentMetadataCriteria();
                        cc.AddFilter(siteModelConfigMetaId, CriteriaFilterOperator.EqualTo, contentModelKeyName);
                        cc.PagingInfo = new Ektron.Cms.PagingInfo(FransDataManager.GetCustomApiPageSize());
                        cc.ReturnMetadata = true;
                        var centersContentList = ContentHelper.GetListByCriteria(cc);
                        if (centersContentList != null && centersContentList.Any())
                        {
                            foreach (var c in centersContentList)
                            {
                                var updateCData = c;
                                updateCData.Title = contentdata.Title;
                                updateCData.Html = contentData.Html;
                                updateCData.Teaser = contentData.Teaser;
                                if (string.IsNullOrEmpty(contentData.Image))
                                {
                                    var cData = cApi.GetContentById(c.Id);
                                    if(cData != null)
                                        updateCData.Image = cData.Image;
                                }
                                else
                                {
                                    updateCData.Image = contentData.Image;
                                }
                                                               
                                updateCData.MetaData = new ContentMetaData[contentData.MetaData.Length];
                                for (int i = 0; i < contentData.MetaData.Length; i++)
                                {
                                    if (contentData.MetaData[i].Id != siteModelConfigMetaId)
                                    {
                                        updateCData.MetaData[i] = contentData.MetaData[i];
                                    }
                                    else
                                    {
                                        updateCData.MetaData[i] = new ContentMetaData()
                                        {
                                            Id = siteModelConfigMetaId,
                                            Text = contentModelKeyName
                                        };
                                    }
                                }
                                ContentHelper.UpdateContent(updateCData);
                                Log.WriteInfo("UpdateClonedSiteContentOnPublish:::Cloned model site content with content id: " + updateCData.Id + " updated");

                                if (!contentData.Quicklink.Contains(".aspx"))
                                {
                                    //update default alias name
                                    string currentContentAlias = contentData.Quicklink;
                                    string modifiedAlias = currentContentAlias.ToLower().Replace("model", updateCData.FolderName);
                                    AliasHelper.UpdateManualAlias(modifiedAlias, updateCData.Id, true);

                                    //get all existing alias from model content id
                                    //add all active & non-default alias names
                                    var aliasCriteria = new AliasCriteria();
                                    aliasCriteria.AddFilter(AliasProperty.TargetId, CriteriaFilterOperator.EqualTo, contentData.Id);
                                    aliasCriteria.AddFilter(AliasProperty.Type, CriteriaFilterOperator.EqualTo, EkEnumeration.AliasRuleType.Manual);
                                    aliasCriteria.AddFilter(AliasProperty.IsDefault, CriteriaFilterOperator.EqualTo, false);
                                    var aliasList = AliasHelper.GetList(aliasCriteria);
                                    if (aliasList != null && aliasList.Any())
                                    {
                                        foreach (var a in aliasList)
                                        {
                                            string modifiedActiveAlias = a.Alias.ToLower().Replace("model", updateCData.FolderName);

                                            var newAliasCriteria = new AliasCriteria();
                                            newAliasCriteria.AddFilter(AliasProperty.TargetId, CriteriaFilterOperator.EqualTo, updateCData.Id);
                                            newAliasCriteria.AddFilter(AliasProperty.Alias, CriteriaFilterOperator.EqualTo, modifiedActiveAlias);
                                            newAliasCriteria.AddFilter(AliasProperty.Type, CriteriaFilterOperator.EqualTo, EkEnumeration.AliasRuleType.Manual);
                                            var newAliasList = AliasHelper.GetList(newAliasCriteria);
                                            if(newAliasList == null || newAliasList.Count <= 0)
                                            {
                                                AliasHelper.AddManualAlias(modifiedActiveAlias, updateCData.Id, false);
                                            }                                           
                                        }
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
                Ektron.Cms.EkException.LogException("UpdateClonedSiteContentOnPublish:::Update Cloned Site ContentOnPublish extension failed for content id:" + contentdata.Id);
            }

            base.OnAfterPublishContent(contentdata, eventArgs);
        }
    }   

}