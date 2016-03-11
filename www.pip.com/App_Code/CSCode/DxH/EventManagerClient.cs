using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ektron.DxH.Client
{

    public class EventManagerClient : ServiceClient<Ektron.DxH.Events.IEventManager>, Ektron.DxH.Events.IEventManager
    {

        public EventManagerClient() : base(DxHUtils.ContextBusEndpoint) { }

        public EventManagerClient(string endPointUrl) : base(endPointUrl) { }


        #region IEventManager Members

        public void AssociateWorkflowsToEvent(List<string> WorkflowName, Common.Events.EventDefinition Event)
        {
           
            Action serviceAction = delegate()
            {
                ServiceInstance.AssociateWorkflowsToEvent(WorkflowName, Event);
            };

            InvokeService(serviceAction);
        }

        public void RaiseEvent(Common.Events.EventInstance Event)
        {
            Action serviceAction = delegate()
            {
                ServiceInstance.RaiseEvent(Event);
            };

            InvokeService(serviceAction);
        }

        #endregion
    }

}