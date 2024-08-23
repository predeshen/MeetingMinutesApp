// Application/UseCases/UpdateMeetingItemStatus.cs
using MeetingMinutesApp.Core.Entities;
using MeetingMinutesApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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
            var meetingItem = await _context.MeetingItems
                .Include(mi => mi.MeetingItemStatuses)
                .FirstOrDefaultAsync(mi => mi.MeetingItemId == meetingItemId);

            if (meetingItem == null)
            {
                throw new Exception("Meeting item not found");
            }

            var meetingItemStatus = meetingItem.MeetingItemStatuses
                .FirstOrDefault(mis => mis.ResponsiblePersonId == responsiblePersonId);

            if (meetingItemStatus == null)
            {
                meetingItemStatus = new MeetingItemStatus
                {
                    MeetingItemId = meetingItemId,
                    Status = status,
                    ResponsiblePersonId = responsiblePersonId
                };
                _context.MeetingItemStatuses.Add(meetingItemStatus);
            }
            else
            {
                meetingItemStatus.Status = status;
            }

            await _context.SaveChangesAsync();
        }
    }
}
