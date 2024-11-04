using BankSystem.Application.Dto.EmployeeDto;
using BankSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateEmployee(
            [FromBody] CreateEmployeeRequest request,
            CancellationToken cancellationToken)
        {
            await _employeeService.AddEmployeeAsync(request, cancellationToken);
            return Ok();
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateEmployee(
            [FromBody] UpdateEmployeeRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _employeeService.UpdateAsync(request, cancellationToken);
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var employee = await _employeeService.GetByIdAsync(id, cancellationToken);
            return Ok(employee);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetEmployees(
            [FromQuery] GetEmployeeFilterRequest request,
            CancellationToken cancellationToken)
        {
            var employees = await _employeeService.GetAsync(request, cancellationToken);
            return Ok(employees);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteEmployee(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            await _employeeService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
