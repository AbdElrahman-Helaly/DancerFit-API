using DancerFit.DTOS;

namespace DancerFit.Services
{
    public interface IDanceServices
    {
        Task<bool> CreateDance(DanceDTO dance);
        Task<bool> UpdateDance(DanceDTO dance);
        Task<bool> DeleteDance(int id);
        Task<DancerDTO> GetDanceById(int id);
        Task<IEnumerable<DanceDTO>> GetAllDance();
    }
}
