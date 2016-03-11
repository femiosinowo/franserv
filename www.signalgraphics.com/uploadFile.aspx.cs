using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class uploadFile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["signedin"] == null ||
            bool.Parse(Session["signedin"].ToString()) == false)
        {
            Response.Redirect("Default.aspx");
        }

        Scribd.Net.Service.Error += new EventHandler<Scribd.Net.ScribdEventArgs>(Service_Error);
        if (IsPostBack)
        {
            // Upload
        }
    }

    void Service_Error(object sender, Scribd.Net.ScribdEventArgs e)
    {
        try
        {
            if (Response != null)
            Response.Write(e.Message);
        }
        catch { }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        System.IO.Stream _fileStream = FileUpload1.PostedFile.InputStream;
        Scribd.Net.Document.Upload(_fileStream, "doc", false);
    }
}
