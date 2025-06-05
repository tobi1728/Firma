using Microsoft.EntityFrameworkCore;
using Firma.PortalWWW.Models;

namespace Firma.PortalWWW.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Horse> Horses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Rider> Riders { get; set; }
        public DbSet<UpcomingRide> UpcomingRides { get; set; }

    }
}
