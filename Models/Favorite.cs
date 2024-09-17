using System.ComponentModel.DataAnnotations;

namespace UpmeetBackend.Models
{
    public class Favorite
    {
        [Required]
        public int UserId { get; set; }
        public int EventId { get; set; }
    }
}
