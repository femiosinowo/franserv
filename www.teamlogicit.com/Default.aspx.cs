using System;
using TeamLogic.CMS;
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
                uxLocal.Visible = true;
                uxNational.Visible = false;
                //set body class
                this.Master.BodyClass += "local";

                string fransId = FransDataManager.GetFranchiseId();
                if ((!string.IsNullOrEmpty(fransId)) && (fransId == "greenvillesc002"))
                    ltrScripts.Text = "<script async src='http://i.simpli.fi/dpx.js?cid=14382&action=100&segment=Team_Logic_Site_Retargeting&m=1'></script>";

            }
            else
            {
                uxNational.Visible = true;
                uxLocal.Visible = false;
                //set body class
                this.Master.BodyClass += "national";
            }
        }
    }

}