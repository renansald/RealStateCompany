using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class PhotosEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Url { get; set; }

    [Required]
    [DefaultValue(false)]
    public bool IsPrimary { get; set; } = false;

    [ForeignKey("PropertyEntity")]
    public int PropertyId { get; set; }

    public PropertyEntity Property { get; set; }

}