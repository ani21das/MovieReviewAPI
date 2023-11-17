﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieReviewAPI.Data.MovieContext;
using MovieReviewAPI.Models.MovieList;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;

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


    
    [HttpGet("export-excel")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> ExportMoviesToExcel()
    {
        var movies = await _context.Movies.ToListAsync();

        // Create a new Excel package
        using (var package = new ExcelPackage())
        {
            // Add a worksheet to the package
            var worksheet = package.Workbook.Worksheets.Add("MovieList");

            // Set headers
            var headers = new string[] { "ID", "Name", "Description", "ReleaseYear", "Rating", "Genre", "Country", "Language" };
            for (var i = 0; i < headers.Length; i++)
            {
                worksheet.Cells[1, i + 1].Value = headers[i];
            }

            // Populate data
            for (var row = 0; row < movies.Count; row++)
            {
                worksheet.Cells[row + 2, 1].Value = movies[row].Id;
                worksheet.Cells[row + 2, 2].Value = movies[row].Name;
                worksheet.Cells[row + 2, 3].Value = movies[row].Description;
                worksheet.Cells[row + 2, 4].Value = movies[row].ReleaseYear;
                worksheet.Cells[row + 2, 5].Value = movies[row].Rating;
                worksheet.Cells[row + 2, 6].Value = movies[row].Genre;
                worksheet.Cells[row + 2, 7].Value = movies[row].Country;
                worksheet.Cells[row + 2, 8].Value = movies[row].Language;
            }

            // Auto-fit columns for better readability
            worksheet.Cells.AutoFitColumns();

            // Set the content type and return the Excel file
            var content = package.GetAsByteArray();
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MovieList.xlsx");
        }
    }

}
