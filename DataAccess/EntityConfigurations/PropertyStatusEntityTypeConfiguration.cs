using DataAccess.Seeds.PropertyStatuses;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfigurations;

public sealed class PropertyStatusEntityTypeConfiguration : BaseEntityConfiguration<PropertyStatus>
{
    protected override void ConfigureEntity(EntityTypeBuilder<PropertyStatus> builder)
    {
        builder.Property(ps => ps.Id)
            .HasColumnName("PropertyStatusId")
            .ValueGeneratedOnAdd();

        builder.Property(ps => ps.StatusName)
            .HasMaxLength(50);
    }

    protected override void Seed(EntityTypeBuilder<PropertyStatus> builder)
    {
        var statuses = PropertyStatusesDataReader.GetAll();
        
        builder.HasData(statuses);
        
        base.Seed(builder);
    }
}