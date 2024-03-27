using KamtkSchedule.Domain.Dtos;
using KamtkSchedule.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KamtkSchedule.Infrastructure.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Group>? Groups { get; set; }
        public DbSet<Teacher>? Teachers { get; set; }
        public DbSet<Pair>? Pairs { get; set; }
        public DbSet<ScheduleDay>? ScheduleDays { get; set; }
        public DbSet<GroupSchedule>? GroupSchedules { get; set; }
        public DbSet<WeeklySchedule>? WeeklySchedules { get; set; }
        public DbSet<Cabinet>? Cabinets { get; set; }
        public DbSet<Discipline>? Disciplines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pair>(e =>
            {
                e.HasMany(e => e.Teachers)
                .WithMany(e => e.Pairs)
                .UsingEntity("TeacherPairs");

                e.HasMany(e => e.Cabinets)
                .WithMany(e => e.Pairs)
                .UsingEntity("PairCabinets");
            });

            modelBuilder.Entity<Discipline>(e =>
            {
                e.HasMany(e => e.Groups)
                .WithMany(e => e.Disciplines)
                .UsingEntity("GroupDisciplines");

                e.HasMany(e => e.Teachers)
                .WithMany(e => e.Disciplines)
                .UsingEntity("TeacherDisciplines");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
