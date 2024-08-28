using FluentValidation;
using MeetingMinutesApp.Presentation.Models;

namespace MeetingMinutesApp.Application.Validators
{
    public class UpdateMeetingItemStatusValidator : AbstractValidator<UpdateMeetingItemStatusRequest>
    {
        public UpdateMeetingItemStatusValidator()
        {
            RuleFor(x => x.StatusId).GreaterThan(0).WithMessage("Invalid status ID.");
            RuleFor(x => x.Status).NotEmpty().WithMessage("Status cannot be empty.");
        }
    }
}