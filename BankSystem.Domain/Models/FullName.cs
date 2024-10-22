using System.Text.Json;

namespace BankSystem.Domain.Models
{
    public class FullName
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; } = null;

        public override bool Equals(object? obj)
        {

            if (obj is not FullName entity || entity == null)
                return false;

            var serialEnti = Serialize(entity);
            var serialThis = Serialize(this);

            if (string.Compare(serialEnti, serialThis) != 0)
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            return Serialize(this).GetHashCode();
        }

        private string Serialize(FullName valueObjects)
        {
            var serializedObjects = JsonSerializer.Serialize(valueObjects);
            return serializedObjects;
        }
    }
}
