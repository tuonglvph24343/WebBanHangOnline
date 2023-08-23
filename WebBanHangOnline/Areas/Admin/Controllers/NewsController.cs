using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Areas.Admin.Controllers
{

    public class NewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/News
        public ActionResult Index()
        {
            var items = db.Newes.OrderByDescending(x => x.Id).ToList();
            return View(items);
        }
        public ActionResult Add()
        {
            return View();
        }
    }
}