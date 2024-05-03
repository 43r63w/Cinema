using Cinema.DAL.Implemantations; 
using Cinema.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatReservationController : ControllerBase
    {
        private readonly ISeatReservationRepository _seatReservationRepository;

        public SeatReservationController(ISeatReservationRepository seatReservationRepository)
        {
            _seatReservationRepository = seatReservationRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<SeatReservation>>> GetAllAsync()
        {
            var seatReservations = await _seatReservationRepository.GetAllAsync().ToListAsync();
            return Ok(seatReservations); 
        }

        [HttpGet("{id}")] 
        public async Task<ActionResult<SeatReservation>> GetByIdAsync(int id)
        {
            var seatReservation = await _seatReservationRepository.GetByIdAsync(id);
            if (seatReservation == null)
            {
                return NotFound();
            }
            return Ok(seatReservation);
        }

        [HttpPost]
        public async Task<ActionResult<SeatReservation>> CreateAsync(SeatReservation model)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            await _seatReservationRepository.InsertAsync(model);
            await _seatReservationRepository.SaveAsync(); 

            return CreatedAtRoute("GetSeatReservation", new { id = model.Id }, model); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, SeatReservation model)
        {
            if (id != model.Id) 
            {
                return BadRequest();
            }

            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _seatReservationRepository.UpdateAsync(model);
                await _seatReservationRepository.SaveAsync();
            }
            catch (DbUpdateConcurrencyException) 
            {
                if (!await _seatReservationRepository.ExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw; 
                }
            }

            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var seatReservation = await _seatReservationRepository.GetByIdAsync(id);
            if (seatReservation == null)
            {
                return NotFound();
            }

            await _seatReservationRepository.DeleteAsync(id);
            await _seatReservationRepository.SaveAsync();

            return NoContent();
        }
    }
}
