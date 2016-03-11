using Ektron.Cms;
using Ektron.Cms.Common;
using Ektron.Cms.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamLogic.CMS
{

    /// <summary>
    /// Summary description for M3TDataManager
    /// </summary>
    public class M3TDataManager
    {
        public M3TDataManager()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// This method is used to get the List of Products and Services Content Data for Nation or Local
        /// </summary>
        /// <returns></returns>
        public static List<ContentData> GetProductAndServices()
        {
            List<ContentData> dataList = null;
            long productsAndServicesFolderId = ConfigHelper.GetValueLong("ProductsAndServicesCategoriesFolderID");
            long productsAndServicesSFId = ConfigHelper.GetValueLong("ProductsAndServicesSmartFormID");
            if (FransDataManager.IsFranchiseSelected())
            {
                List<long> cIds = new List<long>();
                var workAreaData = FransDataManager.GetFransWorkareaData();
                if (workAreaData != null && workAreaData.ProductAndServices != null && workAreaData.ProductAndServices.Any())
                {
                    var psData = workAreaData.ProductAndServices;
                    foreach (var ps in psData)
                        cIds.Add(ps.MainCategoryId);

                    if (cIds != null && cIds.Any())
                    {
                        var contentCriteria = new ContentCriteria(ContentProperty.DateModified, EkEnumeration.OrderByDirection.Descending);
                        contentCriteria.AddFilter(ContentProperty.Id, CriteriaFilterOperator.In, cIds);
                        contentCriteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, productsAndServicesSFId);
                        contentCriteria.OrderByField = ContentProperty.DateModified;
                        dataList = ContentHelper.GetListByCriteria(contentCriteria);
                        if (dataList != null && dataList.Any())
                            dataList = dataList.ToList();
                    }
                }
            }
            else
            {
                var cfc = new ContentCriteria(ContentProperty.DateModified, EkEnumeration.OrderByDirection.Descending);
                cfc.AddFilter(ContentProperty.FolderId, CriteriaFilterOperator.EqualTo, productsAndServicesFolderId);
                cfc.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, productsAndServicesSFId);
                dataList = ContentHelper.GetListByCriteria(cfc);
                if (dataList != null && dataList.Any())
                    dataList = dataList.ToList();
            }
            return dataList;
        }

        public static List<ContentData> GetProductAndServicesSideContent()
        {
            List<ContentData> data = null;
            long folderID = ConfigHelper.GetValueLong("ProductsAndServicesMegamenuFolderID");
            long PSSmartFormXMLConfig = ConfigHelper.GetValueLong("ProductsAndServicesMMSideSmartFormID");
            ContentCriteria criteria = new ContentCriteria();
            if (FransDataManager.IsFranchiseSelected())
            {
                //for now display the side content same as national
                criteria.AddFilter(ContentProperty.FolderId, CriteriaFilterOperator.EqualTo, folderID);
                criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, PSSmartFormXMLConfig);
                data = ContentHelper.GetListByCriteria(criteria);
            }
            else
            {
                criteria.AddFilter(ContentProperty.FolderId, CriteriaFilterOperator.EqualTo, folderID);
                criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, PSSmartFormXMLConfig);
                data = ContentHelper.GetListByCriteria(criteria);
            }
            return data;
        }

        public static List<ContentData> GetCompanyInfoContent()
        {
            List<ContentData> data = null;
            long folderID = ConfigHelper.GetValueLong("NationalCompanyInfoFolderID");
            long SmartFormXMLConfig = ConfigHelper.GetValueLong("CompanyInfoSmartFormID");
            if (FransDataManager.IsFranchiseSelected())
            {
                //get the national about us content
                ContentCriteria criteria = new ContentCriteria();
                criteria.AddFilter(ContentProperty.FolderId, CriteriaFilterOperator.EqualTo, folderID);
                criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, SmartFormXMLConfig);
                data = ContentHelper.GetListByCriteria(criteria);
            }
            else
            {
                ContentCriteria criteria = new ContentCriteria();
                criteria.AddFilter(ContentProperty.FolderId, CriteriaFilterOperator.EqualTo, folderID);
                criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, SmartFormXMLConfig);
                data = ContentHelper.GetListByCriteria(criteria);
            }
            return data;
        }

        public static List<ContentData> GetPartners()
        {
            List<ContentData> dataList = null;
            long partnersTaxId = ConfigHelper.GetValueLong("NationalPartnersTaxId");
            long partnersSFId = ConfigHelper.GetValueLong("PartnersSmartFormID");
            if (FransDataManager.IsFranchiseSelected())
            {
                List<long> cIds = new List<long>();
                var workAreaData = FransDataManager.GetFransWorkareaData();
                if (workAreaData != null)
                {
                    var ids = FransDataManager.GetFransPartnersIds();
                    if (ids != null && ids.Any())
                    {
                        var cfc = new ContentCriteria();
                        cfc.AddFilter(ContentProperty.Id, CriteriaFilterOperator.In, ids);
                        cfc.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, partnersSFId);
                        cfc.PagingInfo = new Ektron.Cms.PagingInfo(FransDataManager.GetCustomApiPageSize(), 1);
                        var dList = ContentHelper.GetListByCriteria(cfc);

                        ///sort again bez Ektorn api is automatically getting sorted by ContentId.
                        if (dList != null && dList.Any())
                        {
                            dataList = new List<ContentData>();
                            foreach (var id in ids)
                            {
                                var cData = dList.Where(x => x.Id == id).FirstOrDefault();
                                if (cData != null)
                                    dataList.Add(cData);
                            }
                        }
                    }
                }
            }
            else
            {
                if (partnersTaxId > 0)
                {
                    //get all items from taxonomy
                    var taxItems = TaxonomyHelper.GetTaxonomyTree(partnersTaxId, 1, true);
                    List<long> cIds = new List<long>();
                    if (taxItems != null && taxItems.TaxonomyItems != null)
                    {
                        foreach (var tItem in taxItems.TaxonomyItems)
                        {
                            cIds.Add(tItem.Id);
                        }
                    }

                    var cfc = new ContentCriteria();
                    cfc.AddFilter(ContentProperty.Id, CriteriaFilterOperator.In, cIds);
                    cfc.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, partnersSFId);
                    var dList = ContentHelper.GetListByCriteria(cfc);

                    ///sort again bez Ektorn api is automatically getting sorted by ContentId.
                    if (dList != null && dList.Any())
                    {
                        dataList = new List<ContentData>();
                        foreach (var id in cIds)
                        {
                            var cData = dList.Where(x => x.Id == id).FirstOrDefault();
                            if (cData != null)
                                dataList.Add(cData);
                        }
                    }
                }
            }
            return dataList;
        }

        public static List<ContentData> GetManagementTeamContent()
        {
            List<ContentData> dataList = null;
            long managementTeamTaxId = ConfigHelper.GetValueLong("ManagementTeamNationalTaxId");
            long managementTeamSFId = ConfigHelper.GetValueLong("ManagementTeamSmartFormID");
            if (FransDataManager.IsFranchiseSelected())
            {
                //commenting out the local team code bez the employees are not managed inside Ektron workarea

                //List<long> cIds = new List<long>();
                //var workAreaData = FransDataManager.GetFransWorkareaData();
                //if (workAreaData != null)
                //{
                //    cIds = workAreaData.CenterEmployeesTeam;
                //    if (cIds != null && cIds.Any())
                //    {
                //        var contentCriteria = new ContentCriteria(ContentProperty.DateModified, EkEnumeration.OrderByDirection.Descending);
                //        contentCriteria.AddFilter(ContentProperty.Id, CriteriaFilterOperator.In, cIds);
                //        contentCriteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, managementTeamSFId);
                //        contentCriteria.OrderByField = ContentProperty.DateModified;
                //        dataList = ContentHelper.GetListByCriteria(contentCriteria);
                //        if (dataList != null && dataList.Any())
                //            dataList = dataList.ToList();
                //    }
                //}
            }
            else
            {
                var ctc = new ContentTaxonomyCriteria(ContentProperty.DateModified, EkEnumeration.OrderByDirection.Descending);
                ctc.AddFilter(managementTeamTaxId);
                ctc.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, managementTeamSFId);
                dataList = ContentHelper.GetListByCriteria(ctc);
                if (dataList != null && dataList.Any())
                    dataList = dataList.ToList();
            }
            return dataList;
        }

        public static List<ContentData> GetNews(int count = 2)
        {
            //List<ContentData> data = null;
            //long folderID = ConfigHelper.GetValueLong("NewsFolderID");
            //long SmartFormXMLConfig = ConfigHelper.GetValueLong("NewsSmartFormID");

            //if (FransDataManager.IsFranchiseSelected())
            //{

            //}
            //else
            //{
            //    ContentCriteria criteria = new ContentCriteria();
            //    criteria.AddFilter(ContentProperty.FolderId, CriteriaFilterOperator.EqualTo, folderID);
            //    criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, SmartFormXMLConfig);
            //    data = ContentHelper.GetListByCriteria(criteria);
            //}
            //return data;

            List<ContentData> dataList = null;
            long newsTaxId = ConfigHelper.GetValueLong("AllNewsNationalTaxId");
            long newsSFId = ConfigHelper.GetValueLong("NewsSmartFormID");
            if (FransDataManager.IsFranchiseSelected())
            {
                List<long> cIds = new List<long>();
                var workAreaData = FransDataManager.GetFransWorkareaData();
                if (workAreaData != null)
                {
                    cIds = workAreaData.NewsContentIds;
                    if (cIds != null && cIds.Any())
                    {
                        var contentCriteria = new ContentCriteria(ContentProperty.DateModified, EkEnumeration.OrderByDirection.Descending);
                        contentCriteria.AddFilter(ContentProperty.Id, CriteriaFilterOperator.In, cIds);
                        contentCriteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, newsSFId);
                        contentCriteria.OrderByField = ContentProperty.DateModified;
                        dataList = ContentHelper.GetListByCriteria(contentCriteria);
                        if (dataList != null && dataList.Any())
                            dataList = dataList.ToList();
                    }
                }
            }
            else
            {
                var ctc = new ContentTaxonomyCriteria(ContentProperty.DateModified, EkEnumeration.OrderByDirection.Descending);
                ctc.AddFilter(newsTaxId);
                ctc.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, newsSFId);
                dataList = ContentHelper.GetListByCriteria(ctc);
                if (dataList != null && dataList.Any())
                    dataList = dataList.ToList();
            }
            return dataList;
        }

        public static List<ContentData> GetInTheMediaContent()
        {
            List<ContentData> dataList = null;
            long inTheMediaTaxId = ConfigHelper.GetValueLong("AllInTheMediaNationalTaxId");
            long inTheMediaSFId = ConfigHelper.GetValueLong("InTheMediaSmartFormID");
            if (FransDataManager.IsFranchiseSelected())
            {
                List<long> cIds = new List<long>();
                var workAreaData = FransDataManager.GetFransWorkareaData();
                if (workAreaData != null && workAreaData.InTheMediaContentIds != null && workAreaData.InTheMediaContentIds.Any())
                {
                    cIds = workAreaData.InTheMediaContentIds;
                    if (cIds != null && cIds.Any())
                    {
                        var contentCriteria = new ContentCriteria(ContentProperty.DateModified, EkEnumeration.OrderByDirection.Descending);
                        contentCriteria.AddFilter(ContentProperty.Id, CriteriaFilterOperator.In, cIds);
                        contentCriteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, inTheMediaSFId);
                        contentCriteria.OrderByField = ContentProperty.DateModified;
                        dataList = ContentHelper.GetListByCriteria(contentCriteria);
                        if (dataList != null && dataList.Any())
                            dataList = dataList.ToList();
                    }
                }
            }
            else
            {
                var ctc = new ContentTaxonomyCriteria(ContentProperty.DateModified, EkEnumeration.OrderByDirection.Descending);
                ctc.AddFilter(inTheMediaTaxId);
                ctc.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, inTheMediaSFId);
                dataList = ContentHelper.GetListByCriteria(ctc);
                if (dataList != null && dataList.Any())
                    dataList = dataList.ToList();
            }
            return dataList;
            //List<ContentData> data = null;
            //long folderID = ConfigHelper.GetValueLong("InTheMediaFolderID");
            //long SmartFormXMLConfig = ConfigHelper.GetValueLong("InTheMediaSmartFormID");

            //if (FransDataManager.IsFranchiseSelected())
            //{

            //}
            //else
            //{
            //    ContentCriteria criteria = new ContentCriteria();
            //    criteria.AddFilter(ContentProperty.FolderId, CriteriaFilterOperator.EqualTo, folderID);
            //    criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, SmartFormXMLConfig);
            //    data = ContentHelper.GetListByCriteria(criteria);
            //}
            //return data;

        }

        public static List<ContentData> GetBriefsAndWhitePapersContent()
        {
            List<ContentData> dataList = null;
            long briefWhitepapersTaxId = ConfigHelper.GetValueLong("BriefAndWhitepapersNationalTaxId");
            long briefWhitepapersSFId = ConfigHelper.GetValueLong("BriefsAndWhitepapersSmartFormID");
            if (FransDataManager.IsFranchiseSelected())
            {
                List<long> cIds = new List<long>();
                var workAreaData = FransDataManager.GetFransWorkareaData();
                if (workAreaData != null && workAreaData.BriefsWhitePapersContentIds != null && workAreaData.BriefsWhitePapersContentIds.Any())
                {
                    cIds = workAreaData.BriefsWhitePapersContentIds;
                    if (cIds != null && cIds.Any())
                    {
                        var contentCriteria = new ContentCriteria(ContentProperty.DateModified, EkEnumeration.OrderByDirection.Descending);
                        contentCriteria.AddFilter(ContentProperty.Id, CriteriaFilterOperator.In, cIds);
                        contentCriteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, briefWhitepapersSFId);
                        contentCriteria.OrderByField = ContentProperty.DateModified;
                        dataList = ContentHelper.GetListByCriteria(contentCriteria);
                        if (dataList != null && dataList.Any())
                            dataList = dataList.ToList();
                    }
                }
            }
            else
            {
                var ctc = new ContentTaxonomyCriteria(ContentProperty.DateModified, EkEnumeration.OrderByDirection.Descending);
                ctc.AddFilter(briefWhitepapersTaxId);
                ctc.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, briefWhitepapersSFId);
                dataList = ContentHelper.GetListByCriteria(ctc);
                if (dataList != null && dataList.Any())
                    dataList = dataList.ToList();
            }
            return dataList;
        }

        public static List<ContentData> GetCaseStudiesContent()
        {
            List<ContentData> dataList = null;
            long caseStudiesTaxId = ConfigHelper.GetValueLong("CaseStudiesNationalTaxId");
            long caseStudiesSFId = ConfigHelper.GetValueLong("CaseStudiesSmartFormID");
            if (FransDataManager.IsFranchiseSelected())
            {
                List<long> cIds = new List<long>();
                var workAreaData = FransDataManager.GetFransWorkareaData();
                if (workAreaData != null && workAreaData.CaseStudiesContentIds != null && workAreaData.CaseStudiesContentIds.Any())
                {
                    cIds = workAreaData.CaseStudiesContentIds;
                    if (cIds != null && cIds.Any())
                    {
                        var contentCriteria = new ContentCriteria(ContentProperty.DateModified, EkEnumeration.OrderByDirection.Descending);
                        contentCriteria.AddFilter(ContentProperty.Id, CriteriaFilterOperator.In, cIds);
                        contentCriteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, caseStudiesSFId);
                        contentCriteria.OrderByField = ContentProperty.DateModified;
                        dataList = ContentHelper.GetListByCriteria(contentCriteria);
                        if (dataList != null && dataList.Any())
                            dataList = dataList.ToList();
                    }
                }
            }
            else
            {
                var ctc = new ContentTaxonomyCriteria(ContentProperty.DateModified, EkEnumeration.OrderByDirection.Descending);
                ctc.AddFilter(caseStudiesTaxId);
                ctc.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, caseStudiesSFId);
                dataList = ContentHelper.GetListByCriteria(ctc);
                if (dataList != null && dataList.Any())
                    dataList = dataList.ToList();
            }
            return dataList;
        }

        public static List<ContentData> GetJobProfiles()
        {
            List<ContentData> data = null;
            long folderID = ConfigHelper.GetValueLong("DepartmentsFolderID");//("JobProfilesFolderID");
            long SmartFormXMLConfig = ConfigHelper.GetValueLong("orgDepartmentSmartFormID");

            if (FransDataManager.IsFranchiseSelected())
            {
                //todo: for now show the profiles on the local site also
                ContentCriteria criteria = new ContentCriteria();
                criteria.AddFilter(ContentProperty.FolderId, CriteriaFilterOperator.EqualTo, folderID);
                criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, SmartFormXMLConfig);
                data = ContentHelper.GetListByCriteria(criteria);
            }
            else
            {
                ContentCriteria criteria = new ContentCriteria();
                criteria.AddFilter(ContentProperty.FolderId, CriteriaFilterOperator.EqualTo, folderID);
                criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, SmartFormXMLConfig);
                data = ContentHelper.GetListByCriteria(criteria);
            }
            return data;
        }

        public static long GetSecondaryNavId()
        {
            long menuId = 0;
            if (FransDataManager.IsFranchiseSelected())
                menuId = ConfigHelper.GetValueLong("LocalSecondaryNavId");
            else
                menuId = ConfigHelper.GetValueLong("NationalSecondaryNavId");
            return menuId;
        }
    }
}