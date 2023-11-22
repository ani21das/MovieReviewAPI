using Microsoft.AspNetCore.Authorization;
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
        IQueryable<MovieListModel> query = _context.Movies.Include(m => m.Reviews);

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

        if (releaseYear.HasValue)
        {
            query = query.Where(m => m.ReleaseYear >= releaseYear.Value);
        }

        if (minRating.HasValue)
        {
            query = query.Where(m => m.Rating >= minRating.Value);
        }

        if (maxRating.HasValue)
        {
            query = query.Where(m => m.Rating <= maxRating.Value);
        }

        if (!string.IsNullOrEmpty(genre))
        {
            query = query.Where(m => EF.Functions.Like(m.Genre, $"%{genre}%"));
        }

        if (!string.IsNullOrEmpty(country))
        {
            query = query.Where(m => EF.Functions.Like(m.Country, $"%{country}%"));
        }

        if (!string.IsNullOrEmpty(language))
        {
            query = query.Where(m => EF.Functions.Like(m.Language, $"%{language}%"));
        }

        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(m => EF.Functions.Like(m.Name, $"%{name}%"));
        }

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
        var movie = await _context.Movies.Include(m => m.Reviews).FirstOrDefaultAsync(m => m.Id == id);

        ////var movie = await _context.Movies.FindAsync(id);

        if (movie == null)
        {
            return NotFound();
        }

        return movie;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<MovieListModel>> CreateMovie([FromForm] MovieListModel newMovie)
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

    [HttpGet("movies/groupby/genre")]
    public async Task<ActionResult<IEnumerable<object>>> GroupMoviesByGenre()
    {
        var groupedMovies = await _context.Movies
            .GroupBy(m => m.Genre)
            .Select(group => new
            {
                Genre = group.Key,
                MovieCount = group.Count()
            })
            .ToListAsync();

        return Ok(groupedMovies);
    }

    [HttpGet("movies/groupby")]
    public async Task<ActionResult<IEnumerable<object>>> GroupMoviesBy(
    [FromQuery] string groupBy)
    {
        IQueryable<MovieListModel> query = _context.Movies.Include(m => m.Reviews);

        if (!string.IsNullOrEmpty(groupBy))
        {
            switch (groupBy.ToLower())
            {
                case "genre":
                    var groupedMoviesByGenre = await query
                        .GroupBy(m => m.Genre)
                        .Select(group => new
                        {
                            GroupKey = group.Key,
                            MovieCount = group.Count(),
                            AverageRating = group.Average(m => m.Rating)
                        })
                        .ToListAsync();
                    return Ok(groupedMoviesByGenre);

                case "releaseyear":
                    var groupedMoviesByReleaseYear = await query
                        .GroupBy(m => m.ReleaseYear)
                        .Select(group => new
                        {
                            GroupKey = group.Key,
                            MovieCount = group.Count(),
                            MaxRating = group.Max(m => m.Rating)
                        })
                        .ToListAsync();
                    return Ok(groupedMoviesByReleaseYear);

                case "country":
                    var groupedMoviesByCountry = await query
                        .GroupBy(m => m.Country)
                        .Select(group => new
                        {
                            GroupKey = group.Key,
                            MovieCount = group.Count(),
                            AverageRating = group.Average(m => m.Rating)
                        })
                        .ToListAsync();
                    return Ok(groupedMoviesByCountry);

                case "language":
                    var groupedMoviesByLanguage = await query
                        .GroupBy(m => m.Language)
                        .Select(group => new
                        {
                            GroupKey = group.Key,
                            MovieCount = group.Count(),
                            MinRating = group.Min(m => m.Rating)
                        })
                        .ToListAsync();
                  
                    return Ok(groupedMoviesByLanguage);
                default:
                    return BadRequest("Invalid groupBy parameter.");
            }
        }
         return BadRequest("Please provide a valid groupBy parameter.");
    }
}

