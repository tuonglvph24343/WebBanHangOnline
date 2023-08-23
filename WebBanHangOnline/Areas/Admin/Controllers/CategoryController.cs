using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Category
        public ActionResult Index()
        {
            var items = db.categories;
            return View(items);
        }


        //thêm mới
        public ActionResult Add()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Category model)
        {
            if (ModelState.IsValid)
            {
                model.ModifiedrDate = DateTime.Now;
                model.CreatedDate = DateTime.Now;
                model.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(model.Title);
                db.categories.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var item = db.categories.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Tìm đối tượng trong cơ sở dữ liệu dựa trên Id
                    var categoryToUpdate = db.categories.FirstOrDefault(c => c.Id == model.Id);

                    if (categoryToUpdate != null)
                    {
                        // Cập nhật các thuộc tính của đối tượng
                        categoryToUpdate.Title = model.Title;
                        categoryToUpdate.Description = model.Description;
                        categoryToUpdate.Alias = model.Alias;
                        categoryToUpdate.SeoDescription = model.SeoDescription;
                        categoryToUpdate.SeoKeywords = model.SeoKeywords;
                        categoryToUpdate.SeoTitle = model.SeoTitle;
                        categoryToUpdate.Position = model.Position;
                        categoryToUpdate.ModifiedrDate = DateTime.Now;
                        categoryToUpdate.ModifierBy = model.ModifierBy;

                        // Lưu thay đổi vào cơ sở dữ liệu
                        db.SaveChanges();

                        // Chuyển hướng sau khi lưu thành công
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // Xử lý trường hợp không tìm thấy đối tượng
                        // Ví dụ: ghi log, thông báo lỗi cho người dùng, ...
                        // return View("NotFound");
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý ngoại lệ nếu có
                    // Ví dụ: ghi log, thông báo lỗi cho người dùng, ...
                    // throw ex;
                    // Hoặc có thể trả về một trang lỗi
                    // return View("Error");
                }
            }

            // ModelState.IsValid == false
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.categories.Find(id);
            if (item != null)
            {
                db.categories.Remove(item);
                db.SaveChanges();
                return Json(new { success= true });
            }
            return Json(new { success = false});
        }
    }
}