using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SirSpeedy.CMS;

public partial class UserControls_HomePageMaps : System.Web.UI.UserControl
{
    /// <summary>
    /// Page Init event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Init(object sender, EventArgs e)
    {
        cbWorlMaps.DefaultContentID = ConfigHelper.GetValueLong("worldMapsContentId");
        cbWorlMaps.CacheInterval = ConfigHelper.GetValueLong("smallCacheInterval");
        cbWorlMaps.Fill();

        cbMapsDescp.DefaultContentID = ConfigHelper.GetValueLong("worldMapsDescpContentId");
        cbMapsDescp.CacheInterval = ConfigHelper.GetValueLong("smallCacheInterval");
        cbMapsDescp.Fill();
    }
    
    /// <summary>
    /// Page load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }
}