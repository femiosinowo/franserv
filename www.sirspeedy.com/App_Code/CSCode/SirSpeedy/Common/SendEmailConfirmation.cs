using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SirSpeedy.CMS
{
    /// <summary>
    /// Summary description for SendEmailConfirmation
    /// </summary>
    public class SendEmailConfirmation
    {
        protected SendEmailConfirmation()
        {
        }

        public static void SendAFileConfirmationMessage(string fromemail,string firstname, string lastname, string jobtitle, string phonenumber, string email, string project_name, string project_quantity, string project_due_date,string center, string filename, string emailOptin)
        {
            try
            {
                StringBuilder sbEmailBody = new StringBuilder();
                sbEmailBody.AppendFormat("{0} {1},<br/>", firstname, lastname);

                string emailBodyMsg = ConfigHelper.GetValueString("EmailConfirmationBodyMessage");
                sbEmailBody.AppendLine(emailBodyMsg + "<br/>");
                sbEmailBody.AppendLine("CONTACT INFORMATION:<br/>");
                if ((String.IsNullOrWhiteSpace(firstname)) || (String.IsNullOrWhiteSpace(lastname)))
                {
                    string name = String.Empty;
                    if (!String.IsNullOrWhiteSpace(firstname)) name = firstname;
                    if (!String.IsNullOrWhiteSpace(lastname)) name = lastname;
                    sbEmailBody.AppendFormat("Name: {0}<br/>", name);
                }
                else
                {
                    sbEmailBody.AppendFormat("First Name: {0}<br/>", firstname);
                    sbEmailBody.AppendFormat("Last Name: {0}<br/>", lastname);
                }
                if (!String.IsNullOrWhiteSpace(jobtitle))
                sbEmailBody.AppendFormat("Title: {0}<br/>", jobtitle);
                sbEmailBody.AppendFormat("Phone: {0}<br/>", phonenumber);
                sbEmailBody.AppendFormat("Email: {0}<br/>", email);
                sbEmailBody.AppendLine("FILE INFORMATION: <br/>");
                sbEmailBody.AppendFormat("For Center: {0}<br/>",center);
                sbEmailBody.AppendFormat("Original File Name: {0}<br/>", filename);

                if (!String.IsNullOrWhiteSpace(project_name))
                    sbEmailBody.AppendFormat("Project Name: {0}<br/>", project_name);
                if (!String.IsNullOrWhiteSpace(project_quantity))
                    sbEmailBody.AppendFormat("Quantity: {0}<br/>", project_quantity);
                if (!String.IsNullOrWhiteSpace(project_due_date))
                    sbEmailBody.AppendFormat("Due Date: {0}<br/>", project_due_date);

                if (!String.IsNullOrWhiteSpace(emailOptin))
                    sbEmailBody.AppendFormat("Email Opt In: {0}<br/>", emailOptin);
                
                string emailSubject = ConfigHelper.GetValueString("EmailConfirmationSubjectLine");
                string displayName = firstname + " " + lastname;
                Utility.SendEmail(email, fromemail, "", sbEmailBody.ToString(), emailSubject, displayName);

                Ektron.Cms.EkException.WriteToEventLog("Email sent",
                    System.Diagnostics.EventLogEntryType.Information);
            }
            catch (Exception ex)
            {
                Ektron.Cms.EkException.WriteToEventLog(ex.Message + " : " + ex.StackTrace, System.Diagnostics.EventLogEntryType.Error);
            }
        }
    }
}