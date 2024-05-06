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
        private readonly UnitOfWork _unitOfWork;

        public GenreController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetCategories")]
        public async Task<IEnumerable<Genre>> GetCategoriesAsync() => await _unitOfWork.GenreRepository.Get();

        [HttpPost("AddGanre")]
        public async Task<IActionResult> AddGanreAsync([FromBody] Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _unitOfWork.GenreRepository.InsertAsync(genre);
            await _unitOfWork.SaveAsync();
            return Ok();
        }

        [HttpDelete("DeleteGanre/{id}")]
        public async Task<IActionResult> DeleteGanreAsync(int id)
        {
            var category = await _unitOfWork.GenreRepository.GetByIDAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            await _unitOfWork.GenreRepository.DeleteAsync(id);
            return Ok();
        }

        [HttpPut("UpdateGanre/{id}")]
        public async Task<IActionResult> UpdateGanreAsync(int id, [FromBody] Genre updatedGenre)
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
            catch (DbUpdateConcurrencyException)
            {
                if (await _unitOfWork.GenreRepository.GetByIDAsync(id) == null)
                {
                    return NotFound();
                }
                throw;
            }
        }
    }
}
