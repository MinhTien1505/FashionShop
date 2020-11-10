using FashionShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace FashionShop.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        FashionShopEntities db = new FashionShopEntities();
        
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
        public ActionResult SendMailGetPassWord(User user)
        {
            if (db.Users.Any(x => x.Username == user.Username))
            {
                string a = RandomString(8);
                var user2 = db.Users.Where(x => x.Username == user.Username).FirstOrDefault();
                if (SendView(user2.Email, "Namngu"))
                {
                    user2.Password = a;
                    db.Entry(user2).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Kq = "Email Sent Successfully.";
                }
                else
                    ViewBag.Kq = "Problem while sending email.";
            }
            else if (user.Username == null)
                return View();
            else
                ViewBag.Kq = "There is no such account ";
            return View();
        }
        private string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }
        private bool SendView(string EMAIL, string a)
        {
            ViewBag.Kq = EMAIL;
            try
            {
                //Configuring webMail class to send emails  
                //gmail smtp server  
                WebMail.SmtpServer = "smtp.gmail.com";
                //gmail port to send emails  
                WebMail.SmtpPort = 587;
                WebMail.SmtpUseDefaultCredentials = true;
                //sending emails with secure protocol  
                WebMail.EnableSsl = true;
                //EmailId used to send emails from application  
                WebMail.UserName = "vohoangan2000@gmail.com";
                WebMail.Password = "an07042000";

                //Sender email address.  
                WebMail.From = "tranhanam1999@gmail.com";

                //Send email  
                WebMail.Send(to: EMAIL, subject: "Change Your password", body: "New you password " + a, cc: null, bcc: null, isBodyHtml: true);
                return true; //"Email Sent Successfully.";
            }
            catch (Exception)
            {
                return false;// "Problem while sending email, Please check details.";
            }
        }
    }
}