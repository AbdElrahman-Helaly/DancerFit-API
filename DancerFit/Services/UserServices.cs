using AutoMapper;
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
        

        public UserServices(UserManager<ApplicationUser> userManager,
                               IMapper mapper,
                            AppDbcontext appDbcontext,
                             SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        { 
            userManager = userManager;
            _mapper = mapper;
            _appDbcontext = appDbcontext;
            _configuration = configuration;
            _signInManager = signInManager;

        }
         async Task<UserDTO> IUserServices.GetUserByIdAsync(string id)
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

        async  Task<string> IUserServices.LoginAsync(UserLoginDto loginDto)
        {
            if (loginDto == null)
            {
                throw new ArgumentNullException(nameof(loginDto));
            }
            ApplicationUser user = await _userManager.FindByEmailAsync(loginDto.Email);
            var Result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, lockoutOnFailure: false);
            if(!Result.Succeeded)
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

          var token = GenerateJwtToken(user);
            return token;
        }

        async Task<UserDTO> IUserServices.RegisterAsync(UserRegisterDto registerDto)
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

            return _mapper.Map<UserDTO>(user);


        }
        private string GenerateJwtToken(ApplicationUser user)
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
    }
}
