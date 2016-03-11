using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SirSpeedy.CMS;
using Ektron.Cms.Content;
using Ektron.Cms.Common;
using Ektron.Cms;

public partial class AdminTool_Templates_ManagePSpageWhitePapers : System.Web.UI.Page
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
                        this.LoadBriefsAndWhitePapers();
                        
                    }
                }
            }
        }
    }

    protected void btnBWP_Click(object sender, EventArgs e)
    {
        if (hddnSelectedBWP.Value != "")
        {
            lblError.Text = "";
            int status = -1;
            var centerId = hddnCenterId.Value;
            if (AdminToolManager.IsPSpageWhitePapersAssigned(centerId))
               status= AdminToolManager.UpdatePSpageWhitePapers(centerId, hddnSelectedBWP.Value);
            else
                status = AdminToolManager.AddPSpageWhitePapers(centerId, hddnSelectedBWP.Value);

            if(status > 0)
            {
                pnlAddPromoMsg.Visible = true;
                LoadBriefsAndWhitePapers();
            }
            else
            {
                lblError.Text = "Sorry, an error has occured saving the data.";
            }            
        }
    }

    private void LoadBriefsAndWhitePapers()
    {
        string centerId = hddnCenterId.Value;
        long localbriefsWhitePaperTaxId = ConfigHelper.GetValueLong("BriefAndWhitepapersLocalTaxId");
        long briefWhitepapersSFId = ConfigHelper.GetValueLong("BriefsAndWhitepapersSmartFormID");
        var localbwpCategory = TaxonomyHelper.GetItem(localbriefsWhitePaperTaxId);

        var workAreaData = FransDataManager.GetFransWorkareaData(centerId, true);
        if (workAreaData != null && workAreaData.BriefsWhitePapersContentIds != null)
        {
            ContentCriteria criteria = new ContentCriteria();
            criteria.AddFilter(ContentProperty.Id, CriteriaFilterOperator.In, workAreaData.BriefsWhitePapersContentIds);
            criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, briefWhitepapersSFId);
            var bwpCList = ContentHelper.GetListByCriteria(criteria);
            if (bwpCList != null && bwpCList.Any())
            {
                var existingBWPList = AdminToolManager.GetSelectedPSpageWhitePapers(hddnCenterId.Value);
                if (existingBWPList != null && existingBWPList.Any())
                {
                    var contentList = from c in bwpCList
                                      select new
                                      {
                                          Availablebwp = existingBWPList.Exists(x => x == c.Id) ? "" : "<div class=\"drag t1\"><span  id='" + c.Id + "'>" + c.Title + "</span></div>",
                                          Selectedbwp = existingBWPList.Exists(x => x == c.Id) ? "<div class=\"drag t1\"><span id='" + c.Id + "'>" + c.Title + "</span></div>" : ""
                                      };
                    lvBWP.DataSource = contentList;
                    lvBWP.DataBind();
                }
                else
                {
                    var contentList = from c in bwpCList
                                      select new
                                      {
                                          Availablebwp = "<div class=\"drag t1\"><span id='" + c.Id + "'>" + c.Title + "</span></div>",
                                          Selectedbwp = ""
                                      };
                    lvBWP.DataSource = contentList;
                    lvBWP.DataBind();
                }
            }
            else
            {
                pnlNoData.Visible = true;
                pnlBWP.Visible = false;
                pnlAddPromoMsg.Visible = false;
            }
        }
        else
        {
            pnlNoData.Visible = true;
            pnlBWP.Visible = false;
            pnlAddPromoMsg.Visible = false;
        }
    }
}