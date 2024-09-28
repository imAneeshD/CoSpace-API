using AutoMapper;
using CoSpace.Core.DTO.Admin;
using CoSpace.Core.Entities;


namespace CoSpace.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AdminDTO, Admin>();
        }
    }
}
