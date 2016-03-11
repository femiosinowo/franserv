using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using TeamLogic.CMS;
using System.Xml;
using Ektron.Cms.Common;
using Ektron.Cms.Content;
using System.Text.RegularExpressions;
using Ektron.Cms.Instrumentation;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Xml.XPath;
using System.Xml.Linq;

public partial class briefs_whitepapers_details : PageBase
{
    public string HowWeCanHelpBackgroundImage = "/images/how_we_can_help_bkg.jpg";
    private static string adminToolConnectionString = ConfigurationManager.ConnectionStrings["TeamLogicITAdminTool.DbConnection"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadPrimaryBusiness();
            LoadMarketingEmployee();
            GetSupplementOutsourcingContent();

            UxBriefsWhitepapers.DataSource = GetBriefsWHitepaperContent();
            UxBriefsWhitepapers.DataBind();
        }
    }

    private DataTable GetBriefsWHitepaperContent()
    {
        DataTable DTSource = new DataTable();
        DataTable sortedDT = new DataTable();
        long folderID = ConfigHelper.GetValueLong("AllBriefsAndWhitepapersFolderID");
        XmlDocument contentXML = new XmlDocument();
        long SmartFormXMLConfig = ConfigHelper.GetValueLong("BriefsAndWhitepapersSmartFormID");
        long contentId = 0;
        if (Request.QueryString["id"] != null)
        {
            contentId = Convert.ToInt64(Request.QueryString["id"]);
        }
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
                        string imgSRC = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                        xml = contentXML.SelectSingleNode("/root/url").InnerXml;
                        string url = Regex.Match(xml, "<a.+?href=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value; //not sure about this
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
        return sortedDT;
    }

    private void LoadPrimaryBusiness()
    {
        try
        {
            DataTable subjects = new DataTable();
            using (SqlConnection con = new SqlConnection(adminToolConnectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT id, label FROM Primary_Business_tbl", con);
                adapter.Fill(subjects);
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
            DataTable subjects = new DataTable();
            using (SqlConnection con = new SqlConnection(adminToolConnectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT id, label FROM Number_of_Marketing_Employeess_tbl", con);
                adapter.Fill(subjects);
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
            string NumSalesRepValue = " "; // ddlSalesReprestative.SelectedValue;
            string businessTypeCRMValue = " "; // ddlBusinessUseCRMType.SelectedValue;
            string yourLocationValue = " "; // ddlYourLocation.SelectedValue;
            string marketingChallengeValue = " "; // marketingChallenge.Value;

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
                        sqlCommand.Parameters.Add("@Number_Sales_RepValue", SqlDbType.NVarChar).Value = NumSalesRepValue;
                        sqlCommand.Parameters.Add("@Business_Type_CRMValue", SqlDbType.NVarChar).Value = businessTypeCRMValue;
                        sqlCommand.Parameters.Add("@Your_Location_Value", SqlDbType.NVarChar).Value = yourLocationValue;
                        sqlCommand.Parameters.Add("@Marketing_Challenge_Value", SqlDbType.NVarChar).Value = marketingChallengeValue;
                        sqlConnection.Open();
                        var reader = sqlCommand.ExecuteNonQuery();
                        isSaved = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
                isSaved = false;
            }

            if (isSaved)
            {
                string pdfPath = this.GetDocUrl();

                //send email
                SendWhitepapersDownloadEmail(centerId, firstName, lastName, email, companyWebsite, zipCode, ddlPrimaryBusiness.SelectedItem.Text, ddlMarketingEmployee.SelectedItem.Text, pdfPath, NumSalesRepValue, businessTypeCRMValue, yourLocationValue, marketingChallengeValue);

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
    private void SendWhitepapersDownloadEmail(string centerId, string firstName, string lastName, string email, string companyWebsite, string zipCode, string PrimaryBusinessValue, string NumMarketingEmployeesValue, string docUrl, string Number_Sales_RepValue, string Business_Type_CRMValue, string Your_Location_Value, string Marketing_Challenge_Value)
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
        var nationalData = SiteDataManager.GetNationalCompanyInfo();
        if (nationalData != null)
        {
            adminEmail = nationalData.WhilePaperDownloadEmail;
        }

        //check if Center selected then use the center whitepaper download email
        if(FransDataManager.IsFranchiseSelected())
        {
            var centerData = FransDataManager.GetFransData();
            if(centerData != null && !string.IsNullOrEmpty(centerData.WhitePaperDownloadEmail) )
            {
                adminEmail = centerData.WhitePaperDownloadEmail;
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
        sbEmailBody.Append("<tr><td>Number of Employee: </td><td>" + NumMarketingEmployeesValue + "</td></tr>");

        //sbEmailBody.Append("<tr><td>Number of Sales Representatives?: </td><td>" + Number_Sales_RepValue + "</td></tr>");
        //sbEmailBody.Append("<tr><td>What CRM does your business use?: </td><td>" + Business_Type_CRMValue + "</td></tr>");
        //sbEmailBody.Append("<tr><td>Where are you located?: </td><td>" + Your_Location_Value + "</td></tr>");
        //sbEmailBody.Append("<tr><td>hat is your biggest marketing challenge?: </td><td>" + Marketing_Challenge_Value + "</td></tr>");

        sbEmailBody.Append("</table>");

        string whitePaperBccField = ConfigHelper.GetValueString("WhitePaperDownloadEmailBcc");
        string emailSubject = ConfigHelper.GetValueString("WhitePaperDownloadEmailSubject");

        Utility.SendEmail(adminEmail, email, "", sbEmailBody.ToString(), emailSubject, whitePaperBccField);
    }

    private string GetDocUrl()
    {
        long folderID = ConfigHelper.GetValueLong("AllBriefsAndWhitepapersFolderID");
        XmlDocument contentXML = new XmlDocument();
        long SmartFormXMLConfig = ConfigHelper.GetValueLong("BriefsAndWhitepapersSmartFormID");
        long contentId = 0;
        if (Request.QueryString["id"] != null)
        {
            contentId = Convert.ToInt64(Request.QueryString["id"]);
        }

        //get contents
        var contentData = ContentHelper.GetContentById(contentId);
        if (contentData != null && contentData.Id > 0)
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
        return string.Empty;
    }
    
    private void GetSupplementOutsourcingContent()
    {
        long supplementCId = ConfigHelper.GetValueLong("BriefsWhitePapersHowCanWeHelpYouCId");
        if (supplementCId > 0)
        {
            var cData = ContentHelper.GetContentById(supplementCId);
            if (cData != null && cData.Html != string.Empty)
            {
                if (!string.IsNullOrEmpty(cData.Image) && cData.Image.ToLower().IndexOf("workarea") <= -1)
                {
                    HowWeCanHelpBackgroundImage = "/" + cData.Image;
                    how_we_can_help_Img.Attributes.Add("data-image", HowWeCanHelpBackgroundImage);
                    how_we_can_help_Img.Attributes.Add("data-image-mobile", HowWeCanHelpBackgroundImage);
                }
                try
                {
                    XDocument xDoc = XDocument.Parse(cData.Html);
                    var xelements = xDoc.XPathSelectElements("/root/Content");
                    StringBuilder sb = new StringBuilder();
                    if (xelements != null && xelements.Any())
                    {
                        var firstElement = xelements.ElementAt(0);
                        var secondElement = xelements.ElementAt(1);
                        if (xDoc.XPathSelectElement("/root/sectionTitle") != null)
                            sb.Append("<div class=\"how_we_can_help_content\"><h2>" + xDoc.XPathSelectElement("/root/sectionTitle").Value + "</h2></div>");
                        sb.Append(" <div class=\"clear\"></div>");

                        sb.Append("<div class=\"bottom_header clearfix\">");
                        sb.Append(" <div class=\"container_24\">");

                        if (firstElement != null)
                        {
                            string link = firstElement.XPathSelectElement("link/a") != null ? firstElement.XPathSelectElement("link/a").Attribute("href").Value : "#";
                            string imagePath = firstElement.XPathSelectElement("image/img") != null ? firstElement.XPathSelectElement("image/img").Attribute("src").Value : "";
                            sb.Append("<div class=\"grid_12 left_choice\"><a href=\"" + link + "\"><img src=\"" + imagePath + "\" /><span>" + firstElement.XPathSelectElement("title").Value + "</span></a></div>");
                        }
                        if (secondElement != null)
                        {
                            string link = secondElement.XPathSelectElement("link/a") != null ? secondElement.XPathSelectElement("link/a").Attribute("href").Value : "#";
                            string imagePath = secondElement.XPathSelectElement("image/img") != null ? secondElement.XPathSelectElement("image/img").Attribute("src").Value : "";
                            sb.Append("<div class=\"grid_12 right_choice\"><a href=\"" + link + "\"><img src=\"" + imagePath + "\"><span>" + secondElement.XPathSelectElement("title").Value + "</span></a></div>");
                        }
                        sb.Append("</div>");
                        sb.Append("</div>");
                        ltrSupplementOutSourcing.Text = sb.ToString();
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }
        }
    }

}