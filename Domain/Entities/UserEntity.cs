using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class UserEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 3)]
    public string Name { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public byte[] Password { get; set; }

    public byte[] PasswordKey { get; set; }
    
    public string Role { get; set; }

}