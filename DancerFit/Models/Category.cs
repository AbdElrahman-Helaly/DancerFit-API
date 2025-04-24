namespace DancerFit.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } // Zumba, Hip Hop, Salsa
        public ICollection<DanceClass> Classes { get; set; }
    }
}
