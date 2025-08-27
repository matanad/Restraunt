using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepository reservationRepository;
        public ReservationController(IReservationRepository reservationRepository)
        {
            this.reservationRepository = reservationRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllReservations()
        {
            try
            {
                var reservations = await reservationRepository.GetAllReservationsAsync();
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationById(int id)
        {
            try
            {
                var reservation = await reservationRepository.GetReservationByIdAsync(id);
                return reservation == null ? NotFound() : Ok(reservation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddReservation([FromBody] NewReservation reservation)
        {
            try
            {
                var createdReservation = await reservationRepository.AddReservationAsync(reservation);
                return CreatedAtAction(nameof(GetReservationById), new { id = createdReservation.Id }, createdReservation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            try
            {
                await reservationRepository.DeleteReservationAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservation(int id, [FromBody] Models.Reservation reservation)
        {
            try
            {
                var updatedReservation = await reservationRepository.UpdateReservationAsync(id, reservation);
                return Ok(updatedReservation);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
