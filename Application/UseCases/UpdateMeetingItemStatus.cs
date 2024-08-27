using MeetingMinutesApp.Infrastructure.Data;
using MeetingMinutesApp.Core.Entities;

public class UpdateMeetingItemStatusRequest
{
    public int StatusId { get; set; }
    public string Status { get; set; }
}

public class UpdateMeetingItemStatus
{
    private readonly MeetingMinutesAppContext _context;

    public UpdateMeetingItemStatus(MeetingMinutesAppContext context)
    {
        _context = context;
    }

    public async Task ExecuteAsync(int meetingItemId, UpdateMeetingItemStatusRequest request)
    {
        var meetingItem = await _context.MeetingItems.FindAsync(meetingItemId);
        if (meetingItem == null)
        {
            throw new Exception("Meeting item not found");
        }

        meetingItem.MeetingItemStatusId = request.StatusId;
        
        await _context.SaveChangesAsync();
    }
}
