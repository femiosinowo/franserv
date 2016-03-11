using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Xml.XPath;
using SirSpeedy.CMS;
using Ektron.Cms.Instrumentation;

public partial class UserControls_HomePageNationalFooter : System.Web.UI.UserControl
{

    /// <summary>
    /// Page init event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Init(object sender, EventArgs e)
    {
            
    }

    /// <summary>
    /// page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var blogsData = BlogsDataManager.GetRssFeed(2);
            uxBlogs.DataSource = blogsData;
            uxBlogs.DataBind();
        }
    }
}