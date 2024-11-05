namespace BankSystem.Application.Dto.ClientDto
{
    public class ClientResponse
    {
        public Guid ClientId { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string PassportNumber { get; set; }
    }
}
