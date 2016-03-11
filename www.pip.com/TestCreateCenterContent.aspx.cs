using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SirSpeedy.CMS;
using Ektron.Cms;
using Ektron.Cms.Common;

public partial class TestCreateCenterContent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var allCenters = FransDataManager.GetAllFransLocationDataList(true);
        if (allCenters != null && allCenters.Any())
        {
            Ektron.Cms.Framework.Organization.FolderManager fMngr = new Ektron.Cms.Framework.Organization.FolderManager(Ektron.Cms.Framework.ApiAccessMode.Admin);
            foreach (var c in allCenters)
            {
                var folderCriteria = new FolderCriteria();
                folderCriteria.AddFilter(FolderProperty.FolderName, CriteriaFilterOperator.EqualTo, c.FransId);
                var folderList = fMngr.GetList(folderCriteria);
                if (folderList != null && folderList.Any() == false)
                {
                    long modelSiteFId = ConfigHelper.GetValueLong("ModelCenterFolderId");

                    var modelFolderData = fMngr.GetItem(modelSiteFId);
                    if (modelFolderData != null)
                    {
                        FolderData localFolderData = new FolderData();
                        localFolderData.Name = c.FransId;
                        localFolderData.ParentId = modelFolderData.ParentId;
                        localFolderData.IsTemplateInherited = true;
                        var newFolderData = fMngr.Add(localFolderData);
                        if (newFolderData != null && newFolderData.Id > 0)
                        {
                            //copy all the content from model folder to new folder
                            var cc = new Ektron.Cms.Content.ContentCriteria();
                            cc.AddFilter(ContentProperty.FolderId, CriteriaFilterOperator.EqualTo, modelSiteFId);
                            var contentList = ContentHelper.GetListByCriteria(cc);

                            if (contentList != null)
                            {
                                foreach (var cData in contentList)
                                {
                                    //copy content
                                    var contentId = ContentHelper.CopyEktContent(cData.Id, newFolderData.Id, 1033, true);

                                    //update alias name
                                    if (!cData.Quicklink.Contains(".aspx"))
                                    {
                                        string currentContentAlias = cData.Quicklink;
                                        string modifiedAlias = currentContentAlias.ToLower().Replace("model", newFolderData.Name);
                                        AliasHelper.AddManualAlias(modifiedAlias, contentId);
                                    }

                                    //update metada
                                    var contentModelKeyName = SirSpeedyUtility.GetModelSiteConfigKey(cData.Id);
                                    var contentData = ContentHelper.GetContentById(contentId);
                                    contentData.MetaData = new ContentMetaData[1];
                                    contentData.MetaData[0] = new ContentMetaData()
                                    {
                                        Id = ConfigHelper.GetValueLong("ModelSiteContentIdKeyMetaDataId"),
                                        Text = contentModelKeyName
                                    };
                                    ContentHelper.UpdateContent(contentData);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}