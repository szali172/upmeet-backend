using System.ComponentModel.DataAnnotations;
using UpmeetBackend.Models;

namespace UpmeetBackend.Data.Dto;

public class EventDto
{
    [Required]
    [MaxLength(100)]
    public string EventName { get; set; }

    [Required]
    [MaxLength(500)]
    public string EventDescription { get; set; }

    [Required]
    [MaxLength(50)]
    public string EventLocation { get; set; }

    public DateTime EventTime { get; set; }

    internal Event ToEvent()
    {
        throw new NotImplementedException();
    }
}
