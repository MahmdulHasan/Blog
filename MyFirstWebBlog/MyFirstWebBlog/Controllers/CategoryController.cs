using MyFirstWebBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyFirstWebBlog.Controllers
{
    public class CategoryController : BaseController
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CategoryList()
        {
            var list = _db.Categories.Select(s => new {
                s.CategoryId,
                s.CategoryName
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
        public ActionResult Category(int id)
        {
            var blogs = _db.BlogCategories.Where(w => w.CategoryId == id).Select(s => s.Blog).ToList();

            return View(blogs);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(CategoryModel category)
        {
            if (ModelState.IsValid)
            {


                _db.Categories.Add(category);
                _db.SaveChanges();

                return RedirectToAction("Index", "Category");

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
            CategoryModel cat = _db.Categories.Find(id);
            if (cat == null)
            {
                return HttpNotFound();
            }
            return View(cat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryModel category)
        {
            if (ModelState.IsValid)
            {
                var cat = _db.Categories.Find(category.CategoryId);

                cat.CategoryName = category.CategoryName;


                //_db.Entry(blog).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }
        [HttpPost]
        public JsonResult DeleteCategory(int? ID)
        {
            using (ApplicationDbContext _db = new ApplicationDbContext())
            {
                var category = _db.Categories.Find(ID);
                if (ID == null)
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                _db.Categories.Remove(category);
                _db.SaveChanges();

                return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
            }
        }
    }
}