using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalGraphics.CMS
{
    /// <summary>
    /// Summary description for Partners
    /// </summary>
    public class Partners
    {
        public long MainTaxonomyId
        { get; set; }
        public String MainCategoryTaxName
        { get; set; }
        public string Center_Id
        { get; set; }
        public long Partner_Id
        { get; set; }
        public string Partner_Name
        { get; set; }
        public string Partner_Tagline
        { get; set; }
        public string Partner_Teaser
        { get; set; }
        public string Partner_Website
        { get; set; }
        public string Partner_Image_Id
        { get; set; }
        public string Partner_Start_Date
        { get; set; }
        public string Partner_End_Date
        { get; set; }
    }
}