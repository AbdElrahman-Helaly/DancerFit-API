using System;
using System.Collections.Generic;
using System.Text;

namespace DancerFit.Services
{
    public interface ITokenService
    {
        string GenerateToken(string userId, string email);

    }
}
