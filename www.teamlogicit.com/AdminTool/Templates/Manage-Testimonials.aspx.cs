using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TeamLogic.CMS;
using Ektron.Cms;
using Ektron.Cms.Instrumentation;
using System.Data;
using System.Reflection;

public partial class AdminTool_Templates_Manage_Testimonials : System.Web.UI.Page
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

    public string SortExpression
    {
        get
        {
            if (ViewState["SortExpr"] == null)
            {
                ViewState["SortExpr"] = "Title";
            }
            return ViewState["SortExpr"] as string;
        }
        set
        {
            ViewState["SortExpr"] = value;
        }
    }

    DataTable dt = new DataTable();

    UserAPI userApi = new UserAPI();
    string centerId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (userApi.UserId > 0)
            {
                this.BindGrid();
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
        this.BindGrid();
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        SortExpression = e.SortExpression;
        //dir = e.SortDirection; //seems to be a bug always coming as Ascending

        if (dir == SortDirection.Ascending)
            dir = SortDirection.Descending;
        else if (dir == SortDirection.Descending)
            dir = SortDirection.Ascending;

        var allTestimonialsList = FransDataManager.GetAllTestimonials(true);
        dt = ToDataTable<Testimonials>(allTestimonialsList);
        if (allTestimonialsList != null && allTestimonialsList.Any())
        {
            DataView sortedView = new DataView(dt);

            string SortDir = string.Empty;
            SortDir = (dir == SortDirection.Ascending ? "Asc" : "Desc");
            sortedView.Sort = SortExpression + " " + SortDir;
            GridView1.DataSource = sortedView;
            GridView1.DataBind();
        }
    }

    private void BindGrid()
    {
        var allTestimonialsList = FransDataManager.GetAllTestimonials(true);
        dt = ToDataTable<Testimonials>(allTestimonialsList);
        if (allTestimonialsList != null && allTestimonialsList.Any())
        {
            DataView sortedView = new DataView(dt);

            string SortDir = string.Empty;
            SortDir = dir == SortDirection.Ascending ? "Asc" : "Desc";
            sortedView.Sort = SortExpression + " " + SortDir;
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