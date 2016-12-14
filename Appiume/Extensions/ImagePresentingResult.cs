using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Simple.ImageResizer;
using Simple.ImageResizer.MvcExtensions;

namespace Kenrapid.CRM.Web.Appiume.Extensions
{
    public class ImagePresentingResult : FilePathResult
    {
        private readonly string _filePath;
        private readonly int _width;
        private readonly int _height;

        public ImagePresentingResult(string filePath, int width = 0, int height = 0) :
                base(filePath, string.Format("image/{0}",
                    filePath.FileExtensionForContentType()))
        {
            _filePath = filePath;
            _width = width;
            _height = height;
        }

        protected override void WriteFile(HttpResponseBase response)
        {
            string resizedFilePath = GetResizedImagePath(_filePath, _width, _height);
            response.SetDefaultImageHeaders(resizedFilePath);
            WriteFileToResponse(resizedFilePath, response);
        }

        private static void WriteFileToResponse(string filePath, HttpResponseBase response)
        {
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                const int bufferLength = 65536;
                var buffer = new byte[bufferLength];

                while (true)
                {
                    int bytesRead = fs.Read(buffer, 0, bufferLength);

                    if (bytesRead == 0)
                    {
                        break;
                    }

                    response.OutputStream.Write(buffer, 0, bytesRead);
                }
            }
        }

        private static string GetResizedImagePath(string filepath, int width, int height)
        {
            string resizedPath = filepath;

            if (width > 0 || height > 0)
            {
                resizedPath = filepath.GetPathForResizedImage(width, height);

                if (!Directory.Exists(resizedPath))
                    Directory.CreateDirectory(new FileInfo(resizedPath).DirectoryName);

                if (!File.Exists(resizedPath))
                {
                    using (var src = new Bitmap(filepath))
                    using (var bmp = new Bitmap(HostingEnvironment.MapPath("~/Content/data/images/bg.jpg")))
                    {
                        var scaledBmp = ScaleImage(bmp, width, height);
                        using (var gr = Graphics.FromImage(scaledBmp))
                        {
                            var scaled = ScaleImage(src, width - 20, height - 20);
                            var drawPoint = new Point(Convert.ToInt32(scaledBmp.Width / 2 - scaled.Width / 2), Convert.ToInt32(scaledBmp.Height / 2 - scaled.Height / 2));
                            var drawSize = new Size(scaled.Width, scaled.Height);

                            gr.DrawImage(scaled, new Rectangle(drawPoint, drawSize));
                            scaledBmp.Save(resizedPath, ImageFormat.Png);
                        }
                    }

                    /*
                    var imageResizer = new ImageResizer(filepath);
                    if (width > 0 && height > 0)
                    {
                        imageResizer.Resize(width, height, ImageEncoding.Jpg90);
                    }
                    else if (width > 0)
                    {
                        imageResizer.Resize(width, ImageEncoding.Jpg90);
                    }
                    imageResizer.SaveToFile(resizedPath);
                    imageResizer.Dispose();
                    */
                }
            }
            return resizedPath;
        }

        /// <summary>
        /// Scales an image proportionally.  Returns a bitmap.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        /// <returns></returns>
        public static Bitmap ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
            Bitmap bmp = new Bitmap(newImage);

            return bmp;
        }
    }
}