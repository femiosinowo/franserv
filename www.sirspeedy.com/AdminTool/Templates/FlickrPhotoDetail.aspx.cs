using SirSpeedy.CMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminTool_Templates_FlickrPhotoDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString.HasKeys())
        {
            string userId = Request.QueryString["userid"];
            string photoSetId = Request.QueryString["id"];

            if ((!string.IsNullOrEmpty(userId)) && (!string.IsNullOrEmpty(photoSetId)))
            {
                var photoSets = FlickrManager.GetFlickrPhotoSets(userId, 1);
                var photos = FlickrManager.GetFlickrPhotoSetsImages(photoSetId);
                if (photos != null && photos.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var pic in photos)
                    {
                        sb.Append("<img src=\"" + pic.LargeUrl + "\" />");
                    }
                    photoSetImages.Text = sb.ToString();
                }
            }
        }
    }
}