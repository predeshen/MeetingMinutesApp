using System.ComponentModel.DataAnnotations;

namespace MeetingMinutesApp.Core.Entities
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }
        public string Name { get; set; }
    }
}
