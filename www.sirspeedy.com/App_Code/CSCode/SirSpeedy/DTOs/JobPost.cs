using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SirSpeedy.CMS
{
    /// <summary>
    /// Summary description for JobPost
    /// </summary>
    public class JobPost
    {
        public long JobId
        { get; set; }
        public string CenterId
        { get; set; }
        public string Title
        { get; set; }
        public string ProfileType
        { get; set; }
        public string Language
        { get; set; }
        public string Location
        { get; set; }
        public string Description
        { get; set; }
        public DateTime DatePosted
        { get; set; }
        public DateTime DateExpire
        { get; set; }
        public bool IsPartTime
        { get; set; }
        public bool IsFullTime
        { get; set; }
        
    }
}