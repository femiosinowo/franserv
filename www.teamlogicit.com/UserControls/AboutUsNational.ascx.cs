using System;
using System.Data;
using System.Xml;
using System.Xml.XPath;
using System.Linq;
using TeamLogic.CMS;
using Ektron.Cms.Content;
using Ektron.Cms.Common;
using Ektron.Cms.Instrumentation;
using System.Xml.Linq;
using System.Text;

public partial class UserControls_AboutUsNational : System.Web.UI.UserControl
{
    public string StartFranchiseBackgroundImage = "/images/noc_bkg.jpg";

    protected void Page_Init(object sender, EventArgs e)
    {
        cbStartFranchise.DefaultContentID = ConfigHelper.GetValueLong("AboutUsNationalStartFransCId");
        cbStartFranchise.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbStartFranchise.Fill();

        if (cbStartFranchise.EkItem != null && !string.IsNullOrEmpty(cbStartFranchise.EkItem.Image) && cbStartFranchise.EkItem.Image.ToLower().IndexOf("workarea") <= -1)
            StartFranchiseBackgroundImage = cbStartFranchise.EkItem.Image;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetCompanyLocationContent();
            GetCompanyInfoNational();
        }

        StartFranchise_img.Attributes.Add("data-image", StartFranchiseBackgroundImage);
        StartFranchise_img.Attributes.Add("data-image-mobile", StartFranchiseBackgroundImage);
    }

    private void GetCompanyLocationContent()
    {
        long cId = ConfigHelper.GetValueLong("AboutSirSpeedyNationalCId");
        XmlDocument contentXML = new XmlDocument();
        long SmartFormXMLConfig = ConfigHelper.GetValueLong("CompanyInfoSmartFormID");

        //get all contents in the specified folder
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

                    nationalData.Address1 = xnList[0]["address1"].InnerXml;
                    nationalData.Address2 = xnList[0]["address2"].InnerXml;
                    nationalData.City = xnList[0]["city"].InnerXml;
                    nationalData.State = xnList[0]["state"].InnerXml;
                    nationalData.Zipcode = xnList[0]["zipcode"].InnerXml;
                    //nationalData. = xnList[0]["daysOperation"].InnerXml;
                    nationalData.HoursOfOperation = xnList[0]["hoursOperation"].InnerXml;
                    nationalData.PhoneNumber = xnList[0]["phone"].InnerXml;
                    nationalData.FaxNumber = xnList[0]["fax"].InnerXml;
                    nationalData.Email = xnList[0]["email"].InnerXml;
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }

            if (nationalData != null)
            {
                StringBuilder contactinfo = new StringBuilder();
                contactinfo.Append("<li class=\"address\">");
                contactinfo.Append(nationalData.Address1);
                if (!string.IsNullOrEmpty(nationalData.Address2))
                {
                    contactinfo.Append(", ");
                    contactinfo.Append(nationalData.Address2);
                }
                contactinfo.Append(", ");
                contactinfo.Append(nationalData.City + ",  " + nationalData.State + " " + nationalData.Zipcode);
                contactinfo.Append("</li>");
                litLocAddress.Text = contactinfo.ToString();

                ltrPhoneNumber.Text = "<li class=\"telephone\"><a href=\"tel:+" + nationalData.PhoneNumber + "\">" + nationalData.PhoneNumber + "</a></li>";
                ltrEmailAddress.Text = "<li class=\"email\"><a href='mailto:" + nationalData.Email + "'>" + nationalData.Email + "</a></li>";

                var updatedAddress = AdminDataManager.GetNationalCompleteAddress(nationalData);
                hiddenCenterLat.Value = updatedAddress.Latitude;
                hiddenCenterLong.Value = updatedAddress.Longitude;
            }
        }
    }

    private void GetCompanyInfoNational()
    {
        long cId = ConfigHelper.GetValueLong("AboutSirSpeedyNationalCId");
        long SmartFormXMLConfig = ConfigHelper.GetValueLong("CompanyInfoSmartFormID");
        ContentCriteria cc = new ContentCriteria();
        cc.AddFilter(ContentProperty.Id, CriteriaFilterOperator.EqualTo, cId);
        cc.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, SmartFormXMLConfig);
        var dataList = ContentHelper.GetListByCriteria(cc);
        if (dataList != null && dataList.Count > 0)
        {
            try
            {
                var cData = from obj in dataList
                            select new
                            {
                                Id = obj.Id,
                                Html = XElement.Parse(obj.Html)
                            };

                var data = from obj in cData
                           select new
                           {
                               Title = obj.Html.XPathSelectElement("title") != null ? obj.Html.XPathSelectElement("title").Value : string.Empty,
                               DespSection1 = obj.Html.XPathSelectElement("tagline") != null ? TLITUtility.ExtractNodeHtml(obj.Html.XPathSelectElement("tagline")) : string.Empty,
                               DespSection2 = obj.Html.XPathSelectElement("statement") != null ? TLITUtility.ExtractNodeHtml(obj.Html.XPathSelectElement("statement")) : string.Empty,
                               Description = obj.Html.XPathSelectElement("disclaimer") != null ? TLITUtility.ExtractNodeHtml(obj.Html.XPathSelectElement("disclaimer")) : string.Empty
                           };
                lvAboutUs.DataSource = data;
                lvAboutUs.DataBind();

            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
        }
    }

}