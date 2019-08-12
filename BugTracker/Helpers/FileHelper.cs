﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace BugTracker.Helpers
{
    public class FileHelper
    {
        public static bool IsWebFriendlyImage(HttpPostedFileBase file)
        {
            if (file == null)
                return false;
            if (file.ContentLength > 2 * 1024 * 1024 || file.ContentLength < 1024)
                return false;
            try
            {
                using (var img = Image.FromStream(file.InputStream))
                {
                    return ImageFormat.Jpeg.Equals(img.RawFormat) ||
                        ImageFormat.Png.Equals(img.RawFormat) ||
                        ImageFormat.Gif.Equals(img.RawFormat) ||
                        ImageFormat.Bmp.Equals(img.RawFormat) ||
                        ImageFormat.Icon.Equals(img.RawFormat) ||
                        ImageFormat.Tiff.Equals(img.RawFormat);
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidAttachment(HttpPostedFileBase file)
        {
            try
            {
                if (file == null)
                    return false;

                if (file.ContentLength > 5 * 1024 * 1024 || file.ContentLength < 1024)
                    return false;

                var extValid = false;
                foreach (var ext in WebConfigurationManager.AppSettings["AllowedAttachmentExtensions"].Split(','))
                {
                    if (Path.GetFileName(file.FileName) == ext)
                    {
                        extValid = true;
                        break;
                    }
                }
                return IsWebFriendlyImage(file) || extValid;
            }
            catch
            {
                return false;
            }

        }

        public static string GetIconPath(string filePath)
        {
            switch (Path.GetExtension(filePath))
            {
                case ".png":
                case ".bmp":
                case ".tif":
                case ".icon":
                case ".jpg":
                case ".jpeg":
                    return filePath;
                case ".pdf":
                    return "/Img/pdf.png";
                case ".doc":
                    return "/Img/doc.png";
                case ".docx":
                    return "/Img/docx.png";
                case ".xls":
                    return "/Img/xls.png";
                case ".xlsx":
                    return "/Img/xlsx.png";
                case ".zip":
                    return "/Img/zip.png";
                default:
                    return "/Img/other.png";
            }
        }
    }
}
