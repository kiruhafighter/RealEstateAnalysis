using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfigurations;

public sealed class OfferEntityTypeConfiguration : BaseEntityConfiguration<Offer>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Offer> builder)
    {
        builder.Property(o => o.Id)
            .HasColumnName("OfferId");

        builder.Property(o => o.OfferAmount)
            .HasPrecision(10, 2);
        
        builder.Property(o => o.OfferDate)
            .HasDefaultValueSql("GETDATE()");

        builder.Property(o => o.Comment)
            .HasMaxLength(500);

        builder.HasOne(o => o.OfferStatus)
            .WithMany()
            .HasForeignKey(o => o.OfferStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.User)
            .WithMany()
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}