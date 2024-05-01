using Cinema.DAL.Models;
using Cinema.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cinema.DAL.Implemantations;

namespace Cinema.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly ILogger<GenreController> _logger;
        private readonly UnitOfWork _unitOfWork;

        public GenreController(ILogger<GenreController> logger, 
            UnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetGenres")]
        public async Task<List<Genre>> GetGenresAsync() => await _unitOfWork.GenreRepository.Get().ToListAsync();

        [HttpPost("AddGenre")]
        public async Task<IActionResult> AddGenreAsync([FromBody] Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _unitOfWork.GenreRepository.InsertAsync(genre);
            return Ok();
        }

        [HttpDelete("DeleteGenre/{id}")]
        public async Task<IActionResult> DeleteGenreAsync(int id)
        {
            var genre = await _unitOfWork.GenreRepository.GetByIDAsync(id);
            if (genre == null)
            {
                return NotFound();
            }

            await _unitOfWork.GenreRepository.DeleteAsync(id);
            return Ok();
        }

        [HttpPut("UpdateGenre/{id}")]
        public async Task<IActionResult> UpdateGenreAsync(int id, [FromBody] Genre updatedGenre)
        {
            if (id != updatedGenre.Id)
            {
                return BadRequest();
            }

            try
            {
                await _unitOfWork.GenreRepository.UpdateAsync(updatedGenre);
                return Ok();
            }
            catch (DbUpdateConcurrencyException exception)
            {
                if (await _unitOfWork.GenreRepository.GetByIDAsync(id) == null)
                {
                    return NotFound();
                }
                _logger.LogError(exception, "Unable to update the database", []);
                throw;
            }
        }
    }
}
