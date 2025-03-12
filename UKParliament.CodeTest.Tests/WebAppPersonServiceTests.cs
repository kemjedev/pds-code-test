using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Moq.Protected;
using UKParliament.CodeTest.Dtos;
using UKParliament.CodeTest.Web.Services;
using Xunit;

namespace UKParliament.CodeTest.Tests.Web.Services
{
    public class WebAppPersonServiceTests
    {
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly HttpClient _httpClient;
        private readonly PersonService _personService;

        public WebAppPersonServiceTests()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("http://localhost/")
            };
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(_httpClient);

            _personService = new PersonService(_httpClientFactoryMock.Object);
        }

        [Fact]
        public async Task GetPeopleAsync_ShouldReturnPeople()
        {
            // Arrange
            var people = new List<PersonDto>
            {
                new PersonDto { Id = 1, FirstName = "John", LastName = "Doe" },
                new PersonDto { Id = 2, FirstName = "Jane", LastName = "Doe" }
            };
            var responseDto = new GetPeopleResponseDto { People = people, IsSuccess = true };
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(responseDto)
            };

            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            // Act
            var result = await _personService.GetPeopleAsync();

            // Assert
            result.Should().BeEquivalentTo(people);
        }

        [Fact]
        public async Task CreateNewPersonAsync_ShouldReturnCreatedPerson()
        {
            // Arrange
            var personDto = new PersonDto { Id = 1, FirstName = "John", LastName = "Doe" };
            var responseDto = new CreatePersonResponseDto { Person = personDto, IsSuccess = true };
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(responseDto)
            };

            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            // Act
            var result = await _personService.CreateNewPersonAsync(personDto);

            // Assert
            result.Should().BeEquivalentTo(personDto);
        }

        [Fact]
        public async Task GetPersonByIdAsync_ShouldReturnPerson()
        {
            // Arrange
            var personDto = new PersonDto { Id = 1, FirstName = "John", LastName = "Doe" };
            var responseDto = new GetPersonResponseDto { Person = personDto, IsSuccess = true };
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(responseDto)
            };

            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            // Act
            var result = await _personService.GetPersonByIdAsync(1);

            // Assert
            result.Should().BeEquivalentTo(personDto);
        }

        [Fact]
        public async Task UpdatePersonAsync_ShouldReturnUpdatedPerson()
        {
            // Arrange
            var personDto = new PersonDto { Id = 1, FirstName = "John", LastName = "Doe" };
            var responseDto = new UpdatePersonResponseDto { Person = personDto, IsSuccess = true };
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(responseDto)
            };

            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            // Act
            var result = await _personService.UpdatePersonAsync(personDto);

            // Assert
            result.Should().BeEquivalentTo(personDto);
        }
    }
}
