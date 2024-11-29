using AutoMapper;
using CoSpace.Core.DTO;
using CoSpace.Core.Entities;


namespace CoSpace.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Admin, AdminDTO>().ReverseMap();
            CreateMap<OrganizationDTO, Organization>().ReverseMap();
            CreateMap<UserRole, UserRole>().ReverseMap();
        }
    }
}
