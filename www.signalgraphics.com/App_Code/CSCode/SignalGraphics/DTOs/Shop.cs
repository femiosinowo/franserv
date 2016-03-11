using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalGraphics.CMS
{
    /// <summary>
    /// Summary description for Shop
    /// </summary>
    public class Shop
    {
        public string Title
        { get; set; }
        public long ContentId
        { get; set; }
        public bool IsActive
        { get; set; }
        public string Image
        { get; set; }
        public string Teaser
        { get; set; } 
        public string Link
        { get; set; }       
    }
}