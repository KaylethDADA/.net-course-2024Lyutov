namespace BankSystem.Application.Dto.ClientDto
{
    public class UpdateClientRequest
    {
        public Guid ClientId { get; set; }
        public string PhoneNumber { get; set; }
        public string PassportNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
    }
}
