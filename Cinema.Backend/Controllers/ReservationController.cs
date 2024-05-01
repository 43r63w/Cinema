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
        private readonly GenericRepository<Reservation> _repository;

        public ReservationController(ILogger<ReservationController> logger,
            UnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.ReservationRepository;
        }


        [HttpGet("GetReservations")]
        public async Task<List<Reservation>> GetReservationAsync() => await _repository.Get()
            .Include(reservation => reservation.Session)
            .Include(reservation => reservation.Session.Movie)
            .Include(reservation => reservation.Session.Movie.Genre)
            .Include(reservation => reservation.Session.CinemaRoom)
            .Include(reservation => reservation.ApplicationUser)
            .ToListAsync();


        [HttpPost("AddReservation")]
        public async Task<IActionResult> AddReservationAsync([FromBody] Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.InsertAsync(reservation);
            return Ok();
        }


        [HttpDelete("DeleteReservation/{id}")]
        public async Task<IActionResult> DeleteReservationAsync(int id)
        {
            var reservation = await _repository.GetByIDAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);
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
                await _repository.UpdateAsync(updatedReservation);
                return Ok();
            }
            catch (DbUpdateConcurrencyException exception)
            {
                var reservation = await _repository.GetByIDAsync(id);
                if (reservation == null)
                {
                    return NotFound();
                }
                _logger.LogError(exception, "Unable to update the database", []);
                throw;
            }
        }

    }
}
