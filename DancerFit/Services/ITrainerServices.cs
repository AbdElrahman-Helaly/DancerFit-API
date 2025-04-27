using DancerFit.DTOS;

namespace DancerFit.Services
{
    public interface ITrainerServices
    {
        Task<IEnumerable<TrainerDto>> GetAllTrainerAsync();      
        Task<TrainerDto> GetTrainerByUserIdAsync(string userId);
        Task<TrainerDto> GetTrainerByIdAsync(int TrainerId);
        Task<IEnumerable<TrainerDto>> GetTrainerByCategoryAsync(int CategoryId);
        Task<TrainerDto> CreateTrainerAsync(TrainerDto TrainerDTO);
        Task<TrainerDto> UpdateTrainerAsync(int TrainerId,TrainerDto TrainerDTO);
        Task<bool> DeleteTrainerAsync(int TrainerId);




    }
}
