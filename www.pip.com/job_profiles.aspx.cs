using System;
using System.Linq;
using System.Web.UI;
using Ektron.Cms;
using Ektron.Cms.Instrumentation;
using System.Xml.Linq;
using System.Xml.XPath;
using Ektron.Cms.Content;
using Ektron.Cms.Common;
using SirSpeedy.CMS;

public partial class job_profiles : System.Web.UI.Page
{
    /// <summary>
    /// page init event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Init(object sender, EventArgs e)
    {
        cbJobProfilesSideContent.DefaultContentID = ConfigHelper.GetValueLong("JobProfilesSideContentID");
        cbJobProfilesSideContent.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbJobProfilesSideContent.Fill();

        cbJobProfilesQuotes.DefaultContentID = ConfigHelper.GetValueLong("JobProfilesSideQuotesContentID");
        cbJobProfilesQuotes.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbJobProfilesQuotes.Fill();

        imageJobProfile1.DefaultContentID = ConfigHelper.GetValueLong("JobProfilesImageLeftContentID");
        imageJobProfile1.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        imageJobProfile1.Fill();

        imageJobProfile2.DefaultContentID = ConfigHelper.GetValueLong("JobProfilesImageRightContentID");
        imageJobProfile2.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        imageJobProfile2.Fill();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadProfileContent();
        }

        //if (FransDataManager.IsFranchiseSelected())
        //    uxJobSearch1.IsLocalSite = true;
        //else
            uxJobSearch1.IsLocalSite = false;

        //cbLandingContent.DefaultContentID = ConfigHelper.GetValueLong("ProfileLandingContent");
        //cbLandingContent.CacheInterval = ConfigHelper.GetValueLong("smallCacheInterval");
        //cbLandingContent.Fill();
    }

    /// <summary>
    /// This method is used to load tabs content
    /// </summary>
    private void LoadProfileContent()
    {
        long profileTaxId = ConfigHelper.GetValueLong("ManageCarrersTaxId");
        var careersTree = TaxonomyHelper.GetTaxonomyTree(profileTaxId, 1, false);

        if(careersTree != null && careersTree.HasChildren)
        {
            this.LoadProfileTabs(careersTree);
            this.LoadProfileTabsContent(careersTree);
            this.LoadProfileStatementTxt(careersTree);
        }
    }
    
    private void LoadProfileTabs(TaxonomyData taxTree)
    {
        var careersTabTree = taxTree.Taxonomy;
        lvProfileTabs.DataSource = careersTabTree;
        lvProfileTabs.DataBind();
    }

    private void LoadProfileTabsContent(TaxonomyData taxTree)
    {
        try
        {
            var taxData = from obj in taxTree.Taxonomy
                              select new
                              {
                                  Name = obj.Name,
                                  Html = XElement.Parse(ContentHtml(obj.Id))
                              };

            var profileContents = from obj in taxData
                         select new
                         {
                             TypeName = obj.Name,
                             Abstract = obj.Html.XPathSelectElement("/abstract") != null ? obj.Html.XPathSelectElement("/abstract").Value : string.Empty,
                             Description = obj.Html.XPathSelectElement("/desc") != null ? obj.Html.XPathSelectElement("/desc").Value : string.Empty,
                             VideoUrl = obj.Html.XPathSelectElement("/url/a") != null ? obj.Html.XPathSelectElement("/url/a").Attribute("href").Value : string.Empty,
                             Image = obj.Html.XPathSelectElement("/image/img") != null ? obj.Html.XPathSelectElement("/image/img").ToString() : string.Empty
                         };
            lvTabsContent.DataSource = profileContents;
            lvTabsContent.DataBind();
        }
        catch(Exception ex)
        {
            Log.WriteError(ex);
        }
    }

    private void LoadProfileStatementTxt(TaxonomyData taxTree)
    {
        try
        {
            var taxData = from obj in taxTree.Taxonomy
                          select new
                          {
                              Name = obj.Name,
                              Html = XElement.Parse(ContentHtml(obj.Id))
                          };

            var profileContents = from obj in taxData
                                  select new
                                  {
                                      TypeName = obj.Name,
                                      Tagline = obj.Html.XPathSelectElement("/tagline") != null ? obj.Html.XPathSelectElement("/tagline").Value : string.Empty
                                  };
            //lvJobProfileStatementTxt.DataSource = profileContents;
            //lvJobProfileStatementTxt.DataBind();
        }
        catch (Exception ex)
        {
            Log.WriteError(ex);
        }
    }
    
    private string ContentHtml(long taxId)
    {
        string html = string.Empty;
        long profileSFId = ConfigHelper.GetValueLong("ProfileTypeSmartFormId");
        var cc = new ContentTaxonomyCriteria();
        cc.AddFilter(taxId);
        //cc.AddFilter(ContentProperty.Id, CriteriaFilterOperator.EqualTo, contentId);
        cc.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, profileSFId);
        var list = ContentHelper.GetListByCriteria(cc);
        if(list != null && list.Any())
        {
            html = list[0].Html;
        }
        return html;
    }
}