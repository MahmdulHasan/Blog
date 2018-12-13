using MyFirstWebBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFirstWebBlog.Controllers
{
    public class BlogController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Blog
        public ActionResult Index()
        {
            var list = _db.Blogs.ToList();
            return View(list);
        }

        public ActionResult Get(int id)
        {
            BlogModel blog = _db.Blogs.Find(id);
            var list = _db.Blogs.ToList();
            if (blog == null)
            {
                return HttpNotFound();
            }
            ViewBag.list = list;
            return View(blog);
        }
    }
}