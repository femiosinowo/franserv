﻿using Ektron.Cms;
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
using System.Xml.XPath;
using System.Xml.Linq;
using TeamLogic.CMS;

public partial class outsourced_IT : PageBase
{    
    protected void Page_Init(object sender, EventArgs e)
    {
        cbHowWeWork.DefaultContentID = ConfigHelper.GetValueLong("ManageITHowWeWorkCId");
        cbHowWeWork.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbHowWeWork.Fill();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.GetSupplementOutsourcingContent();
            this.GetITSourceContent();
            this.LoadWhatWeOfferContent();
            this.GetTestimonials();

            DataTable dtManagedItServiesContent = this.GetManagedItServiesContent();

            if (dtManagedItServiesContent != null && dtManagedItServiesContent.Rows.Count > 0)
            {
                int itemsPerColumn = dtManagedItServiesContent.Rows.Count / 2;
                lvManagedItServicesIcons.GroupItemCount = itemsPerColumn;
            }

            lvManagedItServicesIcons.DataSource = dtManagedItServiesContent;
            lvManagedItServicesIcons.DataBind();

            lvManagedItServicesDetails.DataSource = dtManagedItServiesContent;
            lvManagedItServicesDetails.DataBind();
        }
    }

    private void GetITSourceContent()
    {
        long itSourcesCId = ConfigHelper.GetValueLong("ManageITChildOutsourced");
        var cData = ContentHelper.GetContentById(itSourcesCId);
        if (cData != null && cData.Html != string.Empty)
        {
            try
            {
                XDocument xDoc = XDocument.Parse(cData.Html);
                ltrSectionTitle.Text = "<div class=\"sectionTitle\">" + TLITUtility.ExtractNodeHtml(xDoc.XPathSelectElement("/root/sectionTitle")) + "</div>";
                var xelements = xDoc.XPathSelectElements("/root/Content");
                if (xelements != null && xelements.Any())
                {
                    var itSolutionItems = from obj in xelements
                                          select new
                                          {
                                              Title = obj.XPathSelectElement("title") != null ? obj.XPathSelectElement("title").Value : string.Empty,
                                              SectionId = obj.XPathSelectElement("title") != null ? obj.XPathSelectElement("title").Value.Replace(" ","") : string.Empty,
                                              SubTitle = obj.XPathSelectElement("description") != null ? obj.XPathSelectElement("description").ToString() : string.Empty,
                                              ImagePath = obj.XPathSelectElement("image/img") != null ? obj.XPathSelectElement("image/img").Attribute("src").Value : string.Empty,
                                              Link = obj.XPathSelectElement("link/a") != null ? obj.XPathSelectElement("link/a").Attribute("href").Value : "#"
                                          };

                    lvOurServices.DataSource = itSolutionItems;
                    lvOurServices.DataBind();
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
        }
    }

    private DataTable GetManagedItServiesContent()
    {
        DataTable DTSource = new DataTable();
        DataTable sortedDT = new DataTable();
        XmlDocument contentXML = new XmlDocument();
        DTSource.Columns.Add("id");
        DTSource.Columns.Add("titletxt");
        DTSource.Columns.Add("titlehtml");
        DTSource.Columns.Add("subtitle");
        DTSource.Columns.Add("imgSRC");
        DTSource.Columns.Add("bgimgSRC");
        DTSource.Columns.Add("desc");       

        var contents = SiteDataManager.GetManagedItSerivesContent();
        if (contents != null && contents.Count > 0)
        {
            foreach (Ektron.Cms.ContentData contentData in contents)
            {
                try
                {                    
                    contentXML.LoadXml(contentData.Html);
                    string contentId = contentData.Id.ToString();
                    string hreftext = "#";//"/managed_itservices_detail/?id=" + contentData.Id;
                    XmlNodeList xnList = contentXML.SelectNodes("/root");
                    string title = Regex.Replace(xnList[0]["title"].InnerXml.Replace("<br />", " "), "<.*?>", string.Empty);
                    string titlehtml = xnList[0]["title"].InnerXml;
                    string subtitle = xnList[0]["subTitle"].InnerXml;
                    string xml = contentXML.SelectSingleNode("/root/image").InnerXml;                   
                    string imgSRC = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                    string bgimgSRC = string.Empty;
                    if (contentData.Image != string.Empty && contentData.Image.ToLower().IndexOf("workarea") <= -1)
                    {
                        bgimgSRC = "/" + contentData.Image;
                        //bgimgSRC = Regex.Match(bgImageXML, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                    }
                    string desc = xnList[0]["description"].InnerXml;
                    DTSource.Rows.Add(contentId, title, titlehtml, subtitle, imgSRC, bgimgSRC, desc);
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }
        }
        return DTSource;
    }

    private void LoadWhatWeOfferContent()
    {
        try
        {
            long whatweOfferCid = ConfigHelper.GetValueLong("ManageITServicesLandingCId");
            ContentData contentData = ContentHelper.GetContentById(whatweOfferCid);
            XmlDocument contentXML = new XmlDocument();
            contentXML.LoadXml(contentData.Html);
            XmlNodeList xnList = contentXML.SelectNodes("/root");
            title.Text = xnList[0]["title"].InnerXml;
            litDesc1.Text = xnList[0]["descpSection1"].InnerXml;
            litDesc2.Text = xnList[0]["descpSection2"].InnerXml;
            string videoPath = xnList[0]["video"].InnerText;
            video.Text = "<iframe src=\"" + videoPath + "\" width=\"100%\" height=\"533\" frameborder=\"0\" webkitallowfullscreen=\"\" mozallowfullscreen=\"\" allowfullscreen=\"\"></iframe>";
        }
        catch(Exception ex)
        {
            Log.WriteError(ex);
        }
    }
    
    private void GetTestimonials()
    {
        var allTestimonials = SiteDataManager.GetTestimonials();
        if (allTestimonials != null && allTestimonials.Any())
        {
            var selectedTestimonals = allTestimonials.Where(x => x.IsManageIT == true).Take(4);
            lvTesimonials1.DataSource = selectedTestimonals;
            lvTesimonials1.DataBind();

            var selectedTestimonals2 = allTestimonials.Where(x => x.IsManageIT == true).Skip(4).Take(4);
            lvTesimonials2.DataSource = selectedTestimonals2;
            lvTesimonials2.DataBind();
        }
    }

    private void GetSupplementOutsourcingContent()
    {
        long supplementCId = ConfigHelper.GetValueLong("ManageITChildNeedSupplementsAndITOutSourcing");
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

    public string FormatLastName(string firstName, string lastName)
    {
        string updatedLastName = "";
        if (lastName != null)
        {
            if (firstName != "" && lastName != "")
                updatedLastName = lastName + ", ";
            else if (firstName != "" && lastName == "")
                updatedLastName = ", ";
            else if (firstName == "" && lastName == "")
                updatedLastName = "";
        }
        return updatedLastName;
    }

}