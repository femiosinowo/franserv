using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TeamLogic.CMS;
using Ektron.Cms;
using Ektron.Cms.Instrumentation;

public partial class AdminTool_Templates_manage_all_sent_files : System.Web.UI.Page
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
                        ViewState["CurrentAlphabet"] = "ALL";
                        this.GenerateAlphabets();
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

    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        this.BindGrid(hddnCenterId.Value);
    }

    protected void Alphabet_Click(object sender, EventArgs e)
    {
        LinkButton lnkAlphabet = (LinkButton)sender;
        ViewState["CurrentAlphabet"] = lnkAlphabet.Text;
        this.GenerateAlphabets();
        GridView1.PageIndex = 0;
        this.BindGrid(hddnCenterId.Value);
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            var index = e.RowIndex;
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
            var sFileId = row.Cells[13].Text;

            if (sFileId != string.Empty)
            {
                long id;
                long.TryParse(sFileId, out id);
                var status = AdminToolManager.DeleteSendAFileRecord(id);
                BindGrid(hddnCenterId.Value);
            }
        }
        catch (Exception ex)
        {
            Log.WriteError(ex);
        }
    }

    private void BindGrid(string centerId)
    {
        var allSendAFileData = AdminToolManager.GetAllSendAFileInfo(centerId);
        if (ViewState["CurrentAlphabet"] != null)
        {
            string selectedAlphabet = ViewState["CurrentAlphabet"].ToString();
            if (selectedAlphabet != "ALL")
            {
                selectedAlphabet = selectedAlphabet.ToLower();
                var filteredList = allSendAFileData.Where(x => x.FirstName.ToLower().StartsWith(selectedAlphabet)).ToList();
                GridView1.DataSource = filteredList;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = allSendAFileData;
                GridView1.DataBind();
            }
        }
    }

    private void GenerateAlphabets()
    {
        List<ListItem> alphabets = new List<ListItem>();
        ListItem alphabet = new ListItem();
        alphabet.Value = "ALL";
        alphabet.Selected = alphabet.Value.Equals(ViewState["CurrentAlphabet"]);
        alphabets.Add(alphabet);
        for (int i = 65; i <= 90; i++)
        {
            alphabet = new ListItem();
            alphabet.Value = Char.ConvertFromUtf32(i);
            alphabet.Selected = alphabet.Value.Equals(ViewState["CurrentAlphabet"]);
            alphabets.Add(alphabet);
        }
        rptAlphabets.DataSource = alphabets;
        rptAlphabets.DataBind();
    }
}