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
        public ActionResult AllProducts(string pricemax, string pricemin)
        {
            double min = Convert.ToDouble(pricemin);
            double max = Convert.ToDouble(pricemax);
            if(pricemax == null || pricemin == null)
            {
                return View(db.Products.ToList());
            }
            List<Product> listproduct = new List<Product>();
            foreach (var item in db.Products)
            {
                if(item.Price >= min && item.Price <= max)
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