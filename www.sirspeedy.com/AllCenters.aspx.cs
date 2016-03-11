using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;

using SirSpeedy.CMS;
using Ektron.Cms.Instrumentation;

public partial class AllCenters : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["centerid"]))
            {
                pnlCenterDetails.Visible = true;
                pnlAllCenterData.Visible = false;
                this.FillCenterData(Request.QueryString["centerid"]);
            }
            else
            {
                pnlAllCenterData.Visible = true;
                pnlCenterDetails.Visible = false;
                this.BindGrid();
            }
        }
    }

    private void BindGrid()
    {
        var centerData = FransDataManager.GetAllFransLocationDataList(true);
        GridView1.DataSource = centerData;
        GridView1.DataBind();
    }
    
    private void FillCenterData(string centerId)
    {
        var centerData = FransDataManager.GetFransData(centerId);
        if (centerData != null)
        {
            lblName.Text = centerData.CenterName;
            lblAddress1.Text = centerData.Address1;
            lblAddress2.Text = centerData.Address2;
            lblCity.Text = centerData.City;
            lblState.Text = centerData.StateFullName;
            lblZipCode.Text = centerData.Zipcode;
            lblCountry.Text = centerData.Country;
            lblPhone.Text = centerData.PhoneNumber;
            lblFax.Text = centerData.FaxNumber;

            if (centerData.HoursOfOperation != null)
            {
                string[] hours = centerData.HoursOfOperation.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (hours.Length > 1)
                    lblWorkingHours.Text = hours[0] + " " + hours[1];
                else
                    lblWorkingHours.Text = hours[0];
            }
            lblCenterEmail.Text = centerData.Email;
            lblContactFirstName.Text = centerData.ContactFirstName;
            lblContactLastName.Text = centerData.ContactLastName;
            lblSendAFileEmail.Text = centerData.SendAFileEmail;
            lblRequestAQuote.Text = centerData.RequestAQuoteEmail;
        }

        var thirdPartyData = FransDataManager.GetFransThirdPartyData(centerId);
        if (thirdPartyData != null)
        {
            lblFlickrId.Text = thirdPartyData.FlickrUserId;
            lblTwitterFeedURL.Text = thirdPartyData.TwitterUrl;
            if (thirdPartyData.SocialMediaData != null)
            {
                lblFacebookUrl.Text = thirdPartyData.SocialMediaData.FaceBookUrl;
                lblTwitterUrl.Text = thirdPartyData.SocialMediaData.TwitterUrl;
                lblGooglePlusUrl.Text = thirdPartyData.SocialMediaData.GooglePlusUrl;
                lblLinkedinUrl.Text = thirdPartyData.SocialMediaData.LinkedInUrl;
                lblStumbleUponUrl.Text = thirdPartyData.SocialMediaData.StumbleUponUrl;
                lblFlickrUrl.Text = thirdPartyData.SocialMediaData.FlickrUrl;
                lblYoutubeUrl.Text = thirdPartyData.SocialMediaData.YouTubeUrl;
                lblMarketingTangoUrl.Text = thirdPartyData.SocialMediaData.MarketingTangoUrl;
            }
        }
    }
}