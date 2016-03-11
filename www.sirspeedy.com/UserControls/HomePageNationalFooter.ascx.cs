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
        cbJoinOurTeam.DefaultContentID = ConfigHelper.GetValueLong("JoinOurTeamContentId");
        cbJoinOurTeam.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbJoinOurTeam.Fill();        
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
            this.GetBriefsWhitePapers();
            this.GetRecentJobs();

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
        if(briefsWhitePapersData != null && briefsWhitePapersData.Any())
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
                                 Link = obj.Quicklink,
                                 ImagePath = obj.Html.XPathSelectElement("/image/img") != null ? obj.Html.XPathSelectElement("/image/img").Attribute("src").Value : string.Empty,
                                 ImageTitle = obj.Html.XPathSelectElement("/image/img") != null ? obj.Html.XPathSelectElement("/image/img").Attribute("alt").Value : string.Empty,
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


    /// <summary>
    /// This method is used to display latest 4 jobs from all locations
    /// </summary>
    private void GetRecentJobs()
    {
        var jobList = FransDataManager.GetAllJobsForNational();
        if (jobList != null && jobList.Count > 0)
        {
            var sortedList = jobList.OrderBy(x => x.DatePosted).Reverse().ToList();;
            var filteredList = sortedList.Take(4);
            lvJobsList.DataSource = filteredList;
            lvJobsList.DataBind();
        }
    }
}