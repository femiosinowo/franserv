using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Text;
using System.Configuration;
using System.Collections.Generic;

using Ektron.Cms;
using Ektron.Cms.Framework.Organization;
using System.Xml;


namespace SirSpeedy.CMS
{
    /// <summary>
    /// Summary description for SirSpeedyUtility
    /// </summary>
    public class SirSpeedyUtility
    {
        public static string SISUploadedVirtualFolderPath = System.Configuration.ConfigurationManager.AppSettings["SISUploadedVirtualFolderPath"];

        public SirSpeedyUtility()
        {
            //constructor
        }
        
        public static string GetModelSiteConfigKey(long id)
        {
            string key = "";
            if (id > 0)
            {
                long modelSiteConfigKetCID;
                string cacheKey = "SirSpeedymodelSiteConfigContent";
                long.TryParse(ConfigurationManager.AppSettings["ModelSiteConfigKeysCId"], out modelSiteConfigKetCID);
                ContentData modelSiteConfigContent = new ContentData();

                bool dataExistInCache = ApplicationCache.IsExist(cacheKey);
                if (!dataExistInCache)
                {
                    modelSiteConfigContent = ContentHelper.GetContentById(modelSiteConfigKetCID);
                    if (modelSiteConfigContent != null && modelSiteConfigContent.Id > 0)
                        ApplicationCache.Insert(cacheKey, modelSiteConfigContent, ConfigHelper.GetValueLong("longCacheInterval"));
                }
                else
                {
                    var cacheData = ApplicationCache.GetValue(cacheKey);
                    if (cacheData != null)
                        modelSiteConfigContent = (ContentData)cacheData;
                }

                if (modelSiteConfigContent != null && modelSiteConfigContent.Id > 0)
                {
                    XDocument xDoc = XDocument.Parse(modelSiteConfigContent.Html);
                    var items = xDoc.XPathSelectElements("root/Item");

                    var filterData = (from item in xDoc.Descendants("Item")
                                      where item.Element("Value").Value == id.ToString()
                                      select item).FirstOrDefault();
                    if (filterData != null)
                    {
                        key = filterData.Element("Label").Value;
                    }
                }
            }
            return key;
        }

        public static string ExtractNodeHtml(XElement node)
        {
            StringBuilder innerXml = new StringBuilder();
            if (node != null)
            {
                foreach (XNode n in node.Nodes())
                {
                    innerXml.Append(n.ToString());
                }
            }
            return innerXml.ToString();
        }        
    }
}