using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UdemyMurat.Models.Sınıflar
{
    public class KargoDetay
    {
        [Key]
        public int KargoDetayId { get; set; }

        [Required(ErrorMessage = "Bura boş qala bilməz")]
        [Column(TypeName = "nvarchar")]
        [StringLength(300, ErrorMessage = "Maksimum 300 herfden ibaret olmalidir")]
        public string Aciklama { get; set; }

        [Required(ErrorMessage = "Bura boş qala bilməz")]
        [Column(TypeName = "nvarchar")]
        [StringLength(10, ErrorMessage = "Maksimum 10 herfden ibaret olmalidir")]
        public string TakipKodu { get; set; }

        [Required(ErrorMessage = "Bura boş qala bilməz")]
        [Column(TypeName = "nvarchar")]
        [StringLength(20, ErrorMessage = "Maksimum 20 herfden ibaret olmalidir")]
        public string Personel { get; set; }

        [Required(ErrorMessage = "Bura boş qala bilməz")]
        [Column(TypeName = "nvarchar")]
        [StringLength(25, ErrorMessage = "Maksimum 25 herfden ibaret olmalidir")]
        public string Alici { get; set; }

        [Required(ErrorMessage = "Bura boş qala bilməz")]
        [Column(TypeName = "nvarchar")]
        public string AliciMail { get; set; }

        public DateTime Tarih { get; set; }
    }
}