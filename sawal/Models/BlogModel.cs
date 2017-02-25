using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace sawal.Models {
    public class BlogModel {

        public BlogModel() {
            this.Active = false;
            this.Nav = false;
            this.NavTitle = "";
            this.Title = "";
            this.Caption = "";
            this.Html = "";
            this.BlogType = "";
            this.BlogTypeId = "";
            this.Url = "";
            this.ImagePath = "";
            this.FilePath = "";
            this.PublishDate = DateTime.Now;
            this.DisplayOrder = 0;
            this.Reference = "";
        }

        public string Id { get; set; }
        public bool Active { get; set; }
        public bool Nav { get; set; }
        public string NavTitle { get; set; }
        public string Title { get; set; }
        public string Caption { get; set; }
        [AllowHtml]
        public string Html { get; set; }
        public DateTime PublishDate { get; set; }
        public string BlogTypeId { get; set; }
        public string BlogType { get; set; }
        public string Url { get; set; }
        public string ImagePath { get; set; }
        public string FilePath { get; set; }
        public string Reference { get; set; }
        public int DisplayOrder { get; set; }

        public void SerializeFromXml( XElement nd ) { 
            if(nd.Element("active") != null) {
                this.Active = true;
            }

            if (nd.Element("nav") != null) {
                this.Nav = true;
            }

            this.Id = (String) nd.Element("id");
            this.NavTitle = (String) nd.Element("navtitle");
            this.Title = (String) nd.Element("title");
            this.Caption = (String) nd.Element("caption");
            this.Html = (String) nd.Element("html");
            this.BlogTypeId = (String) nd.Element("blogtypeid");
            this.BlogType = (String)nd.Element("blogtype");
            this.Url = (String) nd.Element("url");
            this.ImagePath = (String) nd.Element("imgpath");
            this.FilePath = (String) nd.Element("filepath");
            this.Reference = (String) nd.Element("reference");
            int nOrder = 0;
            Int32.TryParse((String) nd.Element("displayorder"), out nOrder);
            this.DisplayOrder = nOrder;

            DateTime dt;
            DateTime.TryParse((String) nd.Element("publishdate"), out dt);
            this.PublishDate = dt;
        }

    }
}