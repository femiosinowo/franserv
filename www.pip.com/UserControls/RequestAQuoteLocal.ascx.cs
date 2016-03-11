using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SirSpeedy.CMS;
using System.Text;
using Ektron.Cms.Instrumentation;
using System.Web.Script.Serialization;
using Ektron.Cms.Framework.Content;
using Ektron.Cms.Content;
using System.IO;
using Ektron.Cms;

public partial class UserControls_RequestAQuoteLocal : System.Web.UI.UserControl
{
    /// <summary>
    /// page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetFranchiseDetails();
        }
        this.GetDirectionsForGoogleMap();
    }

    /// <summary>
    /// on file upload event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Upload_LocalComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
    {
        Session["fileSize_" + e.FileId] = e.FileSize;
        Session["fileContents_" + e.FileId] = e.GetContents();
        Session["fileExtension_" + e.FileId] = e.FileName.Substring(e.FileName.LastIndexOf('.'));
        Session["fileName_" + e.FileId] = e.FileName;
    }

    /// <summary>
    /// Gets the Franchise Data
    /// </summary>
    private void GetFranchiseDetails()
    {
        var fd = FransDataManager.GetFransData();
        if (fd != null)
        {
            StringBuilder contactinfo = new StringBuilder();
            contactinfo.Append("<ul class='contact_info'>");
            contactinfo.Append("<li class='contact-icon-location bottom-divider'>");
            contactinfo.Append("<span>");
            contactinfo.Append(fd.Address1);
            contactinfo.Append("<br>");
            contactinfo.Append(fd.City + "  " + fd.State + " " + fd.Zipcode);
            contactinfo.Append("</span>");
            contactinfo.Append("</li>");

            if (!string.IsNullOrEmpty(fd.PhoneNumber))
                contactinfo.Append("<li class='contact-icon-phone'><span>" + fd.PhoneNumber + "</span></li>");

            if (!string.IsNullOrEmpty(fd.FaxNumber))
                contactinfo.Append("<li class='contact-icon-fax'><span>" + fd.FaxNumber + "</span></li>");

            if (!string.IsNullOrEmpty(fd.Email))
                contactinfo.Append("<li class='contact-icon-email'><span><a href='mailto:" + fd.Email + "'>" + fd.Email + "</a></span></li>");

            contactinfo.Append("</ul>");
            litFranchiseContactInfo.Text = contactinfo.ToString();
            googleMapImage.Src = string.Format(FransDataManager.GoogleStaticLargeImagePath, "http://" + Request.ServerVariables["SERVER_NAME"], fd.Latitude, fd.Longitude);
        }
    }


    /// <summary>
    /// this method is used to get directions for google map
    /// </summary>
    private void GetDirectionsForGoogleMap()
    {
        var fransData = FransDataManager.GetFransData();
        if (fransData != null)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(fransData.Address1);
            if (!string.IsNullOrEmpty(fransData.Address2))
                sb.Append(" " + fransData.Address2);
            sb.Append(" " + fransData.City);
            sb.Append(" " + fransData.State);
            sb.Append("-" + fransData.Zipcode);
            sb.Append(" " + fransData.Country);
            string address = sb.ToString().Replace(" ", "+");
            viewDirectionDesktop.HRef = string.Format(FransDataManager.GoogleViewDirectionApiDesktop, address);
            viewDirectionMobile.HRef = string.Format(FransDataManager.GoogleViewDirectionApiMobile, address);
        }
    }
}