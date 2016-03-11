using System;
using System.Linq;
using TeamLogic.CMS;
using Ektron.Cms.Instrumentation;
using System.Xml.Linq;
using System.Xml.XPath;

public partial class UserControls_HomePageCaseStudies : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.GetCaseStudies();
        }
    }

    private void GetCaseStudies()
    {
        var caseStudiesContent = SiteDataManager.GetCaseStudiesContentForHomePage();
        if (caseStudiesContent != null && caseStudiesContent.Any())
        {
            try
            {
                var homePageItems = caseStudiesContent.Take(8);
                var contentData = from obj in homePageItems
                                  select new
                                  {
                                      Id = obj.Id,
                                      Quicklink = obj.Quicklink,
                                      Html = XElement.Parse(obj.Html)
                                  };

                var caseStudyItems = from obj in contentData
                                     select new
                                     {
                                         Title = obj.Html.XPathSelectElement("title") != null ? TLITUtility.ExtractNodeHtml(obj.Html.XPathSelectElement("title")) : string.Empty,
                                         SubTitle = obj.Html.XPathSelectElement("desc") != null ? TLITUtility.ExtractNodeHtml(obj.Html.XPathSelectElement("desc")) : string.Empty,
                                         MainImagePath = obj.Html.XPathSelectElement("/image/img") != null ? obj.Html.XPathSelectElement("/image/img").Attribute("src").Value : string.Empty,
                                         ImagePath = obj.Html.XPathSelectElement("/iconImage/img") != null ? obj.Html.XPathSelectElement("/iconImage/img").Attribute("src").Value : string.Empty,
                                         Link = obj.Quicklink
                                     };

                lvCaseStudies.DataSource = caseStudyItems;
                lvCaseStudies.DataBind();
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
        }
    }
    
}