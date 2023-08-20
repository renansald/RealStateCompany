using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs;

public class PropertyTypeDTO
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
}