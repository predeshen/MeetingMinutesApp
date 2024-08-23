namespace MeetingMinutesApp.Core.Entities
{
    public class MeetingItemStatus
    {
        public int StatusId { get; set; }
        public int MeetingItemId { get; set; }
        public int MeetingId { get; set; }
        public string Status { get; set; }
        public int ResponsiblePersonId { get; set; }
        public MeetingItem MeetingItem { get; set; }
        public Person ResponsiblePerson { get; set; }
    }
}
