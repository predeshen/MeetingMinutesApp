using MeetingMinutesApp.Application.UseCases;
using MeetingMinutesApp.Presentation.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class MeetingsController : ControllerBase
{
    private readonly CaptureNewMeeting _captureNewMeeting;
    private readonly UpdateMeetingItemStatus _updateMeetingItemStatus;

    public MeetingsController(CaptureNewMeeting captureNewMeeting, UpdateMeetingItemStatus updateMeetingItemStatus)
    {
        _captureNewMeeting = captureNewMeeting;
        _updateMeetingItemStatus = updateMeetingItemStatus;
    }

    [HttpPost]
    public async Task<IActionResult> CaptureNewMeeting([FromBody] CaptureNewMeetingRequest request)
    {
        if (request == null)
        {
            return BadRequest("Request cannot be null.");
        }

        await _captureNewMeeting.ExecuteAsync(request.MeetingTypeId, request.Date, request.Time);
        return Ok();
    }

    [HttpPut("{meetingItemId}/status")]
    public async Task<IActionResult> UpdateMeetingItemStatus(int meetingItemId, [FromBody] UpdateMeetingItemStatusRequest request)
    {
        await _updateMeetingItemStatus.ExecuteAsync(meetingItemId, request.Status, request.ResponsiblePersonId);
        return Ok();
    }
}
