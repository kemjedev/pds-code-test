using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UKParliament.CodeTest.Api.Controllers;
using UKParliament.CodeTest.Dtos;
using Xunit;
using UKParliament.CodeTest.Api.Services;
using UKParliament.CodeTest.Tests.AutoMoqSupport;

namespace UKParliament.CodeTest.Tests
{
    public class PersonsControllerTests
    {
        [Theory, AutoMoqData]
        public async Task GetPeople_ShouldReturnOkResult_WhenPeopleExist(
            [Frozen] Mock<IPersonService> personServiceMock,
            [Frozen] Mock<IMapper> mapperMock,
            PersonsController controller,
            List<PersonDto> people)
        {
            // Arrange
            personServiceMock.Setup(service => service.GetPersonsAsync())
                .ReturnsAsync(Result.Ok<IEnumerable<PersonDto>>(people));

            // Act
            var result = await controller.GetPeople();

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.Value.Should().BeOfType<GetPeopleResponseDto>();
            var response = okResult.Value as GetPeopleResponseDto;
            response.IsSuccess.Should().BeTrue();
            response.People.Should().BeEquivalentTo(people);
        }

        [Theory, AutoMoqData]
        public async Task CreateNewPerson_ShouldReturnOkResult_WhenPersonIsCreated(
            [Frozen] Mock<IPersonService> personServiceMock,
            [Frozen] Mock<IMapper> mapperMock,
            PersonsController controller,
            PersonDto personDto,
            PersonDto createdPersonDto)
        {
            // Arrange
            personServiceMock.Setup(service => service.CreateNewPersonAsync(personDto))
                .ReturnsAsync(Result.Ok(createdPersonDto));

            // Act
            var result = await controller.CreateNewPerson(personDto);

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.Value.Should().BeOfType<CreatePersonResponseDto>();
            var response = okResult.Value as CreatePersonResponseDto;
            response.IsSuccess.Should().BeTrue();
            response.Person.Should().BeEquivalentTo(createdPersonDto);
        }

        [Theory, AutoMoqData]
        public async Task UpdatePerson_ShouldReturnOkResult_WhenPersonIsUpdated(
            [Frozen] Mock<IPersonService> personServiceMock,
            [Frozen] Mock<IMapper> mapperMock,
            PersonsController controller,
            PersonDto personDto,
            PersonDto updatedPersonDto)
        {
            // Arrange
            personServiceMock.Setup(service => service.UpdatePersonAsync(personDto))
                .ReturnsAsync(Result.Ok(updatedPersonDto));

            // Act
            var result = await controller.UpdatePerson(personDto);

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.Value.Should().BeOfType<UpdatePersonResponseDto>();
            var response = okResult.Value as UpdatePersonResponseDto;
            response.IsSuccess.Should().BeTrue();
            response.Person.Should().BeEquivalentTo(updatedPersonDto);
        }
    }


}
