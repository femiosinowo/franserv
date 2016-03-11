using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SirSpeedy.CMS;
using System.Text;
using Ektron.Cms.Instrumentation;


public partial class UserControls_HomePageLocations : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
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
        string fransid = FransDataManager.GetFranchiseId();
        var fd = FransDataManager.GetFransData(fransid);

        if (fd != null)
        {
            StringBuilder contactinfo = new StringBuilder();
            StringBuilder sbAdress = new StringBuilder();

            contactinfo.Append("<ul class='location_address'>");
            contactinfo.Append("<li>" + fd.Address1 + "</li>");
            contactinfo.Append("<li>" + fd.City + ", " + fd.State + " " + fd.Zipcode + "</li>");
            contactinfo.Append("</ul>");
            litLocAddress.Text = contactinfo.ToString();

            contactinfo.Clear();
            contactinfo.Append("<ul class='location_contact'>");
            if (!string.IsNullOrEmpty(fd.PhoneNumber))
                contactinfo.Append("<li><span>P: " + fd.PhoneNumber.Replace("-", ".") + "</span></li>");

            if (!string.IsNullOrEmpty(fd.FaxNumber))
                contactinfo.Append("<li><span>F: " + fd.FaxNumber.Replace("-", ".") + "</span></li>");

            if (!string.IsNullOrEmpty(fd.Email))
                contactinfo.Append("<li><span><a href='mailto:" + fd.Email + "'>" + fd.Email + "</a></span></li>");

            contactinfo.Append("</ul>");
            litLocContact.Text = contactinfo.ToString();

            if (!string.IsNullOrEmpty(fd.HoursOfOperation))
            {
                contactinfo.Clear();
                contactinfo.Append("<ul class=\"location_hours\">");
                contactinfo.Append("<li>Store Hours:</li>");
                string[] workingHours = fd.HoursOfOperation.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (workingHours != null)
                {
                    foreach (string h in workingHours)
                    {
                        contactinfo.Append("<li>" + h + "</li>");
                    }
                }
                contactinfo.Append("</ul>");
                ltrWorkingHours.Text = contactinfo.ToString();
            }

            sbAdress.Append(fd.Address1);
            if (!string.IsNullOrEmpty(fd.Address2))
                sbAdress.Append(" " + fd.Address2);
            sbAdress.Append(" " + fd.City);
            sbAdress.Append(" " + fd.State);
            sbAdress.Append("-" + fd.Zipcode);

            string directionsLink = "https://www.google.com/maps?daddr=q={0}";
            hiddenCenterLat.Value = fd.Latitude;
            hiddenCenterLong.Value = fd.Longitude;
            string address = sbAdress.ToString().Replace(" ", "+");
            directions_lb.HRef = string.Format(directionsLink, address);
        }
    }
      
}