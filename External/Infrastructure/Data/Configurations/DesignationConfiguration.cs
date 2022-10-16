using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    internal class DesignationConfiguration : IEntityTypeConfiguration<Designation>
    {
        public void Configure(EntityTypeBuilder<Designation> builder)
        {
            builder.Property(p => p.Id).IsRequired().UseIdentityColumn();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(20);
        }
    }
}
