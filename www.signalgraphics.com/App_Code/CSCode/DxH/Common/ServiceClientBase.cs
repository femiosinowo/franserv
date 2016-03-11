using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Security.Policy;
using Ektron.Cms.Instrumentation;

namespace Ektron.DxH.Client
{
    /// <summary>
    /// Service client base class
    /// </summary>
    /// <typeparam name="T">related type</typeparam>
    public abstract class ServiceClient<T> : IDisposable
    {
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceFileName"></param>



        protected ServiceClient(string serviceUrl)
        {
            this.ServiceUrl = serviceUrl;
        }


        T _serviceInstance;

        /// <summary>
        /// Gets instance of service interface.
        /// </summary>
        protected T ServiceInstance
        {
            get
            {
                if (_serviceInstance == null)
                {
                    _serviceInstance = ChannelFactory<T>.CreateChannel(Binding, Address);
                }

                return _serviceInstance;
            }
        }

        public string ServiceUrl { get; set; }

        /// <summary>
        /// Common method that closes a service call
        /// </summary>
        public void CloseService()
        {
            if (this._serviceInstance != null && this._serviceInstance is IServiceChannel)
            {
                IServiceChannel srv = this._serviceInstance as IServiceChannel;
                if (srv.State == CommunicationState.Faulted)
                {
                    srv.Abort();
                }
                srv.Close();
            }
            // back to null
            _serviceInstance = default(T);
        }

        /// <summary>
        /// Common method that aborts a service call
        /// </summary>
        protected void AbortService()
        {
            if (this._serviceInstance != null && this._serviceInstance is IServiceChannel)
            {
                IServiceChannel srv = this._serviceInstance as IServiceChannel;
                if (srv.State == CommunicationState.Faulted)
                {
                    srv.Abort();
                }
            }
        }

        /// <summary>
        /// Gets Binding for Service Binding.
        /// </summary>
        protected Binding Binding
        {
            get
            {
                Binding binding = new NetTcpBinding(DxHUtils.TcpBindingName);
                return binding;
            }
        }

        /// <summary>
        /// Gets Endpoint address for service.
        /// </summary> 
        protected virtual EndpointAddress Address
        {
            get { return new EndpointAddress(new Uri(ServiceUrl)); }
        }

        /// <summary>
        /// Use this method for executing methods on the underlying service.  Error handling and retrying is built in.
        /// </summary>
        /// <param name="action">The action containing service calls to execute. </param>
        /// <param name="retry">If true, the call will be retried once in the event of a timeout or communication error.</param>
        protected void InvokeService(Action action, bool retry = true)
        {
            try
            {
                action.Invoke();
            }
            catch (TimeoutException ex)
            {
                AbortService();

                //retry once
                //if (retry) { InvokeService(action, false); }
                Log.WriteError("DxH Timeout Error: " + ex.Message);
                throw new FaultException<TimeoutException>(ex, new FaultReason(ex.Message));
            }
            catch (CommunicationException ex)
            {
                AbortService();

                //retry once
                //if (retry) { InvokeService(action, false); }
                Log.WriteError("DxH Communication Error: " + ex.Message);
                throw new FaultException<CommunicationException>(ex, new FaultReason(ex.Message));
            }
            catch (Exception ex)
            {
                AbortService();

                Log.WriteError("DxH Error: " + ex.Message);
                throw new FaultException<Exception>(ex, ex.Message);
            }

        }


        #region IDisposable Members

        public void Dispose()
        {
            CloseService();
        }

        #endregion
    }
}
