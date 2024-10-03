using KoiBet.Entities;  
using Microsoft.EntityFrameworkCore;

namespace KoiBet.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
                .HasOne<Roles>(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.role_id)
                .OnDelete(DeleteBehavior.Restrict);
        }


    }
}
