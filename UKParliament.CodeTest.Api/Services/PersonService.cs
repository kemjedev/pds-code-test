using AutoMapper;
using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Dtos;

namespace UKParliament.CodeTest.Api.Services
{
    public class PersonService : IPersonService
    {
        private readonly PersonManagerContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<PersonDto> _validator;

        public PersonService(PersonManagerContext context, IMapper mapper, IValidator<PersonDto> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Result<IEnumerable<PersonDto>>> GetPeopleAsync()
        {
            var people = await _context.People.Include(p => p.Department).ToListAsync();
            var peopleDto = _mapper.Map<IEnumerable<PersonDto>>(people);
            return Result.Ok(peopleDto);
        }

        public async Task<Result<PersonDto>> GetPersonByIdAsync(int id)
        {
            var person = await _context.People.FirstOrDefaultAsync();
            if (person != null)
            {
                return Result.Ok(_mapper.Map<PersonDto>(person));
            }
            return Result.Fail("Person not found.");
        }

        public async Task<Result<PersonDto>> CreateNewPersonAsync(PersonDto personDto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(personDto);
            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult.Errors.Select(e => new Error(e.ErrorMessage)).ToList());
            }

            var person = _mapper.Map<Person>(personDto);

            _context.People.Add(person);
            await _context.SaveChangesAsync();

            var createdPersonDto = _mapper.Map<PersonDto>(person);
            return Result.Ok(createdPersonDto);
        }

        public async Task<Result<PersonDto>> UpdatePersonAsync(PersonDto personDto)
        {

            ValidationResult validationResult = await _validator.ValidateAsync(personDto);
            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult.Errors.Select(e => new Error(e.ErrorMessage)).ToList());
            }

            var person = await _context.People.FindAsync(personDto.Id);
            if (person == null)
            {
                return Result.Fail("Person not found.");
            }

            try
            {
                //TODO - resolve issue with the mapping back to the entity
                //_mapper.Map(personDto, person);
                //user explicit property mapping for now

                person.FirstName = personDto.FirstName;
                person.LastName = personDto.LastName;
                person.DateOfBirth = (DateTime)personDto.DateOfBirth!;
                person.DepartmentId = personDto.DepartmentId;                

            }
            catch (Exception ex)
            {
                return Result.Fail("An error occurred while updating the person.");
            }

            _context.People.Update(person);
            await _context.SaveChangesAsync();

            var updatedPersonDto = _mapper.Map<PersonDto>(person);
            return Result.Ok(updatedPersonDto);
        }
    }
}