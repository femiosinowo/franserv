using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SirSpeedy.CMS;


public partial class requestaquote : System.Web.UI.Page
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
        DateFormField1.DefaultFormID= ConfigHelper.GetValueLong("DateFieldFormId");
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