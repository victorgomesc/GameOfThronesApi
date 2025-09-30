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
        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<House>()
                .HasOne(h => h.Stronghold)
                .WithOne(s => s.House)
                .HasForeignKey<Stronghold>(s => s.HouseId);

            modelBuilder.Entity<House>()
                .HasMany(h => h.Characters)
                .WithOne(c => c.House)
                .HasForeignKey(c => c.HouseId);

             modelBuilder.Entity<House>()
        .HasMany(h => h.Vassalos)             
        .WithOne(h => h.OverlordHouse)          
        .HasForeignKey(h => h.OverlordHouseId);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username).IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.Email).HasMaxLength(256).IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Username).HasMaxLength(64).IsRequired();

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Characters)
                .WithMany();

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Houses)
                .WithMany();
        }
    }
}
