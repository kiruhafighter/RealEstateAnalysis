using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

internal sealed class RealEstateDBContext : DbContext
{
    public RealEstateDBContext(DbContextOptions<RealEstateDBContext> options) : base(options)
    {
        
    }
    
    public DbSet<Agent> Agents { get; set; } = null!;
    
    public DbSet<Offer> Offers { get; set; } = null!;
    
    public DbSet<OfferStatus> OfferStatuses { get; set; } = null!;
    
    public DbSet<Property> Properties { get; set; } = null!;
    
    public DbSet<PropertyStatus> PropertyStatuses { get; set; } = null!;
    
    public DbSet<PropertyType> PropertyTypes { get; set; } = null!;
    
    public DbSet<Role> Roles { get; set; } = null!;
    
    public DbSet<User> Users { get; set; } = null!;
    
    public DbSet<Image> Images { get; set; } = null!;
    
    public DbSet<UsersFavourite> UsersFavourites { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RealEstateDBContext).Assembly);
    }
}