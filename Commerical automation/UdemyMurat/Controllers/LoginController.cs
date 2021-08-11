using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UdemyMurat.Models.Sınıflar;

namespace UdemyMurat.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        Context c = new Context();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult Partial1()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Partial1(Cariler yenicari)
        {
            var yx = c.Carilers.Where(x=>x.CariMail==yenicari.CariMail).ToList();
            if (yx.Count == 0)
            {
            yenicari.Durum = true;
            c.Carilers.Add(yenicari);
            c.SaveChanges();
                TempData["Message2"] = "Uğurla qeydiyyatdan keçdiniz!Müştəri girişə klik edərək hesabınıza daxil ola bilrəsiniz.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Message2"] = "Bu mail sistemdə hal hazırda qeydiyyatdadır!Xahiş olunur başqa maildən istifadə edəsiniz.";
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public ActionResult CariLogin (Cariler cr)
        {
            var bilgi = c.Carilers.FirstOrDefault(x => x.CariMail == cr.CariMail && x.CariSifre == cr.CariSifre);
            if (bilgi != null)
            {
                FormsAuthentication.SetAuthCookie(bilgi.CariMail, false);
                Session["CariMail"] = bilgi.CariMail.ToString();
                return RedirectToAction("Index", "CariPanel");
            }
            else
            {
                TempData["Message"] = "Istifadəçi mailiniz və ya parolunuz yanlışdır.Zəhmət olmasa yenidən cəhd edin!";
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult AdminLogin(Admin ad)
        {
            var bilgi = c.Admins.FirstOrDefault(x => x.KullaniciAd == ad.KullaniciAd && x.Sifre== ad.Sifre);
            if (bilgi != null)
            {
                FormsAuthentication.SetAuthCookie(bilgi.KullaniciAd, false);
                Session["CariMail"] = bilgi.KullaniciAd.ToString();
                return RedirectToAction("Index", "Kategori");
            }
            else
            {
                TempData["Message3"] = "Istifadəçi adiniz və ya parolunuz yanlışdır.Zəhmət olmasa yenidən cəhd edin!";
                return RedirectToAction("Index", "Login");
            }
        }

    }
}