using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SirSpeedy.CMS;
using System.Xml.Linq;
using System.Xml.XPath;
using Ektron.Cms.Instrumentation;

public partial class UserControls_FooterBrandsAndLinks : System.Web.UI.UserControl
{
    /// <summary>
    /// Page init event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Init(object sender, EventArgs e)
    {
        cbCopyRight.DefaultContentID = ConfigHelper.GetValueLong("SiteCopyRightContentId");
        cbCopyRight.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbCopyRight.Fill();

        if(FransDataManager.IsFranchiseSelected())
        {
            siteLangSelector.Visible = true;
        }
    }

    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.GetPartners();
        }
    }


    /// <summary>
    /// This method is used to get all the partners data
    /// </summary>
    private void GetPartners()
    {
        int displayCount = ConfigHelper.GetValueInt("FooterBrandsDisplayCount");
        if (displayCount == 0)
            displayCount = 4;

        var partnersData = SiteDataManager.GetPartnersContent();
        if (partnersData != null && partnersData.Count > 0)
        {
            try
            {
                var displayData = partnersData.Take(displayCount);
                var contentData = from obj in displayData
                                  select new
                                  {
                                      Id = obj.Id,
                                      Quicklink = obj.Quicklink,
                                      Html = XElement.Parse(obj.Html)
                                  };

                var brands = from obj in contentData
                             select new
                             {
                                 //Link = obj.Html.XPathSelectElement("/url/a") != null ? obj.Html.XPathSelectElement("/url/a").Attribute("href").Value : string.Empty,
                                 Link = "/partners/",
                                 ImagePath = obj.Html.XPathSelectElement("/image/img") != null ? obj.Html.XPathSelectElement("/image/img").Attribute("src").Value : string.Empty,
                                 Title = obj.Html.XPathSelectElement("companyName") != null ? obj.Html.XPathSelectElement("companyName").Value : string.Empty
                             };
                lvBrands.DataSource = brands;
                lvBrands.DataBind();
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
        }
    }

}