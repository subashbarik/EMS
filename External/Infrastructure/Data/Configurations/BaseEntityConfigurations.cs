using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class BaseEntityConfigurations<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity:BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
             builder.Property(p => p.Id).IsRequired().UseIdentityColumn();
             builder.Property(p => p.CreatedDate).IsRequired().HasDefaultValueSql("getdate()");
             builder.Property(p => p.UpdatedDate).IsRequired().HasDefaultValueSql("getdate()");
        }
    }
}