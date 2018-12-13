using MyFirstWebBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFirstWebBlog.Controllers
{
    public class HomeController : BaseController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var blogs = db.Blogs.OrderByDescending(o => o.BlogId).Take(4);
            return View(blogs);
        }

        public ActionResult Category(int id)
        {
            var blogs = db.BlogCategories.Where(w => w.CategoryId == id).Select(s => s.Blog).ToList();

            return View(blogs);
        }

       

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}