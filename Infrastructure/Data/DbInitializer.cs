using MeetingMinutesApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MeetingMinutesApp.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Initialize(MeetingMinutesAppContext context, ILogger logger)
        {
            try
            {
                // Apply migrations
                context.Database.Migrate();

                // Seed data
                if (!context.MeetingTypes.Any())
                {
                    var meetingTypes = new MeetingType[]
                    {
                        new MeetingType { MeetingTypeId = 1, Name = "MANCO" },
                        new MeetingType { MeetingTypeId = 2, Name = "Finance" },
                        new MeetingType { MeetingTypeId = 3, Name = "Project Team Leaders" }
                    };

                    context.MeetingTypes.AddRange(meetingTypes);
                }

                if (!context.Persons.Any())
                {
                    var persons = new Person[]
                    {
                        new Person { PersonId = 1, Name = "John Doe" },
                        new Person { PersonId = 2, Name = "Jane Smith" }
                    };

                    context.Persons.AddRange(persons);
                }

                context.SaveChanges();
                logger.LogInformation("Database initialized and seeded successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while initializing the database.");
                throw;
            }
        }
    }
}
