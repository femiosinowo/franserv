using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SignalGraphics.CMS;
using Ektron.Cms.Instrumentation;

public partial class National : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //FransDataManager.ExpireFranchiseCookie();
            Response.Redirect("/default.aspx");
        }
        catch (Exception ex)
        {
            Log.WriteError(ex);
        }
    }
}