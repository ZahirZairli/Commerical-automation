using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UdemyMurat.Models;
using UdemyMurat.Models.Sınıflar;
using PagedList;
using PagedList.Mvc;

namespace UdemyMurat.Controllers
{
    public class KategoriController : Controller
    {
        Context c = new Context();
        // GET: Kategori
        public ActionResult Index()
        {
            //var degerler = c.Kategoris.ToList().ToPagedList(seh, 5);
            var degerler = c.Kategoris.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KategoriEkle(Kategori k)
        {
            if (!ModelState.IsValid)
            {
                TempData["Msj1"] = "";
                return View("KategoriEkle");
            }
            TempData["Msj1"] = "true";
            c.Kategoris.Add(k);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriSil(int id)
        {
            var ktg = c.Kategoris.Find(id);
            c.Kategoris.Remove(ktg);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult KategoriGetir(int id)
        {
            var ktg = c.Kategoris.Find(id);
            return View("KategoriGetir", ktg);
        }
        public ActionResult KategoriGuncelle(Kategori ktg)
        {
            var id = ktg.KategoriID;
            if (!ModelState.IsValid)
            {
                return View("KategoriGetir");
            }
            var ktgr = c.Kategoris.Find(ktg.KategoriID);
            ktgr.KategoriAd = ktg.KategoriAd;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Deneme()
        {
            Class3 cs = new Class3();
            cs.Kategoriler = new SelectList(c.Kategoris, "KategoriID", "KategoriAd");
            cs.Urunler = new SelectList(c.Uruns, "UrunId", "UrunAd");
            return View(cs);
        }
        public JsonResult UrunGetir(int p)
        {
            var urunler = (from x in c.Uruns
                           join y in c.Kategoris
                           on x.Kategori.KategoriID equals y.KategoriID
                           where x.Kategori.KategoriID == p
                           select new
                           {
                               Text = x.UrunAd,
                               Value = x.UrunId.ToString()
                           }
                           ).ToList();
            return Json(urunler, JsonRequestBehavior.AllowGet);
        }
    }
}