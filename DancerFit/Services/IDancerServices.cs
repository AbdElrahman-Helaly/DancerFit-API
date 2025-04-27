using AutoMapper;
using DancerFit.DTOS;
using DancerFit.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace DancerFit.Services
{
    public interface IDancerServices
    {
        Task<bool> CreateDancer(DancerDTO dancer);
        Task<bool> UpdateDancer(DancerDTO dancer);
        Task<bool> DeleteDancer(int id);
        Task<DancerDTO> GetDancerById(int id);
        Task<IEnumerable<DancerDTO>> GetAllDancers();
       
    }
}
