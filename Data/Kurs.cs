using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace efCoreApp.Data
{
    public class Kurs
    {
        public int KursId { get; set; }
        public string? Baslik { get; set; }

        public int? OgretmenId { get; set; } // Yapı değişikliği varsa migration kullanılır. Bu prop da null yapılır öncesinde OgretmenId yok çünkü
        public Ogretmen Ogretmen { get; set; }
        public ICollection<KursKayit> KursKayitlari { get; set; } = new List<KursKayit>();

    }
}