using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamLogic.CMS
{
    /// <summary>
    /// Summary description for Subscribtion
    /// </summary>
    public class Subscribtion
    {
        public long SubscribtionId
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
        public string OnlineType
        { get; set; }
        public string PrintType
        { get; set; }
        public string Address1
        { get; set; }
        public string Address2
        { get; set; }
        public string City
        { get; set; }
        public string State
        { get; set; }
        public string Zipcode
        { get; set; }
        public string Phone
        { get; set; }
        public DateTime DatePosted
        { get; set; }  
    }
}