using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamLogic.CMS
{
    /// <summary>
    /// Summary description for FransWorkareaData
    /// </summary>
    public class FransWorkareaData
    {       
        public List<long> BannerContentIds
        { get; set; }      
        public List<long> NewsContentIds
        { get; set; }       
        public List<long> CaseStudiesContentIds
        { get; set; }   
        public List<long> PartnersContentIds
        { get; set; }       
        public List<long> InTheMediaContentIds
        { get; set; }
        public List<long> BriefsWhitePapersContentIds
        { get; set; }
        public List<long> CenterEmployeesTeam
        { get; set; }
        public List<ProductsAndServices> ProductAndServices
        { get; set; }
        public List<long> ShopContentIds
        { get; set; }
        public List<long> ManageITContentIds
        { get; set; }
        public List<long> ITSolutionContentIds
        { get; set; }
        public List<long> ProjectExpertsContentIds
        { get; set; }
        public List<long> ITOurSolutionContentIds
        { get; set; }
    }
}