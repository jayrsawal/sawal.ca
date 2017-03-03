using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sawal.Models;
using sawal.Common;

namespace sawal.Controllers
{
    public class PublicationController : ControllerTemplate
    {
        // GET: Publication
        [AllowAnonymous]
        public ActionResult Get(string id) {
            //string id = Request.Params["name"].ToString();
            Blog b = new Blog(this.db);
            BlogModel blog = b.GetBlog(id);
            ViewBag.Blog = blog;
            ViewBag.Page = "Victor Sawal";
            this.PageViewLog(Request);
            return View("Index");
        }

        [AllowAnonymous]
        public ActionResult Publication() {
            ViewBag.Page = "Victor Sawal";
            ViewBag.Title = "Publications";
            this.PageViewLog(Request);
            return View();
        }

        [AllowAnonymous]
        public ActionResult Application() {
            ViewBag.Page = "Victor Sawal";
            ViewBag.Title = "Publications";
            this.PageViewLog(Request);
            return View();
        }
    }
}