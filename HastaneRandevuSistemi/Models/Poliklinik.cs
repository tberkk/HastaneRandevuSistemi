using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HastaneRandevuSistemi.Models
{
    public class Poliklinik
    {
        [Key]
        public int PoliklinikID { get; set; }
        public string PoliklinikAd { get; set; }


        [ForeignKey("HastaneID")]
        public int? HastaneID { get; set; }
        public Hastane? Hastane { get; set; }


        public ICollection<Doktor> DoktorList { get; } = new List<Doktor>();

        public ICollection<Randevu> RandevuList { get; } = new List<Randevu>();
    }
}
