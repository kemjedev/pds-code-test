namespace UKParliament.CodeTest.Dtos
{
    public class ResponseDtoBase
    {
        public bool IsSuccess { get; set; } = false;
        public string Message { get; set; } = string.Empty;
    }
}
