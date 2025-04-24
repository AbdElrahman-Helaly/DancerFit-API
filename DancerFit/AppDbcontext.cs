using Microsoft.EntityFrameworkCore;

namespace DancerFit
{
    public class AppDbcontext :DbContext
    {
        public AppDbcontext(DbContextOptions<AppDbcontext> options) : base(options)
        {
        }

        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.Package> Packages { get; set; }
        public DbSet<Models.Category> Categories { get; set; }
        public DbSet<Models.Trainer> Trainers { get; set; }
        public DbSet<Models.DanceClass> DanceClasses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the relationships and other settings here if needed
            base.OnModelCreating(modelBuilder);
        }
    }
}
