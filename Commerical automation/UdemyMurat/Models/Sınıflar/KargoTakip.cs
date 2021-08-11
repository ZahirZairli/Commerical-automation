using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UdemyMurat.Models.Sınıflar
{
    public class KargoTakip
    {
        [Key]
        public int KargoTakipId { get; set; }

        [Column(TypeName="nvarchar")]
        [StringLength(10, ErrorMessage = "Maksimum 10 herfden ibaret olmalidir")]
        public string TakipKodu { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(100, ErrorMessage = "Maksimum 100 herfden ibaret olmalidir")]
        public string Aciklama { get; set; }

        public DateTime TarihZaman{ get; set; }
    }
}