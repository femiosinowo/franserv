using System;
using System.Linq;
using System.Web.UI;
using SignalGraphics.CMS;
using System.Text;
using System.Xml;
using Ektron.Cms.Content;
using Ektron.Cms.Common;
using Ektron.Cms.Instrumentation;

public partial class login_national : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.GetNationalOfficeDetails();            
        }
    }

    /// <summary>
    /// Gets the Franchise Data
    /// </summary>
    private void GetNationalOfficeDetails()
    {
        long cId = ConfigHelper.GetValueLong("AboutSirSpeedyCId");
        XmlDocument contentXML = new XmlDocument();
        long SmartFormXMLConfig = ConfigHelper.GetValueLong("CompanyInfoSmartFormID");

        ContentCriteria criteria = new ContentCriteria();
        criteria.AddFilter(ContentProperty.Id, CriteriaFilterOperator.EqualTo, cId);
        criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, SmartFormXMLConfig);
        var contents = ContentHelper.GetListByCriteria(criteria);

        if (contents != null && contents.Count > 0)
        {
            var contentData = contents.SingleOrDefault();
            FransData nationalData = new FransData();
            StringBuilder sbAdress = new StringBuilder();
            if (contentData != null && contentData.Html != string.Empty)
            {
                try
                {
                    contentXML.LoadXml(contentData.Html);
                    XmlNodeList xnList = contentXML.SelectNodes("/root");

                    string address1 = nationalData.Address1 = xnList[0]["address1"].InnerXml;
                    sbAdress.Append(address1);

                    string address2 = nationalData.Address2 = xnList[0]["address2"].InnerXml;
                    sbAdress.Append(" " + address2);

                    string city = nationalData.City = xnList[0]["city"].InnerXml;
                    sbAdress.Append(" " + city);

                    string state = nationalData.State = xnList[0]["state"].InnerXml;
                    sbAdress.Append(" " + state);

                    string zipcode = nationalData.Zipcode = xnList[0]["zipcode"].InnerXml;
                    sbAdress.Append("-" + zipcode);

                    string daysOperation = xnList[0]["daysOperation"].InnerXml;
                    string hoursOperation = xnList[0]["hoursOperation"].InnerXml;
                    string phone = xnList[0]["phone"].InnerXml;
                    string fax = xnList[0]["fax"].InnerXml;
                    string email = xnList[0]["email"].InnerXml;

                    StringBuilder contactinfo = new StringBuilder();
                    contactinfo.Append("<ul class='contact_info'>");
                    contactinfo.Append("<li class='contact-icon-location'>");
                    contactinfo.Append("<span>");
                    contactinfo.Append(address1);
                    contactinfo.Append("<br>");
                    contactinfo.Append(city + "  " + state + " " + zipcode);
                    contactinfo.Append("</span>");
                    contactinfo.Append("</li>");
                    contactinfo.Append("</ul>");
                    contactinfo.Append("<hr />");

                    contactinfo.Append("<ul class='contact_info'>");
                    if (!string.IsNullOrEmpty(phone))
                        contactinfo.Append("<li class='contact-icon-phone'><span>" + phone + "</span></li>");

                    if (!string.IsNullOrEmpty(fax))
                        contactinfo.Append("<li class='contact-icon-fax'><span>" + fax + "</span></li>");

                    if (!string.IsNullOrEmpty(email))
                        contactinfo.Append("<li class='contact-icon-email'><span><a href='" + email + "'>" + email + "</a></span></li>");

                    contactinfo.Append("</ul>");
                    contactinfo.Append("<hr />");

                    contactinfo.Append("<ul class='contact_info'>");
                    contactinfo.Append("<li class='contact-icon-hours'>");
                    contactinfo.Append("<span>Hours<br />");
                    contactinfo.Append(daysOperation);
                    contactinfo.Append("<br />");
                    contactinfo.Append(hoursOperation);
                    contactinfo.Append("</span>");
                    contactinfo.Append("</li>");
                    contactinfo.Append("</ul>");

                    var updatedAddress = AdminDataManager.GetNationalCompleteAddress(nationalData);

                    litFranchiseContactInfo.Text = contactinfo.ToString();
                    googleMapImage.Src = string.Format(FransDataManager.GoogleStaticLargeImagePath, "http://" + Request.ServerVariables["SERVER_NAME"], updatedAddress.Latitude, updatedAddress.Longitude);

                    string address = sbAdress.ToString().Replace(" ", "+");
                    viewDirectionDesktop.HRef = string.Format(FransDataManager.GoogleViewDirectionApiDesktop, address);
                    viewDirectionMobile.HRef = string.Format(FransDataManager.GoogleViewDirectionApiMobile, address);
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }
        }
    }
}