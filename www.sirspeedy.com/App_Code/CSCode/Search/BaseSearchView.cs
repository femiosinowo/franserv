using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace SampleSearch.Views
{
    //The Ektron.Cms.WebSearch.SearchData namespace provides a static method EliminateIndexChars that strips 
    //HTML encoding from a string and generates pure text.
    //Please note that as a developer you can create your own search views to customize the information 
    //retrieved by the search provider.
    //If you notice bits of HTML encoding appearing in any columns returned by search view you can use the 
    //EliminateIndexChars method mentioned above to strip the encoded characters from the text. 
    //For example, to remove encoding from the Characterization field generated by the indexing service 
    //you can use Utils.EliminateIndexChars in the code for your custom search view class. 

    /// <summary>
    /// Summary description for BaseSearchView
    /// </summary>
    public abstract class BaseSearchView : DataSourceView
    {
        protected SearchDataSource _owner;
        protected int resultCount;
        public BaseSearchView(SearchDataSource owner, string viewName)
            : base(owner, viewName)
        {
            _owner = owner;
        }
        internal void RaiseChangedEvent()
        {
            OnDataSourceViewChanged(EventArgs.Empty);
        }
    }
}