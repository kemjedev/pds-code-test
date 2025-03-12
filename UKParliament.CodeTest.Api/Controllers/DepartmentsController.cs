using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UKParliament.CodeTest.Dtos;

[ApiController]
[Route("data-api/[controller]")]
public class DepartmentsController : ControllerBase
{
    private readonly IDepartmentService _departmentService;

    public DepartmentsController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpGet]
    //[Authorize]
    [Route("all")]
    public async Task<ActionResult<GetDepartmentsResponseDto>> GetDepartments()
    {
        var result = await _departmentService.GetDepartmentsAsync();

        if (result.IsSuccess)
        {
            return new GetDepartmentsResponseDto
            {
                IsSuccess = true,
                Message = "Departments retrieved successfully.",
                Departments = result.Value
            };
        }
        return BadRequest(new GetDepartmentsResponseDto
        {
            IsSuccess = false,
            Message = result.Errors.FirstOrDefault()?.Message ?? "An error occurred while retrieving people."
        });
    }
}
