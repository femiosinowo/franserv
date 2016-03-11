using System;
using System.Data;
using System.Xml;
using System.Xml.XPath;
using System.Linq;
using SirSpeedy.CMS;
using Ektron.Cms.Content;
using Ektron.Cms.Common;
using Ektron.Cms.Instrumentation;
using System.Xml.Linq;
using System.Text;

public partial class history_national : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        cbHistory.DefaultContentID = ConfigHelper.GetValueLong("HistoryContentId");
        cbHistory.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbHistory.Fill();
    }    
}