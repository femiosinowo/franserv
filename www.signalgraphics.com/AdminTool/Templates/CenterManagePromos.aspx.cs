using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SignalGraphics.CMS;
using Ektron.Cms;
using Ektron.Cms.Instrumentation;

public partial class AdminTool_Templates_CenterManagePromos : System.Web.UI.Page
{    
    string centerId;
    UserAPI userApi = new UserAPI();
    List<TaxonomyItemData> promoLargeImages = new List<TaxonomyItemData>();
    List<TaxonomyItemData> promoSmallImages = new List<TaxonomyItemData>();

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
                        this.FillPromotions(centerId);
                        hdnCenterId.Value = centerId;
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
        
    protected void btnPromotional_Click(object sender, EventArgs e)
    {
        if(Page.IsValid)
        {
            string centerId = hdnCenterId.Value;
            DateTime expireDate = DateTime.MinValue;
            if (promoExpireDate.Value == DateTime.MinValue)
                expireDate = expireDate.AddYears(1999);
            else
                expireDate = promoExpireDate.Value;

            var status = AdminToolManager.UpdateCenterPromotions(centerId, "", hdnSelectedPromos.Value, txtImage1Link.Text, txtImage2Link.Text, expireDate);
            if(status > 0)
            {
                this.FillPromotions(centerId);
                //clear cache
                FransDataManager.GetSelectedPromos(centerId, true);
                promoUpdateMsg.Visible = true;
                lblError.Visible = false;
            }
            else
            {
                promoUpdateMsg.Visible = false;
                lblError.Visible = true;
            }
        }
    }

    private void FillPromotions(string centerId)
    {
        promoLargeImages = GetLargeImages();
        promoSmallImages = GetSmallImages();

        var assignedPromotionList = AdminToolManager.GetAssignedCenterPromotions(centerId);
        if (assignedPromotionList != null && assignedPromotionList.Any())
        {
            var selectedPromoList = AdminToolManager.GetSelectedCenterPromotions(centerId);
            if (selectedPromoList != null && selectedPromoList.Any())
            {
                string ids = "";
                foreach (var p in selectedPromoList)
                    ids += p + ",";
                hdnSelectedPromos.Value = ids;

                List<long> sortedPromos = new List<long>();
                sortedPromos.AddRange(selectedPromoList);
                //add non-selected items after selected items
                foreach (var b in assignedPromotionList)
                {
                    if (!sortedPromos.Contains(b))
                        sortedPromos.Add(b);
                }

                var contentList = from c in sortedPromos
                                  select new
                                  {
                                      AvailablebPromo = selectedPromoList.Exists(x => x == c) ? "" : "<div class=\"drag t1\"><span  id='" + c + "'>" + GetItemHtml(c) + "</span></div>",
                                      SelectedPromo = selectedPromoList.Exists(x => x == c) ? "<div class=\"drag t1\"><span id='" + c + "'>" + GetItemHtml(c) + "</span></div>" : ""
                                  };
                lvPromotionals.DataSource = contentList;
                lvPromotionals.DataBind();

                var pData = AdminToolManager.GetPromotionData(centerId);
                if(pData != null)
                {
                    txtImage1Link.Text = pData.PromoImage1Link;
                    txtImage2Link.Text = pData.PromoImage2Link;
                    if (pData.ExpireDate != null && pData.ExpireDate != DateTime.MinValue && pData.ExpireDate.Year != 2000)
                        promoExpireDate.Value = pData.ExpireDate;
                }
            }
            else
            {
                var contentList = from c in assignedPromotionList
                                  select new
                                  {
                                      AvailablebPromo = "<div class=\"drag t1\"><span id='" + c + "'>" + GetItemHtml(c) + "</span></div>",
                                      SelectedPromo = ""
                                  };
                lvPromotionals.DataSource = contentList;
                lvPromotionals.DataBind();
            }
        }
        else
        {
            pnlPromotions.Visible = false;
            pnlNoPromoResults.Visible = true;
        }
    }
        
    private string GetItemHtml(long contentId)
    {
        string html = "";
        if (contentId > 0)
        {           
            TaxonomyItemData tData = null;
            string imageType = "LargeImage";
            string displayImgType = "Large Image";
            long largeImgTaxId = ConfigHelper.GetValueLong("PromotionLargeImageTaxId");
            long smallImgTaxId = ConfigHelper.GetValueLong("PromotionSmallImageTaxId");
            string domain = "http://" + Request.ServerVariables["server_name"];

            tData = promoLargeImages.Where(x => x.ItemId == contentId).FirstOrDefault();
            if(tData == null)
                tData = promoSmallImages.Where(x => x.ItemId == contentId).FirstOrDefault();

            if (tData != null && tData.Id == contentId)
            {
                if (tData.TaxonomyId == largeImgTaxId)
                {
                    imageType = "LargeImage";
                    displayImgType = "Large Image";
                }
                else if (tData.TaxonomyId == smallImgTaxId)
                {
                    imageType = "SmallImage";
                    displayImgType = "Small Image";
                }


                html = "<span>" + tData.Title + " <a target=\"_blank\" Title=\"" + imageType + "\" class=\"" + imageType + "\" href=\"" + domain + tData.FilePath + "\"><br/>(View " + displayImgType + ")</a></span>";
            }
        }
        return html;
    }

    private List<TaxonomyItemData> GetLargeImages()
    {
        List<TaxonomyItemData> taxItems = null;
        long largeImgTaxId = ConfigHelper.GetValueLong("PromotionLargeImageTaxId");
        var taxTreeData = TaxonomyHelper.GetTaxonomyTree(largeImgTaxId, 1, true);
        if (taxTreeData != null && taxTreeData.TaxonomyItems.Length > 0)
        {
            taxItems = taxTreeData.TaxonomyItems.ToList();
        }
        return taxItems;
    }

    private List<TaxonomyItemData> GetSmallImages()
    {
        List<TaxonomyItemData> taxItems = null;
        long smallImgTaxId = ConfigHelper.GetValueLong("PromotionSmallImageTaxId");
        var taxTreeData = TaxonomyHelper.GetTaxonomyTree(smallImgTaxId, 1, true);
        if (taxTreeData != null && taxTreeData.TaxonomyItems.Length > 0)
        {
            taxItems = taxTreeData.TaxonomyItems.ToList();
        }
        return taxItems;
    }

}