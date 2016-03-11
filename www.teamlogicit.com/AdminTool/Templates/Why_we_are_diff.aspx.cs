using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TeamLogic.CMS;
using Ektron.Cms;

public partial class AdminTool_Templates_Why_we_are_diff : System.Web.UI.Page
{
    UserAPI userApi = new UserAPI();
    string centerId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            txtBannerSubTitle.Value = "We pride ourselves as a leader in the business services market.";
            if (userApi.UserId > 0)
            {
                var udata = CommunityUserHelper.GetUserByUserId(userApi.UserId);
                if (udata != null && udata.Id > 0)
                {
                    var centerUsers = AdminToolManager.GetAllLocalAdmins();
                    var userData = centerUsers.Where(x => x.UserName.ToLower() == udata.Username.ToLower()).FirstOrDefault();
                    if (userData != null)
                    {
                        centerId = hdnCenterId.Value = userData.FransId;
                        this.FillData(centerId);
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

    protected void btnAddContent_Click(object sender, EventArgs e)
    {
        if(Page.IsValid)
        {
            lblError.Text = "";
            var data = new WhyWeAreDiff();
            data.CenterId = hddnCenterId.Value;
            data.BannerTitle = txtBannerTitle.Text;
            data.BannerSubTitle = txtBannerSubTitle.Value;
            data.ContentTitle = txtContentTitle.Text;

            //here is a quick fix & hack for the production issue
            string formattedDes1 = txtContentTagLine.Value;
            formattedDes1 = formattedDes1.Replace("'", "''");
            formattedDes1 = formattedDes1.Replace("<div>", "<span>");
            formattedDes1 = formattedDes1.Replace("</div>", "</span>");
            formattedDes1 = formattedDes1.Replace("<p>", "<span>");
            formattedDes1 = formattedDes1.Replace("</p>", "</span>");
            data.ContentTagLine = formattedDes1;

            string formattedDes2 = txtDescription.Text;
            formattedDes2 = formattedDes2.Replace("'", "''");
            formattedDes2 = formattedDes2.Replace("<div>", "<span>");
            formattedDes2 = formattedDes2.Replace("</div>", "</span>");
            formattedDes2 = formattedDes2.Replace("<p>", "<span>");
            formattedDes2 = formattedDes2.Replace("</p>", "</span>");
            data.ContentDescription = formattedDes2;

            data.VideoLink = txtVideoLink.Text;
            data.VideoStatementText = txtVideoStatementText.Text.Replace("'", "''");
            var status = AdminToolManager.AddUpdateWhyWeAreDiffContent(data);
            if (status > 0)
            {
                updateContentMsg.Visible = true;
                FransDataManager.GetWhyWeAreDiffContent(hddnCenterId.Value, true);
            }
            else
                lblError.Text = "Sorry, an error occured saving Center data.";
        }
    }
    
    private void FillData(string centerId)
    {
        hddnCenterId.Value = centerId;
        var data = FransDataManager.GetWhyWeAreDiffContent(centerId, true);
        if(data != null)
        {
            txtBannerTitle.Text = data.BannerTitle;
            txtBannerSubTitle.Value = data.BannerSubTitle;
            txtContentTitle.Text = data.ContentTitle;
            txtContentTagLine.Value = data.ContentTagLine.Replace("''", "'");
            txtDescription.Text = data.ContentDescription.Replace("''", "'");
            txtVideoLink.Text = data.VideoLink;
            txtVideoStatementText.Text = data.VideoStatementText.Replace("''", "'");
        }
    }
}