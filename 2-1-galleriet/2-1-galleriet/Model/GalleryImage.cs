using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace _2_1_galleriet.Model
{
    public class GalleryImage
    {
        public string Name { get; set; }
        
        private static readonly string _imgPath;
        private static readonly string _thumbPath;

        public string ImgPath { get { return string.Format("{0}{1}", _imgPath, Name); } }
        public string FullPath { get; set; }

        public string ThumbImgPath { get { return string.Format("{0}{1}", _thumbPath, Name); } }
        public string ThumbFullPath { get { return Path.GetDirectoryName(FullPath) + "\\thumb\\" + Name; } }

        static GalleryImage()
        {
            _imgPath = "Content/img/";
            _thumbPath = "Content/img/thumb/";
        }

    }
}