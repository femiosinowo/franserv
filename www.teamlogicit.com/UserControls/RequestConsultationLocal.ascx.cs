using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TeamLogic.CMS;
using Ektron.Cms.Instrumentation;

public partial class UserControls_RequestConsultationLocal : System.Web.UI.UserControl
{
    string centerId = FransDataManager.GetFranchiseId();
    /// <summary>
    /// Page Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        var centerdata = FransDataManager.GetFransData(centerId);
        if (centerdata != null)
        {
             
            ThankYouCB.DefaultContentID = ConfigHelper.GetValueLong("RequestConsultationThanksCId");
            ThankYouCB.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
            ThankYouCB.Fill();
            RequestConsultationWording wording =
                FransDataManager.GetRequestConsultation(centerdata.ConsultationMessageId);
            if (wording != null)
            {
                TitleLabel1.Text = wording.Title;
                DescriptionLiteral1.Text = wording.ConsultationText;
            }
        }
    }

}