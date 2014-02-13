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

        public Dictionary<string, GalleryImage> DictImages { get; set; }

        static Gallery()
        {
            ApprovedExtensions = new Regex("(.jpg|.gif|.png)", RegexOptions.IgnoreCase);
            var invalidChars = new string(Path.GetInvalidFileNameChars());
            SanitizePath = new Regex(string.Format("[{0}]", Regex.Escape(invalidChars)));
            PhysicalUploadedImagesPath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), @"Content\img");
        }

        public Gallery()
        {
            DictImages = GetImagesAsDictionary();
        }

        public void CreateThumbNails()
        {
            foreach (var item in DictImages.Values)
            {
                if (!File.Exists(item.ThumbFullPath))
                {
                    var image = System.Drawing.Image.FromFile(item.FullPath);
                    var thumbnail = image.GetThumbnailImage(60, 45, null, System.IntPtr.Zero);
                    thumbnail.Save(item.ThumbFullPath);
                }
            }
        }

        private Dictionary<string, GalleryImage> GetImagesAsDictionary()
        {
            var di = new DirectoryInfo(PhysicalUploadedImagesPath);
            var dict = new Dictionary<string, GalleryImage>();

            foreach (var info in di.GetFiles())
            {
                dict.Add(info.Name, new GalleryImage
                {
                    Name = info.Name,
                    FullPath = info.FullName,
                });
            }
            return dict;
        }

        public void SaveImage(Stream stream, string filename)
        {
            var newImage = Image.FromStream(stream);
            var path = Path.Combine(PhysicalUploadedImagesPath, filename);
            var numOfExistingImages = 1;

            while (File.Exists(path))
            {
                path = Path.Combine(PhysicalUploadedImagesPath, string.Format("{0}{1}{2}", Path.GetFileNameWithoutExtension(path), numOfExistingImages.ToString(), Path.GetExtension(path)));
                numOfExistingImages++;
            }
            newImage.Save(path);

            var file = new FileInfo(path);
            DictImages.Add(
                file.Name, new GalleryImage
                {
                    Name = file.Name,
                    FullPath = file.FullName,
                });
        }
        public void AddImage()
        {
            //DictImages.Add();
        }
        public GalleryImage GetImage(string name)
        {
            throw new NotImplementedException();
        }
        public void DeleteImage(string name)
        {
            DictImages.Remove(name);
        }
        public bool ImageExists(string name)
        {
            //return File.Exists(Path.Combine(PhysicalUploadedImagesPath, name));
            return DictImages.ContainsKey(name);
        }

        //foreach (var item in Images)
        //{
        //    if (!File.Exists(item.ThumbImgFullPath))
        //    {
        //        var image = System.Drawing.Image.FromFile(item.FullPath);
        //        var thumbnail = image.GetThumbnailImage(60, 45, null, System.IntPtr.Zero);
        //        thumbnail.Save(item.ThumbImgFullPath);
        //    } 
        //}

        //foreach (KeyValuePair<string, GalleryImage> entry in DictImages)
        //{
        //    if (!File.Exists(entry.Value.ThumbImgFullPath))
        //    {
        //        var image = System.Drawing.Image.FromFile(entry.Value.FullPath);
        //        var thumbnail = image.GetThumbnailImage(60, 45, null, System.IntPtr.Zero);
        //        thumbnail.Save(entry.Value.ThumbImgFullPath);
        //    }
        //    // do something with entry.Value or entry.Key
        //}

        //private IEnumerable<GalleryImage> _images;
        //public IEnumerable<GalleryImage> Images { get { return _images ?? (_images = GetImages()); } }
        //private IEnumerable<GalleryImage> GetImages()
        //{
        //    var di = new DirectoryInfo(PhysicalUploadedImagesPath);

        //    return (from fi in di.GetFiles()
        //            select new GalleryImage
        //            {
        //                Name = fi.Name,
        //                ImgPath = string.Format("Content/img/{0}", fi.Name),
        //                FullPath = fi.FullName,
        //                ThumbImgPath = string.Format("Content/img/thumb/{0}", fi.Name),
        //                ThumbImgFullPath = string.Format("{0}/thumb/{1}",fi.DirectoryName, fi.Name),
        //            }).AsEnumerable();
        //}
    }
}