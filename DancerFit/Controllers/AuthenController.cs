using DancerFit.DTOS;
using DancerFit.Models;
using DancerFit.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DancerFit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        private readonly IAuthenServices authenServices;

        public AuthenController(IAuthenServices _authenServices)
        {
            authenServices = _authenServices;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await authenServices.Login(model);
            if (response == null)
            {
                return Unauthorized("Invalid login attempt");
            }

            return Ok(response);
        }

        [HttpPost("Register-User")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await authenServices.RegisterUser(registerDto);
            if (result.Succeeded)
            {
                return Ok("User registered successfully");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost("Register-Trainer")]
        public async Task<IActionResult> RegisterTrainer([FromBody] TrainerDto trainerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await authenServices.RegisterTrainer(trainerDto);
            if (result.Succeeded)
            {
                return Ok("Trainer registered successfully");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
        [HttpPost("Register-Dancer")]
        public async Task<IActionResult> RegisterDancer([FromBody] DancerDTO dancerDTO)
        {
            if (ModelState.IsValid)
            { 
            return BadRequest(ModelState);
            }
            var result = await authenServices.RegisterDancer(dancerDTO);
            if (result.Succeeded)
            {
                return Ok("Dancer registered successfully");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
    
    }

}
