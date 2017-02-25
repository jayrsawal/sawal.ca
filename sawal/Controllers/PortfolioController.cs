﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sawal.Models;
using sawal.Common;

namespace sawal.Controllers
{
    public class PortfolioController : ControllerTemplate {
        // GET: Portfolio
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Get(string id) {
            //string id = Request.Params["name"].ToString();
            Blog b = new Blog(this.db);
            BlogModel blog = b.GetBlog(id);
            ViewBag.Blog = blog;
            ViewBag.Page = "Victor Sawal";
            ViewBag.Title = blog.NavTitle;
            return View("Index");
        }
    }
}