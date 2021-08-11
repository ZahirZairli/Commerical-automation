using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UdemyMurat.Models.Sınıflar;

namespace UdemyMurat.Controllers
{
    public class GaleriController : Controller
    {
        Context c = new Context();
        // GET: Galeri
        public ActionResult Index()
        {
            var dyr = c.Uruns.ToList();
            return View(dyr);
        }
    }
}