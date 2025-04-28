using DancerFit.DTOS;
using DancerFit.Models;
using Microsoft.AspNetCore.Identity;

namespace DancerFit.Services
{
    public interface IAuthenServices
    {
        Task<LoginResponseModel> Login(LoginModel model);
        Task<IdentityResult> RegisterTrainer(TrainerDto model);
        Task<IdentityResult> RegisterDancer(DancerDTO model);
        Task<IdentityResult> RegisterAdmin(UserDTO model);
        Task<IdentityResult> RegisterUser(UserRegisterDto model);

    }
}
