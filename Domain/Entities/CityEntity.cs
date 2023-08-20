using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("Cities")]
public class CityEntity
{
    [Key]
    public int Id { get; set; }

    [DisplayName("City Name")]
    [Required(ErrorMessage = "Cities must be a name")]
    public string Name { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 3)]
    public string Country { get; set; }
}