using System.Net.Http;
using System.Net.Http.Json;
using UKParliament.CodeTest.Dtos;

namespace UKParliament.CodeTest.Services;

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
        return await response.Content.ReadFromJsonAsync<IEnumerable<PersonDto>>();
    }

}