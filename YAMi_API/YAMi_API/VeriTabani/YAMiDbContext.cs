using Microsoft.EntityFrameworkCore;
using YAMi_API.Siniflar;

namespace YAMi_API.VeriTabani
{
    public class YAMiDbContext : DbContext
    {
        public DbSet<Musteri> Musteri { get; set; }
        public DbSet<Personel> Personel { get; set; }
        public DbSet<MeyveVeSebze> MeyveVeSebze { get; set; }
        public DbSet<Kozmetik> Kozmetik { get; set; }
        public DbSet<Pastane> Pastane { get; set; }
        public DbSet<YiyecekVeIcecek> YiyecekVeIcecek { get; set; }
        public DbSet<Sepet> Sepet { get; set; }

        public YAMiDbContext(DbContextOptions<YAMiDbContext> options) : base(options)
        {
        }

        // Optional: Keep OnConfiguring as a fallback, but it should not be configured if options are provided
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=YAMiDb;User Id=YAMi;Password=YAMiSanalMarket3435;TrustServerCertificate=True");
            }
        }
    }
}