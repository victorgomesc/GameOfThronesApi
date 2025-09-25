using GameOfThronesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GameOfThronesAPI.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Stronghold> Strongholds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<House>()
                .HasMany(h => h.Vassalos)
                .WithOne(h => h.OverlordHouse)
                .HasForeignKey(h => h.OverlordHouseId);

            modelBuilder.Entity<House>()
                .HasMany(h => h.Characters)
                .WithOne(c => c.House)
                .HasForeignKey(c => c.HouseId);

            modelBuilder.Entity<House>()
                .HasOne(h => h.Stronghold)
                .WithOne(s => s.House)
                .HasForeignKey<Stronghold>(s => s.HouseId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
