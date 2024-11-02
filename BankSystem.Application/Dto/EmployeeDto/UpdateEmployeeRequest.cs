namespace BankSystem.Application.Dto.EmployeeDto
{
    public class UpdateEmployeeRequest : EmployeeRequest
    {
        public Guid EmployeeId { get; set; }
    }
}
