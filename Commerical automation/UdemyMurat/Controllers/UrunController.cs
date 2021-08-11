using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UdemyMurat.Models.Sınıflar;

namespace UdemyMurat.Controllers
{
    public class UrunController : Controller
    {
        Context c = new Context();
        // GET: Urun
        public ActionResult Index(string p)
        {
            var urunler = from x in c.Uruns.Where(y=>y.Durum==true) select x;
            if (!string.IsNullOrEmpty(p))
            {
                urunler = urunler.Where(z => z.UrunAd.Contains(p));
            }
            return View(urunler.ToList());
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()
                                           select new SelectListItem
                                           { 
                                              Text=x.KategoriAd,   //optionun metni
                                              Value=x.KategoriID.ToString()    //optionun valuesi
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(Urun urn)
        {
            c.Uruns.Add(urn);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunSil(int id)
        {
            var urn = c.Uruns.Find(id);
            urn.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,   //optionun metni
                                               Value = x.KategoriID.ToString()    //optionun valuesi
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            var ktg = c.Uruns.Find(id);
            return View("UrunGetir", ktg);
        }
        public ActionResult UrunGuncelle(Urun urn)
        {
            var old = c.Uruns.Find(urn.UrunId);
            old.UrunAd = urn.UrunAd;
            old.AlisFiyat = urn.AlisFiyat;
            old.Durum = urn.Durum;
            old.KategoriId = urn.KategoriId;
            old.SatisFiyat = urn.SatisFiyat;
            old.Marka = urn.Marka;
            old.Stok = urn.Stok;
            old.UrunGorsel = urn.UrunGorsel;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Satisyap(int id)
        {
            var urun = c.Uruns.Find(id);
            List<SelectListItem> deger1 = (from x in c.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd,   //optionun metni
                                               Value = x.PersonelId.ToString()    //optionun valuesi
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = urun.UrunId;
            ViewBag.dgr3 = urun.SatisFiyat;
            return View();
        }

        [HttpPost]
        public ActionResult Satisyap(SatisHareket sh)
        {
            sh.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            var fiyat1 = c.Uruns.Find(sh.UrunId);
            var qiymet = fiyat1.SatisFiyat;
            sh.Fiyat = qiymet; 
            sh.ToplamTutar = sh.Adet * qiymet;
            c.SatisHarekets.Add(sh);
            c.SaveChanges();
            return RedirectToAction("Index","Satis");
        }
    }
}