namespace DancerFit.Models
{
    public class Dancer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Style { get; set; } // نوع الرقص مثلاً: HipHop, Salsa, Ballet
        public string? PhoneNumber { get; set; } // رقم تليفون اختياري
   
  
    }
}

