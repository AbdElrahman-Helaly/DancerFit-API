namespace DancerFit.Models
{
    public class DanceClass
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int DurationMinutes { get; set; }
        public string Level { get; set; } // Beginner / Intermediate / Advanced
        public DateTime CreatedAt { get; set; }

        // علاقات
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; }
    }
}
