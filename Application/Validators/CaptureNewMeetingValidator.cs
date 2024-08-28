using FluentValidation;
using System;

namespace MeetingMinutesApp.Application.Validators
{
    public class CaptureNewMeetingValidator : AbstractValidator<CaptureNewMeetingRequest>
    {
        public CaptureNewMeetingValidator()
        {
            RuleFor(x => x.MeetingTypeId).GreaterThan(0).WithMessage("Invalid meeting type ID.");
            RuleFor(x => x.Date).NotEqual(default(DateTime)).WithMessage("Invalid date.");
            RuleFor(x => x.Time).NotEqual(default(TimeSpan)).WithMessage("Invalid time.");
        }
    }

    public class CaptureNewMeetingRequest
    {
        public int MeetingTypeId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
    }
}