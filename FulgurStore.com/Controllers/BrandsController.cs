using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FulgurStore.Models;

namespace FulgurStore.Controllers
{
    public class BrandsController : Controller
    {
        FulgurStoreEntities1 db = new FulgurStoreEntities1();
        // GET: Brands
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllBrands()
        {
            return View();
        }
    }
}