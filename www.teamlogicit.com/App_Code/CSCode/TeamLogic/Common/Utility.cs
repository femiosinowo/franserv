using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Security.Cryptography;
using System.Configuration;

using Ektron.Cms;
using Ektron.Cms.Common;
using Ektron.Cms.Instrumentation;

namespace TeamLogic.CMS
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
        public static void SendEmail(string toAddress, string fromAddress, string ccAddress, string emailBody, string emailSubject, string bccAddress = "")
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

                CommonApi commonApi = new CommonApi();
                EkMailService MailService = commonApi.EkMailRef;

                MailService.MailFrom = fromAddress;
                MailService.MailTo = toAddress;
                if (!string.IsNullOrEmpty(ccAddress))
                    MailService.MailCC = ccAddress;
                if (!string.IsNullOrEmpty(bccAddress))
                    MailService.MailBCC = bccAddress;
                MailService.MailSubject = emailSubject;
                MailService.MailBodyText = emailBody;
                MailService.SendMail();
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
        }

        public static string FormatTestimonialTitleAndCompany(object titleobject, object companyobject)
        {
            string text = String.Empty;
            string title = titleobject == null ? null : titleobject.ToString();
            string company = companyobject == null ? null : companyobject.ToString();

            if (!String.IsNullOrWhiteSpace(title) && !String.IsNullOrWhiteSpace(company))
                text = String.Format("{0}, {1}", title, company);
            else if (!String.IsNullOrWhiteSpace(title) && String.IsNullOrWhiteSpace(company))
                text = String.Format("{0}", title);
            else if (String.IsNullOrWhiteSpace(title) && !String.IsNullOrWhiteSpace(company))
                text = String.Format(",{0}", company);

            return text;
        }

        public static string FormatTestimonialContactDetails(object email, object phone)
        {
            StringBuilder sbtext = new StringBuilder();
            if ((phone != null) && (!String.IsNullOrWhiteSpace(phone.ToString()))) sbtext.AppendFormat("<br/>{0}", phone);
            if ((email != null) && (!String.IsNullOrWhiteSpace(email.ToString()))) sbtext.AppendFormat("<br/>{0}", email);
            return sbtext.ToString();
        }

        public static string FormatTestimonialLastName(string firstName, string lastName)
        {
            string updatedLastName = "";
            if (lastName != null)
            {
                if (firstName != "" && lastName != "")
                    updatedLastName = lastName + ", ";
                else if (firstName != "" && lastName == "")
                    updatedLastName = ", ";
                else if (firstName == "" && lastName == "")
                    updatedLastName = "";
            }
            return updatedLastName;
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