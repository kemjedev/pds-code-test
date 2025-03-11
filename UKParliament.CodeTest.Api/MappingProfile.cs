using AutoMapper;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Dtos;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Person, PersonDto>();
        // Other mappings
    }
}
