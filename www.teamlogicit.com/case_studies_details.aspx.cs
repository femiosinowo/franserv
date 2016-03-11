using System;
using System.Linq;
using System.Web.UI;
using TeamLogic.CMS;
using System.Xml;
using System.Text.RegularExpressions;
using Ektron.Cms.Instrumentation;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Text;
using System.Data;
using Scribd.Net;

public partial class case_studies_details : PageBase
{
    public string HowWeCanHelpBackgroundImage = "/images/how_we_can_help_bkg.jpg";
    public string uploadDocPath = "";
    public int uploadDocId = 0;
    public string accesskey = "' '";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetSupplementOutsourcingContent();
            how_we_can_help_img.Attributes.Add("data-image", HowWeCanHelpBackgroundImage);
            how_we_can_help_img.Attributes.Add("data-image-mobile", HowWeCanHelpBackgroundImage);

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

            //UxCaseStudies.DataSource = GetCaseStudiesContent();
            //UxCaseStudies.DataBind();
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
            contentIdParam = Convert.ToInt64(Request.QueryString["id"]);
        }

        foreach (Document doc in documents)
        {
            long CSDocId = GetCaseStudiesScribdDocumentId(contentIdParam);
            scribIdIframe.Text = "<iframe class=\"scribd_iframe_embed\" src=\"//www.scribd.com/embeds/" + CSDocId + "/content?start_page=1&amp;view_mode=scroll&amp;access_key=key-1bye002udmmumga4kqrv&amp;show_recommendations=true\" data-auto-height=\"false\" data-aspect-ratio=\"0.772922022279349\" scrolling=\"yes\" id=\"doc_95172\" width=\"100%\" height=\"600\" frameborder=\"0\"></iframe>";
            

            if (doc.DocumentId == CSDocId)
            {
                uploadDocId = doc.DocumentId;
                accesskey = "'" + doc.AccessKey + "'";
            }
        }
    }

    private long GetCaseStudiesScribdDocumentId(long casestudiesId)
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

                        return Convert.ToInt64(id);
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

    private void GetSupplementOutsourcingContent()
    {
        long supplementCId = ConfigHelper.GetValueLong("CaseStudiesIHowCanWeHelpYouCId");
        if (supplementCId > 0)
        {
            var cData = ContentHelper.GetContentById(supplementCId);
            if (cData != null && cData.Html != string.Empty)
            {
                HowWeCanHelpBackgroundImage = "/" + cData.Image;
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