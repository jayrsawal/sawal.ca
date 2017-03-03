using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sawal.Models;
using sawal.Common;

namespace sawal.Controllers
{
    public class BlogController : ControllerTemplate
    {
        // GET: Blog
        [AllowAnonymous]
        public ActionResult Index() {
            ViewBag.Page = "Victor Sawal";
            ViewBag.Title = "All Blogs";
            this.PageViewLog(Request);
            return View();
        }

        // GET: Blog
        [AllowAnonymous]
        public ActionResult Get(string id) {
            //string id = Request.Params["name"].ToString();
            if(id == null) {
                return View("Index");
            }
            Blog b = new Blog(this.db);
            BlogModel blog = b.GetBlog(id);
            ViewBag.Blog = blog;
            ViewBag.Page = "Victor Sawal";
            ViewBag.Title = blog.NavTitle;
            this.PageViewLog(Request);
            return View("Get");
        }
    }
}