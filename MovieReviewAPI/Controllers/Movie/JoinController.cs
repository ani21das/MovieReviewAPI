using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieReviewAPI.Data.MovieContext;

namespace MovieReviewAPI.Controllers.Movie
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JoinController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public JoinController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet("joins/inner")]
        public async Task<ActionResult<IEnumerable<object>>> InnerJoinMoviesAndReviews()
        {
            var innerJoinResult = await _context.Movies
                .Join(_context.Reviews,
                    movie => movie.Id,
                    review => review.MovieId,
                    (movie, review) => new
                    {
                        movie.Id,
                        movie.Name,
                        review.UserName,
                        review.Comment,
                        review.Recommended
                    })
                .ToListAsync();

            return Ok(innerJoinResult);
        }

        [HttpGet("joins/left")]
        public async Task<ActionResult<IEnumerable<object>>> LeftJoinMoviesAndReviews()
        {
            var leftJoinResult = await _context.Movies
                .GroupJoin(_context.Reviews,
                    movie => movie.Id,
                    review => review.MovieId,
                    (movie, reviews) => new
                    {
                        movie.Id,
                        movie.Name,
                        Reviews = reviews.DefaultIfEmpty() 
                            .Select(review => new
                            {
                                review.Id,
                                review.UserName,
                                review.Comment,
                                review.Recommended
                            })
                            .ToList()
                    })
                .ToListAsync();

            return Ok(leftJoinResult);
        }

        [HttpGet("joins/right")]
        public async Task<ActionResult<IEnumerable<object>>> RightJoinMoviesAndReviews()
        {
            var rightJoinResult = await _context.Reviews
                .GroupJoin(_context.Movies,
                    review => review.MovieId,
                    movie => movie.Id,
                    (review, movies) => new
                    {
                        review.Id,
                        review.UserName,
                        review.Comment,
                        review.Recommended,
                        Movies = movies.DefaultIfEmpty() 
                            .Select(movie => new
                            {
                                movie.Id,
                                movie.Name
                            })
                            .ToList()
                    })
                .ToListAsync();

            return Ok(rightJoinResult);
        }
    }
}
