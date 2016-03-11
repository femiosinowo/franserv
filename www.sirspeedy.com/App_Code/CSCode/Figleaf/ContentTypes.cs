using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ContentTypes
/// </summary>

namespace Webinar.ContentTypes
{
    using ContentManager = Ektron.Cms.Framework.Core.Content.Content;
    using Ektron.Cms;
    using Ektron.Cms.Common;
    using Ektron.Cms.Framework;

    public class ContentType<T>
    {
        public ContentData Content { get; set; }
        public T SmartForm { get; set; }
    }

    public class ContentTypeManager<T>
    {
        public ApiAccessMode AccessMode = ApiAccessMode.Admin;

        public void Update(ContentType<T> contentType)
        {
            Initialize();
            contentType.Content.Html = Ektron.Cms.EkXml.Serialize(typeof(T), contentType.SmartForm);
            contentManager.Update(contentType.Content);
        }

        public void Delete(ContentType<T> contentType)
        {
            Initialize();
            contentManager.Delete(contentType.Content.Id);
        }

        public ContentType<T> GetItem(long id)
        {
            Initialize();

            ContentData contentItem = contentManager.GetItem(id);

            return Make(contentItem);
        }

        public List<ContentType<T>> GetList(Criteria<ContentProperty> criteria)
        {
            Initialize();

            List<ContentData> contentList = contentManager.GetList(criteria);

            return MakeList(contentList);
        }

        private ContentType<T> Make(ContentData contentItem)
        {
            T smartForm = (T) Ektron.Cms.EkXml.Deserialize(typeof(T), contentItem.Html);

            ContentType<T> contentType = new ContentType<T>();
            contentType.SmartForm = smartForm;
            contentType.Content = contentItem;

            return contentType;
        }

        private List<ContentType<T>> MakeList(List<ContentData> contentList)
        {
            List<ContentType<T>> list = new List<ContentType<T>>();
            foreach (ContentData contentItem in contentList)
            {
                ContentType<T> contentType = Make(contentItem);
                list.Add(contentType);
            }

            return list;
        }
      
        private void Initialize()
        {
            if (contentManager == null)
            {
                contentManager = new ContentManager(AccessMode);
            }
        }

        private ContentManager contentManager;
        private Ektron.Cms.Framework.ApiAccessMode accessMode;
    }
}