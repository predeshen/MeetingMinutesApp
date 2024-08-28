using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MeetingMinutesApp.Infrastructure.Data;
using MeetingMinutesApp.Core.Entities;

namespace MeetingMinutesApp.Application.UseCases
{
    public class CaptureNewMeeting
    {
        private readonly MeetingMinutesAppContext _context;
        private readonly ILogger<CaptureNewMeeting> _logger;

        public CaptureNewMeeting(MeetingMinutesAppContext context, ILogger<CaptureNewMeeting> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task ExecuteAsync(int meetingTypeId, DateTime date, TimeSpan time)
        {
            if (meetingTypeId <= 0)
            {
                throw new ArgumentException("Invalid meeting type ID.", nameof(meetingTypeId));
            }

            if (date == default)
            {
                throw new ArgumentException("Invalid date.", nameof(date));
            }

            if (time == default)
            {
                throw new ArgumentException("Invalid time.", nameof(time));
            }

            try
            {
                var meeting = new Meeting
                {
                    MeetingTypeId = meetingTypeId,
                    Date = date,
                    Time = time
                };

                _context.Meetings.Add(meeting);
                await _context.SaveChangesAsync();

                _logger.LogInformation("New meeting captured successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while capturing the new meeting.");
                throw;
            }
        }
    }
}