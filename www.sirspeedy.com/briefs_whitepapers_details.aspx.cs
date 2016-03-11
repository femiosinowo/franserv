using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SirSpeedy.CMS;
using System.Xml;
using Ektron.Cms.Common;
using Ektron.Cms.Content;
using System.Text.RegularExpressions;
using Ektron.Cms.Instrumentation;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using Figleaf;

public partial class briefs_whitepapers_details : System.Web.UI.Page
{
    private static string adminToolConnectionString = ConfigurationManager.ConnectionStrings["SirSpeedyAdminTool.DbConnection"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadPrimaryBusiness();
            LoadMarketingEmployee();

            UxBriefsWhitepapers.DataSource = GetBriefsWHitepaperContent();
            UxBriefsWhitepapers.DataBind();
        }
    }

    private DataTable GetBriefsWHitepaperContent()
    {

        long SmartFormXMLConfig = ConfigHelper.GetValueLong("BriefsAndWhitepapersSmartFormID");
        //  DataTable sortedDT = new DataTable();
        long folderID = ConfigHelper.GetValueLong("AllBriefsAndWhitepapersFolderID");

        long contentId = 0;
        if (Request.QueryString["id"] != null)
        {
            long.TryParse(Request.QueryString["id"], out contentId);
        }

        //Maybe need to modify cacheKey
        string fransId = FransDataManager.IsFranchiseSelected() ? FransDataManager.GetFranchiseId() : "0";
        string cacheKey = String.Format("Sirspeedy:Briefswhitepapersdetails:GetBriefsWhitepaperContent:Folderid={0}:ContentID={1}:SmartFormId={2}:FranchiseId={3}", folderID, contentId, SmartFormXMLConfig, fransId);

        DataTable sortedDT = CacheBase.Get<DataTable>(cacheKey);

        if (sortedDT == default(DataTable) || sortedDT.Rows.Count == 0)
        {
            XmlDocument contentXML = new XmlDocument();

            DataTable DTSource = new DataTable();
            int counter = 0;

            DTSource.Columns.Add("title");
            DTSource.Columns.Add("teaserMM");
            DTSource.Columns.Add("abstract");
            DTSource.Columns.Add("content");
            DTSource.Columns.Add("url");
            DTSource.Columns.Add("imgSRC");
            DTSource.Columns.Add("counter");

            //get all contents in the specified folder
            ContentCriteria criteria = new ContentCriteria();
            criteria.AddFilter(ContentProperty.FolderId, CriteriaFilterOperator.EqualTo, folderID);
            criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, SmartFormXMLConfig);
            var contents = ContentHelper.GetListByCriteria(criteria);

            if (contents != null && contents.Count > 0)
            {
                foreach (Ektron.Cms.ContentData contentData in contents)
                {
                    if (contentData.Id == contentId)
                    {
                        try
                        {
                            contentXML.LoadXml(contentData.Html);

                            XmlNodeList xnList = contentXML.SelectNodes("/root");
                            string title = xnList[0]["title"].InnerXml;
                            hddnWhitePaperTitle.Value = title;
                            string teaserMM = xnList[0]["teaserMM"].InnerXml;
                            string abstractText = xnList[0]["abstract"].InnerXml;
                            string content = xnList[0]["content"].InnerXml;
                            string xml = contentXML.SelectSingleNode("/root/image").InnerXml;
                            string imgSRC =
                                Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1]
                                    .Value;
                            xml = contentXML.SelectSingleNode("/root/url").InnerXml;
                            string url =
                                Regex.Match(xml, "<a.+?href=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1]
                                    .Value; //not sure about this
                            counter++;

                            DTSource.Rows.Add(title, teaserMM, abstractText, content, url, imgSRC, counter);
                        }
                        catch (Exception ex)
                        {
                            Log.WriteError(ex);
                        }
                    }
                }
                DataView DVPSMMSort = DTSource.DefaultView;
                DVPSMMSort.Sort = "counter desc";
                sortedDT = DVPSMMSort.ToTable();
            }
            CacheBase.Put(cacheKey, sortedDT, CacheDuration.For24Hr);
        }
        return sortedDT;
    }

    private void LoadPrimaryBusiness()
    {
        try
        {
            string cacheKey = String.Format("Sirspeedy:briefs_whitepapers_details_:LoadPrimaryBusiness");
            DataTable subjects = CacheBase.Get<DataTable>(cacheKey);
            if (subjects == default(DataTable) || subjects.Rows.Count == 0)
            {
                subjects = new DataTable();
                using (SqlConnection con = new SqlConnection(adminToolConnectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT id, label FROM Primary_Business_tbl", con);
                    adapter.Fill(subjects);
                    ddlPrimaryBusiness.DataSource = subjects;
                    ddlPrimaryBusiness.DataTextField = "label";
                    ddlPrimaryBusiness.DataValueField = "id";
                    ddlPrimaryBusiness.DataBind();
                    CacheBase.Put(cacheKey, subjects, CacheDuration.For24Hr);
                }
            }
            else
            {
                ddlPrimaryBusiness.DataSource = subjects;
                ddlPrimaryBusiness.DataTextField = "label";
                ddlPrimaryBusiness.DataValueField = "id";
                ddlPrimaryBusiness.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.WriteError(ex); // Handle the error
        }

        // Add the initial item - you can add this even if the options from the
        // db were not successfully loaded
        //ddlPrimaryBusiness.Items.Insert(0, new ListItem("<Select Subject>", "0"));

    }

    private void LoadMarketingEmployee()
    {
        try
        {
            string cacheKey = String.Format("Sirspeedy:briefs_whitepapers_details_:LoadMarketingEmployee");
            DataTable subjects = CacheBase.Get<DataTable>(cacheKey);
            if (subjects == default(DataTable) || subjects.Rows.Count == 0)
            {
                subjects = new DataTable();
                using (SqlConnection con = new SqlConnection(adminToolConnectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT id, label FROM Number_of_Marketing_Employeess_tbl", con);
                    adapter.Fill(subjects);
                    ddlMarketingEmployee.DataSource = subjects;
                    ddlMarketingEmployee.DataTextField = "label";
                    ddlMarketingEmployee.DataValueField = "id";
                    ddlMarketingEmployee.DataBind();
                    CacheBase.Put(cacheKey, subjects, CacheDuration.For24Hr);
                }
            }
            else
            {
                ddlMarketingEmployee.DataSource = subjects;
                ddlMarketingEmployee.DataTextField = "label";
                ddlMarketingEmployee.DataValueField = "id";
                ddlMarketingEmployee.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.WriteError(ex);// Handle the error
        }
    }

    protected void btnDownloadNow_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            bool isSaved = false;
            string firstName = fname.Text;
            string lastName = lname.Text;
            string email = emailAdd.Text;
            string companyWebsite = url.Text;
            string zipCode = txtZipCode.Text;
            string centerId = (FransDataManager.GetFranchiseId() != null) ? FransDataManager.GetFranchiseId() : String.Empty;
            int PrimaryBusinessId = Convert.ToInt32(ddlPrimaryBusiness.SelectedIndex);
            string PrimaryBusinessValue = ddlPrimaryBusiness.SelectedValue;
            int NumMarketingEmployees = Convert.ToInt32(ddlMarketingEmployee.SelectedIndex);
            string NumMarketingEmployeesValue = ddlMarketingEmployee.SelectedValue;

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(adminToolConnectionString))
                {
                    string procName = "Save_Brief_Whitepapers_Download";

                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = procName;
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.Parameters.Clear();
                        sqlCommand.Parameters.Add("@First_Name", SqlDbType.NVarChar).Value = firstName;
                        sqlCommand.Parameters.Add("@Last_Name", SqlDbType.NVarChar).Value = lastName;
                        sqlCommand.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;
                        sqlCommand.Parameters.Add("@WebSite", SqlDbType.NVarChar).Value = companyWebsite;
                        sqlCommand.Parameters.Add("@Zipcode", SqlDbType.NVarChar).Value = zipCode;
                        sqlCommand.Parameters.Add("@Primary_Business_Id", SqlDbType.Int).Value = PrimaryBusinessId;
                        sqlCommand.Parameters.Add("@Num_Marketing_Employee_Id", SqlDbType.Int).Value = NumMarketingEmployees;
                        sqlConnection.Open();
                        var reader = sqlCommand.ExecuteNonQuery();
                        isSaved = true;
                    }
                }
            }
            catch(Exception ex)
            {
                Log.WriteError(ex);
                isSaved = false;
            }

            if (isSaved)
            {
                string pdfPath = this.GetDocUrl();

                //send email
                SendWhitepapersDownloadEmail(centerId, firstName, lastName, email, companyWebsite, zipCode, ddlPrimaryBusiness.SelectedItem.Text, ddlMarketingEmployee.SelectedItem.Text, pdfPath);

                //Response.Redirect("http://" + Request.Url.Host + GetDocUrl());
                
                //if (!string.IsNullOrEmpty(pdfPath))
                //{
                    //Response.AddHeader("Content-Disposition", String.Format("attachment; filename={0}", "whitepapers.pdf"));
                    //Response.WriteFile(Server.MapPath(String.Format("~/{0}", pdfPath)));
                    //Response.End();
                    //Response.Redirect(pdfPath, true);

                    var fransId = FransDataManager.GetFranchiseId();
                    if (!string.IsNullOrEmpty(fransId))
                        Response.Redirect("/" + fransId + "/thank-you/?type=whitepaper&pdfPath=" + pdfPath);
                    else
                        Response.Redirect("/thank-you/?type=whitepaper&pdfPath=" + pdfPath);
                //}
            }
        }        
    }

    /// <summary>
    /// helper method to build email data and send to center and copy user
    /// </summary>        
    private void SendWhitepapersDownloadEmail(string centerId, string firstName, string lastName, string email, string companyWebsite, string zipCode, string PrimaryBusinessValue, string NumMarketingEmployeesValue, string docUrl)
    {
        string adminEmail = string.Empty;
        string siteUrl = string.Empty;
        string documentLink = string.Empty;
        string domainName = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
        if (!string.IsNullOrEmpty(centerId))
        {
            //var centerData = FransDataManager.GetFransData(centerId);
            //if (centerData != null && centerData.WhitePaperDownloadEmail != null)
            //    adminEmail = centerData.WhitePaperDownloadEmail;
            siteUrl = "http://" + domainName + "/" + centerId + "/";
            documentLink = "http://" + domainName + "/" + centerId + docUrl;
        }
        else
        {
            //var nationalData = SiteDataManager.GetNationalCompanyInfo();
            //if(nationalData != null)
            //{
            //   adminEmail = nationalData.WhilePaperDownloadEmail;
            //}
            siteUrl = "http://" + domainName + "/";
            documentLink = "http://" + domainName + docUrl;
        }

        //for now send ing the email req to national defined email address as per JIRA ticket # 476
        //reverted back after Kristin request 01/20/2015
        var localCenterInfo = FransDataManager.GetFransData();
        if (localCenterInfo != null && !string.IsNullOrEmpty(localCenterInfo.FransId))
        {
            adminEmail = localCenterInfo.WhitePaperDownloadEmail;
        }
        else
        {
            var nationalData = SiteDataManager.GetNationalCompanyInfo();
            if (nationalData != null)
            {
                adminEmail = nationalData.WhilePaperDownloadEmail;
            }
        }

        if (string.IsNullOrEmpty(adminEmail))
        {
            return;
        }
       
        //string siteUrl = "http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"];       
        StringBuilder sbEmailBody = new StringBuilder();

        sbEmailBody.Append("Hi,");
        sbEmailBody.Append("<br/>");
        sbEmailBody.Append("<p>The following person has downloaded a white paper/brief from your website. We recommend following up within 2 business days.</p>");
        sbEmailBody.Append("<br/>");       
        sbEmailBody.Append("<table>");
        sbEmailBody.Append("<tr><td>FirstName: </td><td>" + firstName + "</td></tr>");
        sbEmailBody.Append("<tr><td>LastName: </td><td>" + lastName + "</td></tr>");
        sbEmailBody.Append("<tr><td>Email: </td><td>" + email + "</td></tr>");
        sbEmailBody.Append("<tr><td>Website: </td><td>" + companyWebsite + "</td></tr>");
        sbEmailBody.Append("<tr><td>Zipcode: </td><td>" + zipCode + "</td></tr>");
        sbEmailBody.Append("<tr><td>White Paper Link: </td><td><a target=\"_blank\" href=\"" + documentLink + "\">" + hddnWhitePaperTitle.Value + "</a></td></tr>");
        sbEmailBody.Append("<tr><td>Primary  Business: </td><td>" + PrimaryBusinessValue + "</td></tr>");
        sbEmailBody.Append("<tr><td>Number of Marketing Employee: </td><td>" + NumMarketingEmployeesValue + "</td></tr>");
        sbEmailBody.Append("</table>");

		string whitePaperBccField = ConfigHelper.GetValueString("WhitePaperDownloadEmailBcc");
        string emailSubject = ConfigHelper.GetValueString("WhitePaperDownloadEmailSubject");
		
        Utility.SendEmail(adminEmail, email, "", sbEmailBody.ToString(), emailSubject, "", whitePaperBccField);
    }

    private string GetDocUrl()
    {
        long folderID = ConfigHelper.GetValueLong("AllBriefsAndWhitepapersFolderID");
        XmlDocument contentXML = new XmlDocument();
        long SmartFormXMLConfig = ConfigHelper.GetValueLong("BriefsAndWhitepapersSmartFormID");
        long contentId = 0;
        if (Request.QueryString["id"] != null)
        {
            long.TryParse(Request.QueryString["id"], out contentId);
        }

        //get all contents in the specified folder
        ContentCriteria criteria = new ContentCriteria();
        criteria.AddFilter(ContentProperty.FolderId, CriteriaFilterOperator.EqualTo, folderID);
        criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, SmartFormXMLConfig);
        var contents = ContentHelper.GetListByCriteria(criteria);

        if (contents != null && contents.Count > 0)
        {
            foreach (Ektron.Cms.ContentData contentData in contents)
            {
                if (contentData.Id == contentId)
                {
                    try
                    {
                        contentXML.LoadXml(contentData.Html);

                        string xml = contentXML.SelectSingleNode("/root/docUpload").InnerXml;
                        string url = Regex.Match(xml, "<a.+?href=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value; //not sure about this

                        return url;
                    }
                    catch (Exception ex)
                    {
                        Log.WriteError(ex);
                    }
                }
            }
        }
        return string.Empty;
    }

    protected void EmailBtn_Click(object sender, EventArgs e)
    {
        //get the page to be emailed
        StringWriter str_wrt = new StringWriter();
        HtmlTextWriter html_wrt = new HtmlTextWriter(str_wrt);
        Page.RenderControl(html_wrt);
        String HTML = str_wrt.ToString();
    }
}