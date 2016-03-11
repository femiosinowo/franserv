using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamLogic.CMS
{
    /// <summary>
    /// Summary description for Menu
    /// </summary>
    public class EktMenu
    {
        public string Title
        { get; set; }
        public string Link
        { get; set; }
        public string Description
        { get; set; }
        public List<EktMenu> SubMenu       
        { get; set; }
    }
}