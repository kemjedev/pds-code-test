using FluentValidation;
using UKParliament.CodeTest.Dtos;

namespace UKParliament.CodeTest.Api.Validators
{
    public class PersonDtoValidator : AbstractValidator<PersonDto>
    {
        public PersonDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.");
            RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("Date of birth is required.");
            RuleFor(x => x.DepartmentId).GreaterThan(0).WithMessage("Department ID must be greater than 0.");
        }
    }
}
