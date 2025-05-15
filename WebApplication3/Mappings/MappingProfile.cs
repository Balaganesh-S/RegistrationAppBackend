using AutoMapper;
using System.Runtime;
using WebApplication3.DTOs;
using WebApplication3.Models;

namespace WebApplication3.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, RegistrationResponseDto>().ReverseMap();
            CreateMap<User, RegistrationRequestDto>().ReverseMap();
        }
    }
}
