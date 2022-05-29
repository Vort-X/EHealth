using EHealth.Entity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace EHealth.DataAccess
{
    public class EHealthDbContext : DbContext
    {
        public EHealthDbContext([NotNull] DbContextOptions<EHealthDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<AppointmentTimeEntity> AppointmentTimes { get; private set; }
        public DbSet<DoctorEntity> Doctors { get; private set; }
        public DbSet<HistoryEntity> Histories { get; private set; }
        public DbSet<OccupationEntity> Occupations { get; private set; }
        public DbSet<StatusEntity> Statuses { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DoctorEntity>().HasMany(d => d.Occupations).WithMany(o => o.Doctors);
            modelBuilder.Entity<DoctorEntity>().HasMany(d => d.AvailableAppointmentTime).WithMany(t => t.AvailableDoctors);
            modelBuilder.Entity<HistoryEntity>().HasOne(h => h.AppointmentDateTime).WithMany();
            modelBuilder.Entity<HistoryEntity>().HasOne(h => h.Status).WithMany();
        }
    }
}
