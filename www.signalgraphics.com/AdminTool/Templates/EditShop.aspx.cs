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
using Ektron.Cms.Framework.Content;
using System.IO;

public partial class AdminTool_Templates_AddTestimonial : System.Web.UI.Page
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
                    var centerUsers = AdminToolManager.GetAllLocalAdmins();
                    var userData = centerUsers.Where(x => x.UserName.ToLower() == udata.Username.ToLower()).FirstOrDefault();
                    if (userData != null)
                    {
                        centerId = userData.FransId;
                        hddnCenterId.Value = centerId;                                         

                        if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                        {
                            long id;
                            long.TryParse(Request.QueryString["id"], out id);
                            this.FillData(id, centerId);
                        }                        
                    }
                }
            }
        }
    }

    protected void btnUpdateShop_Click(object sender, EventArgs e)
    {
        if(Page.IsValid)
        {
            long id;
            long.TryParse(Request.QueryString["id"], out id);
            string isActive = chkIsActive.Checked ? "1" : "0";
            AdminToolManager.UpdateShopContentURL(hddnCenterId.Value, id, txtLink.Text, isActive);
            FransDataManager.GetAllShopsData(hddnCenterId.Value, true);
            FransDataManager.GetFransWorkareaData(hddnCenterId.Value, true);
            pnlUpdateShopMsg.Visible = true;
        }
    }

    private void FillData(long id, string centerId)
    {
        if (id > 0)
        {
            var contentData = ContentHelper.GetContentById(id);
            if (contentData != null && contentData.Id > 0)
            {
                var Html = XElement.Parse(contentData.Html);
                ltrTitle.Text = Html.XPathSelectElement("title") != null ? Html.XPathSelectElement("title").Value : string.Empty;
                ltrTeaser.Text = Html.XPathSelectElement("teaser") != null ? Html.XPathSelectElement("teaser").Value : string.Empty;
                shopImg.Src = Html.XPathSelectElement("/image/img") != null ? Html.XPathSelectElement("/image/img").Attribute("src").Value : string.Empty;
                txtLink.Text = this.GetContentLink(id, centerId);
                chkIsActive.Checked = this.GetContentActiveStatus(id, centerId);
            }
        }
    }

    private string GetContentLink(long contentId, string centerId)
    {
        string link = string.Empty;
        if (contentId > 0)
        {
            var allShopsData = FransDataManager.GetAllShopsData(centerId, true);
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
        }
        return link;
    }

    private bool GetContentActiveStatus(long contentId, string centerId)
    {
        bool status = false;
        if (contentId > 0)
        {
            var allShopsData = FransDataManager.GetAllShopsData(centerId, true);
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