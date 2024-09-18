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

    [HttpGet]
    public async Task<IActionResult> GetAllFavorites()
    {
        return Ok(_upmeetDbContext.Favorites.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetFavoriteById(int id)
    {
        var favoriteEntity = await _upmeetDbContext.Favorites.FindAsync(id);

        if (favoriteEntity == null)
        {
            return NotFound();
        }

        return Ok(favoriteEntity);
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
