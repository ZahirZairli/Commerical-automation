using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UdemyMurat.Models.Sınıflar;

namespace UdemyMurat.Controllers
{
    public class CariController : Controller
    {
        Context c = new Context();
        // GET: Cari
        public ActionResult Index()
        {
            var deyer = c.Carilers.Where(x => x.Durum == true).ToList();
            return View(deyer);
        }
        public ActionResult CariSil(int id)
        {
            var cr = c.Carilers.Find(id);
            cr.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult YeniCari()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniCari(Cariler cr)
        {
            cr.Durum = true;
            c.Carilers.Add(cr);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CariGetir(int id)
        {
            var cr = c.Carilers.Find(id);
            return View(cr);
        }
        public ActionResult CariGuncelle(Cariler cr)
        {
            if (!ModelState.IsValid)
            {
                return View("CariGetir");
            }
            var old = c.Carilers.Find(cr.CariId);
            old.CariAd = cr.CariAd;
            old.CariMail = cr.CariMail;
            old.CariSehir = cr.CariSehir;
            old.CariSoyad = cr.CariSoyad;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriSatis(int id)
        {
            var musteri = c.Carilers.Where(x => x.CariId== id).Select(y => y.CariAd+" "+y.CariSoyad).FirstOrDefault();
            ViewBag.mus = musteri;
            var st = c.SatisHarekets.Where(x => x.CariId == id).ToList();
            return View(st);
        }

    }
}