using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using Ektron.Cms.Framework.Content;
using Ektron.Cms;
using TeamLogic.CMS;
using System.Text.RegularExpressions;
using Ektron.Cms.Instrumentation;
using System.Text;

public partial class _management_team : PageBase
{
    public string JoinTeamBackgroundImage = "/images/join_team_bg.jpg";

    protected void Page_Init(object sender, EventArgs e)
    {
        cbJoinTeam.DefaultContentID = ConfigHelper.GetValueLong("ManagementTeamJoinOurTeamCId");
        cbJoinTeam.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbJoinTeam.Fill();

        if (cbJoinTeam.EkItem != null && !string.IsNullOrEmpty(cbJoinTeam.EkItem.Image) && cbJoinTeam.EkItem.Image.ToLower().IndexOf("workarea") <= -1)
            JoinTeamBackgroundImage = cbJoinTeam.EkItem.Image;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {            
            GetOurTeamInfo();
        }
        join_team_img.Attributes.Add("data-image", JoinTeamBackgroundImage);
        join_team_img.Attributes.Add("data-image-mobile", JoinTeamBackgroundImage);
    }

    private void GetOurTeamInfo()
    {
        var contents = SiteDataManager.GetManagementTeam();
        if (contents != null && contents.Count > 0)
        {
            try
            {
                int count = 1;
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i <= contents.Count / 3; i++)
                {
                    sb.Append("<div id=\"row_" + count + "\" class=\"container_24 mgmt_profile_row\">");
                    sb.Append("<div class=\"grid_24 mgmt_profile_wrapper\">");
                    var filteredEmployeeList = contents.Skip(i * 3).Take(3).ToList();

                    for (int k = 0; k < filteredEmployeeList.Count(); k++)
                    {
                        string cssClass = "";
                        if (k == 0)
                            cssClass = "alpha";
                        else if (k == 2)
                            cssClass = "omega";


                        sb.Append("<div class=\"grid_8 " + cssClass + " cs_container\">");
                        sb.Append(this.GetMemberBasicInfo(filteredEmployeeList[k]));
                        sb.Append(this.GetMemberFullInfo(filteredEmployeeList[k]));
                        sb.Append("</div>");
                    }
                    sb.Append("</div>");
                    sb.Append(" </div>");
                    sb.Append("<div class=\"clear\"></div>");

                    sb.Append("<div id=\"profile_detail_box_" + count + "\" class=\"mgmt_profile_detail_wrapper\">");
                    sb.Append("<div class=\"container_24\">");
                    sb.Append("<div class=\"grid_24\">");
                    sb.Append("<a class=\"close_button\"><span class=\"visuallyhidden\">X</span></a>");
                    sb.Append("<div class=\"profile_detail_content\"></div>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                    count++;
                }
                ltrOurTeam.Text = sb.ToString();
            }
            catch(Exception ex)
            {
                Log.WriteError(ex);
            }
        }
    }

    private string GetMemberBasicInfo(ContentData contentData)
    {
        StringBuilder sbHtml = new StringBuilder();
        XmlDocument contentXML = new XmlDocument();
        contentXML.LoadXml(contentData.Html);

        XmlNodeList xnList = contentXML.SelectNodes("/root");
        if (xnList != null && xnList.Count > 0)
        {
            string firstName = xnList[0]["firstName"].InnerXml;
            string lastName = xnList[0]["lastName"].InnerXml;
            string jobTitle = xnList[0]["jobTitle"].InnerXml;

            string workPhone = string.Empty;
            if (xnList[0]["workPhone"] != null)
                workPhone = xnList[0]["workPhone"].InnerXml;
            string mobilePhone = string.Empty;
            if (xnList[0]["mobileNumber"] != null)
                mobilePhone = xnList[0]["mobileNumber"].InnerXml;
            string email = string.Empty;
            if (xnList[0]["email"] != null)
                email = xnList[0]["email"].InnerXml;

            string gender = xnList[0]["gender"].InnerXml;

            string abstractText = string.Empty;
            if (xnList[0]["abstract"] != null)
                abstractText = xnList[0]["abstract"].InnerXml;
            string bio = xnList[0]["bio"].InnerXml;

            string xml = contentXML.SelectSingleNode("/root/mediumImage").InnerXml;
            string imgSRC = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;

            sbHtml.Append("<a id=\"profile_" + contentData.Id + "\" class=\"cs_image\" href=\"#\">");
            sbHtml.Append("<img src=\"" + imgSRC + "\" alt=\"\" />");
            sbHtml.Append("<div class=\"cs_image_content_wrapper\">");
            sbHtml.Append("<div class=\"cs_image_content\">");
            sbHtml.Append("<h3>" + firstName + " " + lastName + "</h3>");
            sbHtml.Append("<p>" + jobTitle + "</p>");
            sbHtml.Append("<ul>");
            if (!string.IsNullOrEmpty(workPhone))
                sbHtml.Append("<li class=\"mt_phone\"><a href=\"tel:" + workPhone + "\">" + workPhone + "</a></li>");
            if (!string.IsNullOrEmpty(mobilePhone))
                sbHtml.Append("<li class=\"mt_mobile\"><a href=\"tel:" + mobilePhone + "\">" + mobilePhone + "</a></li>");
            if (!string.IsNullOrEmpty(email))
                sbHtml.Append("<li class=\"mt_email\"><a href=\"mailto:" + email + "\">" + email + "</a></li>");
            sbHtml.Append("</ul>");
            sbHtml.Append("<span class=\"more_button\" href=\"#\">more</span>");
            sbHtml.Append("</div>");
            sbHtml.Append("</div>");
            sbHtml.Append("</a>");
        }
        return sbHtml.ToString();
    }

