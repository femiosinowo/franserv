using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.IO.Compression;

namespace TeamLogic.CMS
{
    ///<summary>
    /// Summary description for ViewStateCompressor
    ///</summary>
    public class ViewStateCompressor
    {
        public ViewStateCompressor()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static byte[] CompressViewState(byte[] uncompData)
        {
            using (MemoryStream mem = new MemoryStream())
            {
                CompressionMode mode = CompressionMode.Compress;
                // Use the newly created memory stream for the compressed data.
                using (GZipStream gzip = new GZipStream(mem, mode, true))
                {
                    //Writes compressed byte to the underlying
                    //stream from the specified byte array.
                    gzip.Write(uncompData, 0, uncompData.Length);
                }
                return mem.ToArray();
            }
        }

        public static byte[] DecompressViewState(byte[] compData)
        {
            GZipStream gzip;
            using (MemoryStream inputMem = new MemoryStream())
            {
                inputMem.Write(compData, 0, compData.Length);
                // Reset the memory stream position to begin decompression.
                inputMem.Position = 0;
                CompressionMode mode = CompressionMode.Decompress;
                gzip = new GZipStream(inputMem, mode, true);

                using (MemoryStream outputMem = new MemoryStream())
                {
                    // Read 1024 bytes at a time
                    byte[] buf = new byte[1024];
                    int byteRead = -1;
                    byteRead = gzip.Read(buf, 0, buf.Length);
                    while (byteRead > 0)
                    {
                        //write to memory stream
                        outputMem.Write(buf, 0, byteRead);
                        byteRead = gzip.Read(buf, 0, buf.Length);
                    }
                    gzip.Close();
                    return outputMem.ToArray();
                }
            }
        }
    }
}