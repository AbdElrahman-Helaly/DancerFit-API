using Microsoft.AspNetCore.Identity;
using System.Numerics;

namespace DancerFit.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string FullName { get; internal set; }

    }
}
