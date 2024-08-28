using System;

namespace MeetingMinutesApp.Core.Entities
{
    public class MeetingItem
    {
        public int Id { get; set; }
        public int MeetingId { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string PersonResponsible { get; set; }
        public int MeetingItemStatusId { get; set; } // Foreign key to MeetingItemStatus

        public Meeting Meeting { get; set; }
        public MeetingItemStatus MeetingItemStatus { get; set; }
    }
}
