
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
        }
    }
}
