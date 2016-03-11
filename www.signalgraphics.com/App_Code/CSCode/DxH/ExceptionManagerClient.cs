using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ektron.DxH.Common;
using Ektron.DxH.Common.Exceptions;

namespace Ektron.DxH.Client
{

    public class ExceptionManagerClient : ServiceClient<Ektron.DxH.Exceptions.IExceptionManager>
    {

        public ExceptionManagerClient() : base(DxHUtils.ContextBusEndpoint) { }

        public ExceptionManagerClient(string endPointUrl) : base(endPointUrl) { }




        #region IExceptionManager Members

        public PagingToken<List<ExceptionLogItem>> GetExceptionLogItems(PagingToken<List<ExceptionLogItem>> PageToken, ExceptionLogCriteria FilterCriteria)
        {

            PagingToken<List<ExceptionLogItem>> returnVal = null;

            Action serviceAction = delegate()
            {
                returnVal = ServiceInstance.GetExceptionLogItems(PageToken, FilterCriteria);
            };
            
            InvokeService(serviceAction);
            return returnVal;

        }
        public FilterBounds GetFilterBounds()
        {
            FilterBounds returnVal = null ;
            Action serviceAction = delegate()
            {
                returnVal = ServiceInstance.GetFilterBounds();
            };
            
            InvokeService(serviceAction);
            return returnVal;
        }
        #endregion
    }

}