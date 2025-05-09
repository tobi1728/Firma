using System;
using Firma.Intranet.Models;
using Microsoft.EntityFrameworkCore;

namespace Firma.Intranet.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Horse> Horses { get; set; }
        public DbSet<HorseCheckup> HorseCheckups { get; set; }
        public DbSet<Rider> Riders { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<UpcomingRide> UpcomingRides { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
         => optionsBuilder.UseSqlServer("Server=DESKTOP-ASO4VT2\\SQLEXPRESS;Database=BlueKnightDb;Trusted_Connection=True;TrustServerCertificate=True");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Konfiguracja dla tabeli Horse
            modelBuilder.Entity<Horse>(entity =>
            {
                entity.ToTable("horses");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Breed)
                    .HasMaxLength(50);

                entity.Property(e => e.Status)
                    .HasMaxLength(30);

                entity.Property(e => e.LastCheckup)
                    .HasColumnType("date");
            });

           

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
