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
    /// <summary>
    /// 
    /// </summary>
    public class ImagePresentingFixedFrameResult : FilePathResult
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly string _filePath;

        /// <summary>
        /// 
        /// </summary>
        private readonly int _width;

        /// <summary>
        /// 
        /// </summary>
        private readonly int _height;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public ImagePresentingFixedFrameResult(string filePath, int width = 0, int height = 0) :
                base(filePath, string.Format("image/{0}",
                    filePath.FileExtensionForContentType()))
        {
            _filePath = filePath;
            _width = width;
            _height = height;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        protected override void WriteFile(HttpResponseBase response)
        {
            string resizedFilePath = GetResizedImagePath(_filePath, _width, _height);
            response.SetDefaultImageHeaders(resizedFilePath);
            WriteFileToResponse(resizedFilePath, response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="response"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private static string GetResizedImagePath(string filepath, int width, int height)
        {
            string resizedPath = filepath;

            if (width > 0 || height > 0)
            {
                resizedPath = filepath.GetPathForResizedImageFixedFrame(width, height);

                if (!Directory.Exists(resizedPath))
                    Directory.CreateDirectory(new FileInfo(resizedPath).DirectoryName);

                if (!File.Exists(resizedPath))
                {
                    using (var src = new Bitmap(filepath))
                    using (var bmp = new Bitmap(width, height))
                    {
                        using (var gr = Graphics.FromImage(bmp))
                        {
                            var scaled = ScaleImage(src, width - 20, height - 20);
                            var drawPoint = new Point(Convert.ToInt32(bmp.Width / 2 - scaled.Width / 2), Convert.ToInt32(bmp.Height / 2 - scaled.Height / 2));
                            var drawSize = new Size(scaled.Width, scaled.Height);
                            gr.DrawImage(scaled, new Rectangle(drawPoint, drawSize));

                            if (
                                Convert.ToBoolean(
                                    System.Configuration.ConfigurationManager.AppSettings["crm:WaterMarkOnPic"]))
                            {
                                var waterMarkText = "MK HANDICRAFTS CO., LTD";
                                var drawBrush = new SolidBrush(Color.WhiteSmoke);
                                var drawFont = new Font("Arial", 17, FontStyle.Bold);
                                var textSize = gr.MeasureString(waterMarkText, drawFont);

                                gr.DrawString(waterMarkText, drawFont, drawBrush, new RectangleF(Convert.ToInt32(bmp.Width / 2 - textSize.Width / 2), Convert.ToInt32(bmp.Height / 2 - textSize.Height / 2), bmp.Width, bmp.Height));

                                waterMarkText = "www.mkhandicrafts.com";
                                drawFont = new Font("Arial", 15, FontStyle.Regular);
                                textSize = gr.MeasureString(waterMarkText, drawFont);
                                gr.DrawString(waterMarkText, drawFont, drawBrush, new RectangleF(Convert.ToInt32(bmp.Width / 2 - textSize.Width / 2), Convert.ToInt32(bmp.Height / 2 - textSize.Height / 2) + 70, bmp.Width, bmp.Height));

                            }

                            bmp.Save(resizedPath, ImageFormat.Png);
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