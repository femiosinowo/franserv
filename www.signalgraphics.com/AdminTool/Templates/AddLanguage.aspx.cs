using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SignalGraphics.CMS;

public partial class AdminTool_Templates_AddLanguage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnAddLang_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        if (Page.IsValid)
        {
            string langName = txtAddLang.Text;
            var status = AdminToolManager.IsLanguageExist(langName);
            if (status)
            {
                lblError.Text = "Language Provided already exist in the system. Please provide a unique language name.";
                lblError.Visible = true;
                return;
            }

            var result = AdminToolManager.AddCenterLanguage(langName);
            if (result > 0)
            {
                addLangMessage.Visible = true;
            }
            else
            {
                lblError.Text = "Sorry, an error has occured adding the laguage to the system.";
                lblError.Visible = true;
            }
        }
    }
}