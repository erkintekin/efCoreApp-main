using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace efCoreApp.Data
{
    public class Ogrenci
    {
        // id anlamına gelen KEY

        [Key]
        public int OgrenciId { get; set; }

        [Required(ErrorMessage = "Öğrenci Adı zorunludur.")]
        public string? OgrenciAd { get; set; }

        [Required(ErrorMessage = "Öğrenci Soyadı zorunludur.")]
        public string? OgrenciSoyad { get; set; }

        [Required(ErrorMessage = "E-posta zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçersiz e-posta adresi.")]
        public string? Eposta { get; set; }
        public string AdSoyad
        {
            get
            {
                return this.OgrenciAd + " " + this.OgrenciSoyad;
            }

        }
        public string? Telefon { get; set; }

        public ICollection<KursKayit> KursKayitlar { get; set; } = null!;

    }
}