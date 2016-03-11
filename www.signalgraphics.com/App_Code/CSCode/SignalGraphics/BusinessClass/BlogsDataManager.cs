using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using Ektron.Cms.Instrumentation;
using System.Collections.Generic;

namespace SignalGraphics.CMS
{

    /// <summary>
    /// Summary description for BlogsDataManage
    /// </summary>
    public class BlogsDataManager
    {
        private static string rssFeedUrl = "http://www.marketingtango.com/feed/";

        public BlogsDataManager()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static List<Blog> GetRssFeed(int count = 2)
        {
            List<Blog> list = null;
            int i = 0;
            string paraVal = string.Empty;
            string Des = string.Empty;
            XmlDocument newsUrl = new XmlDocument();
            try
            {
                string cacheKey = "SignalGraphics-MarketingTangoBlogs" + count;
                bool dataExistInCache = ApplicationCache.IsExist(cacheKey);

                if (!dataExistInCache)
                {
                    newsUrl.Load(rssFeedUrl);
                    XDocument doc = XDocument.Parse(newsUrl.InnerXml);
                    var docs = doc.Root.Element("channel").ToString();
                    newsUrl.LoadXml(docs);
                    XmlNodeList idNodes = newsUrl.SelectNodes("channel/item");
                    list = new List<Blog>();
                    Blog blog = null;

                    foreach (XmlNode node in idNodes)
                    {
                        if (i < count)
                        {
                            blog = new Blog();
                            string p = node["description"].InnerXml;
                            Match m = Regex.Match(p, @"<p>\s*(.+?)\s*</p>");

                            if (m.Success)
                            {
                                paraVal = m.Groups[1].Value;
                                Des = paraVal.Substring((paraVal.IndexOf("/>") + 2));
                            }
                            string matchString = Regex.Match(node["description"].InnerXml, "<img.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase).Groups[1].Value;
                            blog.Image = matchString;

                            string[] val = node["pubDate"].InnerText.Split(',');
                            DateTime dt = Convert.ToDateTime(val[1]);
                            string[] Date = val[1].Split(' ');
                            blog.PostDate = dt.ToString("MMMM dd, yyyy");
                            if (node["title"].InnerText.Length > 50)
                            {
                                node["title"].InnerText = node["title"].InnerText.Substring(0, 50);
                                node["title"].InnerText += "...";
                            }
                            blog.Title = node["title"].InnerText;
                            if (Des.Length > 200)
                                Des = Des.Substring(0, 120);
                            blog.Description = Des;
                            blog.MoreLink = node["link"].InnerText;
                            list.Add(blog);
                            i++;
                        }
                    }

                    if(list != null && list.Any())
                        ApplicationCache.Insert(cacheKey, list, ConfigHelper.GetValueLong("longCacheInterval"));
                }
                else
                {
                    var cacheData = ApplicationCache.GetValue(cacheKey);
                    if (cacheData != null)
                        list = (List<Blog>)cacheData;
                }
            }
            catch(Exception ex)
            {
                Log.WriteError(ex);
            }
            return list;
        }

        public static List<Blog> GetRssFeedByCategoryName(string CategoryName, int count = 2)
        {
            List<Blog> list = null;
            if (!string.IsNullOrEmpty(CategoryName))
            {
                int i = 0;
                string catName = CategoryName.Trim().Replace(" ", "");
                string paraVal = string.Empty;
                string Des = string.Empty;
                XmlDocument newsUrl = new XmlDocument();
                try
                {
                    string cacheKey = "SignalGraphics-MarketingTangoBlogs" + catName;
                    bool dataExistInCache = ApplicationCache.IsExist(cacheKey);

                    if (!dataExistInCache)
                    {
                        newsUrl.Load(rssFeedUrl);
                        XDocument doc = XDocument.Parse(newsUrl.InnerXml);
                        var docs = doc.Root.Element("channel").ToString();
                        newsUrl.LoadXml(docs);
                        list = new List<Blog>();
                        Blog blog = null;

                        var itemList = doc.XPathSelectElement("/rss/channel").Descendants("item");
                        if (itemList != null && itemList.Any())
                        {
                            CategoryName = CategoryName.ToLower();
                            var filteredList = from item in itemList
                                               where item.XPathSelectElement("category").Value.ToLower().IndexOf(CategoryName) > -1
                                               select item;

                            if (filteredList != null && filteredList.Any())
                            {
                                foreach (var node in filteredList)
                                {
                                    if (i < count)
                                    {
                                        blog = new Blog();
                                        string p = node.XPathSelectElement("description").Value;
                                        Match m = Regex.Match(p, @"<p>\s*(.+?)\s*</p>");

                                        if (m.Success)
                                        {
                                            paraVal = m.Groups[1].Value;
                                            Des = paraVal.Substring((paraVal.IndexOf("/>") + 2));
                                        }
                                        string matchString = Regex.Match(node.XPathSelectElement("description").Value, "<img.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase).Groups[1].Value;
                                        blog.Image = matchString;

                                        string[] val = node.XPathSelectElement("pubDate").Value.Split(',');
                                        DateTime dt = Convert.ToDateTime(val[1]);
                                        string[] Date = val[1].Split(' ');
                                        blog.PostDate = dt.ToString("MMMM dd, yyyy");
                                        string title="";
                                        if (node.XPathSelectElement("title").Value.Length > 50)
                                        {
                                            title = node.XPathSelectElement("title").Value.Substring(0, 50);
                                            title += "...";
                                        }
                                        else
                                        {
                                            title = node.XPathSelectElement("title").Value;
                                        }
                                        blog.Title = title;
                                        if (Des.Length > 200)
                                            Des = Des.Substring(0, 120);
                                        blog.Description = Des;
                                        blog.MoreLink = node.XPathSelectElement("link").Value;
                                        list.Add(blog);
                                        i++;
                                    }
                                }
                            }
                        }                       

                        if (list != null && list.Any())
                            ApplicationCache.Insert(cacheKey, list, ConfigHelper.GetValueLong("longCacheInterval"));
                    }
                    else
                    {
                        var cacheData = ApplicationCache.GetValue(cacheKey);
                        if (cacheData != null)
                            list = (List<Blog>)cacheData;
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex);
                }
            }
            return list;
        }
    }
}