using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using Ektron.Cms.Settings.UrlAliasing.DataObjects;
using Ektron.Cms.Settings.UrlAliasing;
using Ektron.Cms.Common;
using Ektron.Cms.Settings.UrlAliasing.Validation;
using Ektron.Cms.BusinessObjects;
using System.IO;
using Ektron.Cms.Instrumentation;

namespace Ektron.Cms.Settings.UrlAliasing
{
    public class URLAliasingModule : IHttpModule
    {
        private static string LangTypeParam = "langtype";
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
            this.ProcessRequest(HttpContext.Current);
        }

        public void ProcessRequest(HttpContext context)
        {
            var aRequestInfo = RequestInfoProvider.GetRequestInformation();
            if (HttpContext.Current.Request.Url.PathAndQuery.TrimStart('/').ToLower().StartsWith(aRequestInfo.ApplicationPath.ToLower().Trim('/'))) return;

            var aliasSettingsManager = ObjectFactory.GetAliasSettingsManager(aRequestInfo);
            var settings = aliasSettingsManager.Get();

            if (settings.IsAliasingEnabled)
            {
                var aCacheDuration = 5;
                this.TraceMessage("BEGIN Processing URL");

                // Redirect if extensionless and no trailing forward slash. Backwards compatibility with pre 8.5.1.
                if (Path.GetExtension(context.Request.AppRelativeCurrentExecutionFilePath) == string.Empty && !context.Request.AppRelativeCurrentExecutionFilePath.EndsWith("/"))
                {
                    this.TraceMessage("Missing trailing slash, forcing a Redirect.");
                    context.Response.Clear();
                    context.Response.StatusCode = 301;
                    context.Response.Status = "301 Moved Permanently";
                    context.Response.AddHeader("Location", context.Request.Url.LocalPath + "/" + context.Request.Url.Query);
                    context.Response.End();
                }
                var aCacheKey = context.Request.Url.OriginalString + aRequestInfo.ContentLanguage.ToString();
                var aMessageKey = aCacheKey + "messages";
                var aRequestMessages = (List<RequestValidatorCode>)HttpRuntime.Cache[aMessageKey];

                if (aRequestMessages == null)
                {
                IRequestValidator requestValidator = new RequestValidator();
                    aRequestMessages = requestValidator.validate(context.Request.Url);
                    HttpRuntime.Cache.Insert(aMessageKey, aRequestMessages, null, System.DateTime.UtcNow.AddMinutes(aCacheDuration), Cache.NoSlidingExpiration);
                }

                if (aRequestMessages.Count == 0)
                {
                    var aTargetKey = aCacheKey + "target";
                    var target = (AliasData)HttpRuntime.Cache[aTargetKey];
                    if (target == null)
                {
                    var requestManager = ObjectFactory.GetURLRequestManager(aRequestInfo);
                    target = requestManager.GetTarget(context.Request.Url, aRequestInfo.ContentLanguage) ?? new AliasData();
                        HttpRuntime.Cache.Insert(aTargetKey, target, null, System.DateTime.UtcNow.AddMinutes(aCacheDuration), System.Web.Caching.Cache.NoSlidingExpiration);
                    }

                    // Handle Response
                    var aResponseMessageKey = aCacheKey + "responsemessages";
                    var responseMessages = (List<ResponseValidatorCode>)HttpRuntime.Cache[aResponseMessageKey];
                    if (responseMessages == null)
                    {
                    IResponseValidator responseValidator = new ResponseValidator();
                        responseMessages = responseValidator.validate(target);
                        HttpRuntime.Cache.Insert(aResponseMessageKey, responseMessages, null, System.DateTime.UtcNow.AddMinutes(aCacheDuration), System.Web.Caching.Cache.NoSlidingExpiration);
                    }
                    if (responseMessages.Count == 0)
                    {
                        this.processValidResponse(context, target);
                    }
                    else
                    {
                        this.processInvalidResponse(responseMessages, context);
                    }
                }
                else
                {
                    this.processInvalidRequest(aRequestMessages, context);
                }
            }
            else
            {
                // perform look up for default documents.
                processDefaultDocument(context);
            }
        }

