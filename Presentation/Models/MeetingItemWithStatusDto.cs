namespace MeetingMinutesApp.Presentation.Models
{
    public class MeetingItemWithStatusDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string PersonResponsible { get; set; }
        public string Status { get; set; }
    }
}
