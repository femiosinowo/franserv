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
                        this.FillFeaturedSolution(centerId);
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
                if(selectedBannersList.Length > 6)
                {
                    bannersUpdateMsg.Visible = false;
                    lblError.Visible = true;
                    lblError.Text = "Sorry, you can't select more than 6 items.";
                    return;
                }

                var status = AdminToolManager.UpdateITOurSolutionIds(centerId, hddnSelectedBanners.Value);
                if (status > 0)
                {
                    this.FillFeaturedSolution(centerId);
                    bannersUpdateMsg.Visible = true;
                    lblError.Visible = false;
                }
                else
                {
                    bannersUpdateMsg.Visible = false;
                    lblError.Visible = true;
                    lblError.Text = "Sorry, an error has occured saving Featured Solution data.";
                }
            }
        }
    }

    private void FillFeaturedSolution(string centerId)
    {
        var availableOurSolutionList = AdminToolManager.GetAvailableFeaturedSolutions(centerId);
        if (availableOurSolutionList != null && availableOurSolutionList.Any())
        {
            List<ContentData> availableBannersContent = new List<ContentData>();
            ContentCriteria cc = new ContentCriteria();
            cc.AddFilter(ContentProperty.Id, CriteriaFilterOperator.In, availableOurSolutionList);
            var cList = ContentHelper.GetListByCriteria(cc);

            if (cList != null && cList.Any())
            {
                foreach (var cId in availableOurSolutionList)
                {
                    var cData = cList.Where(x => x.Id == cId).FirstOrDefault();
                    if (cData != null)
                        availableBannersContent.Add(cData);
                }
            }

            var workareaData = FransDataManager.GetFransWorkareaData(centerId, true);
            if (workareaData != null && workareaData.ITOurSolutionContentIds != null && workareaData.ITOurSolutionContentIds.Any())
            {
                var existingBannerList = workareaData.ITOurSolutionContentIds;
                string ids = "";
                foreach (var p in existingBannerList)
                    ids += p + ",";
                hddnSelectedBanners.Value = ids;

                List<ContentData> sortedList = new List<ContentData>();
                //add the selected items on the top
                foreach (var e in existingBannerList)
                {
                    var cData = availableBannersContent.Where(x => x.Id == e).FirstOrDefault();
                    if (cData != null)
                        sortedList.Add(cData);
                }
                //add non-selected items after selected items
                foreach (var b in availableBannersContent)
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
                var contentList = from c in availableBannersContent
                                  select new
                                  {
                                      AvailableBanner = "<div class=\"drag t1\"><span id='" + c.Id + "'>" + c.Title + "</span></div>",
                                      SelectedBanner = ""
                                  };
                lvBanners.DataSource = contentList;
                lvBanners.DataBind();
            }
        }
        else
        {
            pnlPromotions.Visible = false;
            pnlNoBannersResults.Visible = true;
        }
    }

}