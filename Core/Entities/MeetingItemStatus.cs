using System.ComponentModel.DataAnnotations;

namespace MeetingMinutesApp.Core.Entities
{
    public class MeetingItemStatus
    {
        public int MeetingItemStatusId { get; set; }
        public int MeetingItemId { get; set; }
        public string Status { get; set; }
        public int ResponsiblePersonId { get; set; }
        public MeetingItem MeetingItem { get; set; }
    }
}
