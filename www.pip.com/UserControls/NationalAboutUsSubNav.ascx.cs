using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SirSpeedy.CMS;

public partial class UserControls_NationalAboutUsSubNav : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UxNationalAboutUsSubNav.TreeFilter.Id = ConfigHelper.GetValueLong("NationalAboutUsNavId");
        }
    }
}