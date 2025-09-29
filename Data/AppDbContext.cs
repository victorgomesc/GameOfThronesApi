using GameOfThronesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GameOfThronesAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Character> Characters { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Stronghold> Strongholds { get; set; }

        // 👇 ADICIONE ESTA LINHA
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // --- Seus relacionamentos já existentes ---
            // Ex.: House 1–1 Stronghold
            modelBuilder.Entity<House>()
                .HasOne(h => h.Stronghold)
                .WithOne(s => s.House)
                .HasForeignKey<Stronghold>(s => s.HouseId);

            // Ex.: House 1–N Characters
            modelBuilder.Entity<House>()
                .HasMany(h => h.Characters)
                .WithOne(c => c.House)
                .HasForeignKey(c => c.HouseId);

            // Ex.: Vassalos (self reference) 1–N
            modelBuilder.Entity<House>()
                .HasMany(h => h.Vassalos)
                .WithOne(h => h.OverlordHouse)
                .HasForeignKey(h => h.OverlordHouseId);

            // 👇 (Opcional, mas recomendado) Regras para User
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username).IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.Email).HasMaxLength(256).IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Username).HasMaxLength(64).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
