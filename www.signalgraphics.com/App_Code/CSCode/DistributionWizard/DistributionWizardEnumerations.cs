using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for DistributionWizardEnumerations
/// </summary>
public class DistributionWizardEnumerations
{
    /// <summary>
    /// Defines the various distribution modes.
    /// </summary>
    public enum Mode
    {
        CommunityCopy,
        CommunityReplace,
        CommunityRedistribute,
        Sharepoint,
        SharepointRedistribute,
        None
    }
}
