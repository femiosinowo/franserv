using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamLogic.CMS
{
    /// <summary>
    /// Summary description for RequestToQuotes
    /// </summary>
    public class RequestToQuote
    {
        public long QuoteId
        { get; set; }
        public string CenterId
        { get; set; }
        public string FirstName
        { get; set; }
        public string LastName
        { get; set; }
        public string Email
        { get; set; }
        public string JobTitle
        { get; set; }
        public string CompanyName
        { get; set; }
        public string WebSite
        { get; set; }
        public string MobileNumber
        { get; set; }
        public string ProjectName
        { get; set; }
        public string ProjectDescription
        { get; set; }
        public string UploadedFileId
        { get; set; }
        public string ProjectBudget
        { get; set; }
        public DateTime DateSubmitted
        { get; set; }
        
    }
}