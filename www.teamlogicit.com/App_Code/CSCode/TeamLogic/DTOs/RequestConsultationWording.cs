using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamLogic.CMS
{
    /// <summary>
    /// Summary description for RequestConsultationWording
    /// </summary>
    public class RequestConsultationWording
    {
        public int Consultation_Id
        { get; set; }
        public string CenterId
        { get; set; }
        public string ConsultationText
        { get; set; }
        public string Title
        { get; set; }

    }
}