using MyFirstWebBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyFirstWebBlog.Controllers
{
    public class BlogSectionController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: BlogSection
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            var blogs = _db.Blogs.Select(s => new
            {
                Id = s.BlogId,
                Name = s.Title
            }).ToList();
            ViewBag.blogs = blogs;
            return View();
        }
        [HttpPost]
        public ActionResult Create(BlogSectionModel blogSection, HttpPostedFileBase file)
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
                    blogSection.ImageUrl = path;

                }
                _db.BlogSections.Add(blogSection);
                _db.SaveChanges();
                //return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
        public ActionResult BlogSectionList()
        {
            var list = _db.BlogSections.Select(s => new {
                s.BlogSectionId,
                s.BlogId,
                s.SectionTitle,
                s.SectionContent,
               

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
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogSectionModel blogSection = _db.BlogSections.Find(id);
            if (blogSection == null)
            {
                return HttpNotFound();
            }
            return View(blogSection);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BlogSectionModel blogSection)
        {
            if (ModelState.IsValid)
            {
                var blg = _db.BlogSections.Find(blogSection.BlogSectionId);
                blg.SectionTitle = blogSection.SectionTitle;
                blg.SectionContent = blogSection.SectionContent;

                //_db.Entry(blog).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogSection);
        }
        [HttpPost]
        public JsonResult DeleteBlogSection(int? ID)
        {
            using (ApplicationDbContext _db = new ApplicationDbContext())
            {
                var blogSection = _db.BlogSections.Find(ID);
                if (ID == null)
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                _db.BlogSections.Remove(blogSection);
                _db.SaveChanges();

                return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
            }
        }
    }
}