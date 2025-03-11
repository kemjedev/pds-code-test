namespace UKParliament.CodeTest.Dtos
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<PersonDto> People { get; set; } = new List<PersonDto>();
    }
}
