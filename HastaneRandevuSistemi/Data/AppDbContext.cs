using HastaneRandevuSistemi.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace HastaneRandevuSistemi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Hastane> HastaneTable { get; set; }
        public DbSet<Poliklinik> PoliklinikTable { get; set; }
        public DbSet<Doktor> DoktorTable { get; set; }
        public DbSet<Randevu> RandevuTable { get; set; }
        public DbSet<User> UserTable { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB; database=HastaneDB; Integrated Security=false; TrustServerCertificate=True; Trusted_Connection=True; MultipleActiveResultSets=true;");
        }
    }
}
