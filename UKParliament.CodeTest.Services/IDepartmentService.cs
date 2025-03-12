using UKParliament.CodeTest.Dtos;

namespace UKParliament.CodeTest.Web.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync();
    }
}
