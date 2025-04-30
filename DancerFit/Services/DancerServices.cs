using AutoMapper;
using DancerFit.Data;
using DancerFit.DTOS;
using DancerFit.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DancerFit.Services
{
    public class DancerServices : IDancerServices
    {
      private readonly AppDbcontext appDbcontext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;


        public DancerServices(AppDbcontext _appDbcontext,
                                UserManager<ApplicationUser> _userManager,
                               IMapper _mapper)
        { 
       appDbcontext= _appDbcontext;
            userManager = _userManager;
            mapper = _mapper;
        
        }
        public async Task<IEnumerable<DancerDTO>> GetAllDancers()
        {
            var dancers = appDbcontext.Dancers.ToListAsync();
            if (dancers == null)
            {
                throw new Exception("No dancers found");
            }
            var dancerDtos = mapper.Map<IEnumerable<DancerDTO>>(dancers);
            return dancerDtos;

        }

        public async Task<DancerDTO> GetDancerById(int id)
        {
             if(id <= 0)
            {
                throw new ArgumentException("Invalid dancer ID");
            }
            var dancer = appDbcontext.Dancers.FirstOrDefaultAsync(d => d.Id == id);
            if (dancer == null)
            {
                throw new Exception("Dancer not found");
            }
            var dancerDto = mapper.Map<DancerDTO>(dancer);
            return dancerDto;
        }

        public Task<bool> CreateDancer(DancerDTO dancer)
        {
            if (dancer == null)
            {
                throw new ArgumentNullException(nameof(dancer));
            }

            var dancerEntity = mapper.Map<Dancer>(dancer);
            appDbcontext.Dancers.Add(dancerEntity);
            var result = appDbcontext.SaveChangesAsync();
            if (result.Result > 0)
            {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);

        }

        public  async Task<bool> DeleteDancer(int id)
        {
          
            if (id <= 0)
            {
                throw new ArgumentException("Invalid dancer ID");
            }
            var dancer = await appDbcontext.Dancers.FirstOrDefaultAsync(d => d.Id == id);
            if (dancer == null) {
                throw new Exception("Dancer not found");
            }
            appDbcontext.Dancers.Remove(dancer);
            var result = appDbcontext.SaveChangesAsync();
            if (result.Result > 0)
            {
                return true;
            }
            return false;

        }

        public Task<bool> UpdateDancer(DancerDTO dancer)
        {
            var dancerEntity = appDbcontext.Dancers.FirstOrDefaultAsync(d => d.Id == dancer.Id);
            if (dancerEntity == null)
            {
                throw new Exception("Dancer not found");
            }

            var updatedDancer = mapper.Map<Dancer>(dancer);

            appDbcontext.Entry(dancerEntity).CurrentValues.SetValues(updatedDancer);
            var result = appDbcontext.SaveChangesAsync();
            if (result.Result > 0)
            {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }


     



     

    }

}
