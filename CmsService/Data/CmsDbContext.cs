using Microsoft.EntityFrameworkCore;
using CmsService.Models;

namespace CmsService.Data
{
    public class CmsDbContext : DbContext
    {
        public CmsDbContext(DbContextOptions<CmsDbContext> options)
            : base(options)
        {
        }

        public DbSet<CmsEntry> CmsEntries { get; set; }
    }
}