    private string GetMemberFullInfo(ContentData contentData)
    {
        StringBuilder sbHtml = new StringBuilder();
        XmlDocument contentXML = new XmlDocument();
        contentXML.LoadXml(contentData.Html);

        XmlNodeList xnList = contentXML.SelectNodes("/root");
        if (xnList != null && xnList.Count > 0)
        {
            string firstName = xnList[0]["firstName"].InnerXml;
            string lastName = xnList[0]["lastName"].InnerXml;
            string jobTitle = xnList[0]["jobTitle"].InnerXml;

            string workPhone = string.Empty;
            if (xnList[0]["workPhone"] != null)
                workPhone = xnList[0]["workPhone"].InnerXml;
            string mobilePhone = string.Empty;
            if (xnList[0]["mobileNumber"] != null)
                mobilePhone = xnList[0]["mobileNumber"].InnerXml;
            string email = string.Empty;
            if (xnList[0]["email"] != null)
                email = xnList[0]["email"].InnerXml;

            string linkedin = string.Empty;
            if (contentXML.SelectSingleNode("/root/linkedInUrl/a") != null)
                linkedin = contentXML.SelectSingleNode("/root/linkedInUrl/a").Attributes["href"].InnerText;

            string twitterUrl = string.Empty;
            if (contentXML.SelectSingleNode("/root/twitterUrl/a") != null)
                twitterUrl = contentXML.SelectSingleNode("/root/twitterUrl/a").Attributes["href"].InnerText;

            string faceBookUrl = string.Empty;
            if (contentXML.SelectSingleNode("/root/facebookUrl/a") != null)
                faceBookUrl = contentXML.SelectSingleNode("/root/facebookUrl/a").Attributes["href"].InnerText;

            string gender = xnList[0]["gender"].InnerXml;
            string abstractText = xnList[0]["abstract"].InnerXml;
            string bio = xnList[0]["bio"].InnerXml;

            string xml = contentXML.SelectSingleNode("/root/mediumImage").InnerXml;
            string imgSRC = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
            string urlxml = contentXML.SelectSingleNode("/root/linkedInUrl").InnerXml;

            sbHtml.Append("<div id=\"profile_" + contentData.Id + "_detail\" class=\"profile_detail clearfix\">");
            sbHtml.Append("<div class=\"prefix_1 grid_22 suffix_1 omega profile_text\">");
            sbHtml.Append("<div class=\"profile_text_inner\">");
            sbHtml.Append("<div class=\"grid_12\">");
            sbHtml.Append("<h3>" + firstName + " " + lastName + "</h3>");
            sbHtml.Append("<h4>" + jobTitle + "</h4>");
            if (!string.IsNullOrEmpty(abstractText))
                sbHtml.Append("<h2 class=\"headline\">" + abstractText + "</h2>");
            sbHtml.Append("<ul>");
            if (workPhone != string.Empty)
                sbHtml.Append("<li class=\"mt_phone\"><a href=\"tel:" + workPhone + "\">" + workPhone + "</a></li>");
            if (mobilePhone != string.Empty)
                sbHtml.Append("<li class=\"mt_mobile\"><a href=\"tel:" + mobilePhone + "\">" + mobilePhone + "</a></li>");
            if (email != string.Empty)
                sbHtml.Append("<li class=\"mt_email\"><a href=\"mailto:" + email + "\">" + email + "</a></li>");
            sbHtml.Append("</ul>");

            sbHtml.Append("<div class=\"grid_14 social_media\"><ul>");
            if (linkedin != string.Empty)
                sbHtml.Append("<li><a href=\"" + linkedin + "\" target=\"_blank\"><img src=\"/images/social-icons/green_54_linkedin.png\" alt=\"LinkedIn\"></a></li>");
            if (twitterUrl != string.Empty)
                sbHtml.Append("<li><a href=\"" + twitterUrl + "\" target=\"_blank\"><img src=\"/images/social-icons/green_54_twitter.png\" alt=\"Twitter\"></a></li>");
            if (faceBookUrl != string.Empty)
                sbHtml.Append("<li><a href=\"" + faceBookUrl + "\" target=\"_blank\"><img src=\"/images/social-icons/green_54_facebook.png\" alt=\"Facebook\"></a></li>");
            sbHtml.Append("</ul></div>");

            sbHtml.Append("</div>");
            sbHtml.Append(" <div class=\"grid_12\"><div class=\"profileBio\">" + bio + "</div></div>");
            sbHtml.Append("</div>");
            sbHtml.Append("</div>");
            sbHtml.Append("</div>");
        }
        return sbHtml.ToString();
    }    

}