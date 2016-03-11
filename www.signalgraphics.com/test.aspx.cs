using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SignalGraphics.CMS;
using System.Configuration;
using Ektron.Cms.Framework.Content;
using Ektron.Cms.Content;
using Ektron.Cms;
using Ektron.Cms.Common;
using System.Xml.Linq;
using System.Xml.XPath;
using Ektron.Cms.Instrumentation;
using FlickrNet;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Web.Script.Serialization;
using System.Data;


public partial class test : System.Web.UI.Page
{
    private static string adminToolConnectionString = ConfigurationManager.ConnectionStrings["SignalGraphicsAdminTool.DbConnection"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
	
	protected void Upload_NationalComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
    {
               
    }    
}