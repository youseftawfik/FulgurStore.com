using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FulgurStore.Models;
using System.IO;

namespace FulgurStore.Controllers
{
    public class SubCategoriesController : Controller
    {
        FulgurStoreEntities1 db = new FulgurStoreEntities1();

        // GET: SubCategories
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllSubcategories()
        {
            var subcategories = db.SubCategories.ToList();
            return View(subcategories);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var categories = db.Categories.ToList();
            return View(categories);
        }

        [HttpPost]
        public ActionResult Create(SubCategory sub)
        {
            string filename = Path.GetFileNameWithoutExtension(sub.image_file.FileName);
            string extension = Path.GetExtension(sub.image_file.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            sub.SubCategory_image = "~/Images/" + filename;
            filename = Path.Combine(Server.MapPath("~/Images/"), filename);
            sub.image_file.SaveAs(filename);
            db.SubCategories.Add(sub);
            db.SaveChanges();
            ModelState.Clear();
            return RedirectToAction("AllSubcategories");
        }

        public ActionResult Activate(int ID) 
        {
            SubCategory activate = db.SubCategories.Find(ID);
            activate.Status = true;
            db.SaveChanges();
            return RedirectToAction("AllSubcategories");
        }

        public ActionResult Unactivate(int ID)
        {
            SubCategory unactivate = db.SubCategories.Find(ID);
            unactivate.Status = false;
            db.SaveChanges();
            return RedirectToAction("AllSubcategories");
        }

        public ActionResult Delete(int ID)
        {
            SubCategory delete = db.SubCategories.Find(ID);
            db.SubCategories.Remove(delete);
            db.SaveChanges();
            return RedirectToAction("AllSubcategories");
        }
    }
}