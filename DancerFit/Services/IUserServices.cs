using DancerFit.DTOS;
using DancerFit.Models;
using Microsoft.AspNetCore.Identity;

namespace DancerFit.Services
{
    public interface IUserServices
    {
        Task<UserDTO> GetUserByIdAsync(string id);
        Task<string> LoginAsync(UserLoginDto loginDto);
        Task<UserDTO> RegisterAsync(UserRegisterDto registerDto);
        Task<List<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetUserByEmailAsync(string email);
        Task<bool> UpdateUserAsync(UserDTO userDto);
        Task<bool> DeleteUserAsync(string id);








    }
}
