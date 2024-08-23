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
            //To Do : Add relationships and constraints
        }
    }
}
