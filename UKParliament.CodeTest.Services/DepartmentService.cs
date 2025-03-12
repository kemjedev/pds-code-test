using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using UKParliament.CodeTest.Dtos;

namespace UKParliament.CodeTest.Web.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HttpClient _httpClient;

        public DepartmentService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync()
        {
            var response = await _httpClient.GetAsync("data-api/departments/all");
            response.EnsureSuccessStatusCode();

            var responseDto = await response.Content.ReadFromJsonAsync<GetDepartmentsResponseDto>();

            if (responseDto?.Departments != null && responseDto.IsSuccess)
            {
                return responseDto.Departments;
            }
            return Enumerable.Empty<DepartmentDto>();
        }
    }
}
