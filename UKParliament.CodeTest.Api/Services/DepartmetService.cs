using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Dtos;

namespace UKParliament.CodeTest.Api.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly PersonManagerContext _context;
        private readonly IMapper _mapper;

        public DepartmentService(PersonManagerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<DepartmentDto>>> GetDepartmentsAsync()
        {
            var departments = await _context.Departments.ToListAsync();
            var departmentsDto = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
            return Result.Ok(departmentsDto);
        }
    }
}