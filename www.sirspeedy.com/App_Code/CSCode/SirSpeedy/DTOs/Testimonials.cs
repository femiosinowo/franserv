using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SirSpeedy.CMS
{
    /// <summary>
    /// Summary description for Testimonials
    /// </summary>
    public class Testimonials
    {
        public int TestimonialId
        { get; set; }
        public string Center_Id
        { get; set; }
        public string Statement
        { get; set; }
        public string FirstName
        { get; set; }
        public string LastName
        { get; set; }
        public string Created_Date
        { get; set; }
        public string Organization
        { get; set; }
        public string Title
        { get; set; }
        public string PicturePath
        { get; set; }
    }
}
