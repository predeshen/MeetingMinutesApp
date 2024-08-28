using MeetingMinutesApp.Core.Entities;
namespace MeetingMinutesApp.Core.Entities;

public class Meeting
{
    public int Id { get; set; }
    public int MeetingTypeId { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
    public MeetingType MeetingType { get; set; }
    public ICollection<MeetingItem> MeetingItems { get; set; }
}
