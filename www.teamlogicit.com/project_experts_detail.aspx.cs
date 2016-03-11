using Ektron.Cms.Instrumentation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using TeamLogic.CMS;
using System.Text;
using Ektron.Cms;

public partial class project_experts_detail : System.Web.UI.Page
{
    public string HereToHelpBackgroundImage = "/images/lets_connect_bg.jpg";

    protected void Page_Init(object sender, EventArgs e)
    {
        if (FransDataManager.IsFranchiseSelected())
            cbHereToHelp.DefaultContentID = ConfigHelper.GetValueLong("LocalProjectExpertDetailPageHereToHelpCId");
        else
            cbHereToHelp.DefaultContentID = ConfigHelper.GetValueLong("ProjectExpertDetailPageHereToHelpCId");
        cbHereToHelp.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbHereToHelp.Fill();

        if (cbHereToHelp.EkItem != null && !string.IsNullOrEmpty(cbHereToHelp.EkItem.Image) && cbHereToHelp.EkItem.Image.ToLower().IndexOf("workarea") <= -1)
            HereToHelpBackgroundImage = cbHereToHelp.EkItem.Image;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            long contentId = 0;
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                contentId = Convert.ToInt64(Request.QueryString["id"]);
            }

            this.GetProjectExpertsContent();
        }

        here_to_help_img.Attributes.Add("data-image", HereToHelpBackgroundImage);
        here_to_help_img.Attributes.Add("data-image-mobile", HereToHelpBackgroundImage);
    }

    private void GetProjectExpertsContent()
    {
        var contents = SiteDataManager.GetProjectExpertsContent();
        if (contents != null && contents.Count > 0)
        {
            long contentIdQueryString = 0;
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                contentIdQueryString = Convert.ToInt64(Request.QueryString["id"]);
            }

            ContentData defaultContent = new ContentData();
            List<ContentData> cList = new List<ContentData>();
            if (contentIdQueryString > 0)
            {
                defaultContent = contents.Where(x => x.Id == contentIdQueryString).FirstOrDefault();
                if (defaultContent != null && defaultContent.Id > 0)
                    cList.Add(defaultContent);
            }

            foreach (var c in contents)
            {
                if (defaultContent != null && c.Id != defaultContent.Id)
                    cList.Add(c);
            }


            try
            {
                var contentData = from obj in cList
                                  select new
                                  {
                                      Id = obj.Id,
                                      Quicklink = obj.Quicklink,
                                      Html = XElement.Parse(obj.Html)
                                  };
                var expertContent = from obj in contentData
                                    select new
                                    {
                                        id = obj.Id,
                                        titletxt = obj.Html.XPathSelectElement("title") != null ? Regex.Replace(TLITUtility.ExtractNodeHtml(obj.Html.XPathSelectElement("title")), "<.*?>", " ") : "",
                                        sec1desc = obj.Html.XPathSelectElement("descpSection1") != null ? TLITUtility.ExtractNodeHtml(obj.Html.XPathSelectElement("descpSection1")) : "",
                                        sec2desc = obj.Html.XPathSelectElement("descpSection2") != null ? TLITUtility.ExtractNodeHtml(obj.Html.XPathSelectElement("descpSection2")) : "",
                                        fulldesc = obj.Html.XPathSelectElement("fullDescription") != null ? TLITUtility.ExtractNodeHtml(obj.Html.XPathSelectElement("fullDescription")) : "",
                                        testimonials = GetTestimonials()
                                    };

                lvProjectExpertsDetail.DataSource = expertContent;
                lvProjectExpertsDetail.DataBind();

            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
        }
    }

    private List<Testimonials> GetTestimonials()
    {
        List<Testimonials> list = null;
        var allTestimonials = SiteDataManager.GetTestimonials();
        if (allTestimonials != null && allTestimonials.Any())
        {
            list = allTestimonials.Where(x => x.IsProjectExpert == true).Take(4).ToList();
        }
        return list;
    }

    protected string FormatLastName(string firstName, string lastName)
    {
        return Utility.FormatTestimonialLastName(firstName, lastName);
    }

    protected string  FormatTitleCompany(object title, object company)
    {
        return Utility.FormatTestimonialTitleAndCompany(title, company);
    }

    protected string FormatContactDetails(object emailAdress, object phonenumber)
    {
        return Utility.FormatTestimonialContactDetails(emailAdress, phonenumber);
    }

}