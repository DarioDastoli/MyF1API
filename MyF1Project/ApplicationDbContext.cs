using Microsoft.EntityFrameworkCore;
using MyF1Project.Models;

namespace MyF1Project
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Driver> Drivers => Set<Driver>();
        public DbSet<Race> Races => Set<Race>();
        public DbSet<Laptime> laptimes => Set<Laptime>();
    }
}
