using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UdemyMurat.Models.Sınıflar;
namespace UdemyMurat.Controllers
{
    //[Authorize]
    public class FaturaController : Controller
    {
        Context c = new Context();
        // GET: Fatura
        public ActionResult Index()
        {
            var lt = c.Faturalars.ToList();
            return View(lt);
        }
        [HttpGet]
        public ActionResult FaturaEkle()
        {
            List<SelectListItem> deger1 = (from x in c.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd,   //optionun metni
                                               Value = x.PersonelAd    //optionun valuesi
                                           }).ToList();
            ViewBag.dpr = deger1;
            return View();
        }
        [HttpPost]
        public ActionResult FaturaEkle(Faturalar f)
        {
            c.Faturalars.Add(f);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult FaturaGuncelle(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd,   //optionun metni
                                               Value = x.PersonelAd    //optionun valuesi
                                           }).ToList();
            ViewBag.dpr = deger1;
            var fr = c.Faturalars.Find(id);
            return View("FaturaGuncelle",fr);
        }
        public ActionResult FaturaGuncelle(Faturalar fr)
        {
            var old = c.Faturalars.Find(fr.FaturaId);
            old.FaturaSeriNo = fr.FaturaSeriNo;
            old.FaturaSiraNo = fr.FaturaSiraNo;
            old.Tarih = fr.Tarih;
            old.Saat = fr.Saat;
            old.VergiDairesi = fr.VergiDairesi;
            old.TeslimEden = fr.TeslimEden;
            old.TeslimAlan = fr.TeslimAlan;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FaturaDetay(int id)
        {
            var kl = c.FaturaKalems.Where(x => x.FaturaId == id).ToList();
            ViewBag.ft = id;
            return View("FaturaDetay",kl);
        }
        [HttpGet]
        public ActionResult YeniFtKl(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Faturalars.Where(x=>x.FaturaId==id).ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.FaturaId.ToString(),   //optionun metni
                                               Value = x.FaturaId.ToString()    //optionun valuesi
                                           }).ToList();
            ViewBag.dpr = deger1;
            return View();
        }
        [HttpPost]
        public ActionResult YeniFtKl(FaturaKalem fk)
        {
            c.FaturaKalems.Add(fk);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        //public JsonResult pop(int p)
        //{
        //    var urunler = (from x in c.Uruns
        //                   join y in c.Kategoris
        //                   on x.Kategori.KategoriID equals y.KategoriID
        //                   where x.Kategori.KategoriID == p
        //                   select new
        //                   {
        //                       Text = x.UrunAd,
        //                       Value = x.UrunId.ToString()
        //                   }
        //                   ).ToList();
        //    return Json(urunler, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult dinamik()
        {
            Class4 cs = new Class4();
            cs.deyer1 = c.Faturalars.ToList();
            cs.deyer2 = c.FaturaKalems.ToList();
            return View("dinamik", cs);
        }
        public ActionResult FaturaKaydet(string FaturaSeriNo, string FaturaSiraNo, DateTime Tarih, string VergiDairesi,
            string Saat, string TeslimEden, string TeslimAlan,string Toplam, FaturaKalem[] kalemler)
        {
            Faturalar f = new Faturalar();
            f.FaturaSeriNo = FaturaSeriNo;
            f.FaturaSiraNo = FaturaSiraNo;
            f.Tarih = Tarih;
            f.VergiDairesi = VergiDairesi;
            f.Saat = Saat;
            f.TeslimAlan = TeslimAlan;
            f.TeslimEden= TeslimEden;
            f.Toplam = decimal.Parse(Toplam);
            c.Faturalars.Add(f);
            foreach (var item in kalemler)
            {
                FaturaKalem fk = new FaturaKalem();
                fk.Aciklama = item.Aciklama;
                fk.BirimFiyat = item.BirimFiyat;
                fk.FaturaId = item.FaturaKalemId;
                fk.Miktar = item.Miktar;
                fk.Tutar = item.Tutar;
                c.FaturaKalems.Add(fk);
            }
            c.SaveChanges();
            return Json("Əməliyyat uğurla həyata keçirildi", JsonRequestBehavior.AllowGet);
        }
    }
}