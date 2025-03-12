using FluentResults;
using UKParliament.CodeTest.Dtos;

namespace UKParliament.CodeTest.Api.Services
{
    public interface IPersonService
    {
        Task<Result<IEnumerable<PersonDto>>> GetPeopleAsync();
        Task<Result<PersonDto>> CreateNewPersonAsync(PersonDto personDto);
        Task<Result<PersonDto>> UpdatePersonAsync(PersonDto personDto);
        Task<Result<PersonDto>> GetPersonByIdAsync(int id);
    }
}
