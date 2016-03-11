using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

using Ektron.Cms.Core;
using Ektron.Cms.Common;
using Ektron.Cms;
using Ektron.Cms.Settings.DxH;
using Ektron.Cms.Framework.Settings.DxH;
using Ektron.DxH.Tasks;
using System.Xml.Serialization;
using Ektron.DxH.Common.Objects;
using Ektron.DxH.Common.Events;
using Ektron.Cms.Framework.UI.Controls.EktronUI;

using DxhWorkflow = Ektron.DxH.Tasks.Workflow;
using Ektron.DxH.Common.Contracts;
using System.Collections.Concurrent;
using Ektron.Cms.Instrumentation;

namespace Ektron.DxH.Client
{

    /// <summary>
    /// Class for retreiving constants and configuration parameters for DxH.
    /// </summary>
    public class DxHUtils
    {

        public const string DxhConnectionAdapterName = "DxH";
        public const string DxHConnectionName = "Default";

        static string _tcpBindingName;
        static string _contextBusEndpoint;

        static DxHUtils()
        {
            TcpBindingName = "ContextBusNetTcpBinding";
        }

        public static string TcpBindingName { get { return _tcpBindingName; } set { _tcpBindingName = value; } }
        public static string ContextBusEndpoint
        {
            get
            {
                if (string.IsNullOrEmpty(_contextBusEndpoint))
                {
                    DxHConnectionData connection = GetDxHConnection();
                    if (connection != null)
                    {
                        _contextBusEndpoint = connection.EndPoint;
                    }
                }

                return _contextBusEndpoint;
            }
            set { _contextBusEndpoint = value; }
        }

        public static bool IsDxHActive()
        {
            bool retVal = false;

            if (!string.IsNullOrEmpty(ContextBusEndpoint))
            {
                ContextBusClient client = new ContextBusClient();
                retVal = client.TestDxhConnection(ContextBusEndpoint);

            }

            return retVal;
        }

        public static DxHConnectionData GetDxHConnection(string connectionName = DxHConnectionName, string adapterName = DxhConnectionAdapterName)
        {
            DxHConnectionData connection = null;

            if ((adapterName.ToLower() == DxhConnectionAdapterName.ToLower()) || (DxHConnectionName.ToLower() == connectionName.ToLower()))
            {
                connection = GetDxhUrl();
            }
            else
            {
                connection = GetDxHConnection(connectionName);
            }
            return connection;
        }

        public static DxHConnectionData GetDxhUrl()
        {
            DxHConnectionData dxhConfig = null;
            try
            {
                SiteSetting siteSetting = new SiteSetting();
                SiteSettingData dxhSetting = siteSetting.GetItem((long)EkEnumeration.SiteSetting.DxHConnection);
                if ((dxhSetting != null) && (dxhSetting.Value != ""))
                {
                    StringReader settingsReader = new StringReader(dxhSetting.Value);
                    XmlSerializer xml = new XmlSerializer(typeof(DxHConnectionData));
                    dxhConfig = (DxHConnectionData)xml.Deserialize(settingsReader);
                }
            }
            catch (Exception ex)
            {
            }
            return dxhConfig;
        }

        public void SaveDxHConnection(DxHConnectionData connection)
        {
            if (connection.AdapterName.ToLower() == DxhConnectionAdapterName.ToLower())
            {
                DxHConnectionData existingConnection = GetDxhUrl();
                if (existingConnection == null)
                    AddDxhUrl(connection);
                else
                    UpdateDxhUrl(connection);
            }
            //else
            //{
            //    ConnectionManagerClient DxhClient = new ConnectionManagerClient();
            //    Connection DxhConnection = new Connection()
            //    {
            //        ConnectorName = connection.AdapterName,
            //        Name = connection.ConnectionName
            //    };
            //    Connection existingConnection = GetDxhConnection(connection.ConnectionName,"");
            //    if (existingConnection != null)
            //    {
            //        DxhClient.Create(DxhConnection);
            //    }
            //    else
            //    {
            //        DxhConnection.Id = existingConnection.Id;
            //        DxhConnection.Parameters = existingConnection.Parameters;
            //        DxhConnection.ImageSource = existingConnection.ImageSource;
            //        DxhClient.Update(DxhConnection);
            //    }
            //}
        }

