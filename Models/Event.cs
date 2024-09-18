using System.ComponentModel.DataAnnotations;

namespace UpmeetBackend.Models;

public class Event
{
    [Key]
    public int EventId {  get; set; }

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

}
