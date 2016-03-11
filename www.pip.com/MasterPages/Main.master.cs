using System;
using System.Linq;
using SirSpeedy.CMS;
using Ektron.Cms.Instrumentation;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Collections.Generic;
using System.Text;

public partial class Main : MasterBase
{
    public string BodyClass { get; set; }

     /// <summary>
    /// page Init event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            long contentId = 0;
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                long.TryParse(Request.QueryString["id"], out contentId);
            if (!string.IsNullOrEmpty(Request.QueryString["pageid"]))
                long.TryParse(Request.QueryString["pageid"], out contentId);
            else if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                long.TryParse(Request.QueryString["id"], out contentId);

            if (contentId == 0 && Request.RawUrl.Equals("/"))
                contentId = ConfigHelper.GetValueLong("NationalDefaultPageId");

            //uxMetaDataTitles.DefaultContentID = contentId;
            //uxMetaDataTitles.Fill();
            this.GetSEOSiteInfomration(contentId);

            string reqCenterId = FransDataManager.CreateActiveFransSession();
            hddnCenterId.Value = reqCenterId;            
        }
        catch (Exception ex)
        {
            Log.WriteError(ex);
            Log.WriteError(ex.StackTrace);
        }
    }
    
    /// <summary>
    /// page Load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (FransDataManager.IsFranchiseSelected())
            BodyClass += BodyClass + " local";
        else
            BodyClass += BodyClass + " national";

        this.addBodyClass(BodyClass);
    }

    private void addBodyClass(string className)
    {
        //get current body class
        string currentClass = siteContainer.Attributes["class"];
        //if the class name is populated AND there is NO existing body classes
        if (className != null && !string.IsNullOrEmpty(className) && string.IsNullOrEmpty(currentClass))
        {
            siteContainer.Attributes.Add("class", className);
        }
        //if the class name is populated AND there is existing body classes
        else if (className != null && !string.IsNullOrEmpty(className) && !string.IsNullOrEmpty(currentClass))
        {
            //split the new classes 
            string[] newClasses = className.Split(' ');
            //attempt to split the current classes
            string[] currentClasses = (!string.IsNullOrEmpty(currentClass)) ? siteContainer.Attributes["class"].Split(' ') : null;
            //get the unique classes (we don't want to add classes that already exist)
            List<string> uniqueClasses = newClasses.Where(x => !currentClasses.Any(y => y.ToLower() == x.ToLower()) && x != string.Empty).ToList();
            //create a string builder to house the final additions
            StringBuilder newClass = new StringBuilder();
            //add new classes to the string builder
            foreach (string item in uniqueClasses)
            {
                newClass.Append(" ");
                newClass.Append(item);
            }
            siteContainer.Attributes.Add("class", currentClass + newClass.ToString());
        }
    }

    private void GetSEOSiteInfomration(long contentId)
    {
        try
        {
            HtmlMeta meta = new HtmlMeta();
            string seoBrandName = "";
            string cityName = "";
            string stateName = "";
            var nationalData = SiteDataManager.GetNationalCompanyInfo();
            if (nationalData != null)
            {
                seoBrandName = nationalData.SEOMetaTitleBrandName;
                cityName = nationalData.City;
                stateName = nationalData.State;
            }

            if (FransDataManager.IsFranchiseSelected())
            {
                var fransData = FransDataManager.GetFransData();
                if (fransData != null)
                {
                    cityName = fransData.City;
                    stateName = fransData.State;

                    //hiding the default site from google searches
                    if (fransData.FransId.ToLower() == "missionviejoca002")
                    {
                        meta = new HtmlMeta();
                        meta.Name = "robots";
                        meta.Content = "noindex";
                        Page.Header.Controls.AddAt(0, meta);
                    }
                }
            }
            string domainName = Request.ServerVariables["SERVER_NAME"];
            if (!string.IsNullOrEmpty(domainName))
            {
                domainName = domainName.ToLower();
                //hiding the author & internal site pages from google
                if (domainName.Contains("new.pip.com") || domainName.Contains("author.pip.com"))
                {
                    meta = new HtmlMeta();
                    meta.Name = "robots";
                    meta.Content = "noindex";
                    Page.Header.Controls.AddAt(0, meta);
                }
            }

            var cntData = ContentHelper.GetContentById(contentId, true);
            if (cntData != null && cntData.MetaData != null && cntData.MetaData.Count() > 0)
            {
                var dataList = cntData.MetaData.ToList();

                //checking for keyword meta tag 
                var keywordMetaData = dataList.SingleOrDefault(x => x.Id == ConfigHelper.GetValueLong("keywordsMetaId"));
                if (keywordMetaData != null && keywordMetaData.Text != null && keywordMetaData.Text.ToString() != "")
                {
                    meta = new HtmlMeta();
                    meta.Name = "Keywords";
                    meta.Content = keywordMetaData.Text.Replace(";", ",");
                    this.Page.Header.Controls.AddAt(0, meta);
                }

                //checking for description meta tag 
                var descritptionMetaData = dataList.SingleOrDefault(x => x.Id == ConfigHelper.GetValueLong("DescriptionMetaId"));
                if (descritptionMetaData != null && descritptionMetaData.Text != null && descritptionMetaData.Text.ToString() != "")
                {
                    string metaDescription = descritptionMetaData.Text;
                    string formattedDesc = metaDescription;
                    formattedDesc = formattedDesc.Replace("{Brand}", seoBrandName.Trim());
                    formattedDesc = formattedDesc.Replace("{ST}", stateName.Trim());
                    formattedDesc = formattedDesc.Replace("{City}", cityName.Trim());
                    meta = new HtmlMeta();
                    meta.Name = "Description";
                    meta.Content = formattedDesc;
                    this.Page.Header.Controls.AddAt(0, meta);
                }

                //checking for Title meta tag 
                var titleMetaData = dataList.SingleOrDefault(x => x.Id == ConfigHelper.GetValueLong("TitleMetaId"));
                if (titleMetaData != null && titleMetaData.Text != null && titleMetaData.Text.ToString() != "")
                {
                    string metaTitle = titleMetaData.Text;
                    string formattedTitle = metaTitle;
                    formattedTitle = formattedTitle.Replace("{Brand}", seoBrandName.Trim());
                    formattedTitle = formattedTitle.Replace("{ST}", stateName.Trim());
                    formattedTitle = formattedTitle.Replace("{City}", cityName.Trim());
                    Page.Header.Controls.AddAt(0, new LiteralControl("<title>" + formattedTitle + "</title>"));
                }
            }
        }
        catch (Exception ex)
        {
            Log.WriteError(ex);
        }
    }
}
