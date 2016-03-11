using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SignalGraphics.CMS;
using Ektron.Cms;
using Ektron.Cms.Instrumentation;


public partial class AdminTool_Templates_manage_center : System.Web.UI.Page
{    
    string centerId;
    UserAPI userApi = new UserAPI();

    protected void Page_Init(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            var countryList = AdminToolManager.GetAllCountryList();
            ddlCountryList.DataSource = countryList;
            ddlCountryList.DataTextField = "CountryName";
            ddlCountryList.DataValueField = "CountryCode";
            ddlCountryList.DataBind();

            ListItem item = new ListItem() { Text = "-Select One-", Value = "-Select One-", Selected = true };
            ddlCountryList.Items.Insert(0, item);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            var udata = CommunityUserHelper.GetUserByUserId(userApi.UserId);
            if (udata != null && udata.Id > 0)
            {
                var centerUsers = AdminToolManager.GetAllLocalAdmins();
                var userData = centerUsers.Where(x => x.UserName.ToLower() == udata.Username.ToLower()).FirstOrDefault();
                if (userData != null)
                {
                    centerId = userData.FransId;
                    this.FillCenterData(centerId);
                    this.FillThirdPartyData(centerId);
                    hdnCenterId.Value = centerId; 
                }
            }         
        }

        if (!string.IsNullOrEmpty(hdnCenterId.Value))
        {
            var centerData = FransDataManager.GetFransData(hdnCenterId.Value);
            if (centerData != null)
            {
                centerInfo.Visible = true;
                lblCenterName.Text = centerData.CenterName;
                lblCenterId.Text = centerData.FransId;
            }
        }

    }

    protected void btnUpdateCenter_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblError.Text = "";
            string latitude = string.Empty;
            string longitude = string.Empty;
            string stateFullName = string.Empty;
            string stateCode = string.Empty;
            string address1 = txtAddress1.Text;
            string city = txtCity.Text;
            string state;
            if (ddlState.SelectedItem.Value != "N/A")
            { state = ddlState.SelectedItem.Value; }
            else
                state = txtInternationalState.Text;
            string zipCode = txtZipcode.Text;
            string country = ddlCountryList.SelectedItem.Value;
            string workingHours = txtWorkingHoursWeekDays.Text + "," + txtWorkingHoursWeekend.Text;
            try
            {
                string baseUri = "http://maps.googleapis.com/maps/api/geocode/xml?address=" + address1.Replace(" ", "+") + "+" + city + "+" + state + "+" + zipCode + "+" + country + "&sensor=false";

                using (System.Net.WebClient wc = new System.Net.WebClient())
                {
                    string result = wc.DownloadString(baseUri);
                    var xmlElm = System.Xml.Linq.XElement.Parse(result);
                    var status = (from elm in xmlElm.Descendants()
                                  where
                                      elm.Name == "status"
                                  select elm).FirstOrDefault();
                    if (status.Value.ToLower() == "ok")
                    {
                        var lat = (from elm in xmlElm.Descendants()
                                   where
                                       elm.Name == "lat"
                                   select elm).FirstOrDefault();
                        latitude = lat.Value;
                        var lon = (from elm in xmlElm.Descendants()
                                   where
                                       elm.Name == "lng"
                                   select elm).FirstOrDefault();
                        longitude = lon.Value;

                        var addressComponent = from elm in xmlElm.Descendants()
                                               where
                                                   elm.Name == "address_component"
                                               select elm;
                        var stateInfo = (from add in addressComponent
                                         where add.Element("type").Value == "administrative_area_level_1"
                                         select add).FirstOrDefault();
                        if (stateInfo != null)
                        {
                            stateCode = stateInfo.Element("short_name").Value;
                            stateFullName = stateInfo.Element("long_name").Value;
                        }
                    }
                    else if (status.Value.ToLower() == "zero_results")
                    {
                        lblError.Visible = true;
                        lblError.Text = "Invalid address provided, no location found as per Google API. Can you please check the address again!!!";
                        return;
                    }
                }

                if (latitude == string.Empty || longitude == string.Empty || stateFullName == string.Empty)
                {
                    lblError.Visible = true;
                    lblError.Text = "Problem Updating Center";
                    return;
                }

               // string centerId = Request.QueryString["centerId"] != null ? Request.QueryString["centerId"].ToString() : "";
                centerId = hdnCenterId.Value;
                if (centerId != "")
                {
                    var allCenters = FransDataManager.GetAllFransLocationDataList(true);
                    if (allCenters != null && allCenters.Any())
                    {
                        //check if center already exist
                        var centerData = allCenters.Where(x => x.FransId == centerId).FirstOrDefault();
                        if (centerData != null && !string.IsNullOrEmpty(centerData.FransId))
                        {
                            int isCenterDeleted;
                            if (chkIsCenterActive.Checked)
                                isCenterDeleted = 0;
                            else
                                isCenterDeleted = 1;

                            bool status = AdminToolManager.UpdateCenter(centerId, txtName.Text, address1, txtAddress2.Text, city, stateCode,
                                  zipCode, country, txtPhone.Text, txtFax.Text, txtEmail.Text, txtSAFEmail.Text,
                                  txtRAQEmail.Text, workingHours, DateTime.Now, latitude, longitude, stateFullName, txtContactFirstName.Text, 
                                  txtContactLastName.Text, centerData.CmsCommunityGroupId, isCenterDeleted, txtWhitePaperDownloadEmail.Text, txtJobApplicationEmail.Text, txtSubscriptionEmail.Text);
                            if (status)
                            {
                                centerInfoUpdateMsg.Visible = true;
                            }
                            else
                            {
                                lblError.Visible = true;
                                lblError.Text = "Sorry, an exception has occured while adding a Center. Please try check the data provided.";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }
    }

    protected void btnThirdParty_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            //var centerId = Request.QueryString["CenterId"] != null ? Request.QueryString["CenterId"].ToString() : "";
            centerId = hdnCenterId.Value;
            if (centerId != "")
            {
                SocialMedia socialMediaData = new SocialMedia();
                socialMediaData.FaceBookUrl = txtFB.Text;
                socialMediaData.TwitterUrl = txtTwitterUrl.Text;
                socialMediaData.GooglePlusUrl = txtGooglePlus.Text;
                socialMediaData.LinkedInUrl = txtLinkedinUrl.Text;
                socialMediaData.StumbleUponUrl = txtStumbleUrl.Text;
                socialMediaData.FlickrUrl = txtFlickrUrl.Text;
                socialMediaData.YouTubeUrl = txtYouTubeUrl.Text;
                socialMediaData.MarketingTangoUrl = txtMarketingTangoUrl.Text;
                string socialMediaXml = Ektron.Cms.EkXml.Serialize(typeof(SocialMedia), socialMediaData);
                int status = AdminToolManager.UpdateThirdPartyData(centerId, txtFlickrUserId.Text, txtTwitterFeedUrl.Text, socialMediaXml);
                //Response.Redirect("/AdminTool/Templates/Center.aspx?centerid=" + centerId + "&tab=0&type=edit");
                FransDataManager.GetFransThirdPartyData(centerId, true);
                thirdPartyInfoMsg.Visible = true;
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = "No Center Id found!!!";
                //Response.Redirect("/AdminTool/Templates/Center.aspx?tab=0");
            }
        }
    }

    private void FillCenterData(string centerId)
    {
        var centerDataList = FransDataManager.GetAllFransLocationDataList(true);
        if (centerDataList != null && centerDataList.Any())
        {
            var centerData = centerDataList.Where(x => x.FransId == centerId).FirstOrDefault();
            if (centerData != null)
            {
                txtName.Text = centerData.CenterName;
                txtAddress1.Text = centerData.Address1;
                txtAddress2.Text = centerData.Address2;
                txtCity.Text = centerData.City;

                ListItem stateItem = new ListItem();
                stateItem.Value = centerData.State;
                stateItem.Text = centerData.State;

                if (ddlState.Items.IndexOf(stateItem) > -1)
                {
                    int stateIndex = ddlState.Items.IndexOf(stateItem);
                    ddlState.SelectedIndex = stateIndex;
                    //ddlState.SelectedItem.Text = centerData.State;
                }
                else
                    txtInternationalState.Text = centerData.StateFullName;

                txtZipcode.Text = centerData.Zipcode;

                ddlCountryList.SelectedItem.Value = centerData.Country;
                ddlCountryList.SelectedItem.Text = centerData.Country;                
                
                txtPhone.Text = centerData.PhoneNumber;
                txtFax.Text = centerData.FaxNumber;
                string[] workingHours = centerData.HoursOfOperation.Split(',');
                if (workingHours != null && workingHours.Length > 1)
                {
                    txtWorkingHoursWeekDays.Text = workingHours[0];
                    txtWorkingHoursWeekend.Text = workingHours[1];
                }
                txtEmail.Text = centerData.Email;
                txtContactFirstName.Text = centerData.ContactFirstName;
                txtContactLastName.Text = centerData.ContactLastName;
                txtRAQEmail.Text = centerData.RequestAQuoteEmail;
                txtSAFEmail.Text = centerData.SendAFileEmail;
                txtWhitePaperDownloadEmail.Text = centerData.WhitePaperDownloadEmail;
                txtJobApplicationEmail.Text = centerData.JobApplicationEmail;
                txtSubscriptionEmail.Text = centerData.SubscriptionEmail;
            }
        }
    }

    private void FillThirdPartyData(string centerId)
    {        
        var thirdPartyData = FransDataManager.GetFransThirdPartyData(centerId);
        if (thirdPartyData != null)
        {
            txtFlickrUserId.Text = thirdPartyData.FlickrUserId;
            txtTwitterFeedUrl.Text = thirdPartyData.TwitterUrl;
            var socialMediaData = thirdPartyData.SocialMediaData;
            if (socialMediaData != null)
            {
                txtFB.Text = socialMediaData.FaceBookUrl;
                txtTwitterUrl.Text = socialMediaData.TwitterUrl;
                txtGooglePlus.Text = socialMediaData.GooglePlusUrl;
                txtLinkedinUrl.Text = socialMediaData.LinkedInUrl;
                txtStumbleUrl.Text = socialMediaData.StumbleUponUrl;
                txtFlickrUrl.Text = socialMediaData.FlickrUrl;
                txtYouTubeUrl.Text = socialMediaData.YouTubeUrl;
                txtMarketingTangoUrl.Text = socialMediaData.MarketingTangoUrl;
            }
        }
    }
}