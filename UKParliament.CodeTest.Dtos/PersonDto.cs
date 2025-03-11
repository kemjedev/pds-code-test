namespace UKParliament.CodeTest.Dtos
{
    public class PersonDto
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public int DepartmentId { get; set; }
        public required DepartmentDto Department { get; set; }
    }
}
