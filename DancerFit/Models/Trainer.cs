namespace DancerFit.Models
{
    public class Trainer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<DanceClass> Classes { get; set; }


    }
}
