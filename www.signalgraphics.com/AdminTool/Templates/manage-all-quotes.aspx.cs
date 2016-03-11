using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Reflection;

using SignalGraphics.CMS;
using Ektron.Cms;
using Ektron.Cms.Instrumentation;
using Ektron.Cms.Framework.Content;

public partial class AdminTool_Templates_manage_all_quotes : System.Web.UI.Page
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
                ViewState["SortExpr"] = "DateSubmitted";
            }
            return ViewState["SortExpr"] as string;
        }
        set
        {
            ViewState["SortExpr"] = value;
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
            var quoteId = row.Cells[13].Text;

            if (quoteId != string.Empty)
            {
                long id;
                long.TryParse(quoteId, out id);
                var status = AdminToolManager.DeleteRequestToQuote(id);
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
        var allQuotesData = AdminToolManager.GetAllRequestToQuotes(null);
        if (ViewState["CurrentAlphabet"] != null)
        {
            var formattedList = FormatData(allQuotesData);
            if (formattedList != null)
            {
                string selectedAlphabet = ViewState["CurrentAlphabet"].ToString();
                if (selectedAlphabet != "ALL")
                {
                    selectedAlphabet = selectedAlphabet.ToLower();
                    var filteredList = formattedList.Where(x => x.FirstName.ToLower().StartsWith(selectedAlphabet)).ToList();
                    var sortedList = filteredList.OrderBy(x => x.DateSubmitted).Reverse().ToList();
                    dt = ToDataTable<RequestToQuote>(sortedList);
                    DataView sortedView = new DataView(dt);

                    string SortDir = string.Empty;
                    SortDir = dir == SortDirection.Ascending ? "Asc" : "Desc";
					//by default sort list by date submitted
                     if (Page.IsPostBack)
                        sortedView.Sort = SortExpression + " " + SortDir;
                    GridView1.DataSource = sortedView;
                    GridView1.DataBind();
                }
                else
                {
                    var sortedList = formattedList.OrderBy(x => x.DateSubmitted).Reverse().ToList();
                    dt = ToDataTable<RequestToQuote>(sortedList);
                    DataView sortedView = new DataView(dt);

                    string SortDir = string.Empty;
                    SortDir = dir == SortDirection.Ascending ? "Asc" : "Desc";
					//by default sort list by date submitted
                     if (Page.IsPostBack)
                        sortedView.Sort = SortExpression + " " + SortDir;
                    GridView1.DataSource = sortedView;
                    GridView1.DataBind();
                }
            }
            else
            {
                GridView1.DataSource = null;
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
        SortExpression = e.SortExpression;
        //dir = e.SortDirection; //seems to be a bug always coming as Ascending

        if (dir == SortDirection.Ascending)
            dir = SortDirection.Descending;
        else if (dir == SortDirection.Descending)
            dir = SortDirection.Ascending;

        var allQuotesData = AdminToolManager.GetAllRequestToQuotes(null);
        var formattedList = FormatData(allQuotesData);
        if (formattedList != null)
        {
            string selectedAlphabet = ViewState["CurrentAlphabet"].ToString();
            if (selectedAlphabet != "ALL")
            {
                selectedAlphabet = selectedAlphabet.ToLower();
                var filteredList = formattedList.Where(x => x.FirstName.ToLower().StartsWith(selectedAlphabet)).ToList();
                //var sortedList = filteredList.OrderBy(x => x.DateSubmitted).Reverse().ToList();
                dt = ToDataTable<RequestToQuote>(filteredList);
                DataView sortedView = new DataView(dt);

                string SortDir = string.Empty;
                SortDir = (dir == SortDirection.Ascending ? "Asc" : "Desc");
                sortedView.Sort = e.SortExpression + " " + SortDir;
                GridView1.DataSource = sortedView;
                GridView1.DataBind();
            }
            else
            {
                var sortedList = formattedList.OrderBy(x => x.DateSubmitted).Reverse().ToList();
                dt = ToDataTable<RequestToQuote>(sortedList);
                DataView sortedView = new DataView(dt);

                string SortDir = string.Empty;
                SortDir = (dir == SortDirection.Ascending ? "Asc" : "Desc");
                sortedView.Sort = e.SortExpression + " " + SortDir;
                GridView1.DataSource = sortedView;
                GridView1.DataBind();
            }
        }
    }

    private List<RequestToQuote> FormatData(List<RequestToQuote> data)
    {
        //List<RequestToQuote> dataList = null;
        //if (data != null && data.Any())
        //{
        //    LibraryManager libraryManager = new LibraryManager(Ektron.Cms.Framework.ApiAccessMode.Admin);
        //    dataList = new List<RequestToQuote>();
        //    RequestToQuote item = null;
        //    foreach (var d in data)
        //    {
        //        item = d;
        //        if (!string.IsNullOrEmpty(item.UploadedFileId))
        //        {
        //            var idsArray = item.UploadedFileId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        //            if (idsArray != null && idsArray.Any())
        //            {
        //                StringBuilder sb = new StringBuilder();
        //                foreach (var id in idsArray)
        //                {
        //                    sb.Append("<a href=\"" + d.ServerDomain + "/Workarea/DownloadAsset.aspx?id=" + id + "\">" + id + "</a>, ");
        //                }

        //                string fileLinks = "";
        //                fileLinks = sb.ToString();
        //                if (fileLinks.Length > 0)
        //                    fileLinks = fileLinks.Remove(fileLinks.Length - 2, 2);

        //                item.UploadedFilePath = fileLinks;
        //            }
        //        }
        //        dataList.Add(item);
        //    }
        //}
        //return data;
        return data;
    }

}