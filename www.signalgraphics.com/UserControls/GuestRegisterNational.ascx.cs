﻿using SignalGraphics.CMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ektron.Cms.Framework.Content;
using Ektron.Cms.Content;
using System.IO;
using Ektron.Cms.Instrumentation;
using System.Configuration;

public partial class UserControls_GuestRegisterNational : System.Web.UI.UserControl
{
    public string BoxClientId = "'" + ConfigurationManager.AppSettings["BoxAPIClientId"] + "'";
    public string BoxSecretSecret = "'" + ConfigurationManager.AppSettings["BoxAPIClientSecret"] + "'";
    public string GoogleAPIClientId = "'" + ConfigurationManager.AppSettings["GoogleAPIClientId"] + "'";
    public string GoogleDriveAPIDeveloperKey = "'" + ConfigurationManager.AppSettings["GoogleDriveAPIDeveloperKey"] + "'";

    protected void Page_Load(object sender, EventArgs e)
    {
        DateFormField1.DefaultFormID = ConfigHelper.GetValueLong("DateFieldFormId");
        viewDirectionRegisterDesktop.Value = FransDataManager.GoogleViewDirectionApiDesktop;
        viewDirectionRegisterMobile.Value = FransDataManager.GoogleViewDirectionApiMobile;
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