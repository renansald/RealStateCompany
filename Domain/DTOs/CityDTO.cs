using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs;

public class CityDTO
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "City name is required")]
    [StringLength(50, MinimumLength = 3)]
    public string Name { get; set; }
}