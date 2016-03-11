using Ektron.Cms.Instrumentation;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Xml;
using TeamLogic.CMS;

public partial class faqs_detail : PageBase
{
    long contentIdQueryString = 0;
    public string LetsConnectBackgroundImage = "/images/lets_connect_bg.jpg";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["sid"]))
        {
            contentIdQueryString = Convert.ToInt64(Request.QueryString["sid"]);
        }

        if (!Page.IsPostBack)
        {
            lvFaqs.DataSource = this.GetFAQs();
            lvFaqs.DataBind();
        }


        cbLetsConnect.DefaultContentID = ConfigHelper.GetValueLong("FaqIndexLetsConnectCId");
        cbLetsConnect.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbLetsConnect.Fill();

        if (cbLetsConnect.EkItem != null && !string.IsNullOrEmpty(cbLetsConnect.EkItem.Image) && cbLetsConnect.EkItem.Image.ToLower().IndexOf("workarea") <= -1)
            LetsConnectBackgroundImage = cbLetsConnect.EkItem.Image;

        lets_connect.Attributes.Add("data-image", LetsConnectBackgroundImage);
        lets_connect.Attributes.Add("data-image-mobile", LetsConnectBackgroundImage);
        if (FransDataManager.IsFranchiseSelected())
        {
            var fransData = FransDataManager.GetFransData();
            if (fransData != null)
            {
                lblPhoneNumber.Text = fransData.PhoneNumber;
            }
        }
    }
    
    private DataTable GetFAQs()
    {
        DataTable DTSource = new DataTable();
        string cssClass = string.Empty;
        string cssClear = string.Empty;
        XmlDocument contentXML = new XmlDocument();

        DTSource.Columns.Add("contentId");
        DTSource.Columns.Add("title");
        DTSource.Columns.Add("shortdesc");
        DTSource.Columns.Add("image");       
        DTSource.Columns.Add("question");
        DTSource.Columns.Add("answer");
        DTSource.Columns.Add("description");

        var contents = SiteDataManager.GetFaqs();
        if (contents != null && contents.Count > 0)
        {
            foreach (Ektron.Cms.ContentData contentData in contents)
            {
                try
                {
                    contentXML.LoadXml(contentData.Html);
                    XmlNodeList xnList = contentXML.SelectNodes("/root");
                    string contentId = contentData.Id.ToString();
                    string title = xnList[0]["title"].InnerXml;
                    string shortdes = xnList[0]["shortDescription"].InnerXml;
                    string imagexml = contentXML.SelectSingleNode("/root/image").InnerXml;
                    string image = Regex.Match(imagexml, "<a.+?href=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                    string description = xnList[0]["description"].InnerXml;
                    string question = xnList[0]["question"].InnerXml;
                    string answer = xnList[0]["answer"].InnerXml;

                    DTSource.Rows.Add(contentId, title, shortdes, image, question, answer, description);
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }

            }
        }

        DataRow[] dr = DTSource.Select("contentId ='" + contentIdQueryString + "'");
        DataRow newRow = DTSource.NewRow();
        newRow.ItemArray = dr[0].ItemArray;
        DTSource.Rows.Remove(dr[0]);
        DTSource.Rows.InsertAt(newRow, 0);

        return DTSource;
    }
}