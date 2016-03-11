using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SignalGraphics.CMS;

public partial class job_search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            //if (FransDataManager.IsFranchiseSelected())
            //    uxJobSearch1.IsLocalSite = true;
            //else
            uxJobSearch1.IsLocalSite = false;

        //check for page banner image via metadata
        if (cbTagline != null && cbTagline.EkItem != null)
        {
            if (!string.IsNullOrEmpty(cbTagline.EkItem.Image))
            {
                subpage_tagline_wrapper.Attributes.Add("style", "background-image: url('" + cbTagline.EkItem.Image + "')");
            }
        }
    }
}