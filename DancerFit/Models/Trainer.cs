namespace DancerFit.Models
{
    public class Trainer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public string Specialization { get; set; }
        public string Qualifications { get; set; }
        public string LicenseNumber { get; set; }
        public int Categoryid { get; set; }


        public ICollection<DanceClass> Classes { get; set; }


    }
}
