using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using SirSpeedy.CMS;

public partial class SiteMap : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.BodyClass += " footer-sitemap ";

        cbHeader.DefaultContentID = ConfigHelper.GetValueLong("FooterContentHeaderCId");
        cbHeader.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbHeader.Fill();

        cbSiteMapSideContent.DefaultContentID = ConfigHelper.GetValueLong("SiteMapSideCId");
        cbSiteMapSideContent.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbSiteMapSideContent.Fill();

        if (!Page.IsPostBack)
            this.BuildSiteMap();
    }

    private void BuildSiteMap()
    {
        string urlPath = "/";
        bool isFranchiseSelected = false;
        if(FransDataManager.IsFranchiseSelected())
        {
            string centerId = FransDataManager.GetFranchiseId();
            if(!string.IsNullOrEmpty(centerId))
            {
                isFranchiseSelected = true;
                //urlPath = "/" + centerId + "/";
            }
        }  

        StringBuilder sbHtml = new StringBuilder();
        sbHtml.Append("<ul class=\"sitemap\">");
        sbHtml.Append("<li><a class=\"SiteMapLink\" href=\"/\">Home</a></li>");
        sbHtml.Append("<li><a href=\"/product-services/\">Products & Services</a></li>");        
        sbHtml.Append("<li><a href=\"/briefs-whitepapers/\"  class=\"insights-link\">Insights</a>");
            sbHtml.Append("<ul>");
            sbHtml.Append("<li><a href=\"/briefs-whitepapers/\">All Briefs & WhitePapers</a></li>");
            sbHtml.Append("<li><a href=\"/case-studies/\">All Case Studies</a></li>");
            sbHtml.Append("</ul>");
            sbHtml.Append("</li>");
            sbHtml.Append("<li>");
            if (isFranchiseSelected)
              sbHtml.Append("<a class=\"SiteMapLink\" href=\"" + urlPath + "company-info/\">About</a>");
            else
                sbHtml.Append("<a class=\"SiteMapLink\" href=\"/company-info/\">About</a>");
            sbHtml.Append("<ul>");

            if (isFranchiseSelected)
                sbHtml.Append("<li><a href=\"" + urlPath + "company-info/\">Company Info</a></li>");
            else
                sbHtml.Append("<li><a href=\"/company-info/\">Company Info</a></li>");
            
                if (!isFranchiseSelected)
                {
                    sbHtml.Append("<li><a href=\"/company-info/management-team/\">Management Team</a></li>");
                    sbHtml.Append("<li><a href=\"/company-info/partners/\">Partners</a></li>");
                }
                if (isFranchiseSelected)
				{
                    sbHtml.Append("<li><a class=\"SiteMapLink\" href=\"" + urlPath + "testimonials/\">Testimonials</a></li>");
					sbHtml.Append("<li><a class=\"SiteMapLink\" href=\"" + urlPath + "why-we-are-different/\">Why We are Different</a></li>");
			    }
                if(isFranchiseSelected)
                   sbHtml.Append("<li><a class=\"SiteMapLink\" href=\"" + urlPath + "news/\">News</a></li>");
                else
                    sbHtml.Append("<li><a class=\"SiteMapLink\" href=\"/company-info/news/\">News</a></li>");
                if (isFranchiseSelected)
                    sbHtml.Append("<li><a href=\"" + urlPath + "in-the-media/\">In The Media</a></li>");
                else
                    sbHtml.Append("<li><a href=\"/company-info/in-the-media/\">In The Media</a></li>");
                sbHtml.Append("<li><a href=\"/job-search/\">Careers</a></li>");
                if (isFranchiseSelected)
                    sbHtml.Append("<li><a class=\"SiteMapLink\" href=\"" + urlPath + "contact-us/\">Contact Us</a></li>");
            sbHtml.Append("</ul>");
            sbHtml.Append("</li>");
            sbHtml.Append("<li><a target=\"_blank\"  href=\"http://www.marketingtango.com/\">Marketing Tango</a></li>");
        sbHtml.Append("</ul>");

        ltrSiteMap.Text = sbHtml.ToString();

    }
}