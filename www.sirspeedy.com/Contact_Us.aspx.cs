using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SirSpeedy.CMS;
using System.Text;

public partial class Contact_Us : PageBase
{
    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetDirectionsForGoogleMap();
            GetFranchiseDetails();

            if (FransDataManager.IsFranchiseSelected())
            {
                string fransId = FransDataManager.GetFranchiseId();
                if ((!string.IsNullOrEmpty(fransId)) && (fransId.ToLower() == "plantationfl530"))
                    centerSpecificJSCode.Visible = true;
            }
        }
    }

    /// <summary>
    /// Gets the Franchise Data
    /// </summary>
    private void GetFranchiseDetails()
    {
        var fd = FransDataManager.GetFransData();
        if (fd != null)
        {
            ltrPhone.Text = fd.PhoneNumber;
            if (!string.IsNullOrEmpty(fd.FaxNumber))
                ltrFax.Text = "<li class=\"contact-icon-fax\"><span>" + fd.FaxNumber + "</span></li>";
            ltrEmail.Text = "<a href='mailto:" + fd.Email + "'>" + fd.Email + "</a>";
            string address2 = fd.Address2 != null && fd.Address2 != string.Empty ? fd.Address2 + "<br/>" : "";
            ltrAddress.Text = fd.Address1 + "<br/>" + address2 + fd.City + "  " + fd.State + " " + fd.Zipcode;
            if (!string.IsNullOrEmpty(fd.HoursOfOperation))
            {
                var hours = fd.HoursOfOperation.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string hoursDisplay = string.Empty;
                if (hours != null && hours.Length > 0)
                    hoursDisplay = hours[0];
                if (hours != null && hours.Length > 1)
                    hoursDisplay = hoursDisplay + "<br/>"+ hours[1];
                ltrWorkingHours.Text = hoursDisplay;
            }
        }
    }

    /// <summary>
    /// this method is used to update server name & directions for google map
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblError.Text = "";
            var centerId = FransDataManager.GetFranchiseId();
            var status = FransDataManager.SaveContactUsRequest(centerId, txtFirstName.Text, txtLastName.Text, contact_message.Value, txtEmail.Text);
            var fransId = FransDataManager.GetFranchiseId();
            if (!string.IsNullOrEmpty(fransId))
                Response.Redirect("/" + fransId + "/thank-you/?type=contactUs");
            else
                Response.Redirect("/thank-you/?type=contactUs");
        }
    }
}