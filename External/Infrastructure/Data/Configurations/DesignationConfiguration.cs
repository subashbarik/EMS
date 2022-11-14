using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class DesignationConfiguration : BaseEntityConfigurations<Designation>
    {
        public override void Configure(EntityTypeBuilder<Designation> builder)
        {
            base.Configure(builder);
            builder.HasAlternateKey(p => p.Name);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(20);
            builder.Property(p => p.Basic).HasDefaultValue(0);
            builder.Property(p => p.TAPercentage).HasDefaultValue(0);
            builder.Property(p => p.DAPercentage).HasDefaultValue(0);
            builder.Property(p => p.HRAPercentage).HasDefaultValue(0);
            builder.Property(p => p.Description).HasMaxLength(500);
        }
    }
}
