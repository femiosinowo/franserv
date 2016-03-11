using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.Script.Serialization;

using TeamLogic.CMS;
using Ektron.Cms.Instrumentation;

public partial class find_locator : PageBase
{    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.FillCountryList();
            this.GetLocationsForCountry("US");

            if (Request.QueryString.HasKeys())
            {
                if (!string.IsNullOrEmpty(Request.QueryString["location"]) && !string.IsNullOrEmpty(Request.QueryString["distance"]))
                    loadAllLocations.Value = "false";
                else
                    loadAllLocations.Value = "true";

                string location = string.Empty;
                string distance = string.Empty;
                if (!string.IsNullOrEmpty(Request.QueryString["location"]))
                {
                    location = Request.QueryString["location"];
                    location = location.Replace("+", " ");
                }
                if (!string.IsNullOrEmpty(Request.QueryString["distance"]))
                {
                    distance = Request.QueryString["distance"];
                }

                FindLocationCity.Value = location;
                find_location_distance.Value = distance;
                hddnDistance.Value = distance;
            }
        }
    }

    protected void ddlCountryList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCountryList.SelectedIndex > 0)
        {
            var selectedCountryCode = ddlCountryList.SelectedItem.Value;
            this.GetLocationsForCountry(selectedCountryCode);
        }
        else
        {
            this.GetLocationsForCountry("US");
        }
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "appendLocationId", "<script type=\"text/javascript\">window.location.href = \"/find-locator/#all_locations\";</script>", false);
    }

    private void FillCountryList()
    {
        var countryListData = AdminToolManager.GetAllCountryList();
        if (countryListData != null && countryListData.Any())
        {
            ddlCountryList.DataSource = countryListData;
            ddlCountryList.DataTextField = "CountryName";
            ddlCountryList.DataValueField = "CountryCode";
            ddlCountryList.DataBind();
            ListItem item = new ListItem();
            item.Text = "- Choose Another Country -";
            item.Value = "";
            ddlCountryList.Items.Insert(0, item);
        }
    }

    private void GetLocationsForCountry(string countryCode)
    {
        if (countryCode != string.Empty)
        {
            var allLocationData = FransDataManager.GetAllFransLocationDataList();
            if (allLocationData != null && allLocationData.Any())
            {
                var countryList = allLocationData.Where(x => x.Country == countryCode).ToList();
                //as per client append canada list too
                var canadaCenters = allLocationData.Where(x => x.Country == "CA").ToList();
                if(canadaCenters != null && canadaCenters.Any())
                {
                    countryList.AddRange(canadaCenters);
                }

                if (countryList != null && countryList.Any())
                {
                    var filteredList = countryList.OrderBy(x => x.StateFullName).ToList();
                    var stateGroupList = filteredList.GroupBy(x => x.StateFullName);

                    if (stateGroupList != null)
                    {
                        string domainName = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
                        string localSiteUrl = string.Empty;
                        StringBuilder sb = new StringBuilder();
                        foreach (var state in stateGroupList)
                        {
                            sb.Append("<h4>" + state.Key + "</h4>");
                            sb.Append("<ul class=\"location_list\">");
                            foreach (var city in state)
                            {
                                var cityData = city;
                                //as per client hide the following location from the page
                                if (cityData.FransId != "arlingtonva999" && cityData.FransId != "missionviejoca002")
                                {                                   
                                    localSiteUrl = "/" + cityData.FransId + "/";
                                    sb.Append("<li><a href=\"" + localSiteUrl + "\">" + cityData.City + ", <span>" + cityData.Address1 + "</span></a></li>");
                                }
                            }
                            sb.Append("</ul>");
                        }
                        ltrStateData.Text = sb.ToString();
                    }
                }
                else
                {
                    ltrStateData.Text = "No locations found for the selected Country.";
                }
            }
        }
    }
}