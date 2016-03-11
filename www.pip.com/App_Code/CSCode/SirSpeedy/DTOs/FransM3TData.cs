using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SirSpeedy.CMS
{

    /// <summary>
    /// Summary description for FransM3TData
    /// </summary>
    public class FransM3TData
    { 
        public List<long> NewsContentIds
        { get; set; }       
        public List<long> AllInTheMediaIds
        { get; set; }       
        public List<long> CaseStudiesContentIds
        { get; set; }       
        public List<long> BriefsWhitePapersContentIds
        { get; set; }       
        public List<long> OurTeamEmployeeIds
        { get; set; }        
    }
}