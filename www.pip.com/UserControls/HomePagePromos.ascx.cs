using System;
using System.Collections.Generic;
using System.Linq;
using SirSpeedy.CMS;
using System.Text;
using Ektron.Cms;

public partial class UserControls_HomePagePromos : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.GetLocalPromoImages();
            GetFranchiseAddress();
        }
    }

    private void GetLocalPromoImages()
    {
        var centerId = FransDataManager.GetFranchiseId();      
        var selectedPromoItemIds = FransDataManager.GetSelectedPromos(centerId);
        bool showPromo = false;
        if (selectedPromoItemIds != null && selectedPromoItemIds.Any())
        {
            var pData = selectedPromoItemIds.FirstOrDefault();
            centerPromosList.Visible = showPromo = true;
            //turing off the promo expiration
            //if (pData.ExpireDate != null && pData.ExpireDate != DateTime.MinValue && pData.ExpireDate.Year != 2000)
            //{
            //    if (pData.ExpireDate > DateTime.Now)
            //        centerPromosList.Visible = showPromo =true;
            //}
            //else
            //{
            //    centerPromosList.Visible = showPromo =true;
            //}

            if (showPromo)
            {
                StringBuilder sb = new StringBuilder();
                List<TaxonomyItemData> promoLargeImages = GetLargeImages();
                List<TaxonomyItemData> promoSmallImages = GetSmallImages();               

                for (int i = 0; i < pData.SelectedPromoIds.Count; i++)
                {                   
                    var imageItemData = promoLargeImages.Where(x => x.ItemId == pData.SelectedPromoIds[i]).FirstOrDefault();
                    if (imageItemData == null)
                        imageItemData = promoSmallImages.Where(x => x.ItemId == pData.SelectedPromoIds[i]).FirstOrDefault();

                    if (imageItemData != null)
                    {
                        string image1Link = pData.PromoImage1Link != null && pData.PromoImage1Link != string.Empty ? pData.PromoImage1Link : "#";
                        string image2Link = pData.PromoImage2Link != null && pData.PromoImage2Link != string.Empty ? pData.PromoImage2Link : "#";
                        if (i == 0)
                        {
                            ltrPromoImage1.Text = "<a target=\"_blank\" href=\"" + image1Link + "\"><img src=\"" + imageItemData.FilePath + "\" alt=\"" + imageItemData.Title + "\"/></a>";
                            promo_1.Visible = true;
                        }
                        else
                        {
                            ltrPromoImage2.Text = "<a target=\"_blank\" href=\"" + image2Link + "\"><img src=\"" + imageItemData.FilePath + "\" alt=\"" + imageItemData.Title + "\"/></a>";
                            promo_2.Visible = true;
                        }
                    }

                    if (pData.SelectedPromoIds.Count == 1)
                    {
                        promo_1.Attributes.Add("class", "grid_12 large_promo");
                        largePromoSecondCTA.Visible = true;
                    }
                }
                              
                var whyWeAreDiff = FransDataManager.GetWhyWeAreDiffContent(centerId);               
                if (whyWeAreDiff != null)
                {
                    ltrDecription.Text = whyWeAreDiff.ContentTagLine;
                }
            }
        }
    }

    private List<TaxonomyItemData> GetLargeImages()
    {
        List<TaxonomyItemData> taxItems = new List<TaxonomyItemData>();
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
        List<TaxonomyItemData> taxItems = new List<TaxonomyItemData>();
        long smallImgTaxId = ConfigHelper.GetValueLong("PromotionSmallImageTaxId");
        var taxTreeData = TaxonomyHelper.GetTaxonomyTree(smallImgTaxId, 1, true);
        if (taxTreeData != null && taxTreeData.TaxonomyItems.Length > 0)
        {
            taxItems = taxTreeData.TaxonomyItems.ToList();
        }
        return taxItems;
    }

    /// <summary>
    /// Gets the Franchise Data
    /// </summary>
    private void GetFranchiseAddress()
    {
        var fd = FransDataManager.GetFransData();
        if (fd != null)
        {
            string address2 = fd.Address2 != null && fd.Address2 != string.Empty ? fd.Address2 + "<br/>" : "";
            ltrAddress1.Text = fd.Address1 + " " + address2;
            ltrAddress2.Text = fd.City + ",  " + fd.State + " " + fd.Zipcode;
        }
    }
}