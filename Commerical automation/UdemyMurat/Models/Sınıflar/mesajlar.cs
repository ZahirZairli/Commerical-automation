using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UdemyMurat.Models.Sınıflar
{
    public class mesajlar
    {
        [Key]
        public int MesajId { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(50, ErrorMessage = "Maksimum 50 herfden ibaret olmalidir.")]
        public string Gonderici { get; set; }

        [Required(ErrorMessage = "Alıcı bos ola bilmez")]
        [Column(TypeName = "nvarchar")]
        [StringLength(50, ErrorMessage = "Maksimum 50 herfden ibaret olmalidir.")]
        public string Alici { get; set; }

        [Required(ErrorMessage = "Konu adi bos ola bilmez")]
        [Column(TypeName = "nvarchar")]
        [StringLength(50, ErrorMessage = "Maksimum 50 herfden ibaret olmalidir.")]
        public string Konu { get; set; }

        [Required(ErrorMessage = "İçerik bos ola bilmez")]
        [Column(TypeName = "nvarchar")]
        [StringLength(2000, MinimumLength = 20, ErrorMessage = "Maksimum 2000 herfden minimum ise 20 herfden ibaret olmalidir.")]
        public string Icerik { get; set; }

        public bool Durum{ get; set; }

        [Column(TypeName = "Smalldatetime")]
        public DateTime Tarix { get; set; }
    }
}