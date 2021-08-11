using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UdemyMurat.Models.Sınıflar;

namespace UdemyMurat.Controllers
{
    public class KargoController : Controller
    {
        Context c = new Context();
        // GET: Kargo
        public ActionResult Index(string p)
        {
            var kargolar = from x in c.KargoDetays select x;
            if (!string.IsNullOrEmpty(p))
            {
                kargolar = kargolar.Where(z => z.TakipKodu.Contains(p));
            }
            return View(kargolar.ToList());
        }

        public ActionResult KargoEkle()
        {
            Random rnd = new Random();
            string[] karakterler = {"A", "B", "C", "D", "E", "F", "G", "H", "K" };
            int k1, k2, k3;
            k1 = rnd.Next(0, karakterler.Length);
            k2 = rnd.Next(0, karakterler.Length);
            k3 = rnd.Next(0, karakterler.Length);
            int s1, s2, s3;
            s1 = rnd.Next(100, 1000);
            s2 = rnd.Next(10, 99);
            s3 = rnd.Next(10, 99);
            string kod = s1.ToString() + karakterler[k1] + s2 + karakterler[k2] + s3 + karakterler[k3];
            ViewBag.takipkod = kod;
            return View();
        }
        [HttpPost]
        public ActionResult KargoEkle(KargoDetay k)
        {
            k.Tarih = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return View("KargoEkle");
            }
            c.KargoDetays.Add(k);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KargoTakip(string id)
        {
            var degerler = c.KargoTakips.Where(x => x.TakipKodu == id).ToList();
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
    }
}