using AjaxControlToolkit;
using Ektron.Cms;
using Ektron.Cms.Content;
using Ektron.Cms.Framework.Content;
using Ektron.Cms.Instrumentation;
using SirSpeedy.CMS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_RequestAQuoteNational : System.Web.UI.UserControl
{

    /// <summary>
    /// page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        viewDirectionQuoteDesktop.Value = FransDataManager.GoogleViewDirectionApiDesktop;
        viewDirectionQuoteMobile.Value = FransDataManager.GoogleViewDirectionApiMobile;
    }

    /// <summary>
    /// on file upload event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Upload_NationalComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
    {
        string fileExt = e.FileName.Substring(e.FileName.LastIndexOf('.'));
        if (fileExt.ToLower() != ".exe" && fileExt.ToLower() != ".bat" && fileExt.ToLower() != ".cmd")
        {
            Session["fileSize_" + e.FileId] = e.FileSize;
            Session["fileContents_" + e.FileId] = e.GetContents();
            Session["fileExtension_" + e.FileId] = e.FileName.Substring(e.FileName.LastIndexOf('.'));
            Session["fileName_" + e.FileId] = e.FileName;
        }
    }
       
}