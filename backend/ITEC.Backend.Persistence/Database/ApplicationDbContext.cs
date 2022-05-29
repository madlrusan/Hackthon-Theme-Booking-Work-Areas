
using ITEC.Backend.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ITEC.Backend.Persistence.Database
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Office> Offices { get; set; }
        public DbSet<UploadedFile> UploadedFiles { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<Desk> Desks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Office>(b =>
            {
                b.HasKey(o => o.Id);
                b.Property(o => o.Name)
                    .HasMaxLength(250)
                    .IsUnicode()
                    .IsRequired();
                b.Property(o => o.CreatedAtTimeUtc)
                    .IsRequired();
                b.Property(o => o.CreatedByUserId)
                    .IsRequired();
                b.HasOne(o => o.CreatedByUser);
            });

            modelBuilder.Entity<UploadedFile>(b =>
            {
                b.HasKey(o => o.Id);
                b.Property(o => o.Name)
                    .HasMaxLength(200)
                    .IsRequired();
                b.Property(o => o.Path)
                    .HasMaxLength(250)
                    .IsRequired();
                b.Property(o => o.CreatedAtTimeUtc)
                    .IsRequired();
                b.Property(o => o.CreatedByUserId)
                    .IsRequired();
                b.HasOne(o => o.CreatedByUser);
            });

            modelBuilder.Entity<Floor>(b =>
            {
                b.HasKey(o => o.Id);
                b.Property(o => o.Name)
                    .HasMaxLength(250)
                    .IsUnicode()
                    .IsRequired();
                b.Property(o => o.CreatedAtTimeUtc)
                    .IsRequired();
                b.Property(o => o.Rows)
                    .IsRequired();
                b.Property(o => o.Columns)
                    .IsRequired();
                b.Property(o => o.CreatedByUserId)
                    .IsRequired();
                b.Property(o => o.OfficeId)
                    .IsRequired();
                b.HasOne(o => o.CreatedByUser);
                b.HasOne(o => o.Office).WithMany(o => o.Floors).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Desk>(b =>
            {
                b.HasKey(o => o.Id);
                b.Property(o => o.Name)
                    .HasMaxLength(250)
                    .IsUnicode()
                    .IsRequired();
                b.Property(o => o.Order)
                    .IsRequired();
                b.Property(o => o.CreatedAtTimeUtc)
                    .IsRequired();
                b.Property(o => o.CreatedByUserId)
                    .IsRequired();
                b.Property(o => o.FloorId)
                    .IsRequired();
                b.HasOne(o => o.CreatedByUser);
                b.HasOne(o => o.Floor).WithMany(o => o.Desks).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<DeskReservation>(b =>
            {
                b.HasKey(o => o.Id);
                b.Property(o => o.CreatedAtTimeUtc)
                    .IsRequired();
                b.Property(o => o.CreatedByUserId)
                    .IsRequired();
                b.Property(o => o.DeskId)
                    .IsRequired();
                b.Property(o => o.ReservationDate)
                    .IsRequired();
                b.HasOne(o => o.CreatedByUser);
                b.HasOne(o => o.Desk).WithMany(o => o.Reservations).OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
