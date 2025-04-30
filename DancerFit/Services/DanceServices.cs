using AutoMapper;
using DancerFit.Data;
using DancerFit.DTOS;
using DancerFit.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DancerFit.Services
{
    public class DanceServices : IDanceServices
    {
        private readonly AppDbcontext appDbcontext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        public DanceServices(AppDbcontext _appDbcontext,
        UserManager<ApplicationUser> _userManager,
                             IMapper _mapper)
        {
            appDbcontext = _appDbcontext;
            userManager = _userManager;
            mapper = _mapper;
        }
        public async Task<IEnumerable<DanceDTO>> GetAllDance()
        {
        var Dance = appDbcontext.DanceClasses.ToListAsync();
            if (Dance == null)
            {
                throw new Exception("error");
            }
            var result =  mapper.Map<IEnumerable<DanceDTO>>(Dance);
            if (result == null)
            { 
                throw new Exception("error");
            }
            return result;
        }
        public async Task<DancerDTO> GetDanceById(int id)
        {
            if (id == 0)
            { 
                throw new Exception("error");
            }
            var Dance = appDbcontext.DanceClasses.FirstOrDefaultAsync(appDbcontext => appDbcontext.Id == id);
            if (Dance == null)
            { 
                throw new Exception("error");
            }
            var result = mapper.Map<DancerDTO>(Dance);
            return result;
        }
        public Task<bool> CreateDance(DanceDTO dance)
        {
            if (dance == null)
            {
                throw new ArgumentNullException(nameof(dance));
            }

            var danceEntity = mapper.Map<DanceClass>(dance);
            appDbcontext.DanceClasses.Add(danceEntity);
            var result = appDbcontext.SaveChangesAsync();
            if (result.Result > 0)
            {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public async Task<bool> DeleteDance(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentException("Invalid dancer ID");
            }
            var dance = await appDbcontext.DanceClasses.FirstOrDefaultAsync(d => d.Id == id);
            if (dance == null)
            {
                throw new Exception("Dancer not found");
            }
            appDbcontext.DanceClasses.Remove(dance);
            var result = appDbcontext.SaveChangesAsync();
            if (result.Result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> UpdateDance(DanceDTO dance)
        {
            var danceEntity = appDbcontext.DanceClasses.FirstOrDefaultAsync(d => d.Id == dance.Id);
            if (danceEntity == null)
            {
                throw new Exception("Dancer not found");
            }

            var updatedDance = mapper.Map<DanceClass>(dance);

            appDbcontext.Entry(danceEntity).CurrentValues.SetValues(updatedDance);
            var result = appDbcontext.SaveChangesAsync();
            if (result.Result > 0)
            {
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);

        }

      
    }
}