        public Connection GetDxhConnection(string connectionName, string adapterName)
        {
            Connection connection = null;
            ConnectionManagerClient DxhClient = new ConnectionManagerClient();
            List<Connection> DxhConnections = DxhClient.GetAll().ToList();
            // default to using the connection name
            if (!string.IsNullOrEmpty(connectionName))
            {
                connection = DxhConnections.FirstOrDefault(item => item.Name.ToLower() == connectionName.ToLower());
            }
            else if (!string.IsNullOrEmpty(adapterName))
            {
                if (adapterName.ToLower() == DxH.Common.Constants.Adapter.Ektron.ToLower())
                {
                    var settings = new SiteSetting();
                    var dxhInboundSettings = settings.GetItem(EkEnumeration.SiteSetting.DxHInboundConnection.GetHashCode());
                    if (dxhInboundSettings != null && !string.IsNullOrEmpty(dxhInboundSettings.Value))
                    {
                        connection = DxhConnections.FirstOrDefault(item => (item.ConnectorName.ToLower() == adapterName.ToLower() && item.Name.ToLower() == dxhInboundSettings.Value.ToLower()));
                    }
                }
                else
                {
                    connection = DxhConnections.FirstOrDefault(item => item.ConnectorName.ToLower() == adapterName.ToLower());
                }
            }
            return connection;
        }

        public List<DxHConnectionData> GetDxHConnectionList()
        {
            List<DxHConnectionData> connections = new List<DxHConnectionData>();
            try
            {
                ConnectionManagerClient DxhClient = new ConnectionManagerClient();
                List<Connection> DxhConnections = DxhClient.GetAll().ToList();
                var listConnections = new List<DxHConnectionData>();
                listConnections = (from con in DxhConnections
                                   select new DxHConnectionData
                                   {
                                       AdapterName = con.ConnectorName,
                                       ConnectionName = con.Name,
                                       Id = con.Id
                                   }).ToList();

                // Check for Existing Data.              
                if (listConnections.Any())
                {
                    var settings = new SiteSetting();
                    var dxhSetting = settings.GetItem(EkEnumeration.SiteSetting.DxHInboundConnection.GetHashCode());
                    if (dxhSetting != null && !string.IsNullOrEmpty(dxhSetting.Value))
                    {
                        // Display Right Inbound Connection.
                        listConnections.ForEach(
                            connection =>
                            {
                                if (connection.ConnectionName.ToLower() == dxhSetting.Value.ToLower() && connection.AdapterName.ToLower() == Ektron.DxH.Common.Constants.Adapter.Ektron.ToLower())
                                {
                                    connections.Add(connection);
                                }
                                else if (connection.AdapterName.ToLower() != Ektron.DxH.Common.Constants.Adapter.Ektron.ToLower())
                                {
                                    connections.Add(connection);
                                }
                            });
                    }
                    else
                    {
                        // Display Right Connections Info.
                        listConnections.ForEach(
                            connection =>
                            {
                                if (connection.AdapterName.ToLower() != Ektron.DxH.Common.Constants.Adapter.Ektron.ToLower())
                                {
                                    connections.Add(connection);
                                }
                            });
                    }
                }
            }
            catch (Exception ex) { Log.WriteError(ex); }

            return connections;
        }

        private DxHConnectionData AddDxhUrl(DxHConnectionData dxhConnectionData)
        {
            try
            {
                dxhConnectionData.CreateDate = DateTime.Now;
                dxhConnectionData.ModifiedDate = DateTime.Now;
                dxhConnectionData.Id = 0;
                XmlSerializer xml = new XmlSerializer(dxhConnectionData.GetType());
                StringWriter stringWriter = new StringWriter();
                xml.Serialize(stringWriter, dxhConnectionData);
                SiteSetting settings = new SiteSetting();
                SiteSettingData settingData = new SiteSettingData();
                settingData.Id = (long)EkEnumeration.SiteSetting.DxHConnection;
                settingData.SiteId = 0;
                settingData.Value = stringWriter.ToString();
                settings.Add(settingData);
            }
            catch (Exception ex)
            {
            }

            return dxhConnectionData;
        }

