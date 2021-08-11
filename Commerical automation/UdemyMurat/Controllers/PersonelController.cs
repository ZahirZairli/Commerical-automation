using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UdemyMurat.Models.Sınıflar;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace UdemyMurat.Controllers
{
    public class PersonelController : Controller
    {
        Context c = new Context();
        // GET: Personel
        public ActionResult Index()
        {
            var pr = c.Personels.Where(x => x.Durum == true).ToList();
            return View(pr);
        }
        public ActionResult PersonelSil(int id)
        {
            var pr = c.Personels.Find(id);
            pr.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult PersonelEkle()
        {
            List<SelectListItem> deger1 = (from x in c.Departmans.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanAd,   //optionun metni
                                               Value = x.DepartmanId.ToString()    //optionun valuesi
                                           }).ToList();
            ViewBag.dpr = deger1;
            return View();
        }


        [HttpPost]
        public ActionResult PersonelEkle(Personel pr, HttpPostedFileBase PersonelGorsel)
        {
            if (PersonelGorsel != null)
            {
                string PhotoName = "Photo" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond + Path.GetExtension(PersonelGorsel.FileName);
                PersonelGorsel.SaveAs(Server.MapPath("~/Image/" + PhotoName));
                pr.PersonelGorsel ="/Image/" + PhotoName;
            }
            //    if (Request.Files.Count > 0)
            //{
            //    string dosyaad = Path.GetFileName(Request.Files[0].FileName);
            //    string uzanti = Path.GetExtension(Request.Files[0].FileName);
            //    string yol = "~/Image/" + dosyaad + uzanti;
            //    Request.Files[0].SaveAs(Server.MapPath(yol));
            //    pr.PersonelGorsel = "/Image/" + dosyaad + uzanti;
            //}
            pr.Durum = true;
            c.Personels.Add(pr);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PersonelGuncelle(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Departmans.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanAd,   //optionun metni
                                               Value = x.DepartmanId.ToString()    //optionun valuesi
                                           }).ToList();
            ViewBag.dpr = deger1;
            var per = c.Personels.Find(id);
            return View("PersonelGuncelle", per);
        }
        public ActionResult PersonelUp(Personel pr, HttpPostedFileBase PersonelGorsel)
        {
            var old = c.Personels.Find(pr.PersonelId);
            if (PersonelGorsel != null)
            {
                string PhotoName = "Photo" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond + Path.GetExtension(PersonelGorsel.FileName);
                PersonelGorsel.SaveAs(Server.MapPath("~/Image/" + PhotoName));
                old.PersonelGorsel = "/Image/" + PhotoName;
            }
            old.PersonelAd = pr.PersonelAd;
            old.PersonelSoyad = pr.PersonelSoyad;
            old.DepartmanId = pr.DepartmanId;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Personelaktiv(int p = 1)
        {
            var pr = c.Personels.Where(x=> x.Durum == true).ToList().ToPagedList(p, 3);
            return View(pr);
        }
    }
}