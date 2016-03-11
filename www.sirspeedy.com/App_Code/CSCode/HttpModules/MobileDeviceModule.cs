using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ektron.Cms.BusinessObjects;
using Ektron.Cms.Device;
using Ektron.Cms.Common;

namespace Ektron.Cms.Settings.UrlAliasing
{
    public class MobileDeviceModule : IHttpModule
    {
        private IRequestInfoProvider _requestInfoProvider;
        public IRequestInfoProvider RequestInfoProvider
        {
            get
            {
                if (_requestInfoProvider == null)
                    _requestInfoProvider = ObjectFactory.GetRequestInfoProvider();

                return _requestInfoProvider;
            }
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            string targetURL = String.Empty;
            var RequestInfo = RequestInfoProvider.GetRequestInformation();
            var deviceManager = ObjectFactory.GetDeviceConfiguration(RequestInfo);
            targetURL = deviceManager.GetDeviceTemplate(HttpContext.Current.Request.Url.PathAndQuery, RequestInfo.DeviceModel, RequestInfo.DeviceOs, RequestInfo.DeviceType);

            if (!String.IsNullOrEmpty(targetURL))
            {
                HttpContext.Current.RewritePath(targetURL, false);
            }
        }

        #region IHttpModule Members

        public void Dispose()
        {

        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
        }

        #endregion
    }
}