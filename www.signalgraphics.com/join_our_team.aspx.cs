using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SignalGraphics.CMS;
using System.Xml;
using Ektron.Cms.Content;
using Ektron.Cms.Common;
using System.Text.RegularExpressions;
using Ektron.Cms.Instrumentation;
using System.Xml.Linq;
using HtmlAgilityPack;
using System.Text;

public partial class join_our_team : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        cbContentTitle.DefaultContentID = ConfigHelper.GetValueLong("contentTitleWhyWorkWithUsContentId");
        cbContentTitle.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbContentTitle.Fill();

        cbJoinOurTeamsMainContent.DefaultContentID = ConfigHelper.GetValueLong("WhyWorkWithUsMainContentId");
        cbJoinOurTeamsMainContent.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbJoinOurTeamsMainContent.Fill();

        cbFindJob.DefaultContentID = ConfigHelper.GetValueLong("JoinOurTeamFindJobDespCId");
        cbFindJob.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbFindJob.Fill();

        cbSliderTagline.DefaultContentID = ConfigHelper.GetValueLong("WhyWorkWithUsSliderTitleContentId");
        cbSliderTagline.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbSliderTagline.Fill();

        cbSliderTeaser.DefaultContentID = ConfigHelper.GetValueLong("WhyWorkWithUsSliderTeaserContentId");
        cbSliderTeaser.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbSliderTeaser.Fill();

        //UxJoinOurTeamSlider.DataSource = GetSliderImageData();
        //UxJoinOurTeamSlider.DataBind();
    }

    private DataTable GetSliderImageData()
    {
        //DataTable DTSource = new DataTable();
        //DataTable sortedDT = new DataTable();
        //XmlDocument contentXML = new XmlDocument();
        //int counter = 0;

        DataTable DTSource = new DataTable();
        DataTable sortedDT = new DataTable();
        long folderID = ConfigHelper.GetValueLong("JoinOurTeamFolderID");
        XmlDocument contentXML = new XmlDocument();
        long PSSmartFormXMLConfig = ConfigHelper.GetValueLong("JoinOurTeamSliderSmartFormID");
        long SmartFormXMLConfig = PSSmartFormXMLConfig; //your smartform xml config mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm
        int counter = 0;

        DTSource.Columns.Add("imageSRC");
        DTSource.Columns.Add("counter");

        //get all contents in the specified folder
        ContentCriteria criteria = new ContentCriteria();
        criteria.AddFilter(ContentProperty.FolderId, CriteriaFilterOperator.EqualTo, folderID);
        criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, SmartFormXMLConfig);
        var contents = ContentHelper.GetListByCriteria(criteria);

        if (contents != null && contents.Count > 0)
        {
            foreach (Ektron.Cms.ContentData contentData in contents)
            {
                try
                {
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(contentData.Html);
                    HtmlNodeCollection bodyNodes = doc.DocumentNode.SelectNodes("//img[@src]");
                    foreach (var node in bodyNodes)
                    {
                        string src = "<img src=\"" + node.Attributes["src"].Value + "\" />";
                        counter++;
                        DTSource.Rows.Add(src, counter);
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }

            }
            DataView DVPSMMSort = DTSource.DefaultView;
            DVPSMMSort.Sort = "counter asc";
            sortedDT = DVPSMMSort.ToTable();
        }
        return sortedDT;
    }
}