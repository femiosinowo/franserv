using System;
using System.Web.UI;
using System.Data;
using System.Xml;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Ektron.Cms;
using Ektron.Cms.Framework.Content;
using Figleaf;
using SirSpeedy.CMS;
using Ektron.Cms.Content;
using Ektron.Cms.Instrumentation;
using System.Web;
using sun.misc;
using Ektron.Cms.Organization;


public partial class UserControls_MainNav : UserControl
{
    /// <summary>
    /// page init event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    string _fransId = "0";

    protected void Page_Init(object sender, EventArgs e)
    {
        cbLogo.DefaultContentID = ConfigHelper.GetValueLong("SiteLogoContent");
        cbLogo.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbLogo.Fill();

        cbWhyWorkWithUs.DefaultContentID = ConfigHelper.GetValueLong("MegaMenuWhyWorkWithUs");
        cbWhyWorkWithUs.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbWhyWorkWithUs.Fill();

        cbAmazingPeople.DefaultContentID = ConfigHelper.GetValueLong("MegaMenuAmazingPeopleText");
        cbAmazingPeople.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbAmazingPeople.Fill();

        //cbFransJobImage.DefaultContentID = ConfigHelper.GetValueLong("MegaMenuLocalJobImage");
        //cbFransJobImage.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        //cbFransJobImage.Fill();

        cbAboutMarketingTangoImg.DefaultContentID = ConfigHelper.GetValueLong("MegaMenuMarketingTangoImageCId");
        cbAboutMarketingTangoImg.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbAboutMarketingTangoImg.Fill();

        cbAboutMarketingTango.DefaultContentID = ConfigHelper.GetValueLong("MarketingTangoContentId");
        cbAboutMarketingTango.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbAboutMarketingTango.Fill();

        cbHistoryText.DefaultContentID = ConfigHelper.GetValueLong("MegaMenuHistoryTextCId");
        cbHistoryText.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbHistoryText.Fill();

        if (cbAmazingPeople.EkItem != null)
            ltrAmazingPeople.Text = cbAmazingPeople.EkItem.Html;
        
    }
    