        private void processValidResponse(HttpContext context, AliasData target)
        {
            var aRequestInfo = RequestInfoProvider.GetRequestInformation();
            this.TraceMessage("Detected Language: " + aRequestInfo.ContentLanguage);
            this.TraceMessage("Target found, rewrite will be performed: " + target.TargetURL);

            // Languages might be different if alias fallback feature is enabled. 
			//Also if this is regex, there will be no proper language id, it will be 0, so need to make sure it is greater than zero.
            if (target.LanguageId != aRequestInfo.ContentLanguage && !String.IsNullOrEmpty(target.TargetURL) && target.LanguageId > 0)
            {
                this.TraceMessage("Target is in a different language: " + target.LanguageId);

                // If there is a langtype in querystring then do not modify Cookie. User just changed their language.
                // If there is not a language in querystring assume they are new visitor to the site so change their cookie.
                bool modifyUserCookie = (!String.IsNullOrEmpty(context.Request.QueryString[LangTypeParam])) ? false : true;
                if (modifyUserCookie)
                {
                    RequestInformationManager.SetECMCookieValue("SiteLanguage", target.LanguageId.ToString());
                    this.TraceMessage("Usersahh o language updated to match alias");
                }
            }
            this.rewritePath(context, target.TargetURL);
        }

        private void processInvalidResponse(List<ResponseValidatorCode> messages, HttpContext context)
        {
            bool processed = false;
            foreach (ResponseValidatorCode message in messages)
            {
                switch (message)
                {
                    case ResponseValidatorCode.Forbidden:
                        context.Response.StatusCode = 403;
                        context.Response.Status = "403 Forbidden";
                        this.TraceMessage("403 Forbidden");
                        processed = true;
                        break;
                    case ResponseValidatorCode.NoTarget:
                    default:
                        this.TraceMessage("No Target match found");                        
                        break;
                }
            }

            if (!processed)
            {
                this.processDefaultDocument(context);
            }
        }

        private void processInvalidRequest(List<RequestValidatorCode> messages, HttpContext context)
        {
            this.TraceMessage("Request not processed by Aliasing");
            foreach (RequestValidatorCode message in messages)
            {
                switch (message)
                {
                    case RequestValidatorCode.Forbidden:
                        context.Response.StatusCode = 403;
                        context.Response.Status = "403 Forbidden";
                        this.TraceMessage("403 Forbidden");
                        break;
                }
            }
        }

        private void processDefaultDocument(HttpContext context)
        {
            if (System.IO.Path.GetExtension(context.Request.Url.LocalPath) == string.Empty)
            {
                String defaultPage = this.GetDefaultPage(context.Request.PhysicalPath);
                if (!String.IsNullOrEmpty(defaultPage))
                {
                    this.rewritePath(context, context.Request.Url.LocalPath + defaultPage);
                }
            }
        }

        private void rewritePath(HttpContext context, string target)
        {
            context.RewritePath(target, false);
        }

        private string GetDefaultPage(string path)
        {
            string defaultPage = string.Empty;
            char[] seperator = { ',' };
            if (System.Configuration.ConfigurationManager.AppSettings["ek_DefaultPage"] != null)
            {
                string defaultPageSetting = System.Configuration.ConfigurationManager.AppSettings["ek_DefaultPage"];
                string[] defaultPages = defaultPageSetting.Split(seperator);

                //This fixes a bug in IIS7 when the site is installed under the root folder
                if (!path.EndsWith(@"\"))
                {
                    path = path + @"\";
                }

                foreach (string page in defaultPages)
                {
                    string pagePath = path + page;

                    if (System.IO.File.Exists(pagePath))
                    {
                        defaultPage = page;
                        break;
                    }
                }
            }
            return defaultPage;
        }

        private void TraceMessage(string message)
        {
            Log.WriteMessage(String.Format("UrlAliasing: {0} : {1}", HttpContext.Current.Request.Url.PathAndQuery, message), LogLevel.Information, new string[] { "UrlAliasing" });
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
