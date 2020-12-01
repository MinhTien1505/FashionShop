using FashionShop.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace FashionShop.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        FashionShopEntities1 db = new FashionShopEntities1();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Tables()
        {
            return View(db.Products.ToList());
        }
        public ActionResult ManagerProducts()
        {
            return View(db.Products.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id,Name,Price,Image,Description,Category")] Product product)
        {
            if (ModelState.IsValid)
            {
                var image = Request.Files["image"];
                var path = Server.MapPath("~/imageProduct/" + product.Name + ".PNG");
                image.SaveAs(path);
                product.Image = "/imageProduct/" + product.Name + ".PNG";
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ManagerProducts","Admin");
            }
            return View(product);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Tables", "Admin");
        }
        public void ClearApplicationCache()
        {
            List<string> keys = new List<string>();
            Cache cache = new Cache();
            // retrieve application Cache enumerator
            IDictionaryEnumerator enumerator = cache.GetEnumerator();

            // copy all keys that currently exist in Cache
            while (enumerator.MoveNext())
            {
                keys.Add(enumerator.Key.ToString());
            }

            // delete every key from cache
            for (int i = 0; i < keys.Count; i++)
            {
                cache.Remove(keys[i]);
            }
        }
    }
}