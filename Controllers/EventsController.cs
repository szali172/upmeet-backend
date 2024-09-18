using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UpmeetBackend.Data;
using UpmeetBackend.Data.Dto;
using UpmeetBackend.Mapper;
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
       
        var events = await _upmeetDbContext.Events.ToListAsync();

      
        var eventDtos = events.Select(eventEntity => eventEntity.ToEvent()).ToList();

     
        return Ok(eventDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEventById(int id)
    {
        var eventEntity = await _upmeetDbContext.Events.FindAsync(id);

        if (eventEntity == null)
        {
            return NotFound(); 
        }

        var eventDto = eventEntity.ToEvent();
        return Ok(eventDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent([FromBody] EventDto eventDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); 
        }

        var eventEntity = eventDto.ToEvent(); 

        _upmeetDbContext.Events.Add(eventEntity);
        await _upmeetDbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEventById), new { id = eventEntity.EventId }, eventEntity.ToEvent());
      
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(int id, [FromBody] EventDto eventDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); 
        }

        var eventEntity = await _upmeetDbContext.Events.FindAsync(id);
        if (eventEntity == null)
        {
            return NotFound(); 
        }

        
        eventEntity.EventName = eventDto.EventName;
        eventEntity.EventDescription = eventDto.EventDescription;
        eventEntity.EventLocation = eventDto.EventLocation;
        eventEntity.EventTime = eventDto.EventTime;

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
