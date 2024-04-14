using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfigurations;

public sealed class ImageEntityTypeConfiguration : BaseEntityConfiguration<Image>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Image> builder)
    {
        builder.Property(i => i.Id)
            .HasColumnName("ImageId")
            .ValueGeneratedOnAdd();

        builder.Property(i => i.ImagePath)
            .HasMaxLength(200);
    }
}