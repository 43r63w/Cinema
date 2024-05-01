using Cinema.DAL.Models;
using Cinema.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cinema.DAL.Implemantations;

namespace Cinema.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatReservationController : ControllerBase
    {
        private readonly ILogger<SeatReservationController> _logger;
        private readonly UnitOfWork _unitOfWork;
        private readonly GenericRepository<SeatReservation> _repository;

        public SeatReservationController(ILogger<SeatReservationController> logger,
            UnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.SeatResarvationRepository;
        }


        [HttpGet("GetSeatReservations")]
        public async Task<List<SeatReservation>> GetSeatReservationsAsync() => await _repository.Get()
            .Include(seatReservation => seatReservation.Reservation)
            .Include(seatReservation => seatReservation.Reservation.Session)
            .Include(seatReservation => seatReservation.Reservation.Session.Movie)
            .Include(seatReservation => seatReservation.Reservation.Session.Movie.Genre)
            .Include(seatReservation => seatReservation.Reservation.Session.CinemaRoom)
            .Include(seatReservation => seatReservation.Reservation.ApplicationUser)
            .Include(seatReservation => seatReservation.Session)
            .Include(seatReservation => seatReservation.Session.Movie)
            .Include(seatReservation => seatReservation.Session.Movie.Genre)
            .Include(seatReservation => seatReservation.Session.CinemaRoom)
            .ToListAsync();


        [HttpPost("AddSeatReservation")]
        public async Task<IActionResult> AddSeatReservationAsync([FromBody] SeatReservation seatReservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.InsertAsync(seatReservation);
            return Ok();
        }


        [HttpDelete("DeleteSeatReservation/{id}")]
        public async Task<IActionResult> DeleteSeatReservationAsync(int id)
        {
            var seatReservation = await _repository.GetByIDAsync(id);
            if (seatReservation == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);
            return Ok();
        }

        [HttpPut("UpdateSeatReservation/{id}")]
        public async Task<IActionResult> UpdateSeatReservationAsync(int id, [FromBody] SeatReservation updatedSeatReservation)
        {
            if (id != updatedSeatReservation.Id)
            {
                return BadRequest();
            }


            try
            {
                await _repository.UpdateAsync(updatedSeatReservation);
                return Ok();
            }
            catch (DbUpdateConcurrencyException exception)
            {
                var seatReservation = await _repository.GetByIDAsync(id);
                if (seatReservation == null)
                {
                    return NotFound();
                }
                _logger.LogError(exception, "Unable to update the database", []);
                throw;
            }
        }

    }
}
