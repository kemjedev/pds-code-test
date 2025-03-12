namespace UKParliament.CodeTest.Dtos
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public int DepartmentId { get; set; }
        public DepartmentDto? Department { get; set; } 
    }
}
