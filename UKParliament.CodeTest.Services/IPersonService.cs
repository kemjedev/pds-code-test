using UKParliament.CodeTest.Dtos;

namespace UKParliament.CodeTest.Web.Services;

public interface IPersonService
{
    Task<IEnumerable<PersonDto>> GetPersonsAsync();
    Task<PersonDto> CreateNewPersonAsync(PersonDto personDto);
    Task<PersonDto> UpdatePersonAsync(PersonDto personDto);
}