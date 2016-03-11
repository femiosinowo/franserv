using System;
using System.Linq;
using System.Web.UI;
using TeamLogic.CMS;
using Ektron.Cms;
using Ektron.Cms.Instrumentation;
using System.Xml;
using System.Text.RegularExpressions;
using System.Text;
using System.Xml.XPath;
using System.Xml.Linq;

public partial class partners : PageBase
{
    public string HowWeCanHelpBackgroundImage = "/images/how_we_can_help_bkg.jpg";

    protected void Page_Init(object sender, EventArgs e)
    {
        cbIntro.DefaultContentID = ConfigHelper.GetValueLong("PartnerPageDescriptionCId");
        cbIntro.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbIntro.Fill();
        
    }


    // <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetPartners();
            GetSupplementOutsourcingContent();
        }
        how_we_can_help_img.Attributes.Add("data-image", HowWeCanHelpBackgroundImage);
        how_we_can_help_img.Attributes.Add("data-image-mobile", HowWeCanHelpBackgroundImage);        
    }

    private void GetPartners()
    {
        var contents = M3TDataManager.GetPartners();
        if (contents != null && contents.Count > 0)
        {
            try
            {
                int count = 1;
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i <= contents.Count / 4; i++)
                {
                    sb.Append("<div class=\"container_24 partner_row\" id=\"row_" + count + "\">");
                    sb.Append("<div class=\"grid_24 partner_logo_wrapper\">");
                    var filteredEmployeeList = contents.Skip(i * 4).Take(4).ToList();

                    string cssClass = "";
                    for (int k = 0; k < filteredEmployeeList.Count(); k++)
                    {
                        if (k == 0)
                            cssClass = "alpha";
                        else if (k == 3)
                            cssClass = "omega";

                        sb.Append("<div class=\"grid_6 " + cssClass + " partner_logo\">");
                        sb.Append(this.GetPartnersInfo(filteredEmployeeList[k]));
                        sb.Append("</div>");
                        cssClass = "";
                    }
                    sb.Append("</div>");
                    sb.Append(" </div>");
                    sb.Append("<div class=\"clear\"></div>");

                    sb.Append("<div class=\"partners_detail_wrapper\" id=\"detail_box_" + count + "\">");
                    sb.Append("<div class=\"container_24\">");
                    sb.Append("<div class=\"grid_24\">");
                    sb.Append("<a class=\"close_button\"><span class=\"visuallyhidden\">X</span></a>");
                    sb.Append("<div class=\"detail_content\"></div>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                    count++;
                }
                ltrPartners.Text = sb.ToString();
            }
            catch(Exception ex)
            {
                Log.WriteError(ex);
            }
        }
    }

    private string GetPartnersInfo(ContentData contentData)
    {
        StringBuilder sbHtml = new StringBuilder();
        XmlDocument contentXML = new XmlDocument();
        contentXML.LoadXml(contentData.Html);

        XmlNodeList xnList = contentXML.SelectNodes("/root");
        if (xnList != null && xnList.Count > 0)
        {           
            string tagline = xnList[0]["tagline"].InnerXml;
            string teaser = xnList[0]["teaser"].InnerXml;
            string companyName = xnList[0]["companyName"].InnerXml.Replace(" ", "");
            string compyName = companyName.Trim().Replace(" ", "");
            string xml = contentXML.SelectSingleNode("/root/image").InnerXml;
            string imgSRC = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
            string alt = Regex.Match(xml, "<img.+?alt=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
            xml = contentXML.SelectSingleNode("/root/url").InnerXml;
            string url = Regex.Match(xml, "<a.+?href=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;

            string dateCreated = contentData.DateCreated.ToString();
            sbHtml.Append("<a href=\"#\" class=\"logo\" id=\"" + compyName + "\">");
            sbHtml.Append("<img src=\"" + imgSRC + "\" alt=\"\" />");
            sbHtml.Append("</a>");
            sbHtml.Append("<div id=\"" + compyName + "_detail\" class=\"partner_detail clearfix\">");
            sbHtml.Append("<div class=\"grid_10 prefix_1 alpha partner_logo_large\">");
            sbHtml.Append("<img src=\"" + imgSRC + "\" alt=\"\" />");
            sbHtml.Append("</div>");
            sbHtml.Append("<div class=\"grid_12 suffix_1 omega partner_text\">");
            sbHtml.Append(" <div class=\"partner_text_inner\">");
            sbHtml.Append("<h3>" + companyName + "</h3>");
            sbHtml.Append("<p>" + teaser + "</p>");
            sbHtml.Append("<div class=\"square_button\"><a href=\"" + url + "\">" + url + "</a></div>");
            sbHtml.Append("</div>");
            sbHtml.Append("</div>");
            sbHtml.Append("</div>");                        
        }
        return sbHtml.ToString();
    }

    private void GetSupplementOutsourcingContent()
    {
        long supplementCId = ConfigHelper.GetValueLong("PartnerHowWeCanHelpYouCId");
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