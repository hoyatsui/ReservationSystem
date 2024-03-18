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
    public class ProvidersController : ControllerBase
    {
        public readonly IProviderService _providerService;
        private readonly IMapper _mapper;
        public ProvidersController(IProviderService productService, IMapper mapper)
        {
            _providerService = productService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get the list of providers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetProviders()
        {
            var providers = await _providerService.GetAllProviders();
            if(providers == null)
            {
                return NotFound();
            }
            var providerDtos = _mapper.Map<IEnumerable<ProviderDto>>(providers);
            return Ok(providerDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProvider(string name)
        {
           

            var createdProvider = await _providerService.CreateProviderAsync(name);

            return CreatedAtAction(nameof(CreateProvider), new { id = createdProvider.Id }, createdProvider);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvider(int id)
        {
            try
            {
                await _providerService.DeleteProviderAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
