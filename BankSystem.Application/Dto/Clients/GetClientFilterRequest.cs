namespace BankSystem.Application.Dto.ClientDto
{
    public class GetClientFilterRequest
    {
        public Guid? ClientId { get; set; }
        public string? Search { get; set; }
        public DateTime? BirthDay { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}