        private DxHConnectionData UpdateDxhUrl(DxHConnectionData dxhConnectionData)
        {
            try
            {
                dxhConnectionData.ModifiedDate = DateTime.Now;
                XmlSerializer xml = new XmlSerializer(dxhConnectionData.GetType());
                StringWriter stringWriter = new StringWriter();
                xml.Serialize(stringWriter, dxhConnectionData);
                SiteSetting settings = new SiteSetting();
                SiteSettingData settingData = new SiteSettingData();
                settingData.Id = (long)EkEnumeration.SiteSetting.DxHConnection;
                settingData.SiteId = 0;
                settingData.Value = stringWriter.ToString();
                settings.Update(settingData);
            }
            catch (Exception ex)
            {
            }

            return dxhConnectionData;
        }


        public void DeleteDxHConnection(string connectionName, string adapter)
        {
            if ((adapter.ToLower() == DxhConnectionAdapterName.ToLower()) || (DxHConnectionName.ToLower() == connectionName.ToLower()))
            {
                DeleteDxhUrl();
            }
            else
            {
                ConnectionManagerClient DxhClient = new ConnectionManagerClient();
                Connection connection = GetDxhConnection(connectionName, adapter);
                if (connection != null)
                {
                    DxhClient.Delete(connection);

                    if (adapter.ToLower() == Ektron.DxH.Common.Constants.Adapter.Ektron.ToLower())
                    {
                        // Delete DXH InboundConnection.
                        var settings = new SiteSetting();
                        settings.Delete(EkEnumeration.SiteSetting.DxHInboundConnection.GetHashCode());
                    }
                }
            }
        }

