using MeetingMinutesApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingMinutesApp.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Initialize(MeetingMinutesAppContext context)
        {
            // Check if db exists
            context.Database.EnsureCreated();

            // Apply any migrations
            context.Database.Migrate();

            // Seed initial data if empty
            if (!context.MeetingTypes.Any())
            {
                var meetingTypes = new MeetingType[]
                {
                new MeetingType { MeetingTypeId = 1, Name = "MANCO" },
                new MeetingType { MeetingTypeId = 2, Name = "Finance" },
                new MeetingType { MeetingTypeId = 3, Name = "Project Team Leaders" }
                };

                foreach (var mt in meetingTypes)
                {
                    context.MeetingTypes.Add(mt);
                }
            }

            if (!context.Persons.Any())
            {
                var persons = new Person[]
                {
                new Person { PersonId = 1, Name = "John Doe" },
                new Person { PersonId = 2, Name = "Jane Smith" }
                };

                foreach (var p in persons)
                {
                    context.Persons.Add(p);
                }
            }

            context.SaveChanges();
        }
    }
}
