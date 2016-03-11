using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using TeamLogic.CMS;
using Ektron.Cms.Instrumentation;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Text;

public partial class UserControls_HomePageBanners : System.Web.UI.UserControl
{
    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.GetHomeBannerContent();
            this.GetSupplementOutsourcingContent();
        }
    }

    /// <summary>
    /// helper method to get banner images
    /// </summary>
    /// <returns></returns>
    private void GetHomeBannerContent()
    {
        var bannerContents = SiteDataManager.GetBannerContent();
        if (bannerContents != null && bannerContents.Any())
        {
            try
            {
                var contentData = from obj in bannerContents
                                  select new
                                  {
                                      Id = obj.Id,
                                      Quicklink = obj.Quicklink,
                                      Html = XElement.Parse(obj.Html)
                                  };

                var bannerItems = from obj in contentData
                                  select new
                                  {
                                      SubTitle = obj.Html.XPathSelectElement("subTitle") != null ? "<p>" + obj.Html.XPathSelectElement("subTitle").ToString() + "</p>" : string.Empty,
                                      ImagePath = obj.Html.XPathSelectElement("/image/img") != null ? obj.Html.XPathSelectElement("/image/img").Attribute("src").Value : string.Empty,
                                      Title = this.FormattedBannerTitle(obj.Html.Elements("TitleSection"), obj.Html.XPathSelectElement("/link/a"))
                                  };

                var bannerTabInfo = from obj in contentData
                                    select new
                                    {
                                        TabTitle = obj.Html.XPathSelectElement("tabTitle") != null ? obj.Html.XPathSelectElement("tabTitle").Value : string.Empty,
                                        ClassName = obj.Html.XPathSelectElement("tabColor").Value
                                    };


                lvHeaderMenuItems.DataSource = bannerItems;
                lvHeaderMenuItems.DataBind();

                lvBannerTabs.DataSource = bannerTabInfo;
                lvBannerTabs.DataBind();

            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
        }
    }
    
    private string FormattedBannerTitle(IEnumerable<XElement> titleSection, XElement link)
    {
        string formattedText = string.Empty;
        if (titleSection != null)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var xElement in titleSection)
            {
                sb.Append("<h2><span>");

                if (xElement.XPathSelectElement("titlePlainText1") != null)
                    sb.Append(xElement.XPathSelectElement("titlePlainText1").Value);
                if (xElement.XPathSelectElement("titleColorText") != null)
                    sb.Append(" <span class=\"green\"> " + xElement.XPathSelectElement("titleColorText").Value + "</span> ");
                if (xElement.XPathSelectElement("titlePlainText2") != null)
                    sb.Append(xElement.XPathSelectElement("titlePlainText2").Value);

                sb.Append("</span></h2>");
                sb.Append("<div class=\"clear_text\"></div>");
            }

            if (link != null)
            {
                string url = link.Attribute("href").Value;
                string target = "_self";
                if (link.Attribute("target") != null)
                    target = link.Attribute("target").Value;
                formattedText = "<a href=\"" + url + "\" target=\"" + target + "\">" + sb.ToString() + "</a>";
            }
            else
            {
                formattedText = sb.ToString();
            }
        }
        return formattedText;
    }

    private void GetSupplementOutsourcingContent()
    {
        long supplementCId = ConfigHelper.GetValueLong("HomePageNeedSupplementsAndITOutsourcingCId");
        if (supplementCId > 0)
        {
            var cData = ContentHelper.GetContentById(supplementCId);
            if (cData != null && cData.Html != string.Empty)
            {
                try
                {
                    XDocument xDoc = XDocument.Parse(cData.Html);
                    var xelements = xDoc.XPathSelectElements("/root/Content");

                    if (xelements != null && xelements.Any())
                    {
                        var firstElement = xelements.ElementAt(0);
                        var secondElement = xelements.ElementAt(1);

                        if (firstElement != null)
                        {
                            string link = firstElement.XPathSelectElement("link/a") != null ? firstElement.XPathSelectElement("link/a").Attribute("href").Value : "#";
                            string imagePath = firstElement.XPathSelectElement("image/img") != null ? firstElement.XPathSelectElement("image/img").Attribute("src").Value : "";
                            ltrSupplementOutSourcing.Text += "<div class=\"grid_12 left_choice\"><a href=\"" + link + "\"><img src=\"" + imagePath + "\" /><span>" + firstElement.XPathSelectElement("title").Value + "</span></a></div>";
                        }
                        if (secondElement != null)
                        {
                            string link = secondElement.XPathSelectElement("link/a") != null ? secondElement.XPathSelectElement("link/a").Attribute("href").Value : "#";
                            string imagePath = secondElement.XPathSelectElement("image/img") != null ? secondElement.XPathSelectElement("image/img").Attribute("src").Value : "";
                            ltrSupplementOutSourcing.Text += "<div class=\"grid_12 right_choice\"><a href=\"" + link + "\"><img src=\"" + imagePath + "\"><span>" + secondElement.XPathSelectElement("title").Value + "</span></a></div>";
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