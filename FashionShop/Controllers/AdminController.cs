using FashionShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
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
                return RedirectToAction("Tables","Admin");
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
    }
}