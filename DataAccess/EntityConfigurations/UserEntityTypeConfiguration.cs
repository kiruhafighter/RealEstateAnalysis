using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfigurations;

public sealed class UserEntityTypeConfiguration : BaseEntityConfiguration<User>
{
    protected override void ConfigureEntity(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Id)
            .HasColumnName("UserId");
        
        builder.Property(u => u.Email)
            .HasMaxLength(100);

        builder.Property(u => u.FirstName)
            .HasMaxLength(50);
        
        builder.Property(u => u.LastName)
            .HasMaxLength(50);

        builder.Property(u => u.PasswordHash)
            .HasMaxLength(300);

        builder.Property(u => u.PasswordSalt)
            .HasMaxLength(300);

        builder.HasOne(u => u.Role)
            .WithMany()
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}