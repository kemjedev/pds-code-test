using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UKParliament.CodeTest.Api.Services;
using UKParliament.CodeTest.Data;
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
        //[Authorize]
        [Route("all")]
        public async Task<ActionResult<GetPeopleResponseDto>> GetPeople()
        {
            var result = await _personService.GetPeopleAsync();

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

        [HttpGet("{id}")]
        //[Authorize]     
        public async Task<ActionResult<GetPersonResponseDto>> GetPersonById(int id)
        {
            var result = await _personService.GetPersonByIdAsync(id);

            if (result.IsSuccess) 
            {
                return Ok(new GetPersonResponseDto()
                {
                    IsSuccess = true,
                    Message = "Person retrieved successfully.",
                    Person = result.Value
                });
            }

            return NotFound(new GetPersonResponseDto()
            {
                IsSuccess = false,
                Message = "Person not found."
            });
        }

        [HttpPost]
        //[Authorize]
        [Route("create")]
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
        //[Authorize]
        [Route("update")]
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
