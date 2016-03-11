using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ektron.DxH;
using Ektron.Cms;
using Ektron.Cms.Core;
using Ektron.Cms.Common;
using Ektron.Cms.Framework.Settings.DxH;
using Ektron.Cms.DxH;
using Ektron.Cms.Settings.DxH;
using Ektron.DxH.Common.Objects;
using Ektron.DxH.Common.Events;
using Ektron.DxH.Common.Operations;
using Ektron.DxH.Common.Contracts;
using Ektron.DxH.Common.Connectors;
using Ektron.DxH.Common;
namespace Ektron.DxH.Client
{

    public class ContextBusClient : ServiceClient<IContextBus>
    {


        public ContextBusClient() : base(DxHUtils.ContextBusEndpoint) { }

        public ContextBusClient(string endPointUrl) : base(endPointUrl) { }

        #region IContextBus Members



        public List<ConnectorFlyWeight> GetRegisteredAdapterList()
        {
            List<ConnectorFlyWeight> returnVal = new List<ConnectorFlyWeight>();

            Action serviceAction = delegate()
            {
                returnVal = ServiceInstance.GetRegisteredConnectorList();
            };

            InvokeService(serviceAction);
            return returnVal;
        }

        public List<EventDefinition> GetEventDefinitionList(string adapterName)
        {
            List<EventDefinition> returnVal = new List<EventDefinition>();

            Action serviceAction = delegate()
            {
                returnVal = ServiceInstance.GetEventDefinitionList(adapterName);
            };

            InvokeService(serviceAction);
            return returnVal;
        }

        public List<FlyweightObjectDefinition> GetObjectDefinitionNameList(string adapterName)
        {
            List<FlyweightObjectDefinition> returnVal = new List<FlyweightObjectDefinition>();

            Action serviceAction = delegate()
            {
                returnVal = ServiceInstance.GetObjectDefinitionNameList(adapterName);
            };

            InvokeService(serviceAction);
            return returnVal;
        }

        public List<ObjectDefinition> GetObjectDefinitionList(List<FlyweightObjectDefinition> flyweights, string adapterName)
        {
            List<ObjectDefinition> returnVal = new List<ObjectDefinition>();

            Action serviceAction = delegate()
            {
                returnVal = ServiceInstance.GetObjectDefinitionList(flyweights, adapterName);
            };

            InvokeService(serviceAction);
            return returnVal;
        }

        public void Login(string connectionName, string connectorId)
        {
            Action serviceAction = delegate()
            {
                ServiceInstance.Login(connectionName, connectorId);
            };

            InvokeService(serviceAction);                  
        }

        public void Logout(string adapterName)
        {
            Action serviceAction = delegate()
            {
                ServiceInstance.Logout(adapterName);
            };

            InvokeService(serviceAction);    
        }

        public List<OperationDefinition> GetOperationDefinitionList(string adapterName)
        {
            List<OperationDefinition> returnVal = new List<OperationDefinition>();

            Action serviceAction = delegate()
            {
                returnVal = ServiceInstance.GetOperationDefinitionList(adapterName);
            };

            InvokeService(serviceAction);
            return returnVal;
        }

        public List<Field> InvokeOperation(OperationInstance operation, string adapterName)
        {
            List<Field> returnVal = new List<Field>();

            Action serviceAction = delegate()
            {
                returnVal = ServiceInstance.InvokeOperation(operation, adapterName);
            };

            InvokeService(serviceAction);
            return returnVal;
        }

        public ObjectInstance GetObjectInstance(ObjectDefinition objectDefinition, Dictionary<string, object> keyValues, string adapterName)
        {
            ObjectInstance returnVal = null;

            Action serviceAction = delegate()
            {
                returnVal = ServiceInstance.GetObjectInstance(objectDefinition, keyValues, adapterName);
            };

            InvokeService(serviceAction);
            return returnVal;
        }

        public ObjectInstanceList GetObjectInstanceList(ObjectDefinition objectDefinition, ObjectInstanceCriteriaFilter criteria, string adapterName)
        {
            ObjectInstanceList returnVal = null;

            Action serviceAction = delegate()
            {
                returnVal = ServiceInstance.GetObjectInstanceList(objectDefinition, criteria, adapterName);
            };

            InvokeService(serviceAction);
            
            return returnVal;
        }

        public List<SaveMessage> SaveObjectInstance(ObjectInstance objectInstance, string adapterName)
        {
            List<SaveMessage> returnVal = new List<SaveMessage>();

            Action serviceAction = delegate()
            {
                returnVal = ServiceInstance.SaveObjectInstance(objectInstance, adapterName);
            };

            InvokeService(serviceAction);
            return returnVal;
        }
        
        #endregion

        #region ektron 

        public void SetDxhConnection(string url)
        {
            DxHConnectionData connection = DxHUtils.GetDxHConnection();

            if (connection == null)
            {
                connection = new DxHConnectionData()
                {
                    AdapterName = DxHUtils.DxhConnectionAdapterName,
                    ConnectionName = DxHUtils.DxHConnectionName
                };
            }

            connection.EndPoint = url;
            DxHUtils dxhUtils = new DxHUtils();
            dxhUtils.SaveDxHConnection(connection);

            CloseService();
            ServiceUrl = url;
            DxHUtils.ContextBusEndpoint = url;

        }

        public bool TestDxhConnection(string url)
        {
            bool returnVal = true;
            try
            {
                using (ContextBusClient client = new ContextBusClient(url))
                {
                    List<ConnectorFlyWeight> adapters = client.GetRegisteredAdapterList();
                    returnVal = true;
                }
            }
            catch (Exception)
            {
                returnVal = false;
            }
            return returnVal;
        }

        public FlyweightObjectDefinition GetFlyweightObjectDefinition(string adapterName, string objectDefinitionId)
        {
            FlyweightObjectDefinition retVal = null;
            List<FlyweightObjectDefinition> list = GetObjectDefinitionNameList(adapterName);

            if (list != null && list.Count > 0)
            {
                retVal = list.Find(fly => fly.Id == objectDefinitionId);
            }


            return retVal;
        }

        public ObjectDefinition GetObjectDefinition(string adapterName, string objectDefinitionId)
        {
            ObjectDefinition retVal = null;
            FlyweightObjectDefinition objectDef = null;

            if (adapterName.ToLower() == "sharepoint")
            {
                string displayname = objectDefinitionId.Split('|').LastOrDefault();
                objectDef = new FlyweightObjectDefinition() { Id = objectDefinitionId, DisplayName = displayname };
            }
            else 
            {
                objectDef = GetFlyweightObjectDefinition(adapterName, objectDefinitionId);
            }
           
            if (objectDef != null)
            {
                List<ObjectDefinition> objectList = GetObjectDefinitionList(new List<FlyweightObjectDefinition>(){objectDef}, adapterName);

                if (objectList != null && objectList.Count > 0)
                {
                    retVal = objectList.First();
                }
            }


            return retVal;
        }

        #endregion
    }
}

