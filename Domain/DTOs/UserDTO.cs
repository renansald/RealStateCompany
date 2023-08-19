using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs;

public class UserDTO
{
    public int Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Name { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Role { get; set; }

    [Required]
    public string Password { get; set; }
}