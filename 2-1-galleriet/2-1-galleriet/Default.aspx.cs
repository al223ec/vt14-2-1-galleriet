using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _2_1_galleriet.Model;

namespace _2_1_galleriet
{
    public partial class Default : System.Web.UI.Page
    {
        private Gallery _gallery;
        private Gallery Gallery { get { return _gallery ?? (_gallery = new Gallery()); } }

        protected void Page_Load(object sender, EventArgs e)
        {
            string ID = Request.QueryString["name"];
            if (ID != null)
            {
                MainImage.ImageUrl = "Content/img/" + ID;
                MainImage.Visible = true;
            }
            Gallery.CreateThumbNails();
        }

        public IEnumerable<Model.GalleryImage> Repeater_GetData()
        {
            return Gallery.Images;
        }
        protected void Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var galleryImage = e.Item.DataItem as GalleryImage;
            if (galleryImage != null)
            {
                var hyperLink = (HyperLink) e.Item.FindControl("HyperLink");
                hyperLink.NavigateUrl = string.Format("?name={0}", galleryImage.Name);
            }
        }
    }
}