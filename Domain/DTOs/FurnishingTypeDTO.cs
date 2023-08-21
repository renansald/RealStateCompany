using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs;

public class FurnishingTypeDTO
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
}