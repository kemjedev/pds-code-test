namespace UKParliament.CodeTest.Dtos
{
    public class GetPeopleResponseDto
    {
        public bool IsSuccess { get; set; } = false;
        public string Message { get; set; } = string.Empty;
        public IEnumerable<PersonDto>? People { get; set; }
    }
}
