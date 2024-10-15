namespace BankSystem.Domain.Models
{
    public class Client : Person
    {
        public ICollection<Account> Accounts { get; set; }
        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            if (!(obj is Client client))
                return false;

            return PassportNumber == client.PassportNumber;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PassportNumber);
        }
    }
}
