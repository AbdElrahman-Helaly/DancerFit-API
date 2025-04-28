using DancerFit.DTOS;
using DancerFit.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DancerFit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DancerController : ControllerBase
    {
        private readonly IDancerServices dancerServices;
        public DancerController(IDancerServices _dancerServices)
        {
            dancerServices = _dancerServices;
        }
       
        [HttpPost("CreateDancer")]
        public async Task<IActionResult> CreateDancer([FromBody] DancerDTO dancer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await dancerServices.CreateDancer(dancer);
            if (result)
            {
                return Ok("Dancer created successfully");
            }
            else
            {
                return BadRequest("Failed to create dancer");
            }
        }

        [HttpPut("UpdateDancer")]
        public async Task<IActionResult> UpdateDancer([FromBody] DancerDTO dancer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await dancerServices.UpdateDancer(dancer);
            if (result)
            {
                return Ok("Dancer updated successfully");
            }
            else
            {
                return BadRequest("Failed to update dancer");
            }
        }

        [HttpDelete("DeleteDancer/{id}")]
        public async Task<IActionResult> DeleteDancer(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid dancer ID");
            }

            var result = await dancerServices.DeleteDancer(id);
            if (result)
            {
                return Ok("Dancer deleted successfully");
            }
            else
            {
                return BadRequest("Failed to delete dancer");
            }
        }

        [HttpGet("GetDancerById/{id}")]
        public async Task<IActionResult> GetDancerById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid dancer ID");
            }

            var dancer = await dancerServices.GetDancerById(id);
            if (dancer != null)
            {
                return Ok(dancer);
            }
            else
            {
                return NotFound("Dancer not found");
            }
        }

        [HttpGet("GetAllDancers")]
        public async Task<IActionResult> GetAllDancers()
        {
            var dancers = await dancerServices.GetAllDancers();
            if (dancers != null && dancers.Any())
            {
                return Ok(dancers);
            }
            else
            {
                return NotFound("No dancers found");
            }
        }



    }
}
