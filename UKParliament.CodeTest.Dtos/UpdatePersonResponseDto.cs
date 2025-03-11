namespace UKParliament.CodeTest.Dtos
{
    public class UpdatePersonResponseDto
    {
        public bool IsSuccess { get; set; } = false;
        public string Message { get; set; } = string.Empty;
        public PersonDto? Person { get; set; }
    }
}