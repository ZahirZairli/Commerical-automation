using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UdemyMurat.Models.Sınıflar
{
    public class Xeberler
    {
        [Key]
        public int XeberId { get; set; }

        [Column(TypeName = "nvarchar")]
        public string Aciklama { get; set; }

        public DateTime Tarih { get; set; }

        [Column(TypeName = "nvarchar")]
        public string Basliq { get; set; }
        public string Paylaşan { get; set; }
    }
}