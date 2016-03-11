using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalGraphics.CMS
{
    /// <summary>
    /// Summary description for ThirdPartyUploadFileLinks
    /// </summary>
    public class ThirdPartyUploadFileLinks
    {
        public long SendFileId
        { get; set; }
        public long ThirdPartyUploadId
        { get; set; }
        public string FileLink
        { get; set; }
    }
}