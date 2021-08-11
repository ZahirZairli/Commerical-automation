using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using UdemyMurat.Models.Sınıflar;

namespace UdemyMurat.Controllers
{
    public class GrafikController : Controller
    {
        // GET: Grafik
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index2()
        {
            var grafikcek = new Chart(600, 600);
            grafikcek.AddTitle("Kategorya - Mehsul Stok sayi").AddLegend("Stok").AddSeries("Degerler",
                xValue: new[] { "Mobilya", "Ofis Esyalari", "Bilgisayar" }, yValues: new[] { 85, 66, 98 }).Write();
            return File(grafikcek.ToWebImage().GetBytes(), "image/jpeg");
        }

        Context c = new Context();
        public ActionResult Index3()
        {
            ArrayList xvalue = new ArrayList();
            ArrayList yvalue = new ArrayList();
            var netice = c.Uruns.ToList();
            netice.ToList().ForEach(x => xvalue.Add(x.UrunAd));
            netice.ToList().ForEach(y => yvalue.Add(y.Stok));
            var grafik = new Chart(width: 800, height: 800).AddTitle("Stok").AddSeries(chartType: "Pie", name: "Stok",
                xValue: xvalue, yValues: yvalue);
            return File(grafik.ToWebImage().GetBytes(), "image/jpeg");
        }

        Context d = new Context();
        public ActionResult GrafikV()
        {
            ArrayList xvalue = new ArrayList();
            ArrayList yvalue = new ArrayList();

            var netice = d.Uruns.ToList();
            netice.ToList().ForEach(x => xvalue.Add(x.UrunAd));
            netice.ToList().ForEach(y => yvalue.Add(y.Stok));

            var grafik = new Chart(width: 800, height: 800).AddTitle("Stoklar").AddSeries(chartType: "Column", name: "Stok", xValue: xvalue, yValues: yvalue);

            return File(grafik.ToWebImage().GetBytes(), "image/jpeg");
        }










        public ActionResult Index4()
        {
            return View();
        }
        public ActionResult VisualizeUrunResult()
        {
            return Json(Urunlistesi(), JsonRequestBehavior.AllowGet);
        }
        public List<sinif1> Urunlistesi()
        {
            List<sinif1> snf = new List<sinif1>();
            snf.Add(new sinif1()
            {
                UrunAd = "Bilgisayar",
                Stok = 120
            });
            snf.Add(new sinif1()
            {
                UrunAd = "BeyazEsya",
                Stok = 150
            });
            snf.Add(new sinif1()
            {
                UrunAd = "Mobilya",
                Stok = 70
            });
            snf.Add(new sinif1()
            {
                UrunAd = "Kucuk ev aletleri",
                Stok = 180
            });
            snf.Add(new sinif1()
            {
                UrunAd = "Mobil cihazlar",
                Stok = 90
            });
            return snf;
        }





        public ActionResult VisualizeUrunResult2()
        {
            return Json(Urunlistesi2(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index5()
        {
            return View();
        }
        public List<sinif2> Urunlistesi2()
        {
            List<sinif2> snf2 = new List<sinif2>();
            using (var context = new Context())
            {
                snf2 = c.Uruns.Select(x =>new sinif2
                {
                    Urn = x.UrunAd,
                    Stk = x.Stok
                }).ToList();
            }
            return snf2;
        }

        public ActionResult Index6()
        {
            return View();
        }
        public ActionResult Index7()
        {
            return View();
        }
    }
}