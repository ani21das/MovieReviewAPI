using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieReviewAPI.Data.MovieContext;
using MovieReviewAPI.Models.MovieList;
using OfficeOpenXml;

namespace MovieReviewAPI.Controllers.Movie
{
    [Route("api/[controller]")]
    [ApiController]

    public class MovieListImportExportController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public MovieListImportExportController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet("export-excel")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> ExportsMoviesToExcel()
        {
            var movies = await _context.Movies.ToListAsync();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("MovieListAPI");
                var headers = new string[] { "ID", "Name", "Description", "ReleaseYear", "Rating", "Genre", "Country", "Language" };

                for (var i = 0; i < headers.Length; i++)
                {
                    worksheet.Cells[1, i + 1].Value = headers[i];
                }

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

                worksheet.Cells.AutoFitColumns();

                var content = package.GetAsByteArray();
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MovieList.xlsx");
            }
        }

        [HttpPost("import-excel")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> ImportMoviesFromExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is empty");
            }

            try
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            var movie = new MovieListModel
                            {
                                Name = GetValueFromCell(worksheet, row, 2),
                                Description = GetValueFromCell(worksheet, row, 3),
                                ReleaseYear = ParseIntFromCell(worksheet, row, 4),
                                Rating = ParseDoubleFromCell(worksheet, row, 5),
                                Genre = GetValueFromCell(worksheet, row, 6),
                                Country = GetValueFromCell(worksheet, row, 7),
                                Language = GetValueFromCell(worksheet, row, 8),
                            };

                            if (string.IsNullOrEmpty(movie.Name) || movie.ReleaseYear <= 0)
                            {
                                return BadRequest(new { Message = $"Invalid data at row {row}. Please check the file and try again." });
                            }

                            
                            var existingMovie = await _context.Movies.SingleOrDefaultAsync(m => m.Name == movie.Name);

                            if (existingMovie != null)
                            {
                                existingMovie.Description = movie.Description;
                                existingMovie.ReleaseYear = movie.ReleaseYear;
                                existingMovie.Rating = movie.Rating;
                                existingMovie.Genre = movie.Genre;
                                existingMovie.Country = movie.Country;
                                existingMovie.Language = movie.Language;
                            }
                            else
                            {
                                _context.Movies.Add(movie);
                            }
                        }

                        await _context.SaveChangesAsync();

                        return Ok(new { Message = $"{rowCount - 1} movies imported successfully." });
                    }
                }
            }
            catch (DbUpdateException )
            {
                return BadRequest(new { Message = "Error occurred during database update. Check the logs for details." });
            }
        }

        private string GetValueFromCell(ExcelWorksheet worksheet, int row, int column)
        {
            var cellValue = worksheet.Cells[row, column].Value;
            return cellValue?.ToString() ?? string.Empty;
        }

        private int ParseIntFromCell(ExcelWorksheet worksheet, int row, int column)
        {
            var cellValue = worksheet.Cells[row, column].Value;
            return cellValue != null && int.TryParse(cellValue.ToString(), out var result) ? result : 0;
        }

        private double ParseDoubleFromCell(ExcelWorksheet worksheet, int row, int column)
        {
            var cellValue = worksheet.Cells[row, column].Value;
            return cellValue != null && double.TryParse(cellValue.ToString(), out var result) ? result : 0.0;
        }
    }
}
