namespace MeetingMinutesApp.Core.Entities
{
    public class MeetingItem
    {
        public int MeetingItemId { get; set; }
        public int MeetingId { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Meeting Meeting { get; set; }
        public ICollection<MeetingItemStatus> MeetingItemStatuses { get; set; }
    }
}
