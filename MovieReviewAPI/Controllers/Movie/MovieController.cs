﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieReviewAPI.Data.MovieContext;
using MovieReviewAPI.Models.MovieList;
using Newtonsoft.Json;

[Route("api/movies")]
[ApiController]
[Authorize]
public class MovieController : ControllerBase
{
    private readonly ApplicationDBContext _context;

    public MovieController(ApplicationDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieListModel>>> GetMovies(
        [FromQuery] string searchString,
        [FromQuery] int? releaseYear,
        [FromQuery] double? minRating,
        [FromQuery] double? maxRating,
        [FromQuery] string genre,
        [FromQuery] string country,
        [FromQuery] string language,
        [FromQuery] string name,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        IQueryable<MovieListModel> query = _context.Movies;

        // Filter by search string
        if (!string.IsNullOrEmpty(searchString))
        {
            query = query.Where(m =>
                EF.Functions.Like(m.Name, $"%{searchString}%") ||
                EF.Functions.Like(m.Description, $"%{searchString}%") ||
                EF.Functions.Like(m.Genre, $"%{searchString}%") ||
                EF.Functions.Like(m.Country, $"%{searchString}%") ||
                EF.Functions.Like(m.Language, $"%{searchString}%")
            );
        }

        // Filter by release year
        if (releaseYear.HasValue)
        {
            query = query.Where(m => m.ReleaseYear >= releaseYear.Value);
        }

        // Filter by rating range
        if (minRating.HasValue)
        {
            query = query.Where(m => m.Rating >= minRating.Value);
        }

        if (maxRating.HasValue)
        {
            query = query.Where(m => m.Rating <= maxRating.Value);
        }

        // Filter by genre
        if (!string.IsNullOrEmpty(genre))
        {
            query = query.Where(m => EF.Functions.Like(m.Genre, $"%{genre}%"));
        }

        // Filter by country
        if (!string.IsNullOrEmpty(country))
        {
            query = query.Where(m => EF.Functions.Like(m.Country, $"%{country}%"));
        }

        // Filter by language
        if (!string.IsNullOrEmpty(language))
        {
            query = query.Where(m => EF.Functions.Like(m.Language, $"%{language}%"));
        }

        // Filter by name
        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(m => EF.Functions.Like(m.Name, $"%{name}%"));
        }

        // Paginate the result
        var totalCount = await query.CountAsync();
        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        if (page < 1)
        {
            page = 1;
        }

        if (page > totalPages)
        {
            page = totalPages;
        }

        var movies = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        // Create pagination metadata
        var paginationMetadata = new
        {
            totalCount,
            totalPages,
            currentPage = page,
            pageSize
        };

        // Add pagination metadata to the response headers
        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationMetadata));

        return movies;
    }



    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<MovieListModel>>> GetAllMovies()
    {
        var movies = await _context.Movies.ToListAsync();
        return movies;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MovieListModel>> GetMovieById(int id)
    {
        var movie = await _context.Movies.FindAsync(id);

        if (movie == null)
        {
            return NotFound();
        }

        return movie;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<MovieListModel>> CreateMovie([FromBody] MovieListModel newMovie)
    {
        _context.Movies.Add(newMovie);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetMovieById), new { id = newMovie.Id }, newMovie);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateMovie(int id, [FromBody] MovieListModel updatedMovie)
    {
        if (id != updatedMovie.Id)
        {
            return BadRequest();
        }

        _context.Entry(updatedMovie).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Movies.Any(m => m.Id == id))
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
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<MovieListModel>> DeleteMovie(int id)
    {
        var movie = await _context.Movies.FindAsync(id);

        if (movie == null)
        {
            return NotFound();
        }

        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();

        return movie;
    }
}