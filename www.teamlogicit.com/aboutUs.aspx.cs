using System;
using System.Data;
using System.Xml;
using System.Xml.XPath;
using System.Linq;
using TeamLogic.CMS;
using Ektron.Cms.Content;
using Ektron.Cms.Common;
using Ektron.Cms.Instrumentation;
using System.Xml.Linq;
using System.Text;

public partial class about_national : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (FransDataManager.IsFranchiseSelected())
            uxAboutLocal.Visible = true;
        else
            uxNationalAboutUs.Visible = true;
    }
}