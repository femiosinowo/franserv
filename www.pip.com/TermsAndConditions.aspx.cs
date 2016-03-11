using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using SirSpeedy.CMS;

public partial class TermsAndConditions : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.BodyClass += " footer-terms-conditions ";

        cbHeader.DefaultContentID = ConfigHelper.GetValueLong("FooterContentHeaderCId");
        cbHeader.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbHeader.Fill();

        cbSiteMapSideContent.DefaultContentID = ConfigHelper.GetValueLong("TermsSideCId");
        cbSiteMapSideContent.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbSiteMapSideContent.Fill();        
    }
    
}