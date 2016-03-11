using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using SirSpeedy.CMS;
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
        string cacheKey = String.Format("Pip:UserControlsHomePageBanners:GetHomeBannerContent:FranchiseId={0}",
            fransId);

        DataTable sortedDT = CacheBase.Get<DataTable>(cacheKey);

        if (sortedDT == default(DataTable) || sortedDT.Rows.Count == 0)
        {
            DataTable DTSource = new DataTable();
            // DataTable sortedDT = new DataTable();
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
                        if (contentXML.SelectSingleNode("/root/url") != null)
                            xml = contentXML.SelectSingleNode("/root/url").InnerXml;
                        string url =
                            Regex.Match(xml, "<a.+?href=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                            //not sure about this

                        string target = "";
                        string backgroundImage = "";

                        //if (!String.IsNullOrWhiteSpace(backImageSRC))
                        //{
                        //    backgroundImage = "style=\"width: 1899px; height:auto; float: left; display: block; background-image: url('" + backImageSRC + "')\" class=\"flex-active-slide\"";
                        //}
                        //else
                        //{
                        //    backgroundImage = "style=\"width: 1899px; height:auto; float: left; display: block;  background-image: url('" + "../images/home_slider_bg.jpg" + "')\" class=\"flex-active-slide\"";
                        //}


                        if (!String.IsNullOrWhiteSpace(backImageSRC))
                        {
                            backgroundImage = backImageSRC;
                        }
                        else
                        {
                            backgroundImage = "../images/home_slider_bg.jpg";
                        }


                        if (url.StartsWith("http://"))
                        {
                            target = "target=\"_blank\"";
                        }

                        ///Used the display the Image and Text in Left or Right position based on the selection
                        int displayFlag = 0;
                        if (contentXML.SelectSingleNode("/root/Text-Image-Display/TextImageDisp") != null)
                        {
                            xml = contentXML.SelectSingleNode("/root/Text-Image-Display/TextImageDisp").InnerText;
                            if (!String.IsNullOrWhiteSpace(xml))
                                displayFlag = Convert.ToInt16(xml);
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
                        counter++;
                        DTSource.Rows.Add(index, text, imageSRC, backgroundImage, url, target, counter, styleimage,
                            stylecaption, subtitle);
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