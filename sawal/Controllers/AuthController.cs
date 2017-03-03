using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sawal.Models;
using System.Security.Claims;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using sawal.Common;
using sawal.Auth;

namespace sawal.Controllers {
    [AllowAnonymous]
    public class AuthController : ControllerTemplate {
        // GET: Auth
        public ActionResult Index() {
            this.PageViewLog(Request);
            return View();
        }

        [HttpGet]
        public ActionResult Login(string strReturnUrl) {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");

            LoginModel model = new LoginModel {
                ReturnUrl = strReturnUrl
            };
            this.PageViewLog(Request);
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model) {
            if (!ModelState.IsValid) {
                return View();
            }

            ClaimsIdentity identity = null;
            UserConfig user = new UserConfig(db);

            if (user.VerifyUser(model, out identity)) {
                IOwinContext ctx = Request.GetOwinContext();
                IAuthenticationManager authManager = ctx.Authentication;

                authManager.SignIn(identity);

                return Redirect(GetRedirectUrl(model.ReturnUrl));
            }
            // user authN failed
            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }

        public ActionResult Logout() {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;
            authManager.SignOut("ApplicationCookie");
            this.PageViewLog(Request);
            return RedirectToAction("login", "auth");
        }

        private string GetRedirectUrl(string strReturnUrl) {
            if (string.IsNullOrEmpty(strReturnUrl)) {
                return Url.Action("dashboard", "main");
            } else {
                return strReturnUrl;
            }
        }
    }
}