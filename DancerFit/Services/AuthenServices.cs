using DancerFit.DTOS;
using DancerFit.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DancerFit.Services
{
    public class AuthenServices : IAuthenServices
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;
        private readonly ITrainerServices trainerServices;
        private readonly IDancerServices dancerServices;
        private readonly IUserServices userServices;

        public AuthenServices(UserManager<ApplicationUser> _userManager,
                            RoleManager<IdentityRole> _roleManager,
                              IConfiguration _configuration,
                          ITrainerServices _trainerServices, IDancerServices _dancerServices,
                                                         IUserServices _userServices)
        { 
              userManager = _userManager;
              roleManager = _roleManager;
              configuration = _configuration;
              trainerServices = _trainerServices;
               dancerServices = _dancerServices;
                userServices = _userServices;
        }

        public async Task<LoginResponseModel> Login(LoginModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return null;
            }

            var result = await userManager.CheckPasswordAsync(user, model.Password);
            if (!result)
            {
                return null;
            }

            var roles = await userManager.GetRolesAsync(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, string.Join(",", roles))
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new LoginResponseModel
            {
                Token = tokenHandler.WriteToken(token),
                UserId = user.Id,
                Roles = roles.ToList()
            };

        }

        public async Task<IdentityResult> RegisterAdmin(UserDTO model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null)
            { 
            return IdentityResult.Failed(new IdentityError { Description = "Email already exists" });
            }
            var newUser = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber
            };
            var result = await userManager.CreateAsync(newUser, model.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(newUser, "Admin");
            }
            return result;


        }

        public async Task<IdentityResult> RegisterDancer(DancerDTO model)
        {
           
            var userexist = await userManager.FindByEmailAsync(model.Email);
            if (userexist != null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Email already exists" });
            }

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Dancer");
            }
            var Dancerdto = new DancerDTO
            { 
            FullName =model.FullName,
            PhoneNumber = model.PhoneNumber,
           Email = model.Email,
           Age = model.Age,
           Style = model.Style
            };
            var dancer = await dancerServices.CreateDancer(Dancerdto);
            if (dancer == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Failed to create dancer" });

            }

            return result;
        }

        public async Task<IdentityResult> RegisterTrainer(TrainerDto model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Email already exists" });
            }

            var newUser = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber
            };

            var result = await userManager.CreateAsync(newUser, model.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(newUser, "Trainer");
            }
            var trainerdto = new TrainerDto
            {
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                Specialization = model.Specialization,
                Qualifications = model.Qualifications,
                LicenseNumber = model.LicenseNumber
            };
           
            var trainerResult = await trainerServices.CreateTrainerAsync(trainerdto);
            if (trainerResult == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Failed to create trainer" });
            }
          
            return result;

        }

        public async Task<IdentityResult> RegisterUser(UserRegisterDto model)
        {
         var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Email already exists" });
            }

            var newUser = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
            };

            var result = await userManager.CreateAsync(newUser, model.Password);


            if (result.Succeeded)
            {
                var rolesToAdd = model.Roles != null && model.Roles.Any() ? model.Roles : new List<string> { UserRoles.User };

                await userManager.AddToRolesAsync(newUser, rolesToAdd);
            }

            return result;

        }
    }
}
