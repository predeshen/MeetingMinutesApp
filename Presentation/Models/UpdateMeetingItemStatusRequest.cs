namespace MeetingMinutesApp.Presentation.Models
{
    public class UpdateMeetingItemStatusRequest
    {
        public string Status { get; set; }
        public int ResponsiblePersonId { get; set; }
    }
}
