using sawal.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using sawal.Models;

namespace sawal.Controllers {
    public class MainController : ControllerTemplate {

        // GET: Main
        [AllowAnonymous]
        public ActionResult Index() {
            ViewBag.Page = "Victor Sawal";
            ViewBag.Title = "Home";
            return View();
        }

        [AllowAnonymous]
        public ActionResult Error() {
            try {
                this.LogException(Server.GetLastError());
            } catch { }
            return View();
        }

        [HttpGet]
        public ActionResult Dashboard() {
            ViewBag.Page = "Victor Sawal";
            return View();
        }

        [HttpPost]
        public ActionResult Dashboard(BlogModel model) {
            ViewBag.Page = "Victor Sawal";
            ViewBag.Title = "Dashboard";
            Blog b = new Blog(db);
            if (!b.AddNewBlog(model)) {
                ModelState.AddModelError("", "Error adding new blog post. Please make sure all fields are valid.");
            }
            return View();
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
            return Redirect("/main/dashboard");
        }
    }
}