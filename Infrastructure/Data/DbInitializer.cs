using MeetingMinutesApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace MeetingMinutesApp.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Initialize(MeetingMinutesAppContext context)
        {
            try
            {
                context.Database.Migrate();

                // Check if the database is already seeded
                if (context.MeetingTypes.Any() && context.MeetingItemStatuses.Any())
                {
                    Console.WriteLine("Database has already been seeded.");
                    return; // DB has been seeded
                }

                // Seed MeetingTypes
                var meetingTypes = new MeetingType[]
                {
                    new MeetingType { Name = "Manco" },
                    new MeetingType { Name = "Financial" },
                    new MeetingType { Name = "PTL" }
                };

                context.MeetingTypes.AddRange(meetingTypes);

                // Seed MeetingItemStatuses
                var meetingItemStatuses = new MeetingItemStatus[]
                {
                    new MeetingItemStatus { Status = "Open" },
                    new MeetingItemStatus { Status = "Closed" },
                    new MeetingItemStatus { Status = "In Progress" },
                    new MeetingItemStatus { Status = "Carry Forward" }
                };

                context.MeetingItemStatuses.AddRange(meetingItemStatuses);

                context.SaveChanges();
                Console.WriteLine("Database seeding completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + "An error occurred while initializing the database.");
                throw;
            }
        }
    }
}