using System;
using System.Linq;
using System.Web.UI;
using Ektron.Cms;
using Ektron.Cms.Instrumentation;
using System.Xml.Linq;
using System.Xml.XPath;
using Ektron.Cms.Content;
using Ektron.Cms.Common;
using TeamLogic.CMS;

public partial class job_profiles : PageBase
{
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

        cbLandingContent.DefaultContentID = ConfigHelper.GetValueLong("ProfileLandingContent");
        cbLandingContent.CacheInterval = ConfigHelper.GetValueLong("smallCacheInterval");
        cbLandingContent.Fill();

        //check for page banner image via metadata
        if (cbTagline != null && cbTagline.EkItem != null)
        {
            if (!string.IsNullOrEmpty(cbTagline.EkItem.Image))
            {
                subpage_tagline_wrapper.Attributes.Add("style", "background-image: url('" + cbTagline.EkItem.Image + "')");
            }
        }
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
                             Description = obj.Html.XPathSelectElement("/desc") != null ? obj.Html.XPathSelectElement("/desc").ToString() : string.Empty,
                             //VideoUrl = obj.Html.XPathSelectElement("/url/a") != null ? obj.Html.XPathSelectElement("/url/a").Attribute("href").Value : string.Empty
                             VideoUrl = obj.Html.XPathSelectElement("/url/a") != null ? "<div class=\"video_wrapper\"><iframe frameborder=\"0\" allowfullscreen=\"\" mozallowfullscreen=\"\" webkitallowfullscreen=\"\" src=\"" + obj.Html.XPathSelectElement("/url/a").Attribute("href").Value + "\"></iframe></div>" : (obj.Html.XPathSelectElement("image/img") != null) ? "<div class=\"image_wrapper\"><img src=\"" + obj.Html.XPathSelectElement("image/img").Attribute("src").Value + "\" /></div>" : string.Empty
                             //Image = obj.Html.XPathSelectElement("image/img") != null ? "<img src=\"" + obj.Html.XPathSelectElement("image/img").Attribute("src").Value + "\"" : string.Empty
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
            lvJobProfileStatementTxt.DataSource = profileContents;
            lvJobProfileStatementTxt.DataBind();
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