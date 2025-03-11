using AutoMapper;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Dtos;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Person, PersonDto>()
            .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department));
        CreateMap<Department, DepartmentDto>()
            .ForMember(dest => dest.People, opt => opt.MapFrom(src => src.People));
    }
}
