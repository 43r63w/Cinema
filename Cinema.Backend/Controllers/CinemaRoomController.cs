using Cinema.DAL.Models;
using Cinema.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cinema.DAL.Implemantations;

namespace Cinema.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemaRoomController : ControllerBase
    {
        private readonly ILogger<CinemaRoomController> _logger;
        private readonly UnitOfWork _unitOfWork;
        private readonly GenericRepository<CinemaRoom> _repository;

        public CinemaRoomController(ILogger<CinemaRoomController> logger, 
            UnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.CinemaRoomRepository;
        }

        [HttpGet("GetCinemaRooms")]
        public async Task<List<CinemaRoom>> GetCinemaRoomsAsync() => await _repository.Get().ToListAsync();

        [HttpPost("AddCinemaRoom")]
        public async Task<IActionResult> AddCinemaRoomAsync([FromBody] CinemaRoom cinemaRoom)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.InsertAsync(cinemaRoom);
            return Ok();
        }

        [HttpDelete("DeleteCinemaRoom/{id}")]
        public async Task<IActionResult> DeleteCinemaRoomAsync(int id)
        {
            var cinemaRoom = await _repository.GetByIDAsync(id);
            if (cinemaRoom == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);
            return Ok();
        }

        [HttpPut("UpdateCinemaRoom/{id}")]
        public async Task<IActionResult> UpdateCinemaRoomAsync(int id, [FromBody] CinemaRoom updatedCinemaRoom)
        {
            if (id != updatedCinemaRoom.Id)
            {
                return BadRequest();
            }

            try
            {
                await _repository.UpdateAsync(updatedCinemaRoom);
                return Ok();
            }
            catch (DbUpdateConcurrencyException exception)
            {
                if (await _repository.GetByIDAsync(id) == null)
                {
                    return NotFound();
                }
                _logger.LogError(exception, "Unable to update the database", []);
                throw;
            }
        }
    }
}
