using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SirSpeedy.CMS
{

    /// <summary>
    /// Summary description for FransData
    /// </summary>
    public class FransData
    {        
       /// <summary>
        /// Unique local center ID
        /// </summary>
        public string FransId
        { get; set; }
        public string CenterName
        { get; set; }        
        public string PhoneNumber
        { get; set; }
        public string FaxNumber
        { get; set; }
        public string Email
        { get; set; }
        public string ContactFirstName
        { get; set; }
        public string ContactLastName
        { get; set; }
        public string SendAFileEmail
        { get; set; }
        public string RequestAQuoteEmail
        { get; set; }
        public string WhitePaperDownloadEmail
        { get; set; }
        public string JobApplicationEmail
        { get; set; }
        public string SubscriptionEmail
        { get; set; }
        public string Address1
        { get; set; }
        public string Address2
        { get; set; }
        public string City
        { get; set; }
        public string State
        { get; set; }
        public string StateFullName
        { get; set; }
        public string Zipcode
        { get; set; }
        public string Country
        { get; set; }
        public string HoursOfOperation
        { get; set; }
        public string Latitude
        { get; set; }
        public string Longitude
        { get; set; }
        public string ContinentCode
        { get; set; }
        public long CmsCommunityGroupId
        { get; set; }
        public double Miles
        { get; set; }
         /// <summary>
        /// Unique string used to add center to the system
        /// </summary>
        public string FranservId
        { get; set; }
    }    
}