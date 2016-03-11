namespace Ektron.DxH.Client
{
    using Ektron.Cms.Settings.DxH;
    using Ektron.DxH.Common.Connectors;
    using Ektron.DxH.Common.Contracts;
    using Ektron.DxH.Common.Objects;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;



    /// <summary>
    /// The ConnectionManager Client is used to speak with the Ektron DxH to manage connection information to 
    /// each connector/adapter, such as usernames and passwords for Sharepoint, Ektron, etc...
    /// </summary>
    public class ConnectionManagerClient : ServiceClient<IConnectionManager>
    {

        #region ctors
        public ConnectionManagerClient()
            : base(DxHUtils.ContextBusEndpoint)
        {
        }

        public ConnectionManagerClient(string endpoint)
            : base(endpoint)
        {
        }
        #endregion

        #region IConnectionManager Impl

        #region CRUD
        public IEnumerable<Connection> GetAll()
        {
            var rtnVal = new List<Connection>();

            Action serviceAction = delegate()
            {
                rtnVal = ServiceInstance.GetAll().ToList();
            };

            InvokeService(serviceAction);
            return rtnVal;
        }

        public void Create(Connection newConnection)
        {
            Action serviceAction = delegate()
            {
                ServiceInstance.Create(newConnection);
            };

            InvokeService(serviceAction);

            //DxHConnectionData connection = new DxHConnectionData()
            //{
            //    AdapterName = newConnection.ConnectorName,
            //    ConnectionName = newConnection.Name,
            //    Parameters = newConnection.Parameters           
            //};
            //DxHUtils dxhUtils = new DxHUtils();
            //dxhUtils.SaveDxHConnection(connection);
        }

        public void Update(Connection connection)
        {
            Action serviceAction = delegate()
            {
                ServiceInstance.Update(connection);
            };

            InvokeService(serviceAction);
        }

        public void Delete(Connection connection)
        {
            Action serviceAction = delegate()
            {
                ServiceInstance.Delete(connection);
            };
            InvokeService(serviceAction);
            DxHUtils dxhUtils = new DxHUtils();
            dxhUtils.DeleteDxHConnection(connection.Name, connection.ConnectorName);
        }
        #endregion

       

        public IEnumerable<ConnectionParam> GetConnectionParameterList(string connectorId)
        {
            var rtnVal = new List<ConnectionParam>();
            Action serviceAction = delegate()
            {
                rtnVal = ServiceInstance.GetConnectionParameterList(connectorId).ToList();
            };
            InvokeService(serviceAction);
            return rtnVal;
        }

        public bool TestConnection(List<ConnectionParam> parameters, string connectorId)
        {
            bool rtnVal = false;
            Action serviceAction = delegate()
            {
                rtnVal = ServiceInstance.TestConnection(parameters, connectorId);
            };
            InvokeService(serviceAction);
            return rtnVal;
        }

        public IEnumerable<ConnectionParam> LoadConnection(string connectionName, string connectorId)
        {
            var rtnVal = new List<ConnectionParam>();

            Action serviceAction = delegate()
            {
                rtnVal = ServiceInstance.LoadConnection(connectionName, connectorId).ToList();
            };

            InvokeService(serviceAction);

            return rtnVal;
            
        }


        #endregion
    }
}