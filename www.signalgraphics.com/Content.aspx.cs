using System;
using SignalGraphics.CMS;

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
    }
}