using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SirSpeedy.CMS;
using System.Xml;
using Ektron.Cms.Content;
using Ektron.Cms.Common;
using System.Text.RegularExpressions;
using Ektron.Cms.Instrumentation;

public partial class join_our_team : System.Web.UI.Page
{
    /// <summary>
    /// page init event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Init(object sender, EventArgs e)
    {
        cbJoinOurTeamSideContent.DefaultContentID = ConfigHelper.GetValueLong("JoinOurTeamSideContentID");
        cbJoinOurTeamSideContent.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbJoinOurTeamSideContent.Fill();

        cbJoinOurTeamSideQuotes.DefaultContentID = ConfigHelper.GetValueLong("JoinOurTeamSideQuotesContentID");
        cbJoinOurTeamSideQuotes.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbJoinOurTeamSideQuotes.Fill();

        cbJoinOurTeamsMainContent.DefaultContentID = ConfigHelper.GetValueLong("WhyWorkWithUsMainContentId");
        cbJoinOurTeamsMainContent.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbJoinOurTeamsMainContent.Fill();

        cbSliderTagline.DefaultContentID = ConfigHelper.GetValueLong("WhyWorkWithUsSliderTitleContentId");
        cbSliderTagline.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbSliderTagline.Fill();

        cbSliderTeaser.DefaultContentID = ConfigHelper.GetValueLong("WhyWorkWithUsSliderTeaserContentId");
        cbSliderTeaser.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbSliderTeaser.Fill();

        cbFindAJobText.DefaultContentID = ConfigHelper.GetValueLong("JoinOurTeamAStoreLikeNoOtherCId");
        cbFindAJobText.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbFindAJobText.Fill();

        if (cbFindAJobText.EkItem != null && !string.IsNullOrEmpty(cbFindAJobText.EkItem.Image) && cbFindAJobText.EkItem.Image.ToLower().IndexOf("workarea") <= -1)
        {
            findAJobText.Attributes.Add("data-image", cbFindAJobText.EkItem.Image);
            findAJobText.Attributes.Add("data-image-mobile", cbFindAJobText.EkItem.Image);
        }
    }
    
}