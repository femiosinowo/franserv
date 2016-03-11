using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

using Ektron.Cms;
using Ektron.Cms.Common;
using Ektron.Cms.Instrumentation;
using System.Threading.Tasks;

namespace SignalGraphics.CMS
{
    /// <summary>
    /// Summary description for Utility
    /// </summary>
    public class Utility
    {
        private static string _googleUri = ConfigurationManager.AppSettings["GoogleMapRequestURL"];
        private static string _googleClientId = ConfigurationManager.AppSettings["GoogleMapWorkClientId"];
        private static string _googleSignature = ConfigurationManager.AppSettings["GoogleMapWorkSigniture"];

        public Utility()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// This method is used to send email
        /// </summary>
        /// <param name="toAddress">toAddress required</param>
        /// <param name="fromAddress">fromAddress required</param>
        /// <param name="ccAddress">ccAddress optional</param>
        /// <param name="emailBody">emailBody required</param>
        /// <param name="emailSubject">emailSubject required</param>
        public static void SendEmail(string toAddress, string fromAddress, string ccAddress, string emailBody, string emailSubject, string displayName = "", string bccAddress = "")
        {
            try
            {
                if ((string.IsNullOrEmpty(toAddress)) ||
                    (string.IsNullOrEmpty(fromAddress)) ||
                    (string.IsNullOrEmpty(emailSubject)) ||
                    (emailBody == null))
                {
                    return;
                }

                string SMTPServer = ConfigurationManager.AppSettings["ek_SMTPServer"];
                string SMTPPort = ConfigurationManager.AppSettings["ek_SMTPPort"];
                string SMTPUser = ConfigurationManager.AppSettings["ek_SMTPUser"];
                string SMTPPass = ConfigurationManager.AppSettings["ek_SMTPPass"];
                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.Subject = emailSubject;
                    //string emailDisplayName = displayName != "" ? displayName : "Signal Graphics";
                    mailMessage.From = new MailAddress(fromAddress);
                    string[] toarray = toAddress.Split(';');
                    foreach (string address in toarray)
                    {
                        mailMessage.To.Add(address);
                    }
                    if (!string.IsNullOrEmpty(ccAddress))
                        mailMessage.CC.Add(ccAddress);
                    if (!string.IsNullOrEmpty(bccAddress))
                        mailMessage.Bcc.Add(bccAddress);
                    mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                    mailMessage.Body = emailBody;
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
            }
            catch (Exception ex)
            {
                Ektron.Cms.EkException.WriteToEventLog(ex.Message + " : " + ex.StackTrace, System.Diagnostics.EventLogEntryType.Error);
            }
        }

        public static string GetGoogleMapGeocodeUri(string address)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            string url = string.Format("{0}{1}&client={2}&sensor=false"
                                       , _googleUri
                                       , HttpUtility.UrlEncode(address)
                                       , _googleClientId);

            // converting key to bytes will throw an exception, need to replace '-' and '_' characters first.
            string usablePrivateKey = _googleSignature.Replace("-", "+").Replace("_", "/");
            byte[] privateKeyBytes = Convert.FromBase64String(usablePrivateKey);

            Uri uri = new Uri(url);
            byte[] encodedPathAndQueryBytes = encoding.GetBytes(uri.LocalPath + uri.Query);

            // compute the hash
            HMACSHA1 algorithm = new HMACSHA1(privateKeyBytes);
            byte[] hash = algorithm.ComputeHash(encodedPathAndQueryBytes);

            // convert the bytes to string and make url-safe by replacing '+' and '/' characters
            string signature = Convert.ToBase64String(hash).Replace("+", "-").Replace("/", "_");

            // Add the signature to the existing URI.
            return uri.Scheme + "://" + uri.Host + uri.LocalPath + uri.Query + "&signature=" + signature;
        }
    
    }
}