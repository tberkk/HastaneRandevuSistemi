using System.ComponentModel.DataAnnotations;

namespace HastaneRandevuSistemi.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [StringLength(20)]
        public string KullaniciAdi { get; set; }

        [StringLength(20)]
        public string Sifre { get; set; }

        [StringLength(1)]
        public string Rol { get; set; }
    }
}
