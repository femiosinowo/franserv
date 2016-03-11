using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SirSpeedy.CMS;
using System.Xml;
using System.Text.RegularExpressions;
using Ektron.Cms.Instrumentation;
using System.Data;
using Scribd.Net;
using System.IO;

public partial class case_studies_details : System.Web.UI.Page
{
    public string uploadDocPath = "";
    public int uploadDocId = 0;
    public string accesskey = "' '";

    /// <summary>
    /// page init event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Init(object sender, EventArgs e)
    {
        cbCaseStudiesDetailsHeaderImage.DefaultContentID = ConfigHelper.GetValueLong("CaseStudiesDetailPageBannerImgCId");
        cbCaseStudiesDetailsHeaderImage.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbCaseStudiesDetailsHeaderImage.Fill();

        cbCaseStudiesDetailsSideContent.DefaultContentID = ConfigHelper.GetValueLong("CaseStudiesDetailsSideContentID");
        cbCaseStudiesDetailsSideContent.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbCaseStudiesDetailsSideContent.Fill();

        //cbMediaInquires.DefaultContentID = ConfigHelper.GetValueLong("NewsMediaInquiresContentID");
        //cbMediaInquires.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        //cbMediaInquires.Fill();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            uploadDocPath = "";

            if (Session["signedin"] == null)
            {
                //for local testing
                //Scribd.Net.Service.APIKey = "4pj5dyz6fslss5uwjbvwr";
                //Scribd.Net.Service.SecretKey = "sec-4itkd7hdmzm593mwhssbcmlir";
                //Scribd.Net.Service.PublisherID = "pub-87234964971283969157";
                //Session["signedin"] = Scribd.Net.User.Login("aprimandinitest", "onlytest123").ToString();

                //to be used in the server
                Scribd.Net.Service.APIKey = "13licuhynbqqxmgbx546s";
                Scribd.Net.Service.SecretKey = "sec-2a9twlu2nc5a6rwj4ae9kltbjn";
                Scribd.Net.Service.PublisherID = "pub-45928418582887861444";
                Session["signedin"] = Scribd.Net.User.Login("sirspeedyprinting", "obi3-produce").ToString();
            }
            else
            {
                //Scribd.Net.Service.APIKey = "4pj5dyz6fslss5uwjbvwr";
                //Scribd.Net.Service.SecretKey = "sec-4itkd7hdmzm593mwhssbcmlir";
                //Scribd.Net.Service.PublisherID = "pub-87234964971283969157";
                //Session["signedin"] = Scribd.Net.User.Login("aprimandinitest", "onlytest123").ToString();

                //to be used in the server
                Scribd.Net.Service.APIKey = "13licuhynbqqxmgbx546s";
                Scribd.Net.Service.SecretKey = "sec-2a9twlu2nc5a6rwj4ae9kltbjn";
                Scribd.Net.Service.PublisherID = "pub-45928418582887861444";
                Session["signedin"] = Scribd.Net.User.Login("pipprinting", "obi3-produce").ToString();
            }

            if (bool.Parse(Session["signedin"].ToString()) == false)
            {
                Response.Write("Invalid login.");
                Response.End();
            }
            else
            {
                if (!IsPostBack)
                {
                    Scribd.Net.Service.User.ReloadDocuments();
                    BindScribdDocuments(Scribd.Net.Service.User.Documents);
                }
            }

            UxCaseStudies.DataSource = GetCaseStudiesContent();
            UxCaseStudies.DataBind();
        }
    }

    private DataTable GetCaseStudiesContent()
    {
        DataTable DTSource = new DataTable();
        DataTable sortedDT = new DataTable();
        XmlDocument contentXML = new XmlDocument();
        long contentIdParam = 0;
        if (Request.QueryString["id"] != null)
        {
            contentIdParam = Convert.ToInt64(Request.QueryString["id"]);
        }
        int counter = 0;

        DTSource.Columns.Add("title");
        DTSource.Columns.Add("desc");
        DTSource.Columns.Add("documentUpload");
        DTSource.Columns.Add("imgSRC");
        DTSource.Columns.Add("counter");
        DTSource.Columns.Add("hreftext");
        DTSource.Columns.Add("cssClass");
        DTSource.Columns.Add("dateCreated");

        var contents = SiteDataManager.GetCaseStudies();
        if (contents != null && contents.Count > 0)
        {
            foreach (Ektron.Cms.ContentData contentData in contents)
            {
                try
                {

                    if (contentData.Id == contentIdParam)
                    {
                        contentXML.LoadXml(contentData.Html);

                        long contentId = contentData.Id;
                        string hreftext = contentData.Quicklink;
                        string dateCreated = contentData.DateModified.ToString();

                        XmlNodeList xnList = contentXML.SelectNodes("/root");
                        string title = xnList[0]["title"].InnerXml;
                        string desc = xnList[0]["desc"].InnerXml;
                        string isBig = xnList[0]["isBig"].InnerXml;

                        string xml = contentXML.SelectSingleNode("/root/image").InnerXml;
                        string imgSRC = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;

                        xml = contentXML.SelectSingleNode("/root/fileUpload").InnerXml;
                        string documentUpload = Regex.Match(xml, "<a.+?href=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value; //not sure about this

                        uploadDocPath = "'http://" + Request.Url.Host + documentUpload + "'";

                        counter++;

                        string cssClass = "";
                        if (counter == 1 || ((((counter - 1) % 4)) == 0))
                        {
                            cssClass = "alpha";
                        }
                        else if (counter % 4 == 0)
                        {
                            cssClass = "omega";
                        }

                        if (contentId == contentIdParam)
                        {
                            DTSource.Rows.Add(title, desc, documentUpload, imgSRC, counter, hreftext, cssClass, dateCreated);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }
            DataView DVPSMMSort = DTSource.DefaultView;
            DVPSMMSort.Sort = "dateCreated desc";
            sortedDT = DVPSMMSort.ToTable();
        }
        return sortedDT;
    }

    private void BindScribdDocuments(System.Collections.Generic.List<Document> documents)
    {
       long contentIdParam = 0;
        if (Request.QueryString["id"] != null)
        {
            long.TryParse(Request.QueryString["id"], out contentIdParam);
        }

        //foreach (Document doc in documents)
        //{
        //    int CSDocId = GetCaseStudiesScribdDocumentId(contentIdParam);
        //    if (doc.DocumentId == CSDocId)
        //    {
        //        uploadDocId = doc.DocumentId;
        //        accesskey = "'" + doc.AccessKey + "'";
        //    }
        //}

        int CSDocId = GetCaseStudiesScribdDocumentId(contentIdParam);
        if (CSDocId > 0)
        {
            uploadDocId = CSDocId;
        }
    }

    private int GetCaseStudiesScribdDocumentId(long casestudiesId)
    {
        XmlDocument contentXML = new XmlDocument();

        var contents = SiteDataManager.GetCaseStudies();
        if (contents != null && contents.Count > 0)
        {
            foreach (Ektron.Cms.ContentData contentData in contents)
            {
                try
                {
                    if (casestudiesId == contentData.Id)
                    {

                        contentXML.LoadXml(contentData.Html);
                        XmlNodeList xnList = contentXML.SelectNodes("/root");
                        string scribdUrl = xnList[0]["scribdUrl"].InnerXml;
                        var scribdUrlSplit = scribdUrl.Split('/');
                        int indexOfId = 0;
                        for (int i = 0; i < scribdUrlSplit.Length; i++)
                        {
                            if (scribdUrlSplit[i].Equals("doc"))
                            {
                                indexOfId = i + 1;
                                break;
                            }
                        }
                        string id = scribdUrlSplit[indexOfId];

                        return Convert.ToInt32(id);
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }
        }

        return 0;
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