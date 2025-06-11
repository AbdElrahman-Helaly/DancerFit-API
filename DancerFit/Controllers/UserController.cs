using AutoMapper;
using DancerFit.DTOS;
using DancerFit.Models;
using DancerFit.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DancerFit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
      
        public UserController(IUserServices userServices)
        {
          _userServices = userServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userServices.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userServices.GetUserByIdAsync(id);
            return Ok(user);
        }

        [HttpGet("by-email")]
        public async Task<IActionResult> GetByEmail([FromQuery] string email)
        {
            var user = await _userServices.GetUserByEmailAsync(email);
            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserDTO userDto)
        {
            var result = await _userServices.UpdateUserAsync(userDto);
            if (!result)
                return BadRequest("Update failed");

            return Ok("User updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _userServices.DeleteUserAsync(id);
            if (!result)
                return BadRequest("Delete failed");

            return Ok("User deleted successfully");
        }
    

       
       

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var token = await _userServices.LoginAsync(loginDto);
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Invalid login attempt");
            }

            return Ok(new { Token = token });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userServices.RegisterAsync(registerDto);
        


            return Ok("User registered successfully");
        }
       
   
        


    }
}
