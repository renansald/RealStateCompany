using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class PropertyEntity
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int SellRent { get; set; }
    
    [Required]
    [StringLength(30, MinimumLength = 3)]
    public string Name { get; set; }
    
    [Required]
    [ForeignKey("PropertyTypeEntity")]
    public int PropertyTypeId { get; set; }
    
    public PropertyTypeEntity PropertyType { get; set; }

    public int BHK { get; set; }

    [Required]
    public int FurnishingTypeId { get; set; }

    public FurnishingTypeEntity FurnishingType { get; set; }
    
    [Required]
    public decimal Price { get; set; }

    [Required]
    public double BuiltArea { get; set; }

    [Required]
    public double CarpetArea { get; set; }

    [Required]
    [StringLength(50)]
    public string Address { get; set; }

    [StringLength(50)]
    public string Address2 { get; set; }

    [ForeignKey("CityEntity")]
    public int CityId { get; set; }

    public CityEntity City { get; set; }

    [Required]
    public int FloorNumber { get; set; }

    [Required]
    public int FloorTotal { get; set; }

    [Required]
    public bool ReadyToMove { get; set; }

    [Required]
    [StringLength(50)]
    public string MainEnrace { get; set; }

    [Required]
    public int Security { get; set; }

    [Required]
    public bool Gated { get; set; }

    [Required]
    public int Maintenance { get; set; }

    [Required]
    public DateTime EstPossessionOn { get; set; }

    public int Age { get; set; }

    [Required]
    [StringLength(300)]
    public string Description { get; set; }

    public ICollection<PhotosEntity> Photos { get; set; }

    public DateTime PostedOn { get; set; } = DateTime.Now;

    [Required]
    [ForeignKey("UserEntity")]
    public int PostedBy { get; set; }

    public UserEntity User { get; set; }
}