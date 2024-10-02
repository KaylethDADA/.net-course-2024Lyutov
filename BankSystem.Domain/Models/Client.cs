namespace BankSystem.Domain.Models
{
    public class Client : Person
    {
        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            if (!(obj is Client client))
                return false;

            return FullName == client.FullName &&
                   BirthDay.Date == client.BirthDay.Date;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FullName, BirthDay.Date);
        }
    }
}
