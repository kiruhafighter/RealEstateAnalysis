using DataAccess.Seeds.OfferStatuses;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfigurations;

public sealed class OfferStatusEntityTypeConfiguration : BaseEntityConfiguration<OfferStatus>
{
    protected override void ConfigureEntity(EntityTypeBuilder<OfferStatus> builder)
    {
        builder.Property(os => os.Id)
            .HasColumnName("OfferStatusId")
            .ValueGeneratedOnAdd();

        builder.Property(os => os.StatusName)
            .HasMaxLength(50);
    }

    protected override void Seed(EntityTypeBuilder<OfferStatus> builder)
    {
        var statuses = OfferStatusesDataReader.GetAll();
        
        builder.HasData(statuses);
        
        base.Seed(builder);
    }
}