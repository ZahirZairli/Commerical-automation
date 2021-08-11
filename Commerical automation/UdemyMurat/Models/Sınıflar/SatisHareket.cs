using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UdemyMurat.Models.Sınıflar
{
    public class SatisHareket
    {
        [Key]
        public int SatisId { get; set; }
        public DateTime Tarih { get; set; }
        public int Adet { get; set; }
        public decimal Fiyat { get; set; }
        public decimal ToplamTutar { get; set; }
        public int UrunId { get; set; }
        public virtual Urun Urun { get; set; }

        [Display(Name = "Müştəri Id")]
        public int CariId { get; set; }
        public virtual Cariler Cariler { get; set; }

        [Display(Name = "İşçi adı")]
        public int PersonelId { get; set; }
        public virtual Personel Personel { get; set; }
    }
}