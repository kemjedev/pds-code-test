using System.Net.Http.Json;
using UKParliament.CodeTest.Dtos;

namespace UKParliament.CodeTest.Web.Services;

public class PersonService : IPersonService
{

    private readonly HttpClient _httpClient;

    public PersonService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }

    //TODO - an architectural improvement will be to map from the DTO to a ViewModel
    //This will allow the use of UI specific annotations and neater validation
    public async Task<IEnumerable<PersonDto>> GetPeopleAsync()
    {
        var response = await _httpClient.GetAsync("data-api/persons/all");
        response.EnsureSuccessStatusCode();
        var responseDto = await response.Content.ReadFromJsonAsync<GetPeopleResponseDto>();

        if (responseDto?.People != null && responseDto.IsSuccess)
        {
            return responseDto.People;
        }
        return Enumerable.Empty<PersonDto>();
    }

    public async Task<PersonDto> CreateNewPersonAsync(PersonDto personDto)
    {
        var response = await _httpClient.PostAsJsonAsync("data-api/persons/create", personDto);
        response.EnsureSuccessStatusCode();

        var responseDto = await response.Content.ReadFromJsonAsync<CreatePersonResponseDto>();

        if (responseDto?.Person != null && responseDto.IsSuccess)
        {
            return responseDto.Person;
        }

        return default!;
    }

    public async Task<PersonDto> GetPersonByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"data-api/persons/{id}");
        response.EnsureSuccessStatusCode();
        var responseDto = await response.Content.ReadFromJsonAsync<GetPersonResponseDto>();

        if (responseDto?.Person != null && responseDto.IsSuccess)
        {
            return responseDto.Person;
        }
        throw new InvalidOperationException("Failed to retrieve person.");
    }

    public async Task<PersonDto> UpdatePersonAsync(PersonDto personDto)
    {
        var response = await _httpClient.PutAsJsonAsync($"data-api/persons/update", personDto);
        response.EnsureSuccessStatusCode();

        var responseDto = await response.Content.ReadFromJsonAsync<UpdatePersonResponseDto>();

        if (responseDto?.Person != null && responseDto.IsSuccess)
        {
            return responseDto.Person;
        }

        return default!;
    }
}