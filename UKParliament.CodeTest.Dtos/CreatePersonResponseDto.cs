namespace UKParliament.CodeTest.Dtos
{
    public class CreatePersonResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public PersonDto? Person { get; set; }
    }
}