using UKParliament.CodeTest.Dtos;

namespace UKParliament.CodeTest.Services;

public interface IPersonService
{
    Task<IEnumerable<PersonDto>> GetPersonsAsync();
}