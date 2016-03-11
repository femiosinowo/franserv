using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;

namespace EkSharePoint
{
    /// <summary>
    /// Summary description for UserAuthHeader
    /// </summary>
    public class UserAuthHeader : SoapHeader
    {
        #region constructors

        public UserAuthHeader() {}

        #endregion


        #region properties

        private string _authenticationtoken = string.Empty;

        #endregion

        #region publicproperties

        [System.Xml.Serialization.XmlElement("AuthenticationToken")]
        public string AuthenticationToken
        {
            get { return _authenticationtoken; }
            set { _authenticationtoken = value; }
        }

        #endregion
    }
}