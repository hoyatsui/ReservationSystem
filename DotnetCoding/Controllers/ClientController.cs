using Microsoft.AspNetCore.Mvc;
using DotnetCoding.Core.Models;
using DotnetCoding.Services.Interfaces;
using DotnetCoding.Services;
using AutoMapper;
using DotnetCoding.DTOs;

namespace DotnetCoding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        public readonly IClientService _clientService;
        private readonly IMapper _mapper;
        public ClientController(IClientService clientService, IMapper mapper)
        {
            _clientService = clientService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get the list of providers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            var clients = await _clientService.GetAllClientsAsync();
            if(clients == null)
            {
                return NotFound();
            }
            var clientDtos = _mapper.Map<IEnumerable<ClientDto>>(clients); 
            return Ok(clientDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient(string name)
        {
          

            var createdProvider = await _clientService.CreateClientAsync(name);
            return CreatedAtAction(nameof(CreateClient), new { id = createdProvider.Id }, createdProvider);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            try
            {
                await _clientService.DeleteClientAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
