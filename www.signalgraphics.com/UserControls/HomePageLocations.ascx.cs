using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SignalGraphics.CMS;
using System.Text;
using Ektron.Cms.Instrumentation;


public partial class UserControls_HomePageLocations : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        cbGoogleInfoBoxImgLogo.DefaultContentID = ConfigHelper.GetValueLong("GoogleInfoBoxLogo");
        cbGoogleInfoBoxImgLogo.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbGoogleInfoBoxImgLogo.Fill();

        if (!Page.IsPostBack)
        {
            GetFranchiseDetails();           
        }
    }
    
    /// <summary>
    /// Gets the Franchise Data
    /// </summary>
    private void GetFranchiseDetails()
    {   
        var fd = FransDataManager.GetFransData();
        StringBuilder contactinfo = new StringBuilder();

        if (fd != null)
        {
            string directionsLink = "https://www.google.com/maps?q={0}";
            hiddenCenterLat.Value = fd.Latitude;
            hiddenCenterLong.Value = fd.Longitude;

            contactinfo.Append("<p class='location_address'>");
            contactinfo.Append(fd.Address1);
            if(!string.IsNullOrEmpty(fd.Address2))
            {
                contactinfo.Append("<br/>");
                contactinfo.Append(fd.Address2);
            }
            contactinfo.Append("<br/>");
            contactinfo.Append(fd.City + ",  " + fd.State + " " + fd.Zipcode);
            contactinfo.Append("</p>");
            litLocAddress.Text = contactinfo.ToString();

            contactinfo.Clear();
            contactinfo.Append("<ul>");
            if (!string.IsNullOrEmpty(fd.PhoneNumber))
                contactinfo.Append("<li class='location-icon-phone'><span>" + fd.PhoneNumber.Replace("-", ".") + "</span></li>");


            if (!string.IsNullOrEmpty(fd.FaxNumber))
                contactinfo.Append("<li class='location-icon-fax'><span>" + fd.FaxNumber.Replace("-", ".") + "</span></li>");

            if (!string.IsNullOrEmpty(fd.Email))
                contactinfo.Append("<li class='location-icon-email'><span><a href='mailto:" + fd.Email + "'>" + fd.Email + "</a></span></li>");

            contactinfo.Append("</ul>");
            litLocContact.Text = contactinfo.ToString();
            ltrGoogleImgLogo.Text = cbGoogleInfoBoxImgLogo.EkItem != null ? cbGoogleInfoBoxImgLogo.EkItem.Html : "<img alt=\"Sir Speedy\" src=\"/images/logo-infobox.png\"/>";

            if (!string.IsNullOrEmpty(fd.HoursOfOperation))
            {
                contactinfo.Clear();
                contactinfo.Append("<p class=\"store_hours\">");
                contactinfo.Append("<span>Store Hours:</span>");
                string[] workingHours = fd.HoursOfOperation.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (workingHours != null)
                {
                    foreach (string h in workingHours)
                    {
                        contactinfo.Append(h + "<br/>");
                    }
                }
                contactinfo.Append("</p>");
                ltrWorkingHours.Text = contactinfo.ToString();
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(fd.Address1);
            if (!string.IsNullOrEmpty(fd.Address2))
                sb.Append(", " + fd.Address2);
            sb.Append(", " + fd.City);
            sb.Append(", " + fd.State);
            sb.Append("-" + fd.Zipcode);
            sb.Append(", " + fd.Country);
            string address = sb.ToString().Replace(" ", "+");
            directions_lb.HRef = string.Format(directionsLink, address);

        }
    }
       
}