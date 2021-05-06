using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FulgurStore.Models;
using System.IO;

namespace FulgurStore.Controllers
{
    public class CategoryController : Controller
    {
        FulgurStoreEntities1 db = new FulgurStoreEntities1();

        // GET: Category
        
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            string filename = Path.GetFileNameWithoutExtension(category.image_file.FileName);
            string extention = Path.GetExtension(category.image_file.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extention;
            category.Category_image = "~/Images/" + filename;
            filename = Path.Combine(Server.MapPath("~/Images/"), filename);
            category.image_file.SaveAs(filename);
            db.Categories.Add(category);
            db.SaveChanges();
            ModelState.Clear();
            return RedirectToAction("AllCategories");
        }

        public ActionResult AllCategories()
        {
            var Categories = db.Categories.ToList();
            return View(Categories);
        }

        [HttpGet]
        public ActionResult Update(int ID)
        {
            Category Selectcategory = db.Categories.Where(q => q.ID == ID).SingleOrDefault();
            return View(Selectcategory);
        }

        //[HttpPost]
        //public ActionResult Update(Category cat)
        //{
        //    Category oldcategory = db.Categories.Where(q => q.ID == cat.ID).SingleOrDefault();
        //    oldcategory.Category_name = cat.Category_name;
        //    oldcategory.Category_image = cat.Category_image;
        //    oldcategory.Status = cat.Status;
        //    db.SaveChanges();
        //    return RedirectToAction("AllCategories");
        //}

        public ActionResult Activate(int ID)
        {
            Category Activatecategory = db.Categories.Find(ID);
            Activatecategory.Status = true;
            db.SaveChanges();
            return RedirectToAction("AllCategories");
        }

        public ActionResult Unactivate(int ID)
        {
            Category unactivatecategory = db.Categories.Find(ID);
            unactivatecategory.Status = false;
            db.SaveChanges();
            return RedirectToAction("AllCategories");
        }

        public ActionResult Delete(int ID)
        {
            Category Selectedcategory = db.Categories.Find(ID);
            db.Categories.Remove(Selectedcategory);
            db.SaveChanges();
            return RedirectToAction("AllCategories");
        }

    }
}