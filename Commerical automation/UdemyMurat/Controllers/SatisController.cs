using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UdemyMurat.Models.Sınıflar;

namespace UdemyMurat.Controllers
{
    public class SatisController : Controller
    {
        Context c = new Context();
        // GET: Satis
        public ActionResult Index()
        {
            var dyr = c.SatisHarekets.ToList();
            return View(dyr);
        }
        [HttpGet]
        public ActionResult YeniSatis() 
        {
            List<SelectListItem> deger1 = (from x in c.Uruns.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.UrunAd,
                                               Value = x.UrunId.ToString()
                                           }).ToList();

            List<SelectListItem> deger2 = (from x in c.Carilers.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CariAd +" "+x.CariSoyad,
                                               Value = x.CariId.ToString()
                                           }).ToList();
            List<SelectListItem> deger3 = (from x in c.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.PersonelId.ToString()
                                           }).ToList();
            ViewBag.dgr3 = deger3;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr1 = deger1;
            return View();
        }
        [HttpPost]
        public ActionResult YeniSatis(SatisHareket sh)
        {
            sh.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            var fiyat1 = c.Uruns.Find(sh.UrunId);   
            var qiymet = fiyat1.SatisFiyat;
            sh.ToplamTutar = sh.Adet * qiymet;
            c.SatisHarekets.Add(sh);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult SatisGetir(int id) 
        {
            List<SelectListItem> deger1 = (from x in c.Uruns.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.UrunAd,
                                               Value = x.UrunId.ToString()
                                           }).ToList();

            List<SelectListItem> deger2 = (from x in c.Carilers.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CariAd + " " + x.CariSoyad,
                                               Value = x.CariId.ToString()
                                           }).ToList();
            List<SelectListItem> deger3 = (from x in c.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.PersonelId.ToString()
                                           }).ToList();
            ViewBag.dgr3 = deger3;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr1 = deger1;
            var sts = c.SatisHarekets.Find(id);
            return View("SatisGetir",sts);
        }
        [HttpPost]
        public ActionResult SatisGetir(SatisHareket sh)
        {
            var old = c.SatisHarekets.Find(sh.SatisId);
            var ededferqi = sh.Adet - old.Adet;
            old.Adet = sh.Adet;
            old.PersonelId = sh.PersonelId;
            old.CariId = sh.CariId;
            old.UrunId = sh.UrunId;

            var fiyat1 = c.Uruns.Find(sh.UrunId);
            var qiymet = fiyat1.SatisFiyat;
            sh.ToplamTutar = sh.Adet * qiymet;

            old.ToplamTutar = sh.ToplamTutar;

            var id = sh.UrunId;
            var oldur = c.Uruns.Find(id);
            oldur.Stok = (short)(oldur.Stok - ededferqi);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SatisDetay(int id)
        {
            var sts = c.SatisHarekets.Where(x=>x.SatisId==id).ToList();
            return View(sts);
        }
    }
}