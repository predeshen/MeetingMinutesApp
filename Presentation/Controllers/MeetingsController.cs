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
            try
            {
                var meeting = MapToMeeting(request);
                AddPreviousOpenItems(meeting, request.PreviousOpenItems);
                AddNewMeetingItems(meeting, request.NewMeetingItems);

                _context.Meetings.Add(meeting);
                await _context.SaveChangesAsync();

                return Ok(meeting.Id);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Invalid meeting request data.");
            }

        }

        private static Meeting MapToMeeting(CaptureNewMeetingRequest request)
        {
                return new Meeting
                {
                    MeetingTypeId = request.MeetingTypeId,
                    Date = DateTime.Parse(request.Date),
                    Time = TimeSpan.Parse(request.Time),
                    MeetingItems = new List<MeetingItem>() 
                };
        }
        private void AddPreviousOpenItems(Meeting meeting, IEnumerable<MeetingItemWithStatusDto> previousOpenItems)
        {
            foreach (var item in previousOpenItems)
            {
                int statusId = _context.MeetingItemStatuses.Where(x => x.Status == item.Status).Select(x => x.Id).First();

                if (statusId == 4)
                {
                    var meetingItem = new MeetingItem
                    {
                        MeetingId = meeting.Id,
                        Description = item.Description,
                        DueDate = item.DueDate,
                        PersonResponsible = item.PersonResponsible,
                        MeetingItemStatusId = 1
                    };
                    meeting.MeetingItems.Add(meetingItem);
                }
            }
        }

        private void AddNewMeetingItems(Meeting meeting, IEnumerable<MeetingItemDto> newMeetingItems)
        {
            foreach (var item in newMeetingItems)
            {
                int statusId = _context.MeetingItemStatuses.Where(x => x.Status == item.Status).Select(x => x.Id).First();
                meeting.MeetingItems.Add(new MeetingItem
                {
                    Description = item.Description,
                    DueDate = item.DueDate,
                    PersonResponsible = item.PersonResponsible,
                    MeetingItemStatusId = statusId
                });
            }
        }

        [HttpGet("meetingtypes")]
        public async Task<IActionResult> GetMeetingTypes()
        {
            var meetingTypes = await _context.MeetingTypes.ToListAsync();
            return Ok(meetingTypes.ToArray());
        }

        [HttpGet("meetingitemstatustypes")]
        public async Task<IActionResult> GetMeetingitemstatustypes()
        {
            var statusTypes = await _context.MeetingItemStatuses.ToListAsync();
            return Ok(statusTypes.ToArray());
        }

        [HttpGet("previous-open-items/{meetingTypeId}")]
        public async Task<IActionResult> GetPreviousOpenItems(int meetingTypeId)
        {
            var previousMeeting = await _context.Meetings
                .Where(m => m.MeetingTypeId == meetingTypeId)
                .OrderByDescending(m => m.Id)
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
                    PersonResponsible = mi.PersonResponsible
                })
                .ToListAsync();

            return Ok(previousOpenItems.ToArray());
        }

        [HttpGet("getMeeting/{meetingId}")]
        public async Task<IActionResult> GetMeeting(int meetingId)
        {
            var meeting = await _context.Meetings
                .Where(x => x.Id == meetingId)
                .Include(x => x.MeetingItems)
                .ThenInclude(x => x.MeetingItemStatus)
                .FirstOrDefaultAsync();

            if (meeting == null)
            {
                return NotFound("Meeting not found.");
            }

            return Ok(meeting);
        }

        [HttpGet("meetings")]
        public async Task<IActionResult> GetMeetings()
        {
            var meeting = await _context.Meetings.ToListAsync();

            if (meeting == null)
            {
                return NotFound("Meeting not found.");
            }

            return Ok(meeting.ToArray());
        }


        [HttpGet("{meetingId}/items")]
        public async Task<IActionResult> GetMeetingItems(int meetingId)
        {
            var meetingItem = await _context.MeetingItems.Where(x => x.MeetingId == meetingId).ToListAsync();

            if (meetingItem == null)
            {
                return NotFound("Meeting item not found.");
            }
            return Ok(meetingItem.ToArray());
        }
        [HttpPut("meetingitems/{meetingItemId}/status")]
        public async Task<IActionResult> UpdateMeetingItemStatus(int meetingItemId, [FromBody] UpdateMeetingItemStatusRequestDto request)
        {
            var meetingItem = await _context.MeetingItems
                .Include(x => x.MeetingItemStatus)
                .FirstOrDefaultAsync(x => x.Id == meetingItemId);

            if (meetingItem == null)
            {
                return NotFound("Meeting item not found.");
            }

            var status = await _context.MeetingItemStatuses
                .FirstOrDefaultAsync(x => x.Status == request.Status);

            if (status == null)
            {
                return BadRequest("Invalid status.");
            }

            meetingItem.MeetingItemStatus = status;
            await _context.SaveChangesAsync();

            return Ok(meetingItem);
        }
    }
}
