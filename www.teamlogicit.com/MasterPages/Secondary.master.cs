using Ektron.Cms.Instrumentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TeamLogic.CMS;

public partial class MasterPages_Secondary : MasterBase
{
    public string bodyCssClass { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Master.BodyClass += bodyCssClass;
        }
        catch(Exception ex)
        {
            Log.WriteError(ex);
        }
    }
}
