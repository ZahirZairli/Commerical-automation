using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UdemyMurat.Models.Sınıflar
{
    public class Detay
    {
        [Key]
        public int  DetayId { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(50,ErrorMessage ="Maksimum 50 karekter olmalidir")]
        [Required]
        public string  urunad { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(2000, ErrorMessage = "Maksimum 2000 karekter olmalidir")]
        public string  urunbilgi { get; set; }
    }
}