using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Kenrapid.CRM.Web.Appiume.Extensions
{
    public static class FilesystemExtensionMethods
    {
        public static string FileExtensionForContentType(this string fileName)
        {
            var pieces = fileName.Split('.');
            var extension = pieces.Length > 1 ? pieces[pieces.Length - 1]
                : string.Empty;
            return (extension.ToLower() == "jpg") ? "jpeg" : extension;
        }

        /*
        public static string GetPathForResizedImage(this string orgPath, int width = 0, int height = 0)
        {
            var fileInfo = new FileInfo(orgPath);
            string resizedPath = Path.Combine(fileInfo.DirectoryName, "resized", width + "x" + height,
                                              Path.GetFileName(orgPath));
            return resizedPath;
        }
        */

        public static string GetPathForResizedImage(this string orgPath, int width = 0, int height = 0, bool nobg = false)
        {
            var fileInfo = new FileInfo(orgPath);
            var folderName = width + "x" + height;
            if (nobg)
                folderName += "_nobg";
            string resizedPath = Path.Combine(fileInfo.DirectoryName, "resized", folderName,
                                              Path.GetFileName(orgPath));
            return resizedPath;
        }
    }
}