using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SirSpeedy.CMS;
using Ektron.Cms.Instrumentation;
using System.Web.UI.HtmlControls;

public partial class SiteError : PageBase
{

    protected override void OnLoad(System.EventArgs e)
    {
        Response.TrySkipIisCustomErrors = true;
        Response.Status = "404 Not Found";
        Response.StatusCode = 404;
        base.OnLoad(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (FransDataManager.IsFranchiseSelected())
        {
            string fransId = FransDataManager.GetFranchiseId();
            siteMapLink.HRef = "/" + fransId + "/site-map/";
        }
    }
       
}