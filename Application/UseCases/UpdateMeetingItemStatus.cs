using System;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Logging;
using MeetingMinutesApp.Infrastructure.Data;
using MeetingMinutesApp.Core.Entities;
using MeetingMinutesApp.Application.Validators;
using MeetingMinutesApp.Presentation.Models;

public class UpdateMeetingItemStatusRequest
{
    public int StatusId { get; set; }
    public string Status { get; set; }
}

public class UpdateMeetingItemStatus
{
    private readonly MeetingMinutesAppContext _context;
    private readonly ILogger<UpdateMeetingItemStatus> _logger;
    private readonly IValidator<UpdateMeetingItemStatusRequest> _validator;

    public UpdateMeetingItemStatus(MeetingMinutesAppContext context, ILogger<UpdateMeetingItemStatus> logger, IValidator<UpdateMeetingItemStatusRequest> validator)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task ExecuteAsync(int meetingItemId, UpdateMeetingItemStatusRequest request)
    {
        if (meetingItemId <= 0)
        {
            throw new ArgumentException("Invalid meeting item ID.", nameof(meetingItemId));
        }

        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
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