using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ektron.DxH;
using Ektron.Cms;
using Ektron.Cms.Core;
using Ektron.Cms.Common;
using Ektron.Cms.Framework.Settings;
using Ektron.Cms.Settings;
using Ektron.DxH.Tasks;
using Ektron.DxH.Common.Objects;
using Ektron.DxH.Common.Contracts;

namespace Ektron.DxH.Client
{

    public class TaskManagerClient : ServiceClient<Ektron.DxH.Tasks.ITaskManager>
    {


        public TaskManagerClient() : base(DxHUtils.ContextBusEndpoint) { }

        public TaskManagerClient(string endPointUrl) : base(endPointUrl) { }

        #region ITaskManager Members

        public Tasks.InvokeOperationTask CreateInvokeOperationTask(string adapterName, string operationId)
        {
            Tasks.InvokeOperationTask returnVal = null;

            Action serviceAction = delegate()
            {
                returnVal = ServiceInstance.CreateInvokeOperationTask(adapterName, operationId);
            };

            InvokeService(serviceAction);
            return returnVal;
        }

        public Tasks.GetPropertyBagItemsTask CreateGetPropertyBagItemsTask(string[] propList)
        {
            Tasks.GetPropertyBagItemsTask returnVal = null;

            Action serviceAction = delegate()
            {
                returnVal = ServiceInstance.CreateGetPropertyBagItemsTask(propList);
            };

            InvokeService(serviceAction);
            return returnVal;
        }

        public Tasks.GetObjectDefinitionTask CreateGetObjectDefinitionTask(string adapterName, string objectId)
        {
            Tasks.GetObjectDefinitionTask returnVal = null;

            Action serviceAction = delegate()
            {
                returnVal = ServiceInstance.CreateGetObjectDefinitionTask(adapterName, objectId);
            };

            InvokeService(serviceAction);
            return returnVal;
        }

        public GetObjectInstanceTask CreateGetObjectInstanceTask(string adapterName, ObjectDefinition objectDefinition)
        {
 	        throw new NotImplementedException();
        }

        public Tasks.LoginTask  CreateLoginTask(string adapterName, string connectionName)
        {
            Tasks.LoginTask returnVal = null;

            Action serviceAction = delegate()
            {
                returnVal = ServiceInstance.CreateLoginTask(adapterName, connectionName);
            };

            InvokeService(serviceAction);
            return returnVal;
        }

        public Tasks.LogoutTask  CreateLogoutTask(string adapterName)
        {
            Tasks.LogoutTask returnVal = null;

            Action serviceAction = delegate()
            {
                returnVal = ServiceInstance.CreateLogoutTask(adapterName);
            };

            InvokeService(serviceAction);
            return returnVal;
        }

        public Tasks.MappingTask  CreateMappingTask(ObjectDefinition sourceObject, ObjectDefinition targetObject)
        {
            MappingTask returnVal = null;

            Action serviceAction = delegate()
            {
                returnVal = ServiceInstance.CreateMappingTask(sourceObject, targetObject);
            };

            InvokeService(serviceAction);
            return returnVal;
        }

        public SaveObjectInstanceTask CreateSaveObjectTask(string adapterName)
        {
            SaveObjectInstanceTask returnVal = null;

            Action serviceAction = delegate()
            {
                returnVal = ServiceInstance.CreateSaveObjectTask(adapterName);
            };

            InvokeService(serviceAction);
            return returnVal;
        }
		
		public DeleteObjectInstanceTask CreateDeleteObjectTask(string adapterName)
        {
            DeleteObjectInstanceTask returnVal = null;

            Action serviceAction = delegate()
            {
                returnVal = ServiceInstance.CreateDeleteObjectTask(adapterName);
            };

            InvokeService(serviceAction);
            return returnVal;
        }

        public Tasks.Workflow  CreateWorkflow(string name)
        {
            Tasks.Workflow returnVal = null;

            Action serviceAction = delegate()
            {
                returnVal = ServiceInstance.CreateWorkflow(name);
            };

            InvokeService(serviceAction);
            return returnVal;
        }

        public List<WorkflowExecutionMessage>  ExecuteWorkflow(Tasks.Workflow task, Dictionary<string, Field> propertyBagPresets)
        {
            List<WorkflowExecutionMessage> returnVal = null;

            Action serviceAction = delegate()
            {
                returnVal = ServiceInstance.ExecuteWorkflow(task, propertyBagPresets);
            };

            InvokeService(serviceAction);
            return returnVal;
        }

        public List<string>  GetAllWorkflowNames()
        {
            List<string> returnVal = null;

            Action serviceAction = delegate()
            {
                returnVal = ServiceInstance.GetAllWorkflowNames();
            };

            InvokeService(serviceAction);
            return returnVal;
        }

        public Tasks.ContextBusTask  GetTask(Guid TaskID)
        {
            Tasks.ContextBusTask returnVal = null;

            Action serviceAction = delegate()
            {
                returnVal = ServiceInstance.GetTask(TaskID);
            };

            InvokeService(serviceAction);
            return returnVal;
        }

        public Tasks.Workflow  GetWorkflow(string WorkflowName)
        {
            Tasks.Workflow returnVal = null;

            Action serviceAction = delegate()
            {
                returnVal = ServiceInstance.GetWorkflow(WorkflowName);
            };

            InvokeService(serviceAction);
            return returnVal;
        }

        public void DeleteWorkflow(string workflowName)
        {

            Action serviceAction = delegate()
            {
                ServiceInstance.DeleteWorkflow(workflowName);
            };

            InvokeService(serviceAction);
        }

        public Guid  SaveTask(Tasks.ContextBusTask task)
        {
            Guid returnVal = Guid.Empty;

            Action serviceAction = delegate()
            {
                returnVal = ServiceInstance.SaveTask(task);
            };

            InvokeService(serviceAction);
            return returnVal;
        }

        public void  SaveWorkflow(Tasks.Workflow Workflow)
        {
            Action serviceAction = delegate()
            {
                ServiceInstance.SaveWorkflow(Workflow);
            };

            InvokeService(serviceAction);
        }

        public List<ValidationMessage>  ValidateTask(Tasks.ContextBusTask Task)
        {
            List<ValidationMessage> returnVal = new List<ValidationMessage>();
            Action serviceAction = delegate()
            {
                returnVal = ServiceInstance.ValidateTask(Task);
            };

            InvokeService(serviceAction);

            return returnVal;
        }

        #endregion
    }
}

