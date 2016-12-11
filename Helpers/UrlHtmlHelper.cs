using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kenrapid.CRM.Web.Helpers
{
    public static class UrlHtmlHelper
    {
        public static IHtmlString GenBase64Of(string fileName)
        {
            var bytes = System.IO.File.ReadAllBytes(fileName);
            return MvcHtmlString.Create(Convert.ToBase64String(bytes));
        }
        public static IHtmlString GenBase64Of(this HtmlHelper helper, string fileName)
        {
            var bytes = System.IO.File.ReadAllBytes(fileName);
            return MvcHtmlString.Create(CreateBase64Image(bytes));
        }

        private static string CreateBase64Image(byte[] fileBytes)
        {
            Image streamImage;
            /* Ensure we've streamed the document out correctly before we commit to the conversion */
            using (MemoryStream ms = new MemoryStream(fileBytes))
            {
                /* Create a new image, saved as a scaled version of the original */
                // streamImage = ScaleImage(Image.FromStream(ms));
                streamImage = Image.FromStream(ms);
            }
            using (MemoryStream ms = new MemoryStream())
            {
                /* Convert this image back to a base64 string */
                streamImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public static string GetUrl(string imagepath)
        {
            var host = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "");

            return host + imagepath;
        }
    }
}