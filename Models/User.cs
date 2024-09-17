using System.ComponentModel.DataAnnotations;

namespace UpmeetBackend.Models;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required]
    [MaxLength(100)]
    public string UserName { get; set; }
}
