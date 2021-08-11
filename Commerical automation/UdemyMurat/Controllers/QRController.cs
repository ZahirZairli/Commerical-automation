using QRCoder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Drawing.Imaging;

namespace UdemyMurat.Controllers
{
    public class QRController : Controller
    {
        // GET: QR
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string kod)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator kodyarat = new QRCodeGenerator();
                QRCodeGenerator.QRCode karekod = kodyarat.CreateQrCode(kod,
                QRCodeGenerator.ECCLevel.Q);
                using (Bitmap sekil = karekod.GetGraphic(10))
                {
                    sekil.Save(ms, ImageFormat.Png);
                    ViewBag.karekodsekil = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }
                return View();
        }
    }
}