namespace MeetingMinutesApp.Presentation.Models
{
    public class CaptureNewMeetingRequest
    {
        public int MeetingTypeId { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public List<MeetingItemWithStatusDto> PreviousOpenItems { get; set; }
        public List<MeetingItemDto> NewMeetingItems { get; set; }
    }

}
