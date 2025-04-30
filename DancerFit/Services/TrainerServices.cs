using AutoMapper;
using DancerFit.Data;
using DancerFit.DTOS;
using DancerFit.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace DancerFit.Services
{
    public class TrainerServices : ITrainerServices
    {
        private readonly AppDbcontext appDbcontext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;


        public TrainerServices(AppDbcontext _appDbcontext,
                               UserManager<ApplicationUser> _userManager,
                                IMapper _mapper
)
        {
            appDbcontext = _appDbcontext;
            userManager = _userManager;
            mapper=_mapper;
        }


        public async Task<IEnumerable<TrainerDto>> GetAllTrainerAsync()
        {
            var trainers = await appDbcontext.Trainers.ToListAsync();
            var trainerDtos = mapper.Map<IEnumerable<TrainerDto>>(trainers);

            return trainerDtos;
        }

        public async Task<TrainerDto> GetTrainerByIdAsync(int TrainerId)
        {
          
            var trainer = appDbcontext.Trainers.FirstOrDefaultAsync(t => t.Id == TrainerId);
           
            if (trainer == null)
            {
                throw new Exception("Trainer not found");
            }
            var trainerDto = mapper.Map<TrainerDto>(trainer);

            return trainerDto;
        }
        public async Task<IEnumerable<TrainerDto>> GetTrainerByCategoryAsync(int CategoryId)
        {
           var trainers = appDbcontext.Trainers.Where(t => t.Categoryid == CategoryId).ToListAsync();
            if (trainers == null)
            {
                throw new Exception("No trainers found for this category");
            }
            var trainerDtos = mapper.Map<IEnumerable<TrainerDto>>(trainers);
            return trainerDtos;
        }

       
        public async Task<TrainerDto> CreateTrainerAsync(TrainerDto TrainerDTO)
        {
           var user = await userManager.FindByIdAsync(TrainerDTO.Email);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var trainer = mapper.Map<Trainer>(TrainerDTO);
            appDbcontext.Trainers.Add(trainer);
            await appDbcontext.SaveChangesAsync();
            return await GetTrainerByIdAsync(trainer.Id);
        }

       
        public async Task<bool> DeleteTrainerAsync(int TrainerId)
        {
                var trainer = appDbcontext.Trainers.FirstOrDefaultAsync(t => t.Id == TrainerId);
                if (trainer == null)
                {
                 throw new Exception("Trainer not found");
                }
                appDbcontext.Trainers.Remove(await trainer);
                await appDbcontext.SaveChangesAsync();
    
                 return true;
        }


        public async Task<TrainerDto> UpdateTrainerAsync(int TrainerId, TrainerDto TrainerDTO)
        {
            var trainer = await appDbcontext.Trainers.FirstOrDefaultAsync(t => t.Id == TrainerId);
            if (trainer == null)
            {
                throw new Exception("Trainer not found");
            }
            var updatedTrainer = mapper.Map<Trainer>(TrainerDTO);
            appDbcontext.Entry(trainer).CurrentValues.SetValues(updatedTrainer);
            await appDbcontext.SaveChangesAsync();

            return mapper.Map<TrainerDto>(trainer);

        }

        public async Task<TrainerDto> GetTrainerByUserIdAsync(string userId)
        {
            var trainer = await appDbcontext.Trainers.FirstOrDefaultAsync(t => t.UserId == userId);
            if (trainer == null)
            {
                throw new Exception("Trainer not found");
            }
            var trainerDto = mapper.Map<TrainerDto>(trainer);

            return trainerDto;
        
}
        

    }
}
