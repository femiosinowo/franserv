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
using System.Reflection;

public partial class AdminTool_Templates_AllCenters : System.Web.UI.Page
{
    /// <summary>
    /// Sort Direction
    /// </summary>
    public SortDirection sortDirection
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

    DataTable dtable = new DataTable();
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
        var centerData = FransDataManager.GetAllFransLocationDataList();
        dtable = ToDataTable<FransData>(centerData);
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
                DataView dview = new DataView(dtable);
                dview.Sort = SortExpression + " " + (sortDirection == SortDirection.Ascending ? "Asc" : "Desc");
                GridView1.Controls.Clear();
                GridView1.DataSource = dview;
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

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        string SortDir = string.Empty;
        if (sortDirection == SortDirection.Ascending)
        {
            sortDirection = SortDirection.Descending;
            SortDir = "Desc";
        }
        else
        {
            sortDirection = SortDirection.Ascending;
            SortDir = "Asc";
        }
        SortExpression = e.SortExpression;
        BindGrid();
    }

    /// <summary>
    /// To convert to datatable for sorting
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items"></param>
    /// <returns></returns>
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
       
        return dataTable;
    }
}