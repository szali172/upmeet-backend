using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UpmeetBackend.Data;
using UpmeetBackend.Data.Dto;
using UpmeetBackend.Models;

namespace UpmeetBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FavoritesController : ControllerBase
{
    private UpmeetDbContext _upmeetDbContext;

    public FavoritesController(UpmeetDbContext upmeetDbContext)
    {
        _upmeetDbContext = upmeetDbContext;
    }


    [HttpGet("{userId}")]
    public async Task<IActionResult> GetFavoritesByUserId(int userId)
    {
        // Grab the users favorite events
        IQueryable<Favorite> userFavorites = _upmeetDbContext.Favorites.Where(x => x.UserId == userId);

        var result = from favorites in userFavorites
                     join event_ in _upmeetDbContext.Events on favorites.EventId equals event_.EventId into FavoritedEvents
                     from m in FavoritedEvents.DefaultIfEmpty()
                     select new
                     {
                         EventId = favorites.EventId,
                         EventName = m.EventName,
                         EventDescription = m.EventDescription,
                         EventLocation = m.EventLocation,
                         EventTime = m.EventTime
                     };

        return Ok(await result.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> CreateFavorite([FromBody] Favorite favorite)
    {
        Favorite newFavorite = new Favorite();

        newFavorite.UserId = favorite.UserId;
        newFavorite.EventId = favorite.EventId;

        _upmeetDbContext.Add(newFavorite);

        await _upmeetDbContext.SaveChangesAsync();

        return Ok(newFavorite);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFavorite(int id)
    {
        var favoriteEntity = await _upmeetDbContext.Favorites.FindAsync(id);

        if (favoriteEntity == null)
        {
            return NotFound();
        }

        _upmeetDbContext.Favorites.Remove(favoriteEntity);
        await _upmeetDbContext.SaveChangesAsync();

        return NoContent();
    }

}
