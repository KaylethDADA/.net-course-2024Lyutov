namespace BankSystem.Application.Dto.EmployeeDto
{
    public class GetEmployeeFilterRequest
    {
        public Guid? EmployeeId { get; set; }
        public string? Search { get; set; }
        public DateTime BirthDay { get; set; }
        public int Salary { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
