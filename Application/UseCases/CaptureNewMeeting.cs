using MeetingMinutesApp.Core.Entities;
using MeetingMinutesApp.Infrastructure.Data;

namespace MeetingMinutesApp.Application.UseCases
{
    public class CaptureNewMeeting
    {
        private readonly MeetingMinutesAppContext _context;

        public CaptureNewMeeting(MeetingMinutesAppContext context)
        {
            _context = context;
        }

        public async Task ExecuteAsync(int meetingTypeId, DateTime date, TimeSpan time)
        {
            var meeting = new Meeting
            {
                MeetingTypeId = meetingTypeId,
                Date = date,
                Time = time
            };

            _context.Meetings.Add(meeting);
            await _context.SaveChangesAsync();
        }
    }
}