        private void DeleteDxhUrl()
        {
            try
            {
                // this.DxHConnectionDal.Delete(id);
                SiteSetting settings = new SiteSetting();
                settings.Delete((long)EkEnumeration.SiteSetting.DxHConnection);
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// Saves a dxh mapping reference to the CMS database.
        /// </summary>
        /// <param name="adapter">ID of the adapter associated with this mapping.</param>
        /// <param name="connection">ID of connection associated to this mapping.</param>
        /// <param name="mappingTask">Mappingtask to persist</param>
        /// <param name="objectType">The cms object type that is mapped in this mapping.  Not required.</param>
        /// <param name="objectId">The ID of the cms object mapped in this mapping.  Not required.</param>
        /// <returns></returns>
        public static DxHMappingData SaveMapping(string adapter, string connection, MappingTask mappingTask, DxH.Tasks.Workflow workflow = null, EkEnumeration.CMSObjectTypes objectType = EkEnumeration.CMSObjectTypes.Content, long objectId = 0, string mapName = "", int languageid = 0)
        {
            DxHMappingData rtnval = null;
            DxHMappingManager mapManager = new DxHMappingManager();
            if (languageid > 0)
                mapManager.ContentLanguage = languageid;
            DxHMappingCriteria dxhcriteria = new DxHMappingCriteria();
            List<DxHMappingData> mappingdata = null;
            if (workflow != null)
            {
                dxhcriteria.AddFilter(DxhObjectMapProperty.WorkFlowName, CriteriaFilterOperator.EqualTo, workflow.WorkflowName);
                mappingdata = mapManager.GetList(dxhcriteria);
            }
            if (workflow == null)
            {
                dxhcriteria = new DxHMappingCriteria();
                dxhcriteria.AddFilter(DxhObjectMapProperty.SourceObjectDefinitionId, CriteriaFilterOperator.EqualTo, mappingTask.SourceObject.Id);

                mappingdata = mapManager.GetList(dxhcriteria);

            }

            long dxhmapid = 0;
            if (mappingdata == null || mappingdata.Count == 0)
            {
                DxHMappingData mapData = new DxHMappingData()
                {
                    Adapter = adapter,
                    TargetObjectDefinitionId = mappingTask.TargetObject.Id,
                    TargetObjectDefinition = SerializeObject<ObjectDefinition>(mappingTask.TargetObject),
                    Connection = connection,
                    SourceObjectDefinitionId = mappingTask.SourceObject.Id,
                    SourceObjectDefinition = SerializeObject<ObjectDefinition>(mappingTask.SourceObject),
                    MappingTaskId = mappingTask.TaskID,
                    Title = mapName
                };

                if (workflow != null)
                {
                    mapData.WorkflowName = workflow.WorkflowName;
                }

                mapManager.Add(mapData);
                dxhmapid = mapData.Id;
                rtnval = mapData;
            }
            else
            {
                var map = mappingdata.First();
                map.Adapter = adapter;
                map.TargetObjectDefinitionId = mappingTask.TargetObject.Id;
                map.TargetObjectDefinition = SerializeObject<ObjectDefinition>(mappingTask.TargetObject);
                map.Connection = connection;
                map.SourceObjectDefinitionId = mappingTask.SourceObject.Id;
                map.SourceObjectDefinition = SerializeObject<ObjectDefinition>(mappingTask.SourceObject);
                map.MappingTaskId = mappingTask.TaskID;
                map.Title = mapName;

                mapManager.Update(map);
                dxhmapid = mappingdata.First().Id;
                rtnval = mappingdata.First();
            }

            DxHCmsMappingData cmsMap = null;
            //if we have a cms mapping, save it
            if (objectType == EkEnumeration.CMSObjectTypes.Content && objectId != 0)
            {
                DxHCmsMappingCriteria cmsMapCriteria = new DxHCmsMappingCriteria();
                cmsMapCriteria.AddFilter(Cms.Settings.DxH.DxhCmsObjectMapProperty.MappingId, CriteriaFilterOperator.EqualTo, dxhmapid);
                cmsMapCriteria.AddFilter(Cms.Settings.DxH.DxhCmsObjectMapProperty.ExternalObjectId, CriteriaFilterOperator.EqualTo, "");
                cmsMapCriteria.AddFilter(Cms.Settings.DxH.DxhCmsObjectMapProperty.ObjectType, CriteriaFilterOperator.EqualTo, (int)EkEnumeration.CMSObjectTypes.Content);
                cmsMapCriteria.AddFilter(Cms.Settings.DxH.DxhCmsObjectMapProperty.ObjectId, CriteriaFilterOperator.EqualTo, objectId);
                List<DxHCmsMappingData> newcmaplist = mapManager.GetCmsMappingList(cmsMapCriteria);
                if (newcmaplist == null || newcmaplist.Count == 0)
                {
                    cmsMap = new DxHCmsMappingData()
                    {
                        ObjectType = objectType,
                        ObjectId = objectId,
                        MappingId = dxhmapid
                    };

                    if (mapManager.ContentLanguage > 0)
                    {
                        cmsMap.LanguageId = mapManager.ContentLanguage;
                    }

                    mapManager.SaveCmsMappings(cmsMap);
                }



            }
            else
            {
                cmsMap = new DxHCmsMappingData()
                {
                    ObjectType = objectType,
                    ObjectId = objectId,
                    MappingId = dxhmapid
                };

                if (mapManager.ContentLanguage > 0)
                {
                    cmsMap.LanguageId = mapManager.ContentLanguage;
                }

                mapManager.SaveCmsMappings(cmsMap);
            }

            return rtnval;
        }

        public static void SetLeadIdForUser(string ekAdapterName, string ekConnName, string crmAdapterName, string crmConnName, MappingTask map, long formId, int languageId)
        {
            TaskManagerClient taskClient = new TaskManagerClient();
            EventManagerClient eventClient = new EventManagerClient();

            string eventId = string.Format("Form|{0}|{1}_OnSubmit.event", formId, languageId);
            string workflowName = string.Format("Ektron|Form|{0}|{1}|OnSubmit", formId, languageId);

            //check workflow and delete if it already exists.
            DxhWorkflow workflow = taskClient.GetWorkflow(workflowName);
            if (workflow != null)
            {
                taskClient.DeleteWorkflow(workflowName);
            }

            //1. Create workflow tasks
            LoginTask crmLogin = taskClient.CreateLoginTask(crmAdapterName, crmConnName);
            SaveObjectInstanceTask crmSave = taskClient.CreateSaveObjectTask(crmAdapterName);
            List<string> propList = new List<string>()
                {
                    "dxhvisitorguid",
                    "CurrentConnection",
                    "SavedObjectInstanceKey0",
                    "ObjectType",                  
                    "ConnectorName"
                };
            GetPropertyBagItemsTask propBag = taskClient.CreateGetPropertyBagItemsTask(propList.ToArray());
            LogoutTask crmLogout = taskClient.CreateLogoutTask(crmAdapterName);

            LoginTask ekLogin = taskClient.CreateLoginTask(ekAdapterName, ekConnName);
            InvokeOperationTask ekInvoke = taskClient.CreateInvokeOperationTask(ekAdapterName, "SetLeadIdForUser");
            LogoutTask ekLogout = taskClient.CreateLogoutTask(ekAdapterName);

            ////2. Assemble and save workflow
            workflow = taskClient.CreateWorkflow(workflowName);
            {   // crm
                ((ICollection<ContextBusTask>)workflow.Tasks).Add(crmLogin);
                ((ICollection<ContextBusTask>)workflow.Tasks).Add(map);
                ((ICollection<ContextBusTask>)workflow.Tasks).Add(crmSave);
                ((ICollection<ContextBusTask>)workflow.Tasks).Add(propBag);
                ((ICollection<ContextBusTask>)workflow.Tasks).Add(crmLogout);
                // ektron
                ((ICollection<ContextBusTask>)workflow.Tasks).Add(ekLogin);
                ((ICollection<ContextBusTask>)workflow.Tasks).Add(ekInvoke);
                ((ICollection<ContextBusTask>)workflow.Tasks).Add(ekLogout);
            }
            Dictionary<string, Field> propPresets = new Dictionary<string, Field>();
            FieldType fldType = new FieldType(typeof(string));
            FieldDefinition visitorGuidDef = new FieldDefinition("dxhvisitorguid", "dxhvisitorguid", fldType, false, false);

            Field visitorGuid = new Field(visitorGuidDef);
            visitorGuid.DisplayName = visitorGuid.Id = "dxhvisitorguid";
            visitorGuid.Value = Guid.NewGuid().ToString();

            propPresets.Add("dxhvisitorguid", visitorGuid);
            // workflow.Input = submittedForm;
            taskClient.SaveWorkflow(workflow);
            taskClient.CloseService();

            // save workflow name to the CMS database
            DxHMappingData mapData = DxHUtils.GetMapping(EkEnumeration.CMSObjectTypes.Content, formId, languageId);
            mapData.WorkflowName = workflowName;
            DxHMappingManager mapManager = new DxHMappingManager();
            if (null == mapData)
            {
                mapManager.Add(mapData);
            }
            else
            {
                mapManager.Update(mapData);
            }

            EventDefinition formSubmitEvent = new EventDefinition()
            {
                Id = eventId,
                Payload = map.SourceObject
            };

            ////3. save workflow.
            eventClient.AssociateWorkflowsToEvent(new List<string> { workflow.WorkflowName }, formSubmitEvent);
            eventClient.CloseService();
        }


        public static List<WorkflowExecutionMessage> TestFormSubmit(string crmAdapterName, string crmConnectionName, MappingTask map, ObjectInstance testObject)
        {
            TaskManagerClient taskClient = new TaskManagerClient();
            EventManagerClient eventClient = new EventManagerClient();

            string workflowName = string.Format("Ektron|Form|{0}|TestOnSubmit", testObject.Id);

            //1. Create workflow tasks
            LoginTask crmLogin = taskClient.CreateLoginTask(crmAdapterName, crmConnectionName);
            SaveObjectInstanceTask crmSave = taskClient.CreateSaveObjectTask(crmAdapterName);
            LogoutTask crmLogout = taskClient.CreateLogoutTask(crmAdapterName);


            ////2. Assemble and save workflow
            DxhWorkflow workflow = taskClient.CreateWorkflow(workflowName);
            {   // crm
                ((ICollection<ContextBusTask>)workflow.Tasks).Add(crmLogin);
                ((ICollection<ContextBusTask>)workflow.Tasks).Add(map);
                ((ICollection<ContextBusTask>)workflow.Tasks).Add(crmSave);
                ((ICollection<ContextBusTask>)workflow.Tasks).Add(crmLogout);
            }


            workflow.Input = testObject;
            List<WorkflowExecutionMessage> results = taskClient.ExecuteWorkflow(workflow, new Dictionary<string, Field>());
            taskClient.CloseService();

            return results;

        }

        public static DxHMappingData GetMapping(EkEnumeration.CMSObjectTypes objectType, long objectId, int languageId)
        {
            DxHMappingManager mapManager = new DxHMappingManager();
            DxHMappingData mapData = new DxHMappingData();

            DxHCmsMappingCriteria criteria = new DxHCmsMappingCriteria();
            criteria.AddFilter(DxhCmsObjectMapProperty.ObjectType, CriteriaFilterOperator.EqualTo, (int)objectType);
            criteria.AddFilter(DxhCmsObjectMapProperty.ObjectId, CriteriaFilterOperator.EqualTo, objectId);

            criteria.AddFilter(DxhCmsObjectMapProperty.LanguageId, CriteriaFilterOperator.EqualTo, languageId);

            DxHCmsMappingData cmsMap = mapManager.GetCmsMappingList(criteria).FirstOrDefault();

            if (cmsMap != null)
            {
                mapData = mapManager.GetItem(cmsMap.MappingId);
            }


            return mapData;
        }

        public static void DeleteMapping(long id)
        {
            DxHMappingManager mapManager = new DxHMappingManager();
            DxHMappingData mapData = mapManager.GetItem(id);

            if (mapData != null)
            {
                TaskManagerClient tm = new TaskManagerClient();
                tm.DeleteWorkflow(mapData.WorkflowName);

                mapManager.Delete(id);
            }

        }

        public static string SerializeObject<T>(T obj)
        {
            string xml = string.Empty;
            System.Runtime.Serialization.DataContractSerializer s = new System.Runtime.Serialization.DataContractSerializer(typeof(T));

            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                System.Xml.XmlDictionaryWriter w = System.Xml.XmlDictionaryWriter.CreateTextWriter(stream, System.Text.Encoding.UTF8);
                s.WriteObject(w, obj);
                w.Flush();
                xml = System.Text.Encoding.UTF8.GetString(stream.GetBuffer(), 0, (int)stream.Length);
            }

            return xml;
        }


        public static T DeserializeObject<T>(string xml)
        {
            System.Runtime.Serialization.DataContractSerializer s = new System.Runtime.Serialization.DataContractSerializer(typeof(T));

            byte[] b = System.Text.Encoding.UTF8.GetBytes(xml);
            T obj = default(T);
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(b))
            {
                obj = (T)s.ReadObject(stream);
            }

            return obj;
        }
    }

    public static class Extensions
    {
        public static List<TreeItem> GetTreeList(this List<FlyweightObjectDefinition> flyweights)
        {
            List<TreeItem> treeList = new List<TreeItem>();

            flyweights.ForEach(f => treeList.Add(f.GetTreeItem()));

            return treeList;
        }

        public static TreeItem GetTreeItem(this FlyweightObjectDefinition flyweight)
        {
            TreeItem item = new TreeItem()
            {
                Id = flyweight.Id,
                Text = flyweight.DisplayName
            };

            return item;
        }

        public static List<TreeItem> GetTreeList(this List<ObjectInstance> objectList)
        {
            List<TreeItem> treeList = new List<TreeItem>();

            objectList.ForEach(o => treeList.Add(o.GetTreeItem()));

            return treeList;
        }

        public static TreeItem GetTreeItem(this ObjectInstance objInstance)
        {
            TreeItem item = new TreeItem()
            {
                Id = objInstance.Id,
                Text = objInstance.DisplayName
            };

            return item;
        }
    }

}