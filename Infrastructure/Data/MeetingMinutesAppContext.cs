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

        public MeetingMinutesAppContext(DbContextOptions<MeetingMinutesAppContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure primary keys
            modelBuilder.Entity<MeetingType>().HasKey(mt => mt.Id);
            modelBuilder.Entity<Meeting>().HasKey(m => m.Id);
            modelBuilder.Entity<MeetingItem>().HasKey(mi => mi.Id);
            modelBuilder.Entity<MeetingItemStatus>().HasKey(mis => mis.Id);

            // Configure relationships
            modelBuilder.Entity<Meeting>()
                .HasOne(m => m.MeetingType)
                .WithMany()
                .HasForeignKey(m => m.MeetingTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MeetingItem>()
                .HasOne(mi => mi.Meeting)
                .WithMany(m => m.MeetingItems)
                .HasForeignKey(mi => mi.MeetingId)
                .OnDelete(DeleteBehavior.Cascade);

            // Add indexes
            modelBuilder.Entity<Meeting>()
                .HasIndex(m => m.MeetingTypeId)
                .HasDatabaseName("IX_Meeting_MeetingTypeId");

            modelBuilder.Entity<MeetingItem>()
                .HasIndex(mi => mi.MeetingId)
                .HasDatabaseName("IX_MeetingItem_MeetingId");
        }
    }
}