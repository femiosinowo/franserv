using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Web.UI.WebControls;

using SignalGraphics.CMS;
using Ektron.Cms;
using Ektron.Cms.Instrumentation;
using Ektron.Cms.Content;
using Ektron.Cms.Common;


public partial class AdminTool_Templates_Center_Manage_Testimonials : System.Web.UI.Page
{
    UserAPI userApi = new UserAPI();
    string centerId;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (userApi.UserId > 0)
            {
                var udata = CommunityUserHelper.GetUserByUserId(userApi.UserId);
                if (udata != null && udata.Id > 0)
                {
                    var centerUsers = AdminToolManager.GetAllUsers();
                    var userData = centerUsers.Where(x => x.UserName.ToLower() == udata.Username.ToLower()).FirstOrDefault();
                    if (userData != null)
                    {
                        centerId = userData.FransId;
                        hddnCenterId.Value = centerId;
                        this.BindGrid(centerId);
                    }
                }
            }
        }

        if (!string.IsNullOrEmpty(hddnCenterId.Value))
        {
            var centerData = FransDataManager.GetFransData(hddnCenterId.Value);
            if (centerData != null)
            {
                centerInfo.Visible = true;
                lblCenterName.Text = centerData.CenterName;
                lblCenterId.Text = centerData.FransId;
            }
        }
    }
          
    private void BindGrid(string centerId)
    {
        long shopsSFId = ConfigHelper.GetValueLong("ShopSmartFormId");
        var workAreaData = FransDataManager.GetFransWorkareaData(centerId, true);
        if (workAreaData != null && workAreaData.ShopContentIds != null && workAreaData.ShopContentIds.Any())
        {
            var cc = new ContentCriteria();
            cc.AddFilter(ContentProperty.Id, CriteriaFilterOperator.In, workAreaData.ShopContentIds);
            cc.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, shopsSFId);

            var contentList = ContentHelper.GetListByCriteria(cc);
            if (contentList != null && contentList.Any())
            {
                var allShopsData = FransDataManager.GetAllShopsData(centerId, true);

                var contentData = from obj in contentList
                                  select new
                                  {
                                      Id = obj.Id,
                                      Quicklink = obj.Quicklink,
                                      Html = XElement.Parse(obj.Html)
                                  };

                var shopsList = from obj in contentData
                                select new
                                {
                                    Id = obj.Id,
                                    Title = obj.Html.XPathSelectElement("title") != null ? obj.Html.XPathSelectElement("title").Value : string.Empty,
                                    Teaser = obj.Html.XPathSelectElement("teaser") != null ? obj.Html.XPathSelectElement("teaser").Value : string.Empty,
                                    Link = this.GetContentLink(obj.Id, allShopsData),
                                    IsActive = this.GetContentActiveStatus(obj.Id, allShopsData)
                                };
                GridView1.DataSource = shopsList;
                GridView1.DataBind();
            }
        }
    }

    private string GetContentLink(long contentId, List<Shop> allCenterShops)
    {
        string link = string.Empty;
        if (contentId > 0 && allCenterShops != null && allCenterShops.Any())
        {
            foreach (var s in allCenterShops)
            {
                if (s.ContentId == contentId)
                {
                    link = s.Link;
                    break;
                }
            }
        }
        return link;
    }

    private bool GetContentActiveStatus(long contentId, List<Shop> allCenterShops)
    {
        bool status = false;
        if (contentId > 0 && allCenterShops != null && allCenterShops.Any())
        {
            foreach (var s in allCenterShops)
            {
                if (s.ContentId == contentId)
                {
                    status = s.IsActive;
                    break;
                }
            }
        }
        return status;
    }

}