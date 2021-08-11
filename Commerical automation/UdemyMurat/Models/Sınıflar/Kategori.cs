using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UdemyMurat.Models.Sınıflar
{
    public class Kategori
    {
        [Key]
        public int KategoriID { get; set; }

        [Required(ErrorMessage ="kateqoriya adi bos ola bilmez")]
        [Column(TypeName = "Varchar")]
        [StringLength(30,ErrorMessage ="Maksimum 30 herfden ibaret olmalidir.")]
        public string KategoriAd { get; set; }
        public ICollection<Urun> Uruns { get; set; }
    }
}