using AutoMapper;
using DotnetCoding.DTOs;
using DotnetCoding.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        public ReservationsController(IReservationService reservationService, IMapper mapper)
        {
            _reservationService = reservationService;
            _mapper = mapper;
        }


        // Post: api/Reservation
        [HttpPost]
        public async Task<IActionResult> CreateReservation(int clientId, int appointmentSlotId)
        {
            try
            {
                var reservation = await _reservationService.CreateReservationAsync(clientId, appointmentSlotId);
                var reservationDto = _mapper.Map<ReservationDto>(reservation);
                return CreatedAtAction(nameof(GetReservation), new { id = reservationDto.Id }, reservationDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await _reservationService.GetReservationsAsync();
            var reservationDtos = _mapper.Map<IEnumerable<ReservationDto>>(reservations);
            return Ok(reservationDtos);
        }

        //Get
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservation(int id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            
            if(reservation == null)
            {
                return NotFound();
            }
            var reservationDto = _mapper.Map<ReservationDto>(reservation);
            return Ok(reservationDto);
        }

        //Get
        [HttpGet("client/{clientId}")]
        public async Task<IActionResult> GetReservationByClientId(int clientId)
        {
            var reservations = await _reservationService.GetReservationsByClientIdAsync(clientId);
            var reservationDtos = _mapper.Map<IEnumerable<ReservationDto>>(reservations);
            return Ok(reservationDtos);
        }
        [HttpGet("provider/{providerId}")]
        public async Task<IActionResult> GetReservationByProviderId(int providerId)
        {
            var reservations = await _reservationService.GetReservationsByProviderIdAsync(providerId);
            var reservationDtos = _mapper.Map<IEnumerable<ReservationDto>>(reservations);
            return Ok(reservationDtos);
        }
        [HttpPost("{id}/confirm")] 
        public async Task<IActionResult> ConfirmReservation(int id)
        {
            try
            {
                await _reservationService.ConfirmReservationAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> CancelReservation(int id)
        {
            try
            {
                await _reservationService.CancelReservationAsync(id);
                await _reservationService.RemoveReservationExpirationKeyAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            try
            {
                await _reservationService.DeleteReservationAsync(id);
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
