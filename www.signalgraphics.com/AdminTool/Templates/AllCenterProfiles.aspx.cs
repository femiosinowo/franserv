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
using SignalGraphics.CMS;
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

    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["CurrentAlphabet"] = "ALL";
            this.GenerateAlphabets();
            this.BindGrid();
        }
    }

    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        this.BindGrid();
    }

    protected void Alphabet_Click(object sender, EventArgs e)
    {
        LinkButton lnkAlphabet = (LinkButton)sender;
        ViewState["CurrentAlphabet"] = lnkAlphabet.Text;
        this.GenerateAlphabets();
        GridView1.PageIndex = 0;
        this.BindGrid();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            var index = e.RowIndex;
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
            var userId = row.Cells[11].Text;

            if (userId != string.Empty)
            {
                long employeeId;
                long.TryParse(userId, out employeeId);                
                var userListData = AdminToolManager.GetAllUsers();
                var employeeData = userListData.Where(x => x.EmployeeId == employeeId).FirstOrDefault();
                if (employeeData != null && employeeData.EmployeeId > 0)
                {
                    var uData = CommunityUserHelper.GetUserByUserName(employeeData.UserName.ToLower());
                    if (uData != null && uData.Id > 0)
                    {
                        CommunityUserHelper.DeleteUser(uData.Id);
                        AdminToolManager.DeleteCenterUser(employeeData.EmployeeId);
                    }
                    else
                    {
                        AdminToolManager.DeleteCenterUser(employeeData.EmployeeId);
                    }
                    this.BindGrid();
                }
            }
        }
        catch (Exception ex)
        {
            Log.WriteError(ex);
        }
    }

    private void BindGrid()
    {
        List<Employee> userListData = null;

        UserAPI userApi = new UserAPI();
        long userId = userApi.UserId;
        var userData = CommunityUserHelper.GetUserByUserId(userId);
        if (userData != null && userData.Id > 0)
        {
            string centerId = "";
            var centerAdminsList = AdminToolManager.GetAllLocalAdmins();
            var centerData = centerAdminsList.Where(x => x.UserName.ToLower() == userData.Username.ToLower()).FirstOrDefault();
            if (centerData != null && !string.IsNullOrEmpty(centerData.FransId))
            {
                centerId = centerData.FransId;
                userListData = FransDataManager.GetAllEmployee(centerId, true);
                dt = ToDataTable<Employee>(userListData);
                //userListData = userListData.Where(x => x.FransId == centerId && x.Roles != "Center Owner" && x.Roles != "Center Manager").ToList();
            }

            if (ViewState["CurrentAlphabet"] != null)
            {
                string selectedAlphabet = ViewState["CurrentAlphabet"].ToString();
                if (selectedAlphabet != "ALL")
                {
                    List<Employee> filteredList = null;
                    selectedAlphabet = selectedAlphabet.ToLower();
                    if (userListData != null)
                        filteredList = userListData.Where(x => x.FirstName.ToLower().StartsWith(selectedAlphabet)).ToList();
                    GridView1.DataSource = filteredList;
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.DataSource = userListData;
                    GridView1.DataBind();
                }
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