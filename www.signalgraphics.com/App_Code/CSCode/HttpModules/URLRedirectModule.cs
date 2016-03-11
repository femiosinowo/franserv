using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Ektron.Cms.Common;
using Ektron.Cms.Instrumentation;
using Ektron.Cms.Settings.UrlAliasing;
using Ektron.Cms.Settings.UrlAliasing.DataObjects;

namespace Ektron.Cms.Settings.UrlAliasing
{
    public class URLRedirectModule : IHttpModule
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
            var RequestInfo = RequestInfoProvider.GetRequestInformation();
            if (HttpContext.Current.Request.Url.PathAndQuery.TrimStart('/').ToLower().StartsWith(RequestInfo.ApplicationPath.ToLower().Trim('/'))) return;

            var aliasSettingsManager = ObjectFactory.GetAliasSettingsManager(RequestInfo);
            if (aliasSettingsManager.Get().IsURLRedirectEnabled)
            {
                this.TraceMessage("BEGIN Processing URL");

                RedirectData target;
                IRedirectManager redirectManager = ObjectFactory.GetRedirectManager(RequestInfo);
                target = redirectManager.GetTarget(HttpContext.Current.Request.Url);

                if (target != null)
                {
                    switch (target.StatusCode)
                    {
                        case HttpStatusCode.Redirect:
                            this.TraceMessage("Target found, 302 redirect will be performed: " + target.TargetURL);
                            HttpContext.Current.Response.Clear();
                            HttpContext.Current.Response.StatusCode = 302;
                            HttpContext.Current.Response.Status = "302 Found";
                            HttpContext.Current.Response.AddHeader("Location", target.TargetURL);
                            HttpContext.Current.Response.End();
                            break;
                        case HttpStatusCode.MovedPermanently:
                            this.TraceMessage("Target found, 301 redirect will be performed: " + target.TargetURL);
                            HttpContext.Current.Response.Clear();
                            HttpContext.Current.Response.StatusCode = 301;
                            HttpContext.Current.Response.Status = "301 Moved Permanently";
                            HttpContext.Current.Response.AddHeader("Location", target.TargetURL);
                            HttpContext.Current.Response.End();
                            break;
                        case HttpStatusCode.NotFound:
                            if (String.IsNullOrEmpty(target.TargetURL))
                            {
                                this.TraceMessage("Target found, 404 exception thrown.");
                                throw new HttpException(404, "File not found");
                            }
                            else
                            {
                                this.TraceMessage("Target found, 404 redirect will be performed: " + target.TargetURL);
                                HttpContext.Current.Items["ekis404redirect"] = true;
                                HttpContext.Current.RewritePath(target.TargetURL, false);
                            }
                            break;
                        case HttpStatusCode.OK:
                            this.TraceMessage("Target found, rewrite will be performed: " + target.TargetURL);
                            HttpContext.Current.RewritePath(target.TargetURL, false);
                            break;
                    }
                }
                else
                { // null indicates no match, return flow to rest of pipeline
                }
            }
        }

        private void TraceMessage(string message)
        {
            Log.WriteMessage(String.Format("UrlRedirect: {0} : {1}", HttpContext.Current.Request.Url.PathAndQuery, message), LogLevel.Information, new string[] { "UrlAliasing" });
        }

        void context_EndRequest(Object sender, EventArgs e)
        {
            if (HttpContext.Current.Items["ekis404redirect"] != null)
            {
                bool is404 = (bool)HttpContext.Current.Items["ekis404redirect"];
                if (is404 == true)
                {
                    HttpContext.Current.Response.StatusCode = 404;
                    HttpContext.Current.Response.Status = "404 Not Found";
                }
            }
        }

        public void Dispose()
        {

        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
            context.EndRequest += new EventHandler(context_EndRequest);
            context.AcquireRequestState += new EventHandler(SessionClear);
        }

        public void SessionClear(object sender, EventArgs e)
        {
            try
            {
                bool LogoutOnSessionAbandon = false;
                bool.TryParse(ConfigurationManager.AppSettings["LogoutOnSessionAbandon"], out LogoutOnSessionAbandon);
                if (HttpContext.Current != null && HttpContext.Current.Session != null && LogoutOnSessionAbandon)
                {
                    if (HttpContext.Current.Session.IsNewSession && HttpContext.Current.Request.Cookies["ecm"] != null)
                    {
                        if (HttpContext.Current.Response.Cookies[System.Web.Security.FormsAuthentication.FormsCookieName] != null)
                        {
                            HttpContext.Current.Response.Cookies[System.Web.Security.FormsAuthentication.FormsCookieName].Expires = DateTime.Now;
                        }
                        HttpContext.Current.Response.Cookies["ecm"].Expires = DateTime.Now;
                        HttpContext.Current.Response.Cookies["ecmSecure"].Expires = DateTime.Now;
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
