using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs;

public class PhotosDTO
{
    public int Id { get; set; }
    [Required]
    public string Url { get; set; }
    [Required]
    public bool IsPrimary { get; set; } = false;
    [Required]
    public int PropertyId { get; set; }
    public string? PhotoUrl { get; set; }
}