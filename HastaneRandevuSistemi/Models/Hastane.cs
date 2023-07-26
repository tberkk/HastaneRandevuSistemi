using System.ComponentModel.DataAnnotations;

namespace HastaneRandevuSistemi.Models
{
    public class Hastane
    {
        [Key]
        public int HastaneID { get; set; }
        public string HastaneAd { get; set; }


        public ICollection<Poliklinik> PoliklinikList { get; } = new List<Poliklinik>();

        public ICollection<Doktor> DoktorList { get; } = new List<Doktor>();

        public ICollection<Randevu> RandevuList { get; } = new List<Randevu>();
    }
}
