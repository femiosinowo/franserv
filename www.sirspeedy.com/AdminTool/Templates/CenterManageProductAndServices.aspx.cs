using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SirSpeedy.CMS;
using Ektron.Cms;
using Ektron.Cms.Content;
using Ektron.Cms.Common;
using Ektron.Cms.Instrumentation;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using Ektron.Cms.Organization;

public partial class AdminTool_Templates_CenterManageProductAndServices : System.Web.UI.Page
{
    string centerId;
    UserAPI userApi = new UserAPI();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (userApi.UserId > 0)
            {
                var udata = CommunityUserHelper.GetUserByUserId(userApi.UserId);
                if (udata != null && udata.Id > 0)
                {
                    var centerUsers = AdminToolManager.GetAllUsers();
                    var userData = centerUsers.Where(x => x.UserName.ToLower() == udata.Username.ToLower()).FirstOrDefault();
                    if (userData != null)
                    {
                        centerId = userData.FransId;
                        hdnCenterId.Value = centerId;
                        this.FillPSData();                        
                    }
                }
            }
        }

        if (!string.IsNullOrEmpty(hdnCenterId.Value))
        {
            var centerData = FransDataManager.GetFransData(hdnCenterId.Value);
            if (centerData != null)
            {
                centerInfo.Visible = true;
                lblCenterName.Text = centerData.CenterName;
                lblCenterId.Text = centerData.FransId;
            }
        }
    }

    protected void btnProductServices_Click(object sender, EventArgs e)
    {
        if (hddnPSIds.Value != "" && hddnPSFinalSelectedIds.Value != "" && Page.IsValid)
        {
            try
            {
                var finalSelectedIds = hddnPSFinalSelectedIds.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var finalselectedCategories = hddnPSIds.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                string sortedList = string.Empty;
                foreach (var fsId in finalSelectedIds)
                {
                    foreach (var fsC in finalselectedCategories)
                    {
                        var fSelectedCatIds = fsC.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var cID in fSelectedCatIds)
                        {
                            if (cID == fsId)
                                sortedList += fsC + "|";
                        }
                    }
                }

                lblError.Text = "";
                var taxCriteria = new TaxonomyCriteria();
                string[] selectedCategories = sortedList.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                //List<long> ids = new List<long>();
                if (selectedCategories != null)
                {
                    List<ProductsAndServices> psSelectedList = new List<ProductsAndServices>();
                    foreach (var s in selectedCategories)
                    {
                        if (s != "0")
                        {
                            string[] selectedCatIds = s.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                            if (selectedCatIds != null)
                            {
                                bool isValidSelection = false;
                                if (selectedCatIds[0] != null)
                                {
                                    var selectedId = selectedCatIds[0];
                                    isValidSelection = hddnPSFinalSelectedIds.Value.Contains(selectedId);
                                }
                                if (isValidSelection)
                                {
                                    ProductsAndServices item = new ProductsAndServices();
                                    for (int k = 0; k < selectedCatIds.Length; k++)
                                    {
                                        if (k == 0 && selectedCatIds[k] != null)
                                        {
                                            long cId;
                                            long.TryParse(selectedCatIds[k], out cId);
                                            var cData = ContentHelper.GetContentById(cId);
                                            if (cData != null && cData.Id > 0)
                                            {
                                                item.MainCategoryId = cId;
                                                item.MainCategoryName = cData.Title;
                                            }
                                        }
                                        if (k == 1 && selectedCatIds[k] != null)
                                        {
                                            var subCategoryIds = selectedCatIds[k].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                            item.SubCategoryContentId = GetIds(subCategoryIds);
                                        }
                                    }
                                    if (item.MainCategoryId > 0)
                                        psSelectedList.Add(item);
                                }
                            }
                        }
                    }

                    if (psSelectedList.Any())
                    {
                        var serializedTaxData = Ektron.Cms.EkXml.Serialize(typeof(List<ProductsAndServices>), psSelectedList);
                        if (serializedTaxData != null)
                        {
                            var centerId = hdnCenterId.Value;
                            int status = AdminToolManager.UpdateProductAndServices(centerId, serializedTaxData);
                            if (status > 0)
                            {
                                psUpdateMsg.Visible = true;
                                FillPSData();
                            }
                            else
                            {
                                lblError.Text = "Sorry, an error has occured processing your request.";
                                lblError.Visible = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
                lblError.Text = "Sorry, an error has occured processing your request.";
                lblError.Visible = true;
            }
        }
    }
    
    private void FillPSData()
    {
        long productsAndServicesSFId = ConfigHelper.GetValueLong("ProductsAndServicesSmartFormID");
        //long productsAndServicesFolderId = ConfigHelper.GetValueLong("ProductsAndServicesCategoriesFolderID");

        try
        {
            centerId = hdnCenterId.Value;
            var availablePSIds = AdminToolManager.GetAvailablePandS(centerId);

            var cc = new ContentCriteria();
            cc.AddFilter(ContentProperty.Id, CriteriaFilterOperator.In, availablePSIds);
            cc.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, productsAndServicesSFId);

            var contentList = ContentHelper.GetListByCriteria(cc);
            if (contentList != null && contentList.Any())
            {
                var workareaData = FransDataManager.GetFransWorkareaData(centerId, true);
                if (workareaData != null && workareaData.ProductAndServices != null)
                {
                    var existingPSList = workareaData.ProductAndServices;
                    //var subCategoriesList = existingPSList.SubCategories;

                    List<ContentData> sortedList = new List<ContentData>();
                    //add the selected items on the top
                    foreach (var e in existingPSList)
                    {
                        var cData = contentList.Where(x => x.Id == e.MainCategoryId).FirstOrDefault();
                        if (cData != null)
                            sortedList.Add(cData);
                    }
                    //add non-selected items after selected items
                    foreach (var b in contentList)
                    {
                        if (!sortedList.Contains(b))
                            sortedList.Add(b);
                    }

                    var cFinalList = from c in sortedList
                                     select new
                                     {
                                         AvailableCategory = existingPSList.Exists(x => x.MainCategoryId == c.Id) ? "" : "<div class=\"drag t1\"><span  id='" + c.Id + "'>" + c.Title + "</span></div>",
                                         SelectedCategory = existingPSList.Exists(x => x.MainCategoryId == c.Id) ? "<div class=\"drag t1\"><span id='" + c.Id + "'>" + c.Title + "</span></div>" : "",
                                         SelectedCid = existingPSList.Exists(x => x.MainCategoryId == c.Id) ? c.Id : 0
                                     };
                    lvAvailPs.DataSource = cFinalList;
                    lvAvailPs.DataBind();

                    //pre-fill the hidden field
                    StringBuilder ids = new StringBuilder();
                    foreach (var ps in existingPSList)
                    {
                        if (ids.Length == 0)
                            ids.Append(ps.MainCategoryId);
                        else
                            ids.Append("|" + ps.MainCategoryId);
                        int count = 0;
                        foreach (var id in ps.SubCategoryContentId)
                        {
                            if (count == 0)
                                ids.Append(";" + id);
                            else
                                ids.Append("," + id);
                            count++;
                        }
                        hddnPSIds.Value = ids.ToString();
                    }
                }
                else
                {
                    var cFinalList = from c in contentList
                                     select new
                                     {
                                         AvailableCategory = "<div class=\"drag t1\"><span id='" + c.Id + "'>" + c.Title + "</span></div>",
                                         SelectedCategory = ""
                                     };
                    lvAvailPs.DataSource = cFinalList;
                    lvAvailPs.DataBind();
                }

                StringBuilder sb = new StringBuilder();
                foreach (var c in contentList)
                {
                    XDocument xDoc = XDocument.Parse(c.Html);
                    if (xDoc.XPathSelectElements("/root/ViewPort/ViewPortContent") != null)
                    {
                        var subCategoriesNodes = xDoc.XPathSelectElements("/root/ViewPort/ViewPortContent");
                        foreach (var e in subCategoriesNodes)
                        {
                            string title = e.Attribute("datavalue_displayvalue").Value;
                            title = title.Remove(title.IndexOf("«HTML"));
                            sb.Append("<div id=\'" + e.Value + "\' class=\"drag t1 " + c.Id + "\"><span id=\'" + e.Value + "\'>" + title + "</span></div>");
                        }
                    }
                }
                ltrSubCat.Text = sb.ToString();
            }
        }
        catch (Exception ex)
        {
            Log.WriteError(ex);
        }
    }

    private List<long> GetIds(string[] ids)
    {
        List<long> contentIds = new List<long>();
        if (ids != null)
        {
            foreach (string s in ids)
            {
                long id;
                long.TryParse(s, out id);
                if (id > 0)
                    contentIds.Add(id);
            }
        }
        return contentIds;
    }
}