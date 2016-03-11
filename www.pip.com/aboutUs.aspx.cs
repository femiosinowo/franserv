using System;
using System.Data;
using System.Xml;
using System.Xml.XPath;
using System.Linq;
using SirSpeedy.CMS;
using Ektron.Cms.Content;
using Ektron.Cms.Common;
using Ektron.Cms.Instrumentation;
using System.Xml.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class about_national : PageBase
{
    protected string sbAddress = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {       
        if (!Page.IsPostBack)
        {           
            //if (FransDataManager.IsFranchiseSelected() == false)
            //{
            UxAwards.DataSource = GetCompanyAwardsSliderContent();
            UxAwards.DataBind();

            UxCompanyLocation.DataSource = GetCompanyLocationContent();
            UxCompanyLocation.DataBind();
            //corporateLocation.Visible = true;
            GetCompanyInfoNational();
            //}
            //else
            //{
            //    GetCompanyInfoLocal();
            //    GetLocalAwardsSliderContent();
            //    corporateLocation.Visible = false;
            //}

            if (FransDataManager.IsFranchiseSelected())
            {
                desktopNavLocal.Visible = true;
                desktopNavNational.Visible = false;
                aboutUsSubNav.Attributes.Add("Class", "sub_navigation about-us-local");
                Master.BodyClass = " about-pip-local "; 
            }
            else
            {
                desktopNavLocal.Visible = false;
                desktopNavNational.Visible = true;
                aboutUsSubNav.Attributes.Add("Class", "sub_navigation about-us");
                 Master.BodyClass = " company-info ";
            }
        }
    }   
    
    private DataTable GetCompanyAwardsSliderContent()
    {
        DataTable DTSource = new DataTable();
        DataTable sortedDT = new DataTable();
        long folderID = ConfigHelper.GetValueLong("CompanyAwardsFolderID");
        XmlDocument contentXML = new XmlDocument();
        long SmartFormXMLConfig = ConfigHelper.GetValueLong("CompanyAwardsSmartFormID");
        int counter = 0;

        DTSource.Columns.Add("title");
        DTSource.Columns.Add("achievement");
        DTSource.Columns.Add("counter");

        //get all contents in the specified folder
        ContentCriteria criteria = new ContentCriteria();
        criteria.AddFilter(ContentProperty.FolderId, CriteriaFilterOperator.EqualTo, folderID);
        criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, SmartFormXMLConfig);
        var contents = ContentHelper.GetListByCriteria(criteria);

        if (contents != null && contents.Count > 0)
        {
            foreach (Ektron.Cms.ContentData contentData in contents)
            {
                try
                {
                    contentXML.LoadXml(contentData.Html);
                XmlNodeList xnList = contentXML.SelectNodes("/root");

                string title = xnList[0]["title"].InnerXml;
                string achievement = xnList[0]["achievement"].InnerXml;
                counter++;

                DTSource.Rows.Add(title, achievement, counter);
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }
            DataView DVPSMMSort = DTSource.DefaultView;
            DVPSMMSort.Sort = "counter desc";
            sortedDT = DVPSMMSort.ToTable();
        }

        return sortedDT;
    }

    private void GetLocalAwardsSliderContent()
    {
       //todo
    }

    private DataTable GetCompanyLocationContent()
    {
        DataTable DTSource = new DataTable();
        DataTable sortedDT = new DataTable();
        long cId = ConfigHelper.GetValueLong("AboutSirSpeedyCId");
        XmlDocument contentXML = new XmlDocument();
        long SmartFormXMLConfig = ConfigHelper.GetValueLong("CompanyInfoSmartFormID");       

        DTSource.Columns.Add("address1");
        DTSource.Columns.Add("address2");
        DTSource.Columns.Add("city");
        DTSource.Columns.Add("state");
        DTSource.Columns.Add("zipcode");
        DTSource.Columns.Add("phone");
        DTSource.Columns.Add("fax");
        DTSource.Columns.Add("email");
        DTSource.Columns.Add("daysOperation");
        DTSource.Columns.Add("hoursOperation");
        DTSource.Columns.Add("counter");

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

                    string address1 = nationalData.Address1 = xnList[0]["address1"].InnerXml;
                    sbAdress.Append(address1);

                    string address2 = nationalData.Address2 = xnList[0]["address2"].InnerXml;
                    sbAdress.Append(" "+address2);

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

                    DTSource.Rows.Add(address1, address2, city, state, zipcode, phone, fax, email, daysOperation, hoursOperation);
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }

            DataView DVPSMMSort = DTSource.DefaultView;
            DVPSMMSort.Sort = "counter desc";
            sortedDT = DVPSMMSort.ToTable();

            if(nationalData != null && sbAdress.Length > 0)
            {
                
                var updatedAddress = AdminDataManager.GetNationalCompleteAddress(nationalData);
                hiddenCenterLat.Value = updatedAddress.Latitude;
                hiddenCenterLong.Value = updatedAddress.Longitude;
                string address = sbAdress.ToString().Replace(" ", "+");
                sbAddress = address;
            }            
        }
        return sortedDT;
    }

    protected void UxCompanyLocation_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        var directions_lb = (HtmlAnchor)e.Item.FindControl("directions_lb");

        string directionsLink = "https://www.google.com/maps?daddr=q={0}";

        directions_lb.HRef = string.Format(directionsLink, sbAddress);
    }

    private void GetCompanyInfoNational()
    {
        long cId = ConfigHelper.GetValueLong("AboutSirSpeedyCId");
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
                               SubTitle = obj.Html.XPathSelectElement("teaser") != null ? obj.Html.XPathSelectElement("teaser").Value : string.Empty,
                               Description = obj.Html.XPathSelectElement("content") != null ? obj.Html.XPathSelectElement("content").ToString() : string.Empty,
                               videoSRC = obj.Html.XPathSelectElement("videoSRC/a") != null ? obj.Html.XPathSelectElement("videoSRC/a").Attribute("href").Value : string.Empty,
                               statement = obj.Html.XPathSelectElement("statement") != null ? obj.Html.XPathSelectElement("statement").ToString() : string.Empty,
                               Disclaimer = obj.Html.XPathSelectElement("disclaimer") != null ? obj.Html.XPathSelectElement("disclaimer").ToString() : string.Empty,
                               ImagePath = obj.Html.XPathSelectElement("companyLogo/img") != null ? obj.Html.XPathSelectElement("companyLogo/img").Attribute("src").Value : string.Empty,
                               videoImagePath = obj.Html.XPathSelectElement("videoSRC/a") == null && obj.Html.XPathSelectElement("contentImage/img") != null ? obj.Html.XPathSelectElement("contentImage/img").Attribute("src").Value : string.Empty
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

    private void GetCompanyInfoLocal()
    {
        var centerId = FransDataManager.GetFranchiseId();
        if (centerId != string.Empty)
        {
            var centerAboutUsContent = FransDataManager.GetAboutUsContent(centerId);
            if (centerAboutUsContent != null)
            {
                lvAboutUs.DataSource = centerAboutUsContent;
                lvAboutUs.DataBind();
            }
        }
    }
    
}