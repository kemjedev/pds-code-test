namespace UKParliament.CodeTest.Dtos
{
    public class GetDepartmentsResponseDto : ResponseDtoBase
    {
        public IEnumerable<DepartmentDto>? Departments { get; set; }
    }
}
