using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;

using SignalGraphics.CMS;
using Ektron.Cms.Instrumentation;
using System.Reflection;
using Ektron.Cms.Common;

public partial class AdminTool_Templates_AllCenters : System.Web.UI.Page
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


    /// <summary>
    /// For keeping Sort Express on page change
    /// </summary>
    public string SortExpression
    {
        get
        {
            if (ViewState["SortExp"] == null)
            {
                ViewState["SortExp"] = "CenterName";
            }
            return ViewState["SortExp"].ToString();
        }
        set
        {
            ViewState["SortExp"] = value;
        }
    }

    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["CurrentAlphabet"] = "ALL";
            ViewState["SortExp"] = "CenterName";
            this.GenerateAlphabets();
            this.BindGrid();
        }
    }

    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        this.BindGrid();
        string SortDir = string.Empty;
        if (dir == SortDirection.Ascending)
        {
            SortDir = "Asc";
        }
        else
        {
            SortDir = "Desc";
        }
        DataView sortedView = new DataView(dt);
        sortedView.Sort = SortExpression + " " + SortDir;
        GridView1.Controls.Clear();
        GridView1.DataSource = sortedView;
        GridView1.DataBind();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lblError.Text = "";
            var index = e.RowIndex;
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
            var centerId = row.Cells[13].Text;

            if (centerId != string.Empty)
            {
                var fransData = FransDataManager.GetFransData(centerId);
                if(fransData == null)
                {
                    lblError.Text = "Center do not exist in the system.";
                    return;
                }

                var status = AdminToolManager.DisableCenter(centerId);
                if(status)
                {
                    //remove local users which is not part of disbale center call
                    var allCenterAdmins = AdminToolManager.GetAllLocalAdmins();
                    if(allCenterAdmins != null && allCenterAdmins.Any())
                    {
                        var currentCenterAdmins = allCenterAdmins.Where(x => x.FransId == centerId).ToList();
                        if(currentCenterAdmins != null && currentCenterAdmins.Any())
                        {
                            foreach(var u in currentCenterAdmins)
                            {
                                AdminToolManager.DeleteCenterUser(u.EmployeeId);
                                CommunityUserHelper.DeleteUser(u.UserName);
                            }
                        }
                    }

                    //remove all testimonials which is not part of disbale center call
                    var allTestimonialsList = FransDataManager.GetAllTestimonials(centerId, true);
                    foreach(var t in allTestimonialsList)
                    {
                        AdminToolManager.DeleteTestimonial(t.TestimonialId);
                    }

                    //rename ekt folder in site templates
                    long localCentersMainFolderId = ConfigHelper.GetValueLong("LocalCentersMainFolderId");
                    var criteria = new Ektron.Cms.FolderCriteria();
                    criteria.AddFilter(FolderProperty.FolderName, CriteriaFilterOperator.EqualTo, centerId.Trim());
                    criteria.AddFilter(FolderProperty.ParentId, CriteriaFilterOperator.EqualTo, localCentersMainFolderId);
                    var selectedCenterFolderList =  FolderHelper.GetList(criteria);
                    if (selectedCenterFolderList != null && selectedCenterFolderList.Any())
                    {
                        var localCenterFolderId = selectedCenterFolderList.FirstOrDefault();
                        if (localCenterFolderId.Id > 0)
                        {
                            localCenterFolderId.Name = localCenterFolderId.Name + "_REMOVED_" + DateTime.Now.ToString("MMddyyy"); ;
                            FolderHelper.Update(localCenterFolderId);
                        }
                    }

                    //refresh data
                    BindGrid();
                }
                else
                {
                    lblError.Text = "Sorry, an error has occured deleting a Center.";
                }
            }
        }
        catch (Exception ex)
        {
            Log.WriteError(ex);
            lblError.Text = "Sorry, an error has occured processing your request.";
        }
    }

    protected void Alphabet_Click(object sender, EventArgs e)
    {
        LinkButton lnkAlphabet = (LinkButton)sender;
        ViewState["CurrentAlphabet"] = lnkAlphabet.Text;
        this.GenerateAlphabets();
        GridView1.PageIndex = 0;
        this.BindGrid();
    }

    private void BindGrid()
    {
        var centerData = FransDataManager.GetAllFransLocationDataList(true);
        dt = ToDataTable<FransData>(centerData);
        if (ViewState["CurrentAlphabet"] != null)
        {
            string selectedAlphabet = ViewState["CurrentAlphabet"].ToString();
            if (selectedAlphabet != "ALL")
            {
                selectedAlphabet = selectedAlphabet.ToLower();
                var filteredList = centerData.Where(x => x.CenterName.ToLower().StartsWith(selectedAlphabet)).ToList();
                GridView1.DataSource = filteredList;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = centerData;
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
            SortExpression = e.SortExpression;
            GridView1.Controls.Clear();
            GridView1.DataSource = sortedView;
            GridView1.DataBind();
        }
    }
}