using System;
using SirSpeedy.CMS;
using System.Collections.Generic;
using System.Web.Script.Serialization;


public partial class _Default : PageBase
{
    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (FransDataManager.IsFranchiseSelected())
            {
                uxTagLines.Visible = true;
                if (uxPromos != null)
                    uxPromos.Visible = true;
                uxLocations.Visible = true;
                if (uxFransFooter != null)
                    uxFransFooter.Visible = true;
                uxMaps.Visible = false;
                if (uxNationalFooter != null)
                    uxNationalFooter.Visible = false;

                string fransId = FransDataManager.GetFranchiseId();
                if ((!string.IsNullOrEmpty(fransId)) && (fransId.ToLower() == "plantationfl530"))
                    centerSpecificJSCode.Visible = true;
            }
            else
            {
                uxMaps.Visible = true;
                if (uxNationalFooter != null)
                    uxNationalFooter.Visible = true;
                uxTagLines.Visible = false;
                if (uxPromos != null)
                    uxPromos.Visible = false;
                uxLocations.Visible = false;
                if (uxFransFooter != null)
                    uxFransFooter.Visible = false;
            }
        }
    }

}