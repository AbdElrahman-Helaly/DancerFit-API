namespace DancerFit.DTOS
{
    public class TrainerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Specialization { get; set; }
        public string Qualifications { get; set; }
        public string LicenseNumber { get; set; }
        public bool IsAvailable { get; set; }
    }
}
