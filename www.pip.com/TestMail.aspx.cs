using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SirSpeedy.CMS;
using System.Net.Mail;
using System.Text;
using System.Configuration;
using System.Net;
using System.Diagnostics;

public partial class TestMail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();

        string SMTPServer = ConfigurationManager.AppSettings["ek_SMTPServer"];
        string SMTPPort = ConfigurationManager.AppSettings["ek_SMTPPort"];
        string SMTPUser = ConfigurationManager.AppSettings["ek_SMTPUser"];
        string SMTPPass = ConfigurationManager.AppSettings["ek_SMTPPass"];
        string toEmail = "ukumar@figleaf.com";
        string fromEmail = "ukumar@figleaf.com";
        string fromDisplayName = "Uttam Kumar";
        string ccEmail = "ukumar@figleaf.com";
        string bccEmail = "ukumar@figleaf.com";
        string body = "test email for mail server";
        string subject = "Test SMTP Server with static SAF data";

        StringBuilder sbEmailBody = new StringBuilder();
        sbEmailBody.Append("<div>");
        sbEmailBody.Append("Hi,<br/><p>Following data has been submitted for Send A File: </p><table><tr><td>FirstName: </td><td>test</td></tr><tr><td>LastName: </td><td>test</td></tr><tr><td>JobTitle: </td><td>test</td></tr><tr><td>CompanyName: </td><td>test</td></tr><tr><td>Email: </td><td>test@t.com</td></tr><tr><td>PhoneNumber: </td><td>000-000-0000</td></tr><tr><td>ProjectName: </td><td>test</td></tr><tr><td>Project Due Date: </td><td>test</td></tr><tr><td>ProjectDescription: </td><td>test</td></tr><tr><td>ProjectQuantity: </td><td>1</td></tr></table>");
        sbEmailBody.Append("</div>");

        using (MailMessage mailMessage = new MailMessage())
        {
            mailMessage.Subject = subject;
            mailMessage.From = new MailAddress(fromEmail, fromDisplayName);
            string[] toarray = toEmail.Split(';');
            foreach (string address in toarray)
            {
                mailMessage.To.Add(address);
            }
            //mailMessage.To.Add(toEmail);
            //mailMessage.To.Add("uttam0005@gmail.com");
            //mailMessage.To.Add("uttam.kumar47@gmail.com");
            //mailMessage.To.Add("ukumaar22@gmail.com");
            //mailMessage.To.Add("vaish.kanhole@gmail.com");
            //mailMessage.To.Add("vaish.kanhole04@gmail.com");
            mailMessage.CC.Add(ccEmail);
            mailMessage.Bcc.Add(bccEmail);
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            mailMessage.Body = sbEmailBody.ToString();
            mailMessage.IsBodyHtml = true;

            NetworkCredential networkCred = new NetworkCredential();
            networkCred.UserName = SMTPUser;
            networkCred.Password = SMTPPass;

            using (SmtpClient smtpClient = new SmtpClient())
            {
                smtpClient.Host = SMTPServer;
                smtpClient.Credentials = networkCred;
                smtpClient.Port = int.Parse(SMTPPort);
                smtpClient.Send(mailMessage);
            }
        }

        string status = string.Format("Send email response Request took: <strong>{0}</strong><br/><br/>", stopWatch.Elapsed);
        Response.Write("status: " + status);
        //Utility.SendEmail(toEmail, fromEmail, ccEmail, body, subject);
    }
}