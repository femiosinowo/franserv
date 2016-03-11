using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TeamLogic.CMS;
using Ektron.Cms;
using Ektron.Cms.Content;
using Ektron.Cms.Common;
using Ektron.Cms.Instrumentation;

public partial class AdminTool_Templates_CenterManagePromos : System.Web.UI.Page
{    
    string centerId;
    UserAPI userApi = new UserAPI();    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
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
                        this.FillBanners(centerId);
                        hdnCenterId.Value = centerId;
                    }
                }
            }            
        }

        if (!string.IsNullOrEmpty(hdnCenterId.Value))
        {
            var centerData = FransDataManager.GetFransData(hdnCenterId.Value);
            if (centerData != null)
            {
                centerInfo.Visible = true;
                lblCenterName.Text = centerData.CenterName;
                lblCenterId.Text = centerData.FransId;
            }
        }
    }

    protected void btnBanners_Click(object sender, EventArgs e)
    {
        if (Page.IsValid && hddnSelectedBanners.Value != "")
        {
            lblError.Text = "";
            string centerId = hdnCenterId.Value;

            var selectedBannersList = hddnSelectedBanners.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (selectedBannersList != null && selectedBannersList.Length > 0)
            {
                var status = AdminToolManager.UpdateManageITIds(centerId, hddnSelectedBanners.Value);
                if (status > 0)
                {                  
                    this.FillBanners(centerId);
                    bannersUpdateMsg.Visible = true;
                    lblError.Visible = false;
                }
                else
                {
                    bannersUpdateMsg.Visible = false;
                    lblError.Visible = true;
                    lblError.Text = "Sorry, an error has occured saving banners data.";
                }
            }
        }
    }

    private void FillBanners(string centerId)
    {
        long localManageITCategoryId = ConfigHelper.GetValueLong("LocalManagedItSerivesTaxId");
        long smartFormId = ConfigHelper.GetValueLong("GeneralSmartFormId");
        var localBannersCategory = TaxonomyHelper.GetItem(localManageITCategoryId);
        if (localBannersCategory != null && localBannersCategory.Id > 0)
        {
            ContentTaxonomyCriteria criteria = new ContentTaxonomyCriteria();
            criteria.AddFilter(localBannersCategory.Id);
            criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, smartFormId);
            var bannersCList = ContentHelper.GetListByCriteria(criteria);

            if (bannersCList != null && bannersCList.Any())
            {
                var workareaData = FransDataManager.GetFransWorkareaData(centerId, true);
                var existingBannerList = workareaData.ManageITContentIds;
                if (existingBannerList != null && existingBannerList != null)
                {
                    List<ContentData> sortedList = new List<ContentData>();
                    //add the selected items on the top
                    foreach (var e in existingBannerList)
                    {
                        var cData = bannersCList.Where(x => x.Id == e).FirstOrDefault();
                        if (cData != null)
                            sortedList.Add(cData);
                    }
                    //add non-selected items after selected items
                    foreach (var b in bannersCList)
                    {
                        if (!sortedList.Contains(b))
                            sortedList.Add(b);
                    }

                    var contentList = from c in sortedList
                                      select new
                                      {
                                          AvailableBanner = existingBannerList.Exists(x => x == c.Id) ? "" : "<div class=\"drag t1\"><span  id='" + c.Id + "'>" + c.Title + "</span></div>",
                                          SelectedBanner = existingBannerList.Exists(x => x == c.Id) ? "<div class=\"drag t1\"><span id='" + c.Id + "'>" + c.Title + "</span></div>" : ""
                                      };
                    lvBanners.DataSource = contentList;
                    lvBanners.DataBind();
                }
                else
                {
                    var contentList = from c in bannersCList
                                      select new
                                      {
                                          AvailableBanners = "<div class=\"drag t1\"><span id='" + c.Id + "'><a target=\"_blank\" href=\"/admintool/templates/content.aspx?id=" + c.Id + "\">" + c.Title + "</a></span></div>",
                                          SelectedBanners = ""
                                      };
                    lvBanners.DataSource = contentList;
                    lvBanners.DataBind();
                }
            }
        }
    }

}