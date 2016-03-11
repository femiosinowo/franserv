using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Ektron.Cms.Common;
using Ektron.Cms.CookieEncryption;
using Ektron.Cms;
using Ektron.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using Ektron.ASM.AssetConfig;
using System.IO;

public class BlobRedirectModule : IHttpModule
{
    private long userId;
    #region IHttpModule Members

    public void Dispose()
    {
        //throw new NotImplementedException();
    }

    public void Init(HttpApplication context)
    {
        context.BeginRequest += new EventHandler(context_BeginRequest);
    }

    void context_BeginRequest(object sender, EventArgs e)
    {
        string contextUrl = HttpContext.Current.Request.Url.LocalPath.ToLower();
        HttpContext.Current.Items.Add("Url", contextUrl);
        if (contextUrl.Contains("/privateassets/") || contextUrl.Contains(@"\privateassets\") || contextUrl.StartsWith("privateassets/") || contextUrl.StartsWith(@"privateassets\"))
        {
            AuthenticateUser();
            if (UserHasPerms(Path.GetFileNameWithoutExtension(HttpContext.Current.Request.PhysicalPath)))
            {
                string extension = GetExtensionFromName(contextUrl);
                string contentType = "application/" + extension.ToLower();
                switch (extension.ToLower())
                {
                    case "gif": contentType = "image/gif"; break;
                    case "jpg":
                    case "jpeg":
                    case "jpe": contentType = "image/jpeg"; break;
                    case "bmp": contentType = "image/bmp"; break;
                    case "tif":
                    case "tiff": contentType = "image/tiff"; break;
                    case "eps": contentType = "application/postscript"; break;
                    default:
                        HttpContext.Current.Response.AppendHeader(
                           "Content-disposition",
                           "attachment; filename=content." + extension);
                        break;
                }
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = contentType;
                HttpContext.Current.Response.BinaryWrite(BlobOrCdnUrlMapper.GetBlob(BlobOrCdnUrlMapper.Value + contextUrl.TrimStart('/')));
                HttpContext.Current.Response.End();//otherwise 404 error
            }
            else
            {
                throw new UnauthorizedAccessException("You do not have access to the requested resource only users with permissions can access private resources");
            }
        }
        else if (contextUrl.StartsWith("uploadedimages/") ||
            contextUrl.StartsWith(@"uploadedimages\") ||
            contextUrl.Contains(@"\uploadedimages\") ||
            contextUrl.Contains("/uploadedimages/") ||
            contextUrl.StartsWith("uploadedfiles/") ||
            contextUrl.StartsWith(@"uploadedfiles\") ||
            contextUrl.Contains(@"\uploadedfiles\") ||
            contextUrl.Contains("/uploadedfiles/") ||
            contextUrl.StartsWith("assets/") ||
            contextUrl.StartsWith(@"assets\") ||
            contextUrl.Contains(@"\assets\") ||
            contextUrl.Contains("/assets/"))
        {
            string blobUrl = BlobOrCdnUrlMapper.Value + contextUrl.TrimStart('/');
            if (!String.IsNullOrEmpty(BlobOrCdnUrlMapper.Value))
            {
                HttpContext.Current.Response.RedirectPermanent(blobUrl, true);
            }
        }
    }
    public static string GetExtensionFromName(string fileName)
    {
        string ext = "";
        if (fileName != "")
        {
            int indexOfDot = fileName.LastIndexOf('.');
            ext = fileName.Substring(indexOfDot + 1);
        }
        return ext;
    }
    #endregion
    private void AuthenticateUser()
    {
        string cookieName = "ecm";
        if (EkFunctions.IsContextValid() && HttpContext.Current.Request.Cookies.Get(cookieName) != null)
        {
            HttpCookie ektCookie = HttpContext.Current.Request.Cookies.Get(cookieName);

            if (ektCookie["user_id"] == null)
                ektCookie = SecureCookie.Decode(ektCookie);

            userId = long.Parse(ektCookie["user_id"]);
            if (userId == 0)
            {
                throw new UnauthorizedAccessException("You donot have access to the requested resource only users with permissions can access private resources");
            }

            string siteId = ektCookie["site_id"];
            if (siteId == "")
            {
                throw new UnauthorizedAccessException("You donot have access to the requested resource only users with permissions  can access private resources");
            }

            int uniqueId = int.Parse(ektCookie["unique_id"]);
            if (uniqueId == 0)
            {
                throw new UnauthorizedAccessException("You donot have access to the requested resource only users with permissions  can access private resources");
            }
        }
    }
    private bool UserHasPerms(string assetID)
    {
        if ((userId == 999999999) || (userId == 18611864) || (userId == 1))
            return true;

        int retVal = 0;
        Database db = DatabaseFactory.CreateDatabase();

        DbCommand cmd = db.GetStoredProcCommand(DatabaseManager.SPCheckPermissionsForContent);
        db.AddInParameter(cmd, "inAssetID", DbType.String, assetID);
        db.AddInParameter(cmd, "inUserID", DbType.Int64, userId);
        db.AddOutParameter(cmd, "retVal", DbType.Int32, retVal);
        db.ExecuteNonQuery(cmd);

        retVal = (int)db.GetParameterValue(cmd, "retVal");
        return retVal != 0;
    }
}
internal static class BlobOrCdnUrlMapper
{
    private static readonly string _roleCdnOrBlobValue = string.Empty;
    static BlobOrCdnUrlMapper()
    {
        if (string.IsNullOrEmpty(_roleCdnOrBlobValue))
        {
            _roleCdnOrBlobValue = (string)ConfigurationManager.AppSettings["BlobOrCdnUrl"];
        }
    }
    internal static string Value
    {
        get { return _roleCdnOrBlobValue; }
    }
    internal static byte[] GetBlob(string path)
    {
        return Ektron.Storage.StorageClient.Context.File.DownloadByteArray(path);
    }
}