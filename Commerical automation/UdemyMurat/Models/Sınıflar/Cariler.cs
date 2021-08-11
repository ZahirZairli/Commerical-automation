using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UdemyMurat.Models.Sınıflar
{
    public class Cariler
    {
        [Key]
        public int CariId { get; set; }


        [Display(Name = "Müştəri adı")]
        [Column(TypeName = "Varchar")]
        [StringLength(30, ErrorMessage ="Adiniz maksiumum 30 herfden ibaret olmalidir")]
        [Required(ErrorMessage = "Adinizi daxil etmelisiniz")]
        public string CariAd { get; set; }

        [Display(Name = "Müştəri soy adı")]
        [Column(TypeName = "nvarchar")]
        [StringLength(30)]
        [Required(ErrorMessage ="Bu yer bos ola bilmez")]
        public string CariSoyad { get; set; }

        [Display(Name = "Müştəri Şəhər")]
        [Column(TypeName = "nvarchar")]
        [StringLength(20)]
        public string CariSehir { get; set; }


        [Display(Name = "Müştəri mail")]
        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        [Required(ErrorMessage = "Bura bos ola bilmez")]
        public string CariMail{ get; set; }

        [Display(Name = "Müştəri şifre")]
        [Column(TypeName = "nvarchar")]
        [StringLength(50,ErrorMessage ="Maksimum 50 simvolluk paroldan istifade edin")]
        [Required(ErrorMessage ="Bura bos ola bilmez")]
        public string CariSifre { get; set; }

        public bool Durum { get; set; }
        public ICollection<SatisHareket> SatisHarekets { get; set; }
    }
}