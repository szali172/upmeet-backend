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


    /// <summary> Update the passed Event with a given EventDto </summary>
    public static Event UpdateEvent(Event eventEntity, EventDto eventDto)
    {
        eventEntity.EventName = eventDto.EventName;
        eventEntity.EventDescription = eventDto.EventDescription;
        eventEntity.EventLocation = eventDto.EventLocation;
        eventEntity.EventTime = eventDto.EventTime;

        return eventEntity;
    }
}
