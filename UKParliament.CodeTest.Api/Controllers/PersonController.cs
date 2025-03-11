using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UKParliament.CodeTest.Api.Services;
using UKParliament.CodeTest.Dtos;

namespace UKParliament.CodeTest.Api.Controllers
{
    [ApiController]
    [Route("data-api/[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;

        public PersonsController(IMapper mapper, IPersonService personService)
        {
            _personService = personService;
            _mapper = mapper;
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult<GetPeopleResponseDto>> GetPeople()
        {
            var result = await _personService.GetPersonsAsync();

            if (result.IsSuccess)
            {
                return Ok(new GetPeopleResponseDto
                {
                    IsSuccess = true,
                    Message = "People retrieved successfully.",
                    People = result.Value
                });
            }

            return BadRequest(new GetPeopleResponseDto
            {
                IsSuccess = false,
                Message = result.Errors.FirstOrDefault()?.Message ?? "An error occurred while retrieving people."
            });
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CreatePersonResponseDto>> CreateNewPerson(PersonDto personDto)
        {
            var result = await _personService.CreateNewPersonAsync(personDto);

            if (result.IsSuccess)
            {
                return Ok(new CreatePersonResponseDto
                {
                    IsSuccess = true,
                    Message = "Person created successfully.",
                    Person = result.Value
                });
            }

            return BadRequest(new CreatePersonResponseDto
            {
                IsSuccess = false,
                Message = result.Errors.FirstOrDefault()?.Message ?? "An error occurred while creating the person."
            });
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<UpdatePersonResponseDto>> UpdatePerson(PersonDto updatePersonDto)
        {
            var result = await _personService.UpdatePersonAsync(updatePersonDto);

            if (result.IsSuccess)
            {
                return Ok(new UpdatePersonResponseDto
                {
                    IsSuccess = true,
                    Message = "Person updated successfully.",
                    Person = result.Value
                });
            }

            return BadRequest(new UpdatePersonResponseDto
            {
                IsSuccess = false,
                Message = result.Errors.FirstOrDefault()?.Message ?? "An error occurred while updating the person."
            });
        }

    }
}
