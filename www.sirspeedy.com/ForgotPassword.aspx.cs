using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ektron.Cms;
using Ektron.Cms.Instrumentation;
using System.Web.Security;
using System.Text;
using System.Net.Mail;

public partial class ForgotPassword : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        bool success = false;
        string email = txtEmailAddress.Text;

        try
        {
            UserData udata = isEmailAvailable(email);

            if (udata != null)
            {
                success = true;
            }
            else
            {
                success = false;
            }
        }
        catch (Exception ex)
        {
            Log.WriteError(ex);
        }


        if (!success)
        {
            Response.Write("The Email Address is not valid");
        }
        else
        {
            Response.Write("Recovery Password has been sent to you email address");
        }
    }

    protected UserData isEmailAvailable(string email)
    {
        UserAPI userApi = new UserAPI();

        try
        {
            var udata1 = userApi.GetUserByUsername(email);
            if (udata1 != null && udata1.Id > 0)
            {
                // Generate a new 10-character password with 2 non-alphanumeric character.
                string newPassword = Membership.GeneratePassword(10, 2);

                Ektron.Cms.Framework.User.UserManager uMngr = new Ektron.Cms.Framework.User.UserManager(Ektron.Cms.Framework.ApiAccessMode.Admin);

                var uData = uMngr.GetItem(udata1.Id);
                uData.Password = newPassword;
                uMngr.Update(uData);

                udata1.Password = newPassword;

                sendRecoveryPassword(udata1.Email, udata1.Password);

                return udata1;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            Log.WriteError(ex);
        }

        return null;
    }

    private void sendRecoveryPassword(string email, string password)
    {
        string sendFrom = "no-reply@franserv.com";
        string sendTo = email;
        string emailSubject = "Your SirSpeedy Account's Recovery Password";
        string emailContent = "Your new password is " + password + "";

        //string emailHost = "mail.franserv.com";//"127.0.0.1";
        //string emailHost = "smtp.gmail.com";
        //int emailPort = 587;

        //string username = "no-reply";
        //string userpass = "BVpY42iwwYc=";

        //string senderEmail = "figleaf.andhita@gmail.com";
        //string senderPassword = "testingAsdf1234";

        try
        {
            //MailMessage objMail = new MailMessage(sendFrom, sendTo, emailSubject, emailContent);
            //MailAddress copy = new MailAddress("aprimandini@figleaf.com, ukumar@figleaf.com");
            //objMail.Bcc.Add(copy);
            //objMail.BodyEncoding = UTF8Encoding.UTF8;
            //objMail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            //SmtpClient objSmtp = new SmtpClient(emailHost, emailPort);
            //objSmtp.EnableSsl = true;
            //objSmtp.UseDefaultCredentials = true;
            //objSmtp.Credentials = new System.Net.NetworkCredential("figleaf.andhita@gmail.com", "testingAsdf1234");
            //objSmtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            //objSmtp.Send(objMail);

            //var client = new SmtpClient("smtp.gmail.com", 587)
            var client = new SmtpClient("mail.franserv.com", 25)
            {
                Credentials = new System.Net.NetworkCredential("'no-reply@franserv.com", "BVpY42iwwYc="),
                EnableSsl = false
            };
            client.Send(sendFrom, sendTo, emailSubject, emailContent);
        }
        catch (Exception ex)
        {
            Log.WriteError(ex);
        }
    }

}