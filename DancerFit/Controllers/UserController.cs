using AutoMapper;
using DancerFit.DTOS;
using DancerFit.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DancerFit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
       private readonly UserManager<AppDbcontext> _userManager;
        private readonly IMapper mapper;
        public UserController(UserManager<AppDbcontext> userManager,IMapper _mapper)
        {
           userManager= _userManager;
            mapper = _mapper;
        }
   
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userExists = await _userManager.FindByEmailAsync(dto.Email);
            if (userExists != null)
                return BadRequest(new {Message ="Email Used Before Not Valid"});

            var user = mapper.Map<ApplicationUser>(dto);
            var result = await _userManager.CreateAsync(userExists,dto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

                return Ok(new { Message = "User registered successfully" });
            }

      
    
    }
}
