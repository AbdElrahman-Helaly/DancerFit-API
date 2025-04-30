using DancerFit.DTOS;
using DancerFit.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DancerFit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DanceController : ControllerBase
    {
        private readonly DanceServices danceservices;
        public DanceController(DanceServices _danceServices)
        {
            danceservices = _danceServices;
        }

        [HttpGet("GetAllDancer")]
        public async Task<IActionResult> GetAllDance()
        {
            var dancers = await danceservices.GetAllDance();
            if (dancers != null && dancers.Any())
            {
                return Ok(dancers);
            }
            else
            {
                return NotFound("No dancers found");
            }
        }
        [HttpGet("GetDanceById/{id}")]
        public async Task<IActionResult> GetDance(int id)
        {
            var dance = await danceservices.GetDanceById(id);
            if (dance != null)
            {
                return Ok(dance);
            }
            else
            {
                return NotFound("not found");
            }


        }

        [HttpPost("CreateDance")]
        public async Task<IActionResult> CreateDance([FromBody] DanceDTO dance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await danceservices.CreateDance(dance);
            if (result)
            {
                return Ok("Dance created successfully");
            }
            else
            {
                return BadRequest("Failed to create dance");
            }
        }

        [HttpPut("UpdateDance")]
        public async Task<IActionResult> UpdateDance([FromBody] DanceDTO dance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await danceservices.UpdateDance(dance);
            if (result)
            {
                return Ok("Dance updated successfully");
            }
            else
            {
                return BadRequest("Failed to update dance");
            }
        }

        [HttpDelete("DeleteDance/{id}")]
        public async Task<IActionResult> DeleteDance(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid dance ID");
            }

            var result = await danceservices.DeleteDance(id);
            if (result)
            {
                return Ok("Dance deleted successfully");
            }
            else
            {
                return BadRequest("Failed to delete dance");
            }
        }
    }
}
