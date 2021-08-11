using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UdemyMurat.Models.Sınıflar;
using PagedList;
using PagedList.Mvc;

namespace UdemyMurat.Controllers
{
    public class YapilacakController : Controller
    {
        Context c = new Context();
        // GET: Yapilacak
        public ActionResult Index(int p=1)
        {
            var deger1 = c.Carilers.Count().ToString();
            ViewBag.d1 = deger1;

            var deger2 = c.Uruns.Count().ToString();
            ViewBag.d2 = deger2;

            var deger3 = c.Kategoris.Count().ToString();
            ViewBag.d3 = deger3;

            //var deger4 = (from x in c.Carilers select x.CariSehir).Distinct().Count().ToString();
            var deger4 = c.Carilers.GroupBy(x=>x.CariSehir).Count().ToString();
            ViewBag.d4 = deger4;

            var yp = c.Yapilacaks.Where(x => x.Durum == true).ToList().ToPagedList(p, 4);
            return View(yp);
        }
    }
}