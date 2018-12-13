using MyFirstWebBlog.Models;
using MyFirstWebBlog.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFirstWebBlog.Controllers
{
    public class BaseController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var menus = _db.Categories.Select(s => new VmCategory
            {
                Id = s.CategoryId,
                Name = s.CategoryName
            });
            ViewBag.menus = menus;
            base.OnActionExecuting(filterContext);
        }
    }
}