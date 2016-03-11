using System;
using System.Web;
using Ektron.Cms;
using Ektron.Cms.Common;
using Ektron.Cms.Extensibility;
using Ektron.Cms.Extensibility.Content;
using Ektron.Cms;
using Ektron.Cms.Instrumentation;

namespace TeamLogic.CMS
{
    public class ExpireCacheOnPublish : ContentStrategy
    {
        public override void OnAfterPublishContent(ContentData contentdata, CmsEventArgs eventArgs)
        {
            try
            {
                if (HttpRuntime.Cache["urn:Ektron:ApplicationCache:contentblock:" + contentdata.Id.ToString() + contentdata.LanguageId.ToString() + '0'] != null)
                {
                    HttpRuntime.Cache.Remove("urn:Ektron:ApplicationCache:contentblock:" + contentdata.Id.ToString() + contentdata.LanguageId.ToString() + '0');
                }
                if (HttpRuntime.Cache["urn:Ektron:ApplicationCache:contentblock:" + contentdata.Id.ToString() + contentdata.LanguageId.ToString() + '1'] != null)
                {
                    HttpRuntime.Cache.Remove("urn:Ektron:ApplicationCache:contentblock:" + contentdata.Id.ToString() + contentdata.LanguageId.ToString() + '1');
                }

            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
                Ektron.Cms.EkException.LogException("Cache expiration extension failed for content id:" + contentdata.Id);
            }

            base.OnAfterPublishContent(contentdata, eventArgs);
        }
    }   

}