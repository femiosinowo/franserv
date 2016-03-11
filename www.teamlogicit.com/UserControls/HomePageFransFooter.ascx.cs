using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TeamLogic.CMS;
using System.Xml.Linq;
using System.Xml.XPath;
using Ektron.Cms.Instrumentation;

public partial class UserControls_HomePageFransFooter : System.Web.UI.UserControl
{
    /// <summary>
    /// page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.GetBriefsWhitePapers();

            var blogsData = BlogsDataManager.GetRssFeed(2);
            uxBlogs.DataSource = blogsData;
            uxBlogs.DataBind();
        }
    }


    /// <summary>
    /// This method is used to display white papers & briefs content
    /// </summary>
    private void GetBriefsWhitePapers()
    {
        var briefsWhitePapersData = SiteDataManager.GetBriefsAnsWhitePapersForHomePage();
        if (briefsWhitePapersData != null && briefsWhitePapersData.Any())
        {
            try
            {
                var contentData = from obj in briefsWhitePapersData
                                  select new
                                  {
                                      Id = obj.Id,
                                      Quicklink = obj.Quicklink,
                                      Html = XElement.Parse(obj.Html)
                                  };

                var contentHtml = from obj in contentData
                                  select new
                                  {
                                      Link = "/briefs-whitepapers-details/?sid="+obj.Id,
                                      ImagePath = obj.Html.XPathSelectElement("/imageMM/img") != null ? obj.Html.XPathSelectElement("/imageMM/img").Attribute("src").Value : string.Empty,
                                      ImageTitle = obj.Html.XPathSelectElement("/imageMM/img") != null ? obj.Html.XPathSelectElement("/imageMM/img").Attribute("alt").Value : string.Empty,
                                      Title = obj.Html.XPathSelectElement("/title") != null ? obj.Html.XPathSelectElement("/title").Value : string.Empty,
                                      Teaser = obj.Html.XPathSelectElement("/teaserMM") != null ? obj.Html.XPathSelectElement("/teaserMM").Value : string.Empty
                                  };
                lvBriefsWhitePapers.DataSource = contentHtml;
                lvBriefsWhitePapers.DataBind();
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
        }
    }
}