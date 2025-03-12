namespace UKParliament.CodeTest.Dtos
{
    public class GetPeopleResponseDto : ResponseDtoBase
    {
        public IEnumerable<PersonDto>? People { get; set; }
    }
}
