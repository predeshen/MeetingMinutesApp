using MeetingMinutesApp.Core.Entities;
using MeetingMinutesApp.Infrastructure.Data;

namespace MeetingMinutesApp.Application.UseCases
{
    public class UpdateMeetingItemStatus
    {
        private readonly MeetingMinutesAppContext _context;

        public UpdateMeetingItemStatus(MeetingMinutesAppContext context)
        {
            _context = context;
        }

        public async Task ExecuteAsync(int meetingItemId, string status, int responsiblePersonId)
        {
            var meetingItemStatus = new MeetingItemStatus
            {
                MeetingItemId = meetingItemId,
                Status = status,
                ResponsiblePersonId = responsiblePersonId
            };

            _context.MeetingItemStatuses.Add(meetingItemStatus);
            await _context.SaveChangesAsync();
        }
    }
}
