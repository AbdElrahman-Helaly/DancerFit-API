using AutoMapper;
using DancerFit.Data;
using DancerFit.DTOS;
using DancerFit.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DancerFit.Services
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly AppDbcontext _appDbcontext;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;


        public UserServices(UserManager<ApplicationUser> userManager,
                               IMapper mapper,
                            AppDbcontext appDbcontext,
                             SignInManager<ApplicationUser> signInManager, IConfiguration configuration,
                             ITokenService tokenService)
        {
            userManager = userManager;
            _mapper = mapper;
            _appDbcontext = appDbcontext;
            _configuration = configuration;
            _signInManager = signInManager;
            _tokenService = tokenService;

        }


       public  async Task<UserDTO> GetUserByIdAsync(string id)
        {

            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Invalid user ID");
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var userDto = _mapper.Map<UserDTO>(user);
            return userDto;

        }

        public async Task<string>LoginAsync(UserLoginDto loginDto)
        {
            if (loginDto == null)
            {
                throw new ArgumentNullException(nameof(loginDto));
            }
            ApplicationUser user = await _userManager.FindByEmailAsync(loginDto.Email);
            var Result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, lockoutOnFailure: false);
            if (!Result.Succeeded)
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }
            if (user == null)
            {
                throw new Exception("User not found");
            }


            var token = _tokenService.GenerateToken(user.Id, user.Email);
            return token;
        }

        public async Task<UserDTO> RegisterAsync(UserRegisterDto registerDto)
        {
            if (registerDto == null)
            {
                throw new ArgumentNullException(nameof(registerDto));
            }

            var userExists = await _userManager.FindByEmailAsync(registerDto.Email);
            if (userExists != null)
            {
                throw new Exception("Email already in use");
            }

            var user = _mapper.Map<ApplicationUser>(registerDto);
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                throw new Exception("User registration failed");
            }
            var token = _tokenService.GenerateToken(user.Id, user.Email);

            return _mapper.Map<UserDTO>(user);


        }

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            var users = _userManager.Users.ToList();
            return _mapper.Map<List<UserDTO>>(users);
        }

        public async Task<UserDTO> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Invalid email");
            }
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<bool> UpdateUserAsync(UserDTO userDto)
        {
            if (userDto == null)
            {
                throw new ArgumentNullException(nameof(userDto));
            }
            var user = await _userManager.FindByIdAsync(userDto.Id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            user = _mapper.Map<ApplicationUser>(userDto);
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }
        public async Task<bool> DeleteUserAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Invalid user ID");
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }
    }
}



        /*    private string GenerateJwtToken(ApplicationUser user)
            {
                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim("id", user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(7),
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
    }*/

    
