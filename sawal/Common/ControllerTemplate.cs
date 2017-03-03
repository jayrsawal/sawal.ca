using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Collections;
using AdoLib;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Security.Claims;
using sawal.Models;

namespace sawal.Common {
    public class ControllerTemplate : Controller {
        public Database db;
        public string strEnv;
        public Hashtable aFields;
        public BlogListModel blogs;

        public ControllerTemplate(string _strEnv = "test") {
            this.strEnv = _strEnv;
            this.aFields = new Hashtable();
            this.SetDatabase(this.strEnv);
            Blog b = new Blog(db);
            this.blogs = new BlogListModel();
            this.blogs.BlogList = b.GetBlogs();
            ViewBag.BlogListModel = this.blogs;
        }

        public void SetDatabase(string _strEnv = "test") {
            this.strEnv = _strEnv;
            string strConn;

            switch (this.strEnv) {
                case "production":
                    strConn = ConfigurationManager.ConnectionStrings["JPROD"].ConnectionString;
                    break;

                default:
                    strConn = ConfigurationManager.ConnectionStrings["JPROD"].ConnectionString;
                    break;
            }

            db = new Database(strConn);
        }

        private void SetParameters() {

        }

        public void PageViewLog(HttpRequestBase r) {
            if (Request != null) {
                SqlCommand cmd = new SqlCommand("insert into pageviewlog(ipaddr, url, ref) values(@ipaddr, @url, @ref)");
                if (r.UserHostAddress != null) {
                    cmd.Parameters.AddWithValue("@ipaddr", r.UserHostAddress);
                } else {
                    cmd.Parameters.AddWithValue("@ipaddr", DBNull.Value);
                }
                if (r.Url != null) {
                    cmd.Parameters.AddWithValue("@url", r.Url.AbsoluteUri);
                } else {
                    cmd.Parameters.AddWithValue("@url", DBNull.Value);
                }
                if (r.UrlReferrer != null) {
                    cmd.Parameters.AddWithValue("@ref", r.UrlReferrer.AbsoluteUri);
                } else {
                    cmd.Parameters.AddWithValue("@ref", DBNull.Value);
                }
                db.ExecQuery(cmd);
            }
        }

        public void LogException(HttpRequestBase r, Exception ex) {
            string strMessage = ex.Message;
            Exception ex2 = ex;

            while (ex2.InnerException != null) {
                ex2 = ex.InnerException;
                strMessage += " \\ " + ex2.Message;
            }

            SqlCommand cmd = new SqlCommand("insert into systemerror(errortext, url, userid, ref) values(@error, @uri, @userid, @ref)");
            cmd.Parameters.AddWithValue("@error", strMessage);
            if (r.Url != null) {
                cmd.Parameters.AddWithValue("@uri", r.Url.AbsoluteUri);
            } else {
                cmd.Parameters.AddWithValue("@uri", DBNull.Value);
            }
            if (r.UrlReferrer != null) {
                cmd.Parameters.AddWithValue("@ref", r.UrlReferrer.AbsoluteUri);
            } else {
                cmd.Parameters.AddWithValue("@ref", DBNull.Value);
            }
            if (Request.IsAuthenticated) {
                string strUserId = ((ClaimsIdentity)User.Identity).FindFirst("UserId").Value;
                cmd.Parameters.AddWithValue("@userid", strUserId);
            } else {
                cmd.Parameters.AddWithValue("@userid", DBNull.Value);
            }
            db.ExecQuery(cmd);
        }
    }
}