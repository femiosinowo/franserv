using System;
using TeamLogic.CMS;

public partial class Content : PageBase
{
    /// <summary>
    /// Page Init
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Init(object sender, EventArgs e)
    {
        cb1.CacheInterval = ConfigHelper.GetValueLong("smallCacheInterval");
        cb1.Fill();

        //this seems to be an Ektron issue, I update the contentdefault template & also quicklink in library
        //but the request is stillcoming to content.aspx, I also double checked the database Content table & library table and everything is fine
        //here is the dirty fix to resolve the issue.
        if (Request.RawUrl.ToLower().Contains("site-map"))
            Server.Transfer("SiteMapPage.aspx");
    }
}