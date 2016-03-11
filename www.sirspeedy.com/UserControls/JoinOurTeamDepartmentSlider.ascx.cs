using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ektron.Cms.Content;
using Ektron.Cms.Common;
using Figleaf;
using SirSpeedy.CMS;
using System.Xml;
using System.Data;
using System.Text.RegularExpressions;
using Ektron.Cms.Instrumentation;

public partial class UserControls_JoinOurTeamDepartmentSlider : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UxDepartmentSlider.DataSource = GetJoinOurTeamContent();
            UxDepartmentSlider.DataBind();
        }
    }

    private DataTable GetJoinOurTeamContent()
    {
        long folderID = ConfigHelper.GetValueLong("DepartmentsFolderID");
        long SmartFormXMLConfig = ConfigHelper.GetValueLong("orgDepartmentSmartFormID");
        int counter = 0;

        string fransId = FransDataManager.IsFranchiseSelected() ? FransDataManager.GetFranchiseId() : "0";

        string cacheKey = String.Format("Sirspeedy:UserControlsJoinOurTeamDepartmentSlider:GetJoinOurTeamContent:FolderID={0}:SmartFormXMLConfig={1}:FranchiseId={2}", folderID, SmartFormXMLConfig, fransId);

        DataTable sortedDT = CacheBase.Get<DataTable>(cacheKey);

        if (sortedDT == default(DataTable) || sortedDT.Rows.Count == 0)
        {
            DataTable DTSource = new DataTable();
            // DataTable sortedDT = new DataTable();

            XmlDocument contentXML = new XmlDocument();


            DTSource.Columns.Add("name");
            DTSource.Columns.Add("teaserMM");
            DTSource.Columns.Add("imgSRCMM");
            DTSource.Columns.Add("tagline");
            DTSource.Columns.Add("abstract");
            DTSource.Columns.Add("desc");
            DTSource.Columns.Add("url");
            DTSource.Columns.Add("imgSRC");
            DTSource.Columns.Add("hreftext");
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
                        contentXML.LoadXml(contentData.Html);

                        string hreftext = contentData.Quicklink;

                        XmlNodeList xnList = contentXML.SelectNodes("/root");
                        //string name = xnList[0]["name"].InnerXml;
                        string name = TaxonomyHelper.GetTaxonomyNameByContentId(contentData.Id);
                        string teaserMM = xnList[0]["teaserMM"].InnerXml;
                        string tagline = xnList[0]["tagline"].InnerXml;
                        string abstractText = xnList[0]["abstract"].InnerXml;
                        string desc = xnList[0]["desc"].InnerXml;

                        string xml = contentXML.SelectSingleNode("/root/imgSRCMM").InnerXml;
                        string imgSRCMM =
                            Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                        xml = contentXML.SelectSingleNode("/root/image").InnerXml;
                        string imgSRC =
                            Regex.Match(xml, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                        xml = contentXML.SelectSingleNode("/root/url").InnerXml;
                        string url = "#";
                        //Regex.Match(xml, "<a.+?href=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value; //not sure about this
                        if (!string.IsNullOrEmpty(name))
                        {
                            url = "/job-profiles/?type=" + name.ToLower().Replace(" ", "");
                            hreftext = url;
                        }

                        counter++;

                        DTSource.Rows.Add(name, teaserMM, imgSRCMM, tagline, abstractText, desc, url, imgSRC, hreftext,
                            counter);
                    }
                    catch (Exception ex)
                    {
                        Log.WriteError(ex);
                    }
                }
                //DataView DVPSMMSort = DTSource.DefaultView;
                //DVPSMMSort.Sort = "counter desc";
                //sortedDT = DVPSMMSort.ToTable();
                sortedDT = DTSource;
                CacheBase.Put(cacheKey,sortedDT,CacheDuration.For24Hr);
            }
        }
        return sortedDT;
        
    }
}