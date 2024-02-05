using Api.Temperature.Data.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Api.Temperature.Data.Context
{
    public class IdentityDBContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public IdentityDBContext(DbContextOptions<IdentityDBContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(e => e.Token);
                entity.Property(e => e.Token).ValueGeneratedNever();
                entity.HasOne(e => e.User)
                      .WithMany()
                      .HasForeignKey(e => e.UserId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);
            });

        }
    }
}
