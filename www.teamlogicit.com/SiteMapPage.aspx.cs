using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using TeamLogic.CMS;

public partial class SiteMap : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            this.BuildSiteMap();
    }

    private void BuildSiteMap()
    {
        string urlPath = "/";
        if(FransDataManager.IsFranchiseSelected())
        {
            string centerId = FransDataManager.GetFranchiseId();
            if(!string.IsNullOrEmpty(centerId))
            {
                urlPath = "/" + centerId + "/";
            }
        }


        StringBuilder sbHtml = new StringBuilder();
        sbHtml.Append("<ul class=\"sitemap\">");
        sbHtml.Append("<li><a class=\"SiteMapLink\" href=\"/\">Home</a></li>");
        sbHtml.Append("<li><a href=\"/managed-it-services/\">Managed IT Services</a></li>");
        sbHtml.Append("<li><a href=\"/it-solutions/\">IT Solutions</a></li>");
        sbHtml.Append("<li><a href=\"/consulting-projects/\">Consulting & Projects</a></li>");
        sbHtml.Append("<li><a href=\"/briefs-whitepapers/\"  class=\"insights-link\">Resources</a>");
            sbHtml.Append("<ul>");
            sbHtml.Append("<li><a href=\"/briefs-whitepapers/\">All Briefs & WhitePapers</a></li>");
            sbHtml.Append("<li><a href=\"/case-studies/\">All Case Studies</a></li>");
            sbHtml.Append("</ul>");
            sbHtml.Append("</li>");
            sbHtml.Append("<li>");
            sbHtml.Append("<a class=\"SiteMapLink\" href=\"" + urlPath + "company-info/\">About</a>");
            sbHtml.Append("<ul>");
            sbHtml.Append("<li><a href=\"/company-info/\">About Us</a></li>");
                if (!FransDataManager.IsFranchiseSelected())
                {
                    sbHtml.Append("<li><a href=\"/management-team/\">Management Team</a></li>");
                    sbHtml.Append("<li><a href=\"/partners/\">Partners</a></li>");
                }
                if (FransDataManager.IsFranchiseSelected())
                    sbHtml.Append("<li><a class=\"SiteMapLink\" href=\"" + urlPath + "testimonials/\">Testimonials</a></li>");
                sbHtml.Append("<li><a class=\"SiteMapLink\" href=\"" + urlPath + "news/\">NEWS</a></li>");
                sbHtml.Append("<li><a href=\"/in-the-media/\">In The Media</a></li>");
                sbHtml.Append("<li><a href=\"/careers/\">Careers</a></li>");
                if (FransDataManager.IsFranchiseSelected())
                    sbHtml.Append("<li><a class=\"SiteMapLink\" href=\"" + urlPath + "contact-us/\">Contact Us</a></li>");
            sbHtml.Append("</ul>");
            sbHtml.Append("</li>");
            sbHtml.Append("<li><a target=\"_blank\"  href=\"http://www.itinflections.com/\">ITInflections</a></li>");
        sbHtml.Append("</ul>");

        ltrSiteMap.Text = sbHtml.ToString();

    }
}