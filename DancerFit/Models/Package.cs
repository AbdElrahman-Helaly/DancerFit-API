namespace DancerFit.Models
{
    public class Package
    {
        public int Id { get; set; }
        public string Name { get; set; } // Monthly, VIP, Free Trial
        public decimal Price { get; set; }
        public int DurationDays { get; set; }
        public string Description { get; set; }
    }
}
