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
    [StringLength(20, MinimumLength = 8)]
    [RegularExpression("^(?=.*[A-Z])(?=.*[.*@!#^&(){}[]\\-$*_+|:;<>,.?~])(.{8,})$")]
    public string Password { get; set; }
    
    public string Role { get; set; }
}