using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Linq;
using sawal.Models;
using AdoLib;

namespace sawal.Common {
    public class Blog {
        Database db;

        public Blog(Database _db) {
            this.db = _db;
        }

        public Blog() {
        }

        public List<BlogModel> GetBlogs(string strBlogTypeID = null) {
            List<BlogModel> blogs = new List<BlogModel>();
            SqlCommand cmd = new SqlCommand();

            string strCommand = @"select b.*, t.name as blogtype from blog b
left join blogtype t on t.id=b.blogtypeid
where b.deleted is null
order by b.publishdate desc";
            if (strBlogTypeID != null) {
                strCommand += " and b,blogtypeid=@id";
                cmd.Parameters.AddWithValue("@blogtypeid", strBlogTypeID);
            }
            cmd.CommandText = strCommand;
            XDocument xmlBlog = db.ExecQuery(cmd, "Blog");

            foreach (XElement nd in xmlBlog.Descendants("Blog")) {
                BlogModel blog = new BlogModel();
                blog.SerializeFromXml(nd);
                blogs.Add(blog);
            }

            return blogs;
        }



        public BlogModel GetBlog(string strId) {
            BlogModel blog = new BlogModel();

            SqlCommand cmd = new SqlCommand(@"select * from blog where url=@id or @id is null");
            if (strId != null) {
                cmd.Parameters.AddWithValue("@id", strId);
            } else {
                cmd.Parameters.AddWithValue("@id", DBNull.Value);
            }
            XElement nd = db.ExecQueryElem(cmd, "Blog");
            if (nd != null) {
                blog.SerializeFromXml(nd);
            }
            return blog;
        }

        public bool ToggleActiveBlog(string strId, bool bActive) {
            SqlCommand cmd = new SqlCommand(@"update blog set active=@active where id=@id");
            cmd.Parameters.AddWithValue("@id", strId);
            if (bActive) {
                cmd.Parameters.AddWithValue("@active", 1);
            } else {
                cmd.Parameters.AddWithValue("@active", DBNull.Value);
            }
            return db.ExecNonQuery(cmd);
        }

        public bool ToggleNavBlog(string strId, bool bActive) {
            SqlCommand cmd = new SqlCommand(@"update blog set nav=@nav where id=@id");
            cmd.Parameters.AddWithValue("@id", strId);
            if (bActive) {
                cmd.Parameters.AddWithValue("@nav", 1);
            } else {
                cmd.Parameters.AddWithValue("@nav", DBNull.Value);
            }
            return db.ExecNonQuery(cmd);
        }

        public bool AddNewBlog(BlogModel blog) {
            SqlCommand cmd = new SqlCommand(@"
insert into blog(nav, navtitle, title, caption, html, blogtypeid, active, publishdate)
values(@nav, @navtitle, @title, @caption, @html, @blogtypeid, @active, @publishdate)
");
            if (blog.Nav) {
                cmd.Parameters.AddWithValue("@nav", 1);
            } else {
                cmd.Parameters.AddWithValue("@nav", DBNull.Value);
            }

            if (blog.Active) {
                cmd.Parameters.AddWithValue("@active", 1);
            } else {
                cmd.Parameters.AddWithValue("@active", DBNull.Value);
            }

            if (blog.NavTitle != null)
                cmd.Parameters.AddWithValue("@navtitle", blog.NavTitle);
            else
                cmd.Parameters.AddWithValue("@navtitle", DBNull.Value);

            if (blog.Title != null)
                cmd.Parameters.AddWithValue("@title", blog.Title);
            else
                cmd.Parameters.AddWithValue("@title", DBNull.Value);

            if (blog.Caption != null)
                cmd.Parameters.AddWithValue("@caption", blog.Caption);
            else
                cmd.Parameters.AddWithValue("@caption", DBNull.Value);

            if (blog.Html != null)
                cmd.Parameters.AddWithValue("@html", blog.Html);
            else
                cmd.Parameters.AddWithValue("@html", DBNull.Value);

            cmd.Parameters.AddWithValue("@blogtypeid", blog.BlogTypeId);
            cmd.Parameters.AddWithValue("@publishdate", blog.PublishDate);

            try {
                db.ExecNonQuery(cmd);
            } catch {
                return false;
            }
            return true;
        }



        public bool EditBlog(BlogModel blog) {
            SqlCommand cmd = new SqlCommand(@"
update blog set
navtitle=@navtitle
, title=@title
, caption=@caption
, html=@html
, blogtypeid=@blogtypeid
, publishdate=@publishdate
, url=@url
, imgpath=@imagepath
where id=@id
");
            cmd.Parameters.AddWithValue("@id", blog.Id);
            cmd.Parameters.AddWithValue("@navtitle", (blog.NavTitle == null ? "" : blog.NavTitle));
            cmd.Parameters.AddWithValue("@title", (blog.Title == null ? "" : blog.Title));
            cmd.Parameters.AddWithValue("@caption", (blog.Caption == null ? "" : blog.Caption));
            cmd.Parameters.AddWithValue("@html", (blog.Html == null ? "" : blog.Html));
            cmd.Parameters.AddWithValue("@url", (blog.Url == null ? "" : blog.Url));
            cmd.Parameters.AddWithValue("@imagepath", (blog.ImagePath == null ? "" : blog.ImagePath));
            cmd.Parameters.AddWithValue("@blogtypeid", blog.BlogTypeId);
            cmd.Parameters.AddWithValue("@publishdate", blog.PublishDate);
            try {
                db.ExecQuery(cmd);
            } catch(Exception ex) {
                return false;
            }
            return true;
        }



        public bool DeleteBlog(string strId) {
            SqlCommand cmd = new SqlCommand(@"
update blog set deleted=getdate() where id=@id
");
            cmd.Parameters.AddWithValue("@id", strId);

            try {
                db.ExecNonQuery(cmd);
            } catch {
                return false;
            }
            return true;
        }

    }
}