using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamLogic.CMS
{
    /// <summary>
    /// Summary description for FransThirdPartyData
    /// </summary>
    public class FransThirdPartyData
    {
        public SocialMedia SocialMediaData
        { get; set; }
        public String FlickrUserId
        { get; set; }
        public String TwitterUrl
        { get; set; }
        public List<string> SelectedPhotoSetIds
        { get; set; }        
    }
}