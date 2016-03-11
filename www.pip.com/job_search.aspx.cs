using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SirSpeedy.CMS;

public partial class job_search : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        //if (FransDataManager.IsFranchiseSelected())
        //    uxJobSearch1.IsLocalSite = true;
        //else
            uxJobSearch1.IsLocalSite = false;
    }    
}