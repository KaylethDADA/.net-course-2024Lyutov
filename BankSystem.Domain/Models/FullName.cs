using System.Text.Json;

namespace BankSystem.Domain.Models
{
    public class FullName
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; } = null;
    }
}
