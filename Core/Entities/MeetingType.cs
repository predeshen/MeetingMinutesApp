using System.ComponentModel.DataAnnotations;

namespace MeetingMinutesApp.Core.Entities
{
    public class MeetingType
    {
        [Key]
        public int MeetingTypeId { get; set; }
        public string Name { get; set; }
    }
}