    /// <summary>
    /// page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        _fransId = FransDataManager.IsFranchiseSelected() ? FransDataManager.GetFranchiseId() : "0";
        if (!IsPostBack)
        {
            DataTable DTPSMMSource = GetPSMMContent();
            UxProductAndServicesMM.DataSource = DTPSMMSource;
            UxProductAndServicesMM.DataBind();

            DataTable DTPSMMSideSource = GetPSMMSideContent();
            UxPSMMSide.DataSource = DTPSMMSideSource;
            UxPSMMSide.DataBind();            

            if (FransDataManager.IsFranchiseSelected())
            {
                whyWeAreDiff.Visible = true;
                managementSection.Visible = false;
                this.FillWhyDiffContent();

                //about us
                DataTable DTCompanyInfoMMSource = GetCompanyInfoContent();
                UxAboutUs.DataSource = DTCompanyInfoMMSource;
                UxAboutUs.DataBind();
                aboutSirSpeedy.Visible = true;
                companyInfo.Visible = false;
                nationalPartners.Visible = false;
                nationalAboutUsImg.Visible = true;
                NationalAboutUsImage();

                DataTable DTManagementTeamMMSource = GetManagementTeamContentForLocal();
                UxOurTeam.DataSource = DTManagementTeamMMSource;
                UxOurTeam.DataBind();
                ourTeamContent.Visible = true;
                history.Visible = false;

                this.GetRecentLocalCenterJobs();
                this.GetLocalAdminPhotos();
                flickrPortfolio.Visible = true;
                plnLocalJobs.Visible = true;
                plnNationalJobs.Visible = false;

                var fransThirdPartyData = FransDataManager.GetFransThirdPartyData();
                if (fransThirdPartyData != null &&
                    fransThirdPartyData.SocialMediaData != null &&
                    !string.IsNullOrEmpty(fransThirdPartyData.SocialMediaData.MarketingTangoUrl))
                {
                    aboutMarketingTangoURL.HRef = visitTheBlogURL.HRef = recentPostsURL.HRef = fransThirdPartyData.SocialMediaData.MarketingTangoUrl;
                }
            }
            else
            {
                DataTable DTManagementTeamMMSource = GetManagementTeamContent();
                UxManagementTeamMM.DataSource = DTManagementTeamMMSource;
                UxManagementTeamMM.DataBind();

                //company info
                DataTable DTCompanyInfoMMSource = GetCompanyInfoContent();
                UxCompanyInfoMM.DataSource = DTCompanyInfoMMSource;
                UxCompanyInfoMM.DataBind();
                companyInfo.Visible = true;
                aboutSirSpeedy.Visible = false;

                this.GetRecentJobs();
                //this.GetPartners();
                plnNationalJobs.Visible = true;
                plnLocalJobs.Visible = false;
                newsLink.HRef = "/company-info/news/";
                inTheMediaLink.HRef = "/company-info/in-the-media/";
            }           

            DataTable DTNewsMMSource = GetNewsContent();
            UxNewsMM.DataSource = DTNewsMMSource;
            UxNewsMM.DataBind();

            DataTable DTPartnersMMSource = GetPartnersContent();
            UxPartnersMM.DataSource = DTPartnersMMSource;
            UxPartnersMM.DataBind();

            DataTable DTInTheMediaMMSource = GetInTheMediaContent();
            UxInTheMediaMM.DataSource = DTInTheMediaMMSource;
            UxInTheMediaMM.DataBind();

            // insights 
            DataTable DTBriefsWHitepaperMMSource = GetBriefsWHitepaperContent();
            UxBriefsWHitepapersMM.DataSource = DTBriefsWHitepaperMMSource;
            UxBriefsWHitepapersMM.DataBind();

            DataTable DTBigCaseStudieMMSource = GetCaseStudiesContent(1);
            UxBigCaseStudiesMM.DataSource = DTBigCaseStudieMMSource;
            UxBigCaseStudiesMM.DataBind();

            DataTable DTSmallCaseStudieMMSource = GetCaseStudiesContent(2);
            UxSmallCaseStudiesMM.DataSource = DTSmallCaseStudieMMSource;
            UxSmallCaseStudiesMM.DataBind();

            // join our team 
            DataTable DTJoinOurTeamMMSource = GetJoinOurTeamContent();
            UxJoinOurTeamMM.DataSource = DTJoinOurTeamMMSource;
            UxJoinOurTeamMM.DataBind();

            // blog
            var blogsData = BlogsDataManager.GetRssFeed(3);
            lvRecentBlogs.DataSource = blogsData;
            lvRecentBlogs.DataBind();

            this.GetMenuFirstLevel();
            this.GetProductsAndServices();
            this.GetSecondaryNav();            
        }
    }

    private DataTable GetPSMMContent()
    {
        string cacheKey = String.Format("Sirspeedy:UserControls_MainNav:GetPSMMContent:FranchiseId={0}",
            _fransId);

        DataTable sortedDT = CacheBase.Get<DataTable>(cacheKey);

        if (sortedDT == default(DataTable) || sortedDT.Rows.Count == 0)
        {
            DataTable DTSource = new DataTable();
            // DataTable sortedDT = new DataTable();        
            XmlDocument contentXML = new XmlDocument();
            int counter = 0;

            DTSource.Columns.Add("title");
            DTSource.Columns.Add("subtitle");
            DTSource.Columns.Add("imageSRC");
            DTSource.Columns.Add("url");
            DTSource.Columns.Add("content");
            DTSource.Columns.Add("teaser");
            DTSource.Columns.Add("tagline");
            DTSource.Columns.Add("hrefText");
            DTSource.Columns.Add("counter");

            var contents = M3TDataManager.GetProductAndServices();
            if (contents != null && contents.Count > 0)
            {
                foreach (Ektron.Cms.ContentData contentData in contents)
                {
                    try
                    {
                        contentXML.LoadXml(contentData.Html);
                        XmlNodeList xnList = contentXML.SelectNodes("/root");
                        string title = xnList[0]["title"].InnerXml;
                        string subtitle = xnList[0]["subtitle"].InnerXml;
                        string content = xnList[0]["content"].InnerXml;
                        string teaser = xnList[0]["teaser"].InnerXml;
                        string tagline = xnList[0]["tagline"].InnerXml;

                        string hrefText = contentData.Quicklink;

                        string xml = (contentXML.SelectSingleNode("/root/imageSlider").InnerXml == null)
                            ? String.Empty
                            : contentXML.SelectSingleNode("/root/imageSlider").InnerXml;
                        string imgSRC = String.Empty;
                        if (!String.IsNullOrEmpty(xml))
                        {
                            imgSRC =
                                Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1]
                                    .Value;
                        }

                        xml = (contentXML.SelectSingleNode("/root/url").InnerXml == null)
                            ? String.Empty
                            : contentXML.SelectSingleNode("/root/url").InnerXml;
                        string url = "#";
                        if (!String.IsNullOrEmpty(xml))
                        {
                            url =
                                Regex.Match(xml, "<a.+?href=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1]
                                    .Value;
                        }
                        counter++;

                        DTSource.Rows.Add(title, subtitle, imgSRC, url, content, teaser, tagline, hrefText, counter);
                    }
                    catch (Exception ex)
                    {
                        Log.WriteError(ex);
                    }
                }
                DataView DVPSMMSort = DTSource.DefaultView;
                //DVPSMMSort.Sort = "counter asc";
                sortedDT = DVPSMMSort.ToTable();
                CacheBase.Put(cacheKey,sortedDT, CacheDuration.For24Hr);
            }
        }
        return sortedDT;
    }

    private DataTable GetPSMMSideContent()
    {
        string cacheKey = String.Format("Sirspeedy:UserControls_MainNav:GetPSMMSideContent:FranchiseId={0}",
            _fransId);

        DataTable sortedDT = CacheBase.Get<DataTable>(cacheKey);

        if (sortedDT == default(DataTable) || sortedDT.Rows.Count == 0)
        {
            DataTable DTSource = new DataTable();
          //  DataTable sortedDT = new DataTable();
            XmlDocument contentXML = new XmlDocument();
            int counter = 0;

            DTSource.Columns.Add("title");
            DTSource.Columns.Add("imageSRC");
            DTSource.Columns.Add("tagline");
            DTSource.Columns.Add("url");
            DTSource.Columns.Add("counter");

            var contents = M3TDataManager.GetProductAndServicesSideContent();
            if (contents != null && contents.Count > 0)
            {
                foreach (Ektron.Cms.ContentData contentData in contents)
                {
                    try
                    {
                        contentXML.LoadXml(contentData.Html);

                        XmlNodeList xnList = contentXML.SelectNodes("/root");
                        string title = xnList[0]["title"].InnerXml;
                        string tagline = xnList[0]["tagline"].InnerXml;

                        string xml = (contentXML.SelectSingleNode("/root/image").InnerXml == null)
                            ? String.Empty
                            : contentXML.SelectSingleNode("/root/image").InnerXml;
                        string imgSRC = String.Empty;
                        if (!String.IsNullOrEmpty(xml))
                        {
                            imgSRC =
                                Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1]
                                    .Value;
                        }

                        string url =
                            Regex.Match(xml, "<a.+?href=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                            //not sure about this
                        counter++;
                        DTSource.Rows.Add(title, imgSRC, tagline, url, counter);
                    }
                    catch (Exception ex)
                    {
                        Log.WriteError(ex);
                    }
                }
                DataView DVPSMMSort = DTSource.DefaultView;
                DVPSMMSort.Sort = "counter desc";
                sortedDT = DVPSMMSort.ToTable();
                CacheBase.Put(cacheKey, sortedDT, CacheDuration.For24Hr);
            }
        }
        return sortedDT;
    }

    private DataTable GetCompanyInfoContent()
    {
       
        string cacheKey = String.Format("Sirspeedy:UserControls_MainNav:GetCompanyInfoContent:FranchiseId={0}",
            _fransId);

        DataTable sortedDT = CacheBase.Get<DataTable>(cacheKey);

        if (sortedDT == default(DataTable) || sortedDT.Rows.Count == 0)
        {
            DataTable DTSource = new DataTable();
            // DataTable sortedDT = new DataTable();        
            XmlDocument contentXML = new XmlDocument();
            int counter = 0;

            DTSource.Columns.Add("title");
            DTSource.Columns.Add("teaser");
            DTSource.Columns.Add("content");
            DTSource.Columns.Add("tagline");
            DTSource.Columns.Add("url");
            DTSource.Columns.Add("counter");

            var contents = M3TDataManager.GetCompanyInfoContent();
            if (contents != null && contents.Count > 0)
            {
                foreach (Ektron.Cms.ContentData contentData in contents)
                {
                    try
                    {
                        contentXML.LoadXml(contentData.Html);
                        XmlNodeList xnList = contentXML.SelectNodes("/root");

                        string title = xnList[0]["title"].InnerXml;
                        string teaser = xnList[0]["teaser"].InnerXml;
                        string content = xnList[0]["content"].InnerXml;
                        string tagline = xnList[0]["tagline"].InnerXml;

                        string xml = (contentXML.SelectSingleNode("/root/url").InnerXml == null)
                            ? String.Empty
                            : contentXML.SelectSingleNode("/root/url").InnerXml;
                        string url = "#";
                        if (!String.IsNullOrEmpty(xml))
                        {
                            url =
                                Regex.Match(xml, "<a.+?href=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1]
                                    .Value;
                        }

                        string hrefText = "/company-info/";

                        counter++;

                        DTSource.Rows.Add(title, teaser, content, tagline, url, counter);
                    }
                    catch (Exception ex)
                    {
                        Log.WriteError(ex);
                    }
                }
                DataView DVPSMMSort = DTSource.DefaultView;
                DVPSMMSort.Sort = "counter desc";
                sortedDT = DVPSMMSort.ToTable();
                CacheBase.Put(cacheKey, sortedDT, CacheDuration.For24Hr);
            }
        }
        return sortedDT;
    }

    private string NationalAboutUsImage()
    {
        string imagePath = string.Empty;
        var contents = M3TDataManager.GetCompanyInfoContent();
        if(contents != null && contents.Any())
        {
            XmlDocument contentXML = new XmlDocument();    
            foreach (Ektron.Cms.ContentData contentData in contents)
            {
                try
                {
                    contentXML.LoadXml(contentData.Html);
                    XmlNodeList xnList = contentXML.SelectNodes("/root");
                    if (xnList[0]["contentImage"] != null)
                    {
                        var xml = xnList[0]["contentImage"].InnerXml;
                        var imgSRC = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                        aboutUsImg.Text = "<img src=\"" + imgSRC + "\" class=\"why-work-stretch-pic\" alt=\"\" />";
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }
        }
        return imagePath;
    }

    private DataTable GetManagementTeamContent()
    {
       
        string cacheKey = String.Format("Sirspeedy:UserControls_MainNav:GetManagementTeamContent:FranchiseId={0}",
            _fransId);

        DataTable sortedDT = CacheBase.Get<DataTable>(cacheKey);

        if (sortedDT == default(DataTable) || sortedDT.Rows.Count == 0)
        {
            DataTable DTSource = new DataTable();
            //  DataTable sortedDT = new DataTable();
            XmlDocument contentXML = new XmlDocument();
            int counter = 0;

            DTSource.Columns.Add("firstName");
            DTSource.Columns.Add("lastName");
            DTSource.Columns.Add("jobTitle");
            DTSource.Columns.Add("gender");
            DTSource.Columns.Add("imageSRC");
            DTSource.Columns.Add("isMegamenuItem");
            DTSource.Columns.Add("counter");

            var contents = M3TDataManager.GetManagementTeamContent();
            if (contents != null && contents.Count > 0)
            {
                foreach (Ektron.Cms.ContentData contentData in contents)
                {
                    try
                    {
                        contentXML.LoadXml(contentData.Html);

                        XmlNodeList xnList = contentXML.SelectNodes("/root");
                        string firstName = xnList[0]["firstName"].InnerXml;
                        string lastName = xnList[0]["lastName"].InnerXml;
                        string jobTitle = xnList[0]["jobTitle"].InnerXml;
                        string gender = xnList[0]["gender"].InnerXml;
                        string isMegamenuItem = xnList[0]["isMegamenuItem"].InnerXml;
                        string xml = String.Empty;
                        string imgSRC = String.Empty;
                        if (contentXML.SelectSingleNode("/root/mediumImage") != null)
                        {
                            xml = contentXML.SelectSingleNode("/root/mediumImage").InnerXml;
                            imgSRC =
                                Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1]
                                    .Value;
                        }

                        if (imgSRC == string.Empty)
                        {
                            if (gender == "M")
                                imgSRC = "/images/photos/our-team-placeholder-male.png";
                            else
                                imgSRC = "/images/photos/our-team-placeholder-female.png";
                        }

                        counter++;

                        //if (isMegamenuItem.Equals("true"))
                        //{
                        //    DTSource.Rows.Add(firstName, lastName, jobTitle, gender, imgSRC, counter);
                        //}
                        DTSource.Rows.Add(firstName, lastName, jobTitle, gender, imgSRC, counter);
                    }
                    catch (Exception ex)
                    {
                        Log.WriteError(ex);
                    }
                }
                DataView DVPSMMSort = DTSource.DefaultView;
                //DVPSMMSort.Sort = "firstName asc";
                sortedDT = DataviewHelper.SelectTopDataRow(DVPSMMSort.ToTable(), 3);
                CacheBase.Put(cacheKey, sortedDT, CacheDuration.For24Hr);
            }
        }
        return sortedDT;
    }

    private void FillWhyDiffContent()
    {
        var fransId = FransDataManager.GetFranchiseId();
        var whyDiffContent = FransDataManager.GetWhyWeAreDiffContent(fransId);
        if(whyDiffContent != null)
        {
            ltrWhyWeAreDiff.Text = whyDiffContent.ContentTagLine;
        }
    }

    private DataTable GetManagementTeamContentForLocal()
    {
       
        string cacheKey = String.Format("Sirspeedy:UserControls_MainNav:GetManagementTeamContentForLocal:FranchiseId={0}",
            _fransId);

        DataTable sortedDT = CacheBase.Get<DataTable>(cacheKey);

        if (sortedDT == default(DataTable) || sortedDT.Rows.Count == 0)
        {
            DataTable DTSource = new DataTable();
            // DataTable sortedDT = new DataTable();
            XmlDocument contentXML = new XmlDocument();
            int counter = 0;

            DTSource.Columns.Add("firstName");
            DTSource.Columns.Add("lastName");
            DTSource.Columns.Add("jobTitle");
            DTSource.Columns.Add("gender");
            DTSource.Columns.Add("imageSRC");
            DTSource.Columns.Add("isMegamenuItem");
            DTSource.Columns.Add("counter");

            var m3TData = FransDataManager.GetFransM3TData();
            if (m3TData != null && m3TData.OurTeamEmployeeIds != null)
            {
                foreach (var eId in m3TData.OurTeamEmployeeIds)
                {
                    try
                    {
                        var eData = GetEmployeeById(eId);
                        if (eData != null)
                        {
                            string firstName = eData.FirstName;
                            string lastName = eData.LastName;
                            string jobTitle = eData.Roles;
                            string gender = eData.Gender;
                            string imgSRC = eData.PicturePath;

                            if (string.IsNullOrEmpty(imgSRC))
                            {
                                if (gender == "Male")
                                    imgSRC = "/images/photos/our-team-placeholder-male.png";
                                else
                                    imgSRC = "/images/photos/our-team-placeholder-female.png";
                            }

                            counter++;
                            DTSource.Rows.Add(firstName, lastName, jobTitle, gender, imgSRC, counter);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.WriteError(ex);
                    }
                }
                DataView DVPSMMSort = DTSource.DefaultView;
                //DVPSMMSort.Sort = "firstName asc";
                sortedDT = DataviewHelper.SelectTopDataRow(DVPSMMSort.ToTable(), 3);
                CacheBase.Put(cacheKey, sortedDT, CacheDuration.For24Hr);
            }
        }
        return sortedDT;
    }

    private Employee GetEmployeeById(long employeeId)
    {
        Employee e = null;
        if (employeeId > 0)
        {
            return FransDataManager.GetEmployeeById(employeeId);
        }
        return e;
    }

    private DataTable GetNewsContent()
    {
       
        string cacheKey = String.Format("Sirspeedy:UserControls_MainNav:GetNewsContent:FranchiseId={0}",
            _fransId);

        DataTable sortedDT = CacheBase.Get<DataTable>(cacheKey);

        if (sortedDT == default(DataTable) || sortedDT.Rows.Count == 0)
        {

            DataTable DTSource = new DataTable();
            //  DataTable sortedDT = new DataTable();
            XmlDocument contentXML = new XmlDocument();
            int counter = 0;

            DTSource.Columns.Add("title");
            DTSource.Columns.Add("rawDate");
            DTSource.Columns.Add("date");
            DTSource.Columns.Add("city");
            DTSource.Columns.Add("state");
            DTSource.Columns.Add("content");
            DTSource.Columns.Add("imgSRC");
            DTSource.Columns.Add("url");
            DTSource.Columns.Add("hrefText");
            DTSource.Columns.Add("counter");

            var contents = M3TDataManager.GetNews();
            if (contents != null && contents.Count > 0)
            {
                foreach (Ektron.Cms.ContentData contentData in contents)
                {
                    try
                    {
                        contentXML.LoadXml(contentData.Html);
                        XmlNodeList xnList = contentXML.SelectNodes("/root");
                        string title = String.IsNullOrEmpty(xnList[0]["title"].InnerXml)
                            ? String.Empty
                            : xnList[0]["title"].InnerXml;
                        //if (title.Length >= 50)
                        //{
                        //    title += "...";
                        //}
                        if (title.Length > 50)
                        {
                            title = title.Substring(0, 50) + "...";
                        }
                        string titleReformatted = HtmlRemoval.StripTagsRegexCompiled(title);
                        string date = String.IsNullOrEmpty(xnList[0]["date"].InnerXml)
                            ? String.Empty
                            : xnList[0]["date"].InnerXml;
                        string city = String.IsNullOrEmpty(xnList[0]["city"].InnerXml)
                            ? String.Empty
                            : xnList[0]["city"].InnerXml;
                        string state = String.IsNullOrEmpty(xnList[0]["state"].InnerXml)
                            ? String.Empty
                            : xnList[0]["state"].InnerXml;
                        string content = String.IsNullOrEmpty(xnList[0]["content"].InnerXml)
                            ? String.Empty
                            : xnList[0]["content"].InnerXml;

                        DateTime tempdate = Convert.ToDateTime(date);
                        string newFormattedDate = tempdate.ToString("MMMM dd, yyyy");

                        string xml = (contentXML.SelectSingleNode("/root/image").InnerXml == null)
                            ? String.Empty
                            : contentXML.SelectSingleNode("/root/image").InnerXml;
                        string imgSRC = String.Empty;
                        if (!String.IsNullOrEmpty(xml))
                        {
                            imgSRC =
                                Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1]
                                    .Value;
                        }

                        xml = (contentXML.SelectSingleNode("/root/url").InnerXml == null)
                            ? String.Empty
                            : contentXML.SelectSingleNode("/root/url").InnerXml;
                        string url = "#";
                        if (!String.IsNullOrEmpty(xml))
                        {
                            url =
                                Regex.Match(xml, "<a.+?href=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1]
                                    .Value;
                        }

                        string hrefText = contentData.Quicklink;

                        counter++;

                        DTSource.Rows.Add(titleReformatted, date, newFormattedDate, city, state, content, imgSRC, url,
                            hrefText, counter);
                    }
                    catch (Exception ex)
                    {
                        Log.WriteError(ex);
                    }
                }
                DataView DVPSMMSort = DTSource.DefaultView;
                DVPSMMSort.Sort = "rawDate desc";
                sortedDT = DataviewHelper.SelectTopDataRow(DVPSMMSort.ToTable(), 2);
                CacheBase.Put(cacheKey, sortedDT,CacheDuration.For24Hr);
            }
        }
        return sortedDT;
        
    }

    private DataTable GetPartnersContent()
    {
       
        string cacheKey = String.Format("Sirspeedy:UserControls_MainNav:GetPartnersContent:FranchiseId={0}",
            _fransId);
       
        DataTable sortedDT = CacheBase.Get<DataTable>(cacheKey);

        if (sortedDT == default(DataTable) || sortedDT.Rows.Count == 0)
        {
            DataTable DTSource = new DataTable();
            //DataTable sortedDT = new DataTable();
            XmlDocument contentXML = new XmlDocument();
            int counter = 0;

            DTSource.Columns.Add("imgSRC");
            DTSource.Columns.Add("alt");
            DTSource.Columns.Add("url");
            DTSource.Columns.Add("dateCreated");
            DTSource.Columns.Add("counter");

            var contents = M3TDataManager.GetPartners();
            if (contents != null && contents.Count > 0)
            {
                foreach (Ektron.Cms.ContentData contentData in contents)
                {
                    try
                    {
                        contentXML.LoadXml(contentData.Html);
                        XmlNodeList xnList = contentXML.SelectNodes("/root");

                        string xml = (contentXML.SelectSingleNode("/root/image").InnerXml == null)
                            ? String.Empty
                            : contentXML.SelectSingleNode("/root/image").InnerXml;
                        string imgSRC = String.Empty;
                        string alt = String.Empty;
                        if (!String.IsNullOrEmpty(xml))
                        {
                            imgSRC =
                                Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1]
                                    .Value;
                            alt =
                                Regex.Match(xml, "<img.+?alt=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1]
                                    .Value;
                        }

                        xml = (contentXML.SelectSingleNode("/root/url").InnerXml == null)
                            ? String.Empty
                            : contentXML.SelectSingleNode("/root/url").InnerXml;
                        string url = "#";
                        if (!String.IsNullOrEmpty(xml))
                        {
                            url =
                                Regex.Match(xml, "<a.+?href=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1]
                                    .Value;
                        }

                        string dateCreated = contentData.DateCreated.ToString();

                        counter++;

                        DTSource.Rows.Add(imgSRC, alt, url, dateCreated, counter);
                    }
                    catch (Exception ex)
                    {
                        Log.WriteError(ex);
                    }
                }
                DataView DVPSMMSort = DTSource.DefaultView;
                //to get 6 instances to displayed on the MegaMenu
                sortedDT = DataviewHelper.SelectTopDataRow(DVPSMMSort.ToTable(), 6);

                CacheBase.Put(cacheKey, sortedDT, CacheDuration.For24Hr);
            }
        }
        return sortedDT;
    }

    private DataTable GetInTheMediaContent()
    {
       
        string cacheKey = String.Format("Sirspeedy:UserControls_MainNav:GetInTheMediaContent:FranchiseId={0}",
            _fransId);

        DataTable sortedDT = CacheBase.Get<DataTable>(cacheKey);

        if (sortedDT == default(DataTable) || sortedDT.Rows.Count == 0)
        {
            DataTable DTSource = new DataTable();
            //DataTable sortedDT = new DataTable();
            XmlDocument contentXML = new XmlDocument();
            int counter = 0;

            DTSource.Columns.Add("title");
            DTSource.Columns.Add("source");
            DTSource.Columns.Add("rawDate");
            DTSource.Columns.Add("date");
            DTSource.Columns.Add("teaser");
            DTSource.Columns.Add("imgSRC");
            DTSource.Columns.Add("url");
            DTSource.Columns.Add("counter");

            var contents = M3TDataManager.GetInTheMediaContent();
            if (contents != null && contents.Count > 0)
            {
                foreach (Ektron.Cms.ContentData contentData in contents)
                {
                    try
                    {
                        contentXML.LoadXml(contentData.Html);

                        XmlNodeList xnList = contentXML.SelectNodes("/root");
                        string title = xnList[0]["title"].InnerXml;
                        string source = "";
                        if (xnList[0]["sourceName"] != null)
                            source = xnList[0]["sourceName"].InnerXml;

                        string date = "";
                        if (xnList[0]["date"] != null)
                            date = xnList[0]["date"].InnerXml;

                        string teaser = "";
                        if (xnList[0]["teaser"] != null)
                            teaser = xnList[0]["teaser"].InnerXml;

                        string xml = (contentXML.SelectSingleNode("/root/image").InnerXml == null)
                            ? String.Empty
                            : contentXML.SelectSingleNode("/root/image").InnerXml;
                        string imgSRC = String.Empty;
                        if (!String.IsNullOrEmpty(xml))
                        {
                            imgSRC =
                                Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1]
                                    .Value;
                        }

                        xml = (contentXML.SelectSingleNode("/root/url").InnerXml == null)
                            ? String.Empty
                            : contentXML.SelectSingleNode("/root/url").InnerXml;
                        string url = "#";
                        if (!String.IsNullOrEmpty(xml))
                        {
                            url =
                                Regex.Match(xml, "<a.+?href=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1]
                                    .Value;
                        }

                        DateTime tempdate = Convert.ToDateTime(date);
                        string newFormattedDate = tempdate.ToString("MMMM dd, yyyy");

                        counter++;

                        DTSource.Rows.Add(title, source, date, newFormattedDate, teaser, imgSRC, url, counter);
                    }
                    catch (Exception ex)
                    {
                        Log.WriteError(ex);
                    }
                }
                DataView DVPSMMSort = DTSource.DefaultView;
                DVPSMMSort.Sort = "rawDate desc";
                sortedDT = DataviewHelper.SelectTopDataRow(DVPSMMSort.ToTable(), 2);
                CacheBase.Put(cacheKey, sortedDT, CacheDuration.For1Hr);
            }
        }
        return sortedDT;
    }

    private DataTable GetBriefsWHitepaperContent()
    {
       
        string cacheKey = String.Format("Sirspeedy:UserControls_MainNav:GetBriefsWHitepaperContent:FranchiseId={0}",
            _fransId);

        DataTable sortedDT = CacheBase.Get<DataTable>(cacheKey);

        if (sortedDT == default(DataTable) || sortedDT.Rows.Count == 0)
        {
            DataTable DTSource = new DataTable();
            // DataTable sortedDT = new DataTable();
            XmlDocument contentXML = new XmlDocument();
            int counter = 0;

            DTSource.Columns.Add("title");
            DTSource.Columns.Add("teaserMM");
            DTSource.Columns.Add("abstract");
            DTSource.Columns.Add("content");
            DTSource.Columns.Add("url");
            DTSource.Columns.Add("hrefText");
            DTSource.Columns.Add("imgSRC");
            DTSource.Columns.Add("dateCreated");
            DTSource.Columns.Add("counter");

            var contents = M3TDataManager.GetBriefsAndWhitePapersContent();
            if (contents != null && contents.Count > 0)
            {
                foreach (Ektron.Cms.ContentData contentData in contents)
                {
                    try
                    {
                        contentXML.LoadXml(contentData.Html);

                        XmlNodeList xnList = contentXML.SelectNodes("/root");
                        string title = xnList[0]["title"].InnerXml;
                        string teaserMM = xnList[0]["teaserMM"].InnerXml;
                        string abstractText = xnList[0]["abstract"].InnerXml.Substring(0, 50);
                        string content = xnList[0]["content"].InnerXml;

                        string xml = (contentXML.SelectSingleNode("/root/image").InnerXml == null)
                            ? String.Empty
                            : contentXML.SelectSingleNode("/root/image").InnerXml;
                        string imgSRC = String.Empty;
                        if (!String.IsNullOrEmpty(xml))
                        {
                            imgSRC =
                                Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1]
                                    .Value;
                        }

                        xml = (contentXML.SelectSingleNode("/root/url").InnerXml == null)
                            ? String.Empty
                            : contentXML.SelectSingleNode("/root/url").InnerXml;
                        string url = "#";
                        if (!String.IsNullOrEmpty(xml))
                        {
                            url =
                                Regex.Match(xml, "<a.+?href=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1]
                                    .Value;
                        }

                        string dateCreated = contentData.DateCreated.ToString();
                        string hrefText = contentData.Quicklink;
                        counter++;

                        DTSource.Rows.Add(title, teaserMM, abstractText, content, url, hrefText, imgSRC, dateCreated,
                            counter);
                    }
                    catch (Exception ex)
                    {
                        Log.WriteError(ex);
                    }
                }
                DataView DVPSMMSort = DTSource.DefaultView;
                DVPSMMSort.Sort = "dateCreated desc";
                sortedDT = DataviewHelper.SelectTopDataRow(DVPSMMSort.ToTable(), 3);
                CacheBase.Put(cacheKey, sortedDT,CacheDuration.For24Hr);
            }
        }
        return sortedDT;
    }

    private DataTable GetCaseStudiesContent(int count)
    {

        string cacheKey = String.Format("Sirspeedy:UserControls_MainNav:GetCaseStudiesContent:FranchiseId={0}:Count={1}",
            _fransId, count);

        DataTable sortedDT = CacheBase.Get<DataTable>(cacheKey);

        if (sortedDT == default(DataTable) || sortedDT.Rows.Count == 0)
        {
            DataTable DTSource = new DataTable();
            // DataTable sortedDT = new DataTable();
            XmlDocument contentXML = new XmlDocument();
            int counter = 0;

            DTSource.Columns.Add("title");
            DTSource.Columns.Add("desc");
            DTSource.Columns.Add("hrefText");
            DTSource.Columns.Add("imgSRC");
            DTSource.Columns.Add("dateCreated");
            DTSource.Columns.Add("counter");

            var contents = M3TDataManager.GetCaseStudiesContent();
            if (contents != null && contents.Count > 0)
            {
                foreach (Ektron.Cms.ContentData contentData in contents)
                {
                    try
                    {
                        contentXML.LoadXml(contentData.Html);

                        XmlNodeList xnList = contentXML.SelectNodes("/root");
                        string title = xnList[0]["title"].InnerXml;
                        string desc = xnList[0]["desc"].InnerXml;
                        string isBig = xnList[0]["isBig"].InnerXml;

                        string xml = (contentXML.SelectSingleNode("/root/image").InnerXml == null)
                            ? String.Empty
                            : contentXML.SelectSingleNode("/root/image").InnerXml;
                        string imgSRC = String.Empty;
                        if (!String.IsNullOrEmpty(xml))
                        {
                            imgSRC =
                                Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1]
                                    .Value;
                        }

                        string hrefText = contentData.Quicklink;

                        string dateCreated = contentData.DateCreated.ToString();
                        counter++;

                        if (count == 1 && isBig.Equals("true"))
                        {
                            DTSource.Rows.Add(title, desc, hrefText, imgSRC, dateCreated, counter);
                        }
                        else if (count == 2 && isBig.Equals("false"))
                        {
                            DTSource.Rows.Add(title, desc, hrefText, imgSRC, dateCreated, counter);
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
                sortedDT = DataviewHelper.SelectTopDataRow(sortedDT, count);
                CacheBase.Put(cacheKey,sortedDT, CacheDuration.For24Hr);
            }
        }
        return sortedDT;
    }

    private DataTable GetJoinOurTeamContent()
    {
       
        string cacheKey = String.Format("Sirspeedy:UserControls_MainNav:GetJoinOurTeamContent:FranchiseId={0}",
            _fransId);

        DataTable sortedDT = CacheBase.Get<DataTable>(cacheKey);

        if (sortedDT == default(DataTable) || sortedDT.Rows.Count == 0)
        {
            DataTable DTSource = new DataTable();
            // DataTable sortedDT = new DataTable();
            XmlDocument contentXML = new XmlDocument();
            int counter = 0;

            DTSource.Columns.Add("name");
            DTSource.Columns.Add("teaserMM");
            //DTSource.Columns.Add("imgSRCMM");
            DTSource.Columns.Add("tagline");
            DTSource.Columns.Add("abstract");
            DTSource.Columns.Add("desc");
            DTSource.Columns.Add("url");
            DTSource.Columns.Add("imgSRC");
            DTSource.Columns.Add("hrefText");
            DTSource.Columns.Add("counter");

            var contents = M3TDataManager.GetJobProfiles();
            if (contents != null && contents.Count > 0)
            {
                foreach (Ektron.Cms.ContentData contentData in contents)
                {
                    try
                    {
                        contentXML.LoadXml(contentData.Html);

                        string hrefText = contentData.Quicklink;

                        XmlNodeList xnList = contentXML.SelectNodes("/root");
                        string name = TaxonomyHelper.GetTaxonomyNameByContentId(contentData.Id);
                        string teaserMM = xnList[0]["teaserMM"].InnerXml;
                        string tagline = xnList[0]["tagline"].InnerXml;
                        string abstractText = xnList[0]["abstract"].InnerXml;
                        string desc = xnList[0]["desc"].InnerXml;

                        //string xml = contentXML.SelectSingleNode("/root/imgSRCMM").InnerXml;
                        //string imgSRCMM = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;

                        string xml = (contentXML.SelectSingleNode("/root/image").InnerXml == null)
                            ? String.Empty
                            : contentXML.SelectSingleNode("/root/image").InnerXml;
                        string imgSRC = String.Empty;
                        if (!String.IsNullOrEmpty(xml))
                        {
                            imgSRC =
                                Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1]
                                    .Value;
                        }

                        xml = contentXML.SelectSingleNode("/root/url").InnerXml;
                        string url = "#";
                        if (!string.IsNullOrEmpty(name))
                        {
                            var fransId = FransDataManager.GetFranchiseId();
                            if (!string.IsNullOrEmpty(fransId))
                                url = "/" + fransId + "/job-profiles/?type=" + name.ToLower().Replace(" ", "");
                            else
                                url = "/job-profiles/?type=" + name.ToLower().Replace(" ", "");
                            hrefText = url;
                        }
                        counter++;

                        DTSource.Rows.Add(name, teaserMM, tagline, abstractText, desc, url, imgSRC, hrefText, counter);
                    }
                    catch (Exception ex)
                    {
                        Log.WriteError(ex);
                    }
                }
                DataView DVPSMMSort = DTSource.DefaultView;
                DVPSMMSort.Sort = "counter desc";
                sortedDT = DataviewHelper.SelectTopDataRow(DVPSMMSort.ToTable(), 3);
                CacheBase.Put(cacheKey,sortedDT,CacheDuration.For24Hr);
            }
        }
        return sortedDT;
    }
    
    private void GetMenuFirstLevel()
    {
        long mainNavId = ConfigHelper.GetValueLong("SiteMainNavId");
        var mainNavData = GetMenuData(mainNavId);
        if (mainNavData != null && mainNavData.Items.Count > 0)
        {
            //really don't think of any way to get this menu text dynamically
            foreach (var item in mainNavData.Items)
            {
                if (item.Description.Trim().ToLower().IndexOf("products-services") > -1)
                    ltrProductsServices.Text = ltrMblProductServices.Text = ltrMblSubProductServices.Text = item.Text;
                else if (item.Description.Trim().ToLower().IndexOf("portfolio") > -1)
                    ltrPortfolio.Text = item.Text;
                else if (item.Description.Trim().ToLower().IndexOf("about-us") > -1)
                    ltrAboutUs.Text = item.Text;
                else if (item.Description.Trim().ToLower().IndexOf("insights") > -1)
                    ltrInSights.Text = item.Text;
                else if (item.Description.Trim().ToLower().IndexOf("join-our-team") > -1)
                    ltrJoinOurTeam.Text = item.Text;
                else if (item.Description.Trim().ToLower().IndexOf("blog") > -1)
                    ltrBlog.Text = item.Text;
            }
        }
    }

    /// <summary>
    /// This method is used to display the available product & services as mini mobile nav
    /// </summary>
    private void GetProductsAndServices()
    {
        var productServicesData = SiteDataManager.GetProductAndServices();//SiteDataManager.GetProductServicesTaxList();
        if (productServicesData != null && productServicesData.Count > 0)
        {
            listViewProductsServices.DataSource = GetPSMMContent();//productServicesData;
            listViewProductsServices.DataBind();
        }
    }

    private void GetSecondaryNav()
    {
        long secondaryMenuId = M3TDataManager.GetSecondaryNavId();
        var secondaryMenuData = GetMenuData(secondaryMenuId);
        if (secondaryMenuData != null && secondaryMenuData.Items.Count > 0)
        {
            listViewSecondaryNav.DataSource = secondaryMenuData.Items;
            listViewSecondaryNav.DataBind();
        }
    }
    
    private void GetLocalAdminPhotos()
    {
        var photoLists = SiteDataManager.GetFlickrPhotoSetData();
        lvPortfolio.DataSource = photoLists;
        lvPortfolio.DataBind();        
    }
    
    private void GetRecentJobs()
    {
        var jobList = FransDataManager.GetAllJobsForNational();
        if(jobList != null && jobList.Any())
        {
            var filteredList = jobList.OrderBy(x => x.DatePosted).Reverse().Take(3);
            lvJobsList.DataSource = filteredList;
            lvJobsList.DataBind();
        }
    }

    private void GetRecentLocalCenterJobs()
    {
        var jobList = FransDataManager.GetAllJobsForLocal();
        if(jobList != null && jobList.Any())
        {
            var filteredList = jobList.OrderBy(x => x.DatePosted).Reverse().Take(3).ToList();
            if (filteredList != null && filteredList.Any())
            {
                var fransId = FransDataManager.GetFranchiseId();
                var updatedJobPostList = from j in filteredList
                                         select new
                                         {
                                             Title= j.Title,
                                             DatePosted = j.DatePosted,
                                             Location=j.Location,
                                             Link = "/" + fransId + "/job-description/?jobid=" + j.JobId
                                         };
                lvJobsListLocal.DataSource = updatedJobPostList;
                lvJobsListLocal.DataBind();                
            }            
        }
    }
    
    public string FormatUrl(object setId)
    {
        string url = "/portfolio/";
        if (setId != null)
        {
            url = "/portfolio/?setid=" + setId;
            if (FransDataManager.IsFranchiseSelected())
            {
                string fransId = FransDataManager.GetFranchiseId();
                url = "/" + fransId + url;
            }
        }
        return url;
    }

    public string FormatMobileMenuLink(string url)
    {
        string formattedLink = url;
        if (!url.StartsWith("/"))
            formattedLink = "/" + url;
        return formattedLink;
    }

    private MenuData GetMenuData(long navId)
    {
        MenuData data = null;
        string cacheKey = String.Format("Sirspeedy:UserControls_MainNav:GetMenuData:FranchiseId={0}{1}",
            _fransId, navId);

        data = CacheBase.Get<MenuData>(cacheKey);
        if (data == null && navId > 0)
        {
            data = MenuHelper.GetMenuTree(navId);
            CacheBase.Put(cacheKey, data, CacheDuration.For24Hr);
        }
        return data;
    }

}