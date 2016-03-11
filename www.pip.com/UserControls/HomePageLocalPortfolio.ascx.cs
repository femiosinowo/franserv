using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SirSpeedy.CMS;
using System.Text;
using System.Web.Services;

public partial class UserControls_HomePageLocalPortfolio : System.Web.UI.UserControl
{
    private int defaultCount = ConfigHelper.GetValueInt("PortfolioListPageSize");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.LoadImages(defaultCount);
        }
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
            rptPortfolio.DataSource = photoCollections;
            rptPortfolio.DataBind();

            //hdnDisplayCount.Value = reqCount.ToString();
            //if (photoCollections == null || photoCollections.Count <= reqCount)
            //    loadMorePhotos.Visible = false;
        }
    }


    /// <summary>
    /// method to get html for each photo set
    /// </summary>
    /// <param name="sId"></param>
    /// <returns></returns>
    private static string PhotoHtml(string sId)
    {
        string html = string.Empty;

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
        }
        return html;
    }    

    public string FormatUrl(object setId)
    {
        string url = "/portfolio/";
        if (setId != null)
        {
            url = "/portfolio/?setid=" + setId;
            if (FransDataManager.IsFranchiseSelected())
            {
                string fransId = FransDataManager.GetFranchiseId();
                url = "/" + fransId + url;
            }
        }
        return url;
    }

    public string GetFirstImageTitleDesp(string sId)
    {
        string html = string.Empty;
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
        return html;
    }
}