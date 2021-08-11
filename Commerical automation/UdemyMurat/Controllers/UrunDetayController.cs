using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UdemyMurat.Models.Sınıflar;

namespace UdemyMurat.Controllers
{
    public class UrunDetayController : Controller
    {
        Context c = new Context();
        // GET: UrunDetay
        public ActionResult Index()
        {
            Class1 cs = new Class1();
            cs.Mehsul = c.Uruns.ToList();
            cs.Detal = c.Detays.ToList();
            return View(cs);
        }
    }
}