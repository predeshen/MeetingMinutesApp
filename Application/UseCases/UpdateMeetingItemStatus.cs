using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
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
    private readonly ILogger<UpdateMeetingItemStatus> _logger;

    public UpdateMeetingItemStatus(MeetingMinutesAppContext context, ILogger<UpdateMeetingItemStatus> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task ExecuteAsync(int meetingItemId, UpdateMeetingItemStatusRequest request)
    {
        if (meetingItemId <= 0)
        {
            throw new ArgumentException("Invalid meeting item ID.", nameof(meetingItemId));
        }

        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        try
        {
            var meetingItem = await _context.MeetingItems.FindAsync(meetingItemId);
            if (meetingItem == null)
            {
                throw new KeyNotFoundException("Meeting item not found.");
            }

            meetingItem.MeetingItemStatusId = request.StatusId;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Meeting item status updated successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the meeting item status.");
            throw;
        }
    }
}