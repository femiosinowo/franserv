using Ektron.Cms.Instrumentation;
using System;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using TeamLogic.CMS;


public partial class UserControls_FooterNav : System.Web.UI.UserControl
{

    /// <summary>
    /// Page init event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Init(object sender, EventArgs e)
    {
        cbCopyRight.DefaultContentID = ConfigHelper.GetValueLong("SiteCopyRightContentId");
        cbCopyRight.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbCopyRight.Fill();        
    }

    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (FransDataManager.IsFranchiseSelected())
        {
            long localFooterNavId = ConfigHelper.GetValueLong("FooterNavLocalId");
            this.LoadFooterMenu(localFooterNavId);
            subscribeFooter.Visible = true;
        }
        else
        {
            long nationalFooterNavId = ConfigHelper.GetValueLong("FooterNavNationalId");
            this.LoadFooterMenu(nationalFooterNavId);
            findLocationFooter.Visible = true;
        }

        if (!IsPostBack)
        {
            this.GetTwitterUrl();
            this.GetCopyRightText();
        }
    }

    private void LoadFooterMenu(long footerMenuId)
    {
        try
        {

            footerMenu.DefaultMenuID = footerMenuId;
            footerMenu.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
            footerMenu.Fill();

            if (footerMenu.XmlDoc != null)
            {
                string menuXml = footerMenu.XmlDoc.InnerXml;
                XDocument xDoc = XDocument.Parse(menuXml);

                var navItems = xDoc.XPathSelectElements("MenuDataResult/Item/Item");
                if (navItems != null)
                {
                    StringBuilder sbMenu = new StringBuilder();
                    int count = 0;
                    sbMenu.Append("<div class=\"footer_col footer_col_1\">");
                    foreach (var item in navItems)
                    {
                        if (count >= 0 && count < 3)
                        {
                            sbMenu.Append(GetMenuDate(item));
                            count++;
                        }
                    }
                    sbMenu.Append("</div>");
                    count = 0;
                    sbMenu.Append("<div class=\"footer_col footer_col_2\">");
                    foreach (var item in navItems)
                    {
                        if (count >= 3)
                        {
                            sbMenu.Append(GetMenuDate(item));
                        }
                        count++;
                    }
                    sbMenu.Append("</div>");
                    ltrMenuData.Text = sbMenu.ToString();
                }
            }
        }
        catch(Exception ex)
        {
            Log.WriteError(ex);
        }
    }

    private string GetMenuDate(XElement item)
    {
        StringBuilder sb = new StringBuilder();
        string url = "";
        try
        {
            sb.Append("<ul>");
            if (item.XPathSelectElement("Menu/Link") != null)
                url = item.XPathSelectElement("Menu/Link").Value;
            if (item.XPathSelectElement("Menu/Title") != null)
                sb.Append("<li><a href=\"" + url + "\">" + item.XPathSelectElement("Menu/Title").Value + "</a></li>");

            string description = "";
            if (item.XPathSelectElement("Menu/Description") != null)
                description = item.XPathSelectElement("Menu/Description").Value;

            if (item.XPathSelectElement("Menu/Item") != null)
            {
                url = "";
                var subNavItems = item.XPathSelectElements("Menu/Item");
                foreach (var subItem in subNavItems)
                {
                    if (subItem.XPathSelectElement("Menu/Link") != null)
                        url = subItem.XPathSelectElement("Menu/Link").Value;
                    sb.Append("<li><a href=\"" + url + "\">" + subItem.XPathSelectElement("Menu/Title").Value + "</a></li>");
                }
            }
            else if (description == "ITSolutions")
            {
                sb.Append(this.GetITSolutionContent());
            }
            else if (description == "ConsultingProjects")
            {
                //sb.Append(this.GetConsultingProjectsContent());
            }
            sb.Append("</ul>");
        }
        catch (Exception ex)
        {
            Log.WriteError(ex);
            return "";
        }

        return sb.ToString();
    }

    private string GetITSolutionContent()
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            var contentList = SiteDataManager.GetCompleteSolutionsContent();
            if (contentList != null && contentList.Count > 0)
            {
                foreach (var c in contentList)
                {
                    sb.Append("<li><a href=\"" + c.Quicklink + "\">" + c.Title + "</a></li>");
                }
            }
        }
        catch (Exception ex)
        {
            Log.WriteError(ex);
        }
        return sb.ToString();
    }

    private string GetConsultingProjectsContent()
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            var contentList = SiteDataManager.GetProjectExpertsContent();
            if (contentList != null && contentList.Count > 0)
            {
                foreach (var c in contentList)
                {
                    sb.Append("<li><a href=\"/consulting-projects-detail/?sid=" + c.Id + "\">" + c.Title + "</a></li>");
                }
            }
        }
        catch (Exception ex)
        {
            Log.WriteError(ex);
        }
        return sb.ToString();
    }

    /// <summary>
    /// this method is used to get twitter url
    /// </summary>
    private void GetTwitterUrl()
    {
        if (FransDataManager.IsFranchiseSelected())
        {
            string fransTwitterUrl = FransDataManager.GetFransTwitterUrl();
            if (!string.IsNullOrEmpty(fransTwitterUrl))
            {
                twitterWidget.HRef = fransTwitterUrl;
            }
        }
    }


    private void GetCopyRightText()
    {
        if(cbCopyRight != null && cbCopyRight.EkItem != null && cbCopyRight.EkItem.Html != "")
        {
            try
            {
                XDocument xDoc = XDocument.Parse(cbCopyRight.EkItem.Html);
                StringBuilder sb = new StringBuilder();

                if (xDoc.XPathSelectElement("/root/CopyrightText") != null)
                    sb.Append("<li><div class=\"CopyRightText\">" + TLITUtility.ExtractNodeHtml(xDoc.XPathSelectElement("/root/CopyrightText")) + "</div></li>");

                var links = xDoc.XPathSelectElements("/root/Links");
                foreach (var link in links)
                {
                    if (link.XPathSelectElement("a") != null)
                    {
                        string className = link.XPathSelectElement("a").Attribute("class") != null ? link.XPathSelectElement("a").Attribute("class").Value : "";
                        string url = link.XPathSelectElement("a").Attribute("href").Value;
                        if (className == "SiteMapLink" && FransDataManager.IsFranchiseSelected())
                        {
                            url = "/" + FransDataManager.GetFranchiseId() + url;
                        }

                        sb.Append("<li><a class=\"" + className + "\" href=\"" + url + "\">" + link.XPathSelectElement("a").Value + "</a></li>");
                    }
                }

                ltrCopyRight.Text = sb.ToString();
            }
            catch(Exception ex)
            {
                Log.WriteError(ex);
            }
        }
        
    }
}