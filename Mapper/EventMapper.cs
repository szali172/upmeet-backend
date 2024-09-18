using UpmeetBackend.Models;
using UpmeetBackend.Data.Dto;
using System.ComponentModel.DataAnnotations;

namespace UpmeetBackend.Mapper;

public class EventMapper
{   
    public static EventDto ToEventDto(Event eventEntity)
    {
        return new EventDto
        {
            EventName = eventEntity.EventName,
            EventDescription = eventEntity.EventDescription,
            EventLocation = eventEntity.EventLocation,
            EventTime = eventEntity.EventTime
        };
    }

   
    public static Event ToEvent(EventDto eventDto)
    {
        return new Event
        {
            EventName = eventDto.EventName,
            EventDescription = eventDto.EventDescription,
            EventLocation = eventDto.EventLocation,
            EventTime = eventDto.EventTime
        };
    }
}
