using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Dtos;

namespace UKParliament.CodeTest.Api.Controllers
{
    [ApiController]
    [Route("data-api/[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly PersonManagerContext _context;
        private readonly IMapper _mapper;

        public PersonsController(PersonManagerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetPeople()
        {
            var people = await _context.People.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<PersonDto>>(people));
        }

        // Other actions
    }
}
