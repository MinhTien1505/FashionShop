using FashionShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionShop.Controllers
{
    public class ItemController : Controller
    {
        public static string productDetial;
        // GET: Item
        FashionShopEntities1 db = new FashionShopEntities1();
        public ActionResult Index()
        {

            return View(db.Products.ToList());
        }
        public ActionResult AllProducts()
        {
            return View(db.Products.ToList());
        }
        [HttpPost]
        public ActionResult Details(int Id)
        {
            //find item from db or ...
            var data = db.Products.Where(i => i.id.Equals(Id));
            return PartialView(data);
        }
    }
}