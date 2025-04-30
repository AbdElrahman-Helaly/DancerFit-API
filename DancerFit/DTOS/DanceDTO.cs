namespace DancerFit.DTOS
{
    public class DanceDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int DurationMinutes { get; set; }
        public string Level { get; set; } // Beginner / Intermediate / Advanced
        public DateTime CreatedAt { get; set; }
    }
}
