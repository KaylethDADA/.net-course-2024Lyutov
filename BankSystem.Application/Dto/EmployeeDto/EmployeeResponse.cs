namespace BankSystem.Application.Dto.EmployeeDto
{
    public class EmployeeResponse
    {
        public Guid EmployeeId { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string PassportNumber { get; set; }
        public int Salary { get; set; }
        public string Contract { get; set; }
    }
}
