using TeamLogic.CMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_HomePageTagLine : System.Web.UI.UserControl
{
    /// <summary>
    /// Page Load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cbTagLines.DefaultContentID = ConfigHelper.GetValueLong("fransTagLineContentId");
            cbTagLines.CacheInterval = ConfigHelper.GetValueLong("smallCacheInterval");
            cbTagLines.Fill();
        }
    }

}