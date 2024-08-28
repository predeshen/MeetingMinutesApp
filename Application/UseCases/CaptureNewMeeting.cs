using System;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Logging;
using MeetingMinutesApp.Infrastructure.Data;
using MeetingMinutesApp.Core.Entities;
using MeetingMinutesApp.Application.Validators;

namespace MeetingMinutesApp.Application.UseCases
{
    public class CaptureNewMeeting
    {
        private readonly MeetingMinutesAppContext _context;
        private readonly ILogger<CaptureNewMeeting> _logger;
        private readonly IValidator<CaptureNewMeetingRequest> _validator;

        public CaptureNewMeeting(MeetingMinutesAppContext context, ILogger<CaptureNewMeeting> logger, IValidator<CaptureNewMeetingRequest> validator)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task ExecuteAsync(CaptureNewMeetingRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            try
            {
                var meeting = new Meeting
                {
                    MeetingTypeId = request.MeetingTypeId,
                    Date = request.Date,
                    Time = request.Time
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