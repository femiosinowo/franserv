using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ektron.Cms.Instrumentation;
using TeamLogic.CMS;

public partial class AdminTool_Templates_ManageLanguages : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {           
            this.BindGrid();
        }
    }

    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        this.BindGrid();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            var index = e.RowIndex;
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
            var langId = row.Cells[2].Text;

            if (langId != string.Empty)
            {
                long id;
                long.TryParse(langId, out id);
               var status = AdminToolManager.DeleteCenterLanguage(id);
               BindGrid();
            }
        }
        catch (Exception ex)
        {
            Log.WriteError(ex);
        }
    }

    private void BindGrid()
    {
        var langListData = AdminToolManager.GetAllCenterLanguages();
        GridView1.DataSource = langListData;
        GridView1.DataBind();
    }
}