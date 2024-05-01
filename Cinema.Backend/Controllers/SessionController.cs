using Cinema.DAL.Models;
using Cinema.DAL;
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
        public async Task<List<Session>> GetSessionsAsync() => await _unitOfWork.SessionRepository.Get()
            .Include(session => session.Movie)
            .Include(session => session.Movie.Genre)
            .Include(session => session.CinemaRoom)
            .ToListAsync();


        [HttpPost("AddSession")]
        public async Task<IActionResult> AddSessionAsync([FromBody] Session session)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _unitOfWork.SessionRepository.InsertAsync(session);
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
            catch (DbUpdateConcurrencyException exception)
            {
                var session = await _unitOfWork.SessionRepository.GetByIDAsync(id);
                if (session == null)
                {
                    return NotFound();
                }
                _logger.LogError(exception, "Unable to update the database", []);
                throw;
            }
        }

    }
}
