using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SirSpeedy.CMS;
using System.Web.Services;
using System.Text;
using Figleaf;

public partial class portfolio : PageBase
{
    private int defaultCount = ConfigHelper.GetValueInt("PortfolioListPageSize");
    private static string _fransId = FransDataManager.IsFranchiseSelected() ? FransDataManager.GetFranchiseId() : "0";

    /// <summary>
    /// Page init event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Init(object sender, EventArgs e)
    {
        cbPortfolioHeaderImage.DefaultContentID = ConfigHelper.GetValueLong("PortfolioDespContentId");
        cbPortfolioHeaderImage.CacheInterval = ConfigHelper.GetValueLong("smallCacheInterval");
        cbPortfolioHeaderImage.Fill();

        cbPotfolioSideContent.DefaultContentID = ConfigHelper.GetValueLong("PortfolioSideContentId");
        cbPotfolioSideContent.CacheInterval = ConfigHelper.GetValueLong("smallCacheInterval");
        cbPotfolioSideContent.Fill();

    }
    
    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadImages(defaultCount);
        }
    }

    /// <summary>
    /// Load more button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLoadMore_Click(object sender, EventArgs e)
    {
        int requestCount;
        int.TryParse(hdnDisplayCount.Value, out requestCount);
        requestCount = requestCount + defaultCount;
        this.LoadImages(requestCount);
    }

    [WebMethod]
    public static string GetData(string setId)
    {
        string photoHtml = string.Empty;
        if (setId != string.Empty)
        {
            photoHtml = PhotoHtml(setId);
        }
        return photoHtml;
    }
    
    /// <summary>
    /// helper method to get all set images
    /// </summary>
    /// <param name="count"></param>
    private void LoadImages(int reqCount)
    {
        var thirdPartyData = FransDataManager.GetFransThirdPartyData();
        if (thirdPartyData != null)
        {
            string userId = thirdPartyData.FlickrUserId;
            var photoCollections = FlickrManager.GetFlickrPhotoSets(userId, reqCount);
            lvPhotos.DataSource = photoCollections;
            lvPhotos.DataBind();

            hdnDisplayCount.Value = reqCount.ToString();
            if (photoCollections == null || photoCollections.Count <= reqCount)
                loadMorePhotos.Visible = false;
        }
    }
    

    /// <summary>
    /// method to get html for each photo set
    /// </summary>
    /// <param name="sId"></param>
    /// <returns></returns>
    private static string PhotoHtml(string sId)
    {
        string cacheKey = String.Format("Pip:portfolio:PhotoHtml:Sid={0}:FranchiseId={1}", sId, _fransId);
        string html = CacheBase.Get<string>(cacheKey);

        if (String.IsNullOrWhiteSpace(html))
        {
            //string html = string.Empty;

            var photoDataList = FlickrManager.GetFlickrPhotoSetsImages(sId);
            if (photoDataList != null && photoDataList.Any())
            {
                StringBuilder sbHtml = new StringBuilder();
                int i = -1;
                foreach (var photo in photoDataList)
                {
                    var photoData = FlickrManager.GetFlickrPhotoInfo(photo.PhotoId);
                    if (i >= 0)
                    {
                        sbHtml.Append("<div id=\"content-" + photoData.PhotoId + "\" class=\"cs_image_content\"><a class=\"fancybox\" data-content-id=\"content-" + photoData.PhotoId + "\" rel=\"group-" + sId + "\" href=\"" + photoData.LargeUrl + "\">" + photoData.Title + "</a>");
                        sbHtml.Append("<div class=\"cs_image_content_desc_lightbox\"><h3>" + photoData.Title + "</h3>&nbsp;<p>" + photoData.Description + "</p></div></div>");
                    }
                    i++;
                }
                //keep the first image to the last
                var firstImageData = photoDataList.FirstOrDefault();
                var firstPhotoData = FlickrManager.GetFlickrPhotoInfo(firstImageData.PhotoId);
                sbHtml.Append("<div id=\"content-" + firstPhotoData.PhotoId + "\" class=\"cs_image_content\"><a class=\"fancybox\" data-content-id=\"content-" + firstPhotoData.PhotoId + "\" rel=\"group-" + sId + "\" href=\"" + firstPhotoData.LargeUrl + "\">" + firstPhotoData.Title + "</a>");
                sbHtml.Append("<div class=\"cs_image_content_desc_lightbox\"><h3>" + firstPhotoData.Title + "</h3>&nbsp;<p>" + firstPhotoData.Description + "</p></div></div>");

                html = sbHtml.ToString();
                CacheBase.Put(cacheKey, html, CacheDuration.For24Hr);
            }
        }
        return html;
    }

    public string GetFirstImageOfAlbum(string sId)
    {
        string cacheKey = String.Format("Pip:portfolio:GetFirstImageOfAlbum:Sid={0}:FranchiseId={1}",
            sId, _fransId);

        string imagePath = CacheBase.Get<string>(cacheKey);
        if (String.IsNullOrWhiteSpace(imagePath))
        {
            // string imagePath = "";
            var photoDataList = FlickrManager.GetFlickrPhotoSetsImages(sId);
            if (photoDataList != null && photoDataList.Any())
            {
                var firstImageData = photoDataList.FirstOrDefault();
                if (firstImageData != null)
                {
                    imagePath = firstImageData.LargeUrl;
                }
            }
            CacheBase.Put(cacheKey, imagePath, CacheDuration.For24Hr);
        }
        return imagePath;
    }

    public string GetFirstImageTitleDesp(string sId)
    {
        string cacheKey = String.Format("Pip:portfolio:GetFirstImageTitleDesp:Sid={0}:FranchiseId={1}",
            sId, _fransId);
 
        string html = CacheBase.Get<string>(cacheKey);
        if (String.IsNullOrWhiteSpace(html))
        {
            // string html = string.Empty;
            var photoDataList = FlickrManager.GetFlickrPhotoSetsImages(sId);
            if (photoDataList != null && photoDataList.Any())
            {
                var firstImageData = photoDataList.FirstOrDefault();
                if (firstImageData != null)
                {
                    var firstPhotoData = FlickrManager.GetFlickrPhotoInfo(firstImageData.PhotoId);
                    html = "<div class=\"cs_image_content_desc_lightbox\"><h3>" + firstPhotoData.Title + "</h3>&nbsp;<p>" + firstPhotoData.Description + "</p></div>";
                }
            }
            CacheBase.Put(cacheKey, html, CacheDuration.For24Hr);
        }
        return html;
    }
}