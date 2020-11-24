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
        public ActionResult AllProducts(string data)
        {
            if(data == null)
            {
                return View(db.Products.ToList());
            }
            List<Product> listproduct = new List<Product>();
            foreach (var item in db.Products)
            {
                if(data == "All")
                {
                    return View(db.Products.ToList());
                }
                else if(item.Category == data)
                {
                    listproduct.Add(item);
                }
            }
            return View(listproduct);
        }
        public ActionResult ProductDetail(string data)
        {
            int id = Convert.ToInt32(data);
            Product product = db.Products.Find(id);
            return View(product);

        }
        public ActionResult Contact()
        {
            return View();
        }
    }
}