using DancerFit.DTOS;
using DancerFit.Models;
using Microsoft.AspNetCore.Identity;

namespace DancerFit.Services
{
    public interface IUserServices
    {
        Task<UserDTO> RegisterAsync(UserRegisterDto registerDto);
        Task<string> LoginAsync(UserLoginDto loginDto);
        Task<UserDTO> GetUserByIdAsync(string id);
    





    }
}
