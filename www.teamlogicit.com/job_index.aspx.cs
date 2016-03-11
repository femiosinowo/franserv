using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

using TeamLogic.CMS;
using Ektron.Cms;
using Ektron.Cms.Instrumentation;
using Ektron.Cms.Content;
using Ektron.Cms.Common;


public partial class job_index : PageBase
{
    /// <summary>
    /// Page Init
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Init(object sender, EventArgs e)
    {
        cbJoinToday.DefaultContentID = ConfigHelper.GetValueLong("CareerPageJoinTodayCId");
        cbJoinToday.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbJoinToday.Fill();

        cbJobProfile.DefaultContentID = ConfigHelper.GetValueLong("CareerPageProfileIntroCId");
        cbJobProfile.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbJobProfile.Fill();
    }

    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!FransDataManager.IsFranchiseSelected())
            uxJobSearch1.Visible = false;
        if (!Page.IsPostBack)
        {
            this.GetProfileData();
        }
    }

    private void GetProfileData()
    {
        long profileTaxId = ConfigHelper.GetValueLong("ManageCarrersTaxId");
        var careersTree = TaxonomyHelper.GetTaxonomyTree(profileTaxId, 2, false);

        if (careersTree != null && careersTree.HasChildren)
        {
            this.LoadProfileContent(careersTree);            
        }
    }

    private void LoadProfileContent(TaxonomyData taxTree)
    {
        try
        {
            var taxData = from obj in taxTree.Taxonomy
                          select new
                          {
                              Name = obj.Name,
                              BackGroundImg = obj.TaxonomyItems != null && obj.TaxonomyItems.Length > 0 ? obj.TaxonomyItems[0].Image : "",
                              ContentData = ContentHtml(obj.Id)
                          };

            var ContentData = from obj in taxData
                              select new
                              {
                                  Name = obj.Name,
                                  BackGroundImg = obj.ContentData != null ? "/" + obj.ContentData.Image : "",
                                  Html = obj.ContentData != null ? XElement.Parse(obj.ContentData.Html) : null
                              };

            var profileContents = from obj in ContentData
                                  select new
                                  {
                                      TypeName = obj.Name,
                                      ShowContent = obj.Html == null ? false : true,
                                      AnchorName = obj.Name.ToLower().Replace(" ", ""),
                                      SmallImage = obj.Html != null && obj.Html.XPathSelectElement("smallImage/img") != null ? obj.Html.XPathSelectElement("smallImage/img").Attribute("src").Value : string.Empty,
                                      LargeImage = obj.Html != null && obj.Html.XPathSelectElement("largeImage/img") != null ? obj.Html.XPathSelectElement("largeImage/img").Attribute("src").Value : string.Empty,
                                      BackGroundImage = obj.BackGroundImg != "" && obj.BackGroundImg.ToLower().IndexOf("workarea") <= -1 != null ? obj.BackGroundImg : string.Empty,
                                      Description = obj.Html != null && obj.Html.XPathSelectElement("description") != null ? TLITUtility.ExtractNodeHtml(obj.Html.XPathSelectElement("description")) : string.Empty
                                  };
            lvProfileTabs.DataSource = profileContents;
            lvProfileTabs.DataBind();

            lvProfileContent.DataSource = profileContents;
            lvProfileContent.DataBind();            
        }
        catch (Exception ex)
        {
            Log.WriteError(ex);
        }
    }

    private ContentData ContentHtml(long taxId)
    {
        ContentData cData = null;
        long profileSFId = ConfigHelper.GetValueLong("ProfileTypeSmartFormId");
        var cc = new ContentTaxonomyCriteria();
        cc.AddFilter(taxId);
        //cc.AddFilter(ContentProperty.Id, CriteriaFilterOperator.EqualTo, contentId);
        cc.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, profileSFId);
        var list = ContentHelper.GetListByCriteria(cc);
        if (list != null && list.Any())
        {
            cData = list.FirstOrDefault();
        }
        return cData;
    }

}