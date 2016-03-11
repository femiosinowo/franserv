using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Ektron.Cms.Instrumentation;
using Ektron.Cms;
using Ektron.Cms.API.User;
using SirSpeedy.CMS;
using System.Reflection;

public partial class AdminTool_Templates_AllProfiles : System.Web.UI.Page
{
    public SortDirection dir
    {
        get
        {
            if (ViewState["dirState"] == null)
            {
                ViewState["dirState"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["dirState"];
        }
        set
        {
            ViewState["dirState"] = value;
        }
    }
    string centerId = "";
    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["CurrentAlphabet"] = "ALL";           
            this.BindGrid();
        }

        if (!string.IsNullOrEmpty(centerId))
        {
            var centerData = FransDataManager.GetFransData(centerId);
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
        this.BindGrid();
    }    

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            var index = e.RowIndex;
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];            
            var subscribtionId = row.Cells[11].Text;

            if (subscribtionId != string.Empty)
            {
                long sId;
                long.TryParse(subscribtionId, out sId);
                if (sId > 0)
                {
                    AdminToolManager.DeleteSubscription(sId);
                    this.BindGrid();
                }
            }
        }
        catch(Exception ex)
        {
            Log.WriteError(ex);
        }
    }

    private void BindGrid()
    {
        List<Subscribtion> userSubsList = null;

        UserAPI userApi = new UserAPI();
        long userId = userApi.UserId;
        var userData = CommunityUserHelper.GetUserByUserId(userId);
        if (userData != null && userData.Id > 0)
        {           
            var centerAdminsList = AdminToolManager.GetAllLocalAdmins();
            var centerData = centerAdminsList.Where(x => x.UserName.ToLower() == userData.Username.ToLower()).FirstOrDefault();
            if (centerData != null && !string.IsNullOrEmpty(centerData.FransId))
            {
                centerId = centerData.FransId;
                userSubsList = AdminToolManager.GetAllCenterSubscription(centerId);
                dt = ToDataTable<Subscribtion>(userSubsList);
            }

            GridView1.DataSource = userSubsList;
            GridView1.DataBind();
        }
    }    
   
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        this.BindGrid();
        {
            string SortDir = string.Empty;
            if (dir == SortDirection.Ascending)
            {
                dir = SortDirection.Descending;
                SortDir = "Desc";
            }
            else
            {
                dir = SortDirection.Ascending;
                SortDir = "Asc";
            }
            DataView sortedView = new DataView(dt);
            sortedView.Sort = e.SortExpression + " " + SortDir;
            GridView1.DataSource = sortedView;
            GridView1.DataBind();
        }
    }

    public static DataTable ToDataTable<T>(List<T> items)
    {
        DataTable dataTable = new DataTable(typeof(T).Name);

        //Get all the properties
        PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (PropertyInfo prop in Props)
        {
            //Setting column names as Property names
            dataTable.Columns.Add(prop.Name);
        }
        foreach (T item in items)
        {
            var values = new object[Props.Length];
            for (int i = 0; i < Props.Length; i++)
            {
                //inserting property values to datatable rows
                values[i] = Props[i].GetValue(item, null);
            }
            dataTable.Rows.Add(values);
        }
        //put a breakpoint here and check datatable
        return dataTable;
    }
}