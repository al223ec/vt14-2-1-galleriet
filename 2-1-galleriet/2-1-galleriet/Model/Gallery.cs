using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;

namespace _2_1_galleriet.Model
{
    public class Gallery
    {
        private static readonly Regex ApprovedExtensions, SanitizePath;
        private static readonly string PhysicalUploadedImagesPath;

        private IEnumerable<GalleryImage> _images;
        public IEnumerable<GalleryImage> Images { get { return _images ?? (_images = GetImages()); } }

        static Gallery()
        {
            ApprovedExtensions = new Regex("(.jpg|.gif|.png)", RegexOptions.IgnoreCase);
            var invalidChars = new string(Path.GetInvalidFileNameChars());
            SanitizePath = new Regex(string.Format("[{0}]", Regex.Escape(invalidChars)));
            PhysicalUploadedImagesPath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), @"Content\img");
        }

        public void CreateThumbNails()
        {
            foreach (var item in Images)
            {
                if (!File.Exists(item.ThumbImgPath))
                {
                    var image = System.Drawing.Image.FromFile(item.FullPath);
                    var thumbnail = image.GetThumbnailImage(60, 45, null, System.IntPtr.Zero);
                    thumbnail.Save(item.ThumbImgPath);
                }
            }
        }


        private IEnumerable<GalleryImage> GetImages()
        {
            var di = new DirectoryInfo(PhysicalUploadedImagesPath);

            return (from fi in di.GetFiles()
                    select new GalleryImage
                    {
                        Name = fi.Name,
                        ImgPath = string.Format("Content/img/{0}", fi.Name),
                        FullPath = fi.FullName,
                        ThumbImgPath = fi.DirectoryName + "\\thumb\\" + fi.Name,
                    }).AsEnumerable();
        }

        private IEnumerable<GalleryImage> GetImagesAsDictionary()
        {
            var di = Directory.GetFiles(PhysicalUploadedImagesPath);

            var dict = new Dictionary<string, GalleryImage>();
            foreach (var info in di)
            {
                dict.Add(info, new GalleryImage
                {
                    Name = info,
                });
            }
            return dict as IEnumerable<GalleryImage>;
        }

        public string SaveImage(Stream stream, string filename)
        {
            throw new NotImplementedException();
        }
        public GalleryImage GetImage(string name)
        {
            throw new NotImplementedException();
        }
        public static bool ImageExists(string name)
        {
            throw new NotImplementedException();
        }
    }
}