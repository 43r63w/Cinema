
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cinema.DAL.Implemantations;

namespace Cinema.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ILogger<SessionController> _logger;
        private readonly UnitOfWork _unitOfWork;

        public SessionController(ILogger<SessionController> logger,
            UnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }


        [HttpGet("GetSessions")]
        public async Task<IEnumerable<Session>> GetSessionsAsync() => await _unitOfWork.SessionRepository.Get();


        [HttpGet("GetSessionsByGenre/{genre}")]
        public async Task<IEnumerable<Session>> GetSessionsByGenreAsync(string genre)
        {
            var sessions = await _unitOfWork.SessionRepository.Get(
                includeProperties: "Movie,Movie.Genre",
                filter: s => s.Movie.Genre.Name == genre
            );

            return sessions;
        }




        [HttpPost("AddSession")]
        public async Task<IActionResult> AddSessionAsync([FromBody] Session session)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _unitOfWork.SessionRepository.InsertAsync(session);
            await _unitOfWork.SaveAsync();
            return Ok();
        }


        [HttpDelete("DeleteSession/{id}")]
        public async Task<IActionResult> DeleteSessionAsync(int id)
        {
            var session = await _unitOfWork.SessionRepository.GetByIDAsync(id);
            if (session == null)
            {
                return NotFound();
            }

            await _unitOfWork.SessionRepository.DeleteAsync(id);
            return Ok();
        }

        [HttpPut("UpdateSession/{id}")]
        public async Task<IActionResult> UpdateSessionAsync(int id, [FromBody] Session updatedSession)
        {
            if (id != updatedSession.SessionId)
            {
                return BadRequest();
            }

            try
            {
                await _unitOfWork.SessionRepository.UpdateAsync(updatedSession);
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _unitOfWork.SessionRepository.GetByIDAsync(id) == null)
                {
                    return NotFound();
                }
                throw;
            }
        }

    }
}
