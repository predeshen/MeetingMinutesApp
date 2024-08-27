using MeetingMinutesApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace MeetingMinutesApp.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Initialize(MeetingMinutesAppContext context)
        {
            context.Database.Migrate();

            // Check if the database is already seeded
            if (context.MeetingTypes.Any() && context.MeetingItemStatuses.Any())
            {
                return; // DB has been seeded
            }

            // Seed MeetingTypes
            var meetingTypes = new MeetingType[]
            {
                new MeetingType { Name = "Manco" },
                new MeetingType { Name = "Financial" },
                new MeetingType { Name = "PTL" }
            };

            foreach (var mt in meetingTypes)
            {
                context.MeetingTypes.Add(mt);
            }

            // Seed MeetingItemStatuses
            var meetingItemStatuses = new MeetingItemStatus[]
            {
                new MeetingItemStatus { Status = "Open" },
                new MeetingItemStatus { Status = "In Progress" },
                new MeetingItemStatus { Status = "Closed" }
            };

            foreach (var mis in meetingItemStatuses)
            {
                context.MeetingItemStatuses.Add(mis);
            }

            context.SaveChanges();
        }
    }
}
