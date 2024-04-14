using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyStatus = DataAccess.Enums.PropertyStatus;

namespace DataAccess.EntityConfigurations;

public sealed class PropertyEntityTypeConfiguration : BaseEntityConfiguration<Property>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Property> builder)
    {
        builder.Property(p => p.Id)
            .HasColumnName("PropertyId");

        builder.Property(p => p.Name)
            .HasMaxLength(150);

        builder.Property(p => p.Address)
            .HasMaxLength(200);

        builder.Property(p => p.Description)
            .HasMaxLength(500);

        builder.Property(p => p.County)
            .HasMaxLength(50);

        builder.Property(p => p.Country)
            .HasMaxLength(50);
        
        builder.Property(p => p.Locality)
            .HasMaxLength(50);
        
        builder.Property(p => p.Postcode)
            .HasMaxLength(20);
        
        builder.HasOne(p => p.PropertyType)
            .WithMany()
            .HasForeignKey(p => p.PropertyTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.PropertyStatus)
            .WithMany()
            .HasForeignKey(p => p.PropertyStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(p => p.PropertyStatusId)
            .HasDefaultValue(PropertyStatus.Active);

        builder.Property(p => p.Price)
            .HasPrecision(10, 2);

        builder.HasMany(p => p.Images)
            .WithOne()
            .HasForeignKey(i => i.PropertyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Offers)
            .WithOne(o => o.Property)
            .HasForeignKey(o => o.PropertyId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.ToTable("Properties", b => 
            b.IsTemporal(temporal =>
            {
                temporal.UseHistoryTable("PropertyHistory");
                temporal.HasPeriodStart("ValidFrom");
                temporal.HasPeriodEnd("ValidTo");
            }));
    }
}