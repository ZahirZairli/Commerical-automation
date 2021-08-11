using PagedList;
using PagedList.Mvc;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UdemyMurat.Models.Sınıflar;
namespace UdemyMurat.Controllers
{
    public class CariPanelController : Controller
    {
        Context c = new Context();
        // GET: CariPanel
        public ActionResult Index()
        {
            var mail = (string)Session["CariMail"];
            var musteri = c.Carilers.Where(x => x.CariMail == mail).ToList();
            ViewBag.m = mail;
            var carid= c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariId).FirstOrDefault();
            ViewBag.cid = carid;

            var toplamsatis = c.SatisHarekets.Where(x => x.CariId == carid).Count();
            var toplamsatismq = c.SatisHarekets.Where(x => x.CariId == carid).Sum(y => (decimal?)y.ToplamTutar).ToString();
            var toplamsatissy = c.SatisHarekets.Where(x => x.CariId == carid).Sum(y => (decimal?)y.Adet).ToString();
            ViewBag.tms= toplamsatis;
            ViewBag.tmssay= toplamsatissy;
            ViewBag.tmstp= toplamsatismq;

            var adsoyad = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.adsoyad = adsoyad;

            ViewBag.mesaj = c.Mesajlars.Where(x => x.Alici == mail && x.Durum==true).ToList();

            return View(musteri);
        }
        public ActionResult Sil(int id)
        {
            var ol = c.Mesajlars.Find(id);
            ol.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult sifarislerim()
        {
            var mail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == mail.ToString()).Select(y => y.CariId).FirstOrDefault();

            var satislar = c.SatisHarekets.Where(x => x.CariId == id).ToList();
            return View(satislar);
        }

        public ActionResult GelenMesajlar(int id = 1)
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = c.Mesajlars.ToList().OrderByDescending(x=>x.MesajId).Where(x=>x.Alici == mail && x.Durum == true).ToPagedList(id,5);
            ViewBag.say = mesajlar.Count();
            return View(mesajlar);
        }
        [HttpGet]
        public ActionResult YeniMesaj()
        {
            ViewBag.mail = (string)Session["CariMail"];
            return View();
        }
        [HttpPost]
        public ActionResult YeniMesaj(mesajlar yenimesaj)
        {
            yenimesaj.Tarix = DateTime.Parse(DateTime.Now.ToShortDateString());
            yenimesaj.Gonderici = (string)Session["CariMail"];
            yenimesaj.Durum = true;
            if (!ModelState.IsValid)
            {
                return View("YeniMesaj");
            }

            c.Mesajlars.Add(yenimesaj);
            c.SaveChanges();
            return RedirectToAction("GonderilenMesajlar");
        }
        public ActionResult GonderilenMesajlar(int id = 1)
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = c.Mesajlars.OrderByDescending(x=>x.MesajId).ToList().Where(x => x.Gonderici == mail && x.Durum == true).ToPagedList(id, 5);
            ViewBag.say = mesajlar.Count();
            return View(mesajlar);
        }

        public PartialViewResult Partial()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = c.Mesajlars.ToList().Where(x => x.Gonderici == mail && x.Durum == true);
            ViewBag.gonderilen = mesajlar.Count();

            var mesajlar2 = c.Mesajlars.ToList().Where(x => x.Alici == mail && x.Durum == true);
            ViewBag.gelen = mesajlar2.Count();

            var mesajlar3 = c.Mesajlars.ToList().Where(x => x.Alici == mail && x.Durum == false);
            ViewBag.zibil = mesajlar3.Count();

            return PartialView();
        }
        public ActionResult MesajDetay(int id)
        {
            var detay = c.Mesajlars.Where(x=>x.Durum == true && x.MesajId==id).ToList();
            return View(detay);
        }

        public ActionResult KargoTakip(string p)
        {
            var mail = (string)Session["CariMail"];
            var kargolar = from x in c.KargoDetays select x;
            if (!string.IsNullOrEmpty(p))
            {
                kargolar = kargolar.Where(z => z.TakipKodu.Contains(p) && z.AliciMail==mail);
            }
            return View(kargolar.Where(x=>x.AliciMail==mail).ToList());
        }
        public ActionResult CariKargoTakip(string id)
        {
            var degerler = c.KargoTakips.Where(x => x.TakipKodu == id).OrderByDescending(x=>x.TarihZaman).ToList();
            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator kodyarat = new QRCodeGenerator();
                QRCodeGenerator.QRCode karekod = kodyarat.CreateQrCode(id,
                QRCodeGenerator.ECCLevel.Q);
                using (Bitmap sekil = karekod.GetGraphic(10))
                {
                    sekil.Save(ms, ImageFormat.Png);
                    ViewBag.karekodsekil = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }
            return View(degerler);
        }
        public ActionResult LogOut() 
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index","Login");
        }

        public PartialViewResult Setting()
        {
            var mail = (string)Session["CariMail"];
            var cariid = c.Carilers.Where(x => x.CariMail == mail).Select(y=>y.CariId).FirstOrDefault();
            var cari = c.Carilers.Find(cariid);
            return PartialView("Setting", cari);
        }

        public ActionResult cariup(Cariler newcari)
        {
            var mail = (string)Session["CariMail"];
            var cariid = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariId).FirstOrDefault();
            var cari = c.Carilers.Find(cariid);
            cari.CariAd = newcari.CariAd;
            cari.CariSoyad = newcari.CariSoyad;
            cari.CariSehir = newcari.CariSehir;
            cari.CariSifre = newcari.CariSifre;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ZibilQutusu(int id = 1)
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = c.Mesajlars.ToList().OrderByDescending(x => x.MesajId).Where(x => x.Alici == mail && x.Durum == false).ToPagedList(id, 5);
            ViewBag.say = mesajlar.Count();
            return View(mesajlar);
        }
        public ActionResult TamSil(int id)
        {
            var ol = c.Mesajlars.Find(id);
            c.Mesajlars.Remove(ol);
            c.SaveChanges();
            return RedirectToAction("GelenMesajlar");
        }
        public ActionResult Geri(int id)
        {
            var ol = c.Mesajlars.Find(id);
            ol.Durum = true;
            c.SaveChanges();
            return RedirectToAction("GelenMesajlar");
        }

        public PartialViewResult Xeberler()
        {
            var xeber = c.Xeberlers.OrderByDescending(x => x.XeberId).Take(5).ToList();
            return PartialView("Xeberler", xeber);
        }
    }
}