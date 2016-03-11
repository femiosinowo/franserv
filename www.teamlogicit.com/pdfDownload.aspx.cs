using Ektron.Cms.Instrumentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pdfDownload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(Request.QueryString["pdfPath"]))
            {
                string domainName = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
                string pdfPath = Server.MapPath(Request.QueryString["pdfPath"]);

                System.IO.FileStream fs = new System.IO.FileStream(pdfPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                byte[] ar = new byte[(int)fs.Length];
                fs.Read(ar, 0, (int)fs.Length);
                fs.Close();

                Response.AddHeader("content-disposition", "attachment;filename=\"" + (Request.Browser.Browser == "IE" ? (Server.UrlPathEncode(System.IO.Path.GetFileNameWithoutExtension(pdfPath))) : (System.IO.Path.GetFileNameWithoutExtension(pdfPath))) + ".pdf" + "\"");
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(ar);                
            }
        }
        catch(Exception ex)
        {
            Log.WriteError(ex);
        }
    }
}