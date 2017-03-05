using sawal.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using sawal.Models;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace sawal.Controllers {
    public class MainController : ControllerTemplate {

        // GET: Main
        [AllowAnonymous]
        public ActionResult Index() {
            ViewBag.Page = "Victor Sawal";
            ViewBag.Title = "Home";
            this.PageViewLog(Request);
            return View();
        }

        [AllowAnonymous]
        public ActionResult Error() {
            try {
                this.LogException(Request, Server.GetLastError());
            } catch { }
            return View();
        }

        [HttpGet]
        public ActionResult Dashboard() {
            ViewBag.Page = "Victor Sawal";
            this.PageViewLog(Request);
            return View();
        }

        [HttpPost]
        public ActionResult Dashboard(BlogModel model) {
            ViewBag.Page = "Victor Sawal";
            ViewBag.Title = "Dashboard";
            Blog b = new Blog(db);
            if(model.Id != null) {
                b.EditBlog(model);
            } else {
                if (!b.AddNewBlog(model)) {
                    ModelState.AddModelError("", "Error adding new blog post. Please make sure all fields are valid.");
                }
            }
            return RedirectToAction("Dashboard", "Main");
        }

        public string AjaxService() {
            ViewBag.Page = "Victor Sawal";
            ViewBag.Title = "Dashboard";
            Blog b = new Blog(db);
            BlogModel blog = new BlogModel();
            string strId = Request.Params["blogid"].ToString();
            if (Request.Params["service"] != null) {
                switch (Request.Params["service"].ToString().ToLower()) {
                    case "getblog":
                        blog = b.GetBlog(strId, true);
                        break;
                }
            }
            return JsonConvert.SerializeObject(blog);
        }

        public ActionResult Service() {
            ViewBag.Page = "Victor Sawal";
            ViewBag.Title = "Dashboard";
            Blog b = new Blog(db);
            string strId = Request.Params["blogid"].ToString();
            if (Request.Params["service"] != null) {
                switch(Request.Params["service"].ToString().ToLower()) {
                    case "activate":
                        b.ToggleActiveBlog(strId, true);
                        break;
                    case "deactivate":
                        b.ToggleActiveBlog(strId, false);
                        break;
                    case "addnav":
                        b.ToggleNavBlog(strId, true);
                        break;
                    case "removenav":
                        b.ToggleNavBlog(strId, false);
                        break;
                    case "delete":
                        b.DeleteBlog(strId);
                        break;
                }
            }
            this.PageViewLog(Request);
            return Redirect("/main/dashboard");
        }
    }
}