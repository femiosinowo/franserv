using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TeamLogic.CMS;

public partial class UserControls_RequestCallLocal : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        frmRequestCall.DefaultFormID = ConfigHelper.GetValueLong("LocalRegisterForACallFormId");
        frmRequestCall.Fill();
    }
}