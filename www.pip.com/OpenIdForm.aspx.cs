using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class OpenIdForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (OpenID.IsOpenIdRequest)
        {
            OpenIdData data = OpenID.Authenticate();
            if (data.IsSuccess)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("<b>OpenID: {0}</b><br />", data.Identity);
                sb.AppendFormat("email: {0}<br />", data.Parameters["email"]);
                sb.AppendFormat("fullname: {0}<br />", data.Parameters["fullname"]);
                sb.AppendFormat("country: {0}<br />", data.Parameters["country"]);
                sb.AppendFormat("language: {0}<br />", data.Parameters["language"]);

                Response.Write(sb.ToString());
            }
        }
    }

    protected void btnLogon_Click(object sender, EventArgs e)
    {

        bool success = OpenID.Login(txtOpenId.Text, "email,fullname,country", "language");

        if (!success)
        {
            Response.Write("The OpenID is not valid");
        }
    }
}