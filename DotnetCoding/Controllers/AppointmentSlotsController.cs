using AutoMapper;
using DotnetCoding.DTOs;
using DotnetCoding.Services;
using DotnetCoding.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentSlotsController : ControllerBase
    {
        private readonly IAppointmentSlotService _slotService;
        private readonly IMapper _mapper;
        public AppointmentSlotsController(IAppointmentSlotService slotService, IMapper mapper)
        {
            _mapper = mapper;
            _slotService = slotService;
        }

        [HttpPost("CreateForProvider")]
        public async Task<IActionResult> CreateforProvider(int providerId, DateTime start, DateTime end)
        {
            try
            {
                await _slotService.CreateSlotsForProviderAsync(providerId, start, end);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("Available")]
        public async Task<IActionResult> GetAvailable([FromQuery] int? providerId, [FromQuery]DateTime? start, [FromQuery]DateTime? end)
        {
            try
            {
                var slots = await _slotService.GetAvailableSlotsAsync(providerId, start, end);
                var slotDtos = _mapper.Map<IEnumerable<AppointmentSlotDto>>(slots);
                return Ok(slotDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("provider/{providerId}")]
        public async Task<IActionResult> GetSlotsByProviderId(int providerId)
        {
            var slots = await _slotService.GetSlotsByProviderIdAsync(providerId);
            var slotDtos = _mapper.Map<IEnumerable<AppointmentSlotDto>>(slots);
            return Ok(slotDtos);
        }




        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var slots = await _slotService.GetAllAsync();
                var slotDtos = _mapper.Map<IEnumerable<AppointmentSlotDto>>(slots);
                return Ok(slotDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
