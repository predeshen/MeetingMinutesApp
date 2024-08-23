namespace MeetingMinutesApp.Presentation.Models
{
    public class CaptureNewMeetingRequest
    {
        public int MeetingTypeId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
    }

}
