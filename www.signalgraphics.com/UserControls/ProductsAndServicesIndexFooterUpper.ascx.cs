using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Figleaf;
using SignalGraphics.CMS;
using System.Data;
using System.Xml;
using System.Text.RegularExpressions;
using Ektron.Cms.Content;
using Ektron.Cms.Instrumentation;
using Ektron.Cms.Framework.Organization;
using Ektron.Cms.Common;

public partial class UserControls_ProductsAndServicesIndexFooterUpper : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var blogsData = BlogsDataManager.GetRssFeed(3);
            uxBlogs.DataSource = blogsData;
            uxBlogs.DataBind();

            if (FransDataManager.IsFranchiseSelected())
            {
                UxBriefWhitepapers.DataSource = GetBriefsWHitepaperLocalContent();
                UxBriefWhitepapers.DataBind();
            }
            else
            {
                UxBriefWhitepapers.DataSource = GetBriefsWHitepaperContent();
                UxBriefWhitepapers.DataBind();
            }
        }
    }

    private DataTable GetBriefsWHitepaperContent()
    {
        string fransId = FransDataManager.IsFranchiseSelected() ? FransDataManager.GetFranchiseId() : "0";
        string cacheKey = String.Format("SignalGraphics:UserControls_ProductsAndServicesIndexFooterUpper:GetBriefsWHitepaperContent:FranchiseId={0}",
            fransId);
       
        DataTable sortedDT = CacheBase.Get<DataTable>(cacheKey);

        if (sortedDT == default(DataTable) || sortedDT.Rows.Count == 0)
        {
            DataTable DTSource = new DataTable();
            //DataTable sortedDT = new DataTable();
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

            long whitepapersPSCollectionId = ConfigHelper.GetValueLong("BriefsWhitepapersOnPSCollectionId");
            var ccc = new ContentCollectionCriteria();
            ccc.AddFilter(whitepapersPSCollectionId);
            var contentItems = ContentHelper.GetListByCriteria(ccc);
            if (contentItems != null && contentItems.Any())
            {
                foreach (Ektron.Cms.ContentData contentData in contentItems)
                {
                    try
                    {
                        contentXML.LoadXml(contentData.Html);

                        XmlNodeList xnList = contentXML.SelectNodes("/root");
                        string title = xnList[0]["title"].InnerXml;
                        string teaserMM = xnList[0]["teaserMM"].InnerXml;
                        string abstractText = xnList[0]["abstract"].InnerXml;
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
                //DVPSMMSort.Sort = "dateCreated desc";
                sortedDT = DataviewHelper.SelectTopDataRow(DVPSMMSort.ToTable(), 2);
                CacheBase.Put(cacheKey, sortedDT, CacheDuration.For24Hr);
            }
        }
        return sortedDT;
    }

    private DataTable GetBriefsWHitepaperLocalContent()
    {
        string fransId = FransDataManager.IsFranchiseSelected() ? FransDataManager.GetFranchiseId() : "0";
        string cacheKey = String.Format("SignalGraphics:UserControls_ProductsAndServicesIndexFooterUpper:GetBriefsWHitepaperLocalContent:FranchiseId={0}",
            fransId);

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

            long whitepapersPSCollectionId = ConfigHelper.GetValueLong("LocalWhitePapersPSCollId");
            var ccc = new ContentCollectionCriteria();
            ccc.AddFilter(whitepapersPSCollectionId);
            var contentItems = ContentHelper.GetListByCriteria(ccc);
            if (contentItems != null && contentItems.Any())
            {
                foreach (Ektron.Cms.ContentData contentData in contentItems)
                {
                    try
                    {
                        contentXML.LoadXml(contentData.Html);

                        XmlNodeList xnList = contentXML.SelectNodes("/root");
                        string title = xnList[0]["title"].InnerXml;
                        string teaserMM = xnList[0]["teaserMM"].InnerXml;
                        string abstractText = xnList[0]["abstract"].InnerXml;
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
                        string hrefText = "/briefs_whitepapers_details.aspx?sid=" + contentData.Id;
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
                //DVPSMMSort.Sort = "dateCreated desc";
                sortedDT = DataviewHelper.SelectTopDataRow(DVPSMMSort.ToTable(), 2);
                CacheBase.Put(cacheKey,sortedDT,CacheDuration.For24Hr);
            }
        }
        return sortedDT;
    }
}