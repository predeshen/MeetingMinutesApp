using MeetingMinutesApp.Core.Entities;
using MeetingMinutesApp.Infrastructure.Data;
using MeetingMinutesApp.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace MeetingMinutesApp.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MeetingsController : ControllerBase
    {
        private readonly MeetingMinutesAppContext _context;

        public MeetingsController(MeetingMinutesAppContext context)
        {
            _context = context;
        }

        [HttpPost("captureNewMeeting")]
        public async Task<IActionResult> CaptureNewMeeting([FromBody] CaptureNewMeetingRequest request)
        {
            var meeting = new Meeting
            {
                MeetingTypeId = request.MeetingTypeId,
                Date = DateTime.Parse(request.Date),
                Time = TimeSpan.Parse(request.Time),
                MeetingItems = new List<MeetingItem>()
            };

            // Add previous open items with updated statuses
            foreach (var item in request.PreviousOpenItems)
            {
                var meetingItem = new MeetingItem
                {
                    Description = item.Description,
                    DueDate = item.DueDate,
                    PersonResponsible = item.PersonResponsible
                };
                meeting.MeetingItems.Add(meetingItem);
            }

            // Add new items from the request
            foreach (var item in request.NewMeetingItems)
            {
                meeting.MeetingItems.Add(new MeetingItem
                {
                    Description = item.Description,
                    DueDate = item.DueDate,
                    PersonResponsible = item.PersonResponsible,
                    MeetingItemStatusId = 1
                });
            }

            _context.Meetings.Add(meeting);
            await _context.SaveChangesAsync();

            return Ok(meeting);
        }

        [HttpGet("meetingtypes")]
        public async Task<IActionResult> GetMeetingTypes()
        {
            var meetingTypes = await _context.MeetingTypes.ToListAsync();
            return Ok(meetingTypes);
        }

        [HttpGet("meetingitemstatustypes")]
        public async Task<IActionResult> GetMeetingitemstatustypes()
        {
            var statusTypes = await _context.MeetingItemStatuses.ToListAsync();
            return Ok(statusTypes);
        }

        [HttpGet("previous-open-items/{meetingTypeId}")]
        public async Task<IActionResult> GetPreviousOpenItems(int meetingTypeId)
        {
            var previousMeeting = await _context.Meetings
                .Where(m => m.MeetingTypeId == meetingTypeId)
                .OrderByDescending(m => m.Date)
                .FirstOrDefaultAsync();

            if (previousMeeting == null)
            {
                return Ok(new List<MeetingItemWithStatusDto>());
            }

            var previousOpenItems = await _context.MeetingItems
                .Where(mi => mi.MeetingId == previousMeeting.Id)
                .Select(mi => new MeetingItemWithStatusDto
                {
                    Id = mi.Id,
                    Description = mi.Description,
                    DueDate = mi.DueDate,
                    PersonResponsible = mi.PersonResponsible,
                    Status = mi.MeetingItemStatus.Status
                })
                .ToListAsync();

            return Ok(previousOpenItems);
        }
    }
}
