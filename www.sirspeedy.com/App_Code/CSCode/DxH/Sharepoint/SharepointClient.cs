namespace Ektron.DxH.Client.Sharepoint
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Ektron.DxH.Common.Contracts;
    using Ektron.DxH.Common.Connectors;
    using Ektron.DxH.Common.Objects;
    using Ektron.Cms.DxH.Content;
    using Ektron.DxH.Tasks;
    using Ektron.DxH.Common.Operations;
    using Ektron.DxH.Common.Events;
    using DxhWorkflow = Ektron.DxH.Tasks.Workflow;
    using Ektron.DxH.Common;
    using System.Web.Caching;
    using Ektron.Cms.Settings.DxH;
    using Ektron.Cms.Framework.Settings.DxH;
    using Ektron.Cms.Instrumentation;
    using Ektron.Cms;
    using System.Collections.Concurrent;


    /// <summary>
    /// The SharepointClient is used to facilitate the importing of Sharepoint Items surfaced up from the context bus to ektron.
    /// </summary>
    public class SharepointClient
    {
        #region properties

        public SharepointClient()
        {
        }

        private const string AdapterName = "SharePoint";
        private const string EktronHtmlObjectDefName = "HTMLContent";
        private const string BeginLogMessage = "-Begin {0}.{1}";
        private const string FinishLogMessage = "+Finish {0}.{1}";
        private const string CLASS_NAME = "Ektron.DxH.Client.Sharepoint.SharepointClient";
        private string _cachekey = "Ektron.DxH.SharepointClient.{0}.{1}";
        private static readonly object _cacheobject = new object();
        private DateTime _cacheexptime = DateTime.MinValue;

        private ContextBusClient _contextBusClient;

        private ContextBusClient ContextBusClient
        {
            get
            {
                if (_contextBusClient == null)
                {
                    _contextBusClient = new ContextBusClient();
                }

                return _contextBusClient;
            }
        }

        private ConnectionManagerClient _connMgr;

        private ConnectionManagerClient ConnectionManagerClient
        {
            get
            {
                if (_connMgr == null)
                {
                    _connMgr = new ConnectionManagerClient();
                }

                return _connMgr;
            }
        }

        private string ConnectionName
        {
            get { return ""; }
        }

        #endregion

        #region IAdapter Members

        public ObjectDefinition GetObjectDefinition(string objectdefinitionid, string connectionname)
        {
            Log.WriteVerbose(string.Format(BeginLogMessage, CLASS_NAME, "GetObjectDefinition"));
            ObjectDefinition rtnval = null;
            rtnval = HttpContext.Current.Cache[GetCacheKey(objectdefinitionid, connectionname)] as ObjectDefinition;
            if (rtnval == null)
            {
                lock (_cacheobject)
                {
                    rtnval = ContextBusClient.GetObjectDefinition(AdapterName, objectdefinitionid);
                    HttpContext.Current.Cache.Add(GetCacheKey(objectdefinitionid, connectionname), rtnval, null, CacheExpirationDateTime, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);

                }
            }
            Log.WriteVerbose(string.Format(FinishLogMessage, CLASS_NAME, "GetObjectDefinition"));
            return rtnval;
        }



        public ObjectInstanceList GetObjectInstanceList(ObjectDefinition objectDefinition, ObjectInstanceCriteriaFilter criteria)
        {

            return ContextBusClient.GetObjectInstanceList(objectDefinition, criteria, AdapterName);
        }

        public List<ConnectionParam> GetConnectionParameterList()
        {
            return ConnectionManagerClient.GetConnectionParameterList(AdapterName).ToList();
        }


        public List<FlyweightObjectDefinition> GetObjectDefinitionNameList(string connectionname)
        {
            Log.WriteVerbose(string.Format(BeginLogMessage, CLASS_NAME, "GetObjectDefinitionNameList"));
            List<FlyweightObjectDefinition> rtnval = null;
            rtnval = (List<FlyweightObjectDefinition>)HttpContext.Current.Cache[GetCacheKey("", connectionname)];

            if (rtnval == null)
            {
                lock (_cacheobject)
                {
                    rtnval = ContextBusClient.GetObjectDefinitionNameList(AdapterName);
                    HttpContext.Current.Cache.Add(GetCacheKey("", connectionname), rtnval, null, CacheExpirationDateTime, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);

                }
            }
            Log.WriteVerbose(string.Format(FinishLogMessage, CLASS_NAME, "GetObjectDefinitionNameList"));
            return rtnval;
        }

        public List<ObjectDefinition> GetObjectDefinitionList(List<FlyweightObjectDefinition> flyweights)
        {
            return ContextBusClient.GetObjectDefinitionList(flyweights, AdapterName);
        }

        public ObjectInstance GetObjectInstance(ObjectDefinition objectDefinition, Dictionary<string, object> keyValues)
        {
            return ContextBusClient.GetObjectInstance(objectDefinition, keyValues, AdapterName);
        }

        public void Login(string connectionName)
        {
            Log.WriteVerbose(string.Format(BeginLogMessage, CLASS_NAME, "Login(List<ConnectionParam> parameters)"));
            ContextBusClient.Login(connectionName, AdapterName);
            Log.WriteVerbose(string.Format(FinishLogMessage, CLASS_NAME, "Login(List<ConnectionParam> parameters)"));
        }

        public void Logout()
        {
            Log.WriteVerbose(string.Format(BeginLogMessage, CLASS_NAME, "Logout()"));
            ContextBusClient.Logout(AdapterName);
            Log.WriteVerbose(string.Format(FinishLogMessage, CLASS_NAME, "Logout()"));
        }

        public List<SaveMessage> SaveObjectInstance(ObjectInstance objectInstance)
        {
            return ContextBusClient.SaveObjectInstance(objectInstance, AdapterName);
        }

        public bool TestConnection(List<ConnectionParam> parameters)
        {
            return ConnectionManagerClient.TestConnection(parameters, AdapterName);
        }

        #endregion

        public ObjectInstanceList GetListFolders(ObjectDefinition objdefinition, string connectionname)
        {
            Log.WriteVerbose(string.Format(BeginLogMessage, CLASS_NAME, "GetListFolders(ObjectDefinition objdefinition, string connectionname)"));
            ObjectInstanceList rtnval = null;
            rtnval = (ObjectInstanceList)HttpContext.Current.Cache[GetCacheKey(objdefinition.Id + "folderkey", connectionname)];

            if (rtnval == null)
            {
                lock (_cacheobject)
                {
                    ObjectInstanceCriteriaFilter criteriafilter = new ObjectInstanceCriteriaFilter(objdefinition);
                    criteriafilter.AddFilter("FSObjType", ObjectInstanceCriteriaFilterOperator.EqualTo, 1);
                    rtnval = ContextBusClient.GetObjectInstanceList(objdefinition, criteriafilter, AdapterName);
                    HttpContext.Current.Cache.Add(GetCacheKey(objdefinition.Id + "folderkey", connectionname), rtnval, null, CacheExpirationDateTime, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
            }
            Log.WriteVerbose(string.Format(FinishLogMessage, CLASS_NAME, "GetListFolders(ObjectDefinition objdefinition, string connectionname)"));
            return rtnval;
        }

        public ObjectInstanceList GetSubFolders(ObjectDefinition objdefinition, string folderserverrelativeurl, string connectionname)
        {
            Log.WriteVerbose(string.Format(BeginLogMessage, CLASS_NAME, "GetSubFolders(ObjectDefinition objdefinition, string folderserverrelativeurl, string connectionname)"));
            ObjectInstanceList rtnval = null;
            rtnval = (ObjectInstanceList)HttpContext.Current.Cache[GetCacheKey(objdefinition.Id, connectionname) + folderserverrelativeurl];

            if (rtnval == null)
            {
                lock (_cacheobject)
                {
                    ObjectInstanceCriteriaFilter criteriafilter = new ObjectInstanceCriteriaFilter(objdefinition);
                    criteriafilter.AddFilter("FSObjType", ObjectInstanceCriteriaFilterOperator.EqualTo, 1);
                    criteriafilter.AddFilter("FileDirRef", ObjectInstanceCriteriaFilterOperator.EqualTo, folderserverrelativeurl);
                    rtnval = ContextBusClient.GetObjectInstanceList(objdefinition, criteriafilter, AdapterName);
                    HttpContext.Current.Cache.Add(GetCacheKey(objdefinition.Id, connectionname) + folderserverrelativeurl, rtnval, null, CacheExpirationDateTime, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
            }
            Log.WriteVerbose(string.Format(FinishLogMessage, CLASS_NAME, "GetSubFolders(ObjectDefinition objdefinition, string folderserverrelativeurl, string connectionname)"));
            return rtnval;
        }

        public ObjectInstanceList GetListFolderItems(ObjectDefinition objdefinition, PagingInformation paginginfo, string connectionname)
        {
            Log.WriteVerbose(string.Format(BeginLogMessage, CLASS_NAME, "GetListFolderItems(ObjectDefinition objdefinition, PagingInformation paginginfo, string connectionname)"));
            ObjectInstanceList rtnval = null;
            rtnval = (ObjectInstanceList)HttpContext.Current.Cache[GetCacheKey(objdefinition.Id, connectionname) + "," + paginginfo.CurrentPage];

            if (rtnval == null)
            {
                lock (_cacheobject)
                {
                    ObjectInstanceCriteriaFilter criteriafilter = new ObjectInstanceCriteriaFilter(objdefinition);
                    criteriafilter.AddFilter("FSObjType", ObjectInstanceCriteriaFilterOperator.EqualTo, 0);
                    criteriafilter.Paging = paginginfo;
                    rtnval = ContextBusClient.GetObjectInstanceList(objdefinition, criteriafilter, AdapterName);
                    HttpContext.Current.Cache.Add(GetCacheKey(objdefinition.Id, connectionname) + "," + paginginfo.CurrentPage, rtnval, null, CacheExpirationDateTime, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
            }
            Log.WriteVerbose(string.Format(FinishLogMessage, CLASS_NAME, "GetListFolderItems(ObjectDefinition objdefinition, PagingInformation paginginfo, string connectionname)"));
            return rtnval;
        }

        public ObjectInstanceList GetSubFoldersItems(ObjectDefinition objdefinition, PagingInformation paginginfo, string folderserverrelativeurl, string connectionname)
        {
            Log.WriteVerbose(string.Format(BeginLogMessage, CLASS_NAME, "GetSubFoldersItems(ObjectDefinition objdefinition, PagingInformation paginginfo, string folderserverrelativeurl, string connectionname)"));
            ObjectInstanceList rtnval = null;
            rtnval = (ObjectInstanceList)HttpContext.Current.Cache[GetCacheKey(objdefinition.Id, connectionname) + "," + paginginfo.CurrentPage + "," + folderserverrelativeurl];

            if (rtnval == null)
            {
                lock (_cacheobject)
                {
                    ObjectInstanceCriteriaFilter criteriafilter = new ObjectInstanceCriteriaFilter(objdefinition);
                    criteriafilter.Condition = LogicalOperator.And;
                    criteriafilter.AddFilter("FSObjType", ObjectInstanceCriteriaFilterOperator.EqualTo, 0);
                    criteriafilter.AddFilter("FileDirRef", ObjectInstanceCriteriaFilterOperator.EqualTo, folderserverrelativeurl);
                    criteriafilter.Paging = paginginfo;
                    rtnval = ContextBusClient.GetObjectInstanceList(objdefinition, criteriafilter, AdapterName);
                    HttpContext.Current.Cache.Add(GetCacheKey(objdefinition.Id, connectionname) + "," + paginginfo.CurrentPage + "," + folderserverrelativeurl, rtnval, null, CacheExpirationDateTime, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
            }
            Log.WriteVerbose(string.Format(FinishLogMessage, CLASS_NAME, "GetSubFoldersItems(ObjectDefinition objdefinition, PagingInformation paginginfo, string folderserverrelativeurl, string connectionname)"));
            return rtnval;
        }

        public ObjectDefinition GetSmartFormObjectDefinition(EntityItem entity)
        {
            Log.WriteVerbose(string.Format(BeginLogMessage, CLASS_NAME, "GetSmartFormObjectDefinition(EntityItem entity)"));
            ObjectDefinition metaObj = new ObjectDefinition("SmartForm|Sharepoint");
            metaObj.DisplayName = entity.Name;

            foreach (EntityItemProperty property in entity.Properties)
            {
                FieldDefinition field = new FieldDefinition(property.Name, property.DisplayName, new FieldType(typeof(string)), false, false);
            }
            Log.WriteVerbose(string.Format(FinishLogMessage, CLASS_NAME, "GetSmartFormObjectDefinition(EntityItem entity)"));
            return metaObj;
        }

        public EntityItem CreateSmartFormEntityFromObjectDefinition(ObjectDefinition metaObj)
        {
            Log.WriteVerbose(string.Format(BeginLogMessage, CLASS_NAME, "CreateSmartFormEntityFromObjectDefinition(ObjectDefinition metaObj)"));
            EntityItem entity = new EntityItem();

            entity.Name = metaObj.DisplayName;

            foreach (FieldDefinition field in metaObj.Fields)
            {
                EntityItemProperty property = new EntityItemProperty()
                {
                    Name = field.Id,
                    DisplayName = field.DisplayName
                    //Type = field.DataType.
                };
                entity.Properties.Add(property);
            }
            Log.WriteVerbose(string.Format(FinishLogMessage, CLASS_NAME, "CreateSmartFormEntityFromObjectDefinition(ObjectDefinition metaObj)"));
            return entity;
        }

        public ObjectDefinition CreateSmartFormObjectDefinition(ObjectDefinition sharepointMeta)
        {
            return this.CreateSmartFormObjectDefinition(sharepointMeta, 0);
        }

        public ObjectDefinition CreateSmartFormObjectDefinition(ObjectDefinition sharepointMeta, int languageid)
        {
            Log.WriteVerbose(string.Format(BeginLogMessage, CLASS_NAME, "CreateSmartFormObjectDefinition(ObjectDefinition sharepointMeta)"));
            ObjectDefinition smartFormMeta = new ObjectDefinition("SmartForm|Sharepoint|" + sharepointMeta.Id);
            smartFormMeta.DisplayName = sharepointMeta.DisplayName;

            List<FieldDefinition> fields = new List<FieldDefinition>();
            fields.AddRange(sharepointMeta.Fields);
            fields.AddRange(
                this.GetEktronHTMLDefinition(languageid)
                .Fields
                .Where(field =>
                    field.Id.Split('|').FirstOrDefault(split =>
                        split.ToLower().Trim() == "metadata") != null));

            smartFormMeta.Fields = fields;
            Log.WriteVerbose(string.Format(FinishLogMessage, CLASS_NAME, "CreateSmartFormObjectDefinition(ObjectDefinition sharepointMeta)"));
            return smartFormMeta;
        }



        public void UpdateSharePointContent(string sharepointconnectionid, string objectdefinitionid, int sharepointobjectid, long folderid, string workflowname)
        {
            Log.WriteVerbose(string.Format(BeginLogMessage, CLASS_NAME, "UpdateSharePointContent(string sharepointconnectionid, string objectdefinitionid, int sharepointobjectid, long folderid)"));
            EventManagerClient eventClient = new EventManagerClient();

            ObjectInstance eksharepointobjInstance = this.GetFilteredObjectInstanceList(sharepointconnectionid, objectdefinitionid, new List<int> { sharepointobjectid })
                                                        .FirstOrDefault();

            eksharepointobjInstance.Fields.Where(p => p.Id.Equals("DxHSource")).FirstOrDefault().Value = "UpdateEktron";

            Action action = delegate()
            {
                if (eksharepointobjInstance != null)
                {
                    EventInstance itemEvent = new EventInstance()
                    {
                        Id = workflowname,
                        Payload = eksharepointobjInstance
                    };


                    eventClient.RaiseEvent(itemEvent);
                }
            };
            Log.WriteVerbose(string.Format(FinishLogMessage, CLASS_NAME, "UpdateSharePointContent(string sharepointconnectionid, string objectdefinitionid, int sharepointobjectid, long folderid)"));
            action.Invoke();
        }

        public void ImportItemsToFolderWithListDependency(string ektronConnectionId, string sharepointConnectionId, List<MappingTask> MappingTasks, long folderId)
        {
            this.ImportItemsToFolderWithListDependency(ektronConnectionId, sharepointConnectionId, MappingTasks, folderId, false);
        }

        public void ImportItemsToFolderWithListDependency(string ektronConnectionId, string sharepointConnectionId, List<MappingTask> MappingTasks, long folderId, bool isDeleteallowed)
        {
            this.ImportItemsToFolderWithListDependency(ektronConnectionId, sharepointConnectionId, MappingTasks, folderId, false, 1033);
        }

        public void ImportItemsToFolderWithListDependency(string ektronConnectionId, string sharepointConnectionId, List<MappingTask> MappingTasks, long folderId, bool isDeleteallowed, int languageid)
        {
            Log.WriteVerbose(string.Format(BeginLogMessage, CLASS_NAME, "ImportItemsToFolderWithListDependency(string ektronConnectionId, string sharepointConnectionId, string objectdefinitionid, IEnumerable<int> objectsharepointIdList, long folderId)"));
            Action action = delegate()
            {
                foreach (var map in MappingTasks)
                {
                    try
                    {
                        ImportListToFolder(ektronConnectionId, sharepointConnectionId, folderId, map, isDeleteallowed, languageid);
                    }
                    catch (Exception ex)
                    {
                        Log.WriteError(string.Format("{0}.{1} failed: {2}", CLASS_NAME, "ImportListsToFolder", ex.Message));
                        continue;
                    }
                }

            };
            Log.WriteVerbose(string.Format(FinishLogMessage, CLASS_NAME, "ImportItemsToFolderWithListDependency(string ektronConnectionId, string sharepointConnectionId, string objectdefinitionid, IEnumerable<int> objectsharepointIdList, long folderId)"));
            action.Invoke();

        }
        public void ImportItemsToFolderWithOutListDependency(string ektronConnectionId, string sharepointConnectionId, string objectDefinitionId, long folderId, ConcurrentDictionary<int, MappingTask> listIdWithMappingTask)
        {
            this.ImportItemsToFolderWithOutListDependency(ektronConnectionId, sharepointConnectionId, objectDefinitionId, folderId, listIdWithMappingTask, false);
        }
        public void ImportItemsToFolderWithOutListDependency(string ektronConnectionId, string sharepointConnectionId, string objectDefinitionId, long folderId, ConcurrentDictionary<int, MappingTask> listIdWithMappingTask, bool isDeleteallowed)
        {
            this.ImportItemsToFolderWithOutListDependency(ektronConnectionId, sharepointConnectionId, objectDefinitionId, folderId, listIdWithMappingTask, false, 1033);
        }
        public void ImportItemsToFolderWithOutListDependency(string ektronConnectionId, string sharepointConnectionId, string objectDefinitionId, long folderId, ConcurrentDictionary<int, MappingTask> listIdWithMappingTask, bool isDeleteallowed, int languageid)
        {
            Log.WriteVerbose(string.Format(BeginLogMessage, CLASS_NAME, "ImportItemsToFolderWithOutListDependency(string ektronConnectionId, string sharepointConnectionId, string objectDefinitionId, long folderId, IEnumerable<int> objectsharepointIdList)"));
            foreach (var listAndMapping in listIdWithMappingTask)
            {

                List<ObjectInstance> objectinstancelist = this.GetFilteredObjectInstanceList(sharepointConnectionId, listAndMapping.Value.SourceObject, new List<int>() { listAndMapping.Key });

                TaskManagerClient taskClient = new TaskManagerClient();
                EventManagerClient eventClient = new EventManagerClient();

                foreach (ObjectInstance item in objectinstancelist)
                {

                    string id = item.Fields.Single(p => p.Id.ToLower().Equals("id")).Value.ToString();

                    string workflowName = string.Empty;
                    if (isDeleteallowed)
                        workflowName = string.Concat("SharepointDeleteAllowed|Ektron|", objectDefinitionId, "|", folderId, "|ItemDependant|" + id + ";" + languageid);
                    else
                        workflowName = string.Concat("Sharepoint|Ektron|", objectDefinitionId, "|", folderId, "|ItemDependant|" + id + ";" + languageid);



                    //check workflow and delete if it already exists.
                    DxhWorkflow workflow = taskClient.GetWorkflow(workflowName);
                    if (workflow != null)
                    {
                        taskClient.DeleteWorkflow(workflowName);
                    }

                    //1. Create workflow tasks
                    LoginTask login = taskClient.CreateLoginTask("Ektron", ektronConnectionId);
                    SaveObjectInstanceTask saveSharepointToEktron = taskClient.CreateSaveObjectTask("Ektron");
                    LogoutTask logout = taskClient.CreateLogoutTask("Ektron");

                    //2. Assemble and save workflow
                    workflow = taskClient.CreateWorkflow(workflowName);
                    ((ICollection<ContextBusTask>)workflow.Tasks).Add(login);
                    ((ICollection<ContextBusTask>)workflow.Tasks).Add(listAndMapping.Value);
                    ((ICollection<ContextBusTask>)workflow.Tasks).Add(saveSharepointToEktron);
                    ((ICollection<ContextBusTask>)workflow.Tasks).Add(logout);
                    taskClient.SaveWorkflow(workflow);
                    DxHUtils.SaveMapping(AdapterName, sharepointConnectionId, listAndMapping.Value, workflow, Ektron.Cms.Common.EkEnumeration.CMSObjectTypes.Folder, folderId, "", languageid);

                    //3. Create associated Event Defintions for workflow.
                    EventDefinition eventDef = new EventDefinition()
                    {
                        Id = workflowName,
                        Payload = listAndMapping.Value.SourceObject
                    };
                    eventClient.AssociateWorkflowsToEvent(new List<string> { workflow.WorkflowName }, eventDef);

                    if (this.GetListType(objectDefinitionId).ToLower() == "list")
                    {
                        eventDef = new EventDefinition()
                        {
                            Id = string.Concat("SP|List|SPReceiverEvent|", this.GetSiteUrlandListTitle(objectDefinitionId)),
                            Payload = listAndMapping.Value.SourceObject
                        };
                        eventClient.AssociateWorkflowsToEvent(new List<string> { workflow.WorkflowName }, eventDef);

                        eventDef = new EventDefinition()
                        {
                            Id = string.Concat("SP|ListItemAttachment|SPReceiverEvent|", this.GetSiteUrlandListTitle(objectDefinitionId)),
                            Payload = listAndMapping.Value.SourceObject
                        };
                        eventClient.AssociateWorkflowsToEvent(new List<string> { workflow.WorkflowName }, eventDef);
                    }
                    else if (this.GetListType(objectDefinitionId).ToLower() == "documentlibrary")
                    {

                        eventDef = new EventDefinition()
                        {
                            Id = string.Concat("SP|DocumentLibrary|SPReceiverEvent|", this.GetSiteUrlandListTitle(objectDefinitionId)),
                            Payload = listAndMapping.Value.SourceObject
                        };
                        eventClient.AssociateWorkflowsToEvent(new List<string> { workflow.WorkflowName }, eventDef);
                    }

                    if (isDeleteallowed)
                    {
                        string deleteworkflowname = string.Concat("SharepointDelete|Ektron|", objectDefinitionId, "|", folderId, "|ItemDependant|" + id + ";" + languageid);
                        DxhWorkflow deleteworkflow = taskClient.GetWorkflow(deleteworkflowname);
                        if (deleteworkflow != null)
                        {
                            taskClient.DeleteWorkflow(deleteworkflowname);
                        }

                        // Create Delete Workflow Tasks
                        LoginTask deletelogin = taskClient.CreateLoginTask("Ektron", ektronConnectionId);
                        DeleteObjectInstanceTask deleteObjecttask = taskClient.CreateDeleteObjectTask("Ektron");
                        LogoutTask deletelogout = taskClient.CreateLogoutTask("Ektron");

                        //Assemble the Workflow
                        deleteworkflow = taskClient.CreateWorkflow(deleteworkflowname);
                        ((ICollection<ContextBusTask>)deleteworkflow.Tasks).Add(deletelogin);
                        ((ICollection<ContextBusTask>)deleteworkflow.Tasks).Add(listAndMapping.Value);
                        ((ICollection<ContextBusTask>)deleteworkflow.Tasks).Add(deleteObjecttask);
                        ((ICollection<ContextBusTask>)deleteworkflow.Tasks).Add(deletelogout);
                        taskClient.SaveWorkflow(deleteworkflow);

                        eventDef = new EventDefinition()
                        {
                            Id = string.Concat("SPReceiverEvent|Delete|", this.GetSiteUrlandListTitle(objectDefinitionId)),
                            Payload = listAndMapping.Value.SourceObject
                        };
                        eventClient.AssociateWorkflowsToEvent(new List<string> { deleteworkflow.WorkflowName }, eventDef);
                    }

                    item.Fields.Where(p => p.Id.Equals("DxHSource")).FirstOrDefault().Value = "FromEktron";
                    EventInstance itemEvent = new EventInstance()
                    {
                        Id = workflowName,
                        Payload = item
                    };
                    eventClient.RaiseEvent(itemEvent);
                }
                Log.WriteVerbose(string.Format(FinishLogMessage, CLASS_NAME, "ImportItemsToFolderWithOutListDependency(string ektronConnectionId, string sharepointConnectionId, string objectDefinitionId, long folderId, IEnumerable<int> objectsharepointIdList)"));
            }
        }

        public void ImportListsToFolder(string ektronConnectionId, string sharepointConnectionId, long folderId, List<MappingTask> maps)
        {
            this.ImportListsToFolder(ektronConnectionId, sharepointConnectionId, folderId, maps, false);
        }

        public void ImportListsToFolder(string ektronConnectionId, string sharepointConnectionId, long folderId, List<MappingTask> maps, bool isDeleteallowed)
        {
            this.ImportListsToFolder(ektronConnectionId, sharepointConnectionId, folderId, maps, false, 1033);
        }

        public void ImportListsToFolder(string ektronConnectionId, string sharepointConnectionId, long folderId, List<MappingTask> maps, bool isDeleteallowed, int languageid)
        {
            Log.WriteVerbose(string.Format(BeginLogMessage, CLASS_NAME, "ImportListsToFolder(string ektronConnectionId, string sharepointConnectionId, long folderId, List<MappingTask> maps)"));
            Action action = delegate()
            {
                foreach (var map in maps)
                {
                    try
                    {
                        ImportListToFolder(ektronConnectionId, sharepointConnectionId, folderId, map, isDeleteallowed, languageid);
                    }
                    catch (Exception ex)
                    {
                        Log.WriteError(string.Format("{0}.{1} failed: {2}", CLASS_NAME, "ImportListsToFolder", ex.Message));
                        continue;
                    }
                }
            };

            //action.BeginInvoke(null, null);
            action.Invoke();
            Log.WriteVerbose(string.Format(FinishLogMessage, CLASS_NAME, "ImportListsToFolder(string ektronConnectionId, string sharepointConnectionId, IEnumerable<string> objectDefinitionIdList, long folderId)"));
        }

        public void ImportListToFolder(string ektronConnectionId, string sharepointConnectionId, long folderId, MappingTask map)
        {
            this.ImportListToFolder(ektronConnectionId, sharepointConnectionId, folderId, map, false);
        }

        public void ImportListToFolder(string ektronConnectionId, string sharepointConnectionId, long folderId, MappingTask map, bool isDeleteallowed)
        {
            this.ImportListToFolder(ektronConnectionId, sharepointConnectionId, folderId, map, false, 1033);
        }

        public void ImportListToFolder(string ektronConnectionId, string sharepointConnectionId, long folderId, MappingTask map, bool isDeleteallowed, int languageid)
        {
            Log.WriteVerbose(string.Format(BeginLogMessage, CLASS_NAME, "ImportListToFolder(string ektronConnectionId, string sharepointConnectionId, string objectDefinitionId, long folderId, List<ObjectInstance> objectinstancelist, Dictionary<string, string> SharePointFieldToMetaId = null)"));
            TaskManagerClient taskClient = new TaskManagerClient();
            EventManagerClient eventClient = new EventManagerClient();

            string workflowName = string.Empty;
            if (isDeleteallowed)
                workflowName = string.Concat("SharepointDeleteAllowed|Ektron|", map.SourceObject.Id, "|", folderId, "|ListDependant;" + languageid);
            else
                workflowName = string.Concat("Sharepoint|Ektron|", map.SourceObject.Id, "|", folderId, "|ListDependant;" + languageid);

            //check workflow and delete if it already exists.
            DxhWorkflow workflow = taskClient.GetWorkflow(workflowName);
            if (workflow != null)
            {
                taskClient.DeleteWorkflow(workflowName);
            }

            //1. Create workflow tasks
            LoginTask login = taskClient.CreateLoginTask("Ektron", ektronConnectionId);
            SaveObjectInstanceTask saveSharepointToEktron = taskClient.CreateSaveObjectTask("Ektron");
            LogoutTask logout = taskClient.CreateLogoutTask("Ektron");

            //2. Assemble and save workflow
            workflow = taskClient.CreateWorkflow(workflowName);
            ((ICollection<ContextBusTask>)workflow.Tasks).Add(login);
            ((ICollection<ContextBusTask>)workflow.Tasks).Add(map);
            ((ICollection<ContextBusTask>)workflow.Tasks).Add(saveSharepointToEktron);
            ((ICollection<ContextBusTask>)workflow.Tasks).Add(logout);
            taskClient.SaveWorkflow(workflow);
            DxHUtils.SaveMapping(AdapterName, sharepointConnectionId, map, workflow, Ektron.Cms.Common.EkEnumeration.CMSObjectTypes.Folder, folderId, "", languageid);

            //3. Create associated Event Defintions for workflow.
            EventDefinition eventDef = new EventDefinition()
            {
                Id = workflowName,
                Payload = map.SourceObject
            };
            eventClient.AssociateWorkflowsToEvent(new List<string> { workflow.WorkflowName }, eventDef);

            if (this.GetListType(map.SourceObject.Id).ToLower() == "list")
            {
                eventDef = new EventDefinition()
                {
                    Id = string.Concat("SP|List|SPReceiverEvent|", this.GetSiteUrlandListTitle(map.SourceObject.Id)),
                    Payload = map.SourceObject
                };
                eventClient.AssociateWorkflowsToEvent(new List<string> { workflow.WorkflowName }, eventDef);

                eventDef = new EventDefinition()
                {
                    Id = string.Concat("SP|ListItemAttachment|SPReceiverEvent|", this.GetSiteUrlandListTitle(map.SourceObject.Id)),
                    Payload = map.SourceObject
                };
                eventClient.AssociateWorkflowsToEvent(new List<string> { workflow.WorkflowName }, eventDef);
            }
            else if (this.GetListType(map.SourceObject.Id).ToLower() == "documentlibrary")
            {

                eventDef = new EventDefinition()
                {
                    Id = string.Concat("SP|DocumentLibrary|SPReceiverEvent|", this.GetSiteUrlandListTitle(map.SourceObject.Id)),
                    Payload = map.SourceObject
                };
                eventClient.AssociateWorkflowsToEvent(new List<string> { workflow.WorkflowName }, eventDef);
            }

            if (isDeleteallowed)
            {
                string deleteworkflowname = string.Concat("SharepointDelete|Ektron|", map.SourceObject.Id, "|", folderId, "|ListDependant;" + languageid);
                DxhWorkflow deleteworkflow = taskClient.GetWorkflow(deleteworkflowname);
                if (deleteworkflow != null)
                {
                    taskClient.DeleteWorkflow(deleteworkflowname);
                }

                // Create Delete Workflow Tasks
                LoginTask deletelogin = taskClient.CreateLoginTask("Ektron", ektronConnectionId);
                DeleteObjectInstanceTask deleteObjecttask = taskClient.CreateDeleteObjectTask("Ektron");
                LogoutTask deletelogout = taskClient.CreateLogoutTask("Ektron");

                //Assemble the Workflow
                deleteworkflow = taskClient.CreateWorkflow(deleteworkflowname);
                ((ICollection<ContextBusTask>)deleteworkflow.Tasks).Add(deletelogin);
                ((ICollection<ContextBusTask>)deleteworkflow.Tasks).Add(map);
                ((ICollection<ContextBusTask>)deleteworkflow.Tasks).Add(deleteObjecttask);
                ((ICollection<ContextBusTask>)deleteworkflow.Tasks).Add(deletelogout);
                taskClient.SaveWorkflow(deleteworkflow);

                eventDef = new EventDefinition()
                {
                    Id = string.Concat("SPReceiverEvent|Delete|", this.GetSiteUrlandListTitle(map.SourceObject.Id)),
                    Payload = map.SourceObject
                };
                eventClient.AssociateWorkflowsToEvent(new List<string> { deleteworkflow.WorkflowName }, eventDef);
            }

            List<ObjectInstance> spItemList = null;


            //4. Retrieve Sharepoint items and raise event for each one.
            SharePointClientImportHelper.GetInstance().StartImport(sharepointConnectionId, AdapterName, workflowName, map.SourceObject);
            
            Log.WriteVerbose(string.Format(FinishLogMessage, CLASS_NAME, "ImportListToFolder(string ektronConnectionId, string sharepointConnectionId, string objectDefinitionId, long folderId, List<ObjectInstance> objectinstancelist, Dictionary<string, string> SharePointFieldToMetaId = null)"));
        }



        #region private methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objdefinitionid"></param>
        /// <returns></returns>
        private string GetSiteUrlandListTitle(string objdefinitionid)
        {
            string[] splittedstring = objdefinitionid.Split('|');
            if (splittedstring != null && splittedstring.Length > 2)
            {
                return splittedstring[2] + "|" + splittedstring[3];
            }
            return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objdefinitionid"></param>
        /// <returns></returns>
        private string GetListType(string objdefinitionid)
        {
            string[] splittedstring = objdefinitionid.Split('|');
            if (splittedstring != null && splittedstring.Length > 2)
            {
                return splittedstring[1];
            }
            return string.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sharepointconnectionid"></param>
        /// <param name="objdefinitionid"></param>
        /// <param name="objectsharepointidlist"></param>
        /// <returns></returns>
        private List<ObjectInstance> GetFilteredObjectInstanceList(string sharepointconnectionid, ObjectDefinition sharePointDefinition, IEnumerable<int> objectsharepointidlist)
        {
            Log.WriteVerbose(string.Format(BeginLogMessage, CLASS_NAME, "GetFilteredObjectInstanceList(string sharepointconnectionid, ObjectDefinition sharePointDefinition, IEnumerable<int> objectsharepointidlist)"));
            List<ObjectInstance> rtnval = new List<ObjectInstance>();
            //List<ConnectionParam> spParams = ContextBusClient.LoadConnection(sharepointconnectionid, AdapterName);
            Login(sharepointconnectionid);


            foreach (int sharepointid in objectsharepointidlist)
            {
                Dictionary<string, object> filterobject = new Dictionary<string, object>();
                filterobject.Add("ID", sharepointid);
                rtnval.Add(ContextBusClient.GetObjectInstance(sharePointDefinition, filterobject, AdapterName));
            }
            Logout();
            Log.WriteVerbose(string.Format(FinishLogMessage, CLASS_NAME, "GetFilteredObjectInstanceList(string sharepointconnectionid, ObjectDefinition sharePointDefinition, IEnumerable<int> objectsharepointidlist)"));
            return rtnval;
        }

        private List<ObjectInstance> GetFilteredObjectInstanceList(string sharepointconnectionid, string objdefinitionid, IEnumerable<int> objectsharepointidlist)
        {
            Log.WriteVerbose(string.Format(BeginLogMessage, CLASS_NAME, "GetFilteredObjectInstanceList(string sharepointconnectionid, string objdefinitionid, IEnumerable<int> objectsharepointidlist)"));
            List<ObjectInstance> rtnval = new List<ObjectInstance>();
            //List<ConnectionParam> spParams = ContextBusClient.LoadConnection(sharepointconnectionid, AdapterName);
            Login(sharepointconnectionid);

            ObjectDefinition sharepointMeta = ContextBusClient.GetObjectDefinition(AdapterName, objdefinitionid);
            foreach (int sharepointid in objectsharepointidlist)
            {
                Dictionary<string, object> filterobject = new Dictionary<string, object>();
                filterobject.Add("ID", sharepointid);
                rtnval.Add(ContextBusClient.GetObjectInstance(sharepointMeta, filterobject, AdapterName));
            }
            Logout();
            Log.WriteVerbose(string.Format(FinishLogMessage, CLASS_NAME, "GetFilteredObjectInstanceList(string sharepointconnectionid, string objdefinitionid, IEnumerable<int> objectsharepointidlist)"));
            return rtnval;
        }

        /// <summary>
        /// 
        /// </summary>
        private DateTime CacheExpirationDateTime
        {
            get
            {
                if (_cacheexptime == DateTime.MinValue)
                {
                    string cacheinterval = System.Configuration.ConfigurationManager.AppSettings["ek_dxh_sharepoint_cache"].ToString();
                    if (string.IsNullOrEmpty(cacheinterval))
                        _cacheexptime = DateTime.Now.AddSeconds(900);
                    else
                        _cacheexptime = DateTime.Now.AddSeconds(Convert.ToInt32(cacheinterval));

                }
                return _cacheexptime;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectType"></param>
        /// <param name="objectId"></param>
        /// <returns></returns>
        private string GetCacheKey(string objectType, string objectId)
        {
            return string.Format(_cachekey, objectType, objectId);
        }



        private ObjectDefinition GetEktronHTMLDefinition(int languageid)
        {
            Log.WriteVerbose(string.Format(BeginLogMessage, CLASS_NAME, "GetObjectDefinition"));


            ObjectDefinition rtnval = null;
            rtnval = (ObjectDefinition)HttpContext.Current.Cache[GetCacheKey("EktronHTMLContent" + languageid.ToString(), "Ektron")];
            if (rtnval == null)
            {
                ConnectionManagerClient ConnectionManager = new ConnectionManagerClient();
                List<Connection> connections = ConnectionManager.GetAll().ToList();
                Connection Connection = (from connection in connections where connection.ConnectorName.Equals("Ektron") select connection).First();

                ContextBusClient.Login(Connection.Name, Connection.ConnectorName);
                string displayname = "Ektron Html Content";
                if (languageid > 0)
                    displayname = "Ektron Html Content|" + languageid;
                FlyweightObjectDefinition objectDef = new FlyweightObjectDefinition() { Id = "HTMLContent", DisplayName = displayname };
                lock (_cacheobject)
                {
                    rtnval = ContextBusClient.GetObjectDefinitionList(new List<FlyweightObjectDefinition>() { objectDef }, Connection.ConnectorName)[0];
                    HttpContext.Current.Cache.Add(GetCacheKey("EktronHTMLContent" + languageid.ToString(), "Ektron"), rtnval, null, CacheExpirationDateTime, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);

                }
                ContextBusClient.Logout(Connection.ConnectorName);
            }
            Log.WriteVerbose(string.Format(FinishLogMessage, CLASS_NAME, "GetObjectDefinition"));
            return rtnval;
        }
        #endregion

    }

}