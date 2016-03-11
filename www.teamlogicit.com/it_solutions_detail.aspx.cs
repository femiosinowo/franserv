using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

using Ektron.Cms;
using Ektron.Cms.Instrumentation;
using TeamLogic.CMS;


public partial class it_solutions_detail : PageBase
{
    private string HowWeCanHelpBackgroundImage = "/images/how_we_can_help_bkg.jpg";

    protected void Page_Load(object sender, EventArgs e)
    {
        long contentId = 0;
        if (!string.IsNullOrEmpty(Request.QueryString["id"]))
        {
            contentId = Convert.ToInt64(Request.QueryString["id"]);
        }

        if (!IsPostBack)
        {
            this.GetSolutionsDetailContent(contentId);
            this.GetHowWeCanHelpContent();

            DataTable dtCaseStudies = this.GetCaseStudiesContent();
            lvCaseStudies.DataSource = dtCaseStudies;
            lvCaseStudies.DataBind();
        }
    }

    private void GetSolutionsDetailContent(long cid)
    {
        DataTable DTSource = new DataTable();
        DTSource.Columns.Add("headline");
        DTSource.Columns.Add("subheadline");
        DTSource.Columns.Add("description");
        DTSource.Columns.Add("imgsrc");


        string pdfLink = "";
        ContentData solutionDetail = new ContentData();
        solutionDetail = ContentHelper.GetContentById(cid);
        XmlDocument contentXML = new XmlDocument();
        contentXML.LoadXml(solutionDetail.Html);
        XmlNodeList xnList0 = contentXML.SelectNodes("/root");
        if (xnList0[0]["pdfLink"] != null)
        {
            string xmlpdf = xnList0[0]["pdfLink"].InnerXml;
            pdfLink = Regex.Match(xmlpdf, "<a.+?href=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
            if (!string.IsNullOrEmpty(pdfLink))
            {
                prflink.Text = "<a href='" + pdfLink + "'>";
                pdfLinkButton.Visible = true;
            }
        }

        XmlNodeList xnList = contentXML.SelectNodes("/root/ContentSection");
        string headline = "";
        string subheadline = "";
        string description = "";
        string xml = "";
        string imgsrc = "";
        for (int i = 0; i < xnList.Count; i++)
        {
            headline = xnList[i]["headline"].InnerXml;
            subheadline = xnList[i]["subHeadline"].InnerXml;
            description = xnList[i]["description"].InnerXml;
            xml = xnList[i]["image"].InnerXml;
            imgsrc = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;

            DTSource.Rows.Add(headline, subheadline, description, imgsrc);
        }
        lvSolutionsDetail.DataSource = DTSource;
        lvSolutionsDetail.DataBind();
    }

    private DataTable GetCaseStudiesContent()
    {
        DataTable DTSource = new DataTable();
        DataTable sortedDT = new DataTable();
        XmlDocument contentXML = new XmlDocument();
        int counter = 0;

        DTSource.Columns.Add("title");
        DTSource.Columns.Add("desc");
        DTSource.Columns.Add("imgSRC");
        DTSource.Columns.Add("iconimgSRC");
        DTSource.Columns.Add("counter");
        DTSource.Columns.Add("hreftext");

        var contents = SiteDataManager.GetCaseStudies();
        if (contents != null && contents.Count > 0)
        {
            foreach (Ektron.Cms.ContentData contentData in contents)
            {
                try
                {
                    contentXML.LoadXml(contentData.Html);
                    string hreftext = contentData.Quicklink;
                    string dateCreated = contentData.DateModified.ToString();

                    XmlNodeList xnList = contentXML.SelectNodes("/root");
                    string title = xnList[0]["title"].InnerXml;
                    string desc = xnList[0]["desc"].InnerXml;
                    //string isBig = xnList[0]["isBig"].InnerXml;
                    string xml = contentXML.SelectSingleNode("/root/image").InnerXml;
                    string imgSRC = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;

                    string iconxml = contentXML.SelectSingleNode("/root/iconImage").InnerXml;
                    string iconimgSRC = Regex.Match(iconxml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;

                    counter++;
                    DTSource.Rows.Add(title, desc, imgSRC, iconimgSRC, counter, hreftext);
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }
        }
        return DTSource;
    }

    private void GetHowWeCanHelpContent()
    {
        long supplementCId = ConfigHelper.GetValueLong("ITSolutionsHowWeCanHelpCId");
        if (supplementCId > 0)
        {
            var cData = ContentHelper.GetContentById(supplementCId);
            if (cData != null && cData.Html != string.Empty)
            {
                if (!string.IsNullOrEmpty(cData.Image) && cData.Image.ToLower().IndexOf("workarea") <= -1)
                {
                    HowWeCanHelpBackgroundImage = "/" + cData.Image;
                    how_we_can_help.Attributes.Add("data-image", HowWeCanHelpBackgroundImage);
                    how_we_can_help.Attributes.Add("data-image-mobile", HowWeCanHelpBackgroundImage);
                }


                try
                {
                    XDocument xDoc = XDocument.Parse(cData.Html);

                    if (xDoc.XPathSelectElement("/root/sectionTitle") != null)
                        ltrHowWeCanHelpTitle.Text = xDoc.XPathSelectElement("/root/sectionTitle").ToString();

                    var xelements = xDoc.XPathSelectElements("/root/Content");
                    if (xelements != null && xelements.Any())
                    {
                        var firstElement = xelements.ElementAt(0);
                        var secondElement = xelements.ElementAt(1);

                        if (firstElement != null)
                        {
                            string link = firstElement.XPathSelectElement("link/a") != null ? firstElement.XPathSelectElement("link/a").Attribute("href").Value : "#";
                            string imagePath = firstElement.XPathSelectElement("image/img") != null ? firstElement.XPathSelectElement("image/img").Attribute("src").Value : "";
                            ltrHowWeCanHelp.Text += "<div class=\"grid_12 left_choice\"><a href=\"" + link + "\"><img src=\"" + imagePath + "\" /><span>" + firstElement.XPathSelectElement("title").Value + "</span></a></div>";
                        }
                        if (secondElement != null)
                        {
                            string link = secondElement.XPathSelectElement("link/a") != null ? secondElement.XPathSelectElement("link/a").Attribute("href").Value : "#";
                            string imagePath = secondElement.XPathSelectElement("image/img") != null ? secondElement.XPathSelectElement("image/img").Attribute("src").Value : "";
                            ltrHowWeCanHelp.Text += "<div class=\"grid_12 right_choice\"><a href=\"" + link + "\"><img src=\"" + imagePath + "\"><span>" + secondElement.XPathSelectElement("title").Value + "</span></a></div>";
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

}
