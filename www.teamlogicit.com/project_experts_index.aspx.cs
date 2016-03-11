using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Ektron.Cms.Instrumentation;
using TeamLogic.CMS;

public partial class UserControls_project_experts_index : PageBase
{
    protected void Page_Init(object sender, EventArgs e)
    {
        cbCurrentProject.DefaultContentID = ConfigHelper.GetValueLong("ProjectExpertDescriptionCId");
        cbCurrentProject.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbCurrentProject.Fill();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dtProjectExpertsContent = this.GetProjectExpertsContent();
            lvProjectExpertsIcons.DataSource = dtProjectExpertsContent;
            lvProjectExpertsIcons.DataBind();

            lvProjectExpertsDetail.DataSource = dtProjectExpertsContent;
            lvProjectExpertsDetail.DataBind();
        }
    }

    private DataTable GetProjectExpertsContent()
    {
        DataTable DTSource = new DataTable();
        DataTable sortedDT = new DataTable();
        XmlDocument contentXML = new XmlDocument();
        DTSource.Columns.Add("id");
        DTSource.Columns.Add("titletxt");
        DTSource.Columns.Add("titlehtml");
        DTSource.Columns.Add("subtitle");
        DTSource.Columns.Add("headline");
        DTSource.Columns.Add("imgSRC");
        DTSource.Columns.Add("bgimgSRC");
        DTSource.Columns.Add("desc");
        DTSource.Columns.Add("hreftext");

        var contents = SiteDataManager.GetProjectExpertsContent();
        if (contents != null && contents.Count > 0)
        {
            foreach (Ektron.Cms.ContentData contentData in contents)
            {
                try
                {
                    contentXML.LoadXml(contentData.Html);
                    string contentId = contentData.Id.ToString();
                    string hreftext = contentData.Quicklink;
                    XmlNodeList xnList = contentXML.SelectNodes("/root");

                    string titlehtml = xnList[0]["title"].InnerXml;
                    string title = Regex.Replace(titlehtml, "<.*?>", " ");
                    string subtitle = xnList[0]["subTitle"].InnerXml;
                    string headline = string.Empty;
                    if (xnList[0]["header"] != null)
                        headline = xnList[0]["header"].InnerText;
                    string xml = contentXML.SelectSingleNode("/root/image").InnerXml;
                    string imgSRC = Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                    string bgimgSRC = string.Empty;
                    if (contentData.Image != string.Empty && contentData.Image.ToLower().IndexOf("workarea") <= -1)
                    {
                        bgimgSRC = "/" + contentData.Image;
                        //bgimgSRC = Regex.Match(bgImageXML, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                    }
                    string desc = xnList[0]["shortDescription"].InnerXml;
                    DTSource.Rows.Add(contentId, title, titlehtml, subtitle, headline, imgSRC, bgimgSRC, desc, hreftext);
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }
        }
        return DTSource;
    }
}