using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DancerFit.Data
{
    public class AppDbcontext : IdentityDbContext
    {
        public AppDbcontext(DbContextOptions<AppDbcontext> options) : base(options)
        {
        }

        public DbSet<Models.ApplicationUser> Users { get; set; }
        public DbSet<Models.Package> Packages { get; set; }
        public DbSet<Models.Category> Categories { get; set; }
        public DbSet<Models.Trainer> Trainers { get; set; }
        public DbSet<Models.DanceClass> DanceClasses { get; set; }
        public DbSet<Models.Dancer> Dancers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the relationships and other settings here if needed
            base.OnModelCreating(modelBuilder);
        }
    }
}
