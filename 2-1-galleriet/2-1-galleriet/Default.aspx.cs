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
        private Service _service;
        private Service Service { get { return _service ?? (_service = new Service()); } }

        protected void Page_Load(object sender, EventArgs e)
        { }

        public IEnumerable<Model.GalleryImage> Repeater_GetData()
        {
            return Service.GetImages();
        }
    }
}