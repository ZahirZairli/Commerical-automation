using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UdemyMurat.Models.Sınıflar;

namespace UdemyMurat.Controllers
{
    public class DepartmanController : Controller
    {
        Context c = new Context();
        // GET: Depatman
        //[Authorize]
        [Authorize(Roles = "A")]
        public ActionResult Index()
        {
            var dpn = c.Departmans.Where(x=> x.Durum==true).ToList();
            return View(dpn);
        }
        [Authorize(Roles ="A")]
        [HttpGet]
        public ActionResult DepartmanEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DepartmanEkle(Departman k)
        {
            c.Departmans.Add(k);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DepartmanSil(int id)
        {
            var ktg = c.Departmans.Find(id);
            ktg.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DepartmanGetir(int id)
        {
            var ktg = c.Departmans.Find(id);
            return View("DepartmanGetir", ktg);
        }
        public ActionResult DepartmanGuncelle(Departman ktg)
        {
            var ktgr = c.Departmans.Find(ktg.DepartmanId);
            ktgr.DepartmanAd = ktg.DepartmanAd;
            ktgr.Durum = ktg.Durum;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DepartmanDetay(int id)
        {
            var dpt = c.Departmans.Where(x => x.DepartmanId == id).Select(y=>y.DepartmanAd).FirstOrDefault();
            ViewBag.d = dpt;
            var prs = c.Personels.Where(x => x.DepartmanId== id && x.Durum==true).ToList();
            return View(prs );
        }
        public ActionResult DepartmanPersonelSatis(int id)
        {
            var pr = c.Personels.Where(x => x.PersonelId == id).Select(y => y.PersonelAd + y.PersonelSoyad).FirstOrDefault();
            ViewBag.per = pr;
            var degerler = c.SatisHarekets.Where(x => x.PersonelId == id).ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult Xeber()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Xeber(Xeberler k)
        {
            k.Tarih = DateTime.Now;
            c.Xeberlers.Add(k);
            c.SaveChanges();
            return RedirectToAction("Xeber");
        }
    }
}