using SirSpeedy.CMS;
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
        cbTagLines.DefaultContentID = ConfigHelper.GetValueLong("fransTagLineContentId");
        cbTagLines.CacheInterval = ConfigHelper.GetValueLong("smallCacheInterval");
        cbTagLines.Fill();

        if (cbTagLines.EkItem != null && !string.IsNullOrEmpty(cbTagLines.EkItem.Image) && cbTagLines.EkItem.Image.ToLower().IndexOf("workarea") <= -1)
        {
            tagLineContent.Attributes.Add("data-image", cbTagLines.EkItem.Image);
            tagLineContent.Attributes.Add("data-image-mobile", cbTagLines.EkItem.Image); 
        }
    }

}