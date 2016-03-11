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
                Scribd.Net.Service.APIKey = "64imgnx7x95qscshy0g6l";
                Scribd.Net.Service.SecretKey = "sec-5rzr8e3oec65nt1zkcthvpzxli";
                Scribd.Net.Service.PublisherID = "pub-50507450725942359735";
                Session["signedin"] = Scribd.Net.User.Login("sirspeedyprinting", "obi3-produce").ToString();
            }
            else
            {
                //Scribd.Net.Service.APIKey = "4pj5dyz6fslss5uwjbvwr";
                //Scribd.Net.Service.SecretKey = "sec-4itkd7hdmzm593mwhssbcmlir";
                //Scribd.Net.Service.PublisherID = "pub-87234964971283969157";
                //Session["signedin"] = Scribd.Net.User.Login("aprimandinitest", "onlytest123").ToString();

                //to be used in the server
                Scribd.Net.Service.APIKey = "64imgnx7x95qscshy0g6l";
                Scribd.Net.Service.SecretKey = "sec-5rzr8e3oec65nt1zkcthvpzxli";
                Scribd.Net.Service.PublisherID = "pub-50507450725942359735";
                Session["signedin"] = Scribd.Net.User.Login("sirspeedyprinting", "obi3-produce").ToString();
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

            GetCaseStudiesContent();
        }
    }

    private void GetCaseStudiesContent()
    {        
        XmlDocument contentXML = new XmlDocument();
        long contentIdParam = 0;
        if (Request.QueryString["id"] != null)
        {
            long.TryParse(Request.QueryString["id"], out contentIdParam);
        }
        int counter = 0;

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
                        ltrTitle.Text = xnList[0]["title"].InnerXml;
                        ltrDescription.Text = xnList[0]["desc"].InnerXml;
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
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }
        }
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