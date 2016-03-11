using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SignalGraphics.CMS;

public partial class TestMail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string toEmail = "ukumar@figleaf.com";
        string fromEmail = "ukumar@figleaf.com";
        string ccEmail = "ukumar@figleaf.com";
        string body = "test email for mail server";
        string subject = "Test SMTP Server";

        Utility.SendEmail(toEmail, fromEmail, ccEmail, body, subject);
    }
}