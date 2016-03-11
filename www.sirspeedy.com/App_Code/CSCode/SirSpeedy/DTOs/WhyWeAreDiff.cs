using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SirSpeedy.CMS
{
    /// <summary>
    /// Summary description for WhyWeAreDiff
    /// </summary>
    public class WhyWeAreDiff
    {
        public long WhyWeAreDiffId
        { get; set; }
        public string CenterId
        { get; set; }
        public string BannerTitle
        { get; set; }
        public string BannerSubTitle
        { get; set; }
        public string ContentTitle
        { get; set; }
        public string ContentTagLine
        { get; set; }
        public string ContentDescription
        { get; set; }
        public string VideoLink
        { get; set; }
        public string VideoStatementText
        { get; set; }       
        public DateTime DateCreated
        { get; set; }
        public DateTime DateModified
        { get; set; }
        public string Image_Path
        { get; set; }        
    }
}