using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TeamLogic.CMS;
using Ektron.Cms.Instrumentation;
using Ektron.Cms.Organization;
using Ektron.Cms.Common;
using Ektron.Cms.Content;
using System.Text;
using Ektron.Cms;
using System.Xml.Linq;
using System.Xml.XPath;
using Ektron.Cms.Framework.Settings.UrlAliasing;
using Ektron.Cms.Settings.UrlAliasing.DataObjects;
using System.Net;

public partial class AdminTool_Templates_Center : System.Web.UI.Page
{
    string centerId = string.Empty;
    bool isNewCenter = false;

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

            this.FillCenterDropDown();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString.HasKeys())
            {
                if (!string.IsNullOrEmpty(Request.QueryString["centerId"]))
                {
                    centerId = Request.QueryString["centerId"];
                    addCenterTitle.Visible = false;
                    editCenterTitle.Visible = true;
                    btnAddCenter.Visible = false;
                    btnUpdateCenter.Visible = true;
                    txtFranservId.ReadOnly = true;
                }
                else
                {
                    btnUpdateCenter.Visible = false;
                }

                if (!string.IsNullOrEmpty(Request.QueryString["type"]))
                {
                    isNewCenter = Request.QueryString["type"] == "add" ? true : false;
                }

                if (!string.IsNullOrEmpty(Request.QueryString["tab"]))
                {
                    string tab = Request.QueryString["tab"];
                    hdnSelectedTab.Value = tab;
                }

                if (!string.IsNullOrEmpty(Request.QueryString["clone"]))
                {
                    cloneCenterSuccessMsg.Visible = true;
                }
                if (!string.IsNullOrEmpty(Request.QueryString["selectedCenter"]))
                {
                    string selectedCenter = Request.QueryString["selectedCenter"];
                    ddlCenterId.SelectedValue = selectedCenter;
                }

            }

            this.FillCenterData();
            this.FillThirdPartyData();
            this.FillBanners();
            this.FillNews();
            this.FillPartners();
            this.FillITSolution();
            this.FillFeaturedSolution();
            this.FillConsultingProjects();

            if (!string.IsNullOrEmpty(centerId))
            {
                var centerData = FransDataManager.GetFransData(centerId);
                if (centerData != null)
                {
                    centerInfo.Visible = true;
                    lblCenterName.Text = centerData.CenterName;
                    lblCenterId.Text = centerData.FransId;
                }
            }
        }
    }

    protected void btnAddCenter_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblError.Text = "";
            string latitude = string.Empty;
            string longitude = string.Empty;
            string stateFullName = string.Empty;
            string stateCode = string.Empty;
            string address1 = txtAddress1.Text;
			string address2 = txtAddress2.Text;
            string city = txtCity.Text;
            string state;
            if (ddlState.SelectedItem.Value != "N/A")
            { state = ddlState.SelectedItem.Value; }
            else
                state = txtInternationalState.Text;
            string zipCode = txtZipcode.Text;
            string country = ddlCountryList.SelectedItem.Value;
            string workingHours = txtWorkingHoursWeekDays.Text + "," + txtWorkingHoursWeekend.Text;
            string consultationid = ddlConsultation.SelectedValue.Trim();
            try
            {
                string formattedAddress = "";
                if (address2 != "")
                    formattedAddress = address1 + ", " + address2 + ", " + city + ", " + state + " - " + zipCode;
                else
                    formattedAddress = address1 + ", " + city + ", " + state + " - " + zipCode;

                string baseUri = Utility.GetGoogleMapGeocodeUri(formattedAddress);
				
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
                                         where add.Element("type") != null && add.Element("type").Value == "administrative_area_level_1"
                                         select add).FirstOrDefault();
                        if (stateInfo != null)
                        {
                            stateCode = stateInfo.Element("short_name").Value;
                            stateFullName = stateInfo.Element("long_name").Value;
                        }
                        else
                        {
                            //for US territory
                            stateInfo = (from add in addressComponent
                                         where add.Element("type").Value == "country"
                                         select add).FirstOrDefault();
                            if (stateInfo != null)
                            {
                                stateCode = stateInfo.Element("short_name").Value;
                                stateFullName = stateInfo.Element("long_name").Value;
                            }
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
                    lblError.Text = "Problem Adding Center";
                    return;
                }

                string centerId = GetCenterId(city, stateCode, txtFranservId.Text);
                //check if center already exist 
                if (AdminToolManager.IsCenterExist(centerId))
                {
                    lblError.Visible = true;
                    lblError.Text = "Center already exist with the id: '" + centerId + "'. Please provide a unique Franserv Id.";
                    return;                   
                }

                var cmsCenterGroupId = CommunityUserHelper.AddCommunityGroup(centerId, txtPhone.Text);
                bool addCenterStatus = AdminToolManager.AddCenter(centerId, txtName.Text, address1, txtAddress2.Text, city, stateCode,
                     zipCode, country, txtPhone.Text, txtFax.Text, txtEmail.Text, txtSAFEmail.Text,
                     txtRAQEmail.Text, workingHours, DateTime.Now, latitude, longitude, stateFullName,
                     txtContactFirstName.Text, txtContactLastName.Text, cmsCenterGroupId, txtFranservId.Text, txtWhitePaperDownloadEmail.Text, txtJobApplicationEmail.Text, txtSubscriptionEmail.Text, txtClientLoginURL.Text, consultationid);
                if (addCenterStatus)
                {
                    var status = this.CloneEktFolder(centerId);
                    if (status)
                        AddContentAliasForNewClonedCenter(centerId);
                    pnlAddedMessage.Visible = true;
                    pnlCenterInfo.Visible = false;
                    Response.Redirect("/AdminTool/Templates/Center.aspx?centerid=" + centerId + "&tab=1&type=edit");
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "Sorry, an exception has occured while adding a Center. Please try check the data provided.";
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
			string address2 = txtAddress2.Text;
            string city = txtCity.Text;
            string state;
            if (ddlState.SelectedItem.Value != "N/A")
            { state = ddlState.SelectedItem.Value; }
            else
                state = txtInternationalState.Text;
            string zipCode = txtZipcode.Text;
            string country = ddlCountryList.SelectedItem.Value;
            string workingHours = txtWorkingHoursWeekDays.Text + "," + txtWorkingHoursWeekend.Text;
            string consultationid = ddlConsultation.SelectedValue.Trim();
            try
            {
                string formattedAddress = "";
                if (address2 != "")
                    formattedAddress = address1 + ", " + address2 + ", " + city + ", " + state + " - " + zipCode;
                else
                    formattedAddress = address1 + ", " + city + ", " + state + " - " + zipCode;

                string baseUri = Utility.GetGoogleMapGeocodeUri(formattedAddress);
				
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
                                         where add.Element("type") != null && add.Element("type").Value == "administrative_area_level_1"
                                         select add).FirstOrDefault();
                        if (stateInfo != null)
                        {
                            stateCode = stateInfo.Element("short_name").Value;
                            stateFullName = stateInfo.Element("long_name").Value;
                        }
                        else
                        {
                            //for US territory
                            stateInfo = (from add in addressComponent
                                         where add.Element("type").Value == "country"
                                         select add).FirstOrDefault();
                            if (stateInfo != null)
                            {
                                stateCode = stateInfo.Element("short_name").Value;
                                stateFullName = stateInfo.Element("long_name").Value;
                            }
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

                string centerId = Request.QueryString["centerId"] != null ? Request.QueryString["centerId"].ToString() : "";
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
                                  txtContactLastName.Text, centerData.CmsCommunityGroupId, isCenterDeleted, txtWhitePaperDownloadEmail.Text, txtJobApplicationEmail.Text,
                                  txtSubscriptionEmail.Text, txtClientLoginURL.Text, txtRequestConsultationEmail.Text, consultationid);
                            if (status)
                            {
                                pnlEditMessage.Visible = true;
                                pnlCenterInfo.Visible = false;
                                Response.Redirect("/AdminTool/Templates/Center.aspx?centerid=" + centerId + "&tab=1&type=edit");
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
                Log.WriteError(ex.StackTrace);
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }
    }

    protected void btnThirdParty_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            var centerId = Request.QueryString["CenterId"] != null ? Request.QueryString["CenterId"].ToString() : "";
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
                socialMediaData.ITInflectionsUrl = txtInflectionsUrl.Text;
                string socialMediaXml = Ektron.Cms.EkXml.Serialize(typeof(SocialMedia), socialMediaData);
                int status = AdminToolManager.UpdateThirdPartyData(centerId, "", txtTwitterFeedUrl.Text, socialMediaXml);
                Response.Redirect("/AdminTool/Templates/Center.aspx?centerid=" + centerId + "&tab=2&type=edit");
            }
            else
            {
                //lblError.Visible = true;
                //lblError.Text = "No Center Id found!!!";
                Response.Redirect("/AdminTool/Templates/Center.aspx?tab=0");
            }
        }
    }

    protected void btnCloneCenter_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblCloneCenterError.Text = "";
            var centerId = Request.QueryString["CenterId"] != null ? Request.QueryString["CenterId"].ToString() : "";
            if (ddlCenterId.SelectedIndex > 0 && centerId != "")
            {
                var selectedCenterId = ddlCenterId.SelectedValue;
                var status = AdminToolManager.CloneCenter(selectedCenterId, centerId);
                if (status)
                {
                    cloneCenterSuccessMsg.Visible = true;
                    Response.Redirect("/AdminTool/Templates/Center.aspx?centerid=" + centerId + "&tab=2&type=edit&clone=1&selectedCenter=" + selectedCenterId + "");
                }
                else
                    lblCloneCenterError.Text = "Sorry, an error has occured saving the center settings.";
            }
        }
    }

    protected void btnNoCloning_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            var centerId = Request.QueryString["CenterId"] != null ? Request.QueryString["CenterId"].ToString() : "";
            Response.Redirect("/AdminTool/Templates/Center.aspx?centerid=" + centerId + "&tab=3&type=edit");
        }
    }

    protected void btnBanners_Click(object sender, EventArgs e)
    {
        if (hddnSelectedBanners.Value != "")
        {
            lblError.Text = "";
            var centerId = Request.QueryString["CenterId"] != null ? Request.QueryString["CenterId"].ToString() : "";
            if (centerId != "")
            {
                int status = -1;
                if (AdminToolManager.IsBannersAssigned(centerId))
                    status = AdminToolManager.UpdateAvailableBanners(centerId, hddnSelectedBanners.Value);
                else
                    status = AdminToolManager.AddAvailableBanners(centerId, hddnSelectedBanners.Value);
                if (status > 0)
                {
                    //AdminToolManager.UpdateBannerIds(centerId, hddnSelectedBanners.Value);
                    Response.Redirect("/AdminTool/Templates/Center.aspx?centerid=" + centerId + "&tab=4&type=edit");
                }
                else
                {
                    lblError.Text = "Sorry, an error occured saving Banners data.";
                    lblError.Visible = true;
                }
            }
            else
            {
                //lblError.Visible = true;
                //lblError.Text = "No Center Id found!!!";
                Response.Redirect("/AdminTool/Templates/Center.aspx?tab=0&type=add");
            }
        }
    }

    protected void btnNews_Click(object sender, EventArgs e)
    {
        if (hddnSelectedNews.Value != "")
        {
            lblError.Text = "";
            var centerId = Request.QueryString["CenterId"] != null ? Request.QueryString["CenterId"].ToString() : "";
            if (centerId != "")
            {
                AdminToolManager.UpdateNewsIds(centerId, hddnSelectedNews.Value);
                Response.Redirect("/AdminTool/Templates/Center.aspx?centerid=" + centerId + "&tab=5&type=edit");
            }
            else
            {
                //lblError.Visible = true;
                //lblError.Text = "No Center Id found!!!";
                Response.Redirect("/AdminTool/Templates/Center.aspx?tab=0");
            }
        }
    }

    protected void btnPartners_Click(object sender, EventArgs e)
    {
        if (hddnSelectedPartners.Value != "")
        {
            lblError.Text = "";
            var centerId = Request.QueryString["CenterId"] != null ? Request.QueryString["CenterId"].ToString() : "";
            if (centerId != "")
            {
                AdminToolManager.UpdatePartnersIds(centerId, hddnSelectedPartners.Value);
                Response.Redirect("/AdminTool/Templates/Center.aspx?centerid=" + centerId + "&tab=6&type=edit");
            }
            else
            {
                //lblError.Visible = true;
                //lblError.Text = "No Center Id found!!!";
                Response.Redirect("/AdminTool/Templates/Center.aspx?tab=0");
            }
        }
    }

    protected void btnITSolution_Click(object sender, EventArgs e)
    {
        if (hddnSelectedITSolution.Value != "")
        {
            lblError.Text = "";
            var centerId = Request.QueryString["CenterId"] != null ? Request.QueryString["CenterId"].ToString() : "";
            if (centerId != "")
            {
                int status = -1;
                if (AdminToolManager.IsITSolutionsAssigned(centerId))
                    status = AdminToolManager.UpdateAvailableITSolutions(centerId, hddnSelectedITSolution.Value);
                else
                    status = AdminToolManager.AddAvailableITSolutions(centerId, hddnSelectedITSolution.Value);
                if (status > 0)
                {
                    //AdminToolManager.UpdateBannerIds(centerId, hddnSelectedBanners.Value);
                    Response.Redirect("/AdminTool/Templates/Center.aspx?centerid=" + centerId + "&tab=7&type=edit");
                }
                else
                {
                    lblError.Text = "Sorry, an error occured saving IT Solution data.";
                    lblError.Visible = true;
                }
            }
            else
            {
                //lblError.Visible = true;
                //lblError.Text = "No Center Id found!!!";
                Response.Redirect("/AdminTool/Templates/Center.aspx?tab=0&type=add");
            }
        }
    }

    protected void btnFeaturedSolution_Click(object sender, EventArgs e)
    {
        if (hddnSelectedFeaturedSolution.Value != "")
        {
            lblError.Text = "";
            var centerId = Request.QueryString["CenterId"] != null ? Request.QueryString["CenterId"].ToString() : "";
            if (centerId != "")
            {
                int status = -1;
                if (AdminToolManager.IsFeaturedSolutionsAssigned(centerId))
                    status = AdminToolManager.UpdateAvailableFeatureSolutions(centerId, hddnSelectedFeaturedSolution.Value);
                else
                    status = AdminToolManager.AddAvailableFeaturedSolutions(centerId, hddnSelectedFeaturedSolution.Value);
                if (status > 0)
                {
                    //AdminToolManager.UpdateBannerIds(centerId, hddnSelectedBanners.Value);
                    Response.Redirect("/AdminTool/Templates/Center.aspx?centerid=" + centerId + "&tab=8&type=edit");
                }
                else
                {
                    lblError.Text = "Sorry, an error occured saving Featured Solution data.";
                    lblError.Visible = true;
                }
            }
            else
            {
                //lblError.Visible = true;
                //lblError.Text = "No Center Id found!!!";
                Response.Redirect("/AdminTool/Templates/Center.aspx?tab=0&type=add");
            }
        }
    }

    protected void btnConsultingProject_Click(object sender, EventArgs e)
    {
        if (hddnSelectedConsultingProject.Value != "")
        {
            lblError.Text = "";
            var centerId = Request.QueryString["CenterId"] != null ? Request.QueryString["CenterId"].ToString() : "";
            if (centerId != "")
            {
                int status = -1;
                if (AdminToolManager.IsConsultingProjectAssigned(centerId))
                    status = AdminToolManager.UpdateAvailableConsultingProject(centerId, hddnSelectedConsultingProject.Value);
                else
                    status = AdminToolManager.AddAvailableConsultingProject(centerId, hddnSelectedConsultingProject.Value);
                if (status > 0)
                {
                    //AdminToolManager.UpdateBannerIds(centerId, hddnSelectedBanners.Value);
                    Response.Redirect("/AdminTool/Templates/Center.aspx?centerid=" + centerId + "&tab=8&type=edit");
                }
                else
                {
                    lblError.Text = "Sorry, an error occured saving Consulting & Projects data.";
                    lblError.Visible = true;
                }
            }
            else
            {
                //lblError.Visible = true;
                //lblError.Text = "No Center Id found!!!";
                Response.Redirect("/AdminTool/Templates/Center.aspx?tab=0&type=add");
            }
        }
    }

    private string GetCenterId(string city, string stateCode, string franservId)
    {
        string last3Digit = string.Empty;
        if (franservId.Length > 4)
        {
            last3Digit = franservId.Substring(franservId.Length - 3, 3);
        }
        city = city.Replace(" ", "");
        city = city.Replace(".", "_");
        city = city.Replace(",", "_");

        stateCode = stateCode.Replace(" ", "");
        stateCode = stateCode.Replace(".", "_");
        stateCode = stateCode.Replace(",", "_");

        franservId = franservId.Replace(" ", "");
        franservId = franservId.Replace(".", "_");
        franservId = franservId.Replace(",", "_");

        string fransId = franservId.Length > 4 ? last3Digit : franservId;
        string centerId = city.ToLower() + stateCode.ToLower() + fransId.ToLower();
        centerId = centerId.Trim().ToLower();
        return centerId;
    }

    private void FillCenterData()
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
                    ddlState.SelectedItem.Text = centerData.State;
                    ddlState.SelectedItem.Value = centerData.State;
                }
                else
                {
                    ddlState.SelectedValue = "N/A";
                    txtInternationalState.Text = centerData.StateFullName;
                }

                txtZipcode.Text = centerData.Zipcode;
                //ddlCountryList.SelectedItem.Text = centerData.Country;
                ddlCountryList.SelectedValue = centerData.Country;
                if (centerData.Country != "US")
                {
                    chkZipInternational.Checked = true;
                    chkPhoneInternational.Checked = true;
                    chkFaxInternational.Checked = true;
                }
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
                txtFranservId.Text = centerData.FranservId != null && centerData.FranservId != string.Empty ? centerData.FranservId : "0";
                txtWhitePaperDownloadEmail.Text = centerData.WhitePaperDownloadEmail;
                txtJobApplicationEmail.Text = centerData.JobApplicationEmail;
                txtSubscriptionEmail.Text = centerData.SubscriptionEmail;
                if (!String.IsNullOrWhiteSpace(centerData.ClientLoginUrl))
                    txtClientLoginURL.Text = centerData.ClientLoginUrl;
                txtRequestConsultationEmail.Text = centerData.RequestConsultationEmail;

                ddlConsultation.DataSource = AdminToolManager.GetAllRequestConsultationText(centerData.FransId);
                ddlConsultation.DataTextField = "Title";
                ddlConsultation.DataValueField = "Consultation_Id";
                ddlConsultation.DataBind();


                RequestConsultationWording rcitem = FransDataManager.GetRequestConsultation(centerData.ConsultationMessageId);
                if (rcitem != null)
                {
                    ListItem consultationItem = new ListItem();
                    consultationItem.Value = rcitem.Consultation_Id.ToString();
                    consultationItem.Text = rcitem.Title;

                    int checkindex = ddlConsultation.Items.IndexOf(consultationItem);
                    if (checkindex > -1)
                    {
                        ddlConsultation.SelectedIndex = checkindex;
                    }

                }
                ListItem item = new ListItem() { Text = "None", Value = "0" };
                ddlConsultation.Items.Insert(0, item);
            }
        }
    }

    private void FillBanners()
    {
        long localBannersCategoryId = ConfigHelper.GetValueLong("LocalBannerTaxonomyId");
        long bannerSmartFormId = ConfigHelper.GetValueLong("HomeBannerSmartFormID");
        var localBannersCategory = TaxonomyHelper.GetItem(localBannersCategoryId);
        if (localBannersCategory != null && localBannersCategory.Id > 0)
        {
            ContentTaxonomyCriteria criteria = new ContentTaxonomyCriteria();
            criteria.AddFilter(localBannersCategory.Id);
            criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, bannerSmartFormId);
            var bannersCList = ContentHelper.GetListByCriteria(criteria);

            if (bannersCList != null && bannersCList.Any())
            {
                //var workareaData = FransDataManager.GetFransWorkareaData(centerId, true);
                var existingBannerList = AdminToolManager.GetAvailableBanners(centerId);
                if (existingBannerList != null && existingBannerList != null)
                {
                    var contentList = from c in bannersCList
                                      select new
                                      {
                                          AvailableBanners = existingBannerList.Exists(x => x == c.Id) ? "" : "<div class=\"drag t1\"><span  id='" + c.Id + "'><a target=\"_blank\" href=\"/admintool/templates/content.aspx?id=" + c.Id + "\">" + c.Title + "</a></span></div>",
                                          SelectedBanners = existingBannerList.Exists(x => x == c.Id) ? "<div class=\"drag t1\"><span id='" + c.Id + "'><a target=\"_blank\" href=\"/admintool/templates/content.aspx?id=" + c.Id + "\">" + c.Title + "</a></span></div>" : ""
                                      };
                    lvBanners.DataSource = contentList;
                    lvBanners.DataBind();
                }
                else
                {
                    var contentList = from c in bannersCList
                                      select new
                                      {
                                          AvailableBanners = "<div class=\"drag t1\"><span id='" + c.Id + "'><a target=\"_blank\" href=\"/admintool/templates/content.aspx?id=" + c.Id + "\">" + c.Title + "</a></span></div>",
                                          SelectedBanners = ""
                                      };
                    lvBanners.DataSource = contentList;
                    lvBanners.DataBind();
                }
            }
        }
    }

    private void FillNews()
    {
        long localNewsTaxId = ConfigHelper.GetValueLong("AllNewsLocalTaxId");
        long newsSFId = ConfigHelper.GetValueLong("NewsSmartFormID");
        var localNewsCategory = TaxonomyHelper.GetItem(localNewsTaxId);
        if (localNewsCategory != null && localNewsCategory.Id > 0)
        {
            ContentTaxonomyCriteria criteria = new ContentTaxonomyCriteria();
            criteria.AddFilter(localNewsCategory.Id);
            criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, newsSFId);
            var newsCList = ContentHelper.GetListByCriteria(criteria);

            if (newsCList != null && newsCList.Any())
            {
                var workareaData = FransDataManager.GetFransWorkareaData(centerId, true);
                if (workareaData != null && workareaData.NewsContentIds != null && isNewCenter == false)
                {
                    var existingNewsList = workareaData.NewsContentIds;
                    var contentList = from c in newsCList
                                      select new
                                      {
                                          AvailableNews = existingNewsList.Exists(x => x == c.Id) ? "" : "<div class=\"drag t1\"><span  id='" + c.Id + "'>" + c.Title + "</span></div>",
                                          SelectedNews = existingNewsList.Exists(x => x == c.Id) ? "<div class=\"drag t1\"><span id='" + c.Id + "'>" + c.Title + "</span></div>" : ""
                                      };
                    lvNews.DataSource = contentList;
                    lvNews.DataBind();
                }
                else
                {
                    var contentList = from c in newsCList
                                      select new
                                      {
                                          AvailableNews = "<div class=\"drag t1\"><span id='" + c.Id + "'>" + c.Title + "</span></div>",
                                          SelectedNews = ""
                                      };
                    lvNews.DataSource = contentList;
                    lvNews.DataBind();
                }
            }
        }
    }

    private void FillPartners()
    {
        long localPartnerTaxId = ConfigHelper.GetValueLong("LocalPartnersTaxId");
        long partnersSFId = ConfigHelper.GetValueLong("PartnersSmartFormID");
        var localPartnerCategory = TaxonomyHelper.GetItem(localPartnerTaxId);
        if (localPartnerCategory != null && localPartnerCategory.Id > 0)
        {
            ContentTaxonomyCriteria criteria = new ContentTaxonomyCriteria();
            criteria.AddFilter(localPartnerCategory.Id);
            criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, partnersSFId);
            var partnerCList = ContentHelper.GetListByCriteria(criteria);

            if (partnerCList != null && partnerCList.Any())
            {
                var workareaData = FransDataManager.GetFransWorkareaData(centerId, true);
                if (workareaData != null && workareaData.PartnersContentIds != null && isNewCenter == false)
                {
                    var existingPartnersList = workareaData.PartnersContentIds;
                    List<ContentData> sortedList = new List<ContentData>();
                    //add the selected items on the top
                    foreach (var e in existingPartnersList)
                    {
                        var cData = partnerCList.Where(x => x.Id == e).FirstOrDefault();
                        if (cData != null)
                            sortedList.Add(cData);
                    }
                    //add non-selected items after selected items
                    foreach (var b in partnerCList)
                    {
                        if (!sortedList.Contains(b))
                            sortedList.Add(b);
                    }

                    var contentList = from c in sortedList
                                      select new
                                      {
                                          AvailablePartners = existingPartnersList.Exists(x => x == c.Id) ? "" : "<div class=\"drag t1\"><span  id='" + c.Id + "'>" + c.Title + "</span></div>",
                                          SelectedPartners = existingPartnersList.Exists(x => x == c.Id) ? "<div class=\"drag t1\"><span id='" + c.Id + "'>" + c.Title + "</span></div>" : ""
                                      };
                    lvPartners.DataSource = contentList;
                    lvPartners.DataBind();
                }
                else
                {
                    var contentList = from c in partnerCList
                                      select new
                                      {
                                          AvailablePartners = "<div class=\"drag t1\"><span id='" + c.Id + "'>" + c.Title + "</span></div>",
                                          SelectedPartners = ""
                                      };
                    lvPartners.DataSource = contentList;
                    lvPartners.DataBind();
                }
            }
        }
    }

    private void FillITSolution()
    {
        long itSolutionCategoryId = ConfigHelper.GetValueLong("LocalITSolutionTaxId");
        long smartFormId = ConfigHelper.GetValueLong("ITSolutionSmartFormId");
        var localBannersCategory = TaxonomyHelper.GetItem(itSolutionCategoryId);
        if (localBannersCategory != null && localBannersCategory.Id > 0)
        {
            ContentTaxonomyCriteria criteria = new ContentTaxonomyCriteria();
            criteria.AddFilter(localBannersCategory.Id);
            criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, smartFormId);
            var bannersCList = ContentHelper.GetListByCriteria(criteria);

            if (bannersCList != null && bannersCList.Any())
            {
                //var workareaData = FransDataManager.GetFransWorkareaData(centerId, true);
                var existingITSolutionList = AdminToolManager.GetAvailableITSolutions(centerId);
                if (existingITSolutionList != null && existingITSolutionList.Any())
                {
                    List<ContentData> sortedList = new List<ContentData>();
                    //add the selected items on the top
                    foreach (var e in existingITSolutionList)
                    {
                        var cData = bannersCList.Where(x => x.Id == e).FirstOrDefault();
                        if (cData != null)
                            sortedList.Add(cData);
                    }
                    //add non-selected items after selected items
                    foreach (var b in bannersCList)
                    {
                        if (!sortedList.Contains(b))
                            sortedList.Add(b);
                    }

                    var contentList = from c in sortedList
                                      select new
                                      {
                                          AvailableITSolution = existingITSolutionList.Exists(x => x == c.Id) ? "" : "<div class=\"drag t1\"><span  id='" + c.Id + "'>" + c.Title + "</span></div>",
                                          SelectedITSolution = existingITSolutionList.Exists(x => x == c.Id) ? "<div class=\"drag t1\"><span id='" + c.Id + "'>" + c.Title + "</span></div>" : ""
                                      };
                    lvITSolution.DataSource = contentList;
                    lvITSolution.DataBind();
                }
                else
                {
                    var contentList = from c in bannersCList
                                      select new
                                      {
                                          AvailableITSolution = "<div class=\"drag t1\"><span id='" + c.Id + "'><a target=\"_blank\" href=\"/admintool/templates/content.aspx?id=" + c.Id + "\">" + c.Title + "</a></span></div>",
                                          SelectedITSolution = ""
                                      };
                    lvITSolution.DataSource = contentList;
                    lvITSolution.DataBind();
                }
            }
        }
    }

    private void FillFeaturedSolution()
    {
        long itSolutionCategoryId = ConfigHelper.GetValueLong("LocalITSolutionTaxId");
        long smartFormId = ConfigHelper.GetValueLong("ITSolutionSmartFormId");
        var localBannersCategory = TaxonomyHelper.GetItem(itSolutionCategoryId);
        if (localBannersCategory != null && localBannersCategory.Id > 0)
        {
            ContentTaxonomyCriteria criteria = new ContentTaxonomyCriteria();
            criteria.AddFilter(localBannersCategory.Id);
            criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, smartFormId);
            var bannersCList = ContentHelper.GetListByCriteria(criteria);

            if (bannersCList != null && bannersCList.Any())
            {
                //var workareaData = FransDataManager.GetFransWorkareaData(centerId, true);
                var existingFeaturedSolutionList = AdminToolManager.GetAvailableFeaturedSolutions(centerId);
                if (existingFeaturedSolutionList != null && existingFeaturedSolutionList.Any())
                {
                    List<ContentData> sortedList = new List<ContentData>();
                    //add the selected items on the top
                    foreach (var e in existingFeaturedSolutionList)
                    {
                        var cData = bannersCList.Where(x => x.Id == e).FirstOrDefault();
                        if (cData != null)
                            sortedList.Add(cData);
                    }
                    //add non-selected items after selected items
                    foreach (var b in bannersCList)
                    {
                        if (!sortedList.Contains(b))
                            sortedList.Add(b);
                    }

                    var contentList = from c in sortedList
                                      select new
                                      {
                                          AvailableFeaturedSolution = existingFeaturedSolutionList.Exists(x => x == c.Id) ? "" : "<div class=\"drag t1\"><span  id='" + c.Id + "'>" + c.Title + "</span></div>",
                                          SelectedFeaturedSolution = existingFeaturedSolutionList.Exists(x => x == c.Id) ? "<div class=\"drag t1\"><span id='" + c.Id + "'>" + c.Title + "</span></div>" : ""
                                      };
                    lvFeaturedSolution.DataSource = contentList;
                    lvFeaturedSolution.DataBind();
                }
                else
                {
                    var contentList = from c in bannersCList
                                      select new
                                      {
                                          AvailableFeaturedSolution = "<div class=\"drag t1\"><span id='" + c.Id + "'><a target=\"_blank\" href=\"/admintool/templates/content.aspx?id=" + c.Id + "\">" + c.Title + "</a></span></div>",
                                          SelectedFeaturedSolution = ""
                                      };
                    lvFeaturedSolution.DataSource = contentList;
                    lvFeaturedSolution.DataBind();
                }
            }
        }
    }

    private void FillConsultingProjects()
    {
        long localManageITCategoryId = ConfigHelper.GetValueLong("LocalProjectExpertsTaxId");
        long smartFormId = ConfigHelper.GetValueLong("ProjectExpertsSmartFormId");
        var localBannersCategory = TaxonomyHelper.GetItem(localManageITCategoryId);
        if (localBannersCategory != null && localBannersCategory.Id > 0)
        {
            ContentTaxonomyCriteria criteria = new ContentTaxonomyCriteria();
            criteria.AddFilter(localBannersCategory.Id);
            criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, smartFormId);
            var bannersCList = ContentHelper.GetListByCriteria(criteria);

            if (bannersCList != null && bannersCList.Any())
            {
                //var workareaData = FransDataManager.GetFransWorkareaData(centerId, true);
                var existingConsultingProjectList = AdminToolManager.GetAvailableConsultingProject(centerId);
                if (existingConsultingProjectList != null && existingConsultingProjectList.Any())
                {
                    List<ContentData> sortedList = new List<ContentData>();
                    //add the selected items on the top
                    foreach (var e in existingConsultingProjectList)
                    {
                        var cData = bannersCList.Where(x => x.Id == e).FirstOrDefault();
                        if (cData != null)
                            sortedList.Add(cData);
                    }
                    //add non-selected items after selected items
                    foreach (var b in bannersCList)
                    {
                        if (!sortedList.Contains(b))
                            sortedList.Add(b);
                    }

                    var contentList = from c in sortedList
                                      select new
                                      {
                                          AvailableConsultingProject = existingConsultingProjectList.Exists(x => x == c.Id) ? "" : "<div class=\"drag t1\"><span  id='" + c.Id + "'>" + c.Title + "</span></div>",
                                          SelectedConsultingProject = existingConsultingProjectList.Exists(x => x == c.Id) ? "<div class=\"drag t1\"><span id='" + c.Id + "'>" + c.Title + "</span></div>" : ""
                                      };
                    lvConsultingProject.DataSource = contentList;
                    lvConsultingProject.DataBind();
                }
                else
                {
                    var contentList = from c in bannersCList
                                      select new
                                      {
                                          AvailableConsultingProject = "<div class=\"drag t1\"><span id='" + c.Id + "'><a target=\"_blank\" href=\"/admintool/templates/content.aspx?id=" + c.Id + "\">" + c.Title + "</a></span></div>",
                                          SelectedConsultingProject = ""
                                      };
                    lvConsultingProject.DataSource = contentList;
                    lvConsultingProject.DataBind();
                }
            }
        }
    }
    
    private void FillThirdPartyData()
    {
        var centerId = Request.QueryString["CenterId"] != null ? Request.QueryString["CenterId"].ToString() : "";
        if (centerId != "" && isNewCenter == false)
        {
            var thirdPartyData = FransDataManager.GetFransThirdPartyData(centerId, true);
            if (thirdPartyData != null)
            {
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
                    txtInflectionsUrl.Text = socialMediaData.ITInflectionsUrl;
                }
            }
        }
    }

    private List<long> GetIds(string[] ids)
    {
        List<long> contentIds = new List<long>();
        if (ids != null)
        {
            foreach (string s in ids)
            {
                long id;
                long.TryParse(s, out id);
                if (id > 0)
                    contentIds.Add(id);
            }
        }
        return contentIds;
    }

    private void FillCenterDropDown()
    {
        var allCenters = FransDataManager.GetAllFransLocationDataList(true);
        if (allCenters != null && allCenters.Any())
        {
            var currentCenterId = Request.QueryString["CenterId"] != null ? Request.QueryString["CenterId"].ToString() : "";
            var centerList = from c in allCenters
                             where c.FransId != currentCenterId
                             select new
                             {
                                 CenterName = c.CenterName,
                                 CenterId = c.FransId
                             };
            if (centerList != null)
            {
                centerList = centerList.OrderBy(x => x.CenterName);
                ddlCenterId.DataSource = centerList;
                ddlCenterId.DataTextField = "CenterName";
                ddlCenterId.DataValueField = "CenterId";
                ddlCenterId.DataBind();
            }
        }
        var item = new ListItem();
        item.Text = "-Select One-";
        item.Value = "-Select One-";
        item.Selected = true;
        ddlCenterId.Items.Insert(0, item);
    }

    protected void chkNationlalData_CheckedChanged(object sender, EventArgs e)
    {
        var nationalMediaLinks = AdminDataManager.GetNationalSocialMediaLinks();

        if (chkNationlalData.Checked)
        {
            txtFB.Text = nationalMediaLinks.FaceBookUrl;
            txtTwitterUrl.Text = nationalMediaLinks.TwitterUrl;
            txtGooglePlus.Text = nationalMediaLinks.GooglePlusUrl;
            txtLinkedinUrl.Text = nationalMediaLinks.LinkedInUrl;
            txtStumbleUrl.Text = nationalMediaLinks.StumbleUponUrl;
            txtFlickrUrl.Text = nationalMediaLinks.FlickrUrl;
            txtYouTubeUrl.Text = nationalMediaLinks.YouTubeUrl;
            txtInflectionsUrl.Text = nationalMediaLinks.ITInflectionsUrl;
        }
        else
        {
            txtFB.Text = "";
            txtTwitterUrl.Text = "";
            txtGooglePlus.Text = "";
            txtLinkedinUrl.Text = "";
            txtStumbleUrl.Text = "";
            txtFlickrUrl.Text = "";
            txtYouTubeUrl.Text = "";
            txtInflectionsUrl.Text = "";
        }
    }

    private bool CloneEktFolder(string centerName)
    {
        bool status = false;
        if (!string.IsNullOrEmpty(centerName))
        {
            long modelSiteFId = ConfigHelper.GetValueLong("ModelCenterFolderId");

            var modelFolderData = FolderHelper.GetItem(modelSiteFId);
            if (modelFolderData != null)
            {
                FolderData localFolderData = new FolderData();
                localFolderData.Name = centerName;
                localFolderData.IsTemplateInherited = true;
                localFolderData.ParentId = modelFolderData.ParentId;
                var newFolderData = FolderHelper.Add(localFolderData);             
                if (newFolderData != null && newFolderData.Id > 0)
                {
                    status = true;
                    //copy all the content from model folder to new folder
                    var cc = new Ektron.Cms.Content.ContentCriteria();
                    cc.AddFilter(ContentProperty.FolderId, CriteriaFilterOperator.EqualTo, modelSiteFId);
                    var modelContentList = ContentHelper.GetListByCriteria(cc);

                    if (modelContentList != null)
                    {
                        foreach (var cData in modelContentList)
                        {
                            //copy content
                            var contentId = ContentHelper.CopyEktContent(cData.Id, newFolderData.Id, 1033, true);

                            //update alias name
                            if (!cData.Quicklink.Contains(".aspx"))
                            {
                                string currentContentAlias = cData.Quicklink;
                                string modifiedAlias = currentContentAlias.ToLower().Replace("model", newFolderData.Name);
                                AliasHelper.AddManualAlias(modifiedAlias, contentId);
                            }

                            //update metada
                            var contentModelKeyName = TLITUtility.GetModelSiteConfigKey(cData.Id);
                            var contentData = ContentHelper.GetContentById(contentId);
                            contentData.MetaData = new ContentMetaData[1];
                            contentData.MetaData[0] = new ContentMetaData()
                            {
                                Id = ConfigHelper.GetValueLong("ModelSiteContentIdKeyMetaDataId"),
                                Text = contentModelKeyName
                            };
                            ContentHelper.UpdateContent(contentData);
                        }
                    }
                }
            }
        }
        return status;
    }

    private void AddContentAliasForNewClonedCenter(string centerName)
    {
        if (!string.IsNullOrEmpty(centerName))
        {
            ContentCriteria cc = new ContentCriteria();

            //update all itSolution content
            long itSolutionSmartFormId = ConfigHelper.GetValueLong("ITSolutionSmartFormId");
            cc = new ContentCriteria();
            cc.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, itSolutionSmartFormId);
            cc.PagingInfo = new PagingInfo(FransDataManager.GetCustomApiPageSize());
            var itSolutionList = ContentHelper.GetListByCriteria(cc);
            if (itSolutionList != null && itSolutionList.Any())
            {
                foreach (var s in itSolutionList)
                {
                    var aliasContent = AliasHelper.GetAlias(s.Id, 1033, EkEnumeration.TargetType.Content);
                    if (aliasContent != null)
                    {
                        string centerContentAlias = centerName + "/" + aliasContent.Alias;
                        AliasHelper.AddManualAlias(centerContentAlias, s.Id, false);
                    }
                }
            }

            //update all projectExpert content
            long projectExpertSmartFormId = ConfigHelper.GetValueLong("ProjectExpertsSmartFormId");
            cc = new ContentCriteria();
            cc.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, projectExpertSmartFormId);
            cc.PagingInfo = new PagingInfo(FransDataManager.GetCustomApiPageSize());
            var projectExpertList = ContentHelper.GetListByCriteria(cc);
            if (projectExpertList != null && projectExpertList.Any())
            {
                foreach (var px in projectExpertList)
                {
                    var aliasContent = AliasHelper.GetAlias(px.Id, 1033, EkEnumeration.TargetType.Content);
                    if (aliasContent != null)
                    {
                        string centerContentAlias = centerName + "/" + aliasContent.Alias;
                        AliasHelper.AddManualAlias(centerContentAlias, px.Id, false);
                    }
                }
            }

            //update all case studies content
            long caseStudiesSmartFormId = ConfigHelper.GetValueLong("CaseStudiesSmartFormID");
            cc = new ContentCriteria();
            cc.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, caseStudiesSmartFormId);
            cc.PagingInfo = new PagingInfo(FransDataManager.GetCustomApiPageSize());
            var caseStudiesList = ContentHelper.GetListByCriteria(cc);
            if (caseStudiesList != null && caseStudiesList.Any())
            {
                foreach (var cs in caseStudiesList)
                {
                    var aliasContent = AliasHelper.GetAlias(cs.Id, 1033, EkEnumeration.TargetType.Content);
                    if (aliasContent != null)
                    {
                        string centerContentAlias = centerName + "/" + aliasContent.Alias;
                        AliasHelper.AddManualAlias(centerContentAlias, cs.Id, false);
                    }
                }
            }


            //update all whitePapers content
            long whitePapersSmartFormId = ConfigHelper.GetValueLong("BriefsAndWhitepapersSmartFormID");
            cc = new ContentCriteria();
            cc.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, whitePapersSmartFormId);
            cc.PagingInfo = new PagingInfo(FransDataManager.GetCustomApiPageSize());
            var whitePapersList = ContentHelper.GetListByCriteria(cc);
            if (whitePapersList != null && whitePapersList.Any())
            {
                foreach (var wp in whitePapersList)
                {
                    var aliasContent = AliasHelper.GetAlias(wp.Id, 1033, EkEnumeration.TargetType.Content);
                    if (aliasContent != null)
                    {
                        string centerContentAlias = centerName + "/" + aliasContent.Alias;
                        AliasHelper.AddManualAlias(centerContentAlias, wp.Id, false);
                    }
                }
            }

            //update all news content
            long newsSmartFormId = ConfigHelper.GetValueLong("NewsSmartFormID");
            cc = new ContentCriteria();
            cc.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, newsSmartFormId);
            cc.PagingInfo = new PagingInfo(FransDataManager.GetCustomApiPageSize());
            var newsList = ContentHelper.GetListByCriteria(cc);
            if (newsList != null && newsList.Any())
            {
                foreach (var n in newsList)
                {
                    var aliasContent = AliasHelper.GetAlias(n.Id, 1033, EkEnumeration.TargetType.Content);
                    if (aliasContent != null)
                    {
                        string centerContentAlias = centerName + "/" + aliasContent.Alias;
                        AliasHelper.AddManualAlias(centerContentAlias, n.Id, false);
                    }
                }
            }

            //add a URL-redirect
            RedirectManager redirectMngr = new RedirectManager(Ektron.Cms.Framework.ApiAccessMode.Admin);
            string localCenterDefaultPageUrl = "/" + centerName + "/default.aspx";
            string localCenterTargetPageUrl = "/" + centerName + "/";
            int permanentlyCode = 301;

            var rc = new RedirectCriteria();
            rc.AddFilter(RedirectProperty.SourceURL, CriteriaFilterOperator.EqualTo, localCenterDefaultPageUrl);
            rc.PagingInfo = new PagingInfo(FransDataManager.GetCustomApiPageSize());
            var list = redirectMngr.GetList(rc);
            if (list == null || list.Count == 0)
            {
                var newRedirect = new RedirectData();
                newRedirect.Active = true;
                newRedirect.SiteId = 0; //default site
                newRedirect.SourceURL = localCenterDefaultPageUrl;
                newRedirect.TargetURL = localCenterTargetPageUrl;
                newRedirect.StatusCode = (HttpStatusCode)permanentlyCode;
                var updatedData = redirectMngr.Add(newRedirect);
            }

        }
    }
        
}