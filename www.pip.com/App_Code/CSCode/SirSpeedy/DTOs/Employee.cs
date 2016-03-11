using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SirSpeedy.CMS
{
    /// <summary>
    /// Summary description for Employee
    /// </summary>
    public class Employee
    {
        public string FransId
        { get; set; }
        public long EmployeeId 
        { get; set; }
        public string UserName
        { get; set; }
        public string FirstName
        { get; set; }
        public string LastName
        { get; set; }
        public string MobileNumber
        { get; set; }
        public string WorkPhone
        { get; set; }
        public string FaxPhone
        { get; set; }
        public string Email
        { get; set; }
        public string Title
        { get; set; }
        public string IMScreenName
        { get; set; }
        public string IMService
        { get; set; }
        public string Bio
        { get; set; }
        public string Company
        { get; set; }
        public string PicturePath
        { get; set; }
        public string Gender
        { get; set; }
        public string Roles
        { get; set; }
        public int IsActive
        { get; set; }
        public string FranservId
        {
            get;
            set;
        }
    }
}