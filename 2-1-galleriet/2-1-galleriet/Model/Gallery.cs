using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;

namespace _2_1_galleriet.Model
{
    public class Gallery
    {
        private static readonly Regex ApprovedExtensions, SanitizePath;
        private static readonly string  PhysicalUploadedImagesPath;

        static Gallery()
        {
            ApprovedExtensions = new Regex("(.jpg|.gif|.png)", RegexOptions.IgnoreCase);

            var invalidChars = new string(Path.GetInvalidFileNameChars());
            SanitizePath = new Regex(string.Format("[{0}]", Regex.Escape(invalidChars)));

            PhysicalUploadedImagesPath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), @"Content\img"); 
        }


    }
}