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
            if (Session["upload"] != null) 
            {
                SuccessFullUploadPanel.Visible = true;
                SuccessFullUploadPanel.CssClass = "success";
                OutputLiteral.Text = "Bilden laddades upp";
                Session.Remove("upload"); 
            }

            Gallery.CreateThumbNails();
        }

        public IEnumerable<Model.GalleryImage> Repeater_GetData()
        {
            return Gallery.DictImages.Values;
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
                    Session["upload"] = true;  
                    Response.Redirect(string.Format("?name={0}", Gallery.SaveImage(FileUploader.PostedFile.InputStream, FileUploader.PostedFile.FileName)));
                }
                catch (Exception ex)
                {
                    SuccessFullUploadPanel.Visible = true;
                    SuccessFullUploadPanel.CssClass = "error";
                    OutputLiteral.Text = "fail " + ex.Message;
                }
            }
        }

        //protected void CloseUploadButton_Click(object sender, EventArgs e)
        //{
        //     Response.Redirect(RemoveQueryStringByKey(HttpContext.Current.Request.Url.AbsoluteUri, "upload")); 
        //}
        ////http://stackoverflow.com/questions/11052744/how-to-efficiently-remove-a-query-string-by-key-from-a-url
        //public static string RemoveQueryStringByKey(string url, string key)
        //{
        //    var uri = new Uri(url);

        //    // this gets all the query string key value pairs as a collection
        //    var newQueryString = HttpUtility.ParseQueryString(uri.Query);

        //    // this removes the key if exists
        //    newQueryString.Remove(key);

        //    // this gets the page path from root without QueryString
        //    string pagePathWithoutQueryString = uri.GetLeftPart(UriPartial.Path);

        //    return newQueryString.Count > 0
        //        ? String.Format("{0}?{1}", pagePathWithoutQueryString, newQueryString)
        //        : pagePathWithoutQueryString;
        //}
    }
}