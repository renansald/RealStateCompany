using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<CityEntity> Cities { get; set; }

    public DbSet<UserEntity> Users { get; set; }

    public DbSet<PropertyEntity> Properties { get; set; }

    public DbSet<PropertyTypeEntity> PropertyType { get; set; }

    public DbSet<FurnishingTypeEntity> FurnishingType { get; set; }

    public DbSet<PhotosEntity> Photos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>()
            .HasIndex(x => x.Email)
            .IsUnique();
    }
}