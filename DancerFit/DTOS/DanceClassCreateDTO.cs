using System.ComponentModel.DataAnnotations;

namespace DancerFit.DTOS
{
    public class DanceClassCreateDTO
    {

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public int DurationMinutes { get; set; }
        public string Level { get; set; }

        public int CategoryId { get; set; }
        public int TrainerId { get; set; }
    }
}
