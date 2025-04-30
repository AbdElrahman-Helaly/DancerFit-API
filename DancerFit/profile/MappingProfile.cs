using AutoMapper;
using DancerFit.DTOS;
using DancerFit.Models;

namespace DancerFit.profile
{
    public class MappingProfile: Profile
    {
        public MappingProfile() 
        {
            CreateMap<ApplicationUser, UserRegisterDto>().ReverseMap();
            CreateMap<Trainer, TrainerDto>();
            CreateMap<TrainerDto, Trainer>().ReverseMap();
            CreateMap<Dancer, DancerDTO>();
            CreateMap<Category, CategoryDTO>();
        }
    }
}
