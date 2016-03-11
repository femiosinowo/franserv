using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SirSpeedy.CMS;

public partial class UserControls_HomePageJoinOurTeam : System.Web.UI.UserControl
{

    /// <summary>
    /// page init event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Init(object sender, EventArgs e)
    {
        cbJoinOurTeamImg.DefaultContentID = ConfigHelper.GetValueLong("HomePageJoinOurTeamCId");
        cbJoinOurTeamImg.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbJoinOurTeamImg.Fill();        
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
            this.GetRecentJobs();           
        }
    }
   
    /// <summary>
    /// This method is used to display latest 3 jobs from all locations
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