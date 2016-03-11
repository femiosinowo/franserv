using Ektron.Cms;
using Ektron.Cms.Instrumentation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using TeamLogic.CMS;

public partial class faqs : PageBase
{
    private int defaultCount = 10;
    public string LetsConnectBackgroundImage = "/images/lets_connect_bg.jpg";

    protected void Page_Load(object sender, EventArgs e)
    {
        cbLetsConnect.DefaultContentID = ConfigHelper.GetValueLong("FaqIndexLetsConnectCId");
        cbLetsConnect.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbLetsConnect.Fill();

        if (cbLetsConnect.EkItem != null && !string.IsNullOrEmpty(cbLetsConnect.EkItem.Image) && cbLetsConnect.EkItem.Image.ToLower().IndexOf("workarea") <= -1)
            LetsConnectBackgroundImage = cbLetsConnect.EkItem.Image;

        lets_connect.Attributes.Add("data-image", LetsConnectBackgroundImage);
        lets_connect.Attributes.Add("data-image-mobile", LetsConnectBackgroundImage);

        if(FransDataManager.IsFranchiseSelected())
        {
            var fransData = FransDataManager.GetFransData();
            if(fransData != null)
            {
                lblPhoneNumber.Text = fransData.PhoneNumber;
            }
        }

        if (!Page.IsPostBack)
        {
            lvfaqCommonQuestions.DataSource = this.GetFAQCommonQuestions();
            lvfaqCommonQuestions.DataBind();

            lvfaqs.DataSource = this.GetFAQs();
            lvfaqs.DataBind();
        }
    }
    
    protected void LoadMoreLinkButton_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lvfaqCommonQuestions.DataSource = this.GetFAQCommonQuestions();
            lvfaqCommonQuestions.DataBind();

            lvfaqs.DataSource = this.GetFAQs();
            lvfaqs.DataBind();
        }
    }
    
    private DataTable GetFAQs()
    {
        DataTable DTSource = new DataTable();
        string cssClass = string.Empty;
        string cssClear = string.Empty;
        XmlDocument contentXML = new XmlDocument();
        int counter = 1;
       
        DTSource.Columns.Add("title");
        DTSource.Columns.Add("shortdesc");        
        DTSource.Columns.Add("image");        
        DTSource.Columns.Add("url");
        DTSource.Columns.Add("cssClass");
        DTSource.Columns.Add("cssClear");

        var contents = SiteDataManager.GetFaqs();
        if (contents != null && contents.Count > 0)
        {
            if (contents.Count < defaultCount)
                loadMoreNews.Visible = false;

            foreach (Ektron.Cms.ContentData contentData in contents)
            {
                try
                {
                    contentXML.LoadXml(contentData.Html);
                    XmlNodeList xnList = contentXML.SelectNodes("/root");
                    string contentId = contentData.Id.ToString();
                    string title = xnList[0]["title"].InnerXml;
                    string shortdes = xnList[0]["shortDescription"].InnerXml;
                    string image = "";
                    if (contentXML.SelectSingleNode("/root/image/img") != null)
                    {
                        image = contentXML.SelectSingleNode("/root/image/img").Attributes["src"].Value;
                        //string image = Regex.Match(imagexml, "<a.+?href=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                    }
                    string hreftext = "/faqs-detail/?sid=" + contentData.Id;
                    cssClass = "";
                    cssClear = "";
                    if (counter == 1)
                        cssClass = "alpha";
                    if (counter == 3)
                    {
                        counter = 0;
                        cssClass = "omega";
                        cssClear = "<div class=\"clear\"></div>";
                    }
                    counter++;
                    DTSource.Rows.Add(title, shortdes, image, hreftext, cssClass, cssClear);
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }
        }
        return DTSource;
    }

    private DataTable GetFAQCommonQuestions()
    {
        long faqCommonQuestionContent = ConfigHelper.GetValueInt("FAQCommonQuestions");
        DataTable DTSource = new DataTable();
        if (faqCommonQuestionContent > 0)
        {
            int counter = 0;
            XmlDocument contentXML = new XmlDocument();
            ContentData contentData = ContentHelper.GetContentById(faqCommonQuestionContent);
            if (contentData != null && contentData.Html != "")
            {
                string question = string.Empty;
                string answer = string.Empty;

                DTSource.Columns.Add("question");
                DTSource.Columns.Add("answer");
                DTSource.Columns.Add("counter");

                try
                {
                    contentXML.LoadXml(contentData.Html);
                    XmlNodeList xnList = contentXML.SelectNodes("/root/FaqQuestion");
                    counter++;

                    foreach (XmlNode node in xnList)
                    {
                        question = node.SelectSingleNode("faqQuestionField").InnerXml;
                        answer = node.SelectSingleNode("faqAnswerField").InnerXml;
                        DTSource.Rows.Add(question, answer, counter);
                    }
                }
                catch(Exception ex)
                {
                    Log.WriteError(ex);
                }
            }
        }
        return DTSource;
    }
}