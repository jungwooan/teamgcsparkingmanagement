using Microsoft.EntityFrameworkCore;
using ParkingManagement.Models;

namespace ParkingManagement.Data
{
    public class ParkingDbContext : DbContext
    {
        public ParkingDbContext(DbContextOptions<ParkingDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ParkingUsage> ParkingUsages { get; set; }
        public DbSet<ParkingReturn> ParkingReturns { get; set; }
        public DbSet<ExternalParking> ExternalParkings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.KakaoId).IsUnique();
            });

            // ParkingUsage configuration
            modelBuilder.Entity<ParkingUsage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.User)
                    .WithMany(u => u.ParkingUsages)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // ParkingReturn configuration
            modelBuilder.Entity<ParkingReturn>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.User)
                    .WithMany(u => u.ParkingReturns)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.RelatedUsage)
                    .WithMany()
                    .HasForeignKey(e => e.RelatedUsageId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // ExternalParking configuration
            modelBuilder.Entity<ExternalParking>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.User)
                    .WithMany(u => u.ExternalParkings)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
