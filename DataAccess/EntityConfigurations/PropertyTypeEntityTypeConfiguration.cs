using DataAccess.Seeds.PropertyTypes;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfigurations;

public sealed class PropertyTypeEntityTypeConfiguration : BaseEntityConfiguration<PropertyType>
{
    protected override void ConfigureEntity(EntityTypeBuilder<PropertyType> builder)
    {
        builder.Property(pt => pt.Id)
            .HasColumnName("PropertyTypeId")
            .ValueGeneratedOnAdd();
        
        builder.Property(pt => pt.TypeName)
            .HasMaxLength(50);
    }

    protected override void Seed(EntityTypeBuilder<PropertyType> builder)
    {
        var types = PropertyTypesDataReader.GetAll();
        
        builder.HasData(types);
        
        base.Seed(builder);
    }
}