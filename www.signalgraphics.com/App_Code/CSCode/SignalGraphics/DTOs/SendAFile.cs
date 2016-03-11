using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalGraphics.CMS
{
    /// <summary>
    /// Summary description for SendAFile
    /// </summary>
    public class SendAFile
    {
        public long SendFileId
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
        public string MobileNumber
        { get; set; }
        public string WebSite
        { get; set; }
        public string ProjectName
        { get; set; }
        public string ProjectDueDate
        { get; set; }
        public string ProjectDescription
        { get; set; }
        public string UploadedFileId
        { get; set; }
        public string UploadedFileExternalLinks
        { get; set; }
        public string ProjectQuantity
        { get; set; }
        public DateTime DateSubmitted
        { get; set; }
        public string ServerDomain
        { get; set; }
    }
}