using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SirSpeedy.CMS
{
    /// <summary>
    /// Summary description for Promotion
    /// </summary>
    public class Promotion
    {
        public long PromotionId
        { get; set; }       
        public List<long> SelectedPromoIds
        { get; set; }
        public string PromoImage1Link
        { get; set; }
        public string PromoImage2Link
        { get; set; }
        public DateTime ExpireDate
        { get; set; } 
        
    }
}