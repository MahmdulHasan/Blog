using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyFirstWebBlog.Models;

namespace MyFirstWebBlog.Controllers
{
    public class BlogCategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BlogCategories
        public ActionResult Index()
        {
            var blogCategories = db.BlogCategories.Include(b => b.Blog).Include(b => b.Category);
            return View(blogCategories.ToList());
        }

        // GET: BlogCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogCategoryModel blogCategoryModel = db.BlogCategories.Find(id);
            if (blogCategoryModel == null)
            {
                return HttpNotFound();
            }
            return View(blogCategoryModel);
        }

        // GET: BlogCategories/Create
        public ActionResult Create()
        {
            ViewBag.BlogId = new SelectList(db.Blogs, "BlogId", "Title");
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: BlogCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BlogId,CategoryId")] BlogCategoryModel blogCategoryModel)
        {
            if (ModelState.IsValid)
            {
                db.BlogCategories.Add(blogCategoryModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BlogId = new SelectList(db.Blogs, "BlogId", "Title", blogCategoryModel.BlogId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", blogCategoryModel.CategoryId);
            return View(blogCategoryModel);
        }

        // GET: BlogCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogCategoryModel blogCategoryModel = db.BlogCategories.Find(id);
            if (blogCategoryModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.BlogId = new SelectList(db.Blogs, "BlogId", "Title", blogCategoryModel.BlogId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", blogCategoryModel.CategoryId);
            return View(blogCategoryModel);
        }

        // POST: BlogCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BlogId,CategoryId")] BlogCategoryModel blogCategoryModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blogCategoryModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BlogId = new SelectList(db.Blogs, "BlogId", "Title", blogCategoryModel.BlogId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", blogCategoryModel.CategoryId);
            return View(blogCategoryModel);
        }

        // GET: BlogCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogCategoryModel blogCategoryModel = db.BlogCategories.Find(id);
            if (blogCategoryModel == null)
            {
                return HttpNotFound();
            }
            return View(blogCategoryModel);
        }

        // POST: BlogCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogCategoryModel blogCategoryModel = db.BlogCategories.Find(id);
            db.BlogCategories.Remove(blogCategoryModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
