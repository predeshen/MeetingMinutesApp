using MeetingMinutesApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingMinutesApp.Infrastructure.Data
{
    public class MeetingMinutesAppContext : DbContext
    {
        public DbSet<MeetingType> MeetingTypes { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<MeetingItem> MeetingItems { get; set; }
        public DbSet<MeetingItemStatus> MeetingItemStatuses { get; set; }
        public DbSet<Person> Persons { get; set; }

        public MeetingMinutesAppContext(DbContextOptions<MeetingMinutesAppContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed initial data
            modelBuilder.Entity<MeetingType>().HasData(
                new MeetingType { MeetingTypeId = 1, Name = "MANCO" },
                new MeetingType { MeetingTypeId = 2, Name = "Finance" },
                new MeetingType { MeetingTypeId = 3, Name = "Project Team Leaders" }
            );

            modelBuilder.Entity<Person>().HasData(
                new Person { PersonId = 1, Name = "John Doe" },
                new Person { PersonId = 2, Name = "Jane Smith" }
            );
        }
    }
}
