using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class FurnishingTypeEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Name { get; set; }
}