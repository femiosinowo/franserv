using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using SignalGraphics.CMS;
using Ektron.Cms.Instrumentation;
using Figleaf;

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
            DataTable DTHomeBannerSource = GetHomeBannerContent();
            UxHomeSlider.DataSource = DTHomeBannerSource;
            UxHomeSlider.DataBind();
        }
    }

    /// <summary>
    /// helper method to get banner images
    /// </summary>
    /// <returns></returns>
    private DataTable GetHomeBannerContent()
    {

        string fransId = FransDataManager.IsFranchiseSelected() ? FransDataManager.GetFranchiseId() : "0";
        string cacheKey = String.Format("SignalGraphics:UserControlsHomePageBanners:GetHomeBannerContent:FranchiseId={0}",
            fransId);

        DataTable sortedDT = CacheBase.Get<DataTable>(cacheKey);

        if (sortedDT == default(DataTable) || sortedDT.Rows.Count == 0)
        {
            DataTable DTSource = new DataTable();
            //   DataTable sortedDT = new DataTable();
            XmlDocument contentXML = new XmlDocument();
            int counter = 0;

            DTSource.Columns.Add("index");
            DTSource.Columns.Add("text");
            DTSource.Columns.Add("imageSRC");
            DTSource.Columns.Add("backImageSRC");
            DTSource.Columns.Add("url");
            DTSource.Columns.Add("target");
            DTSource.Columns.Add("counter");
            DTSource.Columns.Add("styleimage");
            DTSource.Columns.Add("stylecaption");
            DTSource.Columns.Add("subtitle");
            DTSource.Columns.Add("H4Styles");


            var bannerContents = SiteDataManager.GetBannerContent();
            if (bannerContents != null && bannerContents.Any())
            {
                foreach (Ektron.Cms.ContentData contentData in bannerContents)
                {
                    try
                    {
                        contentXML.LoadXml(contentData.Html);
                        XmlNodeList xnList = contentXML.SelectNodes("/root");
                        string text = xnList[0]["text"].InnerXml;
                        int index = Convert.ToInt32(xnList[0]["index"].InnerXml);

                        string xml = contentXML.SelectSingleNode("/root/image").InnerXml;
                        string imageSRC = "";
                        string imgSRC =
                            Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                        if (!string.IsNullOrEmpty(imgSRC))
                            imageSRC = "<img src=\"" + imgSRC + "\" alt=\"" + text + "\" />";

                        xml = contentXML.SelectSingleNode("/root/backgroundImage").InnerXml;
                        string backImageSRC =
                            Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                        xml = "";
                        string target = "_self";

                        if (contentXML.SelectSingleNode("/root/url") != null)
                        {
                            xml = contentXML.SelectSingleNode("/root/url").InnerXml;
                            if (contentXML.SelectSingleNode("/root/url/a") != null &&
                                contentXML.SelectSingleNode("/root/url/a").Attributes["target"] != null)
                                target = contentXML.SelectSingleNode("/root/url/a").Attributes["target"].Value;
                        }
                        string url =
                            Regex.Match(xml, "<a.+?href=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                        string backgroundImage = "";

                        ///Used the display the Image and Text in Left or Right position based on the selection
                        int displayFlag = 0;
                        if (contentXML.SelectSingleNode("/root/Text-Image-Display/TextImageDisp") != null)
                        {
                            xml = contentXML.SelectSingleNode("/root/Text-Image-Display/TextImageDisp").InnerText;
                            if (!String.IsNullOrWhiteSpace(xml))
                                displayFlag = Convert.ToInt16(xml);
                        }

                        if (!String.IsNullOrWhiteSpace(backImageSRC))
                        {
                            if (displayFlag != 1)
                                backgroundImage =
                                    "style=\"width: 1899px; float: left; display: block; background-image: url('" +
                                    backImageSRC + "'); background-position: right;\" class=\"flex-active-slide\"";
                            else
                                backgroundImage =
                                    "style=\"width: 1899px; float: left; display: block; background-image: url('" +
                                    backImageSRC + "'); background-position: left;\" class=\"flex-active-slide\"";
                        }
                        else
                        {
                            if (displayFlag != 1)
                                backgroundImage =
                                    "style=\"width: 1899px; float: left; display: block;  background-image: url('" +
                                    "../images/home_slider_bg.jpg" +
                                    "'); background-position: right;\" class=\"flex-active-slide\"";
                            else
                                backgroundImage =
                                    "style=\"width: 1899px; float: left; display: block;  background-image: url('" +
                                    "../images/home_slider_bg.jpg" + "')\" class=\"flex-active-slide\"";
                        }

                        string styleimage = "";
                        string stylecaption = "";

                        if (displayFlag == 1)
                        {
                            styleimage = "float:left";
                            stylecaption = "float:right";
                        }
                        else
                        {
                            styleimage = "float:right";
                            stylecaption = "float:left";
                        }
                        xml = "";

                        string subtitle = "";
                        if (contentXML.SelectSingleNode("/root/subtitle") != null)
                        {
                            xml = contentXML.SelectSingleNode("/root/subtitle").InnerXml;
                            if (!String.IsNullOrWhiteSpace(xml))
                                subtitle = xml;
                        }

                        string H4Styles = "top:27%";
                        if (contentXML.SelectSingleNode("/root/topSpacing") != null)
                        {
                            H4Styles = "top:" + contentXML.SelectSingleNode("/root/topSpacing").InnerText.Trim() + "%";
                        }

                        counter++;
                        DTSource.Rows.Add(index, text, imageSRC, backgroundImage, url, target, counter, styleimage,
                            stylecaption, subtitle, H4Styles);
                    }
                    catch (Exception ex)
                    {
                        Log.WriteError(ex);
                    }
                }
                DataView DVPSMMSort = DTSource.DefaultView;
                //DVPSMMSort.Sort = "index asc";
                sortedDT = DVPSMMSort.ToTable();

                CacheBase.Put(cacheKey, sortedDT, CacheDuration.For24Hr);
            }
        }
        return sortedDT;
    }
}