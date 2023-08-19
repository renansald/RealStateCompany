using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs;

public class LoginDTO
{
    [Required]
    [StringLength(50, MinimumLength=3)]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}