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
        private Gallery Gallery { get { return _gallery ?? (_gallery = new Gallery());  } }

        protected void Page_Load(object sender, EventArgs e)
        {
            string imgName = Request.QueryString["name"];
            if (imgName != null)
            {
                if (Gallery.ImageExists(imgName))
                {
                    MainImage.ImageUrl = Gallery.DictImages[imgName].ImgPath;
                    MainImage.Visible = true;
                }
            }
            Gallery.CreateThumbNails();
        }

        public IEnumerable<Model.GalleryImage> Repeater_GetData()
        {
            return from entry in Gallery.DictImages.Values select entry;
        }

        protected void Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var galleryImage = e.Item.DataItem as GalleryImage;
            if (galleryImage != null)
            {
                var hyperLink = (HyperLink)e.Item.FindControl("HyperLink");
                hyperLink.NavigateUrl = string.Format("?name={0}", galleryImage.Name);
            }
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            Validate();
            if (!IsValid)
            {
                return;
            }
            if (FileUploader.PostedFile.ContentLength != 0)
            {
                try
                {
                    //kontrollera filen ?? 
                    Gallery.SaveImage(FileUploader.PostedFile.InputStream, FileUploader.PostedFile.FileName);
                    Page_Load(sender, e);

                    SuccessFullUploadPanel.Visible = true;
                    SuccessFullUploadPanel.CssClass = "success"; 
                    OutputLiteral.Text = "Bilden laddades upp";
                }
                catch (Exception ex)
                {
                    SuccessFullUploadPanel.Visible = true;
                    SuccessFullUploadPanel.CssClass = "error";
                    OutputLiteral.Text = "fail " + ex.Message;
                }
            }
        }
    }
}