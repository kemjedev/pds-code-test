using FluentResults;
using UKParliament.CodeTest.Dtos;
public interface IDepartmentService
{
    Task<Result<IEnumerable<DepartmentDto>>> GetDepartmentsAsync();
}