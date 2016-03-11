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
        Master.BodyClass += " home ";

        if (FransDataManager.IsFranchiseSelected())
        {           
            uxPromos.Visible = true;
            uxLocations.Visible = true;                    
            uxPortfolio.Visible = true;
            uxFransFooter.Visible = true;
 
            uxMaps.Visible = false;
            uxTagLines.Visible = false;                  
            uxBriefAndWhitepapers.Visible = false;
            uxCaseStudies.Visible = false;      
            uxJoinOurTeam.Visible = false;
            uxNationalFooter.Visible = false;
        }
        else
        {
            uxMaps.Visible = true;
            uxTagLines.Visible = true;            
            uxBriefAndWhitepapers.Visible = true;
            uxCaseStudies.Visible = true;
            uxJoinOurTeam.Visible = true;
            uxNationalFooter.Visible = true;
            uxProductsAndServices.Visible = true;
                        
            uxPromos.Visible = false;
            uxLocations.Visible = false;
            uxPortfolio.Visible = false;
            uxFransFooter.Visible = false;
        }
    }

}