using Bunit;
using FluentAssertions;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using UKParliament.CodeTest.Dtos;
using UKParliament.CodeTest.Web.Components.PeopleManagement.Components;
using UKParliament.CodeTest.Web.Services;
using Xunit;

namespace UKParliament.CodeTest.Tests.Web.Components.PeopleManagement.Components
{
    public class WebAppPersonListComponentTests : TestContext
    {
        private readonly Mock<IPersonService> _personServiceMock;

        public WebAppPersonListComponentTests()
        {
            _personServiceMock = new Mock<IPersonService>();
            Services.AddSingleton(_personServiceMock.Object);
        }

        [Fact]
        public void ShouldRenderPersonList()
        {
            // Arrange
            var persons = new List<PersonDto>
            {
                new PersonDto { Id = 1, FirstName = "John", LastName = "Doe", DateOfBirth = new DateTime(1980, 1, 1), Department = new DepartmentDto { Name = "HR" } },
                new PersonDto { Id = 2, FirstName = "Jane", LastName = "Doe", DateOfBirth = new DateTime(1990, 1, 1), Department = new DepartmentDto { Name = "IT" } }
            };
            _personServiceMock.Setup(service => service.GetPeopleAsync()).ReturnsAsync(persons);

            // Act
            var cut = RenderComponent<PersonList>();

            // Assert
            cut.FindAll("tbody tr").Count.Should().Be(2);
            cut.Find("#edit-button-1").TextContent.Should().Be("Edit");
            cut.Find("#edit-button-2").TextContent.Should().Be("Edit");
        }

        [Fact]
        public void ShouldRenderEmptyMessageWhenNoPersons()
        {
            // Arrange
            _personServiceMock.Setup(service => service.GetPeopleAsync()).ReturnsAsync(new List<PersonDto>());

            // Act
            var cut = RenderComponent<PersonList>();

            // Assert
            cut.Find("tbody tr td").TextContent.Should().Be("There are no people in the database yet. Click Add Person to enter a new person.");
        }

        [Fact]
        public void ShouldInvokeOnEditPersonCallbackWhenEditButtonClicked()
        {
            // Arrange
            var persons = new List<PersonDto>
            {
                new PersonDto { Id = 1, FirstName = "John", LastName = "Doe", DateOfBirth = new DateTime(1980, 1, 1), Department = new DepartmentDto { Name = "HR" } }
            };
            _personServiceMock.Setup(service => service.GetPeopleAsync()).ReturnsAsync(persons);
            var editPersonCalled = false;
            Func<PersonDto, Task> onEditPerson = person =>
            {
                editPersonCalled = true;
                return Task.CompletedTask;
            };

            // Act
            var cut = RenderComponent<PersonList>(parameters => parameters.Add(p => p.OnEditPerson, EventCallback.Factory.Create(this, onEditPerson)));
            cut.Find("#edit-button-1").Click();

            // Assert
            editPersonCalled.Should().BeTrue();
        }
    }
}
