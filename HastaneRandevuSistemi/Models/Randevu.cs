using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HastaneRandevuSistemi.Models
{
    public class Randevu
    {
        [Key]
        public int RandevuID { get; set; }
        public string RandevuGun { get; set; }
        public string RandevuSaat { get; set; }


        [ForeignKey("DoktorID")]
        public int? DoktorID { get; set; }
        public Doktor? Doktor { get; set; }


        [ForeignKey("PoliklinikID")]
        public int? PoliklinikID { get; set; }
        public Poliklinik? Poliklinik { get; set; }


        [ForeignKey("HastaneID")]
        public int? HastaneID { get; set; }
        public Hastane? Hastane { get; set; }
    }
}
