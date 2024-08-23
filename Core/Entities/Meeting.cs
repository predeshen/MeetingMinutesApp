using System.ComponentModel.DataAnnotations;

namespace MeetingMinutesApp.Core.Entities
{
    public class Meeting
    {
        [Key]
        public int MeetingId { get; set; }
        public int MeetingTypeId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public MeetingType MeetingType { get; set; }
        public ICollection<MeetingItem> MeetingItems { get; set; }
    }
}
