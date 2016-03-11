using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamLogic.CMS
{
    /// <summary>
    /// Summary description for Subscribtion
    /// </summary>
    public class ConsultationRequest
    {
        public long ConsultationRequestId
        { get; set; }
        public string CenterId
        { get; set; }
        public string FirstName
        { get; set; }
        public string LastName
        { get; set; }
        public string Email
        { get; set; }
        public string Company
        { get; set; }
        public string HowCanWeHelp
        { get; set; }
        public bool IsSignupChecked
        { get; set; }
        public string Phone
        { get; set; }
        public DateTime DatePosted
        { get; set; }  
    }
}