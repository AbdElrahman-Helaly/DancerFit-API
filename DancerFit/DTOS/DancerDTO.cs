namespace DancerFit.DTOS
{
    public class DancerDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Style { get; set; } //  HipHop, Salsa, Ballet
        public string? PhoneNumber { get; set; } 
    }
    public class CreateDancerDto
    {
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Style { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
