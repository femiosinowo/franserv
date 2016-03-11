using Ektron.Cms;
using Ektron.Cms.Framework.Content;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class URLRedirects : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    protected void btnAddRedirects_Click(object sender, EventArgs e)
    {
        AddLocalRedirects();
        AddNationalRedirects();
        lblStatus.Text = "301 re-directs entries are updated in the system.";
    }

    protected void AddLocalRedirects()
    {
        string physicalPath = "C:\\Temp\\Local Redirects Updated.xlsx";
        long localRedirectConfigContentId = 7737;

        StringBuilder sb = new StringBuilder();
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataAdapter da = new OleDbDataAdapter();
        DataSet ds = new DataSet();
        String strNewPath = physicalPath;
        String connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strNewPath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
        String query = "SELECT * FROM [redirects-local$]"; // You can use any different queries to get the data from the excel sheet
        OleDbConnection conn = new OleDbConnection(connString);
        if (conn.State == ConnectionState.Closed) conn.Open();
        try
        {
            cmd = new OleDbCommand(query, conn);
            da = new OleDbDataAdapter(cmd);
            da.Fill(ds);
            sb.Append("<root>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];
                string oldURL = string.Empty;
                string newURL = string.Empty;
                if (i > 4)
                {
                    oldURL = dr[1].ToString().Replace("http://www.teamlogicit.com", string.Empty).Replace("http://author.teamlogicit.com", string.Empty);
                    newURL = dr[5].ToString().Replace("http://www.teamlogicit.com", string.Empty).Replace("http://author.teamlogicit.com", string.Empty).Replace("setauketny203", "s_setauketny203");
                    if (oldURL != "" && newURL != "")
                    {
                        sb.Append("<Item>");
                        sb.Append("<Label>");
                        sb.Append(oldURL.Replace("&", "&amp;").ToLower());
                        sb.Append("</Label>");
                        sb.Append("<Value>");
                        sb.Append(newURL.Replace("&", "&amp;").ToLower());
                        sb.Append("</Value>");
                        sb.Append("<decription>");
                        sb.Append("</decription>");
                        sb.Append("</Item>");
                    }
                }
            }
            sb.Append("</root>");
            ContentManager cntManager = new ContentManager(Ektron.Cms.Framework.ApiAccessMode.Admin);        
            ContentData cData = new ContentData();
            cData = cntManager.GetItem(localRedirectConfigContentId, false);
            cData.Html = sb.ToString();
            cntManager.Update(cData);
        }
        catch(Exception ex)
        {         
            Response.Write(ex.Message);
        }
        finally
        {
            da.Dispose();
            conn.Close();
        }
    }


    protected void AddNationalRedirects()
    {
        string physicalPath = "C:\\Temp\\National Redirects.xlsx";
        long nationalRedirectConfigContentId = 7736;

        StringBuilder sb = new StringBuilder();
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataAdapter da = new OleDbDataAdapter();
        DataSet ds = new DataSet();
        String strNewPath = physicalPath;
        String connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strNewPath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
        String query = "SELECT * FROM [redirects-national$]"; // You can use any different queries to get the data from the excel sheet
        OleDbConnection conn = new OleDbConnection(connString);
        if (conn.State == ConnectionState.Closed) conn.Open();
        try
        {
            cmd = new OleDbCommand(query, conn);
            da = new OleDbDataAdapter(cmd);
            da.Fill(ds);
            sb.Append("<root>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];
                string oldURL = string.Empty;
                string newURL = string.Empty;
                if (i > 4)
                {
                    oldURL = dr[1].ToString().Replace("http://www.teamlogicit.com", string.Empty).Replace("http://author.teamlogicit.com", string.Empty);
                    newURL = dr[3].ToString().Replace("http://www.teamlogicit.com", string.Empty).Replace("http://author.teamlogicit.com", string.Empty);
                    if (oldURL != "" && newURL != "")
                    {
                        sb.Append("<Item>");
                        sb.Append("<Label>");
                        sb.Append(oldURL.Replace("&", "&amp;").ToLower());
                        sb.Append("</Label>");
                        sb.Append("<Value>");
                        sb.Append(newURL.Replace("&", "&amp;").ToLower());
                        sb.Append("</Value>");
                        sb.Append("<decription>");
                        sb.Append("</decription>");
                        sb.Append("</Item>");
                    }
                }
            }
            sb.Append("</root>");
            ContentManager cntManager = new ContentManager(Ektron.Cms.Framework.ApiAccessMode.Admin);
            ContentData cData = new ContentData();
            cData = cntManager.GetItem(nationalRedirectConfigContentId, false);
            cData.Html = sb.ToString();
            cntManager.Update(cData);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {
            da.Dispose();
            conn.Close();
        }
    }
    
}