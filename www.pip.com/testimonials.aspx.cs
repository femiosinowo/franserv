using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SirSpeedy.CMS;
using System.Xml;
using Ektron.Cms.Content;
using Ektron.Cms.Common;
using System.Text.RegularExpressions;
using Ektron.Cms.Instrumentation;

public partial class testimonials : System.Web.UI.Page
{
    private int defaultCount = 10;

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.BodyClass = " testimonials ";
       
        if (!Page.IsPostBack)
        {
            if (FransDataManager.IsFranchiseSelected())
            {
                this.GetLocalCenterTestimonials(defaultCount);
            }
            else
            {
                UxTestimonials.DataSource = GetTestimonialsContent(defaultCount);
                UxTestimonials.DataBind();
            }
        }
    }

    //protected void LoadMoreLinkButton_Click(object sender, EventArgs e)
    //{
    //    if (Page.IsValid)
    //    {
    //        int requestCount;
    //        int.TryParse(hdnDisplayCount.Value, out requestCount);
    //        requestCount = requestCount + defaultCount;
    //        if (FransDataManager.IsFranchiseSelected())
    //        {
    //            this.GetLocalCenterTestimonials(requestCount);
    //        }
    //        else
    //        {
    //            UxTestimonials.DataSource = GetTestimonialsContent(requestCount);
    //            UxTestimonials.DataBind();
    //        }
    //    }
    //}

    private DataTable GetTestimonialsContent(int reqCount)
    {
        DataTable DTSource = new DataTable();
        DataTable sortedDT = new DataTable();
        long folderID = ConfigHelper.GetValueLong("TestimonialsFolderID");
        XmlDocument contentXML = new XmlDocument();
        long SmartFormXMLConfig = ConfigHelper.GetValueLong("TestimonialsSmartFormID");
        int counter = 0;
        string title = string.Empty;
        DTSource.Columns.Add("statement");
        DTSource.Columns.Add("firstName");
        DTSource.Columns.Add("lastName");
        DTSource.Columns.Add("organization");
        DTSource.Columns.Add("imageSRC");
        DTSource.Columns.Add("cssClassText");
        DTSource.Columns.Add("logo_wrapper");
        DTSource.Columns.Add("submittedDate");
        DTSource.Columns.Add("counter");
        DTSource.Columns.Add("title");

        //get all contents in the specified folder
        ContentCriteria criteria = new ContentCriteria();
        criteria.AddFilter(ContentProperty.FolderId, CriteriaFilterOperator.EqualTo, folderID);
        criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, SmartFormXMLConfig);
        var contents = ContentHelper.GetListByCriteria(criteria);

        if (contents != null && contents.Count > 0)
        {
            //var customFilter = contents.Take(reqCount).ToList();
            var customFilter = contents.ToList();
            foreach (Ektron.Cms.ContentData contentData in customFilter)
            {
                try
                {
                    contentXML.LoadXml(contentData.Html);

                    string submittedDate = contentData.DateCreated.ToString();

                    XmlNodeList xnList = contentXML.SelectNodes("/root");
                    string statement = xnList[0]["Statement"].InnerXml;
                    string firstName = xnList[0]["firstName"].InnerXml;
                    string lastName = xnList[0]["lastName"].InnerXml;
                    string organization = ", " + xnList[0]["organization"].InnerXml;

                    string xml = contentXML.SelectSingleNode("/root/imageSRC").InnerXml;
                    string imgSRC = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                    counter++;

                    string cssClass = "";
                    string logo_wrapper = "";

                    if (!String.IsNullOrWhiteSpace(imgSRC))
                    {
                        cssClass = "source_logo";
                        logo_wrapper = "<div class=\"logo_wrapper\"><div class=\"logo_content\"><img src=\"" + imgSRC + "\" alt=\"logo\" /></div></div>";
                    }

                    DTSource.Rows.Add(statement, firstName, lastName, organization, imgSRC, cssClass, logo_wrapper, submittedDate, counter, title);
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }
            DataView DVPSMMSort = DTSource.DefaultView;
            DVPSMMSort.Sort = "submittedDate desc";
            sortedDT = DVPSMMSort.ToTable();

            //hdnDisplayCount.Value = reqCount.ToString();
            //if (contents.Count <= reqCount)
            //    loadMoreContent.Visible = false;
        }
        return sortedDT;
    }

    private void GetLocalCenterTestimonials(int reqCount)
    {
        var centerId = FransDataManager.GetFranchiseId();
        var allCenterTestimoials = FransDataManager.GetAllTestimonials(centerId);
        if (allCenterTestimoials != null && allCenterTestimoials.Any())
        {
           // var customFilter = allCenterTestimoials.Take(reqCount).ToList();
            var customFilter = allCenterTestimoials.ToList();
            var testimonials = from t in customFilter
                               select new
                               {
                                   firstName = t.FirstName,
                                   lastName = t.LastName,
                                   organization = t.Organization != null && t.Organization != string.Empty ? ", " + t.Organization : "",
                                   logo_wrapper = t.PicturePath != null && t.PicturePath != string.Empty ? "<div class=\"logo_wrapper\"><img src=\"" + t.PicturePath + "\" alt=\"logo\" /></div>" : "",
                                   cssClassText = t.PicturePath != null && t.PicturePath != string.Empty ? "source_logo" : "",
                                   statement = t.Statement.Replace("''", "'"),
                                   submittedDate = t.Created_Date,
                                   title = t.Title
                               };
            UxTestimonials.DataSource = testimonials;
            UxTestimonials.DataBind();

            //hdnDisplayCount.Value = reqCount.ToString();
            //if (allCenterTestimoials.Count <= reqCount)
            //    loadMoreContent.Visible = false;
        }
        else
        {
            pnlNoTestimonials.Visible = true;
        }
    }

}