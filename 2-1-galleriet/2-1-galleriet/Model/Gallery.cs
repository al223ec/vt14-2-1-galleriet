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
        
        private static Dictionary<string, GalleryImage> _dictImages;
        
        public IReadOnlyDictionary<string, GalleryImage> DictImages { get { return _dictImages as IReadOnlyDictionary<string, GalleryImage>; } }

        static Gallery()
        {
            ApprovedExtensions = new Regex("(.jpg|.gif|.png)", RegexOptions.IgnoreCase);
            var invalidChars = new string(Path.GetInvalidFileNameChars());

            SanitizePath = new Regex(string.Format("[{0}]", Regex.Escape(invalidChars)));
            PhysicalUploadedImagesPath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), @"Content\img");

            _dictImages = new Dictionary<string, GalleryImage>();
            InitGallery();
        }

        private static void InitGallery()
        {
            var di = new DirectoryInfo(PhysicalUploadedImagesPath);
            foreach (var info in di.GetFiles())
            {
                AddImage(info as FileInfo);
            }
        }

        public void CreateThumbNails()
        {
            foreach (var item in _dictImages.Values)
            {
                if (!File.Exists(item.ThumbFullPath))
                {
                    var image = System.Drawing.Image.FromFile(item.FullPath);
                    var thumbnail = image.GetThumbnailImage(60, 45, null, System.IntPtr.Zero);
                    thumbnail.Save(item.ThumbFullPath);
                }
            }
        }
        public void SaveImage(Stream stream, string filename)
        {
            if (ApprovedExtensions.IsMatch(filename))
            {
                var newImage = Image.FromStream(stream);
                if (IsValidImage(newImage))
                {
                    var imageName = Path.GetFileNameWithoutExtension(filename);
                    var path = Path.Combine(PhysicalUploadedImagesPath, filename);
                    var numOfExistingImages = 1;

                    while (File.Exists(path))
                    {
                        path = Path.Combine(PhysicalUploadedImagesPath, string.Format("{0}{1}{2}", imageName, string.Format("({0})", numOfExistingImages), Path.GetExtension(path)));
                        numOfExistingImages++;
                    }
                    newImage.Save(path);
                    AddImage(new FileInfo(path));
                }
                else
                {
                    throw new ArgumentException("Bilden är inte i några av dom tillåtna formaten, GIF, PNG eller JPEG"); 
                }
            }
            else
            {
                throw new ArgumentException("Filen verkar inte ha en godkänd filändelse"); 
            }
        }
        //Kan jag ha denna statisk?? 
        private static void AddImage(FileInfo file)
        {
            _dictImages.Add(
                file.Name, new GalleryImage
                {
                    Name = file.Name,
                    FullPath = file.FullName,
                });
        }

        public GalleryImage GetImage(string name)
        {
            if (ImageExists(name))
            {
                return _dictImages[name];
            }
            else
            {
                //throw new ArgumentException(string.Format("{0} Finns inte i listan", name));
                return null;
            }
        }

        private static bool IsValidImage(Image image)
        {
            return image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Gif.Guid ||
                    image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Jpeg.Guid ||
                    image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Png.Guid;
        }

        public void DeleteImage(string name)
        {
            _dictImages.Remove(name);
        }
        public bool ImageExists(string name)
        {
            return _dictImages.ContainsKey(name);
        }

        //public Gallery()
        //{
        //    DictImages = new Dictionary<string, GalleryImage>(100);
        //    var di = new DirectoryInfo(PhysicalUploadedImagesPath);
        //    foreach (var info in di.GetFiles())
        //    {
        //        AddImage(info as FileInfo);
        //    }
        //}
        //private void GetImagesAsDictionary()
        //{
        //    DictImages = new Dictionary<string, GalleryImage>(100);

        //    var di = new DirectoryInfo(PhysicalUploadedImagesPath);
        //    //var dict = new Dictionary<string, GalleryImage>();

        //    foreach (var info in di.GetFiles())
        //    {
        //        AddImage(info as FileInfo); 
        //        //dict.Add(info.Name, new GalleryImage
        //        //{
        //        //    Name = info.Name,
        //        //    FullPath = info.FullName,
        //        //});
        //    }
        //   // return dict;
        //}

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