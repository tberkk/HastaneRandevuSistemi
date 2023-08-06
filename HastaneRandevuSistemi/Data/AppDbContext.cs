using HastaneRandevuSistemi.Models;
using Microsoft.EntityFrameworkCore;

namespace HastaneRandevuSistemi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Hastane> HastaneTable { get; set; }
        public DbSet<Poliklinik> PoliklinikTable { get; set; }
        public DbSet<Doktor> DoktorTable { get; set; }
        public DbSet<Randevu> RandevuTable { get; set; }


    }
}
