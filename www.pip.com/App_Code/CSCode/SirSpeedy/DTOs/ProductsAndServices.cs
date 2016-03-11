using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SirSpeedy.CMS
{
    /// <summary>
    /// Summary description for ServicesAndProducts
    /// </summary>
    public class ProductsAndServices
    {
        public long MainCategoryId
        { get; set; }
        public String MainCategoryName
        { get; set; }      
        public List<long> SubCategoryContentId
        { get; set; }
    }
}