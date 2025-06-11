using DancerFit.DTOS;
using DancerFit.Models;
using DancerFit.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DancerFit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerController : ControllerBase
    {
        private readonly ITrainerServices trainerServices;

        public TrainerController(ITrainerServices _trainerServices)
        {
            _trainerServices = trainerServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTrainer()
        {
            var doctors = await trainerServices.GetAllTrainerAsync();
            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrainerById(int id)
        {
            var doctor = await trainerServices.GetTrainerByIdAsync(id);
            if (doctor == null)
                return NotFound();

            return Ok(doctor);
        }

        [HttpGet("User/{userId}")]
        public async Task<IActionResult> GetTrainerByUserId(string userId)
        {
            var doctor = await trainerServices.GetTrainerByUserIdAsync(userId);
            if (doctor == null)
                return NotFound();

            return Ok(doctor);
        }

        [HttpGet("Category/{Categoryid}")]
        public async Task<IActionResult> GetTrainerbycategory(int CategoryId)
        {
            var doctors = await trainerServices.GetTrainerByCategoryAsync(CategoryId);
            return Ok(doctors);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> CreateTrainer([FromBody] TrainerDto trainerDto)
        {
            if (trainerDto == null)
                return BadRequest("Trainer data is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var trainer = await trainerServices.CreateTrainerAsync(trainerDto);
                if (trainer == null)
                    return BadRequest("User not found or invalid data.");

                return CreatedAtAction(nameof(GetTrainerById), new { id = trainer.Id }, trainer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the trainer.");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> UpdateTrainer(int id, [FromBody] TrainerDto trainerDto)
        {
            if (trainerDto == null)
                return BadRequest("Trainer data is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedTrainer = await trainerServices.UpdateTrainerAsync(id, trainerDto);
                if (updatedTrainer == null)
                    return NotFound();

                return Ok(updatedTrainer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the trainer.");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(UserRoles.Admin)]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var result = await trainerServices.DeleteTrainerAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("MyProfile")]
        [Authorize(Roles = "Trainee")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var Trainer = await trainerServices.GetTrainerByUserIdAsync(userId);
            if (Trainer == null)
                return NotFound("Trainer profile not found.");

            return Ok(Trainer);
        }

    }
}


