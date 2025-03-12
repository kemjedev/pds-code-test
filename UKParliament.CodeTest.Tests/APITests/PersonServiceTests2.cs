using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Moq;
using UKParliament.CodeTest.Api.Services;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Dtos;
using UKParliament.CodeTest.Tests.AutoMoqSupport;
using Xunit;

namespace UKParliament.CodeTest.Tests
{

    public class PersonServiceTests2
    {
        //[Theory, AutoMoqData]
        //public async Task GetPersonsAsync_ShouldReturnPeople(
        //    [Frozen] Mock<PersonManagerContext> contextMock,
        //    [Frozen] Mock<IMapper> mapperMock,
        //    PersonService service,
        //    List<Person> people,
        //    List<PersonDto> peopleDto)
        //{
        //    // Arrange
        //    var dbSetMock = people.AsQueryable().BuildMockDbSet();
        //    contextMock.Setup(context => context.People).Returns(dbSetMock.Object);
        //    mapperMock.Setup(mapper => mapper.Map<IEnumerable<PersonDto>>(people)).Returns(peopleDto);

        //    // Act
        //    var result = await service.GetPersonsAsync();

        //    // Assert
        //    result.IsSuccess.Should().BeTrue();
        //    result.Value.Should().BeEquivalentTo(peopleDto);
        //}

        [Theory, AutoMoqData]
        public async Task CreateNewPersonAsync_ShouldReturnCreatedPerson(
            [Frozen] Mock<PersonManagerContext> contextMock,
            [Frozen] Mock<IMapper> mapperMock,
            [Frozen] Mock<IValidator<PersonDto>> validatorMock,
            PersonService service,
            PersonDto personDto,
            Person person,
            PersonDto createdPersonDto)
        {
            // Arrange
            validatorMock.Setup(validator => validator.ValidateAsync(personDto, default))
                .ReturnsAsync(new ValidationResult());
            mapperMock.Setup(mapper => mapper.Map<Person>(personDto)).Returns(person);
            mapperMock.Setup(mapper => mapper.Map<PersonDto>(person)).Returns(createdPersonDto);

            // Act
            var result = await service.CreateNewPersonAsync(personDto);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(createdPersonDto);
        }

        //    [Theory, AutoMoqData]
        //    public async Task UpdatePersonAsync_ShouldReturnUpdatedPerson(
        //        [Frozen] Mock<PersonManagerContext> contextMock,
        //        [Frozen] Mock<IMapper> mapperMock,
        //        PersonService service,
        //        PersonDto personDto,
        //        Person person,
        //        PersonDto updatedPersonDto)
        //    {
        //        // Arrange
        //        var dbSetMock = new Mock<DbSet<Person>>();
        //        dbSetMock.Setup(db => db.FindAsync(personDto.Id)).ReturnsAsync(new ValueTask<Person>(person));
        //        contextMock.Setup(context => context.People).Returns(dbSetMock.Object);
        //        mapperMock.Setup(mapper => mapper.Map<PersonDto>(person)).Returns(updatedPersonDto);

        //        // Act
        //        var result = await service.UpdatePersonAsync(personDto);

        //        // Assert
        //        result.IsSuccess.Should().BeTrue();
        //        result.Value.Should().BeEquivalentTo(updatedPersonDto);
        //    }
        //}
    }
}
