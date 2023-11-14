using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieReviewAPI.Data.MovieContext;
using MovieReviewAPI.Models.MovieList;

namespace MovieReviewAPI.Controllers.Movie
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public ReviewController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpPost("{id}/reviews")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<MovieListModel>> AddReview(int id, [FromForm] ReviewModel reviewModel)
        {
            var userName = User.Identity.Name;

            var movie = await _context.Movies.Include(m => m.Reviews).FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            var newReview = new ReviewModel
            {
                Comment = reviewModel.Comment,
                Recommended = reviewModel.Recommended,
                UserName = userName,
                MovieId = id,
                MovieName = movie.Name,
                CreatedAt = DateTime.UtcNow
            };

            movie.Reviews.Add(newReview);
            await _context.SaveChangesAsync();

            return Ok(newReview);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewModel>> GetReviewByMovieId(int id)
        {
            var reviews = await _context.Reviews
                     .Where(r => r.MovieId == id)
                     .ToListAsync();

            if (reviews == null || !reviews.Any())
            {
                return NotFound();
            }

            return Ok(reviews);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewModel>>> GetAllReviews()
        {
            var reviews = await _context.Reviews.ToListAsync();

            return Ok(reviews);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<ReviewModel>>> GetFilteredReviews(
            [FromQuery] string MovieName,
            [FromQuery] string UserName,
            [FromQuery] bool? Recommended,
            [FromQuery] string searchString)
        {
            var query = _context.Reviews.AsQueryable();

            // Filter by MovieName
            if (!string.IsNullOrEmpty(MovieName))
            {
                query = query.Where(r => EF.Functions.Like(r.MovieName, $"%{searchString}%"));
            }

            if (!string.IsNullOrEmpty(UserName))
            {
                query = query.Where(r => EF.Functions.Like(r.UserName, $"%{searchString}%"));
            }

            if (Recommended.HasValue)
            {
                query = query.Where(r => r.Recommended == Recommended.Value);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(r =>
                    EF.Functions.Like(r.UserName,$"%{searchString}%") ||
                    EF.Functions.Like(r.MovieName, $"%{searchString}%") ||
                    EF.Functions.Like(r.Comment, $"%{searchString}%"));
            }

            var filteredReviews = await query.ToListAsync();

            return Ok(filteredReviews);
        }
    }
}
