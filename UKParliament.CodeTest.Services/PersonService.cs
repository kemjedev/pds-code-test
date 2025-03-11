using System.Net.Http;
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

    public async Task<IEnumerable<PersonDto>> GetPersonsAsync()
    {
        var response = await _httpClient.GetAsync("data-api/persons");
        response.EnsureSuccessStatusCode();
        var persons = await response.Content.ReadFromJsonAsync<IEnumerable<PersonDto>>();
        return persons ?? Enumerable.Empty<PersonDto>();
    }

    public async Task<PersonDto> CreateNewPersonAsync(PersonDto personDto)
    {
        var response = await _httpClient.PostAsJsonAsync("data-api/persons", personDto);
        response.EnsureSuccessStatusCode();
        var createdPerson = await response.Content.ReadFromJsonAsync<PersonDto>();
        return createdPerson ?? throw new InvalidOperationException("Failed to create person.");
    }

    public async Task<PersonDto> UpdatePersonAsync(PersonDto personDto)
    {
        var response = await _httpClient.PutAsJsonAsync($"data-api/persons/{personDto.Id}", personDto);
        response.EnsureSuccessStatusCode();
        var updatedPerson = await response.Content.ReadFromJsonAsync<PersonDto>();
        return updatedPerson ?? throw new InvalidOperationException("Failed to update person.");
    }

}