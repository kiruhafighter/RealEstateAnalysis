using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfigurations;

public sealed class UsersFavouriteEntityTypeConfiguration : BaseEntityConfiguration<UsersFavourite>
{
    protected override void ConfigureEntity(EntityTypeBuilder<UsersFavourite> builder)
    {
        builder.Property(uf => uf.Id)
            .HasColumnName("UsersFavouriteId")
            .ValueGeneratedOnAdd();

        builder.HasOne(uf => uf.User)
            .WithMany(u => u.UsersFavourites)
            .HasForeignKey(uf => uf.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(uf => uf.Property)
            .WithMany()
            .HasForeignKey(uf => uf.PropertyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}