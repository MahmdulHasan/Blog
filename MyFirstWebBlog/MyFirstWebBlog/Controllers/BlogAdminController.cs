using MyFirstWebBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyFirstWebBlog.Controllers
{
    [Authorize]
    public class BlogAdminController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: BlogAdmin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BlogList()
        {
            var list = _db.Blogs.Select(s => new {
                s.BlogId,
                s.Title,
                s.Content,
                s.Author,
                s.Status,
            
            }).ToList();

            return Json(new
            {
                sEcho = "",
                iTotalRecords = list.Count(),
                iTotalDisplayRecords = list.Count(),
                data = list
            },
                JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var cats = _db.Categories.Select(s => new
            {
                Id = s.CategoryId,
                Name = s.CategoryName
            }).ToList();
            ViewBag.cats = cats;

            return View();

        }

        [HttpPost]
        public ActionResult Create(BlogModel blog, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {

                System.IO.Directory.CreateDirectory(Server.MapPath("~/Images/Blog"));
                string path = "";
                if (file != null)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string physicalPath =
                        System.IO.Path.Combine(Server.MapPath("~/Images/Blog"), pic);
                    path = "/Images/Blog/" + pic;
                    // file is uploaded
                    file.SaveAs(physicalPath);
                    blog.ThumbnailImageUrl = path;

                }

                blog.CreationDate = DateTime.Now;

                _db.Blogs.Add(blog);
                _db.SaveChanges();
                var b = blog;
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogModel blog = _db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BlogModel blog)
        {
            if (ModelState.IsValid)
            {
                var blg = _db.Blogs.Find(blog.BlogId);
                blg.LastUpdateDate = DateTime.Now;
                blg.Title = blog.Title;
                blg.Content = blog.Content;
                blg.Author = blog.Author;
                blg.Status = blog.Status;

                //_db.Entry(blog).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blog);
        }


        [HttpPost]
        public JsonResult DeleteBlog(int? ID)
        {
            using (ApplicationDbContext _db = new ApplicationDbContext())
            {
                var blog = _db.Blogs.Find(ID);
                if (ID == null)
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                _db.Blogs.Remove(blog);
                _db.SaveChanges();

                return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
            }
        }
    }
}