using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HastaneRandevuSistemi.Models
{
    public class Doktor
    {
        [Key]
        public int DoktorID { get; set; }
        public string DoktorAd { get; set; }
        public string DoktorSoyad { get; set; }


        [ForeignKey("PoliklinikID")]
        public int? PoliklinikID { get; set; }
        public Poliklinik? Poliklinik { get; set; }


        [ForeignKey("HastaneID")]
        public int? HastaneID { get; set; }
        public Hastane? Hastane { get; set; }

        public ICollection<Randevu> RandevuList { get; } = new List<Randevu>();
    }
}
