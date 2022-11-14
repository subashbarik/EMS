using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class EmployeeTypeConfigurations:BaseEntityConfigurations<EmployeeType>
    {
        public override void Configure(EntityTypeBuilder<EmployeeType> builder)
        {
            base.Configure(builder);
            builder.HasAlternateKey(p => p.Name);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(20);
            builder.Property(p => p.Description).HasMaxLength(500);
        }
    }
}