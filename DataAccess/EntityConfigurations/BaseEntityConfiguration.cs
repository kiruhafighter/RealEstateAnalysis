using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfigurations;

public abstract class BaseEntityConfiguration<T>
    : IEntityTypeConfiguration<T>
        where T : class
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        ConfigureEntity(builder);
        Seed(builder);
    }
    
    protected virtual void ConfigureEntity(EntityTypeBuilder<T> builder)
    {
    }
    
    protected virtual void Seed(EntityTypeBuilder<T> builder)
    {
    }
}