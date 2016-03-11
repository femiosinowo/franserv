using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TeamLogic.CMS;
using System.Text;


public partial class social_register : PageBase
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
                uxLocalRegister.Visible = true;
                uxNationalRegister.Visible = false;
            }
            else
            {
                uxNationalRegister.Visible = true;
                uxLocalRegister.Visible = false;
            }
        }
    }
    
}