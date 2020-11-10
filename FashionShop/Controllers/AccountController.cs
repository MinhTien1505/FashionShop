using FashionShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FashionShop.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        Model1 db = new Model1();
        
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
        public ActionResult Login(string username, string password)
        {
            if (username != null && password != null)
            {
                if (ModelState.IsValid)
                {
                    var f_password = GetMD5(password);
                    var data = db.Users.Where(s => s.Username.Equals(username) && s.Password.Equals(f_password)).ToList();
                    if (data.Count() > 0)
                    {
                        //add session
                        Session["FullName"] = data.FirstOrDefault().Fullname;
                        Session["Email"] = data.FirstOrDefault().Email;
                        Session["idUser"] = data.FirstOrDefault().id;
                        Session["Avatar"] = data.FirstOrDefault().Avatar;
                        return RedirectToAction("Index", "Products");
                    }
                    else
                    {
                        ViewBag.error = "Login failed";
                        return View();
                    }
                }
            }

            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = db.Users.FirstOrDefault(s => s.Username == _user.Username);
                if (check == null)
                {
                    var f = Request.Files["Avatar"];
                    var path = Server.MapPath("~/Image/" + _user.Username + ".PNG");
                    f.SaveAs(path);
                    _user.Avatar = "~/Image/" + _user.Username + ".PNG";
                    _user.Password = GetMD5(_user.Password);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Users.Add(_user);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.error = "Account already exists";
                    return View();
                }
            }
            return View();
        }
    }
}