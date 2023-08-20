﻿using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class PropertyTypeEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
}