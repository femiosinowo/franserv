using System;
using System.Linq;
using System.Web.UI;
using SignalGraphics.CMS;
using Ektron.Cms.Instrumentation;
using Ektron.Cms.Content;
using Ektron.Cms.Common;
using System.Xml.Linq;
using System.Xml.XPath;

public partial class Shop : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {           
            GetCenterShops();
        }
    }

    private void GetCenterShops()
    {
        long shopsSFId = ConfigHelper.GetValueLong("ShopSmartFormId");
        long shopsFolderId = ConfigHelper.GetValueLong("ShopContentFolderId");

        try
        {
            var workAreaData = FransDataManager.GetFransWorkareaData();
            if (workAreaData != null && workAreaData.ShopContentIds != null && workAreaData.ShopContentIds.Any())
            {
                var cc = new ContentCriteria();
                cc.AddFilter(ContentProperty.Id, CriteriaFilterOperator.In, workAreaData.ShopContentIds);
                cc.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, shopsSFId);

                var contentList = ContentHelper.GetListByCriteria(cc);
                if (contentList != null && contentList.Any())
                {
                    string centerId = FransDataManager.GetFranchiseId();

                    var contentData = from obj in contentList
                                      select new
                                      {
                                          Id = obj.Id,
                                          Quicklink = obj.Quicklink,
                                          Html = XElement.Parse(obj.Html)
                                      };

                    var shopsList = from obj in contentData
                                    where GetContentActiveStatus(obj.Id, centerId) == true
                                    select new
                                    {
                                        Link = this.GetContentLink(obj.Id, centerId), //obj.Html.XPathSelectElement("/link/a") != null ? obj.Html.XPathSelectElement("/link/a").Attribute("href").Value : "#",
                                        Image = obj.Html.XPathSelectElement("/image/img") != null ? obj.Html.XPathSelectElement("/image/img").Attribute("src").Value : string.Empty,
                                        Title = obj.Html.XPathSelectElement("title") != null ? obj.Html.XPathSelectElement("title").Value : string.Empty,
                                        Teaser = obj.Html.XPathSelectElement("teaser") != null ? obj.Html.XPathSelectElement("teaser").Value : string.Empty
                                    };
                    lvShpos.DataSource = shopsList;
                    lvShpos.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            Log.WriteError(ex);
        }
    }

    private string GetContentLink(long contentId, string centerId)
    {
        string link = string.Empty;
        if (contentId > 0)
        {
            var allShopsData = FransDataManager.GetAllShopsData(centerId);
            if (allShopsData != null && allShopsData.Any())
            {
                foreach (var s in allShopsData)
                {
                    if (s.ContentId == contentId)
                    {
                        link = s.Link;
                        break;
                    }
                }
            }
            if (link == string.Empty)
                link = "#";
        }
        return link;
    }

    private bool GetContentActiveStatus(long contentId, string centerId)
    {
        bool status = false;
        if (contentId > 0)
        {
            var allShopsData = FransDataManager.GetAllShopsData(centerId);
            if (allShopsData != null && allShopsData.Any())
            {
                foreach (var s in allShopsData)
                {
                    if (s.ContentId == contentId)
                    {
                        status = s.IsActive;
                        break;
                    }
                }
            }
        }
        return status;
    }
}