using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TeamLogic.CMS;
using Ektron.Cms.Instrumentation;

public partial class UserControls_subscribeLocal : System.Web.UI.UserControl
{
    /// <summary>
    /// Page Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        cbDescription.DefaultContentID = ConfigHelper.GetValueLong("LocalSubscribeFormDecpCId");
        cbDescription.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbDescription.Fill();         
    }


}