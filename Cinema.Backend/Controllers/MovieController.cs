﻿using Cinema.DAL.Models;
using Cinema.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cinema.DAL.Implemantations;

namespace Cinema.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly ILogger<MovieController> _logger;
        private readonly UnitOfWork _unitOfWork;

        public MovieController(ILogger<MovieController> logger,
            UnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }


        [HttpGet("GetMovies")]
        public async Task<List<Movie>> GetMoviesAsync() => await _unitOfWork.MovieRepository.Get().Include(movie => movie.Genre).ToListAsync();


        [HttpPost("AddMovie")]
        public async Task<IActionResult> AddMovieAsync([FromBody] Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _unitOfWork.MovieRepository.InsertAsync(movie);
            return Ok();
        }


        [HttpDelete("DeleteMovie/{id}")]
        public async Task<IActionResult> DeleteMovieAsync(int id)
        {
            var movie = await _unitOfWork.MovieRepository.GetByIDAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            await _unitOfWork.MovieRepository.DeleteAsync(id);
            return Ok();
        }

        [HttpPut("UpdateMovie/{id}")]
        public async Task<IActionResult> UpdateMovieAsync(int id, [FromBody] Movie updatedMovie)
        {
            if (id != updatedMovie.Id)
            {
                return BadRequest();
            }


            try
            {
                await _unitOfWork.MovieRepository.UpdateAsync(updatedMovie);
                return Ok();
            }
            catch (DbUpdateConcurrencyException exception)
            {
                var movie = await _unitOfWork.MovieRepository.GetByIDAsync(id);
                if (movie == null)
                {
                    return NotFound();
                }
                _logger.LogError(exception, "Unable to update the database", []);
                throw;
            }
        }

    }
}
