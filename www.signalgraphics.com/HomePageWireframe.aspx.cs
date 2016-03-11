using System;
using SignalGraphics.CMS;

public partial class HomePageWireframe : PageBuilderBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PageHost1.CacheInterval = ConfigHelper.GetValueLong("smallCacheInterval");
        }
    }    
}