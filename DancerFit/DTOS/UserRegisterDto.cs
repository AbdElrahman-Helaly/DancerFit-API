using DancerFit.Models;
using System.ComponentModel.DataAnnotations;

namespace DancerFit.DTOS
{
    public class UserRegisterDto
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }


        [Required]
        public List<string> Roles { get; set; }

    }
}
