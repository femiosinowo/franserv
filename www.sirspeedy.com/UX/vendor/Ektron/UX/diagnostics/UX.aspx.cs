using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ektron.Cms.PageBuilder;

public partial class UX_UX : PageBuilder
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public override void Error(string message)
    {
        jsAlert(message);
    }
    public override void Notify(string message)
    {
        jsAlert(message);
    }
    public void jsAlert(string message)
    {
        try
        {
            Literal lit = new Literal();
            lit.Text = "<script type=\"\" language=\"\">{0}</script>";
            lit.Text = string.Format(lit.Text, "alert('" + message + "');");
            this.Form.Controls.Add(lit);
        }
        catch (Exception)
        {
        }
    }
}