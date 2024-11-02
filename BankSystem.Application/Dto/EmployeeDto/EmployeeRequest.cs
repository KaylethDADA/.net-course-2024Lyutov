namespace BankSystem.Application.Dto.EmployeeDto
{
    public class EmployeeRequest
    {
        public string PhoneNumber { get; set; }
        public string PassportNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public int Salary { get; set; }
        public string Contract { get; set; }
    }
}
