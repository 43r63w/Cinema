using Cinema.DAL.Models;
using Cinema.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public CategoryController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetCategories")]
        public async Task<List<Genre>> GetCategoriesAsync() => await _unitOfWork.CategoryRepository.Get().ToListAsync();

        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategoryAsync([FromBody] Genre category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _unitOfWork.CategoryRepository.InsertAsync(category);
            await _unitOfWork.SaveAsync();
            return Ok();
        }

        [HttpDelete("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategoryAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIDAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            await _unitOfWork.CategoryRepository.DeleteAsync(id);
            return Ok();
        }

        [HttpPut("UpdateCategory/{id}")]
        public async Task<IActionResult> UpdateCategoryAsync(int id, [FromBody] Genre updatedCategory)
        {
            if (id != updatedCategory.Id)
            {
                return BadRequest();
            }

            try
            {
                await _unitOfWork.CategoryRepository.UpdateAsync(updatedCategory);
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _unitOfWork.CategoryRepository.GetByIDAsync(id) == null)
                {
                    return NotFound();
                }
                throw;
            }
        }
    }
}
