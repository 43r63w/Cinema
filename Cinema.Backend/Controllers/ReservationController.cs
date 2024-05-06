using Cinema.DAL.Models;
using Cinema.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cinema.DAL.Implemantations;

namespace Cinema.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ILogger<ReservationController> _logger;
        private readonly UnitOfWork _unitOfWork;

        public ReservationController(ILogger<ReservationController> logger,
            UnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }


        [HttpGet("GetReservations")]
        public async Task<IEnumerable<Reservation>> GetReservationsAsync() => await _unitOfWork.ReservationRepository.Get();


        [HttpPost("AddReservation")]
        public async Task<IActionResult> AddReservationAsync([FromBody] Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _unitOfWork.ReservationRepository.InsertAsync(reservation);
            await _unitOfWork.SaveAsync();
            return Ok();
        }


        [HttpDelete("DeleteReservation/{id}")]
        public async Task<IActionResult> DeleteReservationAsync(int id)
        {
            var reservation = await _unitOfWork.ReservationRepository.GetByIDAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            await _unitOfWork.ReservationRepository.DeleteAsync(id);
            return Ok();
        }

        [HttpPut("UpdateReservation/{id}")]
        public async Task<IActionResult> UpdateReservationAsync(int id, [FromBody] Reservation updatedReservation)
        {
            if (id != updatedReservation.ReservationId)
            {
                return BadRequest();
            }

            try
            {
                await _unitOfWork.ReservationRepository.UpdateAsync(updatedReservation);
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _unitOfWork.ReservationRepository.GetByIDAsync(id) == null)
                {
                    return NotFound();
                }
                throw;
            }
        }

    }
}
