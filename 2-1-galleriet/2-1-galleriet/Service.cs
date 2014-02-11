using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
namespace _2_1_galleriet.Model
{
    public class Service
    {
        private static readonly string WorkingPath;
        private IEnumerable<GalleryImage> GalleryImages { get; set; }

        static Service()
        {
            WorkingPath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), @"Content\img"); 
        }

        public IEnumerable<GalleryImage> GetImages()
        {
            var di = new DirectoryInfo(WorkingPath); 

            return (from fi in di.GetFiles()
                select new GalleryImage{
                    Name = fi.Name,
                    ImgPath = string.Format("Content/img/{0}", fi.Name),
                }).AsEnumerable();
        }

        private IEnumerable<GalleryImage> ReadImage() 
        {
            throw new NotImplementedException();
        }
    }
}