using DataAccess.Seeds.Roles;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfigurations;

public sealed class RoleEntityTypeConfiguration : BaseEntityConfiguration<Role>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Role> builder)
    {
        builder.Property(r => r.Id)
            .HasColumnName("RoleId")
            .ValueGeneratedOnAdd();

        builder.Property(r => r.RoleName)
            .HasMaxLength(50);
    }

    protected override void Seed(EntityTypeBuilder<Role> builder)
    {
        var roles = RolesDataReader.GetAll();

        builder.HasData(roles);
        
        base.Seed(builder);
    }
}