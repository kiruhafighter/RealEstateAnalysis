using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfigurations;

public sealed class AgentEntityTypeConfiguration : BaseEntityConfiguration<Agent>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Agent> builder)
    {
        builder.Property(a => a.Id)
            .HasColumnName("AgentId");
        
        builder.Property(a => a.FirstName)
            .HasMaxLength(50);
        
        builder.Property(a => a.LastName)
            .HasMaxLength(50);
        
        builder.Property(a => a.Email)
            .HasMaxLength(60);

        builder.Property(a => a.PhoneNumber)
            .HasMaxLength(15);

        builder.Property(a => a.AgencyName)
            .HasMaxLength(150);

        builder.HasOne(a => a.User)
            .WithOne()
            .HasForeignKey<Agent>(a => a.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(a => a.Properties)
            .WithOne(o => o.Agent)
            .HasForeignKey(o => o.AgentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}