using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieReviewAPI.Data.MovieContext;
using MovieReviewAPI.Models.MovieList;
using MovieReviewAPI.Models.PlayList;

namespace MovieReviewAPI.Controllers.Movie
{
    public class PlayListController
    {
    }
}

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "User")]
public class PlayListController : ControllerBase
{
    private readonly ApplicationDBContext _context;

    public PlayListController(ApplicationDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlayListModel>>> GetPlaylists()
    {
        var userName = User.Identity.Name;
        var playlists = await _context.PlayLists
        .Where(p => p.UserName == userName)
            .Include(p => p.Movies)
            .ToListAsync();

        return Ok(playlists);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PlayListModel>> GetPlaylistById(int id)
    {
        var userName = User.Identity.Name;
        var playlist = await _context.PlayLists
            .Where(p => p.UserName == userName && p.Id == id)
            .Include(p => p.Movies)
        .FirstOrDefaultAsync();
        if (playlist == null)
        {
            return NotFound();
        }
        return Ok(playlist);
    }
    [HttpPost]
    public async Task<ActionResult<PlayListModel>> CreatePlaylist([FromForm] PlayListModel newPlaylist)
    {
        var userName = User.Identity.Name;

        // Check if the provided ID is 0 (default for int) or not specified
        if (newPlaylist.Id != 0)
        {
            return BadRequest("Do not specify an ID when creating a new playlist. The ID will be generated automatically.");
        }

        newPlaylist.UserName = userName;

        _context.PlayLists.Add(newPlaylist);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPlaylistById), new { id = newPlaylist.Id }, newPlaylist);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePlaylist(int id, [FromForm] PlayListModel updatedPlaylist)
    {
        var userName = User.Identity.Name;

        var playlist = await _context.PlayLists
            .Where(p => p.UserName == userName && p.Id == id)
            .FirstOrDefaultAsync();

        if (playlist == null)
        {
            return NotFound();
        }

        // Update playlist properties here
        playlist.Name = updatedPlaylist.Name;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<PlayListModel>> DeletePlaylist(int id)
    {
        var userName = User.Identity.Name;

        var playlist = await _context.PlayLists
            .Where(p => p.UserName == userName && p.Id == id)
            .FirstOrDefaultAsync();

        if (playlist == null)
        {
            return NotFound();
        }

        _context.PlayLists.Remove(playlist);
        await _context.SaveChangesAsync();

        return Ok(playlist);
    }

    // Add movies in playlist
    [HttpPost("{id}/add-movie")]
    public async Task<IActionResult> AddMovieToPlaylist(int id, [FromForm] int movieId)
    {
        var userName = User.Identity.Name;

        var playlist = await _context.PlayLists
            .Where(p => p.UserName == userName && p.Id == id)
            .Include(p => p.Movies)
            .FirstOrDefaultAsync();

        if (playlist == null)
        {
            return NotFound("Playlist not found");
        }

        // Fetch the movie from the MovieList table based on the specified movie ID
        var existingMovie = await _context.Movies
            .FirstOrDefaultAsync(m => m.Id == movieId);

        if (existingMovie == null)
        {
            return NotFound("Movie not found");
        }

        // Check if the movie is already in the playlist
        if (playlist.Movies.Any(m => m.Id == existingMovie.Id))
        {
            return BadRequest("Movie already exists in the playlist");
        }

        // Add the existing movie to the playlist
        playlist.Movies.Add(existingMovie);
        await _context.SaveChangesAsync();

        return Ok(playlist);
    }

    [HttpDelete("{id}/remove-movie/{movieId}")]
    public async Task<IActionResult> RemoveMovieFromPlaylist(int id, int movieId)
    {
        var userName = User.Identity.Name;

        var playlist = await _context.PlayLists
            .Where(p => p.UserName == userName && p.Id == id)
            .Include(p => p.Movies)
            .FirstOrDefaultAsync();

        if (playlist == null)
        {
            return NotFound("Playlist not found");
        }

        // Fetch the movie from the playlist based on the specified movie ID
        var movieToRemove = playlist.Movies.FirstOrDefault(m => m.Id == movieId);

        if (movieToRemove == null)
        {
            return NotFound("Movie not found in the playlist");
        }

        // Remove the movie from the playlist
        playlist.Movies.Remove(movieToRemove);
        await _context.SaveChangesAsync();

        return Ok(playlist);
    }

}
