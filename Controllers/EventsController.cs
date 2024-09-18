using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UpmeetBackend.Data;
using UpmeetBackend.Data.Dto;
using UpmeetBackend.Mapper;
using UpmeetBackend.Models;
namespace UpmeetBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private UpmeetDbContext _upmeetDbContext;

    public EventsController(UpmeetDbContext upmeetDbContext)
    {
        _upmeetDbContext = upmeetDbContext;
    }



    [HttpGet]
    public async Task<IActionResult> GetAllEvents()
    {
        return Ok(await _upmeetDbContext.Events.ToListAsync());
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetEventById(int id)
    {
        Event? eventEntity = await _upmeetDbContext.Events.FindAsync(id);

        if (eventEntity == null)
        {
            return NotFound(); 
        }

        return Ok(eventEntity);
    }


    [HttpPost]
    public async Task<IActionResult> CreateEvent([FromBody] EventDto eventDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); 
        }

        Event eventEntity = EventMapper.ToEvent(eventDto);

        await _upmeetDbContext.Events.AddAsync(eventEntity);
        await _upmeetDbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEventById), new { id = eventEntity.EventId }, eventEntity);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(int id, [FromBody] EventDto eventDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); 
        }

        Event? eventEntity = await _upmeetDbContext.Events.FindAsync(id);

        if (eventEntity == null)
        {
            return NotFound(); 
        }

        Event updatedEvent = EventMapper.UpdateEvent(eventEntity, eventDto);

        _upmeetDbContext.Update(updatedEvent);
        await _upmeetDbContext.SaveChangesAsync();

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        var eventEntity = await _upmeetDbContext.Events.FindAsync(id);

        if (eventEntity == null)
        {
            return NotFound(); 
        }

        _upmeetDbContext.Events.Remove(eventEntity);
        await _upmeetDbContext.SaveChangesAsync(); 

        return NoContent(); 
    }

}
