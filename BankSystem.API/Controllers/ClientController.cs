using BankSystem.Application.Dto.ClientDto;
using BankSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ClientService _clientService;

        public ClientController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateClient(
            [FromBody] CreateClientRequest request,
            CancellationToken cancellationToken)
        {
            await _clientService.AddClientAsync(request, cancellationToken);
            return Ok();
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateClient(
            [FromBody] UpdateClientRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _clientService.UpdateClientAsync(request, cancellationToken);
            return Ok(result);
        }

        [HttpGet("GetById/{clientId}")]
        public async Task<IActionResult> GetClientById(
            [FromRoute] Guid clientId,
            CancellationToken cancellationToken)
        {
            var client = await _clientService.GetByIdAsync(clientId, cancellationToken);
            return Ok(client);
        }

        [HttpPost("Get")]
        public async Task<IActionResult> GetClients(
            [FromQuery] GetClientFilterRequest filterRequest,
            CancellationToken cancellationToken)
        {
            var clients = await _clientService.GetAsync(filterRequest, cancellationToken);
            return Ok(clients);
        }

        [HttpDelete("Delete/{clientId}")]
        public async Task<IActionResult> DeleteClient(
            [FromRoute] Guid clientId,
            CancellationToken cancellationToken)
        {
            await _clientService.DeleteClientAsync(clientId, cancellationToken);
            return Ok();
        }
    }
}
